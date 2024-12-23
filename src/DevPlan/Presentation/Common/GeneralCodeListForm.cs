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
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 開発符号検索
    /// </summary>
    public partial class GeneralCodeListForm : BaseSubForm
    {
        #region メンバ
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        #region <<< 画面プロパティの設定 >>>
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "開発符号検索"; } }
        #endregion

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 開発フラグ(0:開発外 1:開発中)
        /// </summary>
        public string UNDER_DEVELOPMENT { get; set; }

        /// <summary>
        /// 利用許可フラグ(0:許可なし 1:許可あり)
        /// </summary>
        public short PERMIT_FLG { get; set; }

        /// <summary>
        /// 機能区分
        /// </summary>
        /// <remarks>
        /// 00:デフォルト
        /// 01:業務計画表
        /// 02:月次計画表
        /// 03:試験車日程
        /// 04:カーシェア日程
        /// 05:外製車日程
        /// 06:進捗履歴
        /// 07:作業履歴(試験車)
        /// 08:作業履歴(外製車)
        /// 09:作業履歴(カーシェア車)
        /// </remarks>
        public string FUNCTION_CLASS { get; set; } = "00";

        /// <summary>
        /// 大文字､小文字､全角､半角を区別して検索(0：区別する、1：区別しない）
        /// </summary>
        public int DIFF_DATA { get; set; }
        #endregion

        #region <<< 内部利用 >>>
        /// <summary>
        /// 開発符号リスト
        /// </summary>
        private List<GeneralCodeSearchOutModel> DataList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GeneralCodeListForm()
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
        private void GeneralCodeListForm_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            this.CustomTemplate = new CustomTemplate() { MultiRow = this.GeneralCodeListMultiRow, RowCountLabel = this.RowCountLabel };

            this.GeneralCodeListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new GeneralCodeListMultiRowTemplate());

            this.GeneralCodeListMultiRow.CellClick += this.GeneralCodeListMultiRow_CellClick;

            base.ListFormTitleLabel.Text = this.FormTitle;

            this.SetSearchCondition();

            this.SetGridData();

        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.GeneralCodeListMultiRow.CurrentCell = null;

        }
        #endregion

        #region 検索条件のセット
        /// <summary>
        /// 検索条件のセット
        /// </summary>
        private void SetSearchCondition()
        {
            #region <<< パラメータを受け取った場合は初期値に設定 >>
            //初期表示フォーカス
            this.ActiveControl = GeneralCodeTextBox;
            // 車系
            this.CarGroupTextBox.Text = this.CAR_GROUP;
            // 開発符号
            this.GeneralCodeTextBox.Text = this.GENERAL_CODE;
            #endregion
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            // データの取得
            this.DataList = GetGridDataList();

            // データバインド
            this.CustomTemplate.SetDataSource(this.DataList);

            // 検索結果文言
            this.SearchResultLabel.Text = (DataList == null || DataList.Any() == false) ? Resources.KKM00005 : string.Empty;

            //一覧を未選択状態に設定
            this.GeneralCodeListMultiRow.CurrentCell = null;

        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<GeneralCodeSearchOutModel> GetGridDataList()
        {
            // APIに渡すパラメータ設定
            var itemCond = new GeneralCodeSearchInModel
            {
                // 車系
                CAR_GROUP = this.CarGroupTextBox.Text,

                // 開発符号
                GENERAL_CODE = this.GeneralCodeTextBox.Text,

                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // 開発フラグ
                UNDER_DEVELOPMENT = this.UNDER_DEVELOPMENT,

                // 機能区分
                FUNCTION_CLASS = this.FUNCTION_CLASS,

                // 大文字､小文字､全角､半角を区別?
                DIFF_DATA = this.DIFF_DATA,

            };

            // Get実行
            var list = new List<GeneralCodeSearchOutModel>();
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, itemCond);
            if (res!=null && res.Status == Const.StatusSuccess)
            {
                list = (res.Results).ToList();
            }

            return list;
        }
        #endregion

        #region 検索ボタンのクリック
        /// <summary>
        /// 検索ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSearchButton_Click(object sender, EventArgs e)
        {
            this.SetGridData();

        }
        #endregion

        #region グリッドのセルクリック
        /// <summary>
        /// グリッドのセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = this.GeneralCodeListMultiRow;

                this.CAR_GROUP = (string)grid.CurrentRow.Cells["CarGroupDataGridViewTextBoxColumn"].Value;
                this.GENERAL_CODE = (string)grid.CurrentRow.Cells["GeneralCodeDataGridViewTextBoxColumn"].Value;
                this.PERMIT_FLG = (short)this.DataList.FirstOrDefault(x => x.GENERAL_CODE == this.GENERAL_CODE)?.PERMIT_FLG;

                base.FormOkClose();
            }
        }
        #endregion
    }
}
