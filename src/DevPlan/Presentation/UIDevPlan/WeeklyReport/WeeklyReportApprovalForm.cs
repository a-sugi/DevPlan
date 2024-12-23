using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.WeeklyReport
{
    /// <summary>
    /// 担当検索
    /// </summary>
    public partial class WeeklyReportApprovalForm : BaseSubForm
    {
        #region メンバ変数
        /// <summary>期間</summary>
        private DateTime WeekendDate;
        /// <summary>部ID</summary>
        private String DepartmentID;
        /// <summary>課ID</summary>
        private String SectionID;
        /// <summary>課グループID</summary>
        private String SectionGroupID;
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "承認履歴"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return 680; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return 360; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>期間</summary>
        public DateTime WEEKEND_DATE { get { return WeekendDate; } set { WeekendDate = value; } }
        /// <summary>部ID</summary>
        public String DEPARTMENT_ID { get { return DepartmentID; } set { DepartmentID = value; } }
        /// <summary>課ID</summary>
        public String SECTION_ID { get { return SectionID; } set { SectionID = value; } }
        /// <summary>担当ID</summary>
        public String SECTION_GROUP_ID { get { return SectionGroupID; } set { SectionGroupID = value; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WeeklyReportApprovalForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 画面表示
        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportApproveForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }
        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //タイトル表示
            base.ListFormTitleLabel.Text = this.FormTitle;

            //承認履歴表示
            this.SetGridData();
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            //グリッドデータ初期化
            this.ApproveHistoryListDataGridView.Rows.Clear();
            this.ApproveHistoryListDataGridView.AutoGenerateColumns = false;
            this.ApproveHistoryListDataGridView.DataSource = GetGridDataList();

            //状態（日本語）を表示
            foreach (DataGridViewRow row in this.ApproveHistoryListDataGridView.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals("0"))
                {
                    row.Cells[1].Value = "承認解除";
                }
                else if (row.Cells[0].Value.ToString().Equals("1"))
                {
                    row.Cells[1].Value = "承認";
                }
            }

            if(this.ApproveHistoryListDataGridView.Rows.Count == 0)
            {
                //情報がない場合メッセージ表示
                //Messenger.Info(Resources.KKM00005);
                MessageLabel.Text = Resources.KKM00005;
            }
        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        /// <returnsList<WeeklyReportApproveOutModel>></returns>
        private List<WeeklyReportApproveOutModel> GetGridDataList()
        {
            // パラメータ設定
            var itemCond = new WeeklyReportApproveInModel
            {
                // 期間
                WEEKEND_DATE = this.WeekendDate,

                // 部ID
                DEPARTMENT_ID = this.DepartmentID,

                // 課ID
                SECTION_ID = this.SectionID,

                // 担当ID
                SECTION_GROUP_ID = this.SectionGroupID
            };

            // Get実行
            var res = HttpUtil.GetResponse<WeeklyReportApproveInModel, WeeklyReportApproveOutModel>(ControllerType.WeeklyReportApproval, itemCond);

            return (res.Results).ToList();
        }
        #endregion
    }
}
