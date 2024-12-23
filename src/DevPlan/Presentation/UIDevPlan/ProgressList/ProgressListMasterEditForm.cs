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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UIDevPlan.ProgressList
{
    /// <summary>
    /// 目標進度リストマスタ編集
    /// </summary>
    public partial class ProgressListMasterEditForm : BaseSubForm
    {
        #region メンバ変数
        private long masterID = -1;

        private Point dragStartPosition = Point.Empty;

        private const int PasteColumunCount = 4;

        private const int NoneColumn = 5;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "目標進度リストマスタ編集"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>目標進度リストマスタ</summary>
        private List<TargetProgressListMasterModel> MokuhyouSindoListMasterList { get; set; }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgressListMasterEditForm()
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
        private void ProgressListMasterEditForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            try
            {
                //バインド中フラグON
                this.IsBind = true;

                //目標進度リストマスタ初期化
                this.MokuhyouSindoListMasterList = new List<TargetProgressListMasterModel>();

                //性能名プルダウン
                var cond = new PerformanceNameInModel
                {
                    // 課ID
                    SECTION_ID = SessionDto.SectionID

                };
                FormControlUtil.SetComboBoxItem(this.PerformanceNameComboBox, HttpUtil.GetResponse<PerformanceNameInModel, PerformanceNameOutModel>(ControllerType.PerformanceName, cond)?.Results, false);

                //目標進度リスト一覧の設定
                this.ProgressListMasterGridView.AutoGenerateColumns = false;

                //大項目
                this.LargeColumn.Tag = "Wide(50);ItemName(大項目)";

                //中項目
                this.MiddleColumn.Tag = "Wide(50);ItemName(中項目)";

                //小項目
                this.SmallColumn.Tag = "Wide(50);ItemName(小項目)";

                //目標値
                this.GoalColumn.Tag = "Byte(200);ItemName(目標値)";

                //性能名があるかどうか
                if (this.PerformanceNameComboBox.Items.Count > 0)
                {
                    //先頭を選択
                    this.PerformanceNameComboBox.SelectedIndex = 0;

                    //目標進度リストマスタの設定
                    this.SetMokuhyouSindoListMasterBySeinou();

                }
                else
                {
                    //行追加ボタン
                    this.RowAddButton.Visible = false;

                    //行削除ボタン
                    this.RowDeleteButton.Visible = false;

                    //登録ボタン
                    this.EntryButton.Visible = false;

                }

            }
            finally
            {
                //バインド中フラグOFF
                this.IsBind = false;

            }

        }
        #endregion

        #region 性能名リストボックス
        /// <summary>
        /// リストボックス変更
        /// </summary>
        private void PerformanceNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;

            }

            FormControlUtil.FormWait(this, () =>
            {
                //作業履歴の編集は終了
                this.ProgressListMasterGridView.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.IsEditMokuhyouSindoListMasterEntry() == true)
                {
                    //目標進度リストマスタの設定
                    this.SetMokuhyouSindoListMasterBySeinou();

                }

            });

        }
        #endregion

        #region 編集モード
        /// <summary>
        /// 編集モード
        /// </summary>
        private void EditModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //未選択の場合は終了
            var radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                return;

            }

            //編集モード切替
            var flg = this.EditModeNoramlRadioButton.Checked;
            this.ProgressListMasterGridView.SelectionMode = flg == true ? DataGridViewSelectionMode.RowHeaderSelect : DataGridViewSelectionMode.FullRowSelect;
            this.ProgressListMasterGridView.AllowDrop = !flg;
            this.ProgressListMasterGridView.ReadOnly = !flg;

        }
        #endregion

        #region 行追加ボタン
        /// <summary>
        /// 行追加ボタンクリック
        /// </summary>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            var master = new TargetProgressListMasterModel
            {
                //ID
                ID = this.masterID--,

                //性能名_ID
                性能名_ID = Convert.ToInt32(this.PerformanceNameComboBox.SelectedValue),

                //編集フラグ
                EDIT_FLG = true

            };

            var index = this.ProgressListMasterGridView.RowCount;

            //一覧を選択しているかどうか
            if (this.ProgressListMasterGridView.CurrentCell == null)
            {
                //最後尾に追加
                this.MokuhyouSindoListMasterList.Add(master);

            }
            else
            {
                //選択行の次の行に追加
                index = this.ProgressListMasterGridView.CurrentCell.RowIndex + 1;
                this.MokuhyouSindoListMasterList.Insert(index, master);

            }

            //再バインド
            this.SetMokuhyouSindoListMaster();

            //追加行を選択状態に設定
            var row = this.ProgressListMasterGridView.Rows[index];
            this.ProgressListMasterGridView.CurrentCell = row.Cells[0];
            row.Selected = true;

            //追加行までスクロール
            this.ProgressListMasterGridView.FirstDisplayedScrollingRowIndex = index;

        }
        #endregion

        #region 行削除ボタン
        /// <summary>
        /// 行削除ボタンクリック
        /// </summary>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            //一覧を選択しているかどうか
            if (this.ProgressListMasterGridView.CurrentCell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;
            }

            var columnIndex = this.ProgressListMasterGridView.CurrentCell.ColumnIndex;
            var rowIndex = this.ProgressListMasterGridView.CurrentCell.RowIndex;
            var row = this.ProgressListMasterGridView.Rows[rowIndex];
            var id = Convert.ToInt64(row.Cells[this.IdColumn.Name].Value);
            var master = this.MokuhyouSindoListMasterList.First(x => x.ID == id);

            //削除フラグと編集フラグを有効化
            master.EDIT_FLG = true;
            master.DELETE_FLG = true;

            //再バインド
            this.SetMokuhyouSindoListMaster();

            //行があればスクロール
            if (this.ProgressListMasterGridView.RowCount > 0)
            {
                //最終行の削除の場合は調整
                if (this.ProgressListMasterGridView.RowCount <= rowIndex)
                {
                    rowIndex--;

                }

                //行を選択状態に設定
                row = this.ProgressListMasterGridView.Rows[rowIndex];
                this.ProgressListMasterGridView.CurrentCell = row.Cells[columnIndex];

                //行までスクロール
                this.ProgressListMasterGridView.FirstDisplayedScrollingRowIndex = rowIndex;

            }

            //削除完了メッセージ
            Messenger.Info(Resources.KKM00003);

        }
        #endregion

        #region 目標進度リストマスタ一覧
        /// <summary>
        /// 目標進度リストマスタ一覧セル描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var columnIndex = e.ColumnIndex;
            var rowIndex = e.RowIndex;

            //セルの下側を線なしに設定
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            //1行目か1列目か編集日時以降の列の場合は上線を設定
            if (rowIndex < 1 || columnIndex < 1 || columnIndex >= NoneColumn)
            {
                e.AdvancedBorderStyle.Top = this.ProgressListMasterGridView.AdvancedCellBorderStyle.Top;
                return;

            }

            var cellValue = this.ProgressListMasterGridView[columnIndex, rowIndex].Value;
            var prevCellValue = this.ProgressListMasterGridView[columnIndex, rowIndex - 1].Value;

            var value = cellValue == null ? "" : cellValue.ToString();
            var prevValue = prevCellValue == null ? "" : prevCellValue.ToString();

            var defaultCellStyle = this.ProgressListMasterGridView.DefaultCellStyle;

            //文言が上の行と同じかどうか
            var flg = (value == prevValue);
            if (flg == true)
            {
                //上線と文字の色を設定
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                e.CellStyle.ForeColor = e.CellStyle.BackColor == Const.ErrorBackColor ? defaultCellStyle.ForeColor : defaultCellStyle.BackColor;

            }
            else
            {
                //上線と文字の色を設定
                e.AdvancedBorderStyle.Top = this.ProgressListMasterGridView.AdvancedCellBorderStyle.Top;
                e.CellStyle.ForeColor = defaultCellStyle.ForeColor;

            }

        }

        /// <summary>
        /// 目標進度リストマスタ一覧マウス押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //表示順変更を未選択なら終了
            if (this.EditModeSortNumRadioButton.Checked == false)
            {
                return;

            }

            //無効な行か列なら終了
            var hitTestInfo = this.ProgressListMasterGridView.HitTest(e.X, e.Y);
            if (hitTestInfo.RowIndex < 0 || hitTestInfo.ColumnIndex < 0)
            {
                return;

            }

            //ドラッグアンドドロップ開始位置を取得
            this.dragStartPosition = e.Location;

        }

        /// <summary>
        /// 目標進度リストマスタ一覧マウス移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_MouseMove(object sender, MouseEventArgs e)
        {
            //表示順変更を未選択なら終了
            if (this.EditModeSortNumRadioButton.Checked == false)
            {
                return;

            }

            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            //ドラッグ開始位置が無ければ終了
            if (this.dragStartPosition == Point.Empty)
            {
                return;

            }

            var moveRect = new Rectangle(this.dragStartPosition.X - SystemInformation.DragSize.Width / 2, this.dragStartPosition.Y - SystemInformation.DragSize.Height / 2,
                    SystemInformation.DragSize.Width, SystemInformation.DragSize.Height);

            //ドラッグ開始位置を超えているかどうか
            if (moveRect.Contains(e.Location) == false)
            {
                //座標から行を取得
                var hitTestInfo = this.ProgressListMasterGridView.HitTest(this.dragStartPosition.X, this.dragStartPosition.Y);

                //ドラッグアンドドロップ開始
                this.ProgressListMasterGridView.DoDragDrop(hitTestInfo.RowIndex, DragDropEffects.Move);

                //ドラッグ開始位置を初期化
                this.dragStartPosition = Point.Empty;

            }

        }

        /// <summary>
        /// 目標進度リストマスタ一覧ドラッグ開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_DragOver(object sender, DragEventArgs e)
        {
            //表示順変更を未選択なら終了
            if (this.EditModeSortNumRadioButton.Checked == false)
            {
                return;

            }

            //無効な行か列かどうか
            var clientPoint = this.ProgressListMasterGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = this.ProgressListMasterGridView.HitTest(clientPoint.X, clientPoint.Y);
            var flg = hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader || (hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0);

            //ドロップ不可かどうか
            e.Effect = flg == true ? DragDropEffects.Move : DragDropEffects.None;

        }

        /// <summary>
        /// 目標進度リストマスタ一覧ドラッグアンドドラップ完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_DragDrop(object sender, DragEventArgs e)
        {
            //表示順変更を未選択なら終了
            if (this.EditModeSortNumRadioButton.Checked == false)
            {
                return;

            }

            Func<int, TargetProgressListMasterModel> getMaster = index =>
            {
                var row = this.ProgressListMasterGridView.Rows[index];
                var id = Convert.ToInt64(row.Cells[this.IdColumn.Name].Value);

                var master = this.MokuhyouSindoListMasterList.First(x => x.ID == id);

                //編集フラグ
                master.EDIT_FLG = true;

                return master;

            };

            //ドロップ先の行を取得
            var clientPoint = this.ProgressListMasterGridView.PointToClient(new Point(e.X, e.Y));
            var hitTestInfo = this.ProgressListMasterGridView.HitTest(clientPoint.X, clientPoint.Y);
            var destIndex = hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader ? 0 : hitTestInfo.RowIndex;

            //ドロップ元の行を取得
            var sourceIndex = (int)e.Data.GetData(typeof(int));

            //移動元と移動先が同じなら終了
            if (destIndex == sourceIndex)
            {
                return;

            }

            //削除対象を取得
            var list = this.MokuhyouSindoListMasterList.Where(x => x.ID > 0 && x.DELETE_FLG == true).ToList();

            for (var i = 0; i < this.ProgressListMasterGridView.RowCount; i++)
            {
                //移動元の行なら次へ
                if (sourceIndex == i)
                {
                    continue;

                }

                //移動先の行なら移動元の行を追加
                if (destIndex == i)
                {
                    var master = getMaster(sourceIndex);

                    //行ヘッダーかどうか
                    if (hitTestInfo.Type == DataGridViewHitTestType.ColumnHeader)
                    {
                        //先頭に挿入
                        list.Insert(0, master);
                        list.Add(getMaster(i));

                    }
                    else
                    {
                        //追加
                        list.Add(getMaster(i));
                        list.Add(master);

                    }

                }
                else
                {
                    //追加
                    list.Add(getMaster(i));

                }

            }

            //目標進度リストマスタ再設定
            this.MokuhyouSindoListMasterList = list;

            //目標進度リストマスタの設定
            this.SetMokuhyouSindoListMaster();

        }

        /// <summary>
        /// 目標進度リストマスタ一覧のキーダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //性能名が無ければ終了
            if (this.PerformanceNameComboBox.Items.Count == 0)
            {
                return;

            }

            //貼付以外は終了
            var isPaste = (e.Control == true && e.KeyCode == Keys.V) || (e.Shift == true && e.KeyCode == Keys.Insert);
            if (isPaste == false)
            {
                return;

            }

            //テキストがなければ終了
            var data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.Text) == false)
            {
                return;

            }

            var value = data.GetData(DataFormats.Text);
            var txt = value == null ? "" : value.ToString().TrimEnd().Replace(Const.CrLf, Const.Lf).Replace(Const.Cr, Const.Lf);

            var seinouID = Convert.ToInt32(this.PerformanceNameComboBox.SelectedValue);

            var list = new List<TargetProgressListMasterModel>();

            //行ごとに分割
            foreach (var row in txt.Split(new[] { Const.Lf }, StringSplitOptions.None))
            {
                var cells = row.Split(new[] { Const.Tab }, StringSplitOptions.None).ToList();

                //目標値までなければ空文字で追加
                while (cells.Count < PasteColumunCount)
                {
                    cells.Add("");

                }

                //追加
                list.Add(new TargetProgressListMasterModel
                {
                    //ID
                    ID = this.masterID--,

                    //大項目
                    大項目 = cells[0],

                    //中項目
                    中項目 = cells[1],

                    //小項目
                    小項目 = cells[2],

                    //目標値
                    目標値 = cells[3],

                    //性能名_ID
                    性能名_ID = seinouID,

                    //編集フラグ
                    EDIT_FLG = true

                });

            }

            var index = 0;

            //選択行があるかどうか
            if (this.ProgressListMasterGridView.CurrentCell != null)
            {
                index = this.ProgressListMasterGridView.CurrentCell.RowIndex + 1;

            }

            //選択行の次の行に追加
            this.MokuhyouSindoListMasterList.InsertRange(index, list);

            //再バインド
            this.SetMokuhyouSindoListMaster();

        }

        /// <summary>
        /// 目標進度リストマスタ一覧の項目の値変更後
        /// </summary>
        private void ProgressListMasterGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行の場合は何もしない
            if (e.RowIndex < 0)
            {
                return;

            }

            var row = this.ProgressListMasterGridView.Rows[e.RowIndex];

            var id = Convert.ToInt64(row.Cells[this.IdColumn.Name].Value);

            var master = this.MokuhyouSindoListMasterList.First(x => x.ID == id);

            //編集フラグを有効化
            master.EDIT_FLG = true;

        }
        #endregion

        #region 登録ボタン
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //目標進度リストマスタが登録できたかどうか
                if (this.EntryMokuhyouSindoListMaster() == true)
                {
                    //目標進度リストマスタの設定
                    this.SetMokuhyouSindoListMasterBySeinou();

                }
               
            });

        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListMasterEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //作業履歴の編集は終了
                this.ProgressListMasterGridView.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.IsEditMokuhyouSindoListMasterEntry() == false)
                {
                    //登録に失敗した場合は閉じさせない
                    e.Cancel = true;

                }

            });

        }
        #endregion

        #region 目標進度リストマスタの設定
        /// <summary>
        /// 目標進度リストマスタの設定(性能)
        /// </summary>
        private void SetMokuhyouSindoListMasterBySeinou()
        {
            var id = Convert.ToInt32(this.PerformanceNameComboBox.SelectedValue);

            //目標進度リストマスタ取得
            this.MokuhyouSindoListMasterList = this.GetMokuhyouSindoListMasterList(id);

            //目標進度リストマスタの設定
            this.SetMokuhyouSindoListMaster();

            //作業履歴が取得できたかどうか
            this.ListDataLabel.Text = this.MokuhyouSindoListMasterList.Any() == true ? "" : Resources.KKM00005;

        }

        /// <summary>
        /// 目標進度リストマスタの設定
        /// </summary>
        private void SetMokuhyouSindoListMaster()
        {
            //バインドデータを初期化
            this.ProgressListMasterGridView.DataSource = null;

            //削除フラグが無効なデータに並び順を設定
            var target = this.MokuhyouSindoListMasterList.Where(x => x.DELETE_FLG == false).Select((x, i) =>
            {
                //並び順
                x.NO = i + 1;

                return x;

            }).ToArray();

            //削除フラグが無効のデータがあればバインド
            if (target.Any() == true)
            {
                this.ProgressListMasterGridView.DataSource = target;

            }

            //一覧を未選択状態に設定
            this.ProgressListMasterGridView.CurrentCell = null;

        }
        #endregion

        #region 変更目標進度リストマスタ登録可否
        /// <summary>
        /// 変更目標進度リストマスタ登録可否
        /// </summary>
        /// <returns></returns>
        private bool IsEditMokuhyouSindoListMasterEntry()
        {
            var isEdit = this.MokuhyouSindoListMasterList.Any(x => x.EDIT_FLG == true);

            //画面を変更していないか登録するかどうか
            if (isEdit == false || (isEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;

            }

            //目標進度リストマスタ登録
            return this.EntryMokuhyouSindoListMaster();

        }
        #endregion

        #region 目標進度リストマスタの登録
        /// <summary>
        /// 目標進度リストマスタの登録
        /// </summary>
        private bool EntryMokuhyouSindoListMaster()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            //登録対象があるかどうか
            var list = this.GetEntryMokuhyouSindoListMaster();
            if (list == null || list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00022);
                return false;

            }

            //目標進度リストマスター登録
            var res = HttpUtil.PutResponse(ControllerType.TargetProgressListMaster, list);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;

            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            return true;

        }

        /// <summary>
        /// 登録する目標進度リストマスタを取得
        /// </summary>
        private IEnumerable<TargetProgressListMasterModel> GetEntryMokuhyouSindoListMaster()
        {
            var list = this.MokuhyouSindoListMasterList.Where(x => x.ID > 0 && x.DELETE_FLG == true).ToList();

            var now = DateTime.Now;

            var sortNo = 0;

            Func<string, string> lineEmpty = s => string.IsNullOrWhiteSpace(s) == true ? "" : s.Replace(Const.Cr, "").Replace(Const.Lf, "");

            foreach (DataGridViewRow row in this.ProgressListMasterGridView.Rows)
            {
                var id = Convert.ToInt64(row.Cells[this.IdColumn.Name].Value);
                var master = this.MokuhyouSindoListMasterList.First(x => x.ID == id);
                var isEdit = master.EDIT_FLG;

                //変更している行かどうか
                if (isEdit == true)
                {
                    //入力日時
                    master.編集日時 = now;

                    //編集者_ID
                    master.編集者_ID = SessionDto.UserId;

                    //編集者_NAME
                    master.編集者_NAME = SessionDto.UserName;

                }

                //登録済の目標進度リストマスタかどうか
                if (master.ID < 0)
                {
                    //ID
                    master.ID = 0;

                }

                //大項目
                master.大項目 = lineEmpty(master.大項目);

                //中項目
                master.中項目 = lineEmpty(master.中項目);

                //小項目
                master.小項目 = lineEmpty(master.小項目);

                //目標値
                master.目標値 = lineEmpty(master.目標値);

                //ソート順再設定
                master.SORT_NO = ++sortNo;

                list.Add(master);

            }

            return list;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// 目標進度リストマスタリストの取得
        /// </summary>
        /// <param name="id">性能ID</param>
        /// <returns></returns>
        private List<TargetProgressListMasterModel> GetMokuhyouSindoListMasterList(int id)
        {
            var cond = new TargetProgressListMasterSearchModel
            {
                //性能名_ID
                性能名_ID = id

            };

            //APIで取得
            var res = HttpUtil.GetResponse<TargetProgressListMasterSearchModel, TargetProgressListMasterModel>(ControllerType.TargetProgressListMaster, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TargetProgressListMasterModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list;

        }
        #endregion

    }
}
