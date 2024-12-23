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
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 項目移動フォーム（開発符号）
    /// </summary>
    public partial class ScheduleItemGeneralCodeMoveForm<item> : BaseSubForm where item : class, new()
    {
        /// <summary>開発符号検索出力モデルクラス</summary>
        private GeneralCodeSearchOutModel SelectRow;

        /// <summary>画面名</summary>
        public override string FormTitle
        {
            get
            {
                return string.Format("項目移動({0})", FormSubTitle);
            }
        }

        /// <summary>画面名(サブ)</summary>
        public ScheduleItemType FormSubType { get; set; }

        /// <summary>スケジュール項目</summary>
        public ScheduleItemModel<item> ScheduleItem { get; set; }

        /// <summary>画面名(サブ)</summary>
        public string FormSubTitle { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScheduleItemGeneralCodeMoveForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemGeneralCodeMoveForm_Load(object sender, EventArgs e)
        {
            this.MessageLabel.Text = string.Format(this.MessageLabel.Text, FormSubTitle);

            // 他画面のテンプレート調整（専用テンプレート作成要）
            this.generalCodeListMultiRowTemplate1.Width = 359;
            this.generalCodeListMultiRowTemplate1.ColumnHeaders[0].Cells["columnHeaderCell2"].Size = new System.Drawing.Size(188, this.generalCodeListMultiRowTemplate1.ColumnHeaders[0].Cells["columnHeaderCell2"].Size.Height);
            this.generalCodeListMultiRowTemplate1.Row.Cells["GeneralCodeDataGridViewTextBoxColumn"].Size = new System.Drawing.Size(188, this.generalCodeListMultiRowTemplate1.Row.Cells["GeneralCodeDataGridViewTextBoxColumn"].Size.Height);

            ScheduleItemMoveMultiRow.Template =
                  new CustomTemplate() { MultiRow = ScheduleItemMoveMultiRow }.SetContextMenuTemplate(generalCodeListMultiRowTemplate1);

            InitForm(UnderDevelopmentCheckBox.Checked);
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
                this.SelectRow = (GeneralCodeSearchOutModel)((GcMultiRow)sender).Rows[e.RowIndex].DataBoundItem;
            }
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
                GENERAL_CODE = this.SelectRow.GENERAL_CODE,
                SCHEDULE_ITEM_TYPE = FormSubType
            };

            var result = HttpUtil.PutResponse<bool>(ControllerType.ScheduleItemMove, cond, false);

            if (result.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00002);
                base.FormOkClose();
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
            var itemCond = new GeneralCodeSearchInModel
            {
                PERSONEL_ID = SessionDto.UserId,
                UNDER_DEVELOPMENT = ((underDevelopmentCheck == true) ? "1" : null)
            };

            var list = new List<GeneralCodeSearchOutModel>();
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, itemCond);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list = (res.Results).ToList();
            }

            this.ScheduleItemMoveMultiRow.DataSource = list;
            this.ScheduleItemMoveMultiRow.CurrentCell = null;
            this.SelectRow = null;
        }
    }
}
