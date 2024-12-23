using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;

using DevPlan.Presentation.Base;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System.Drawing;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon;
using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 課検索(複数選択)
    /// </summary>
    public partial class ExceptMonthlyInspectionForm : BaseSubForm
    {
        #region メンバ変数
        private CustomTemplate ExceptMonthlyInspectionTemplate = new CustomTemplate();
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "使用履歴設定"; } }

        /// <summary>機能ID</summary>
        public FunctionID FunctionId { get; set; }

        /// <summary>全選択フラグ</summary>
        public bool IsSelectedAll { get; set; } = false;

        /// <summary>全選択フラグ(他条件)</summary>
        public bool IsOtherSelectedAll { get; set; } = false;

        /// <summary>試験車情報</summary>
        public List<TestCarCommonModel> TestCarList { get; set; }

        /// <summary>課名（カンマ区切り）</summary>
        public string[] SectionsGroups { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        public List<ExceptMonthlyInspectionOutModels> DefList { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExceptMonthlyInspectionForm()
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
        private void ProgressListSectionForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }
        
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            base.Text = this.FormTitle;
            base.ListFormTitleLabel.Text = this.FormTitle;

            // MultiRowの初期化
            this.InitMultiRow();

            //グリッド表示
            this.SetGridData();

            //フォーカス
            this.ActiveControl = SectionListMultiRow;
        }

        /// <summary>
        /// MultiRowの初期化
        /// </summary>
        private void InitMultiRow()
        {
            // プロパティ
            this.ExceptMonthlyInspectionTemplate.MultiRow = this.SectionListMultiRow;

            // テンプレート
            this.SectionListMultiRow.Template = this.ExceptMonthlyInspectionTemplate.SetContextMenuTemplate(new ExceptMonthlyInspectionMultiRowTemplate());

            // 選択列のフィルターアイテム設定
            var cellCheck = this.SectionListMultiRow.Template.ColumnHeaders[0].Cells["CheckHeaderCell"] as ColumnHeaderCell;
            cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
            cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // 各種設定
            this.SectionListMultiRow.MouseWheelCount = new GridAppConfigAccessor().GetGridMouseWheelCount();
            this.SectionListMultiRow.VerticalScrollCount = this.SectionListMultiRow.MouseWheelCount;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressListSectionForm_Shown(object sender, EventArgs e)
        {
            this.SectionListMultiRow.CurrentCell = null;
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            //グリッドデータの取得
            var list = GetGridDataList();

            this.DefList = list;

            // データバインド
            this.ExceptMonthlyInspectionTemplate.SetDataSource(list);

            int index = 0;
            foreach (var row in this.SectionListMultiRow.Rows)
            {
                if (!row.Visible) continue;
                
                row.Cells["CheckBoxColumn"].Value = list[index].FLAG_月例点検;

                index++;
            }

            //一覧を未選択状態に設定
            this.SectionListMultiRow.CurrentCell = null;
        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<ExceptMonthlyInspectionOutModels> GetGridDataList()
        {

            // Get実行
            var res = HttpUtil.GetResponse<ExceptMonthlyInspectionOutModels, ExceptMonthlyInspectionOutModels>(ControllerType.ExceptMonthlyInspection, null);

            return (res.Results).ToList();
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
            var message = string.Empty;

            var list = new List<ExceptMonthlyInspectionUpdateModels>();

            for (int i = 0; i < SectionListMultiRow.RowCount; i++)
            {
                if (SectionListMultiRow.Rows[i].Cells["SectionColumn"].Value == null)
                {
                    continue;
                }

                var sectionData = this.DefList.First(x => x.SECTION_ID == SectionListMultiRow.Rows[i].Cells["SectionIDColumn"].Value.ToString());

                if(Convert.ToBoolean(SectionListMultiRow.Rows[i].Cells["CheckBoxColumn"].Value) != Convert.ToBoolean(sectionData.FLAG_月例点検))
                {
                    var item = Convert.ToBoolean(SectionListMultiRow.Rows[i].Cells["CheckBoxColumn"].Value);
                    var flg = Convert.ToInt32(item);
                    list.Add(new ExceptMonthlyInspectionUpdateModels() { SECTION_ID = SectionListMultiRow.Rows[i].Cells["SectionIDColumn"].Value.ToString(), FLAG_月例点検 = flg });
                    var index = DefList.FindIndex(x => x.SECTION_ID == SectionListMultiRow.Rows[i].Cells["SectionIDColumn"].Value.ToString());
                    DefList[index].FLAG_月例点検 = flg;
                }
            }

            //登録処理
            if (PutData(list))
            {
                //正常終了メッセージ
                Messenger.Info(Resources.KKM00002); // 登録完了

                //画面を閉じる
                //this.FormOkClose();
            }
        }
        #endregion
        
        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionListMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            // チェックボックスをOn/Off
            var checkCell = ((CheckBoxCell)((GcMultiRow)sender).Rows[e.RowIndex].Cells["CheckBoxColumn"]);
            var item = Convert.ToBoolean(checkCell.Value);
            checkCell.Value = item ? 1 : 0;
        }
        #endregion

        #region データ処理
        /// <summary>
        /// データ更新処理
        /// </summary>
        private bool PutData(List<ExceptMonthlyInspectionUpdateModels> list)
        {
            var res = HttpUtil.PostResponse<ExceptMonthlyInspectionUpdateModels>(ControllerType.ExceptMonthlyInspection, list);
            return res != null && res.Status == Const.StatusSuccess;
        }
        #endregion
    }
}
