using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.Presentation.UITestCar.ControlSheet;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UITestCar.Othe.TestCarHistory
{
    /// <summary>
    /// 使用履歴承認状況
    /// </summary>
    public partial class TestCarUseHistoryApprovalForm : BaseSubForm
    {
        #region メンバ変数
        private DataGridViewUtil<TestCarUseHistoryApprovalModel> gridUtil = null;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "使用履歴承認状況"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>試験車使用履歴</summary>
        public TestCarUseHistoryModel TestCarUseHistory { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarUseHistoryApprovalForm()
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
        private void TestCarUseHistoryApprovalForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //画面初期化
                this.InitForm();

                //グリッドビュー初期化
                this.InitGridView();

            });

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //履歴日付
            this.HistoryDateLabel.Text = DateTimeUtil.ConvertDateString(this.TestCarUseHistory.処理日);

            //履歴内容
            this.HistoryContentsLabel.Text = this.TestCarUseHistory.試験内容;

        }

        /// <summary>
        /// グリッドビュー初期化
        /// </summary>
        private void InitGridView()
        {
            //データグリッドビュー初期化
            this.gridUtil = new DataGridViewUtil<TestCarUseHistoryApprovalModel>(this.ListDataGridView, false);

            //試験車使用履歴承認状況取得
            this.gridUtil.DataSource = this.GetTestCarUseHistoryApprovalList();

        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarUseHistoryApprovalForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;

        }
        #endregion

        #region API
        /// <summary>
        /// 試験車使用履歴承認状況取得
        /// </summary>
        /// <returns></returns>
        private List<TestCarUseHistoryApprovalModel> GetTestCarUseHistoryApprovalList()
        {
            var cond = new TestCarUseHistorySearchModel
            {
                //データID
                データID = this.TestCarUseHistory.データID,

                //履歴NO
                履歴NO = this.TestCarUseHistory.履歴NO,

                //SEQNO
                SEQNO = this.TestCarUseHistory.SEQNO

            };

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarUseHistorySearchModel, TestCarUseHistoryApprovalModel>(ControllerType.TestCarUseHistoryApproval, cond);

            //レスポンスが取得できたかどうか
            var list = new List<TestCarUseHistoryApprovalModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion
    }
}
