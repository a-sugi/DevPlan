using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;
using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Common;
using System.Data;
using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{
    /// <summary>
    /// CAP課題履歴
    /// </summary>
    public partial class CapAndProductHistoryForm : BaseForm
    {
        #region メンバ変数
        private readonly DateTime ConstDate = new DateTime(2001, 1, 1);

        /// <summary>項目ID</summary>
        private String ID;

        /// <summary>
        /// ADユーザー情報
        /// </summary>
        private Dictionary<string, ADUserInfo> ADUserDictionary;

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();

        /// <summary>
        /// 赤文字列名とNoの対応表
        /// </summary>
        private Dictionary<string, string> redColList = new Dictionary<string, string>
            {
                {"001","専門部署名" },
                {"002","対策予定" },
                {"003","対策案" },
                {"005","分類" },
                {"006","評価レベル" },
                {"007","完了日程" },
                {"008","供試品" },
                {"009","出図日程" },
            };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "CAP課題履歴"; } }

        /// <summary>項目ID</summary>
        public String ITEM_ID { get { return ID; } set { ID = value; } }
        #endregion

        #region 内部変数

        private const string LAST_DATA_TEXT = "最新";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CapAndProductHistoryForm()
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
        private void CarShareForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            //AD情報の取得
            this.ADUserDictionary = ADUserInfoData.Dictionary;
        }
        #endregion

        #region 画面初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            // テンプレート設定
            this.customTemplate.ColumnHeaderHeight = 90;
            this.customTemplate.RowCountLabel = RowCountLabel;
            this.customTemplate.MultiRow = this.CapMultiRow;
            this.CapMultiRow.Template = this.customTemplate.SetContextMenuTemplate(new CapAndProductHistoryMultiRowTemplate());
            this.CapMultiRow.MouseWheelCount = new GridAppConfigAccessor().GetGridMouseWheelCount();
            this.CapMultiRow.VerticalScrollCount = this.CapMultiRow.MouseWheelCount;

            //カラムヘッダの背景色を変更
            SetColumnHeaderColor();

            //グリッド更新
            SearchItems();
        }

        /// <summary>
        /// カラムヘッダの色指定
        /// </summary>
        private void SetColumnHeaderColor()
        {
            Color correspondenceColor = Color.Khaki;
            Color otherColor = Color.LightGray;
            Color foreColor = Color.Black;

            CapMultiRow.ColumnHeaders[0].Cells["ch_承認表示"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_承認表示"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_専門部署名"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_専門部署名"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対策案編集者"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対策案編集者"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対策予定"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対策予定"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対応策"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_対応策"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_事前把握"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_事前把握"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_分類"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_分類"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_評価レベル"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_評価レベル"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_完了日程コンボボックス"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_完了日程コンボボックス"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_完了日程"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_完了日程"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_供試品"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_供試品"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_出図日程"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_出図日程"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_出図日程コンボボックス"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_出図日程コンボボックス"].Style.ForeColor = foreColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_織込時期"].Style.BackColor = correspondenceColor;
            CapMultiRow.ColumnHeaders[0].Cells["ch_織込時期"].Style.ForeColor = foreColor;

            this.CapMultiRow.ColumnHeaders[0].Cells["ch_履歴作成日時"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_履歴作成日時"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_編集日"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_編集日"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_編集者"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_編集者"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_内線番号"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_内線番号"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_回答期限設定日"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_回答期限設定日"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_承認日"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_承認日"].Style.ForeColor = foreColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_承認者"].Style.BackColor = otherColor;
            this.CapMultiRow.ColumnHeaders[0].Cells["ch_承認者"].Style.ForeColor = foreColor;
        }
        #endregion

        #region 情報取得表示
        /// <summary>
        /// 情報取得表示
        /// </summary>
        private void SearchItems()
        {
            var paramCond = new CapSearchModel
            {
                項目_ID = Convert.ToInt64(this.ID),
                PERSONEL_ID = SessionDto.UserId,
            };

            //APIで取得
            var res = HttpUtil.GetResponse<CapSearchModel, CapModel>(ControllerType.Cap, paramCond);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return;
            }

            //取得した情報を画面表示
            var list = (res.Results).ToList();
            if (list.Count == 0)
            {
                return;
            }

            this.customTemplate.SetDataSource(list);

            foreach (var row in CapMultiRow.Rows)
            {
                var idx = row.Index;

                if (row.Cells["FLAG_承認"].Value == null || row.Cells["FLAG_承認"].Value.ToString() != "1")
                {
                    row.Cells["承認表示"].Value = "×";
                }
                else
                {
                    row.Cells["承認表示"].Value = "○";
                }

                if (Convert.ToDateTime(row.Cells["完了日程"].Value) == ConstDate)
                {
                    row.Cells["完了日程"].Value = null;
                    row.Cells["完了日程コンボボックス"].Value = "済";
                }

                if (Convert.ToDateTime(row.Cells["出図日程"].Value) == ConstDate)
                {
                    row.Cells["出図日程"].Value = null;
                    row.Cells["出図日程コンボボックス"].Value = "済";
                }

                if (list[idx].分類 != null)
                {
                    row.Cells["分類"].ToolTipText = list[idx].分類 + " " + list[idx].分類意味;
                }
                if (list[idx].評価レベル != null)
                {
                    row.Cells["評価レベル"].ToolTipText = list[idx].評価レベル + " " + list[idx].評価意味 + " " + list[idx].レベル基準 + " " + list[idx].判断イメージ;
                }

                if (IsLastHistoryData(idx))
                {
                    row.Cells["InsertDateColumn"].Style.Format = LAST_DATA_TEXT;
                }

                // 内線番号
                if (row.Cells["編集者_ID"].Value != null)
                {
                    if (ADUserDictionary != null)
                    {
                        var personelId = Convert.ToString(row.Cells["編集者_ID"].Value);
                        var searchPersonelId = personelId.PadLeft(5, '0').Substring(0, 5);

                        var val = new ADUserInfo();
                        var key = string.Format("{0}_{1}", searchPersonelId, Convert.ToString(row.Cells["編集者"].Value)).Replace(" ", "").Replace("　", "");

                        ADUserDictionary.TryGetValue(key, out val);

                        // 内線番号あり
                        if (val != null || val?.Tel != null)
                        {
                            row["内線番号"].Value = val.Tel;
                        }
                    }
                }

                // 赤文字設定
                var redSettings = Convert.ToString(row.Cells["修正カラム"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var no in redSettings)
                {
                    string colName;
                    if (this.redColList.TryGetValue(no, out colName))
                    {
                        row.Cells[colName].Style.ForeColor = Color.Red;
                    }
                }

                row.Cells["対策案"].PerformVerticalAutoFit();
            }


            // 車種
            GeneralCodeLabel.Text = list[0].GENERAL_CODE;

            //No.
            NoLabel.Text = list[0].NO.ToString();

            //重要度
            ImportanceLabel.Text = list[0].重要度 + "  " + list[0].説明;

            //評価車両
            CarLabel.Text = list[0].評価車両;

            //項目
            ItemLabel.Text = list[0].項目;

            //詳細
            DetailLabel.Text = list[0].詳細;

            //フォロー状況
            FollowLabel.Text = list[0].フォロー状況;

            //CAP確認時期
            CapScheduleLabel.Text = list[0].織込時期_項目;

            //指摘分類
            KindLabel.Text = list[0].指摘分類;

            //CAP確認結果
            CapResultLabel.Text = list[0].CAP確認結果;

            //グリッドを非選択
            CapMultiRow.CurrentCell = null;
        }
        #endregion

        #region 枠線描画
        /// <summary>
        /// 枠線描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapDataGridView_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < -1 || e.CellIndex < 0 || e.CellIndex > this.CapMultiRow.Columns.Count - 1)
            {
                return;
            }

            if (this.CapMultiRow.Columns[e.CellIndex].Name == "完了日程")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Left = new Line(LineStyle.None, Color.White);
            }
            if (this.CapMultiRow.Columns[e.CellIndex].Name == "完了日程コンボボックス")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Right = new Line(LineStyle.None, Color.White);
            }
            if (this.CapMultiRow.Columns[e.CellIndex].Name == "出図日程")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Left = new Line(LineStyle.None, Color.White);
            }
            if (this.CapMultiRow.Columns[e.CellIndex].Name == "出図日程コンボボックス")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Right = new Line(LineStyle.None, Color.White);
            }
        }
        #endregion

        #region 選択行の表示制御

        /// <summary>
        /// セルゲットフォーカス時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellEnter(object sender, CellEventArgs e)
        {
            if (e.CellName == "InsertDateColumn" && IsLastHistoryData(e.RowIndex))
            {
                for (var idx = e.RowIndex; idx >= 0; idx--)
                {
                    CapMultiRow.Rows[idx].Cells["InsertDateColumn"].Style.Format = "G";

                    if (idx == 0 || !CapMultiRow.IsMerged(new CellPosition(idx, e.CellIndex), new CellPosition(idx - 1, e.CellIndex)))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// セルロストフォーカス時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellLeave(object sender, CellEventArgs e)
        {
            if (e.CellName == "InsertDateColumn" && IsLastHistoryData(e.RowIndex))
            {
                for (var idx = e.RowIndex; idx >= 0; idx--)
                {
                    CapMultiRow.Rows[idx].Cells["InsertDateColumn"].Style.Format = LAST_DATA_TEXT;

                    if (idx == 0 || !CapMultiRow.IsMerged(new CellPosition(idx, e.CellIndex), new CellPosition(idx - 1, e.CellIndex)))
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 履歴作成日時のチェックを行います。
        /// </summary>
        /// <remarks>
        /// FLAG_最新が1の時はTrue、それ以外の時はfalseを返却します。
        /// </remarks>
        /// <param name="rowIndex">確認対象のCapDataGridViewのRowIndex</param>
        /// <returns></returns>
        private bool IsLastHistoryData(int rowIndex)
        {
            var flag = CapMultiRow.Rows[rowIndex].Cells["FlagNewColumn"].Value;

            if (flag == null)
            {
                return false;
            }

            return flag.ToString() == "1";
        }

        #endregion

    }
}
