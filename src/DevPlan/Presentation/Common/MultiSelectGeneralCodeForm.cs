using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 開発符号検索(複数選択)
    /// </summary>
    public partial class MultiSelectGeneralCodeForm : BaseSubForm
    {
        #region メンバ
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        #region 画面プロパティの設定
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "開発符号検索(複数選択)"; } }
        #endregion

        #region 外部フォームとやり取りするためのプロパティ
        /// <summary>
        /// 開発符号
        /// </summary>
        public List<string> GeneralCodes { get; set; }

        /// <summary>
        /// 大文字､小文字､全角､半角を区別して検索(0：区別する、1：区別しない）
        /// </summary>
        public int DIFF_DATA { get; set; }

        /// <summary>
        /// 選択した開発符号モデルリスト
        /// </summary>
        public List<GeneralCodeSearchOutModel> GModels { get; set; }

        /// <summary>機能ID</summary>
        public FunctionID FunctionId { get; set; }

        /// <summary>全選択フラグ</summary>
        public bool IsSelectedAll { get; set; } = false;

        /// <summary>全選択フラグ(他条件)</summary>
        public bool IsOtherSelectedAll { get; set; } = false;
        #endregion

        #region 内部利用
        /// <summary>
        /// 開発符号リスト
        /// </summary>
        private List<GeneralCodeSearchOutModel> DataList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiSelectGeneralCodeForm()
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
            this.CustomTemplate = new CustomTemplate() { MultiRow = GeneralCodeListMultiRow };

            this.GeneralCodeListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new MultiSelectGeneralCodeMultiRowTemplate());

            // 選択列のフィルターアイテム設定
            var cellCheck = this.GeneralCodeListMultiRow.Template.ColumnHeaders[0].Cells["CheckHeaderCell"] as ColumnHeaderCell;
            cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
            cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            base.ListFormTitleLabel.Text = this.FormTitle;

            this.SetGridData();

            this.ActiveControl = GeneralCodeListMultiRow; 
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

            if (GeneralCodes != null && GeneralCodes.Count > 0)
            {
                // 前回選択情報を反映
                GeneralCodeListMultiRow.Rows
                    .Where((x) => GeneralCodes.Contains(Convert.ToString(x.Cells["GeneralCodeColumn"].Value)))
                    .ToList()
                    .ForEach((x) => x.Cells["CheckBoxColumn"].Value = true);

                // チェックボックスONの項目を上部に表示する
                GeneralCodeListMultiRow.Sort("CheckBoxColumn", SortOrder.Descending);
            }

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
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // 大文字､小文字､全角､半角を区別?
                DIFF_DATA = this.DIFF_DATA,

                // 開発フラグ(0:開発外 1:開発中 省略時は条件に含めない)
                UNDER_DEVELOPMENT = "1"
            };

            // Get実行
            var res = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, itemCond);
            if (res.ErrorCode == ApiMessageType.KKE03002.ToString())
            {
                Messenger.Warn(Resources.KKM00005);
            }

            return (res.Results).ToList();
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

        #region 登録ボタン押下
        /// <summary>
        /// 登録ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            GeneralCodes = new List<string>();
            GModels = new List<GeneralCodeSearchOutModel>();

            for (int i = 0; i < GeneralCodeListMultiRow.RowCount; i++)
            {
                if (GeneralCodeListMultiRow.Rows[i].Cells["GeneralCodeColumn"].Value == null)
                {
                    continue;
                }

                if (Convert.ToBoolean(GeneralCodeListMultiRow.Rows[i].Cells["CheckBoxColumn"].Value) == true)
                {
                    var target = GeneralCodeListMultiRow.Rows[i].Cells["GeneralCodeColumn"].Value.ToString();
                    GeneralCodes.Add(target);
                    GModels.Add(DataList.Single((x) => x.GENERAL_CODE == target));
                }
            }

            // 全検索フラグのセット
            IsSelectedAll = this.GeneralCodes.Count == GeneralCodeListMultiRow.RowCount ? true : false;

            var message = string.Empty;

            if (20 < this.GeneralCodes.Count && !IsSelectedAll)
            {
                //DBの文字数制限
                if (FunctionId == FunctionID.CAP)
                {
                    message = string.Format(Resources.KKM03025, "車種", Const.CrLf + "20件以下で選択してください。");
                }
                else
                {
                    message = string.Format(Resources.KKM03025, "開発符号", "");
                }
            }
            else if (20 < this.GeneralCodes.Count && IsSelectedAll && IsOtherSelectedAll)
            {
                // 複合条件
                if (FunctionId == FunctionID.CAP)
                {
                    message = string.Format(Resources.KKM03025, "車種", Const.CrLf + "20件以下で選択してください。");
                }
            }

            // エラーメッセージの表示
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messenger.Warn(message);

                GeneralCodes = new List<string>();
                GModels = new List<GeneralCodeSearchOutModel>();

                return;
            }

            //画面を閉じる
            this.FormOkClose();
        }
        #endregion

        #region 表示全選択チェックボックス操作
        /// <summary>
        /// 表示全選択チェックボックス操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // カレント退避
            var cur = GeneralCodeListMultiRow.CurrentCell;

            foreach (var row in GeneralCodeListMultiRow.Rows)
            {
                // 非表示行は未処理
                if (!row.Visible) continue;

                row.Cells[0].Value = VisibleCheckBox.Checked;
            }

            GeneralCodeListMultiRow.CurrentCell = cur;
        }
        #endregion

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeListMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            // チェックボックスをOn/Off
            var checkCell = ((CheckBoxCell)((GcMultiRow)sender).Rows[e.RowIndex].Cells["CheckBoxColumn"]);
            checkCell.Value = !Convert.ToBoolean(checkCell.Value);
        }
        #endregion
    }
}
