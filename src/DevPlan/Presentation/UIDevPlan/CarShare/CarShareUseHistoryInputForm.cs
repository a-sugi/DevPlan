using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Util;
using System.Text.RegularExpressions;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// 使用履歴簡易入力
    /// </summary>
    public partial class CarShareUseHistoryInputForm : BaseSubForm
    {
        #region メンバ変数
        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();

        /// <summary>作業履歴リスト</summary>
        private List<WorkCarSharingHistoryModel> WorkHistoryList { get; set; } = new List<WorkCarSharingHistoryModel>();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "使用履歴簡易入力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>所在地</summary>
        public string Establish { get; set; }

        //CSV出力項目
        private readonly string[] CsvHeaders =
        {
            "SelectedColumn",
            "MonthlyTextBoxCell",
            "ManageNoColumn",
            "ParkingNoColumn",
            "RegistNoColumn",
            "CarGroupColumn",
            "GeneralCodeColumn",
            "ManageGroupColumn",
            "TodayColumn",
            "TestDetailColumn",
            "OdoColumn",
            "NewestSeqNoColumn",
            "NewestApprovalStatusColumn",
            "NewestDateColumn",
            "NewestTestDetailColumn",
            "NewestOdoColumn",
            "ErrorMessageColumn",
            "ExceptColumn"
        };
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarShareUseHistoryInputForm()
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
        private void CarSharingUseHistoryInputForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // 一覧設定
                this.SetWorkHistoryList();

                AllSelectCheckBox.Checked = true;
            });

            //Append Start 2021/07/12 矢作
            // 情報列
            var info = new string[] { "最新履歴_SEQNO", "最新履歴_承認状況", "最新履歴_日付", "最新履歴_試験内容", "最新履歴_実走行距離" };
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // 反映ボタン
            this.EntryButton.Visible = isUpdate;
            
            // 情報列
            var info = new string[] { "最新履歴_SEQNO", "最新履歴_承認状況", "最新履歴_日付", "最新履歴_試験内容", "最新履歴_実走行距離" };

            // 課題列の色設定
            //foreach (var row in this.ListMultiRow.Rows)
            //{
            //    row.Cells[5].Style.BackColor = Color.Aqua;
            //}
            
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            // テンプレート設定
            this.customTemplate.RowCountLabel = this.RowCountLabel;
            this.customTemplate.MultiRow = this.ListMultiRow;

            //Append Start 2022/07/27 杉浦 ヘッダーサイズ制御追加
            this.customTemplate.ColumnHeaderHeight = 70;
            //Append End 2022/07/27 杉浦 ヘッダーサイズ制御追加

            this.ListMultiRow.Template = this.customTemplate.SetContextMenuTemplate(new CarShareUseHistoryInputMultiRowTemplate());

            // 選択列のフィルターアイテム設定
            var headerCell = this.ListMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell1"] as ColumnHeaderCell;
            headerCell.DropDownContextMenuStrip.Items.RemoveAt(headerCell.DropDownContextMenuStrip.Items.Count - 1);
            headerCell.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // 前月走行距離無視登録列のフィルターアイテム設定
            var headerCell2 = this.ListMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell11"] as ColumnHeaderCell;
            headerCell2.DropDownContextMenuStrip.Items.RemoveAt(headerCell2.DropDownContextMenuStrip.Items.Count - 1);
            headerCell2.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });
        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarSharingUseHistoryInputForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 作業履歴一覧設定
        /// </summary>
        /// <param name="isNewSearch"></param>
        private void SetWorkHistoryList(bool isNewSearch = true, bool isPast = false)
        {
            if (isPast) return;

            // 描画停止
            this.ListMultiRow.SuspendLayout();

            if (isNewSearch)
            {
                this.WorkHistoryList = this.GetCarSharingWorkHistoryList();
            }

            // 複製の作成
            var copyList = new List<WorkCarSharingHistoryModel>(this.WorkHistoryList);

            // 調整
            copyList.RemoveAll(x => x.月例点検省略有無 == (this.MonthlyInputCheckBox.Checked ? "XX" : "不要"));

            // データバインド
            this.customTemplate.SetDataSource(copyList);

            // 試験車が取得できたかどうか
            if (copyList?.Any() == true)
            {
                // リストデータラベルのセット
                this.ListDataLabel.Text = string.Empty;

                // レイアウトの設定
                this.SetLayout();
            }
            else
            {
                // リストデータラベルのセット
                this.ListDataLabel.Text = Resources.KKM00005;
            }

            // 一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;

            // 描画再開
            this.ListMultiRow.ResumeLayout();

            foreach (var row in this.ListMultiRow.Rows)
            {
                if (!row.Visible) continue;

                row.Cells["SelectedColumn"].Value = true;
            }
        }

        /// <summary>
        /// レイアウトの設定
        /// </summary>
        private void SetLayout()
        {
            foreach (var row in this.ListMultiRow.Rows)
            {
                if (row.Index > 0)
                {
                    var border = new Border();

                    if (row.Cells["DataIdColumn"].Value.ToString() != this.ListMultiRow.Rows[row.Index - 1].Cells["DataIdColumn"].Value.ToString())
                    {
                        // 下線設定
                        border.Bottom = new Line(LineStyle.Thin, Color.Gray);
                    }

                    this.ListMultiRow.Rows[row.Index - 1].Border = border;
                }

                // 表示高調整
                //row.Cells["CurrentSituationColumn"].PerformVerticalAutoFit();
            }
        }
        #endregion

        #region 反映ボタンクリック
        /// <summary>
        /// 反映ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //Append Start 2022/03/16 杉浦
                List<WorkCarSharingHistoryModel> entryList = new List<WorkCarSharingHistoryModel>();

                for (int i = 0; i < ListMultiRow.RowCount; i++)
                {
                    if (ListMultiRow.Rows[i].Modified)
                    {
                        WorkCarSharingHistoryModel data = (WorkCarSharingHistoryModel)ListMultiRow.Rows[i].DataBoundItem;

                        //リストに追加
                        entryList.Add(data);
                    }
                    else
                    {
                        WorkCarSharingHistoryModel data = (WorkCarSharingHistoryModel)ListMultiRow.Rows[i].DataBoundItem;
                        if (!string.IsNullOrEmpty(data.ERROR_MESSAGE))
                        {
                            //リストに追加
                            entryList.Add(data);
                        }
                    }
                }
                var reaction = new bool();
                reaction = true;
                if(entryList != null && entryList.Count > 0)
                {
                    reaction = Messenger.Confirm("編集中のデータがあります。編集中のデータが削除されますがよろしいですか？") == DialogResult.Yes;
                }
                if (reaction)
                {
                //Append End 2022/03/16 杉浦
                    // 試験車使用履歴を起動できたかどうか
                    if (this.OpenCarSharingrUseHistory() == false)
                    {
                        Messenger.Warn(Resources.KKM00009);
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// 試験車使用履歴の起動
        /// </summary>
        private bool OpenCarSharingrUseHistory()
        {
            var ret = false;

            // 試験車管理権限の取得
            var authority = this.GetFunctionList(FunctionID.CarShare).FirstOrDefault();

            // 選択行の格納
            foreach (var row in this.ListMultiRow.Rows.Where(x => (x.Cells["SelectedColumn"].FormattedValue.ToString()) == "True" ? true : false))
            {
                var dataID = (int)row.Cells["DataIdColumn"].Value;
                var historyNo = (int)row.Cells["HistoryNoColumn"].Value;

                var carShareHistory = (WorkCarSharingHistoryModel)row.DataBoundItem;
                int i;
                int? 前回走行距離 = null;
                if (int.TryParse(carShareHistory.最新履歴_実走行距離, out i)) 前回走行距離 = i;

                var wHistory = new WorkHistoryModel()
                {
                    ID = carShareHistory.ID,
                    CATEGORY_ID = carShareHistory.CATEGORY_ID,
                    CATEGORY = carShareHistory.CATEGORY,
                    CURRENT_SITUATION = carShareHistory.CURRENT_SITUATION,
                    試験内容 = carShareHistory.試験内容,
                    実走行距離 = carShareHistory.実走行距離,
                    FUTURE_SCHEDULE = carShareHistory.FUTURE_SCHEDULE,
                    INPUT_PERSONEL_ID = carShareHistory.INPUT_PERSONEL_ID,
                    INPUT_NAME = carShareHistory.INPUT_NAME,
                    INPUT_SECTION_CODE = carShareHistory.INPUT_SECTION_CODE,
                    INPUT_DATETIME = carShareHistory.INPUT_DATETIME,
                    OPEN_CLOSE = carShareHistory.OPEN_CLOSE,
                    LISTED_DATE = carShareHistory.LISTED_DATE,
                    SORT_NO = carShareHistory.SORT_NO,
                    IMPORTANT_ITEM = carShareHistory.IMPORTANT_ITEM,
                    MARK = carShareHistory.MARK,
                    INPUT_LOGIN_ID = carShareHistory.INPUT_LOGIN_ID,
                    種別_ID = carShareHistory.種別_ID,
                    種別 = carShareHistory.種別,
                    SCHEDULE_ID = carShareHistory.SCHEDULE_ID,
                    EDIT_FLG = carShareHistory.EDIT_FLG,
                    DELETE_FLG = carShareHistory.DELETE_FLG,
                    データID = carShareHistory.データID,
                    履歴NO = carShareHistory.履歴NO,
                    管理票NO = carShareHistory.管理票NO,
                    CAR_GROUP = carShareHistory.CAR_GROUP,
                    GENERAL_CODE = carShareHistory.GENERAL_CODE,
                    月例点検省略有無 = carShareHistory.月例点検省略有無,
                    最新履歴_SEQNO = carShareHistory.最新履歴_SEQNO,
                    最新履歴_承認状況 = carShareHistory.最新履歴_承認状況,
                    最新履歴_日付 = carShareHistory.最新履歴_日付,
                    最新履歴_試験内容 = carShareHistory.最新履歴_試験内容,
                    最新履歴_実走行距離 = 前回走行距離
                };

                // 試験車使用履歴画面表示
                new FormUtil(new UITestCar.Othe.TestCarHistory.TestCarHistoryForm
                {
                    TestCar = new TestCarCommonModel { データID = dataID, 履歴NO = historyNo },
                    WorkHistory = wHistory,
                    UserAuthority = authority,
                    //Delete Start 2022/03/10 杉浦
                    //Reload = () => FormControlUtil.FormWait(this, () => this.SetWorkHistoryList()),
                    //Delete End 2022/03/10 杉浦
                    Reload = (bool isPast) => FormControlUtil.FormWait(this, () => this.SetWorkHistoryList(true, isPast)),
                    Activate = () => this.Activate()
                })
                .SingleFormShow(this, false);


                
                ret = true;
            }

            return ret;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 作業履歴(試験車)取得
        /// </summary>
        /// <returns></returns>
        private List<WorkCarSharingHistoryModel> GetCarSharingWorkHistoryList()
        {
            string locate = null;
            if (this.Establish == "群馬")
            {
                locate = "1496";
            }
            else
            {
                locate = "1497";
            }

            // 条件設定
            var cond = new CarSharingWorkHistorySearchModel()
            {
                ESTABLISH = locate,
                NECESSARY_INSPECTION_FLAG = !this.MonthlyInputCheckBox.Checked,
            };

            // APIで取得
            var res = HttpUtil.GetResponse<CarSharingWorkHistorySearchModel, WorkCarSharingHistoryModel>(ControllerType.CarSharingWorkHistory, cond);

            // レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                return res.Results.ToList();
            }

            return new List<WorkCarSharingHistoryModel>();
        }
        #endregion

        #region 全選択チェックボックス描画処理
        /// <summary>
        /// 全選択チェックボックス描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - AllSelectCheckBox.Width) / 2, (bmp.Height - AllSelectCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllSelectCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            // チェックボックスの表示を更新する
            if (e.CellIndex == 0 && e.RowIndex == -1)
            {
                AllSelectCheckBox.Checked = !AllSelectCheckBox.Checked;
            }

            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var row = this.ListMultiRow.Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];

            //Update Start 2022/03/02 杉浦
            //if (cell.Name == "SelectedColumn" || (cell.Name != "SelectedColumn" && cell.ReadOnly == true))
            if (cell.Name == "SelectedColumn")
            //Update End 2022/03/02 杉浦
            {
                // チェックボックスの値を更新する
                row.Cells["SelectedColumn"].Value = !(row.Cells["SelectedColumn"].FormattedValue.ToString() == "True" ? true : false);
            }
        }
        #endregion

        #region MultiRowセル結合イベント
        /// <summary>
        /// MultiRowセル結合イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_QueryCellMergeState(object sender, QueryCellMergeStateEventArgs e)
        {
            if (e.ShouldMerge == true)
            {
                var itemIdName = "DataIdColumn";

                if (e.QueryCell.CellName != itemIdName)
                {
                    var newQueryValue = this.ListMultiRow.Rows[e.QueryCell.RowIndex].Cells[itemIdName].Value;
                    var newTargetValue = this.ListMultiRow.Rows[e.TargetCell.RowIndex].Cells[itemIdName].Value;

                    // データIDが同じ場合セル結合させる
                    e.ShouldMerge = newQueryValue?.ToString() == newTargetValue?.ToString();
                }
            }
        }
        #endregion

        #region 全選択チェックボックス本体変更時
        /// <summary>
        /// 全選択チェックボックス本体変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
             {
                 var dataId = 0;

                 // CheckBoxCell 不具合対応
                 this.ListMultiRow.CurrentCell = null;

                 foreach (var row in this.ListMultiRow.Rows)
                 {
                     //Append Start 2020/01/27 杉浦
                     // 非表示行は未処理
                     if (!row.Visible) continue;
                     //Append End 2020/01/27 杉浦

                     //if (dataId != (int)row.Cells["DataIdColumn"].Value)
                     //{
                     //    // 車両の一番上を選択・非選択
                     //    row.Cells["SelectedColumn"].Value = this.AllSelectCheckBox.Checked;
                     //}
                     //else
                     //{
                     //    // 上記以外はすべて非選択
                     //    row.Cells["SelectedColumn"].Value = false;
                     //}
                     row.Cells["SelectedColumn"].Value = this.AllSelectCheckBox.Checked;

                     // データIDの退避
                     dataId = (int)row.Cells["DataIdColumn"].Value;
                 }
             });
        }
        #endregion

        #region 月例点検不要チェックボックスチェンジイベント
        /// <summary>
        /// 月例点検不要チェックボックスチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 一覧設定
            FormControlUtil.FormWait(this, () => this.SetWorkHistoryList(true, false));
        }
        #endregion


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NewestHistoryCheckBox.Checked)
            {
                this.ListMultiRow.Columns[11].Expand();
                this.ListMultiRow.Columns[12].Expand();
                this.ListMultiRow.Columns[13].Expand();
                this.ListMultiRow.Columns[14].Expand();
                this.ListMultiRow.Columns[15].Expand();
            }
            else
            {
                this.ListMultiRow.Columns[11].Collapse();
                this.ListMultiRow.Columns[12].Collapse();
                this.ListMultiRow.Columns[13].Collapse();
                this.ListMultiRow.Columns[14].Collapse();
                this.ListMultiRow.Columns[15].Collapse();
            }
        }

        #region 一括登録ボタン押下時アクション
        private void BulkRegButton_Click(object sender, EventArgs e)
        {
            var list = new List<WorkCarSharingHistoryModel>();
            foreach (var row in this.ListMultiRow.Rows.Where(x => (x.Cells["SelectedColumn"].FormattedValue.ToString()) == "True" ? true : false))
            {
                var item = (WorkCarSharingHistoryModel)row.DataBoundItem;
                if (row.Cells["ExceptColumn"].FormattedValue.ToString() == "True")
                {
                    item.前月走行距離無視登録 = true;
                }
                list.Add(item);
            }


            if (list != null && list.Select(x => x.管理票NO).Distinct().Count() != 0)
            {
                var count = list.Select(x => x.管理票NO).Distinct().Count();
                if (Messenger.Confirm(string.Format("{0}台の車両の使用履歴を一括登録します。\n登録することで、関連部署に一度に複数の承認依頼がされます。\n本当によろしいですか？", count), MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
                {
                    if (CheckBulkReg(list))
                    {

                        //登録後メッセージ
                        Messenger.Info(Resources.KKM00002);

                        this.SetWorkHistoryList();

                    }
                    else
                    {
                        foreach (var row in this.ListMultiRow.Rows)
                        {
                            var item = (WorkCarSharingHistoryModel)row.DataBoundItem;
                            if (!string.IsNullOrEmpty(item.ERROR_MESSAGE))
                            {
                                if (item.ERROR_MESSAGE.Contains("日付"))
                                {
                                    row.Cells[8].Style.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    row.Cells[8].Style.BackColor = Color.Beige;
                                }

                                if (item.ERROR_MESSAGE.Contains("試験内容"))
                                {
                                    row.Cells[9].Style.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    row.Cells[9].Style.BackColor = Color.Beige;
                                }

                                if (item.ERROR_MESSAGE.Contains("実走行距離"))
                                {
                                    row.Cells[10].Style.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    row.Cells[10].Style.BackColor = Color.Beige;
                                }
                            }
                            else
                            {
                                row.Cells[8].Style.BackColor = Color.Beige;
                                row.Cells[9].Style.BackColor = Color.Beige;
                                row.Cells[10].Style.BackColor = Color.Beige;
                            }
                        }
                    }

                }

                //Append Start 2022/03/10 杉浦
                var scroll = this.ListMultiRow.FirstDisplayedLocation;
                var cell = this.ListMultiRow.CurrentCell;
                //Append End 2022/03/10 杉浦

                ////Delete Start 2022/07/27 杉浦 チェックボックス処理削除
                ////Append Start 2022/03/03 杉浦
                //var setList = this.ListMultiRow.DataSource;
                //this.ListMultiRow.DataSource = null;
                //this.ListMultiRow.DataSource = setList;
                //foreach (var row in this.ListMultiRow.Rows)
                //{
                //    if (!row.Visible) continue;

                //    row.Cells["SelectedColumn"].Value = true;
                //}
                ////Append End 2022/03/03 杉浦
                ////Delete Start 2022/07/27 杉浦 チェックボックス処理削除

                //Append Start 2022/03/10 杉浦
                this.ListMultiRow.FirstDisplayedLocation = scroll;
                this.ListMultiRow.CurrentCell = cell;
                //Append End 2022/03/10 杉浦
            }
            else
            {
                Messenger.Warn(Resources.KKM00009);
            }
        }

        public bool CheckBulkReg(List<WorkCarSharingHistoryModel> list)
        {
            var workHistoryList = ChangeCarShareToTestCar(list);
            bool errFlg = false;
            foreach (var item in list)
            {
                var isMonthlyInspectionOmit = item.月例点検省略有無.Equals("不要");
                var msg = string.Empty;
                item.ERROR_MESSAGE = string.Empty;
                //月例点検承認省略フローではなく、処理日の入力がOKかどうか
                if (item.LISTED_DATE != null && item.最新履歴_日付 != null && !isMonthlyInspectionOmit && item.最新履歴_日付.Value.Month != item.LISTED_DATE.Value.AddMonths(-1).Month)
                {
                    msg += Resources.TCM03019 + "\n";
                }
                // 日付
                if (item.LISTED_DATE == null)
                {
                    msg += string.Format(Resources.KKM00001, "日付") + "\n";
                }

                // 試験内容
                if (string.IsNullOrWhiteSpace(item.試験内容) || string.IsNullOrEmpty(item.試験内容))
                {
                    msg += string.Format(Resources.KKM00001, "試験内容") + "\n";
                }

                // 実走行距離
                bool checkInt = true;
                if (string.IsNullOrWhiteSpace(item.実走行距離) || string.IsNullOrEmpty(item.実走行距離))
                {
                    msg += string.Format(Resources.KKM00001, "実走行距離") + "\n";
                }
                else if (!Regex.IsMatch(item.実走行距離, "^[0-9]{1,25}$"))
                {
                    msg += string.Format(Resources.KKM00032, "実走行距離") + "\n";
                }

                //数値に変換できなければ次へ
                var current = 0;
                if (int.TryParse(item.実走行距離, out current) == false)
                {
                    checkInt = false;
                }

                //登録するか問い合わせ
                int i;
                int? 前回走行距離 = null;
                if (int.TryParse(item.最新履歴_実走行距離, out i)) 前回走行距離 = i;
                if (checkInt && (!item.前月走行距離無視登録 && 前回走行距離 != null && current < 前回走行距離))
                {
                    msg += "実走行距離が短いです。" + "\n";
                }

                //承認中のデータがあるかどうか
                if (item.最新履歴_STEPNO > 0)
                {
                    msg += Resources.TCM03021 + "\n";

                }

                item.ERROR_MESSAGE = msg;
                if (!string.IsNullOrEmpty(msg))
                {
                    errFlg = true;
                }
            }

            var section_id = list[0].管理責任課ID;
            var section_group_id = list[0].管理責任部署ID;

            var target = workHistoryList.Select(x => new ApplicationApprovalCarModel
            {
                //表示種別
                TARGET_TYPE = ApplicationApprovalCarTargetType.MonthlyInspection,

                //データID
                データID = x.データID,

                //履歴NO
                履歴NO = x.履歴NO,

                //STEPNO
                STEPNO = x.STEPNO,

                //承認要件コード
                承認要件コード = x.承認要件コード,

                //管理部署承認
                管理部署承認 = x.管理部署承認,

                //承認状況
                承認状況 = x.承認状況,

                //承認者レベル
                承認者レベル = x.承認者レベル,

                //移管先部署_課ID
                移管先部署_SECTION_ID = x.移管先部署_SECTION_ID,

                //移管先部署ID
                移管先部署ID = x.移管先部署ID,

                //課ID
                SECTION_ID = section_id,

                //担当ID
                SECTION_GROUP_ID = section_group_id,

                //駐車場指定
                駐車場指定 = x.駐車場指定,

                //ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //アクセス権限
                ACCESS_LEVEL = x.承認者レベル,

                //編集者
                編集者 = SessionDto.UserId,

                //チェック結果
                CHECK_RESULT = CheckResultType.Ok

            }).ToList();

            //試験車使用履歴権限チェック
            var targetResult = this.IsTestCarUseHistoryAuthority(target);
            if (targetResult.Any(x => x.CHECK_RESULT != CheckResultType.Ok))
            {
                foreach (var data in targetResult.Where(x => x.CHECK_RESULT != CheckResultType.Ok))
                {
                    var itemNo = list.Select((item, index) => new { Index = index, Value = item }).Where(item => item.Value.データID == data.データID && item.Value.履歴NO == data.履歴NO).Select(item => item.Index).ToList();
                    for (int i = 0; i < itemNo.Count; i++)
                    {
                        list[itemNo[i]].ERROR_MESSAGE += Resources.TCM03009;
                        errFlg = true;
                    }
                }
            }

            if (errFlg)
            {
                var errCount = list.Count(x => !string.IsNullOrEmpty(x.ERROR_MESSAGE));
                var regCount = list.Count(x => string.IsNullOrEmpty(x.ERROR_MESSAGE));
                Messenger.Warn(string.Format("エラーがあります。エラーメッセージを確認してください。\n(登録可能：{0}件　登録不可：{1}件)", regCount, errCount));
                return false;
            }

            //試験車使用履歴更新
            if (this.UpdatetTestCarHistory(this.GetEntryTestCarHistory(list)) == false)
            {
                return false;
            }

            //月例点検省略の場合
            var MonthlyInspectionOmitList = ChangeCarShareToTestCar(list.Where(x => x.月例点検省略有無 == "無").ToList());
            // 試験車使用履歴登録
            if (this.InsertTestCarUseHistory(MonthlyInspectionOmitList.Where(x => x.STEPNO == -1).Select(x => new ApplicationApprovalCarModel
            {
                //データID
                データID = x.データID,

                //履歴NO
                履歴NO = x.履歴NO,

                //SEQNO
                SEQNO = x.SEQNO,

                //承認要件コード
                承認要件コード = x.承認要件コード,

                //STEPNO
                STEPNO = 0,

                //承認状況
                承認状況 = "済",

                //承認者レベル
                承認者レベル = null,

                //管理部署承認
                管理部署承認 = null,

                //処理日
                処理日 = x.処理日,

                //管理責任課名
                SECTION_CODE = x.管理責任課名,

                //管理責任部署名
                SECTION_GROUP_CODE = x.管理責任部署名,

                //使用課名
                使用課名 = x.使用課名,

                //使用部署名
                使用部署名 = x.使用部署名,

                //管理所在地
                ESTABLISHMENT = x.ESTABLISHMENT,

                //試験内容
                試験内容 = x.試験内容,

                //工事区分NO
                工事区分NO = x.工事区分NO,

                //実走行距離
                実走行距離 = x.実走行距離,

                //編集者
                編集者 = x.編集者,

                //移管先部署ID
                移管先部署ID = null,

                //駐車場番号
                駐車場番号 = null,

                //チェック結果
                CHECK_RESULT = CheckResultType.Ok,

                //登録種別
                ADD_TYPE = AddType.History

            }).ToList()) == false)
            {
                return false;
            }

            return true;
        }

        #region 変換用
        private List<TestCarUseHistoryModel> ChangeCarShareToTestCar(List<WorkCarSharingHistoryModel> list)
        {
            var returnList = new List<TestCarUseHistoryModel>();
            foreach (var item in list)
            {
                //Append Start 2023/04/24 杉浦 使用履歴簡易入力時に承認状況の表記がおかしい
                var est = Establish == "群馬" ? "g" : "t";
                //Append End 2023/04/24 杉浦 使用履歴簡易入力時に承認状況の表記がおかしい

                returnList.Add(new TestCarUseHistoryModel()
                {
                    //データID
                    データID = item.データID.Value,

                    //履歴NO
                    履歴NO = item.履歴NO.Value,

                    //SEQNO
                    SEQNO = 0,

                    //承認要件コード
                    承認要件コード = "C",

                    //STEPNO
                    STEPNO = -1,

                    //承認状況
                    承認状況 = "入力待ち",

                    //承認者レベル
                    承認者レベル = "2",

                    //管理部署承認
                    管理部署承認 = "0",

                    //処理日
                    処理日 = item.LISTED_DATE,

                    //管理責任課名
                    管理責任課名 = item.管理責任課,

                    //管理責任部署名
                    管理責任部署名 = item.管理責任部署,

                    //管理責任部署
                    管理責任部署 = string.Format("{0} {1}", item.管理責任課, item.管理責任部署),

                    //使用課名
                    使用課名 = item.管理責任課,

                    //使用部署名
                    使用部署名 = item.管理責任部署,

                    //管理所在地
                    //Update Start 2023/04/24 杉浦 使用履歴簡易入力時に承認状況の表記がおかしい
                    //ESTABLISHMENT = Establish,
                    ESTABLISHMENT = est,
                    //Update End 2023/04/24 杉浦 使用履歴簡易入力時に承認状況の表記がおかしい

                    //使用部署
                    使用部署 = string.Format("{0} {1}", item.管理責任課, item.管理責任部署),

                    //工事区分NO
                    工事区分NO = item.工事区分NO,

                    //編集者
                    編集者 = SessionDto.UserId,

                    //ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    //試験内容
                    試験内容 = item.試験内容,

                    //実走行距離
                    実走行距離 = item.実走行距離,
                });
            }
            return returnList;
        }
        #endregion

        /// <summary>
        /// 試験車使用履歴権限チェック
        /// </summary>
        /// <param name="list">試験車使用履歴</param>
        /// <returns></returns>
        private List<ApplicationApprovalCarModel> IsTestCarUseHistoryAuthority(List<ApplicationApprovalCarModel> list)
        {
            //APIで取得
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistoryAuthorityCheck, list);

            //レスポンスが取得できたかどうか
            var results = new List<ApplicationApprovalCarModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                results.AddRange(res.Results);

            }

            return results;

        }

        /// <summary>
        /// 登録試験車使用履歴取得
        /// </summary>
        /// <returns></returns>
        private TestCarHistoryModel GetEntryTestCarHistory(List<WorkCarSharingHistoryModel> historyList)
        {
            var history = new TestCarHistoryModel();
            var workHistoryList = ChangeCarShareToTestCar(historyList);

            //試験車使用履歴があるかどうか
            if (workHistoryList.Count > 0)
            {
                for (int i = 0; i < workHistoryList.Count; i++)
                {
                    // 月例点検（新規追加）は除外
                    if (historyList[i].月例点検省略有無 == "無")
                    {
                        workHistoryList.RemoveAt(i);
                    }
                }

                //試験車使用履歴
                history.TestCarUseHistoryList = workHistoryList;
            }

            return history;

        }

        /// <summary>
        /// 試験車使用履歴更新
        /// </summary>
        /// <param name="history">試験車使用履歴</param>
        /// <returns></returns>
        private bool UpdatetTestCarHistory(TestCarHistoryModel history)
        {
            var list = new[] { history };

            var res = HttpUtil.PutResponse(ControllerType.TestCarHistory, list);

            return res != null && res.Status == Const.StatusSuccess;

        }

        /// <summary>
        /// 試験車使用履歴登録
        /// </summary>
        /// <param name="useHistory">試験車使用履歴</param>
        /// <returns></returns>
        private bool InsertTestCarUseHistory(List<ApplicationApprovalCarModel> useHistory)
        {
            var res = HttpUtil.PostResponse(ControllerType.TestCarUseHistory, useHistory);

            return res != null && res.Status == Const.StatusSuccess;

        }

        #endregion

        #region Excel出力
        private void PrintButton_Click(object sender, EventArgs e)
        {

            List<Row> csvList = new List<Row>();

            foreach (var row in this.ListMultiRow.Rows)
            {
                csvList.Add(row);
            }

            if (csvList.Count == 0)
            {
                Messenger.Warn(Resources.TCM03008);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "Excel ブック (*.xlsx)|*.xlsx;", FileName = string.Format("{0}_{1:yyyyMMddHHmmss}", this.FormTitle, DateTime.Now) })
            {
                //保存先が選択されたかどうか
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var map = new Dictionary<int, string>();

                    foreach (var header in CsvHeaders)
                    {
                        var i = 0;

                        foreach (var col in this.ListMultiRow.Columns)
                        {
                            //対象の列なら追加
                            if (col.Name == header)
                            {
                                map[i] = header;
                                break;
                            }

                            i++;
                        }
                    }

                    //CSV出力用カラムのカラム名から"CSV"を削除
                    string[] outHeaders = (string[])CsvHeaders.Clone();

                    // Excel出力
                    using (var xls = new XlsUtil())
                    {
                        var sheetName = "Sheet1";

                        var headers = outHeaders.Select((x, i) => new { Key = string.Format("{0}1", xls.GetColumnAddress(i)), Value = x }).ToArray();

                        var lastColumn = xls.GetColumnAddress(headers.Count() - 1);

                        var headerRange = string.Format("A1:{0}1", lastColumn);

                        //ヘッダーを書き込み
                        var headerMap = headers.ToDictionary(x => x.Key, x => x.Value == "SelectedColumn" ? " " :
                                                                         x.Value == "MonthlyTextBoxCell" ? "月例点検" :
                                                                         x.Value == "ManageNoColumn" ? "管理票NO" :
                                                                         x.Value == "ParkingNoColumn" ? "駐車場番号" :
                                                                         x.Value == "RegistNoColumn" ? "登録ナンバー" :
                                                                         x.Value == "CarGroupColumn" ? "車系" :
                                                                         x.Value == "GeneralCodeColumn" ? "開発符号" :
                                                                         x.Value == "ManageGroupColumn" ? "管理責任部署" :
                                                                         x.Value == "TodayColumn" ? "日付" :
                                                                         x.Value == "TestDetailColumn" ? "試験内容" :
                                                                         x.Value == "OdoColumn" ? "実走行距離" :
                                                                         x.Value == "NewestSeqNoColumn" ? "最新使用履歴_SEQNO" :
                                                                         x.Value == "NewestApprovalStatusColumn" ? "最新使用履歴_承認状況" :
                                                                         x.Value == "NewestDateColumn" ? "最新使用履歴_日付" :
                                                                         x.Value == "NewestTestDetailColumn" ? "最新使用履歴_試験内容" :
                                                                         x.Value == "NewestOdoColumn" ? "最新使用履歴_実走行距離" :
                                                                         x.Value == "ErrorMessageColumn" ? "エラーメッセージ" : 
                                                                         x.Value == "ExceptColumn" ? "前月走行距離無視登録" : x.Value);
                        xls.WriteSheet(sheetName, headerMap);

                        //罫線の設定
                        xls.SetBorder(sheetName, headerRange);
                        xls.CopyRow(sheetName, headerRange, string.Format("A2:{0}{1}", lastColumn, csvList.Count() + 1));

                        //シートに書き込み
                        xls.WriteSheet(sheetName, csvList,
                            (row =>
                            {
                                var list = new List<string>();

                                foreach (var kv in map)
                                {
                                    var index = kv.Key;
                                    var header = kv.Value;
                                    var cell = row.Cells[index];
                                    var value = cell.Value;

                                    var date = new DateTime();
                                    if (value != null && DateTime.TryParse(value.ToString(), out date))
                                    {
                                        value = date.ToString("yyyy/MM/dd");
                                    }
                                    if(header == "SelectedColumn" || header == "ExceptColumn")
                                    {
                                        value = value.ToString() == "false" || value.ToString() == "False" ? "無" : "有";
                                    }

                                    list.Add(value == null ? "" : value.ToString());
                                }

                                return list.Select((x, i) => new { Key = xls.GetColumnAddress(i), Value = x }).ToDictionary(x => x.Key, x => x.Value);
                            }), 1);

                        //ヘッダーを中寄
                        xls.SetAlignmentCenter(sheetName, string.Format("A1:{0}1", lastColumn));

                        //列幅の自動調整
                        xls.AutoSizeColumn(sheetName, string.Format("A:{0}", lastColumn));

                        // フッターの設定
                        xls.SetFooter(sheetName, string.Format("社外転用禁止\n{0} {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), SessionDto.SectionCode, SessionDto.UserName));

                        //ブックの保存
                        xls.Save(sfd.FileName);
                    }
                }
            }
        }
        #endregion

        private void CarShareUseHistoryInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<WorkCarSharingHistoryModel> entryList = new List<WorkCarSharingHistoryModel>();
            
            for (int i = 0; i < ListMultiRow.RowCount; i++)
            {
                if (ListMultiRow.Rows[i].Modified)
                {
                    WorkCarSharingHistoryModel data = (WorkCarSharingHistoryModel)ListMultiRow.Rows[i].DataBoundItem;
                    
                    //リストに追加
                    entryList.Add(data);
                }
                //Append Start 2022/03/15 杉浦 チェック追加
                else
                {
                    WorkCarSharingHistoryModel data = (WorkCarSharingHistoryModel)ListMultiRow.Rows[i].DataBoundItem;
                    if (!string.IsNullOrEmpty(data.ERROR_MESSAGE))
                    {
                        //リストに追加
                        entryList.Add(data);
                    }
                }
                //Append End 2022/03/15 杉浦 チェック追加
            }
            if (0 < entryList.Count)
            {
                //変更があれば確認ダイアログ表示
                //Update Start 2022/03/10 杉浦
                //if (Messenger.Confirm("編集中のデータがあります。画面を離れますか？") == DialogResult.No)
                if (Messenger.Confirm("編集中のデータがあります。編集中のデータが削除されますがよろしいですか？") == DialogResult.No)
                //Update Start 2022/03/10 杉浦
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
