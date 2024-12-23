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
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 設計チェック指摘履歴
    /// </summary>
    public partial class DesignCheckPointHistoryForm : BaseSubDialogForm
    {
        #region 内部変数

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate CustomTemplate = new CustomTemplate();

        #region 列情報
        /// <summary>
        /// 列情報
        /// </summary>
        private List<ColInfo> ColInfos = new List<ColInfo>()
            {
                // 指摘No
                new ColInfo() { Index = 0, RowCellName = "NoTextBoxColumn", HiddenCellName = "HiddenNo" , Visible = true, HeaderName = "ch_No" },
                
                //Append Start 2021/07/28 杉浦 設計チェックインポート
                // 試作管理No
                new ColInfo() { Index = 0, RowCellName = "MgrNoTextBoxColumn", HiddenCellName = "HiddenMgrNo" , Visible = true, HeaderName = "ch_MgrNo" },
                //Append End 2021/07/28 杉浦 設計チェックインポート
                
                // ステータス
                new ColInfo() { Index = 1, RowCellName = "StatusTextBoxColumn", HiddenCellName = "HiddenStatus" , Visible = true, HeaderName = "ch_Status" },
                
                //Update Start 2021/07/28 杉浦 設計チェックインポート
                //// 指摘部品
                //new ColInfo() { Index = 2, RowCellName = "PartsTextBoxColumn", HiddenCellName = "HiddenParts" , Visible = true, HeaderName = "ch_Parts" },

                //// 状況
                //new ColInfo() { Index = 3, RowCellName = "SituationTextBoxColumn", HiddenCellName = "HiddenSituation" , Visible = true, HeaderName = "ch_Situation" },

                //// 処置しないチェック
                //new ColInfo() { Index = 4, RowCellName = "TreatmentCheckBoxColumn", HiddenCellName = "HiddenTreatment" , Visible = true, HeaderName = "ch_Treatment" },

                //// [処置内容]どこの部署が?
                //new ColInfo() { Index = 5, RowCellName = "TreatmentSectionTextBoxColumn", HiddenCellName = "HiddenTreatmentSection" , Visible = true, HeaderName = "ch_TreatmentSection" },

                //// [処置内容]何を
                //new ColInfo() { Index = 6, RowCellName = "TreatmentTargetTextBoxColumn", HiddenCellName = "HiddenTreatmentTarget" , Visible = true, HeaderName = "ch_TreatmentTarget" },

                //// [処置内容]どのように
                //new ColInfo() { Index = 7, RowCellName = "TreatmentHowTextBoxColumn", HiddenCellName = "HiddenTreatmentHow" , Visible = true, HeaderName = "ch_TreatmentHow" },

                //// [処置内容]調整:済
                //new ColInfo() { Index = 8, RowCellName = "TreatmentOKCheckBoxColumn", HiddenCellName = "HiddenTreatmentOK" , Visible = true, HeaderName = "ch_TreatmentOK" },

                //// [処置内容]誰と
                //new ColInfo() { Index = 9, RowCellName = "TreatmentWhoTextBoxColumn", HiddenCellName = "HiddenTreatmentWho" , Visible = true, HeaderName = "ch_TreatmentWho" },

                //// [処置内容]いつまでに
                //new ColInfo() { Index = 10, RowCellName = "TreatmentWhenCalendarColumn", HiddenCellName = "HiddenTreatmentWhen" , Visible = true, HeaderName = "ch_TreatmentWhen" },

                //// 要試作改修
                //new ColInfo() { Index = 11, RowCellName = "RepairCheckBoxColumn", HiddenCellName = "HiddenRepair" , Visible = true, HeaderName = "ch_Repair" },

                //// 部品納入日
                //new ColInfo() { Index = 12, RowCellName = "PartsGetDayCalendarColumn", HiddenCellName = "HiddenPartsGetDay" , Visible = true, HeaderName = "ch_PartsGetDay" },

                //// 完了確認日
                //new ColInfo() { Index = 13, RowCellName = "EndDayCalendarColumn", HiddenCellName = "" , Visible = true, IsNonFilter = true, HeaderName = "ch_EndDay" },

                //// 担当課長承認
                //new ColInfo() { Index = 14, RowCellName = "ApprovalOKCheckBoxColumn", HiddenCellName = "HiddenApprovalOK" , Visible = true, HeaderName = "ch_ApprovalOK" },

                //// 試験車名
                //new ColInfo() { Index = 15, RowCellName = "TestCarNameTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_TestCarName" },

                //// 設計チェック時状況
                //new ColInfo() { Index = 16, RowCellName = "ProgressTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_Progress" },

                //// 担当課
                //new ColInfo() { Index = 17, RowCellName = "SectionTextBoxColumn", HiddenCellName = "HiddenSection" , Visible = true, HeaderName = "ch_Section" },

                //// 担当者
                //new ColInfo() { Index = 18, RowCellName = "UserTextBoxColumn", HiddenCellName = "HiddenUser" , Visible = true, HeaderName = "ch_User" },

                //// 担当者TEL
                //new ColInfo() { Index = 19, RowCellName = "UserTelTextBoxColumn", HiddenCellName = "HiddenUserTel" , Visible = true, HeaderName = "ch_UserTel" },

                //// 編集日
                //new ColInfo() { Index = 20, RowCellName = "EditDateTextColumn", HiddenCellName = "HiddenEditDate" , Visible = true, HeaderName = "ch_EditDate" },

                //// 編集者
                //new ColInfo() { Index = 21, RowCellName = "EditUserNameTextBoxColumn", HiddenCellName = "HiddenEditUserName" , Visible = true, HeaderName = "ch_EditUserName" },

                //// 以下非表示列

                //// 開催日_ID
                //new ColInfo() { Index = 22, RowCellName = "OpenDayIDColumn", HiddenCellName = "" , Visible = false },

                //// 担当者_ID
                //new ColInfo() { Index = 23, RowCellName = "UserIDTextBoxColumn", HiddenCellName = "" , Visible = false },

                //// 履歴数
                //new ColInfo() { Index = 24, RowCellName = "HistoryCountColumn", HiddenCellName = "" , Visible = false },

                //// 指摘ID
                //new ColInfo() { Index = 25, RowCellName = "IDTextBoxColumn", HiddenCellName = "" , Visible = false },

                //// 対象車両ID
                //new ColInfo() { Index = 26, RowCellName = "CarIDTextBoxColumn", HiddenCellName = "" , Visible = false },

                // ステータス
                new ColInfo() { Index = 2, RowCellName = "MgrStatusTextBoxColumn", HiddenCellName = "HiddenMgrStatus" , Visible = true, HeaderName = "ch_MgrStatus" },
                
                // 指摘部品
                new ColInfo() { Index = 3, RowCellName = "PartsTextBoxColumn", HiddenCellName = "HiddenParts" , Visible = true, HeaderName = "ch_Parts" },

                // 状況
                new ColInfo() { Index = 4, RowCellName = "SituationTextBoxColumn", HiddenCellName = "HiddenSituation" , Visible = true, HeaderName = "ch_Situation" },

                // 処置しないチェック
                new ColInfo() { Index = 5, RowCellName = "TreatmentCheckBoxColumn", HiddenCellName = "HiddenTreatment" , Visible = true, HeaderName = "ch_Treatment" },

                // [処置内容]どこの部署が?
                new ColInfo() { Index = 6, RowCellName = "TreatmentSectionTextBoxColumn", HiddenCellName = "HiddenTreatmentSection" , Visible = true, HeaderName = "ch_TreatmentSection" },

                // [処置内容]何を
                new ColInfo() { Index = 7, RowCellName = "TreatmentTargetTextBoxColumn", HiddenCellName = "HiddenTreatmentTarget" , Visible = true, HeaderName = "ch_TreatmentTarget" },

                // [処置内容]どのように
                new ColInfo() { Index = 8, RowCellName = "TreatmentHowTextBoxColumn", HiddenCellName = "HiddenTreatmentHow" , Visible = true, HeaderName = "ch_TreatmentHow" },

                // [処置内容]調整:済
                new ColInfo() { Index = 9, RowCellName = "TreatmentOKCheckBoxColumn", HiddenCellName = "HiddenTreatmentOK" , Visible = true, HeaderName = "ch_TreatmentOK" },

                // [処置内容]誰と
                new ColInfo() { Index = 10, RowCellName = "TreatmentWhoTextBoxColumn", HiddenCellName = "HiddenTreatmentWho" , Visible = true, HeaderName = "ch_TreatmentWho" },

                // [処置内容]いつまでに
                new ColInfo() { Index = 11, RowCellName = "TreatmentWhenCalendarColumn", HiddenCellName = "HiddenTreatmentWhen" , Visible = true, HeaderName = "ch_TreatmentWhen" },

                // 要試作改修
                new ColInfo() { Index = 12, RowCellName = "RepairCheckBoxColumn", HiddenCellName = "HiddenRepair" , Visible = true, HeaderName = "ch_Repair" },

                // 部品納入日
                new ColInfo() { Index = 13, RowCellName = "PartsGetDayCalendarColumn", HiddenCellName = "HiddenPartsGetDay" , Visible = true, HeaderName = "ch_PartsGetDay" },

                // 完了確認日
                new ColInfo() { Index = 14, RowCellName = "EndDayCalendarColumn", HiddenCellName = "" , Visible = true, IsNonFilter = true, HeaderName = "ch_EndDay" },

                // 担当課長承認
                new ColInfo() { Index = 15, RowCellName = "ApprovalOKCheckBoxColumn", HiddenCellName = "HiddenApprovalOK" , Visible = true, HeaderName = "ch_ApprovalOK" },

                // 試験車名
                new ColInfo() { Index = 16, RowCellName = "TestCarNameTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_TestCarName" },

                // 設計チェック時状況
                new ColInfo() { Index = 17, RowCellName = "ProgressTextBoxColumn", HiddenCellName = "", Visible = true, IsNonFilter = true, HeaderName = "ch_Progress" },

                // 担当課
                new ColInfo() { Index = 18, RowCellName = "SectionTextBoxColumn", HiddenCellName = "HiddenSection" , Visible = true, HeaderName = "ch_Section" },

                // 担当者
                new ColInfo() { Index = 19, RowCellName = "UserTextBoxColumn", HiddenCellName = "HiddenUser" , Visible = true, HeaderName = "ch_User" },

                // 担当者TEL
                new ColInfo() { Index = 20, RowCellName = "UserTelTextBoxColumn", HiddenCellName = "HiddenUserTel" , Visible = true, HeaderName = "ch_UserTel" },

                // 編集日
                new ColInfo() { Index = 21, RowCellName = "EditDateTextColumn", HiddenCellName = "HiddenEditDate" , Visible = true, HeaderName = "ch_EditDate" },

                // 編集者
                new ColInfo() { Index = 22, RowCellName = "EditUserNameTextBoxColumn", HiddenCellName = "HiddenEditUserName" , Visible = true, HeaderName = "ch_EditUserName" },

                // 以下非表示列

                // 開催日_ID
                new ColInfo() { Index = 23, RowCellName = "OpenDayIDColumn", HiddenCellName = "" , Visible = false },

                // 担当者_ID
                new ColInfo() { Index = 24, RowCellName = "UserIDTextBoxColumn", HiddenCellName = "" , Visible = false },

                // 履歴数
                new ColInfo() { Index = 25, RowCellName = "HistoryCountColumn", HiddenCellName = "" , Visible = false },

                // 指摘ID
                new ColInfo() { Index = 26, RowCellName = "IDTextBoxColumn", HiddenCellName = "" , Visible = false },

                // 対象車両ID
                new ColInfo() { Index = 27, RowCellName = "CarIDTextBoxColumn", HiddenCellName = "" , Visible = false },
                
                //Update End 2021/07/28 杉浦 設計チェックインポート

            };
        #endregion

        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "設計チェック指摘履歴"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; }

        /// <summary>設計チェック指摘</summary>
        public DesignCheckPointGetOutModel DesignCheckPoint { get; set; } = null;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckPointHistoryForm()
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
        private void DesignCheckPointHistoryForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //データグリッドビューの初期化
                this.InitDataGridView();

                //画面初期化
                this.InitForm();
            });
        }

        /// <summary>
        /// データグリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            Template tmp = new DesignCheckPointHistoryMulitiRowTemplate();

            // テンプレート設定
            this.CustomTemplate.ColumnHeaderHeight = 70;
            this.CustomTemplate.RowCountLabel = RowCountLabel;
            this.CustomTemplate.MultiRow = this.PointMultiRow;
            var template = this.CustomTemplate.SetContextMenuTemplate(tmp);

            // ヘッダセル
            foreach (var cell in template.ColumnHeaders[0].Cells)
            {
                // フィルタ消し
                if (this.ColInfos.Where((y) => y.IsNonFilter == true).Any((y) => y.HeaderName == cell.Name))
                {
                    var hCell = cell as ColumnHeaderCell;

                    hCell.DropDownContextMenuStrip = null;
                }

                // フィルタテキスト読み取り専用
                if (cell is FilteringTextBoxCell)
                {
                    var fCell = cell as FilteringTextBoxCell;

                    if (this.ColInfos.Where((y) => y.IsNonFilter == true).Any((y) => y.RowCellName == fCell.FilteringCellName))
                    {
                        fCell.ReadOnly = true;
                        fCell.Style.BackColor = Color.WhiteSmoke;
                    }
                }
            }

            // 隠しセル
            foreach (var info in this.ColInfos.FindAll((x) => string.IsNullOrEmpty(x.HiddenCellName) == false))
            {
                template.Row.Cells[info.HiddenCellName].Size = template.Row.Cells[info.RowCellName].Size;
                template.Row.Cells[info.HiddenCellName].Location = template.Row.Cells[info.RowCellName].Location;
                template.Row.Cells[info.HiddenCellName].Style.Font = template.Row.Cells[info.RowCellName].Style.Font;
                template.Row.Cells[info.HiddenCellName].Style.BackColor = template.Row.Cells[info.RowCellName].Style.BackColor;
                template.Row.Cells[info.HiddenCellName].Style.ForeColor = template.Row.Cells[info.RowCellName].Style.ForeColor;
            }

            // フィルターアイテム設定：処置しない
            var cellTreatment = template.ColumnHeaders[0].Cells["ch_Treatment"] as ColumnHeaderCell;
            cellTreatment.DropDownContextMenuStrip.Items.RemoveAt(cellTreatment.DropDownContextMenuStrip.Items.Count - 1);
            cellTreatment.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("処置しない", "処置する") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // フィルターアイテム設定：調整：済
            var cellTreatmentOK = template.ColumnHeaders[0].Cells["ch_TreatmentOK"] as ColumnHeaderCell;
            cellTreatmentOK.DropDownContextMenuStrip.Items.RemoveAt(cellTreatmentOK.DropDownContextMenuStrip.Items.Count - 1);
            cellTreatmentOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // フィルターアイテム設定：要試作改修
            var cellRepair = template.ColumnHeaders[0].Cells["ch_Repair"] as ColumnHeaderCell;
            cellRepair.DropDownContextMenuStrip.Items.RemoveAt(cellRepair.DropDownContextMenuStrip.Items.Count - 1);
            cellRepair.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("要", "否") { MaxCount = CustomTemplate.FilterItemMaxCount });

            // フィルターアイテム設定：担当課長承認
            var cellApprovalOK = template.ColumnHeaders[0].Cells["ch_ApprovalOK"] as ColumnHeaderCell;
            cellApprovalOK.DropDownContextMenuStrip.Items.RemoveAt(cellApprovalOK.DropDownContextMenuStrip.Items.Count - 1);
            cellApprovalOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

            Color correspondenceColor = Color.Khaki;
            Color otherColor = Color.LightGray;
            Color foreColor = Color.Black;

            // 処置内容列
            template.ColumnHeaders[0].Cells["ch_TreatmentSection"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentSection"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentTarget"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentTarget"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentHow"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentHow"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentOK"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentOK"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentWho"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentWho"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Style.BackColor = otherColor;
            template.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Style.ForeColor = foreColor;

            // 試験車列
            template.ColumnHeaders[0].Cells["ch_TestCarName"].Style.BackColor = correspondenceColor;
            template.ColumnHeaders[0].Cells["ch_TestCarName"].Style.ForeColor = foreColor;
            template.ColumnHeaders[0].Cells["ch_Progress"].Style.BackColor = correspondenceColor;
            template.ColumnHeaders[0].Cells["ch_Progress"].Style.ForeColor = foreColor;

            // テンプレートセット
            this.PointMultiRow.Template = template;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            // 開催日
            this.OpenDayLabel.Text = DateTimeUtil.ConvertDateString(this.DesignCheck?.開催日);

            // 設計チェック名
            this.DesignCheckNameLabel.Text = this.DesignCheck?.名称;
            this.DesignCheckNameLabel.Text += this.DesignCheck?.回 > 0 ? " " + this.DesignCheck?.回 + "回目" : "";

            // データバインド
            this.CustomTemplate.SetDataSource(this.GetDesignCheckPointList());

            // セル結合処理
            foreach (var row in this.PointMultiRow.Rows)
            {
                this.ColInfos

                    // マージ対象列
                    .FindAll((x) => string.IsNullOrEmpty(x.HiddenCellName) == false)

                    .ForEach((x) =>
                    {
                        // 各IDの先頭行のみ表示
                        row.Cells[x.RowCellName].Visible = IsFirstPointRow(row.Index);

                        // 各IDの先頭行のみ下罫線消す
                        row.Cells[x.RowCellName].Style.Border = GetBorder(row.Index);

                        // 各IDの先頭行のみ非表示
                        row.Cells[x.HiddenCellName].Visible = !IsFirstPointRow(row.Index);

                        // 隠しセル選択不可
                        row.Cells[x.HiddenCellName].Selectable = false;
                    });

                // 行の高さ調整
                row.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit();
            }

            // 一覧を未選択状態に設定
            this.PointMultiRow.CurrentCell = null;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckPointHistoryForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.PointMultiRow.CurrentCell = null;

        }
        #endregion

        #region 設計チェック指摘一覧イベント

        #region セル結合時
        /// <summary>
        /// セル結合時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_QueryCellMergeState(object sender, QueryCellMergeStateEventArgs e)
        {
            if (e.ShouldMerge == true)
            {
                var itemIdIndex = this.PointMultiRow.Columns["IDTextBoxColumn"].Index;

                if (e.QueryCell.CellIndex != itemIdIndex)
                {
                    CellPosition newQueryCell = new CellPosition(e.QueryCell.RowIndex, itemIdIndex);
                    CellPosition newTargetCell = new CellPosition(e.TargetCell.RowIndex, itemIdIndex);

                    // 項目IDが同じ場合セル結合させる
                    e.ShouldMerge = this.PointMultiRow.IsMerged(newQueryCell, newTargetCell);
                }
            }
        }
        #endregion

        #endregion

        #region データの取得
        /// <summary>
        /// 設計チェック指摘一覧の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointGetOutModel> GetDesignCheckPointList()
        {
            //パラメータ設定
            var cond = new DesignCheckPointGetInModel
            {
                // 開催日_ID
                開催日_ID = (int)this.DesignCheck.ID,

                //Update Start 2021/07/28 杉浦 設計チェックインポート
                //// 指摘NO
                //指摘NO = (int)this.DesignCheckPoint.指摘NO,
                // 指摘NO
                指摘NO = this.DesignCheckPoint.指摘NO,

                // 試作管理NO
                試作管理NO = this.DesignCheckPoint.試作管理NO
                //Update Start 2021/07/28 杉浦 設計チェックインポート

            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckPointGetInModel, DesignCheckPointGetOutModel>(ControllerType.DesignCheckPoint, cond);

            //レスポンスが取得できたかどうか
            var list = new List<DesignCheckPointGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results.ToArray().Select(x =>
                {
                    x.STATUS = x.FLAG_CLOSE == 1 ? "CLOSE" : "OPEN";

                    //Append Start 2021/07/28 杉浦 設計チェックインポート
                    x.試作CLOSE = x.FLAG_試作CLOSE == 1 ? "済" : "未";
                    //Append End 2021/07/28 杉浦 設計チェックインポート

                    return x;

                }));

            }

            return list;

        }
        #endregion

        #region Borderを取得します。
        /// <summary>
        /// Borderを取得します。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private Border GetBorder(int rowIndex)
        {
            var border = new Border(
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver),
                                new Line(LineStyle.Thin, Color.Silver));

            if (rowIndex == -1 || rowIndex == PointMultiRow.RowCount - 1) return border;

            var now = PointMultiRow.Rows[rowIndex].Cells["IDTextBoxColumn"].Value.ToString();
            var aft = PointMultiRow.Rows[rowIndex + 1].Cells["IDTextBoxColumn"].Value.ToString();

            if (now != aft)
            {
                return border;
            }
            else
            {
                border.Bottom = Line.Empty;
                return border;
            }
        }
        #endregion

        #region 該当行がその指摘Noの最初の行か？
        /// <summary>
        /// 該当行がその指摘Noの最初の行か？
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool IsFirstPointRow(int rowIndex)
        {
            if (rowIndex == 0)
            {
                return true;
            }

            var now = this.PointMultiRow.Rows[rowIndex].Cells["IDTextBoxColumn"].Value;
            var bef = this.PointMultiRow.Rows[rowIndex - 1].Cells["IDTextBoxColumn"].Value;

            return Convert.ToString(now) != Convert.ToString(bef);
        }
        #endregion

        #region 列情報クラス
        /// <summary>
        /// 列情報クラス
        /// </summary>
        private class ColInfo
        {
            public int Index { get; set; }

            /// <summary>
            /// その列の行に表示するセル名
            /// </summary>
            public string RowCellName { get; set; }

            /// <summary>
            /// RowCellNameに対応する隠しセル名（セル結合用）
            /// </summary>
            public string HiddenCellName { get; set; }

            /// <summary>
            /// その列を表示するか？
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// 列ヘッダー名
            /// </summary>
            public string HeaderName { get; set; }

            /// <summary>
            /// 列ヘッダーフィルターを表示しないか？
            /// </summary>
            public bool IsNonFilter { get; set; }
        }
        #endregion
    }
}
