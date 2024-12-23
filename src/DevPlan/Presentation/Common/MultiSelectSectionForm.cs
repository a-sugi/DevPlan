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
    public partial class MultiSelectSectionForm : BaseSubForm
    {
        #region メンバ変数
        private CustomTemplate MultiSelectSectionTemplate = new CustomTemplate();
        #endregion

        #region プロパティ
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "課検索(複数選択)"; } }

        /// <summary>課名（カンマ区切り）</summary>
        public string Sections { get; set; }

        /// <summary>更新ボタン可否</summary>
        public bool EntryButtonEnabled { get { return this.EntryButton.Enabled; } set { this.EntryButton.Enabled = value; } }

        /// <summary>機能ID</summary>
        public FunctionID FunctionId { get; set; }

        /// <summary>全選択フラグ</summary>
        public bool IsSelectedAll { get; set; } = false;

        /// <summary>全選択フラグ(他条件)</summary>
        public bool IsOtherSelectedAll { get; set; } = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MultiSelectSectionForm()
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
            this.MultiSelectSectionTemplate.MultiRow = this.SectionListMultiRow;

            // テンプレート
            this.SectionListMultiRow.Template = this.MultiSelectSectionTemplate.SetContextMenuTemplate(new MultiSelectSectionMultiRowTemplate());

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
            //選択設定
            string[] sectionList = null;
            if (Sections != null && 0 < Sections.Length)
            {
                sectionList = Sections.Split(',');
            }

            //グリッドデータの取得
            var list = GetGridDataList();

            // データバインド
            this.MultiSelectSectionTemplate.SetDataSource(list);

            if (sectionList != null && sectionList.Count() > 0)
            {
                // 前回選択情報を反映
                SectionListMultiRow.Rows
                    .Where((x) => sectionList.Contains(Convert.ToString(x.Cells["SectionColumn"].Value)))
                    .ToList()
                    .ForEach((x) => x.Cells["CheckBoxColumn"].Value = true);

                // チェックボックスONの項目を上部に表示する
                SectionListMultiRow.Sort("CheckBoxColumn", SortOrder.Descending);
            }

            //一覧を未選択状態に設定
            this.SectionListMultiRow.CurrentCell = null;
        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<SectionModel> GetGridDataList()
        {
            // パラメータ設定
            var itemCond = new SectionSearchModel
            {
                //パラメータなし
            };

            // Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

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
            var list = new List<string>();
            this.Sections = "";

            for (int i = 0; i < SectionListMultiRow.RowCount; i++)
            {
                if (SectionListMultiRow.Rows[i].Cells["SectionColumn"].Value == null)
                {
                    continue;
                }

                if (Convert.ToBoolean(SectionListMultiRow.Rows[i].Cells["CheckBoxColumn"].Value) == true)
                {
                    list.Add(SectionListMultiRow.Rows[i].Cells["SectionColumn"].Value.ToString());
                }
            }

            // 全検索フラグのセット
            IsSelectedAll = list.Count == SectionListMultiRow.RowCount ? true: false;

            var message = string.Empty;

            if (20 < list.Count && !IsSelectedAll)
            {
                // DBの文字数制限
                if (FunctionId == FunctionID.CAP)
                {
                    message = string.Format(Resources.KKM03025, "専門部署名", Const.CrLf + "20件以下で選択してください。");
                }
                else
                {
                    message = string.Format(Resources.KKM03025, "関連課", "");
                }
            }
            else if (20 < list.Count && IsSelectedAll && IsOtherSelectedAll)
            {
                // 複合条件
                if (FunctionId == FunctionID.CAP)
                {
                    message = string.Format(Resources.KKM03025, "専門部署名", Const.CrLf + "20件以下で選択してください。");
                }
            }

            // エラーメッセージの表示
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messenger.Warn(message);

                return;
            }

            this.Sections = string.Join(",", list);

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
            var cur = SectionListMultiRow.CurrentCell;

            foreach (var row in SectionListMultiRow.Rows)
            {
                // 非表示行は未処理
                if (!row.Visible) continue;

                row.Cells[0].Value = VisibleCheckBox.Checked;
            }

            SectionListMultiRow.CurrentCell = cur;
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
            checkCell.Value = !Convert.ToBoolean(checkCell.Value);
        }
        #endregion
    }
}
