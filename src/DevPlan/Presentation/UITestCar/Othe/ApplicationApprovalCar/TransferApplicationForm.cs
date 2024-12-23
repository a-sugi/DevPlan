using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    /// <summary>
    /// 移管申請
    /// </summary>
    public partial class TransferApplicationForm : BaseSubForm
    {
        #region メンバ変数
        private List<TestCarCollisionCarManagementDepartmentModel> testCarCollisionSectionGroup = null;

        private const string Ikan = "{0}へ移管";

        private string departmentCode = null;
        private string sectionCode = null;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "移管申請"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        public ApplicationApprovalCarModel TestCar { get; set; }

        /// <summary>所属</summary>
        public string Affiliation { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TransferApplicationForm()
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
        private void DisposalApplicationForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //試験車衝突車管理部署
            this.testCarCollisionSectionGroup = this.testCarCollisionSectionGroup ?? this.GetTestCarCollisionSectionGroup();

            //管理票NO
            this.ManagementNoLabel.Text = this.TestCar.管理票NO;

            //実走行距離
            this.RunningDistanceTextBox.Text = this.TestCar.実走行距離;
            this.ActiveControl = this.RunningDistanceTextBox;

            //移管先部署
            FormControlUtil.ClearComboBoxDataSource(this.SectionGroupComboBox);

        }
        #endregion

        #region 移管先部署マウスダウン
        /// <summary>
        /// 移管先部署マウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID, ESTABLISHMENT = this.Affiliation })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_GROUP_ID,
                        NAME = form.SECTION_GROUP_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionGroupComboBox, new[] { value }, false);
                    this.SectionGroupComboBox.SelectedIndex = 0;

                    //部・課をセット
                    this.departmentCode = form.DEPARTMENT_CODE;
                    this.sectionCode = form.SECTION_CODE;
                }

            }

        }

        #endregion

        #region OKボタンクリック
        /// <summary>
        /// OKボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            //登録可能かどうか
            if (this.IsEntry() == true)
            {
                //試験内容
                this.TestCar.試験内容 = string.Format(Ikan, string.Format("{0}-{1}-{2}", this.departmentCode, this.sectionCode, this.SectionGroupComboBox.Text));

                //実走行距離
                this.TestCar.実走行距離 = this.RunningDistanceTextBox.Text;

                //移管先部署ID
                this.TestCar.移管先部署ID = this.SectionGroupComboBox.SelectedValue.ToString();

                //フォームクローズ
                this.FormOkClose();

            }

        }

        /// <summary>
        /// 登録可否をチェック
        /// </summary>
        /// <returns></returns>
        private bool IsEntry()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //移管先部署
            map[this.SectionGroupComboBox] = (c, name) =>
            {
                var errMsg = "";

                //管理責任部署の課が試験車衝突車管理部署で
                //登録ナンバーと駐車場番号がなしで
                //移管先部署が試験車衝突車管理部署以外はエラー
                if ((this.testCarCollisionSectionGroup.Any(x => x.SECTION_ID == this.TestCar.SECTION_ID) == true) &&
                    (string.IsNullOrWhiteSpace(this.TestCar.登録ナンバー) == true && string.IsNullOrWhiteSpace(this.TestCar.駐車場番号) == true) &&
                    (this.testCarCollisionSectionGroup.Any(x => x.SECTION_GROUP_ID == this.SectionGroupComboBox.SelectedValue.ToString()) == false))
                {
                    errMsg = Resources.TCM03013;

                }

                return errMsg;

            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }
        #endregion

        #region API
        /// <summary>
        /// 試験車衝突車管理部署取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarCollisionCarManagementDepartmentModel> GetTestCarCollisionSectionGroup()
        {
            var cond = new TestCarCollisionCarManagementDepartmentSearchModel { IS_SECTION_GROUP = true };

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarCollisionCarManagementDepartmentSearchModel, TestCarCollisionCarManagementDepartmentModel>(ControllerType.TestCarCollisionCarManagementDepartment, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarCollisionCarManagementDepartmentModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion
    }
}
