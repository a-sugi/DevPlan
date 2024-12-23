using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 受領票選択
    /// </summary>
    public partial class DesignCheckPcAuthForm : BaseSubForm
    {
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "設計チェックPC選択"; } }
        public override bool IsFormSizeFixed { get { return true; } }


        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// PC種別(戻り値)
        /// </summary>
        public string PcAuthKind { get; set; }
        #endregion

        public DesignCheckPcAuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCopyMoveForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 印刷ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            //選択項目設定
            var status = FormControlUtil.GetRadioButtonValue(this.StatusPanel);
            if (status != null)
            {
                this.PcAuthKind = status.ToString();
            }

            //閉じる
            base.FormOkClose();
        }
    }
}
