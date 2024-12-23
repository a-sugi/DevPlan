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

namespace DevPlan.Presentation.UIDevPlan.WorkProgress
{
    /// <summary>
    /// 進捗履歴
    /// </summary>
    public partial class WorkProgressHistroryForm : BaseSubForm
    {
        #region メンバ変数
        private decimal workHistoryID = -1;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "進捗履歴"; } }

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

        /// <summary>お気に入りバインド中可否</summary>
        private bool IsFavoriteBind { get; set; }

        /// <summary>お気に入りID</summary>
        public long? FavoriteID { get; set; }

        /// <summary>月次計画利用フラグ</summary>
        public bool IsMonthlyPlan { get; set; } = false;

        /// <summary>スケジュール項目</summary>
        public WorkProgressItemModel ScheduleItem { get; set; }

        /// <summary>作業履歴</summary>
        public List<WorkHistoryModel> WorkHistoryList { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkProgressHistroryForm()
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
        private void WorkProgressHistoryForm_Load(object sender, EventArgs e)
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
            var isMonthly = this.IsMonthlyPlan;

            //作業履歴初期化
            this.WorkHistoryList = new List<WorkHistoryModel>();

            //お気に入り
            this.SetFavoriteComboBox();

            //ステータス
            this.StatusPanel.Enabled = isUpdate;

            //行追加ボタン
            this.RowAddButton.Visible = isUpdate;

            //行削除ボタン
            this.RowDeleteButton.Visible = isUpdate;

            //作業履歴一覧の設定
            this.HistoryDataGridView.AutoGenerateColumns = false;
            this.HistoryDataGridView.ReadOnly = !isUpdate;

            //日付
            this.ListedDateColumn.Tag = "Required;ItemName(日付)";

            //現状
            this.CurrentSituationColumn.Tag = "Byte(2000);ItemName(現状)";

            //今後の予定
            this.FutureScheduleColumn.Tag = "Byte(2000);ItemName(今後の予定)";

            //登録ボタン
            this.EntryButton.Visible = isUpdate;

            // 月次計画利用
            if (isMonthly)
            {
                // 読取専用
                this.HistoryDataGridView.ReadOnly = isMonthly;

                // 非活性
                this.FavoriteComboBox.Enabled = 
                this.FavoriteEditButton.Enabled = 
                this.StatusPanel.Enabled = 
                this.RowAddButton.Enabled = 
                this.RowDeleteButton.Enabled = 
                this.EntryButton.Enabled = 
                this.FavoriteEntryButton.Enabled = !isMonthly;
            }

            //お気に入りIDがあるかどうか
            if (this.FavoriteID != null)
            {
                this.FavoriteComboBox.SelectedValue = this.FavoriteID;

            }
            else
            {
                //スケジュール項目と作業履歴の設定
                this.SetItemHistoryByScheduleID(this.ScheduleItem.ID);

            }

        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkProgressHistoryForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.HistoryDataGridView.CurrentCell = null;

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

            //利用許可があるかどうか
            var item = this.FavoriteComboBox.SelectedItem as FavoriteSearchOutModel;
            if (item.PERMIT_FLG != 1)
            {
                Messenger.Warn(Resources.KKM00020);
                return;

            }

            //スケジュール項目と作業履歴を設定
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
                CLASS_DATA = Const.FavoriteWorkProgressHistory

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
                this.StatusPanel.BackColor = this.OpenRadioButton.Checked == true ? Color.Red : Color.Blue;

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
                CATEGORY_ID = this.ScheduleItem.ID,

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
            this.HistoryDataGridView.Rows[0].Selected = true;

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
            if (this.HistoryDataGridView.CurrentCell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;

            }

            var row = this.HistoryDataGridView.Rows[this.HistoryDataGridView.CurrentCell.RowIndex];

            var id = Convert.ToDecimal(row.Cells[this.IdColumn.Name].Value);

            var workHistory = this.WorkHistoryList.First(x => x.ID == id);

            //削除フラグと編集フラグを有効化
            workHistory.EDIT_FLG = true;
            workHistory.DELETE_FLG = true;

            //再バインド
            this.SetWorkHistory();

            //削除完了メッセージ
            Messenger.Info(Resources.KKM00003);

        }
        #endregion

        #region 作業履歴の項目の値変更後
        /// <summary>
        /// 作業履歴の項目の値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行の場合は何もしない
            if (e.RowIndex < 0)
            {
                return;

            }

            var row = this.HistoryDataGridView.Rows[e.RowIndex];

            var id = Convert.ToDecimal(row.Cells[this.IdColumn.Name].Value);

            var workHistory = this.WorkHistoryList.First(x => x.ID == id);

            //編集フラグを有効化
            workHistory.EDIT_FLG = true;

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
                    this.SetItemHistoryByScheduleID(this.ScheduleItem.ID);

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
                HISTORY_CODE = Const.HistoryWorkProgress,

                //履歴ID
                HISTORY_ID = this.ScheduleItem.ID,

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
        private void WorkProgressHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //作業履歴の編集は終了
                this.HistoryDataGridView.EndEdit();

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
        /// <param name="favorite">お気に入りID</param>
        private void SetItemHistoryByScheduleID(long id, bool favorite = false)
        {
            //スケジュール項目取得
            this.ScheduleItem = this.GetScheduleItem(id);

            //スケジュール項目が取得できたかどうか
            if (this.ScheduleItem == null)
            {
                if (favorite == true)
                {
                    Messenger.Warn(Resources.KKM00040);
                }
                return;

            }

            //項目名
            this.ItemNameLabel.Text = this.ScheduleItem.CATEGORY;

            //ステータス
            var flg = this.ScheduleItem.CLOSED_DATE == null;
            this.OpenRadioButton.Checked = flg;
            this.CloseRadioButton.Checked = !flg;

            //作業履歴
            this.WorkHistoryList = this.GetWorkHistoryList();
            this.SetWorkHistory();

            //作業履歴が取得できたかどうか
            this.ListDataLabel.Text = this.WorkHistoryList.Any() == true ? "" : Resources.KKM00005;

        }

        /// <summary>
        /// 作業履歴設定
        /// </summary>
        private void SetWorkHistory()
        {
            //バインドデータを初期化
            this.HistoryDataGridView.DataSource = null;

            //削除フラグが無効のデータがあればバインド
            var target = this.WorkHistoryList.Where(x => x.DELETE_FLG == false).ToArray();
            if (target.Any() == true)
            {
                this.HistoryDataGridView.DataSource = target;

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
                CLASS_DATA = Const.FavoriteWorkProgressHistory

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
        /// <returns>IEnumerable<WorkProgressItemModel></returns>
        private WorkProgressItemModel GetScheduleItem(long id)
        {
            //パラメータ設定
            var cond = new WorkProgressItemSearchModel { ID = id };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkProgressItemSearchModel, WorkProgressItemModel>(ControllerType.WorkProgress, cond);

            //レスポンスが取得できたかどうか
            var list = new List<WorkProgressItemModel>();
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
                CATEGORY_ID = this.ScheduleItem.ID

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
            var list = this.GetEntryWorkHistoryModel();
            if (list != null && list.Any() == true)
            {
                //作業履歴登録
                var res = HttpUtil.PutResponse(ControllerType.WorkProgressHistory, list);

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
        private IEnumerable<WorkHistoryModel> GetEntryWorkHistoryModel()
        {
            var list = this.WorkHistoryList.Where(x => x.ID > 0 && x.DELETE_FLG == true).ToList();

            var now = DateTime.Now;

            foreach (DataGridViewRow row in this.HistoryDataGridView.Rows)
            {
                var id = Convert.ToDecimal(row.Cells[this.IdColumn.Name].Value);
                
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

                }

                list.Add(workHistory);

            }

            //登録対象が無い場合はダミーデータを設定
            if (list.Any() == false)
            {
                list.Add(new WorkHistoryModel
                {
                    //カテゴリーID
                    CATEGORY_ID = this.ScheduleItem.ID,

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
    }
}
