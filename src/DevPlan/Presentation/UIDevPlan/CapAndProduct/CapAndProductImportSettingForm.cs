using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{
    /// <summary>
    /// CAPデータインポート設定画面
    /// </summary>
    public partial class CapAndProductImportSettingForm : BaseSubDialogForm
    {
        /// <summary>
        /// 指摘No
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 仕向地
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CapAndProductImportSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle
        {
            get
            {
                return "インポートデータ設定";
            }
        }

        /// <summary>
        /// Noテキストの入力制限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 0～9と、バックスペース以外の時は、イベントキャンセル
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportSettingForm_Load(object sender, EventArgs e)
        {
            txtNo.Text = No.ToString();

            cmbPlace.ValueMember = "仕向";
            cmbPlace.DisplayMember = "仕向";
            cmbPlace.DataSource = (new[] { new CapLocationModel() }).Concat(HttpUtil.GetResponse<CapLocationModel>(ControllerType.CapLocation)?.Results).ToList();

            txtNo.Tag = "ItemName(指摘No);" + Const.Required;
            cmbPlace.Tag = "ItemName(仕向地);" + Const.Required;
        }

        /// <summary>
        /// OKボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;
            }

            No = txtNo.Text;
            Place = cmbPlace.Text;

            FormOkClose();
        }
    }
}
