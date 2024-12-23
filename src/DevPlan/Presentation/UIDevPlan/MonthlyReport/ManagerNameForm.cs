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
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MonthlyReport
{
    /// <summary>
    /// 部長名設定
    /// </summary>
    public partial class ManagerNameForm : BaseSubForm
    {
        #region メンバ変数
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "部長名設定"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return 300; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>部ID</summary>
        public string DepartmentID { get; set; }

        /// <summary>部長名</summary>
        public string ManagerName { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ManagerNameForm()
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
        private void ManagerNameForm_Load(object sender, EventArgs e)
        {
            // 初期化
            this.Init();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            //初期表示フォーカス
            this.ActiveControl = ManagerNameTextBox;

            //部IDから部長名を取得
            if (this.DepartmentID == null)
            {
                return;
            }

            //パラメータ設定
            var itemCond = new ManagerNameSearchModel
            {
                //部ID
                DEPARTMENT_ID = this.DepartmentID,
            };

            //Get実行
            var res = HttpUtil.GetResponse<ManagerNameSearchModel, ManagerNameModel>(ControllerType.ManagerName, itemCond);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //取得した部長名を画面に表示
                List<ManagerNameModel> list = (res.Results).ToList();
                if ((list != null) && (0 < list.Count))
                {
                    ManagerNameTextBox.Text = list[0].MANAGER_NAME;
                }
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
                //部長名を戻り値に設定
                bool result = this.SetManagerName();

                //ダイアログを閉じる
                if (result == true)
                {
                    base.FormOkClose();
                }
            });
        }
        #endregion

        #region 部長名設定
        /// <summary>
        /// 部長名設定
        /// </summary>
        private bool SetManagerName()
        {
            if (0 < ManagerNameTextBox.Text.Length)
            {
                this.ManagerName = ManagerNameTextBox.Text;
                return true;
            }
            else
            {
                Messenger.Warn(string.Format(Resources.KKM00001, "部長名"));
                return false;
            }
        }
        #endregion
    }
}
