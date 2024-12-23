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

namespace DevPlan.Presentation.UIDevPlan.OperationPlan
{
    /// <summary>
    /// 業務計画表項目詳細
    /// </summary>
    public partial class OperationPlanItemForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "項目詳細（計画表）"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール情報</summary>
        public ScheduleItemModel<WorkScheduleItemGetOutModel> ITEM { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>所属グループID</summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>更新権限</summary>
        public bool IS_UPDATE { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OperationPlanItemForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 画面ロード
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperationPlanItemForm_Load(object sender, EventArgs e)
        {
            // 初期化
            this.Init();
        }
        #endregion

        #region 初期化
        public void Init()
        {
            //初期表示フォーカス
            this.ActiveControl = CategoryTextBox;

            if (this.ITEM == null) return;

            if (!this.ITEM.ScheduleItemEdit.Equals(ScheduleItemEditType.Update))
            {
                this.DeleteButton.Visible = false;
            }

            // 担当
            var sectiongroupcode = this.ITEM.ScheduleItem != null && this.ITEM.ScheduleItem.SECTION_GROUP_CODE != null
                ? this.ITEM.ScheduleItem.SECTION_GROUP_CODE : SessionDto.SectionGroupCode;
            var sectiongroupid = this.ITEM.ScheduleItem != null && this.ITEM.ScheduleItem.SECTION_GROUP_ID != null
                ? this.ITEM.ScheduleItem.SECTION_GROUP_ID : SessionDto.SectionGroupID;

            this.SectionGroupComboBox.DisplayMember = "CODE";
            this.SectionGroupComboBox.ValueMember = "ID";
            this.SectionGroupComboBox.DataSource 
                = new List<ComboBoxDto>{ new ComboBoxDto(){ CODE = sectiongroupcode, ID = sectiongroupid } };
            this.SectionGroupComboBox.SelectedValue = sectiongroupid;

            // 項目名
            this.CategoryTextBox.Text = this.ITEM.ScheduleItemEdit.Equals(ScheduleItemEditType.Update)
                ? this.ITEM.ScheduleItem.CATEGORY : string.Empty;

            // 更新権限
            if (!IS_UPDATE)
            {
                this.EntryButton.Visible = false;
                this.DeleteButton.Visible = false;
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

            // スケジュール項目更新
            if (this.ITEM != null && this.ITEM.ScheduleItemEdit.Equals(ScheduleItemEditType.Update))
            {
                //スケジュール項目のチェック
                if (this.IsEntryScheduleItem(ScheduleItemEditType.Update))
                {
                    var res = this.PutData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00002); // 登録（更新）完了

                        base.FormOkClose();
                    }
                }
            }
            // スケジュール登録
            else
            {
                //スケジュール項目のチェック
                if (this.IsEntryScheduleItem(ScheduleItemEditType.Insert))
                {
                    var res = this.PostData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00002); // 登録（更新）完了

                        base.FormOkClose();
                    }
                }
            }
        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (Messenger.Confirm(Resources.KKM00007)   // 削除確認
                .Equals(DialogResult.No)) return;

