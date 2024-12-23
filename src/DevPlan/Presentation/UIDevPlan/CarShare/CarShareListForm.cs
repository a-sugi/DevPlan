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

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア一覧画面
    /// </summary>
    public partial class CarShareListForm : BaseSubDialogForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return this.IsNaisei ? "カーシェア内製車一覧" : "カーシェア外製車一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>カーシェアリスト</summary>
        private List<CarShareInnerSearchOutModel> CarShareList { get; set; }

        /// <summary>選択した車</summary>
        public CarShareInnerSearchOutModel SelectedCarShare { get; private set; }

        /// <summary>項目名</summary>
        public string ItemName { get; private set; }

        /// <summary>内製車フラグ</summary>
        public bool IsNaisei { get; set; } = false;

        /// <summary>検索条件</summary>
        public CarShareInnerSearchInModel SearchCondition { get; set; } = new CarShareInnerSearchInModel();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarShareListForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            //一覧初期化
            this.InitList();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            return;
        }

        /// <summary>
        /// 一覧初期化
        /// </summary>
        private void InitList()
        {
            var customTempate = new CustomTemplate() { MultiRow = this.CarShareListMultiRow, RowCountLabel = this.RowCountLabel };

            this.CarShareListMultiRow.Template = customTempate
                .SetContextMenuTemplate(IsNaisei ? new CarShareListInnerMultiRowTemplate() as Template: new CarShareListOuterMultiRowTemplate() as Template);

            this.CarShareListMultiRow.DoubleClick += CarShareListMultiRow_DoubleClick;

            //カーシェア車一覧
            this.CarShareList = new List<CarShareInnerSearchOutModel>();

            var controller = IsNaisei ? ControllerType.CarShareInner : ControllerType.CarShareOuter;

            //APIで取得
            var res = HttpUtil.GetResponse<CarShareInnerSearchInModel, CarShareInnerSearchOutModel>(controller, SearchCondition);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //カーシェア一覧に設定
                this.CarShareList.AddRange(res.Results);
                customTempate.SetDataSource(this.CarShareList);
            }

            //一覧が取得できたかどうか
            if (this.CarShareList.Any() == false)
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
            this.CarShareListMultiRow.CurrentCell = null;

        }
        #endregion

        #region 一覧ダブルクリック
        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareListMultiRow_DoubleClick(object sender, EventArgs e)
        {
            //グリッドを基準としたカーソル位置のポイントを取得
            var p = this.CarShareListMultiRow.PointToClient(Cursor.Position);

            //取得したポイントからHitTestでセル位置取得
            var ht = this.CarShareListMultiRow.HitTest(p.X, p.Y);

            //ダブルクリックした箇所ごとの分岐
            switch (ht.Type)
            {
                //行ヘッダー、セル
                case GrapeCity.Win.MultiRow.HitTestType.Row:
                    //車選択
                    this.SelectCar();
                    break;

            }

        }
        #endregion

        #region 一覧クリック
        /// <summary>
        /// 一覧クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareListMultiRow_Click(object sender, EventArgs e)
        {
            if (((GcMultiRow)sender).CurrentCell != null && ((GcMultiRow)sender).CurrentCell is FilteringTextBoxCell)
            {
                ((GcMultiRow)sender).BeginEdit(true);
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
            //車選択
            this.SelectCar();

        }
        #endregion

        #region 試験車選択
        /// <summary>
        /// 試験車選択
        /// </summary>
        private void SelectCar()
        {
            //一覧を選択しているかどうか
            if (this.CarShareListMultiRow.CurrentCell == null || this.CarShareListMultiRow.CurrentCell is FilteringTextBoxCell)
            {
                Messenger.Warn(string.Format(Resources.KKM00009));
                return;

            }

            var kanriNo = this.CarShareListMultiRow.SelectedRows[0].Cells["ManagementNoColumn"].Value?.ToString();

            //選択した車
            var car = this.SelectedCarShare = this.CarShareList.First(x => x.管理票NO == kanriNo);

            var sb = new StringBuilder();

            Func<int?, string> get = s => s == 1 ? "あり" : "なし";

            //カーシェア内製車かどうか
            if (this.IsNaisei)
            {
                //項目名の組み立て(1行目)
                sb.AppendFormat("{0} {1}({2})", car.車系, car.開発符号, car.登録ナンバー).AppendLine();

            }
            else
            {
                //項目名の組み立て(1行目)
                sb.AppendFormat("{0} ({1})", car.外製車名, car.登録ナンバー).AppendLine();

            }

            //項目名の組み立て(2行目)
            sb.AppendFormat("駐車場 {0}", car.駐車場番号).AppendLine();

            //項目名の組み立て(3行目)
            sb.AppendFormat("ETC{0}、ナビ {1}", get(car.FLAG_ETC付), get(car.FLAG_ナビ付));

            //項目名
            this.ItemName = sb.ToString().Trim();

            //フォームクローズ(OK)
            base.FormOkClose();

        }
        #endregion
    }
}
