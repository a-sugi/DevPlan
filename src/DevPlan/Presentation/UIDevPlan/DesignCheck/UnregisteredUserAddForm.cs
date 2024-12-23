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

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 未登録参加者追加
    /// </summary>
    public partial class UnregisteredUserAddForm : BaseSubForm
    {
        #region 内部変数

        /// <summary>
        /// 部全件リスト
        /// </summary>
        private List<DepartmentModel> Departs = new List<DepartmentModel>();

        /// <summary>
        /// 課全件リスト
        /// </summary>
        private List<SectionModel> Sections = new List<SectionModel>();

        /// <summary>
        /// 担当全件リスト
        /// </summary>
        private List<SectionGroupModel> Groups = new List<SectionGroupModel>();

        #endregion

        #region プロパティ

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "未登録参加者追加"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>
        /// 参加者氏名
        /// </summary>
        public string NAME { get; private set; }

        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; private set; }

        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; private set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnregisteredUserAddForm()
        {
            InitializeComponent();
        }


        #endregion

        #region 画面ロード時
        /// <summary>
        /// 画面ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnregisteredUserAddForm_Load(object sender, EventArgs e)
        {
            // 取得
            GetDepars();
            GetSections();
            GetGroups();

            // 設定
            cmbDepartment.DataSource = Departs;
            cmbDepartment.DisplayMember = "DEPARTMENT_CODE";
            cmbDepartment.ValueMember = "DEPARTMENT_ID";
            cmbSection.DataSource = Sections;
            cmbSection.DisplayMember = "SECTION_CODE";
            cmbSection.ValueMember = "SECTION_ID";
            cmbGroup.DataSource = Groups;
            cmbGroup.DisplayMember = "SECTION_GROUP_CODE";
            cmbGroup.ValueMember = "SECTION_GROUP_ID";

            // バリデーション設定
            this.TxtName.Tag = Const.ItemName+ "(氏名);" + Const.Byte + "(50);" + Const.Required;
        }
        #endregion

        #region 画面表示後
        /// <summary>
        /// 画面表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnregisteredUserAddForm_Shown(object sender, EventArgs e)
        {
            // イベント登録
            cmbDepartment.SelectedIndexChanged += cmbDepartment_SelectedIndexChanged;
            cmbSection.SelectedIndexChanged += cmbSection_SelectedIndexChanged;
            cmbGroup.SelectedIndexChanged += cmbGroup_SelectedIndexChanged;

            this.ActiveControl = TxtName;
            TxtName.Focus();
        }
        #endregion

        #region 登録ボタン押下時
        /// <summary>
        /// 登録ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            // バリデーションチェック
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;
            }

            this.NAME = this.TxtName.Text;

            DEPARTMENT_ID = cmbDepartment.SelectedValue == null ? "" : cmbDepartment.SelectedValue.ToString();
            DEPARTMENT_CODE = cmbDepartment.Text;
            SECTION_ID = cmbSection.SelectedValue == null ? "" : cmbSection.SelectedValue?.ToString();
            SECTION_CODE = cmbSection.Text;
            SECTION_GROUP_ID = cmbGroup.SelectedValue == null ? "" : cmbGroup.SelectedValue?.ToString();
            SECTION_GROUP_CODE = cmbGroup.Text;

            base.FormOkClose();
        }
        #endregion

        #region 部取得
        /// <summary>
        /// 部取得
        /// </summary>
        private void GetDepars()
        {
            var res = HttpUtil.GetResponse<DepartmentSearchModel, DepartmentModel>(ControllerType.Department, new DepartmentSearchModel());

            Departs = (res.Results).ToList();
            Departs.Insert(0, new DepartmentModel());
        }
        #endregion

        #region 課取得
        /// <summary>
        /// 課取得
        /// </summary>
        private void GetSections()
        {
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, new SectionSearchModel());

            Sections = (res.Results).ToList();
            Sections.Insert(0, new SectionModel());
        }
        #endregion

        #region 担当取得
        /// <summary>
        /// 担当取得
        /// </summary>
        private void GetGroups()
        {
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, new SectionGroupSearchModel());

            Groups = (res.Results).ToList();
            Groups.Insert(0, new SectionGroupModel());
        }
        #endregion

        #region 部変更時
        /// <summary>
        /// 部変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged(() =>
            {
                // 課の設定
                SetSections();
                cmbSection.SelectedIndex = -1;

                // 担当の設定
                SetGroups();
                cmbGroup.SelectedIndex = -1;
            });
        }
        #endregion

        #region 課変更時
        /// <summary>
        /// 課変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged(() =>
            {
                if (cmbSection.SelectedValue == null)
                {
                    // 未選択時は自分のみ再設定
                    SetSections();
                }
                else
                {
                    // 選択時は部確定
                    var model = Sections.Single((x) => x.SECTION_ID == Convert.ToString(cmbSection.SelectedValue));

                    // 部の設定
                    cmbDepartment.SelectedValue = model.DEPARTMENT_ID;

                    // 課の設定
                    SetSections();
                    cmbSection.SelectedValue = model.SECTION_ID;
                }

                // 担当の設定
                SetGroups();
                cmbGroup.SelectedIndex = -1;
            });
        }
        #endregion

        #region 担当変更時
        /// <summary>
        /// 担当変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged(() =>
            {
                if (cmbGroup.SelectedValue == null)
                {
                    // 未選択時は自分のみ再設定
                    SetGroups();
                }
                else
                {
                    // 選択時は部・課確定
                    var model = Groups.Single((x) => x.SECTION_GROUP_ID == Convert.ToString(cmbGroup.SelectedValue));

                    // 部の設定
                    cmbDepartment.SelectedValue = model.DEPARTMENT_ID;

                    // 課の設定
                    SetSections();
                    cmbSection.SelectedValue = model.SECTION_ID;

                    // 担当の設定
                    SetGroups();
                    cmbGroup.SelectedValue= model.SECTION_GROUP_ID;
                }
            });
        }
        #endregion

        #region 課のデータソースを設定します。
        /// <summary>
        /// 課のデータソースを設定します。
        /// </summary>
        private void SetSections()
        {
            if (cmbDepartment.SelectedValue == null)
            {
                cmbSection.DataSource = Sections;
            }
            else
            {
                var dtId = Convert.ToString(cmbDepartment.SelectedValue);
                cmbSection.DataSource = Sections.Where((x) => x.DEPARTMENT_ID == dtId || x.DEPARTMENT_ID == null).ToList();
            }
        }
        #endregion

        #region 担当のデータソースを設定します。
        /// <summary>
        /// 担当のデータソースを設定します。
        /// </summary>
        private void SetGroups()
        {
            if (cmbDepartment.SelectedValue == null)
            {
                cmbGroup.DataSource = Groups;
            }
            else
            { 
                if (cmbSection.SelectedValue == null)
                {
                    var dtId = Convert.ToString(cmbDepartment.SelectedValue);
                    cmbGroup.DataSource = Groups.Where((x) => x.DEPARTMENT_ID == dtId || x.DEPARTMENT_ID == null).ToList();
                }
                else
                {
                    var scId = Convert.ToString(cmbSection.SelectedValue);
                    cmbGroup.DataSource = Groups.Where((x) => x.SECTION_ID == scId || x.SECTION_ID == null).ToList();
                }
            }
        }
        #endregion

        #region コンボボックス変更時の処理を実行する際はイベントを止める
        /// <summary>
        /// コンボボックス変更時の処理を実行する際はイベントを止める
        /// </summary>
        /// <param name="method"></param>
        private void SelectedIndexChanged(Action method)
        {
            cmbDepartment.SelectedIndexChanged -= cmbDepartment_SelectedIndexChanged;
            cmbSection.SelectedIndexChanged -= cmbSection_SelectedIndexChanged;
            cmbGroup.SelectedIndexChanged -= cmbGroup_SelectedIndexChanged;

            method.Invoke();

            cmbDepartment.SelectedIndexChanged += cmbDepartment_SelectedIndexChanged;
            cmbSection.SelectedIndexChanged += cmbSection_SelectedIndexChanged;
            cmbGroup.SelectedIndexChanged += cmbGroup_SelectedIndexChanged;
        }
        #endregion
    }
}
