using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 項目移動フォーム（車系）
    /// </summary>
    public partial class ScheduleItemCarGroupMoveForm<item> : BaseSubForm where item : class, new()
    {
        /// <summary>車系検索出力モデルクラス</summary>
        private CarGroupSearchOutModel SelectRow;

        /// <summary>画面名</summary>
        public override string FormTitle { get { return string.Format("項目移動({0})", FormSubTitle); } }

        /// <summary>画面名(サブ)</summary>
        public ScheduleItemType FormSubType { get; set; }

        /// <summary>スケジュール項目</summary>
        public ScheduleItemModel<item> ScheduleItem { get; set; }

        /// <summary>画面名(サブ)</summary>
        public string FormSubTitle { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScheduleItemCarGroupMoveForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemCarGroupMoveForm_Load(object sender, EventArgs e)
        {
            this.MessageLabel.Text = string.Format(this.MessageLabel.Text, FormSubTitle);

            ScheduleItemMoveMultiRow.Template =
                  new CustomTemplate() { MultiRow = ScheduleItemMoveMultiRow }.SetContextMenuTemplate(carGroupListMultiRowTemplate1);

            InitForm(UnderDevelopmentCheckBox.Checked);
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            if (this.SelectRow == null || this.ScheduleItemMoveMultiRow.CurrentCell is FilteringTextBoxCell)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            var cond = new ScheduleItemMoveInModel
            {
                ID = this.ScheduleItem.ID,
                GENERAL_CODE = this.SelectRow.CAR_GROUP,
                SCHEDULE_ITEM_TYPE  = FormSubType   
            };

            var result = HttpUtil.PutResponse<bool>(ControllerType.ScheduleItemMove, cond, false);

            if (result.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
                base.FormOkClose();
            }
        }

        /// <summary>
        /// 行クリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemMoveMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.SelectRow = (CarGroupSearchOutModel)((GcMultiRow)sender).Rows[e.RowIndex].DataBoundItem;
            }
        }

        /// <summary>
        /// 開発中も表示するチェックボックス切り替え時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderDevelopmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            InitForm(UnderDevelopmentCheckBox.Checked);
        }

        /// <summary>
        /// グリッド初期化
        /// </summary>
        /// <param name="underDevelopmentCheck"></param>
        private void InitForm(bool underDevelopmentCheck)
        {
            var list = new List<CarGroupSearchOutModel>();
            var res = HttpUtil.GetResponse<CarGroupSearchInModel, CarGroupSearchOutModel>
                (ControllerType.CarGroup, new CarGroupSearchInModel
                {
                    PERSONEL_ID = SessionDto.UserId,
                    UNDER_DEVELOPMENT = ((underDevelopmentCheck == true) ? 1 : (int?)null)
                });

            if (res != null && res.Status == Const.StatusSuccess)
            {
                list = (res.Results).ToList();
            }
            list.RemoveAll(x => x.CAR_GROUP == this.ScheduleItem.GeneralCode);

            this.ScheduleItemMoveMultiRow.DataSource = list;
            this.ScheduleItemMoveMultiRow.CurrentCell = null;
            this.SelectRow = null;
        }
    }
}
