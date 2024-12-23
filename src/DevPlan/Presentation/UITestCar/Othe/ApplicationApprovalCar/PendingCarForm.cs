using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Config;

using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UITestCar.Othe.TestCarHistory;

namespace DevPlan.Presentation.UITestCar.Othe.ApplicationApprovalCar
{
    /// <summary>
    /// 処理待ち状況
    /// </summary>
    public partial class PendingCarForm : BaseTestCarForm
    {
        #region メンバ変数

        /// <summary>
        /// バインドソース
        /// </summary>
        private BindingSource DataSource = new BindingSource();

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate CustomTemplate = new CustomTemplate();

        /// <summary>
        /// バインド中可否
        /// </summary>
        private bool IsBind { get; set; } = false;

        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "処理待ち状況"; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>取得リスト</summary>
        private List<PendingCarModel> DataList { get; set; } = new List<PendingCarModel>();

        /// <summary>取得リスト(仮)</summary>
        public List<PendingCarModel> TempDataList { get; set; } = new List<PendingCarModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PendingCarForm()
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
        private void PendingCarForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 画面初期化
                this.InitForm();

                // MultiRow初期化
                this.InitMultiRow();

                // 処理待ち状況一覧設定
                this.SetPendingCarList();
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            // バインドフラグ
            this.IsBind = true;

            try
            {
                // Excel出力ボタン
                this.DownloadButton.Visible = isExport;
            }
            finally
            {
                // バインドフラグ
                this.IsBind = false;
            }
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.PendingCarMultiRow;

            this.PendingCarMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new PendingCarMultiRowTemplate());

            // データーソース
            this.PendingCarMultiRow.DataSource = this.DataSource;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PendingCarForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.PendingCarMultiRow.CurrentCell = null;

            this.ActiveControl = this.SearchButton;
            this.SearchButton.Focus();
        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // 処理待ち状況一覧設定
            FormControlUtil.FormWait(this, () => this.SetPendingCarList());
        }
        #endregion

        #region 処理待ち状況一覧イベント
        /// <summary>
        /// 処理待ち状況一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PendingCarMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = ((GcMultiRow)sender).Rows[e.RowIndex];
            var dataID = (int)row.Cells["DataIDColumn"].Value;
            var historyNo = (int)row.Cells["HistoryNoColumn"].Value;

            if (dataID > 0 && historyNo > 0)
            {
                //試験車使用履歴画面表示
                new FormUtil(new TestCarHistoryForm
                {
                    TestCar = new TestCarCommonModel { データID = dataID, 履歴NO = historyNo },
                    UserAuthority = this.UserAuthority,
                    IsMonthlyInspection = false,
                    Reload = (bool isPast) => FormControlUtil.FormWait(this, () => this.SetPendingCarList())
                })
                .SingleFormShow(this);
            }
        }
        #endregion

        #region 処理待ち状況一覧設定
        /// <summary>
        /// 処理待ち状況一覧設定
        /// </summary>
        private void SetPendingCarList()
        {
            // 前画面よりでデータを引き継いでいる場合
            if (this.TempDataList != null && this.TempDataList.Any())
            {
                // 処理待ち状況データの引継
                this.DataList = this.TempDataList;
                this.TempDataList = null;
            }
            else
            {
                // 処理待ち状況一覧データの取得
                this.DataList = this.GetPendingCarList();
            }

            // データバインド
            this.CustomTemplate.SetDataSource(this.DataList, this.DataSource);

            // 一覧を未選択状態に設定
            this.PendingCarMultiRow.CurrentCell = null;

            // 一覧データラベル設定
            this.ListDataLabel.Text = this.DataList.Any() == true ? "処理待ち車両があります。" : Resources.KKM00005;
        }
        #endregion

        #region WebAPI
        /// <summary>
        /// 処理待ち状況の取得
        /// </summary>
        /// <returns></returns>
        private List<PendingCarModel> GetPendingCarList()
        {
            // APIで取得
            var res = HttpUtil.GetResponse<PendingCarSearchModel, PendingCarModel>
                (ControllerType.PendingCar, new PendingCarSearchModel { PERSONEL_ID = SessionDto.UserId, IPPAN_FLAG = Properties.Settings.Default.MyPendingCar });

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return new List<PendingCarModel>();
            }

            return res.Results.ToList();
        }

        /// <summary>
        /// 試験車情報の取得
        /// </summary>
        /// <param name="dataID"></param>
        /// <param name="historyNo"></param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCar(int dataID, int historyNo)
        {
            //APIで取得
            var res = HttpUtil.PostResponse<TestCarCommonModel>
                (ControllerType.TestCarHistory, new TestCarCommonSearchModel { データID = dataID, 履歴NO = historyNo });

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results.FirstOrDefault();
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            var util = new MultiRowUtil(this.PendingCarMultiRow);
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        #region 派生イベント
        /// <summary>
        /// 処理待ち状況確認を自部署のみ表示するメニュークリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        override protected void AppConfigMyPendingCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 基底イベント
            base.AppConfigMyPendingCarToolStripMenuItem_Click(sender, e);

            // 処理待ち状況一覧設定
            FormControlUtil.FormWait(this, () => this.SetPendingCarList());
        }
        #endregion
    }
}