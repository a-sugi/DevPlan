using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 検討会課題履歴
    /// </summary>
    public partial class MeetingDocumentHistoryForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "検討会課題履歴"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return false; } }

        /// <summary>検討会資料詳細</summary>
        public MeetingDocumentDetailModel MeetingDocumentDetai { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MeetingDocumentHistoryForm()
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
        private void MeetingDocumentHistoryForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            FormControlUtil.FormWait(this, () => this.InitForm());

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //描画停止
            this.HistoryDataGridView.SuspendLayout();

            //元の設定値を取得
            var autoSizeColumnsMode = this.HistoryDataGridView.AutoSizeColumnsMode;
            var autoSizeRowsMode = this.HistoryDataGridView.AutoSizeRowsMode;
            var columnHeadersHeightSizeMode = this.HistoryDataGridView.ColumnHeadersHeightSizeMode;

            //行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない
            this.HistoryDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.HistoryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.HistoryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //ダブルバァッファリング有効化
            var type = typeof(DataGridView);
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.HistoryDataGridView, true);

            //検討会資料履歴一覧の設定
            this.HistoryDataGridView.AutoGenerateColumns = false;
            this.HistoryDataGridView.DataSource = this.GetMeetingDocumentDetailList();

            //ヘッダーの改行コードを置き換え
            foreach (DataGridViewColumn col in this.HistoryDataGridView.Columns)
            {
                var headerText = col.HeaderText;
                col.HeaderText = headerText.Replace(@"\n", Const.CrLf);

                //改行がある場合は折り返し
                if (col.HeaderText != headerText)
                {
                    col.HeaderCell.Style.WrapMode = DataGridViewTriState.True;

                }

            }

            //列ヘッダーの幅で再設定
            this.HistoryDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);

            //元の設定値を復元
            this.HistoryDataGridView.AutoSizeColumnsMode = autoSizeColumnsMode;
            this.HistoryDataGridView.AutoSizeRowsMode = autoSizeRowsMode;
            this.HistoryDataGridView.ColumnHeadersHeightSizeMode = columnHeadersHeightSizeMode;

            //描画再開
            this.HistoryDataGridView.ResumeLayout(true);


        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeetingDocumentHistoryForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.HistoryDataGridView.CurrentCell = null;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// 検討会資料詳細の取得
        /// </summary>
        /// <returns></returns>
        private List<MeetingDocumentDetailModel> GetMeetingDocumentDetailList()
        {
            //パラメータ設定
            var cond = new MeetingDocumentDetailSearchModel
            {
                //カテゴリーID
                CATEGORY_ID = this.MeetingDocumentDetai.CATEGORY_ID

            };

            //APIで取得
            var res = HttpUtil.GetResponse<MeetingDocumentDetailSearchModel, MeetingDocumentDetailModel>(ControllerType.MeetingDocumentDetail, cond);

            //レスポンスが取得できたかどうか
            var list = new List<MeetingDocumentDetailModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

    }
}