            // スケジュール項目削除
            if (this.ITEM != null && this.ITEM.ScheduleItemEdit.Equals(ScheduleItemEditType.Update))
            {
                //スケジュール項目のチェック
                if (this.IsEntryScheduleItem(ScheduleItemEditType.Delete))
                {
                    var res = this.DeleteData();

                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        Messenger.Info(Resources.KKM00003); // 削除完了

                        base.FormOkClose();
                    }
                }
            }
        }
        #endregion

        #region 担当コンボボックスクリック
        /// <summary>
        /// 担当コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupComboBox_Click(object sender, EventArgs e)
        {
            this.label1.Focus();

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                // 担当検索
                if (form.ShowDialog().Equals(DialogResult.OK) && form.SECTION_GROUP_ID != null)
                {
                    // パラメータ取得
                    var code = form.SECTION_GROUP_CODE;
                    var id = form.SECTION_GROUP_ID;

                    // コンボボックス設定
                    this.SectionGroupComboBox.DataSource = new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                    this.SectionGroupComboBox.SelectedValue = id;
                }

            }

        }
        #endregion

        #region スケジュール項目のチェック
        /// <summary>
        /// スケジュール項目のチェック
        /// </summary>
        /// <param name="type">スケジュール編集区分</param>
        /// <returns>チェック可否</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type)
        {
            var flg = true;

            var item = this.ITEM.ScheduleItem;

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);

                return false;

            }

            //スケジュール編集区分ごとの分岐
            switch (type)
            {
                //更新
                //削除
                case ScheduleItemEditType.Update:
                case ScheduleItemEditType.Delete:
                    //データが存在しているかどうか
                    item = this.GetData();
                    if (item == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Info(Resources.KKM00021);
                        flg = false;
                    }
                    else
                    {
                        //スケジュール再設定
                        this.ITEM.ScheduleItem = item;
                    }
                    break;
            }

            return flg;
        }
        #endregion

        #region データ取得処理
        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns>WorkScheduleItemGetOutModel</returns>
        private WorkScheduleItemGetOutModel GetData()
        {
            WorkScheduleItemGetOutModel item = null;

            //検索条件
            var cond = new WorkScheduleItemGetInModel { SCHEDULE_ID = this.ITEM.ID };

            //APIで取得
            var res = HttpUtil.GetResponse<WorkScheduleItemGetInModel, WorkScheduleItemGetOutModel>(ControllerType.WorkScheduleItem, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status.Equals(Const.StatusSuccess))
            {
                item = res.Results.FirstOrDefault();
            }

            return item;
        }
        #endregion

        #region データ登録処理
        /// <summary>
        /// データ登録処理
        /// </summary>
        private ResponseDto<WorkScheduleItemPostInModel> PostData()
        {
            var data = new WorkScheduleItemPostInModel
            {
                // 開発符号
                GENERAL_CODE = this.ITEM.ScheduleItem != null && this.ITEM.ScheduleItem.GENERAL_CODE != null 
                    ? this.ITEM.ScheduleItem.GENERAL_CODE : this.GENERAL_CODE,
                // カテゴリー
                CATEGORY = this.CategoryTextBox.Text,
                // 所属グループID
                SECTION_GROUP_ID = this.SectionGroupComboBox.SelectedValue.ToString(),
                // 並び順
                SORT_NO = this.ITEM.SortNo <= 0 ? 0 : this.ITEM.SortNo + 0.1,
                // 行数
                PARALLEL_INDEX_GROUP = 1,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            return HttpUtil.PostResponse<WorkScheduleItemPostInModel>(ControllerType.WorkScheduleItem, data);
        }
        #endregion

        #region データ更新処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private ResponseDto<WorkScheduleItemPutInModel> PutData()
        {
            var data = new WorkScheduleItemPutInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.ITEM.ID,
                // カテゴリー
                CATEGORY = this.CategoryTextBox.Text,
                // 所属グループID
                SECTION_GROUP_ID = this.SectionGroupComboBox.SelectedValue.ToString(),
                // 並び順
                SORT_NO = this.ITEM.SortNo,
                // 行数
                PARALLEL_INDEX_GROUP = this.ITEM.RowCount,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId
            };

            return HttpUtil.PutResponse<WorkScheduleItemPutInModel>(ControllerType.WorkScheduleItem, data);
        }
        #endregion        

        #region データ削除処理
        /// <summary>
        /// データ削除処理
        /// </summary>
        private ResponseDto<WorkScheduleItemDeleteInModel> DeleteData()
        {
            var data = new WorkScheduleItemDeleteInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.ITEM.ID
            };

            return HttpUtil.DeleteResponse<WorkScheduleItemDeleteInModel>(ControllerType.WorkScheduleItem, data);
        }
        #endregion
    }
}
