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
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.TestCarSchedule
{
    /// <summary>
    /// 試験車一覧画面
    /// </summary>
    public partial class TestCarListForm : BaseSubDialogForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>試験車リスト</summary>
        private List<TestCarSearchOutModel> TestCarList { get; set; }

        /// <summary>選択した試験車</summary>
        public TestCarSearchOutModel SelectedTestCar { get; set; }

        /// <summary>項目名</summary>
        public string ItemName { get; set; }

        //Append Start 2021/10/28 矢作

        /// <summary>駐車場検索画面からかどうか</summary>
        public bool fromParking { get; set; }

        //Append End 2021/10/28 矢作

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestCarListForm()
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
        private void TestCarListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void InitForm()
        {
            var customTemplate = new CustomTemplate() { MultiRow = this.ListDataMultiRow, RowCountLabel = this.RowCountLabel };

            this.ListDataMultiRow.Template = customTemplate.SetContextMenuTemplate(this.ListDataMultiRow.Template);

            this.ListDataMultiRow.DoubleClick += ListDataMultiRow_DoubleClick;

            //試験車一覧
            this.TestCarList = new List<TestCarSearchOutModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<TestCarSearchOutModel>(ControllerType.TestCar);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //試験車一覧に設定
                this.TestCarList.AddRange(res.Results);
                customTemplate.SetDataSource(this.TestCarList);
            }

            //一覧が取得できたかどうか
            if (this.TestCarList.Any() == false)
            {
                this.ListDataLabel.Text = Resources.KKM00005;
            }
        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListDataMultiRow.CurrentCell = null;
        }
        #endregion

        #region 一覧ダブルクリック
        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListDataMultiRow_DoubleClick(object sender, EventArgs e)
        {
            //グリッドを基準としたカーソル位置のポイントを取得
            var p = this.ListDataMultiRow.PointToClient(Cursor.Position);

            //取得したポイントからHitTestでセル位置取得
            var ht = this.ListDataMultiRow.HitTest(p.X, p.Y);

            //ダブルクリックした箇所ごとの分岐
            switch (ht.Type)
            {
                case HitTestType.Row:
                    //試験車選択
                    this.SelectTestCar();
                    break;
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
            //Update Start 2021/10/28 矢作
            ////試験車選択
            //this.SelectTestCar();

            //駐車場検索画面からかどうかで処理を分岐
            if (fromParking == true)
            {
                DialogResult dialogResult;
                var kanriNo = this.ListDataMultiRow.SelectedRows[0].Cells["ManagementNoColumn"].Value?.ToString();

                //選択した試験車
                var testCar = this.SelectedTestCar = this.TestCarList.First(x => x.管理票NO == kanriNo);
                if (string.IsNullOrEmpty(testCar.駐車場番号))
                {
                    dialogResult = Messenger.Confirm("登録しますか？");
                }
                else
                {
                    dialogResult = Messenger.Confirm("本車両はすでに駐車場NOが登録済です。上書きしますか？");
                }
                if (dialogResult == DialogResult.Yes)
                {
                    //試験車選択
                    this.SelectTestCar();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                }
            }
            else
            {
                //試験車選択
                this.SelectTestCar();
            }

            //Update End 2021/10/28 矢作
        }
        #endregion

        #region 試験車選択
        /// <summary>
        /// 試験車選択
        /// </summary>
        private void SelectTestCar()
        {
            //一覧を選択しているかどうか
            if (this.ListDataMultiRow.CurrentCell == null || this.ListDataMultiRow.CurrentCell is FilteringTextBoxCell)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
 
            }

            var kanriNo = this.ListDataMultiRow.SelectedRows[0].Cells["ManagementNoColumn"].Value?.ToString();

            //選択した試験車
            var testCar = this.SelectedTestCar = this.TestCarList.First(x => x.管理票NO == kanriNo);

            var sb = new StringBuilder();

            Func<short?, string> get = s => s == 1 ? "あり" : "なし";

            //項目名の組み立て(1行目)
            sb.AppendFormat("{0} {1}", testCar.車系, testCar.開発符号).AppendLine();

            //項目名の組み立て(2行目)
            sb.AppendFormat("駐車場 {0}", testCar.駐車場番号).AppendLine();

            //項目名の組み立て(3行目)
            sb.AppendFormat("ETC{0}、ナビ {1}", get(testCar.FLAG_ETC付), get(testCar.FLAG_ナビ付));

            //項目名
            this.ItemName = sb.ToString().Trim();

            //フォームクローズ(OK)
            base.FormOkClose();

        }
        #endregion
    }
}
