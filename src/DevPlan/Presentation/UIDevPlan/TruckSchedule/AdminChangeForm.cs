using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// 管理者情報変更画面
    /// </summary>
    public partial class AdminChangeForm : BaseSubForm
    {
        /// <summary>
        /// 画面名
        /// </summary>
        public override string FormTitle { get { return "トラック管理者変更"; } }

        /// <summary>
        /// 権限
        /// </summary>
        public UserAuthorityOutModel UserAuthority { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AdminChangeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminChangeForm_Load(object sender, EventArgs e)
        {
            var res= HttpUtil.GetResponse<TruckManagementUserModel>(ControllerType.TruckManagementUser);
            var list = new List<TruckManagementUserModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }
            
            ComboBoxSetting.SetComboBox(ReservedPersonComboBox, list.First().PERSONEL_ID, list.First().NAME);

            this.RequesterTelTextBox.Text = list.First().TEL;
            
            this.UserAuthority = base.GetFunction(FunctionID.Truck);

            this.ActiveControl = ReservedPersonComboBox;
        }

        /// <summary>
        /// 管理者名コンボボックスクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservedPersonComboBox_DropDown(object sender, EventArgs e)
        {
            using (var form = new UserListForm
            {
                DepartmentCode = SessionDto.DepartmentCode,
                SectionCode = SessionDto.SectionCode,
                UserAuthority = this.UserAuthority,
                IsSearchLimit = false,
                StatusCode = "a"
            })
            {
                var result = form.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    ComboBoxSetting.SetComboBox(ReservedPersonComboBox, form.User.PERSONEL_ID, form.User.NAME);
                }
                SendKeys.Send("{ENTER}");
                if (result == DialogResult.OK)
                {
                    this.RequesterTelTextBox.Text = new ADUtil().GetUserData(ADUtilTypeEnum.TEL, form.User.PERSONEL_ID, form.User.NAME);
                }
            }
        }

        /// <summary>
        /// 登録ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            var msg = Validator.GetFormInputErrorMessage(this);
            
            if (msg != "")
            {
                Messenger.Warn(msg);
            }
            else
            {
                var val = new TruckManagementUserModel
                {
                    PERSONEL_ID = ((ComboBoxSetting)ReservedPersonComboBox.SelectedItem).Key,
                    TEL = this.RequesterTelTextBox.Text
                };

                var res = HttpUtil.PutResponse<TruckManagementUserModel>(ControllerType.TruckManagementUser, val);

                if (res == null || res.Status != Const.StatusSuccess)
                {
                    Messenger.Warn(res.ErrorMessage);
                }
                else
                {
                    Messenger.Info(Resources.KKM00002);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
