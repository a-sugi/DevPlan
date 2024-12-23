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
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Attributes;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.OuterCar
{
    /// <summary>
    /// 作業履歴(外製車)
    /// </summary>
    public partial class OuterCarHistoryForm : BaseSubDialogForm
    {
        #region メンバ変数
        private decimal workHistoryID = -1;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "作業履歴(外製車)"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return false; } }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>お気に入りバインド中可否</summary>
        private bool IsFavoriteBind { get; set; }

        /// <summary>お気に入りID</summary>
        public long? FavoriteID { get; set; }

        /// <summary>スケジュール項目</summary>
        public OuterCarScheduleItemGetOutModel ScheduleItem { get; set; }

        /// <summary>作業履歴</summary>
        public List<WorkHistoryModel> WorkHistoryList { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>種別(先頭空)</summary>
        private List<ClassSearchOutModel> ClassList { get; set; } = new List<ClassSearchOutModel>() { new ClassSearchOutModel() };

        /// <summary>親画面リロードデリゲート</summary>
        public System.Action Reload { get; set; } = () => { };

        /// <summary>車両名変更機能利用可否</summary>
        public bool IsItemNameEdit { get; internal set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OuterCarHistoryForm()
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
        private void OuterCarHistoryForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isOuterCar = base.GetFunction(FunctionID.OuterCar).MANAGEMENT_FLG == '1';

            //作業履歴初期化
            this.WorkHistoryList = new List<WorkHistoryModel>();

            //お気に入り
            this.SetFavoriteComboBox();

            //ステータス
            this.StatusPanel.Enabled = isManagement;

            //車両名の編集ボタン
            this.ScheduleItemEditButton.Visible = isOuterCar;
            if (IsItemNameEdit == false) { this.ScheduleItemEditButton.Visible = IsItemNameEdit; }

            //行追加ボタン
            this.RowAddButton.Visible = isUpdate;

            //行削除ボタン
            this.RowDeleteButton.Visible = isUpdate;

            //文字色変更ボタン
            this.ColorChangeButton.Visible = (isUpdate || isManagement);

            //作業履歴一覧の設定
            this.HistoryMultiRow.ReadOnly = !isUpdate;
            
            //登録ボタン
            this.EntryButton.Visible = isUpdate;

            // MultiRowテンプレート作成
            var dataList = new Dictionary<string, CustomMultiRowCellStyle>();

            foreach (var col in typeof(WorkHistoryModel).GetProperties())
            {
                var style = new CustomMultiRowCellStyle((CellSettingAttribute)Attribute.GetCustomAttribute(col, typeof(CellSettingAttribute)), col.Name);

                style.Width = 130;
                style.Type = MultiRowCellType.TEXT;

                dataList.Add(col.Name, style);
            }

            // 種別リストの追加
            ClassList.AddRange(HttpUtil.GetResponse<ClassSearchOutModel>(ControllerType.Class)?.Results.ToList());

            dataList["種別_ID"].Type = MultiRowCellType.COMBOBOX;
            dataList["種別_ID"].ReadOnly = false;
            dataList["種別_ID"].ValueMember = "ID";
            dataList["種別_ID"].DisplayMember = "種別";
            dataList["種別_ID"].ComboBoxDataSource = ClassList;

            dataList["LISTED_DATE"].Type = MultiRowCellType.DATETIME;

            dataList["INPUT_DATETIME"].Type = MultiRowCellType.DATETIME_LONG;
            dataList["INPUT_DATETIME"].CustomFormat = "yyyy/MM/dd HH:mm:ss";

            dataList["CURRENT_SITUATION"].Width = 200;

            this.HistoryMultiRow.Template = new CustomTemplate(dataList, true, this.HistoryMultiRow);
            this.HistoryMultiRow.Tag = "Required;ItemName(進捗状況)";

            // Enterキー
            this.HistoryMultiRow.ShortcutKeyManager.Unregister(Keys.Enter);
            this.HistoryMultiRow.ShortcutKeyManager.Register(SelectionActions.MoveDown, Keys.Enter);

            //お気に入りIDがあるかどうか
            if (this.FavoriteID != null)
            {
                this.FavoriteComboBox.SelectedValue = this.FavoriteID;

            }
            else
            {
                //スケジュール項目と作業履歴の設定
                this.SetItemHistoryByScheduleID(this.ScheduleItem.CATEGORY_ID);

            }
            // GPSデータがある場合はMapボタンを表示する
            var xeyeList = this.GetXeyeUrl();
            if (xeyeList == null || xeyeList.Count() == 0) this.GpsMapButton.Enabled = false;
            else this.GpsMapButton.Enabled = true;

        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterCarHistoryForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.HistoryMultiRow.CurrentCell = null;

        }
        #endregion

        #region お気に入り選択
        /// <summary>
        /// お気に入り選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //お気に入りバインド中か先頭を選択なら終了
            if (this.IsFavoriteBind == true || this.FavoriteComboBox.SelectedIndex == 0)
            {
                return;

            }

            //変更した履歴の登録に成功したかどうか
            if (this.IsEditHistoryEntry() == false)
            {
                return;

            }

            var item = this.FavoriteComboBox.SelectedItem as FavoriteSearchOutModel;

            //スケジュール項目と作業履歴を設定
            this.HistoryMultiRow.FirstDisplayedLocation = Point.Empty;
            this.SetItemHistoryByFavoriteID(item.ID);

        }
        #endregion

        #region お気に入り編集ボタンクリック
        /// <summary>
        /// お気に入り編集ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEditButton_Click(object sender, EventArgs e)
        {
            var favorite = new FavoriteSearchInModel
            {
                // データ区分
                CLASS_DATA = Const.FavoriteOuterCarWorkHistory

            };

            // お気に入り検索
            using (var form = new FavoriteListForm(favorite))
            {
                //お気に入りデータがない場合はメッセージ表示後終了
                if (form.GetGridDataList().Count == 0)
                {
                    Messenger.Info(Resources.KKM00036);

                }
                else
                {
                    //OKの場合はお気に入り再設定
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        //お気に入りコンボボックス再設定
                        this.SetFavoriteComboBox();

                    }

                }

            }
        }
        #endregion

        #region ステータス変更
        /// <summary>
        /// ステータス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //選択されている場合は背景色を設定
            var radio = sender as RadioButton;
            if (radio.Checked == true)
            {
                this.StatusPanel.BackColor
                    = this.OpenRadioButton.BackColor
                    = this.CloseRadioButton.BackColor
                    = this.OpenRadioButton.Checked == true ? Color.Red : Color.Blue;
            }

        }
        #endregion

        #region 車両名の編集ボタンクリック
        /// <summary>
        /// 車両名の編集ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemEditButton_Click(object sender, EventArgs e)
        {
            var item = new ScheduleItemModel<OuterCarScheduleItemGetOutModel>()
            {
                // ID
                ID = ScheduleItem.CATEGORY_ID,

                // スケジュール編集区分
                ScheduleItemEdit = ScheduleItemEditType.Update,

                // スケジュール項目
                ScheduleItem = ScheduleItem
            };

            using (var form = new ScheduleItemDetailForm<OuterCarScheduleItemGetOutModel, OuterCarScheduleGetOutModel>()
            {
                FormSubTitle = "外製車",
                FunctionId = FunctionID.OuterCar,
                Item = item,
                IsRestriction = true
            })
            {
                // 項目編集
                if (form.ShowDialog().Equals(DialogResult.OK))
                {
                    //スケジュール項目と作業履歴の設定
                    this.SetItemHistoryByScheduleID(item.ID);

                    // 親画面リロード
                    this.Reload();
                }
            }
        }
        #endregion

        #region 行追加ボタンクリック
        /// <summary>
        /// 行追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            var workHistory = new WorkHistoryModel
            {
                //ID
                ID = this.workHistoryID--,

                //カテゴリーID
                CATEGORY_ID = this.ScheduleItem.CATEGORY_ID,

                //日付
                LISTED_DATE = DateTime.Today,

                //並び順
                SORT_NO = int.MinValue,

                //編集フラグ
                EDIT_FLG = true

            };

            //先頭に行追加して再バインド
            this.WorkHistoryList.Insert(0, workHistory);
            this.SetWorkHistory();

            //先頭行を選択状態に設定
            this.HistoryMultiRow.Rows[0].Selected = true;

            //先頭行を選択状態にするため、スクロールも頭へ移動
            this.HistoryMultiRow.FirstDisplayedLocation = new Point(0, 0);
        }
        #endregion

        #region 行削除ボタンクリック
        /// <summary>
        /// 行削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            //一覧を選択しているかどうか
            if (this.HistoryMultiRow.CurrentCell == null ||
                (this.HistoryMultiRow.CurrentCell is FilteringTextBoxCell))
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            var row = this.HistoryMultiRow.Rows[this.HistoryMultiRow.CurrentCell.RowIndex];

            var id = ((WorkHistoryModel)row.DataBoundItem).ID;

            var workHistory = this.WorkHistoryList.First(x => x.ID == id);

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 編集許可チェック（他部署）
            if (!string.IsNullOrWhiteSpace(workHistory.INPUT_SECTION_CODE) &&
                workHistory.INPUT_SECTION_CODE != SessionDto.SectionCode)
            {
                // 管理権限なし
                if (!isManagement)
                {
                    // 入力エラー文言表示
                    Messenger.Warn(string.Format(Resources.KKM03036, "SJSB"));

                    return;
                }
            }

            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;

            }

            //削除フラグと編集フラグを有効化
            workHistory.EDIT_FLG = true;
            workHistory.DELETE_FLG = true;

            //再バインド
            this.SetWorkHistory();

            //削除完了メッセージ
            Messenger.Info(Resources.KKM00003);

        }
        #endregion

        #region 作業履歴のイベント
        /// <summary>
        /// セルバリデーションバリデーション
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // バインド中は終了
            if (IsBind)
            {
                return;
            }

            var grid = ((GcMultiRow)sender);

            var name = e.CellName;

            if (name != "CURRENT_SITUATION" && 
                name != "INPUT_NAME" && 
                name != "INPUT_SECTION_CODE" && 
                name != "INPUT_DATETIME" && 
                name != "LISTED_DATE" && 
                name != "種別_ID")
            {
                return;
            }

            var row = grid.Rows[e.RowIndex];
            var cell = row.Cells[name];

            var id = ((WorkHistoryModel)row.DataBoundItem).ID;

            var workHistory = this.WorkHistoryList.First(x => x.ID == id);
            var val = workHistory.GetType().GetProperty(name).GetValue(workHistory);

            if (val == e.FormattedValue) return;

            if (cell is TextBoxCell)
            {
                if ((string)val == (string)e.FormattedValue) return;
            }
            else if (cell is DateTimePickerCell)
            {
                if (((DateTime)val).ToString("yyyy/MM/dd") == ((DateTime)e.FormattedValue).ToString("yyyy/MM/dd")) return;
            }
            else if (cell is ComboBoxCell)
            {
                if ((int?)val == ClassList.FirstOrDefault(x => x.種別 == (string)e.FormattedValue).ID) return;
            }
            else
            {
                return;
            }

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 編集許可チェック（他部署）
            if (!string.IsNullOrWhiteSpace(workHistory.INPUT_SECTION_CODE) && 
                workHistory.INPUT_SECTION_CODE != SessionDto.SectionCode)
            {
                // 管理権限なし
                if (!isManagement)
                {
                    // 入力エラー文言表示
                    Messenger.Warn(string.Format(Resources.KKM03036, "SJSB"));

                    // キャンセル
                    e.Cancel = true;

                    // 値を元に戻す
                    grid.CancelEdit();

                    return;
                }
            }

            // 編集フラグON
            workHistory.EDIT_FLG = true;
        }

        /// <summary>
        /// 作業履歴の値エラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryMultiRow_DataError(object sender, GrapeCity.Win.MultiRow.DataErrorEventArgs e)
        {
            // 種別または日付の列かどうか
            if (e.CellName == "種別_ID" || e.CellName == "LISTED_DATE")
            {
                // 未処理
            }
            else
            {
                // 例外はスロー
                throw e.Exception;
            }
        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //作業履歴が登録できたかどうか
                if (this.EntryWorkHistory() == true)
                {
                    //スケジュール項目と作業履歴の設定
                    this.SetItemHistoryByScheduleID(this.ScheduleItem.CATEGORY_ID);

                }

            });

        }
        #endregion

        #region お気に入り登録ボタンクリック
        /// <summary>
        /// お気に入り登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteEntryButton_Click(object sender, EventArgs e)
        {
            //お気に入りが上限件数まで登録済かどうか
            if (this.GetFavoriteList().Count() >= Const.FavoriteEntryMax)
            {
                Messenger.Warn(Resources.KKM00016);
                return;

            }

            var favorite = new HistoryFavoriteItemModel
            {
                //履歴区分
                HISTORY_CODE = Const.HistoryOuterCar,

                //履歴ID
                HISTORY_ID = this.ScheduleItem.CATEGORY_ID,

                //車系
                CAR_GROUP = this.ScheduleItem.GENERAL_CODE,

                //開発符号
                GENERAL_CODE = this.ScheduleItem.GENERAL_CODE

            };

            using (var form = new FavoriteEntryForm(favorite))
            {
                //OKの場合はお気に入り再設定
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //お気に入りコンボボックス再設定
                    this.SetFavoriteComboBox();

                }

            }

        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OuterCarHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //作業履歴の編集は終了
                this.HistoryMultiRow.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.IsEditHistoryEntry() == false)
                {
                    //登録に失敗した場合は閉じさせない
                    e.Cancel = true;

                }

            });

        }
        #endregion

        #region お気に入りのコンボボックス設定
        /// <summary>
        /// お気に入りのコンボボックス設定
        /// </summary>
        private void SetFavoriteComboBox()
        {
            //お気に入りバインド中ON
            this.IsFavoriteBind = true;

            //お気に入り
            FormControlUtil.SetComboBoxItem(this.FavoriteComboBox, this.GetFavoriteList());

            //お気に入りバインド中OFF
            this.IsFavoriteBind = false;

        }
        #endregion

        #region スケジュール項目と作業履歴の設定
        /// <summary>
        /// スケジュール項目と作業履歴の設定(お気に入りID)
        /// </summary>
        /// <param name="id">ID</param>
        private void SetItemHistoryByFavoriteID(long id)
        {
            //お気に入りが取得できたかどうか
            var favorite = this.GetFavorite(id);
            if (favorite == null)
            {
                return;

            }

            //スケジュール項目と作業履歴の設定
            this.SetItemHistoryByScheduleID(favorite.HISTORY_ID, true);

        }

        /// <summary>
        /// スケジュール項目と作業履歴の設定(スケジュールID)
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isFavorite">お気に入り利用フラグ</param>
        private void SetItemHistoryByScheduleID(long id, bool isFavorite = false)
        {
            //スケジュール項目取得
            var scheduleItem = this.GetScheduleItem(id);

            //スケジュール項目が取得できたかどうか
            if (scheduleItem == null)
            {
                if (isFavorite) Messenger.Warn(Resources.KKM00040);
                return;

            }

            //スケジュール項目設定
            this.ScheduleItem = scheduleItem;

            //SJSBの予約許可が必要
            this.SjsbReservationLabel.Visible = this.ScheduleItem.FLAG_要予約許可 == 1;

            //車両名
            this.ItemNameLabel.Text = this.ScheduleItem.CATEGORY;

            //最終予約可能日
            this.LastReservationLabel.Text = DateTimeUtil.ConvertDateString(this.ScheduleItem.最終予約可能日);

            //ステータス
            var flg = this.ScheduleItem.CLOSED_DATE == null;
            this.OpenRadioButton.Checked = flg;
            this.CloseRadioButton.Checked = !flg;

            //作業履歴
            this.WorkHistoryList = this.GetWorkHistoryList();
            this.SetWorkHistory();
        }

        /// <summary>
        /// 作業履歴設定
        /// </summary>
        private void SetWorkHistory()
        {
            // バインド中ON
            this.IsBind = true;

            try
            {
                var scroll = this.HistoryMultiRow.FirstDisplayedLocation;


                // フィルタクリア
                this.HistoryMultiRow.ClearAllFilters();

                //バインドデータを初期化
                this.HistoryMultiRow.DataSource = null;

                //削除フラグが無効のデータがあればバインド
                var target = this.WorkHistoryList.Where(x => x.DELETE_FLG == false).ToArray();
                if (target.Any() == true)
                {
                    this.HistoryMultiRow.DataSource = target;

                    for (int i = 0; i < this.HistoryMultiRow.RowCount; i++)
                    {
                        HistoryMultiRow.Rows[i][0].PerformVerticalAutoFit();

                        var data = (WorkHistoryModel)HistoryMultiRow.Rows[i].DataBoundItem;

                        if (data.IMPORTANT_ITEM == 1)
                        {
                            foreach (var cell in this.HistoryMultiRow.Rows[i].Cells)
                                cell.Style.ForeColor = cell.Style.ForeColor = Color.Red;
                        }
                    }

                    this.HistoryMultiRow.FirstDisplayedLocation = scroll;
                }
            }
            finally
            {
                this.IsBind = false;
            }
        }
        #endregion

        #region 変更作業履歴登録可否
        /// <summary>
        /// 変更作業履歴登録可否
        /// </summary>
        /// <returns></returns>
        private bool IsEditHistoryEntry()
        {
            //履歴を編集しているかどうか
            var isEdit = this.WorkHistoryList.Any(x => x.EDIT_FLG == true);
            if (isEdit == false && this.ScheduleItem != null)
            {
                isEdit = (this.ScheduleItem.CLOSED_DATE == null) != this.OpenRadioButton.Checked;

            }

            //画面を変更していないか登録するかどうか
            if (isEdit == false || (isEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;

            }

            //作業履歴登録
            return this.EntryWorkHistory();

        }
        #endregion

        #region データの取得
        /// <summary>
        /// お気に入りの取得
        /// </summary>
        private List<FavoriteSearchOutModel> GetFavoriteList()
        {
            //パラメータ設定
            var cond = new FavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // データ区分
                CLASS_DATA = Const.FavoriteOuterCarWorkHistory

            };

            //APIで取得
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<FavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// お気に入りの取得
        /// </summary>
        /// <param name="id">ID</param>
        private HistoryFavoriteSearchOutModel GetFavorite(long id)
        {
            return this.GetFavoriteList(new HistoryFavoriteSearchInModel { ID = id }).FirstOrDefault();

        }

        /// <summary>
        /// お気に入りの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<HistoryFavoriteSearchOutModel> GetFavoriteList(HistoryFavoriteSearchInModel cond)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<HistoryFavoriteSearchInModel, HistoryFavoriteSearchOutModel>(ControllerType.HistoryFavorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<HistoryFavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// スケジュール項目取得
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private OuterCarScheduleItemGetOutModel GetScheduleItem(long id)
        {
            //パラメータ設定
            var cond = new OuterCarScheduleItemGetInModel { ID = id };

            //APIで取得
            var res = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, cond);

            //レスポンスが取得できたかどうか
            var list = new List<OuterCarScheduleItemGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            //返却
            return list.FirstOrDefault();

        }
        
        /// <summary>
        /// 作業履歴の取得
        /// </summary>
        /// <returns></returns>
        private List<WorkHistoryModel> GetWorkHistoryList()
        {
            //パラメータ設定
            var cond = new WorkHistorySearchModel
            {
                //カテゴリーID
                CATEGORY_ID = this.ScheduleItem.CATEGORY_ID

            };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkHistorySearchModel, WorkHistoryModel>(ControllerType.WorkHistory, cond);

            //レスポンスが取得できたかどうか
            var list = new List<WorkHistoryModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// XeyeのUrlを取得する
        /// </summary>
        /// <returns></returns>
        private List<ScheduleToXeyeOutModel> GetXeyeUrl()
        {
            //管理票Noを取得する
            ScheduleToXeyeSearchModel searchModel = new ScheduleToXeyeSearchModel();
            searchModel.物品名2 = this.ScheduleItem.管理票NO;

            // XeyeのIDを取得する
            var res = HttpUtil.GetResponse<ScheduleToXeyeSearchModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, searchModel);
            var xeyeId = new List<ScheduleToXeyeOutModel>();
            if (res.Results.Count() != 0) xeyeId.AddRange(res.Results);

            return xeyeId;
        }
        #endregion

        #region 作業履歴の登録
        /// <summary>
        /// 作業履歴の登録
        /// </summary>
        /// <returns></returns>
        private bool EntryWorkHistory()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            //登録対象があるかどうか
            var list = this.GetEntryWorkHistory();
            if (list != null && list.Any() == true)
            {
                //作業履歴登録
                var res = HttpUtil.PutResponse(ControllerType.OuterCarWorkHistory, list);

                //レスポンスが取得できたかどうか
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    return false;

                }

            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            return true;

        }

        /// <summary>
        /// 登録する作業履歴を取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<WorkHistoryModel> GetEntryWorkHistory()
        {
            var list = this.WorkHistoryList.Where(x => x.ID > 0 && x.DELETE_FLG == true).ToList();

            var now = DateTime.Now;

            foreach (var row in this.HistoryMultiRow.Rows)
            {
                var id = ((WorkHistoryModel)row.DataBoundItem).ID;

                var workHistory = this.WorkHistoryList.First(x => x.ID == id);

                var isEdit = workHistory.EDIT_FLG;

                //変更している行かどうか
                if (isEdit == true)
                {
                    //入力者パーソナルID
                    workHistory.INPUT_PERSONEL_ID = SessionDto.UserId;

                    //入力者名
                    workHistory.INPUT_NAME = SessionDto.UserName;

                    //入力者課コード
                    workHistory.INPUT_SECTION_CODE = SessionDto.SectionCode;

                    //入力日時
                    workHistory.INPUT_DATETIME = now;

                    //赤黒の文字色
                    workHistory.IMPORTANT_ITEM = (row.Cells.Any(x => x.Style.ForeColor == Color.Red)) ? 1 : (int?)null;
                }

                list.Add(workHistory);

            }

            //登録対象が無い場合はダミーデータを設定
            if (list.Any() == false)
            {
                list.Add(new WorkHistoryModel
                {
                    //カテゴリーID
                    CATEGORY_ID = this.ScheduleItem.CATEGORY_ID,

                    //入力者パーソナルID
                    INPUT_PERSONEL_ID = SessionDto.UserId,

                    //削除フラグ
                    DELETE_FLG = true

                });

            }

            //ステータス
            var status = FormControlUtil.GetRadioButtonValue(this.StatusPanel);
            list.ForEach(x => x.OPEN_CLOSE = status);

            var sortNo = 1;

            //ソート順再設定
            foreach (var id in list.Where(x => x.DELETE_FLG == false).OrderByDescending(x => x.LISTED_DATE).ThenBy(x => x.SORT_NO).ThenBy(x => x.ID).Select(x => x.ID))
            {
                var workHistory = list.First(x => x.ID == id);

                //登録済の作業履歴かどうか
                if (workHistory.ID < 0)
                {
                    //ID
                    workHistory.ID = 0;

                }

                //並び順
                workHistory.SORT_NO = sortNo++;

            }

            return list;

        }
        #endregion

        #region グリッド文字色変更ボタンクリック
        /// <summary>
        /// グリッド文字色変更ボタンクリックイベント。
        /// </summary>
        /// <remarks>
        /// 現在選択されているレコードの文字色を変更し、編集フラグをTrueにします。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorChangeButton_Click(object sender, EventArgs e)
        {
            if (this.HistoryMultiRow.CurrentCell == null ||
                (this.HistoryMultiRow.CurrentCell is FilteringTextBoxCell))
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            var selectRow = this.HistoryMultiRow.Rows[this.HistoryMultiRow.CurrentCell.RowIndex];

            var id = ((WorkHistoryModel)selectRow.DataBoundItem).ID;

            var workHistory = this.WorkHistoryList.First(x => x.ID == id);

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 編集許可チェック（他部署）
            if (!string.IsNullOrWhiteSpace(workHistory.INPUT_SECTION_CODE) &&
                workHistory.INPUT_SECTION_CODE != SessionDto.SectionCode)
            {
                // 管理権限なし
                if (!isManagement)
                {
                    // 入力エラー文言表示
                    Messenger.Warn(string.Format(Resources.KKM03036, "SJSB"));

                    return;
                }
            }
            
            Color setColor = Color.Black;
            foreach (var cell in selectRow.Cells)
            {
                cell.Style.ForeColor = cell.Style.ForeColor != Color.Red ? Color.Red : Color.Black;
                setColor = cell.Style.ForeColor;
            }

            var model = this.WorkHistoryList.First(x => x.ID == ((WorkHistoryModel)selectRow.DataBoundItem).ID);
            model.EDIT_FLG = true;
            model.IMPORTANT_ITEM = (setColor == Color.Red) ? 1 : (int?)null;
        }
        #endregion

        private void GpsMapButton_Click(object sender, EventArgs e)
        {
            var xeyeList = this.GetXeyeUrl();
            // Xeyeページに接続するフォームを起動
            var frm = new WebBrowserForm(xeyeList[0].備考);
            frm.Show();
        }
    }
}
