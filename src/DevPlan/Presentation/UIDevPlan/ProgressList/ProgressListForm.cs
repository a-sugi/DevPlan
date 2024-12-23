/*
 * 開発計画表システム
 * 目標進度リスト
 */ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    /// <summary>
    /// 目標進度リスト
    /// </summary>
    public partial class ProgressListForm : BaseForm
    {
        #region メンバ変数
        private List<TargetProgressListNameSearchOutModel> progressListNameList;

        private List<TargetProgressListSearchOutModel> progressList;

        private bool gridModifyFlg = false;

        private int seasonCount = 0;

        private enum CursolType
        {
            Normal,
            Circle,
            Cross,
            NotYet,
            NA,
            Triangle,
            Square
        }

        private readonly Dictionary<CursolType, string> CursolTypeMap = new Dictionary<CursolType, string>
        {
            { CursolType.Normal, "↑" },
            { CursolType.Circle, "○" },
            { CursolType.Cross, "×" },
            { CursolType.NotYet, "未" },
            { CursolType.NA, "－" },
            { CursolType.Triangle, "△" },
            { CursolType.Square, "□" },
        };

        private Dictionary<CursolType, Cursor> cursolMap = null;

        private CursolType cursolType = CursolType.Normal;

        private enum EditModeType : int
        {
            Normal = 0,
            BatchInput = 1,
            InvisibleItemDisplay = 2,
            MultiDelete = 3,
            ItemMove = 4
        }

        private EditModeType editMode = EditModeType.Normal;

        private string currentCheckListName;

        private string currentGeneralCode = "";

        private readonly string[] EditModeSeinou = { "通常", "関連課一括入力モード", "非表示項目表示モード", "複数項目削除モード", "項目移動モード" };
        private readonly string[] EditModeNone = { "通常", "関連課一括入力モード", "非表示項目表示モード" };

        private long mokuhyouSindoId = -1;

        private Point dragStartPosition = Point.Empty;

        private bool isBind = false;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "目標進度リスト"; } }

        /// <summary>フォーム横幅</summary>
        public override int FormWidth { get { return 1200; } }

        /// <summary>フォーム縦幅</summary>
        public override int FormHeight { get { return 700; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressListForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //編集モードで通常を選択
            this.EditModeComboBox.DataSource = EditModeSeinou;
            this.EditModeComboBox.SelectedIndex = 0;

            //ダブルバァッファリング有効化
            var type = typeof(DataGridView);
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.ProgressListDataGridView, true);

            // 自動カラム追加を無効化
            this.CheckListDataGridView.AutoGenerateColumns = false;
            this.ProgressListDataGridView.AutoGenerateColumns = false;

            // グリッドタイトルの設定
            this.SetTitleProgressListGrid();

            // 目標進度チェックリスト一覧の取得
            this.RefreshCheckList();

            //カーソル連想配列配列初期化
            this.InitCursolMap();

            //権限初期設定
            this.InitAuthority();

        }

        /// <summary>
        /// カーソル連想配列配列初期化
        /// </summary>
        private void InitCursolMap()
        {
            Func<byte[], Cursor> getCursor = cursor =>
            {
                using (var ms = cursor.ToMemoryStream())
                {
                    return new Cursor(ms);

                }
            };

            //カーソルタイプごとのカーソル
            this.cursolMap = new Dictionary<CursolType, Cursor>
            {
                { CursolType.Normal, Cursors.Arrow },
                { CursolType.Circle, getCursor(Properties.Resources.arrow_o) },
                { CursolType.Cross, getCursor(Properties.Resources.arrow_x) },
                { CursolType.NotYet, getCursor(Properties.Resources.arrow_me) },
                { CursolType.NA, getCursor(Properties.Resources.arrow__) },
                { CursolType.Triangle, getCursor(Properties.Resources.arrow_triangle) },
                { CursolType.Square, getCursor(Properties.Resources.arrow_square) }

            };
        }

        /// <summary>
        /// 権限初期設定
        /// </summary>
        private void InitAuthority()
        {
            //権限
            this.UserAuthority = base.GetFunction(FunctionID.GoalProgress);

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //目標進度一覧
            this.ProgressListDataGridView.ReadOnly = true;

            //登録ボタン
            this.RegistrationButton.Visible = isUpdate;

            //目標進度リスト追加ボタン
            this.MakeListButton.Visible = isManagement;

            //マスタ編集ボタン
            this.EditMasterButton.Visible = isManagement;

            //ダウンロードボタン
            this.DownLoadButton.Visible = isExport;

        }
        #endregion

        #region チェックリスト一覧の更新
        /// <summary>
        /// チェックリスト一覧の更新
        /// </summary>
        private void RefreshCheckList()
        {
            //リスト一覧更新前に変更チェックと右グリッドクリア処理実施。
            ConfirmRegist(Resources.KKM03007);
            ProgressListDataGridView.DataSource = null;
            if (progressList != null)
            {
                progressList.Clear();
            }
            SetTitleProgressListGrid();

            // APIから目標進度リスト一覧取得
            FormControlUtil.FormWait(this, () => progressListNameList = GetCheckList());

            // グリッドのデータソース設定
            CheckListDataGridView.DataSource = progressListNameList;

            //一覧を未選択状態に変更
            CheckListDataGridView.CurrentCell = null;

        }
        #endregion

        #region 目標進度リストの更新
        /// <summary>
        /// 目標進度リストの更新
        /// </summary>
        private void RefreshProgressList()
        {
            // APIから目標進度リストの内容を取得
            FormControlUtil.FormWait(this, () => progressList = this.GetProgressList());

            // グリッドタイトルの設定
            this.SetTitleProgressListGrid();

            //グリッドの設定
            this.InvisibleGrid();

        }
        #endregion

        #region 目標進度リスト一覧の取得
        /// <summary>
        /// 目標進度リスト一覧の取得
        /// </summary>
        /// <param name="checkListNameId">目標進度リスト名ID</param>
        /// <returns>List</returns>
        private List<TargetProgressListNameSearchOutModel> GetCheckList(int? checkListNameId = null)
        {
            var cond = new TargetProgressListNameSearchInModel()
            {
                DEPARTMENT_ID = SessionDto.DepartmentID,
                PERSONEL_ID = SessionDto.UserId,
                DIVISION_CATEGORY = FormControlUtil.GetRadioButtonValue(this.DivisionCategoryPanel),
                PROCESS_CATEGORY = FormControlUtil.GetRadioButtonValue(this.ProcessCategoryPanel)
            };

            //目標進度リスト名IDで取得するかどうか
            if (checkListNameId != null)
            {
                //目標進度リスト名ID
                cond.CHECKLIST_NAME_ID = checkListNameId;

            }

            var res = HttpUtil.GetResponse<TargetProgressListNameSearchInModel, TargetProgressListNameSearchOutModel>(ControllerType.TargetProgressListName, cond);

            return res.Results.ToList();

        }
        #endregion

        #region 目標進度リストの取得
        /// <summary>
        /// 目標進度リストの取得
        /// </summary>
        /// <returns>List</returns>
        private List<TargetProgressListSearchOutModel> GetProgressList()
        {
            var row = this.CheckListDataGridView.CurrentCell.OwningRow;

            var cond = new TargetProgressListSearchInModel()
            {
                GENERAL_CODE = this.currentGeneralCode,
                CHECKLIST_NAME_ID = Convert.ToInt32(row.Cells[this.ChecklistNameIdColumn.Name].Value),
                DEPARTMENT_ID = Convert.ToString(row.Cells[this.DepartmentIdColumn.Name].Value),
                SECTION_ID = Convert.ToString(row.Cells[this.SectionIdColumn.Name].Value),
                SECTION_CD = SessionDto.SectionCode,
                DISPLAY_MODE = FormControlUtil.GetRadioButtonValue(ProcessCategoryPanel),
                RELATION_DISPLAY_FLAG = FormControlUtil.GetRadioButtonValue(RelationalDivisionPanel)

            };

            var res = HttpUtil.GetResponse<TargetProgressListSearchInModel, TargetProgressListSearchOutModel>(ControllerType.TargetProgressList, cond);

            return res.Results.ToList();

        }
        #endregion

        #region 上のセルと同内容ならば結合（しているように見せる）
        /// <summary>
        /// 目標進度一覧セル描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 「大項目」「中項目」「課」「性能名」以外は何もしない
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 &&
                ProgressListDataGridView.Columns.Count > 0)
            {

                // 非表示項目の背景色をグレーに設定
                var id = Convert.ToInt64(this.ProgressListDataGridView.Rows[e.RowIndex].Cells["CHECKLIST_ITEMNAME_ID"].Value);
                if (progressList.FirstOrDefault(x => x.CHECKLIST_ITEMNAME_ID == id)?.ITEM_FLAG_DISP == 0)
                {
                    e.CellStyle.BackColor = Color.LightGray;
                }

                if (ProgressListDataGridView.Columns[e.ColumnIndex].Name == "LARGE_CLASSIFICATION" ||
                //ProgressListDataGridView.Columns[e.ColumnIndex].Name == "MIDDLE_CLASSIFICATION" ||
                ProgressListDataGridView.Columns[e.ColumnIndex].Name == "SECTION_CODE" ||
                ProgressListDataGridView.Columns[e.ColumnIndex].Name == "SPEC_NAME")
                {

                    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                    {
                        e.CellStyle.ForeColor = e.CellStyle.BackColor;
                        e.FormattingApplied = true;
                    }
                }
                else if (ProgressListDataGridView.Columns[e.ColumnIndex].Name == "MIDDLE_CLASSIFICATION")
                {

                    if (IsTheSameCellValue(e.ColumnIndex - 1, e.RowIndex) && IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                    {
                        e.CellStyle.ForeColor = e.CellStyle.BackColor;
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        /// <summary>
        /// CellPaintingイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 「大項目」「中項目」「課」「性能名」以外は何もしない
            if (e.ColumnIndex >= 0 &&
                e.RowIndex >= 0 &&
                ProgressListDataGridView.Columns.Count > 0 &&
            (ProgressListDataGridView.Columns[e.ColumnIndex].Name == "LARGE_CLASSIFICATION" ||
            ProgressListDataGridView.Columns[e.ColumnIndex].Name == "MIDDLE_CLASSIFICATION" ||
            ProgressListDataGridView.Columns[e.ColumnIndex].Name == "SECTION_CODE" ||
            ProgressListDataGridView.Columns[e.ColumnIndex].Name == "SPEC_NAME"))
            {
                // セルの下側の境界線を「境界線なし」に設定
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

                if (ProgressListDataGridView.Columns[e.ColumnIndex].Name == "MIDDLE_CLASSIFICATION")
                {
                    if (e.RowIndex == 0 || (IsTheSameCellValue(e.ColumnIndex - 1, e.RowIndex) && IsTheSameCellValue(e.ColumnIndex, e.RowIndex)))
                    {
                        // セルの上側の境界線を「境界線なし」に設定
                        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

                    }
                    else
                    {
                        // セルの上側の境界線を既定の境界線に設定
                        e.AdvancedBorderStyle.Top = ProgressListDataGridView.AdvancedCellBorderStyle.Top;

                    }

                }
                else
                {
                    if (e.RowIndex == 0 || IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                    {
                        // セルの上側の境界線を「境界線なし」に設定
                        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

                    }
                    else
                    {
                        // セルの上側の境界線を既定の境界線に設定
                        e.AdvancedBorderStyle.Top = ProgressListDataGridView.AdvancedCellBorderStyle.Top;

                    }

                }

            }

            //最終行は下線を設定
            if (ProgressListDataGridView.RowCount > 0 && e.RowIndex + 1 == ProgressListDataGridView.RowCount)
            {
                e.AdvancedBorderStyle.Bottom = ProgressListDataGridView.AdvancedCellBorderStyle.Bottom;

            }

        }

        /// <summary>
        /// 上のセルと同内容ならば結合（しているように見せる）
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsTheSameCellValue(int column, int row)
        {
            if (column < 0 || row < 1)
            {
                return false;
            }

            var cell1 = ProgressListDataGridView[column, row];
            var cell2 = ProgressListDataGridView[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            // セルの値を比較
            return (cell1.Value.ToString() == cell2.Value.ToString());

        }
        #endregion

        #region カーソルタイプボタン
        /// <summary>
        /// カーソルタイプボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CursorTypeButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag == null ? "" : button.Tag.ToString();

            //タグが存在するかどうか
            if (CursolTypeMap.Values.Contains(tag) == true)
            {
                cursolType = CursolTypeMap.First(x => x.Value == tag).Key;

            }
            else
            {
                cursolType = CursolType.Normal;

            }

            //目標進度一覧のカーソル変更
            this.ProgressListDataGridView.Cursor = this.cursolMap[cursolType];

        }
        #endregion

        #region ダウンロードボタンクリック
        /// <summary>
        /// ダウンロードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownLoadButton_Click(object sender, EventArgs e)
        {
            // 今バージョンではボタン表示のみ
        }
        #endregion

        #region 種別ラジオボタンクリック
        /// <summary>
        /// 「性能別」ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeRadioButton_Click(object sender, EventArgs e)
        {
            //チェックリスト一覧の更新
            this.RefreshCheckList();

            //関連課
            this.RelationalDivisionPanel.Enabled = !this.BySpecRadioButton.Checked;

            //関連課が使用不可かどうか
            if (this.RelationalDivisionPanel.Enabled == false)
            {
                this.AllRadioButton.Checked = true;

            }

            //編集モードを再設定
            this.EditModeComboBox.DataSource = (this.BySpecRadioButton.Checked == true) ? EditModeSeinou : EditModeNone;
            this.EditModeComboBox.SelectedIndex = 0;

        }
        #endregion

        #region 関連課ラジオボタンクリック
        /// <summary>
        /// 関連課ラジオボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelationRadioButton_Click(object sender, EventArgs e)
        {
            //チェックリストを選択しているかどうか
            if (this.CheckListDataGridView.CurrentCell != null)
            {
                //目標進度リスト再表示
                this.RefreshProgressList();

            }

        }
        #endregion

        #region 目標進度リスト一覧のセルクリック→目標進度リストの表示
        /// <summary>
        /// 目標進度リスト一覧のセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ConfirmRegist(Resources.KKM03007);

            if (e.RowIndex >= 0)
            {
                // カレントチェックリスト名の設定
                currentCheckListName = progressListNameList[e.RowIndex].CHECKLIST_NAME;

                // カレント開発符号の設定
                currentGeneralCode = progressListNameList[e.RowIndex].GENERAL_CODE;

                // ラベルにチェックリスト名を表示
                ProgressListNameLabel.Text = string.Format("目標進度リスト({0})", currentCheckListName);

                // 目標進度リストの取得
                var mokuhyou = progressListNameList[e.RowIndex];
                this.RefreshProgressList();

            }
        }
        #endregion

        #region 保存していない情報がある場合の確認ポップアップ
        /// <summary>
        /// 保存していない情報がある場合の確認ポップアップ
        /// </summary>
        private void ConfirmRegist(string messageText)
        {
            if (gridModifyFlg)
            {
                if (Messenger.Confirm(messageText) == DialogResult.Yes)
                {
                    FormControlUtil.FormWait(this, () => UpdateProgressList());
                   
                }
            }

            gridModifyFlg = false;

        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistrationButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //入力がOKかどうか
                var msg = Validator.GetFormInputErrorMessage(this);
                if (msg != "")
                {
                    Messenger.Warn(msg);
                    return;

                }

                //目標進度リスト更新
                this.UpdateProgressList();

                //目標進度リストを選択しているかどうか
                if (CheckListDataGridView.SelectedCells.Count > 0)
                {
                    // 目標進度リストの取得
                    var mokuhyou = progressListNameList[CheckListDataGridView.SelectedCells[0].RowIndex];
                    this.RefreshProgressList();

                }

            });

        }

        /// <summary>
        /// 目標進度リスト更新処理
        /// </summary>
        private void UpdateProgressList()
        {
            Func<string, string> lineEmpty = s => string.IsNullOrWhiteSpace(s) == true ? "" : s.Replace(Const.Cr, "").Replace(Const.Lf, "");

            //対象が無ければ終了
            if (this.progressList == null || this.progressList.Any() == false)
            {
                return;

            }

            // 更新APIに渡すデータ群
            var list = new List<TargetProgressListUpdateInModel>();

            var i = 0;

            foreach (var pl in progressList)
            {
                var model = new TargetProgressListUpdateInModel
                {
                    CHECKLIST_NAME_ID = pl.CHECKLIST_NAME_ID,
                    CHECKLIST_ITEMNAME_ID = pl.CHECKLIST_ITEMNAME_ID < 0 ? 0 : pl.CHECKLIST_ITEMNAME_ID,
                    LARGE_CLASSIFICATION = pl.LARGE_CLASSIFICATION,
                    MIDDLE_CLASSIFICATION = pl.MIDDLE_CLASSIFICATION,
                    SMALL_CLASSIFICATION = pl.SMALL_CLASSIFICATION,
                    SORT_NO = i++,
                    TARGET_VALUE = pl.TARGET_VALUE,
                    ACHIEVED_VALUE = pl.ACHIEVED_VALUE,
                    DISPLAY_FLAG = pl.ITEM_FLAG_DISP,
                    RELATIONAL_DIVISION_CODE = pl.RELATIONAL_DIVISION_CODE,
                    EDITED_DATE = pl.EDITED_DATE,
                    EDITOR_ID = pl.EDITOR_LOGIN_ID,
                    EDITOR_LOGIN_ID = pl.EDITOR_LOGIN_ID,
                    CHECKLIST_ID = pl.CHECKLIST_ID,
                    CONFIRM_SEASON_ID = pl.CONFIRM_SEASON_ID,
                    CONFIRM_SEASON_NAME = pl.CONFIRM_SEASON_NAME,
                    SEASON_SEQUENCE = pl.SEASON_SEQUENCE,
                    CONFIRM_RESULTS = pl.CONFIRM_RESULTS

                };

                //大項目
                model.LARGE_CLASSIFICATION = lineEmpty(model.LARGE_CLASSIFICATION);

                //中項目
                model.MIDDLE_CLASSIFICATION = lineEmpty(model.MIDDLE_CLASSIFICATION);

                //小項目
                model.SMALL_CLASSIFICATION = lineEmpty(model.SMALL_CLASSIFICATION);

                //目標値
                model.TARGET_VALUE = lineEmpty(model.TARGET_VALUE);

                //達成値
                model.ACHIEVED_VALUE = lineEmpty(model.ACHIEVED_VALUE);

                list.Add(model);

            }

            // 更新APIの実行
            var res = HttpUtil.PutResponse(ControllerType.TargetProgressList, list);

            if (res.Status == Const.StatusSuccess)
            {
                // 正常処理の場合はメッセージ
                Messenger.Info(Resources.KKM00002);

            }

        }

        #endregion

        #region 進度リスト作成ボタンクリック
        /// <summary>
        /// 進度リスト作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeListButton_Click(object sender, EventArgs e)
        {
            MakeNewProgressList();
        }
        #endregion

        #region マスタ編集ボタンクリック
        /// <summary>
        /// マスタ編集ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMasterButton_Click(object sender, EventArgs e)
        {
            using (var frm = new ProgressListMasterEditForm())
            {
                frm.ShowDialog();
            }
        }
        #endregion

        #region チェックリスト右クリックメニュー「名称変更」クリック
        /// <summary>
        /// チェックリスト右クリックメニュー「名称変更」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListToolStripChangeName_Click(object sender, EventArgs e)
        {
            //登録可能かどうか
            var checkListNameId = Convert.ToInt32(this.CheckListContextMenuStrip.Tag);
            if (this.IsEntryMokuhyouCheckList(checkListNameId) == true)
            {
                //目標進度リスト名称変更処理
                this.RenameProgressList(checkListNameId);

            }
            else
            {
                // 目標進度チェックリスト一覧の取得
                this.RefreshCheckList();

            }

        }

        /// <summary>
        /// 目標進度リスト名称変更処理
        /// </summary>
        /// <param name="checkListNameId">目標進度リスト名ID</param>
        private void RenameProgressList(int checkListNameId)
        {
            var listName = progressListNameList.First(x => x.CHECKLIST_NAME_ID == checkListNameId);

            using (var frm = new ProgressListNameForm { ListNameID = listName.CHECKLIST_NAME_ID, ListName = listName.CHECKLIST_NAME })
            {
                //OKかどうか
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    //チェックリスト一覧再設定
                    this.RefreshCheckList();

                }

            }

        }
        #endregion

        #region チェックリスト右クリックメニュー「削除」クリック
        /// <summary>
        /// チェックリスト右クリックメニュー「削除」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListToolStripDelete_Click(object sender, EventArgs e)
        {
            var checkListNameId = Convert.ToInt32(this.CheckListContextMenuStrip.Tag);

            //登録可能かどうか
            if (this.IsEntryMokuhyouCheckList(checkListNameId) == true)
            {
                //目標進度リスト削除処理
                this.DeleteProgressList(checkListNameId);

            }
            else
            {
                // 目標進度チェックリスト一覧の取得
                this.RefreshCheckList();

            }

        }

        /// <summary>
        /// 目標進度リスト削除処理
        /// </summary>
        /// <param name="checkListNameId">目標進度リスト名ID</param>

        private void DeleteProgressList(int checkListNameId)
        {
            // 確認メッセージ
            if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
            {
                // 削除APIに渡すパラメータセット
                var cond = new TargetProgressListDeleteInModel()
                {
                    CHECKLIST_NAME_ID = checkListNameId

                };

                // 削除APIの実行
                var res = HttpUtil.DeleteResponse<TargetProgressListDeleteInModel>(ControllerType.TargetProgressList, cond);

                //削除に成功したかどうか
                if (res.Status == Const.StatusSuccess)
                {
                    // チェックリスト一覧のリフレッシュ
                    RefreshCheckList();

                    //目標進度リストのクリア
                    if (progressList != null)
                    {
                        progressList.Clear();
                    }

                    // 目標進度リスト一覧のクリア
                    ProgressListDataGridView.DataSource = null;

                    // 正常終了の場合はメッセージ
                    Messenger.Info(Resources.KKM00003);

                }

            }

        }
        #endregion

        #region 目標進度チェックリストのチェック
        /// <summary>
        /// 目標進度チェックリストのチェック
        /// </summary>
        /// <returns>登録可否</returns>
        private bool IsEntryMokuhyouCheckList(int checkListNameId)
        {
            //目標進度チェックリストが存在しているかどうか
            var list = this.GetCheckList(checkListNameId);
            if (list == null || list.Any() == false)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;

            }

            return true;

        }
        #endregion

        #region 目標進度リスト右クリックメニュー「項目追加」クリック
        /// <summary>
        /// 目標進度リスト右クリックメニュー「項目追加」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListToolStripItemAdd_Click(object sender, EventArgs e)
        {
            AddItem();

        }

        /// <summary>
        /// 項目追加処理
        /// </summary>
        private void AddItem()
        {
            var id = Convert.ToInt64(this.ProgressListContextMenuStrip.Tag);
            var index = 0;

            var kakuninList = new List<Tuple<long, string, int>>();
            foreach (DataGridViewColumn col in ProgressListDataGridView.Columns)
            {
                //確認結果の列以外なら次へ
                if (col.Name.StartsWith("ITEM_") == false)
                {
                    continue;

                }

                kakuninList.Add(col.Tag as Tuple<long, string, int>);

            }

            //追加するモデル
            var item = new TargetProgressListSearchOutModel
            {
                CHECKLIST_ITEMNAME_ID = this.mokuhyouSindoId--,
                ITEM_FLAG_DISP = 1,
                CONFIRM_SEASON_ID = kakuninList.Select(x => x.Item1).ToArray(),
                CONFIRM_SEASON_NAME = kakuninList.Select(x => x.Item2).ToArray(),
                SEASON_SEQUENCE = kakuninList.Select(x => x.Item3).ToArray(),
                CONFIRM_RESULTS = new string[kakuninList.Count]

            };

            foreach (var mokuhyou in progressList)
            {
                index++;

                //IDが同じかどうか
                if (id == mokuhyou.CHECKLIST_ITEMNAME_ID)
                {
                    //チェックリスト名ID
                    item.CHECKLIST_NAME_ID = mokuhyou.CHECKLIST_NAME_ID;
                    break;

                }

            }

            //追加
            progressList.Insert(index, item);

            //一覧に追加
            index = 0;
            foreach (DataGridViewRow row in ProgressListDataGridView.Rows)
            {
                index++;

                //IDが同じかどうか
                if (id == Convert.ToInt64(row.Cells["CHECKLIST_ITEMNAME_ID"].Value))
                {
                    var list = ProgressListDataGridView.DataSource as BindingList<TargetProgressListSearchOutModel>;
                    list.Insert(index, item);
                    break;
                }

                
            }

        }
        #endregion

        #region 目標進度リスト右クリックメニュー「項目削除(非表示)」クリック
        /// <summary>
        /// 目標進度リスト右クリックメニュー「項目削除(非表示)」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripItemDelete_Click(object sender, EventArgs e)
        {
            this.SetItemDisplay(false);

        }
        #endregion

        #region 目標進度リスト右クリックメニュー「項目再表示」クリック
        /// <summary>
        /// 目標進度リスト右クリックメニュー「項目再表示」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripItemReDisplay_Click(object sender, EventArgs e)
        {
            this.SetItemDisplay(true);

        }
        #endregion

        #region 項目の表示設定
        /// <summary>
        /// 項目の表示設定
        /// </summary>
        /// <param name="isDisplay"></param>
        private void SetItemDisplay(bool isDisplay)
        {
            var idList = new List<long>();
            var currentId = Convert.ToInt64(this.ProgressListContextMenuStrip.Tag);

            //編集モードごとの分岐
            switch (editMode)
            {
                //通常
                //非表示項目表示モード
                case EditModeType.Normal:
                case EditModeType.InvisibleItemDisplay:
                    //選択した行だけが対象
                    idList.Add(currentId);
                    break;

                //複数項目削除モード
                case EditModeType.MultiDelete:
                    //選択した行を象
                    idList.Add(currentId);

                    //選択している全ての行を追加
                    foreach (DataGridViewRow row in ProgressListDataGridView.SelectedRows)
                    {
                        idList.Add(Convert.ToInt64(row.Cells["CHECKLIST_ITEMNAME_ID"].Value));

                    }
                    break;

                default:
                    return;

            }

            foreach (var id in idList)
            {
                //表示設定設定
                var mokuhyou = progressList.First(x => x.CHECKLIST_ITEMNAME_ID == id);
                mokuhyou.ITEM_FLAG_DISP = isDisplay == true ? 1 : 0;

            }

            //一覧再設定
            InvisibleGrid();

            //一覧編集
            gridModifyFlg = true;

        }
        #endregion

        #region 目標進度のグリッドの表示を切替
        /// <summary>
        /// 目標進度のグリッドの表示を切替
        /// </summary>
        private void InvisibleGrid()
        {
            //一覧が無ければ終了
            if (this.progressList == null || progressList.Any() == false)
            {
                ProgressListDataGridView.DataSource = null;
                return;

            }

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            var selectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            var allowDrop = false;
            var readOnly = true;
            var multiSelect = false;

            var list = progressList.Where(x => x.ITEM_FLAG_DISP == 1);

            //編集モードごとの分岐
            switch (editMode)
            {
                //通常
                case EditModeType.Normal:
                    readOnly = false;
                    break;

                //関連課一括入力モード
                case EditModeType.BatchInput:
                    selectionMode = DataGridViewSelectionMode.FullRowSelect;
                    multiSelect = true;
                    break;

                //非表示項目表示モード
                case EditModeType.InvisibleItemDisplay:
                    selectionMode = DataGridViewSelectionMode.FullRowSelect;
                    multiSelect = true;

                    list = progressList;
                    break;

                //複数項目削除モード
                case EditModeType.MultiDelete:
                    selectionMode = DataGridViewSelectionMode.FullRowSelect;
                    multiSelect = true;
                    break;

                //項目移動モード
                case EditModeType.ItemMove:
                    list = progressList;

                    selectionMode = DataGridViewSelectionMode.FullRowSelect;
                    allowDrop = true;
                    break;

            }

            //更新権限が無い場合は読み取り専用
            if (isUpdate == false)
            {
                readOnly = true;
            }

            //元の設定値を取得
            var autoSizeColumnsMode = ProgressListDataGridView.AutoSizeColumnsMode;
            var autoSizeRowsMode = ProgressListDataGridView.AutoSizeRowsMode;
            var columnHeadersHeightSizeMode = ProgressListDataGridView.ColumnHeadersHeightSizeMode;

            //行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない
            ProgressListDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ProgressListDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            ProgressListDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //グリッドビューの設定
            ProgressListDataGridView.SelectionMode = selectionMode;
            ProgressListDataGridView.AllowDrop = allowDrop;
            ProgressListDataGridView.ReadOnly = readOnly;
            ProgressListDataGridView.MultiSelect = multiSelect;

            //編集可能な場合は編集可能な列だけ編集可能に設定
            if (readOnly == false)
            {
                //一旦すべての列を読み取り専用に変更
                foreach (DataGridViewColumn col in ProgressListDataGridView.Columns)
                {
                    col.ReadOnly = true;

                }

                //大項目から達成度は編集可
                ProgressListDataGridView.Columns["LARGE_CLASSIFICATION"].ReadOnly = false;
                ProgressListDataGridView.Columns["MIDDLE_CLASSIFICATION"].ReadOnly = false;
                ProgressListDataGridView.Columns["SMALL_CLASSIFICATION"].ReadOnly = false;
                ProgressListDataGridView.Columns["TARGET_VALUE"].ReadOnly = false;
                ProgressListDataGridView.Columns["ACHIEVED_VALUE"].ReadOnly = false;

            }

            //バインド開始
            this.isBind = true;

            //バインド
            var bindList = new BindingList<TargetProgressListSearchOutModel>(list.ToList());
            ProgressListDataGridView.DataSource = bindList;

            // 確認時期の読込とグリッドへの配置
            for (var i = 0; i < bindList.Count ; i++)
            {
                var bind = bindList[i];

                var confirmResults = bind.CONFIRM_RESULTS;

                var cnt = confirmResults.Length;

                if (cnt > 0)
                {
                    for (var j = 0; j <= seasonCount - 1; j++)
                    {
                        ProgressListDataGridView[string.Format("ITEM_{0:000}", j + 1), i].Value = confirmResults[j];

                    }

                }

            }

            //元の設定値を復元
            ProgressListDataGridView.AutoSizeColumnsMode = autoSizeColumnsMode;
            ProgressListDataGridView.AutoSizeRowsMode = autoSizeRowsMode;
            ProgressListDataGridView.ColumnHeadersHeightSizeMode = columnHeadersHeightSizeMode;

            //一覧は未選択状態
            ProgressListDataGridView.CurrentCell = null;

            //一覧未編集
            gridModifyFlg = false;

            //バインド終了
            this.isBind = false;

        }
        #endregion

        #region 目標進度リストグリッドのタイトルを設定する
        /// <summary>
        /// 目標進度リストグリッドのタイトルを設定する
        /// </summary>
        private void SetTitleProgressListGrid()
        {
            // グリッドのカラムをクリア
            ProgressListDataGridView.Columns.Clear();

            //チェックリスト項目名_ID
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CHECKLIST_ITEMNAME_ID",
                DataPropertyName = "CHECKLIST_ITEMNAME_ID",
                HeaderText = "ID",
                Frozen = true,
                Visible = false
            });

            // 課、性能名　※種別「性能別」選択時には非表示
            if (!BySpecRadioButton.Checked)
            {
                //課
                ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SECTION_CODE",
                    DataPropertyName = "SECTION_CODE",
                    HeaderText = "課",
                    Frozen = true
                });

                //性能名
                ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "SPEC_NAME",
                    DataPropertyName = "SPEC_NAME",
                    HeaderText = "性能名",
                    Frozen = true
                });

            }

            // 大項目
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LARGE_CLASSIFICATION",
                DataPropertyName = "LARGE_CLASSIFICATION",
                HeaderText = "大項目",
                Frozen = true,
                MaxInputLength = 50,
                Tag = "Wide(50);ItemName(大項目)"
            });

            //中項目
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MIDDLE_CLASSIFICATION",
                DataPropertyName = "MIDDLE_CLASSIFICATION",
                HeaderText = "中項目",
                Frozen = true,
                MaxInputLength = 50,
                Tag = "Wide(50);ItemName(中項目)"
            });

            //小項目
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SMALL_CLASSIFICATION",
                DataPropertyName = "SMALL_CLASSIFICATION",
                HeaderText = "小項目",
                Frozen = true,
                MaxInputLength = 50,
                Tag = "Wide(50);ItemName(小項目)"
            });

            //目標値
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TARGET_VALUE",
                DataPropertyName = "TARGET_VALUE",
                HeaderText = "目標値",
                Frozen = true,
                MaxInputLength = 200,
                Tag = "Byte(200);ItemName(目標値)"
            });

            //達成値
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ACHIEVED_VALUE",
                DataPropertyName = "ACHIEVED_VALUE",
                HeaderText = "達成値",
                Frozen = true,
                MaxInputLength = 50,
                Tag = "Wide(50);ItemName(達成値)"
            });

            // チェック項目群　選択したチェックリストによって項目数が変動
            if (progressList != null && progressList.Count > 0)
            {
                seasonCount = 1;

                var mokuhyou = progressList[0];

                for (var i = 0; i < mokuhyou.CONFIRM_SEASON_NAME.Length; i++)
                {
                    ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = string.Format("ITEM_{0:000}", seasonCount++),
                        HeaderText = mokuhyou.CONFIRM_SEASON_NAME[i],
                        Tag = Tuple.Create(mokuhyou.CONFIRM_SEASON_ID[i], mokuhyou.CONFIRM_SEASON_NAME[i], mokuhyou.SEASON_SEQUENCE[i])

                    });

                }

                seasonCount--;
            }

            // 関連課
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RELATIONAL_DIVISION_CODE",
                DataPropertyName = "RELATIONAL_DIVISION_CODE",
                HeaderText = "関連課",
                Width = 200

            });

            // 編集日時
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EDITED_DATE",
                DataPropertyName = "EDITED_DATE",
                HeaderText = "編集日時",
                MinimumWidth = 180,
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = Const.FormatDateTime
                }
            });

            // 編集者
            ProgressListDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EDITOR_NAME",
                DataPropertyName = "EDITOR_NAME",
                HeaderText = "編集者",
                MinimumWidth = 150,
                Width = 150
            });

            //チェックリスト項目名_IDは非表示
            ProgressListDataGridView.Columns["CHECKLIST_ITEMNAME_ID"].Visible = false;

        }
        #endregion

        #region 目標進度リストのセルクリック
        /// <summary>
        /// 目標進度リストのセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;

            }

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var col = this.ProgressListDataGridView.Columns[e.ColumnIndex];

            Action setKakuninKekka = () =>
            {
                //カレントカーソルがノーマル以外で確認結果列ならばクリックしたセルに結果を書き込む
                if (cursolType != CursolType.Normal && col.Name.StartsWith("ITEM_") == true)
                {
                    //更新権限が無ければ終了
                    if (isUpdate == false)
                    {
                        return;

                    }

                    //確認結果列に値を設定
                    this.SetKakuninKekka(e.RowIndex, e.ColumnIndex);

                }

            };

            Action selectRow = () =>
            {
                //列名ごとの分岐
                switch (col.Name)
                {
                    //大項目
                    //中項目
                    case "LARGE_CLASSIFICATION":
                    case "MIDDLE_CLASSIFICATION":
                        var row = this.ProgressListDataGridView.Rows[e.RowIndex];

                        var value = row.Cells[e.ColumnIndex].Value;
                        var name = value == null ? "" : value.ToString();

                        var rowList = new List<DataGridViewRow> { row };

                        //先頭行かどうか
                        if (e.RowIndex != 0)
                        {
                            var index = e.RowIndex - 1;

                            do
                            {
                                var r = this.ProgressListDataGridView.Rows[index--];
                                value = r.Cells[e.ColumnIndex].Value;

                                //値が同じかどうか
                                if (name == (value == null ? "" : value.ToString()))
                                {
                                    rowList.Add(r);

                                }
                                else
                                {
                                    break;

                                }

                            } while (index >= 0);

                        }

                        //最終行かどうか
                        var lastIndex = this.ProgressListDataGridView.RowCount - 1;
                        if (e.RowIndex != lastIndex)
                        {
                            var index = e.RowIndex + 1;

                            do
                            {
                                var r = this.ProgressListDataGridView.Rows[index++];
                                value = r.Cells[e.ColumnIndex].Value;

                                //値が同じかどうか
                                if (name == (value == null ? "" : value.ToString()))
                                {
                                    rowList.Add(r);

                                }
                                else
                                {
                                    break;

                                }

                            } while (index <= lastIndex);

                        }

                        //行選択
                        rowList.ForEach(r => r.Selected = true);
                        break;

                    default:
                        break;

                }
            };

            //編集モードごとの分岐
            switch (editMode)
            {
                //通常
                //非表示項目表示モード
                case EditModeType.Normal:
                case EditModeType.InvisibleItemDisplay:
                    //確認結果設定
                    setKakuninKekka();
                    break;

                //関連課一括入力モード
                case EditModeType.BatchInput:
                    //確認結果設定
                    setKakuninKekka();

                    //行選択
                    selectRow();
                    break;

                //複数項目削除モード
                case EditModeType.MultiDelete:
                    //行選択
                    selectRow();
                    break;

                default:
                    return;

            }


        }

        /// <summary>
        /// 確認結果列に値を設定
        /// </summary>
        /// <param name="rowIndex">行インデックス</param>
        /// <param name="columnIndex">列インデックス</param>
        private void SetKakuninKekka(int rowIndex, int columnIndex)
        {
            var itemIndex = 0;
            if (int.TryParse(ProgressListDataGridView.Columns[columnIndex].Name.Substring(6), out itemIndex))
            {
                //グリッドに反映
                var row = ProgressListDataGridView.Rows[rowIndex];
                row.Cells[columnIndex].Value = CursolTypeMap[cursolType];

                //モデルに反映
                var id = Convert.ToInt64(row.Cells["CHECKLIST_ITEMNAME_ID"].Value);
                var mokuhyou = progressList.First(x => x.CHECKLIST_ITEMNAME_ID == id);
                mokuhyou.CONFIRM_RESULTS[itemIndex - 1] = CursolTypeMap[cursolType];
                mokuhyou.EDITED_DATE = DateTime.Now;
                mokuhyou.EDITOR_NAME = SessionDto.UserName;
                mokuhyou.EDITOR_LOGIN_ID = SessionDto.UserId;

            }

        }

        /// <summary>
        /// 目標進度リストのセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //編集モードごとの分岐
            switch (editMode)
            {
                //関連課一括入力モード
                //複数項目削除モード
                case EditModeType.BatchInput:
                case EditModeType.MultiDelete:
                    //クリック時のイベントを実行
                    this.ProgressListDataGridView_CellClick(sender, e);
                    break;

                default:
                    return;

            }
        }
        #endregion

        #region 目標進度リストのセル内容が変更された
        /// <summary>
        /// 目標進度リストのセル内容が変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行の場合は何もしない
            if (e.RowIndex < 0)
            {
                return;

            }

            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            //編集情報セット
            var row = this.ProgressListDataGridView.Rows[e.RowIndex];
            var id = Convert.ToInt64(row.Cells["CHECKLIST_ITEMNAME_ID"].Value);
            var mokuhyou = progressList.First(x => x.CHECKLIST_ITEMNAME_ID == id);
            mokuhyou.EDITED_DATE = DateTime.Now;
            mokuhyou.EDITOR_NAME = SessionDto.UserName;
            mokuhyou.EDITOR_LOGIN_ID = SessionDto.UserId;

            // グリッド編集フラグを立てる
            gridModifyFlg = true;

        }
        #endregion

        #region 列内の「未」をカウントして返す
        /// <summary>
        /// 列内の「未」をカウントして返す
        /// </summary>
        /// <param name="Cols">グリッドのカラム</param>
        /// <returns>列内の「未」の数</returns>
        private int NotYetCounter(int Cols)
        {
            // 戻り値の初期化
            int NotYetCount = 0;

            // チェック項目の「未」をカウント

            return NotYetCount;
        }

        #endregion

        #region 「自部署」ラジオボタンのチェック状態が変化
        /// <summary>
        /// 「自部署」ラジオボタンのチェック状態が変化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyDivisionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCheckList();
            CheckListToolStripDelete.Visible = FormControlUtil.GetRadioButtonValue(DivisionCategoryPanel) == "1";
        }
        #endregion

        #region 「全部」ラジオボタンのチェック状態が変化
        /// <summary>
        /// 「全部」ラジオボタンのチェック状態が変化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllDivisionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RefreshCheckList();
            
        }
        #endregion

        #region 編集モードが変更された
        /// <summary>
        /// 編集モードが変更された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 保存していないデータが存在する状態でモード切替をしようとした場合にはオペレータに問い合わせ
            if (gridModifyFlg)
            {
                this.ConfirmRegist(Resources.KKM03007);

                //目標進度リストの更新
                var mokuhyou = progressListNameList[CheckListDataGridView.SelectedCells[0].RowIndex];
                this.RefreshProgressList();

            }

            editMode = (EditModeType)EditModeComboBox.SelectedIndex;

            //一覧表示切替
            this.InvisibleGrid();

        }
        #endregion

        #region フォームを閉じようとした時
        /// <summary>
        /// フォームを閉じようとした時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 保存していないデータが存在する状態でフォームを閉じようとした場合にはオペレータに問い合わせ
            if (gridModifyFlg)
            {
                ConfirmRegist(Resources.KKM00006);

            }

        }
        #endregion

        #region 目標進度リスト新規作成処理
        /// <summary>
        /// 目標進度リスト新規作成処理
        /// </summary>
        private void MakeNewProgressList()
        {
            // 種別が部別・課別の場合はエラーメッセージ
            if (FormControlUtil.GetRadioButtonValue(ProcessCategoryPanel) != "1")
            {
                if (Messenger.Confirm(Resources.KKM03008) != DialogResult.Yes)
                {
                    return;
                }

                FormControlUtil.SetRadioButtonValue<string>(ProcessCategoryPanel, "1");

            }

            using (var form = new ProgressListCandidateForm())
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //目標進度チェックリスト再設定
                    this.RefreshCheckList();

                }

            }

        }
        #endregion

        #region 目標進度リストマウスダウン
        /// <summary>
        /// 目標進度リストマウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckListDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //性能別が選択されていなければ終了
            if (this.BySpecRadioButton.Checked == false)
            {
                return;

            }

            //右クリック以外は終了
            if (e.Button != MouseButtons.Right)
            {
                return;

            }

            //管理権限がなければ終了
            if (this.UserAuthority.MANAGEMENT_FLG != '1')
            {
                return;

            }

            //無効な行か列なら終了
            var point = new Point(e.X, e.Y);
            var hitTestInfo = this.CheckListDataGridView.HitTest(point.X, point.Y);
            if (hitTestInfo.Type == DataGridViewHitTestType.None || hitTestInfo.RowIndex < 0 || hitTestInfo.ColumnIndex < 0)
            {
                return;

            }

            //コンテキストメニュー表示
            var row = this.CheckListDataGridView.Rows[hitTestInfo.RowIndex];
            this.CheckListContextMenuStrip.Tag = Convert.ToInt32(row.Cells[this.ChecklistNameIdColumn.Name].Value);
            this.CheckListContextMenuStrip.Show(this.CheckListDataGridView, point);

        }
        #endregion

        #region 目標進度リスト一覧
        /// <summary>
        /// 目標進度リスト一覧マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //無効な行か列なら終了
            var point = new Point(e.X, e.Y);
            var hitTestInfo = this.ProgressListDataGridView.HitTest(point.X, point.Y);
            if (hitTestInfo.Type == DataGridViewHitTestType.None || hitTestInfo.RowIndex < 0 || hitTestInfo.ColumnIndex < 0)
            {
                return;

            }

            var row = this.ProgressListDataGridView.Rows[hitTestInfo.RowIndex];

            //ボタンごとの分岐
            switch (e.Button)
            {
                //左クリック
                case MouseButtons.Left:
                    //編集モードごとの分岐
                    switch (editMode)
                    {
                        //通常
                        //関連課一括入力モード
                        case EditModeType.Normal:
                        case EditModeType.BatchInput:
                            //複数行選択可能かどうか
                            if (this.ProgressListDataGridView.MultiSelect == true)
                            {
                                row.Selected = true;

                            }

                            //関連課の列を選択しているかどうか
                            if (this.ProgressListDataGridView.Columns[hitTestInfo.ColumnIndex].Name == "RELATIONAL_DIVISION_CODE")
                            {
                                this.SetKanrenka(hitTestInfo.RowIndex, hitTestInfo.ColumnIndex);

                            }
                            break;

                        //項目移動モード
                        case EditModeType.ItemMove:
                            //更新権限が無ければ終了
                            if (isUpdate == false)
                            {
                                return;

                            }

                            //ドラッグアンドドロップ開始位置を取得
                            this.dragStartPosition = e.Location;
                            break;

                        default:
                            return;
                    }
                    break;

                //右クリック
                case MouseButtons.Right:
                    //管理権限が無ければ終了
                    if (isManagement == false)
                    {
                        return;

                    }

                    //項目追加
                    this.ProgressListToolStripItemAdd.Enabled = editMode == EditModeType.Normal;

                    //項目削除
                    this.ToolStripItemDelete.Enabled = (editMode == EditModeType.Normal || editMode == EditModeType.MultiDelete);

                    //項目再表示
                    this.ToolStripItemReDisplay.Enabled = editMode == EditModeType.InvisibleItemDisplay;

                    //コンテキストメニュー表示
                    this.ProgressListContextMenuStrip.Tag = row.Cells["CHECKLIST_ITEMNAME_ID"].Value;
                    this.ProgressListContextMenuStrip.Show(this.ProgressListDataGridView, point);
                    break;

                default:
                    return;
            }

        }

        /// <summary>
        /// 関連課列に値を設定
        /// </summary>
        /// <param name="rowIndex">行インデックス</param>
        /// <param name="columnIndex">列インデックス</param>
        private void SetKanrenka(int rowIndex, int columnIndex)
        {
            var row = this.ProgressListDataGridView.Rows[rowIndex];

            //編集モードごとの分岐
            switch (editMode)
            {
                //通常
                case EditModeType.Normal:
                    //対象行を選択状態に設定
                    row.Cells[columnIndex].Selected = true;
                    break;

            }

            var sections = row.Cells[columnIndex].Value == null ? "" : row.Cells[columnIndex].Value.ToString();

            // 関連課フォーム呼出
            using (var frm = new MultiSelectSectionForm { Sections = sections, EntryButtonEnabled = this.UserAuthority.UPDATE_FLG == '1' })
            {
                // 関連課フォーム表示
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var list = new List<DataGridViewRow> { row };

                    //一括編集モードかどうか
                    if (editMode == EditModeType.BatchInput)
                    {
                        //選択している行を追加
                        foreach (DataGridViewRow r in this.ProgressListDataGridView.SelectedRows)
                        {
                            if (list.Contains(r) == false)
                            {
                                list.Add(r);

                            }

                        }

                    }

                    foreach (DataGridViewRow r in list)
                    {
                        // 関連課フォームから値を受け取る
                        var id = Convert.ToInt64(r.Cells["CHECKLIST_ITEMNAME_ID"].Value);
                        var mokuhyou = progressList.First(x => x.CHECKLIST_ITEMNAME_ID == id);
                        mokuhyou.RELATIONAL_DIVISION_CODE = frm.Sections;
                        mokuhyou.EDITED_DATE = DateTime.Now;
                        mokuhyou.EDITOR_NAME = SessionDto.UserName;
                        mokuhyou.EDITOR_LOGIN_ID = SessionDto.UserId;

                    }

                    //一覧編集
                    gridModifyFlg = true;

                }

            }

        }

        /// <summary>
        /// 目標進度リスト一覧マウス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            //項目移動モード以外か左クリック以外かドラッグ開始位置が無ければ終了
            if (editMode != EditModeType.ItemMove || e.Button != MouseButtons.Left || this.dragStartPosition == Point.Empty)
            {
                return;

            }

            var moveRect = new Rectangle(this.dragStartPosition.X - SystemInformation.DragSize.Width / 2, this.dragStartPosition.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width, SystemInformation.DragSize.Height);

            //ドラッグ開始位置を超えているかどうか
            if (moveRect.Contains(e.Location) == false)
            {
                //座標から行を取得
                var hitTestInfo = this.ProgressListDataGridView.HitTest(this.dragStartPosition.X, this.dragStartPosition.Y);

                //ドラッグアンドドロップ開始
                this.ProgressListDataGridView.DoDragDrop(hitTestInfo.RowIndex, DragDropEffects.Move);

                //ドラッグ開始位置を初期化
                this.dragStartPosition = Point.Empty;

            }
        }

        /// <summary>
        /// 目標進度リスト一覧ドラッグ開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_DragOver(object sender, DragEventArgs e)
        {
            //項目移動モード以外は終了
            if (editMode != EditModeType.ItemMove)
            {
                return;

            }

            //無効な行か列かどうか
            var clientPoint = this.ProgressListDataGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = this.ProgressListDataGridView.HitTest(clientPoint.X, clientPoint.Y);
            var flg = hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader || (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0);

            //ドロップ不可かどうか
            e.Effect = flg == true ? DragDropEffects.Move : DragDropEffects.None;

        }

        /// <summary>
        /// 目標進度リスト一覧ドラッグアンドドラップ完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            //項目移動モード以外は終了
            if (editMode != EditModeType.ItemMove)
            {
                return;

            }

            //ドロップ先の行を取得
            var clientPoint = this.ProgressListDataGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = this.ProgressListDataGridView.HitTest(clientPoint.X, clientPoint.Y);
            var destIndex = hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader ? 0 : hitTestInfo.RowIndex;

            //ドロップ元の行を取得
            var sourceIndex = (int)e.Data.GetData(typeof(int));

            //移動元と移動先が同じなら終了
            if (destIndex == sourceIndex)
            {
                return;

            }

            Func<int, TargetProgressListSearchOutModel> getMokuhyou = index =>
            {
                var row = this.ProgressListDataGridView.Rows[index];
                var id = Convert.ToInt64(row.Cells["CHECKLIST_ITEMNAME_ID"].Value);

                return this.progressList.First(x => x.CHECKLIST_ITEMNAME_ID == id);

            };

            var list = new List<TargetProgressListSearchOutModel>();

            for (var i = 0; i < this.ProgressListDataGridView.RowCount; i++)
            {
                //移動元の行なら次へ
                if (sourceIndex == i)
                {
                    continue;

                }

                //移動先の行なら移動元の行を追加
                if (destIndex == i)
                {
                    //目標進度取得
                    var mokuhyou = getMokuhyou(sourceIndex);

                    //行ヘッダーかどうか
                    if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
                    {
                        //先頭に挿入
                        list.Insert(0, mokuhyou);
                        list.Add(getMokuhyou(i));

                    }
                    else
                    {
                        //追加
                        list.Add(getMokuhyou(i));
                        list.Add(mokuhyou);

                    }

                }
                else
                {
                    //追加
                    list.Add(getMokuhyou(i));

                }

            }

            //再バインド
            this.progressList = list;

            //一覧表示切替
            this.InvisibleGrid();

            //一覧編集
            gridModifyFlg = true;

        }
        #endregion
    }
}
