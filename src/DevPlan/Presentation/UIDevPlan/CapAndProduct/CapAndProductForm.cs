using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Linq.Expressions;

using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;

using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{
    /// <summary>
    /// CAP課題一覧
    /// </summary>
    public partial class CapAndProductForm : BaseForm
    {
        #region メンバ変数

        /// <summary>
        /// 行追加する時の仮のID
        /// </summary>
        private int tempId = -1;

        private readonly Dictionary<short, string> StatusMap = new Dictionary<short, string>
        {
            { 0, "OPEN" },
            { 1, "CLOSE" }
        };

        //CSV出力項目
        private readonly string[] CsvHeaders =
        {
            "車種",
            "No",
            "回答期限",
            "重要度",
            "項目",
            "詳細",
            "評価車両",
            "承認",
            "専門部署名",
            "対策予定",
            "対策案",
            "完了日程CSV",
            "供試品",
            "出図日程CSV",
            "織込時期",
            "事前把握",
            "分類",
            "評価レベル",
            //Append Start 2021/06/25 矢作
            "方向付け確定期限",
            //Append End 2021/06/25 矢作
        };

        /// <summary>
        /// CAP項目
        /// </summary>
        private readonly List<string> ItemHeaders = new List<string>
        {
            "No",
            "種別",
            "重要度",
            "項目",
            "詳細",
            "評価車両",
            "仕向地",
            "CAP確認結果",
            "フォロー状況",
            "CAP確認時期",
            "ステータス",
        };

        /// <summary>
        /// 権限制御項目
        /// </summary>
        private readonly string[] AuthTargetCells = new string[]
        {
            "CAP確認",
            "No",
            "回答期限",
            "種別",
            "重要度",
            "項目",
            "詳細",
            "評価車両",
            "仕向地",
            "CAP確認結果",
            //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
            "N_DRIVE_LINK",
            //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
            "承認",
            "専門部署名",
            "対策予定",
            "対策案",
            "完了日程コンボボックス",
            "完了日程",
            "供試品",
            "出図日程コンボボックス",
            "出図日程",
            "織込時期",
            "事前把握",
            "分類",
            "評価レベル",
            "フォロー状況",
            "CAP確認時期",
            "ステータス",
            //Append Start 2021/06/15 矢作
            "方向付け確定期限",
            //Append End 2021/06/15 矢作
        };

        /// <summary>
        /// 編集情報更新項目
        /// </summary>
        private readonly List<string> EditInfoTargetCells = new List<string>
        {
            //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
            "N_DRIVE_LINK",
            //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
            "専門部署名",
            "対策予定",
            "対策案",
            "完了日程コンボボックス",
            "完了日程",
            "供試品",
            "出図日程コンボボックス",
            "出図日程",
            "織込時期",
            "事前把握",
            "分類",
            "評価レベル",
        };

        /// <summary>
        /// "済"固定日時
        /// </summary>
        private static readonly DateTime ConstDate = new DateTime(2001, 1, 1);

        /// <summary>
        /// "済"表示文字
        /// </summary>
        private const string ConstFinish = "済";

        /// <summary>
        /// データソース用リスト
        /// </summary>
        private List<CapModel> CapItemList;

        /// <summary>
        /// バインディングソース
        /// </summary>
        private BindingSource bindingSource = new BindingSource();

        /// <summary>
        /// ADユーザー情報
        /// </summary>
        private Dictionary<string, ADUserInfo> ADUserDictionary;

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();

        /// <summary>
        /// カスタムセルスタイル
        /// </summary>
        private static CustomMultiRowCellStyle customMultiRowCellStyle = new CustomMultiRowCellStyle();

        /// <summary>
        /// クラス生成管理クラス
        /// </summary>
        private Factory factory;

        //Append Start 2021/06/21 矢作
        /// <summary>
        /// フォロー状況・赤文字対象
        /// </summary>
        private readonly List<string> redList = new List<string>
        {
            "調査中／調査待ち",
            "改善案検討中",
            "本開発車ではこのままとしたい",
            "本開発車ではこのままとしたい ",
            "本開発車ではこのままとする（クローズ）",
        };
        //Append End 2021/06/21 矢作

        #endregion


        #region プロパティ

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "CAP課題一覧"; } }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        /// <summary>編集フラグ</summary>
        private bool IsEdit { get; set; } = false;

        /// <summary>全選択フラグ(開発符号)</summary>
        public bool IsGeneralCodeAllSerach { get; set; } = false;

        /// <summary>全選択フラグ(課)</summary>
        public bool IsSectionCodeAllSerach { get; set; } = false;

        /// <summary>回答期限編集フラグ</summary>
        private bool IsEditAnswerLimit { get; set; } = false;

        /// <summary>全展開フラグ</summary>
        private bool IsAllAutoFit { get; set; } = false;
        #endregion

        #region 内部変数

        /// <summary>検索結果</summary>
        CapSearchModel SearchModel;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CapAndProductForm()
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
            FormControlUtil.FormWait(this, () =>
            {
                // クラス初期化
                factory = new Factory(this.CapMultiRow, base.GetFunction(FunctionID.CAP), GetSectionUseGeneralCodeList());
                factory.GetBtnInstance("No").AskEntry = () => IsUpdate();

                // Append Start 2021/01/25 杉浦
                // 回答期限(一般用)
                this.FromNullableDateTimePicker.Value = null;
                this.ToNullableDateTimePicker.Value = null;
                // Append End 2021/01/25 杉浦

                // MultiRow設定
                this.InitMultiRow();

                // 画面初期化
                this.InitForm();

                // コマンドボタン共通処理を追加してイベント登録
                this.factory.CmdBtn.SetEvent(ForeColorButton, ForeColorButton_Click);
                this.factory.CmdBtn.SetEvent(AddButton, AddButton_Click);
                this.factory.CmdBtn.SetEvent(DeleteButton, DeleteButton_Click);

                //AD情報の取得
                this.ADUserDictionary = ADUserInfoData.Dictionary;
            });
        }

        /// <summary>
        /// MultiRow設定
        /// </summary>
        private void InitMultiRow()
        {
            var tmp = new CapAndProductMultiRowTemplate();
            var col = tmp.ColumnHeaders[0];
            var row = tmp.Row;

            var capKind = row.Cells["種別"] as ComboBoxCell;
            capKind.ValueMember = "種別";
            capKind.DisplayMember = "種別";
            capKind.DataSource = (new[] { new CapKindModel() }).Concat(HttpUtil.GetResponse<CapKindModel>(ControllerType.CapKind)?.Results).ToList();

            var capSample = row.Cells["供試品"] as ComboBoxCell;
            capSample.ValueMember = "供試品";
            capSample.DisplayMember = "供試品";
            capSample.DataSource = (new[] { new CapSampleModel() }).Concat(HttpUtil.GetResponse<CapSampleModel>(ControllerType.CapSample)?.Results).ToList();

            var capStage = row.Cells["織込時期"] as ComboBoxCell;
            capStage.ValueMember = "織込時期";
            capStage.DisplayMember = "織込時期";
            capStage.DataSource = (new[] { new CapStageModel() }).Concat(HttpUtil.GetResponse<CapStageModel>(ControllerType.CapStage)?.Results).Where(x => x.FLAG_DISP == 1).ToList();

            var capFollow = row.Cells["フォロー状況"] as ComboBoxCell;
            capFollow.ValueMember = "フォロー状況";
            capFollow.DisplayMember = "フォロー状況";
            capFollow.DataSource = (new[] { new CapFollowModel() }).Concat(HttpUtil.GetResponse<CapFollowModel>(ControllerType.CapFollow)?.Results).ToList();

            var capLocation = row.Cells["仕向地"] as ComboBoxCell;
            capLocation.ValueMember = "仕向";
            capLocation.DisplayMember = "仕向";
            capLocation.DataSource = (new[] { new CapLocationModel() }).Concat(HttpUtil.GetResponse<CapLocationModel>(ControllerType.CapLocation)?.Results).ToList();

            var capSchedule = row.Cells["対策予定"] as ComboBoxCell;
            capSchedule.ValueMember = "対策予定";
            capSchedule.DisplayMember = "対策予定";
            capSchedule.DataSource = (new[] { new CapScheduleModel() }).Concat(HttpUtil.GetResponse<CapScheduleModel>(ControllerType.CapSchedule)?.Results).ToList();

            var capCheck = row.Cells["CAP確認時期"] as ComboBoxCell;
            capCheck.ValueMember = "織込時期";
            capCheck.DisplayMember = "織込時期";
            capCheck.DataSource = (new[] { new CapStageModel() }).Concat(HttpUtil.GetResponse<CapStageModel>(ControllerType.CapStage)?.Results).ToList();

            var status = row.Cells["ステータス"] as ComboBoxCell;
            status.ValueMember = "Key";
            status.DisplayMember = "Value";
            status.DataSource = this.StatusMap.ToArray();

            // 権限制御
            if (!this.factory.Auth.IsAdmin())
            {
                var diff = col.Cells["ch_CAP確認"].Width;

                // CAP確認
                col.Cells["ch_CAP確認"].Visible = false;
                row.Cells["CAP確認"].Visible = false;

                // 車種
                col.Cells["ch_車種"].Location = new Point(col.Cells["ch_車種"].Location.X - diff, col.Cells["ch_車種"].Location.Y);
                row.Cells["車種"].Location = new Point(row.Cells["車種"].Location.X - diff, row.Cells["車種"].Location.Y);

                // 指摘No
                col.Cells["ch_No"].Location = new Point(col.Cells["ch_No"].Location.X - diff, col.Cells["ch_No"].Location.Y);
                row.Cells["No"].Location = new Point(row.Cells["No"].Location.X - diff, row.Cells["No"].Location.Y);

                // 回答期限
                col.Cells["ch_回答期限"].Location = new Point(col.Cells["ch_回答期限"].Location.X - diff, col.Cells["ch_回答期限"].Location.Y);
                row.Cells["回答期限"].Location = new Point(row.Cells["回答期限"].Location.X - diff, row.Cells["回答期限"].Location.Y);

                // 種別
                col.Cells["ch_種別"].Location = new Point(col.Cells["ch_種別"].Location.X - diff, col.Cells["ch_種別"].Location.Y);
                row.Cells["種別"].Location = new Point(row.Cells["種別"].Location.X - diff, row.Cells["種別"].Location.Y);

                // 重要度
                col.Cells["ch_重要度"].Location = new Point(col.Cells["ch_重要度"].Location.X - diff, col.Cells["ch_重要度"].Location.Y);
                row.Cells["重要度"].Location = new Point(row.Cells["重要度"].Location.X - diff, row.Cells["重要度"].Location.Y);

                // 項目
                col.Cells["ch_項目"].Location = new Point(col.Cells["ch_項目"].Location.X - diff, col.Cells["ch_項目"].Location.Y);
                row.Cells["項目"].Location = new Point(row.Cells["項目"].Location.X - diff, row.Cells["項目"].Location.Y);

                // 項目詳細
                col.Cells["ch_詳細"].Location = new Point(col.Cells["ch_詳細"].Location.X - diff, col.Cells["ch_詳細"].Location.Y);
                row.Cells["詳細"].Location = new Point(row.Cells["詳細"].Location.X - diff, row.Cells["詳細"].Location.Y);

                col.Cells["ch_詳細"].Size = new Size(col.Cells["ch_詳細"].Width + diff, col.Cells["ch_詳細"].Height);
                row.Cells["詳細"].Size = new Size(row.Cells["詳細"].Width + diff, row.Cells["詳細"].Height);
            }

            // テンプレート設定
            this.customTemplate.ColumnHeaderHeight = 90;
            this.customTemplate.GroupCountLabel = this.GroupCountLabel;
            this.customTemplate.GroupCountFormat = "指摘No数： {0:#,0}/{1:#,0} 件";
            this.customTemplate.GroupCountCellName = "No";
            this.customTemplate.MultiRow = this.CapMultiRow;
            this.CapMultiRow.Template = this.customTemplate.SetContextMenuTemplate(tmp);

            // CAP確認列のフィルターアイテム設定
            var cellCap = this.CapMultiRow.Template.ColumnHeaders[0].Cells["ch_CAP確認"] as ColumnHeaderCell;
            //Append Start 2021/08/12 杉浦 CAP要望対応
            if(cellCap.DropDownContextMenuStrip != null)
            {
                //Append End 2021/08/12 杉浦 CAP要望対応
                cellCap.DropDownContextMenuStrip.Items.RemoveAt(cellCap.DropDownContextMenuStrip.Items.Count - 1);
                cellCap.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });
                //Append Start 2021/08/12 杉浦 CAP要望対応
            }
            //Append End 2021/08/12 杉浦 CAP要望対応

            // 承認列のフィルターアイテム設定
            var cellApproval = this.CapMultiRow.Template.ColumnHeaders[0].Cells["ch_承認"] as ColumnHeaderCell;
            //Append Start 2021/08/12 杉浦 CAP要望対応
            if (cellApproval.DropDownContextMenuStrip != null)
            {
                //Append End 2021/08/12 杉浦 CAP要望対応
                cellApproval.DropDownContextMenuStrip.Items.RemoveAt(cellApproval.DropDownContextMenuStrip.Items.Count - 1);
                cellApproval.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });
                //Append Start 2021/08/12 杉浦 CAP要望対応
            }
            //Append End 2021/08/12 杉浦 CAP要望対応

            // ダブルバッファリング有効
            this.CapMultiRow.GetType().InvokeMember(
               "DoubleBuffered",
               System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
               null,
               this.CapMultiRow,
               new object[] { true });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            // 初期条件設定
            this.SetClearConditon();

            //権限設定
            this.SetFunctionAuth();

            //カラムヘッダの背景色を変更
            this.SetColumnHeaderColor();

            // Hiddenパネルの背景色設定
            this.HidePanel1.BackColor = this.ContentsPanel.BackColor;
            this.HidePanel2.BackColor = this.ContentsPanel.BackColor;
            this.HidePanel3.BackColor = this.ContentsPanel.BackColor;
        }

        /// <summary>
        /// 初期条件設定
        /// </summary>
        private void SetClearConditon()
        {
            var isAdmin = this.factory.Auth.IsAdmin();

            // バインドフラグ
            this.IsBind = true;

            try
            {
                // 車種
                this.GeneralCodeComboBox.Items.Clear();
                this.GeneralCodeComboBox.SelectedIndex = -1;
                FormControlUtil.SetComboBoxBackColor(GeneralCodeComboBox, Const.DefaultBackColor);

                // 専門部署名
                this.SectionComboBox.Items.Clear();
                this.SectionComboBox.SelectedIndex = -1;
                FormControlUtil.SetComboBoxBackColor(SectionComboBox, Const.DefaultBackColor);

                // 関連課
                this.RelationSectionCheckBox.Checked = isAdmin ? false : true;
                this.RelationSectionCheckBox.Enabled = isAdmin ? false : true;

                // 回答期限
                this.FromNullableDateTimePicker.Value = null;
                this.ToNullableDateTimePicker.Value = null;

                // ステータス
                this.OpenCheckBox.Checked = true;
                this.CloseCheckBox.Checked = isAdmin ? true : false;

                // 部署承認
                this.SectionOffCheckBox.Checked = true;
                this.SectionOnCheckBox.Checked = true;

                // 重要度
                this.ImportanceComboBox.Items.Clear();
                this.ImportanceComboBox.SelectedIndex = -1;

                // CAP確認
                this.CapOffCheckBox.Checked = true;
                this.CapOnCheckBox.Checked = true;

                // 専門部署名
                if (!isAdmin)
                {
                    this.SectionComboBox.Items.Add(SessionDto.SectionCode);
                    this.SectionComboBox.SelectedIndex = 0;
                }
            }
            finally
            {
                // バインドフラグ
                this.IsBind = false;

                // 全選択フラグ(開発符号)
                this.IsGeneralCodeAllSerach = false;

                // 全選択フラグ(課)>
                this.IsSectionCodeAllSerach = false;
            }
        }

        /// <summary>
        /// 権限設定
        /// </summary>
        private void SetFunctionAuth()
        {
            AddButton.Visible = this.factory.Auth.IsVisible(AddButton);
            DeleteButton.Visible = this.factory.Auth.IsVisible(DeleteButton);
            ImportButton.Visible = this.factory.Auth.IsVisible(ImportButton);
            ExportButton.Visible = this.factory.Auth.IsVisible(ExportButton);
            EntryButton.Visible = this.factory.Auth.IsVisible(EntryButton);
            ForeColorButton.Visible = this.factory.Auth.IsVisible(ForeColorButton);
            DepartmentDownloadButton.Visible = this.factory.Auth.IsVisible(DepartmentDownloadButton);
            SectionDownloadButton.Visible = this.factory.Auth.IsVisible(SectionDownloadButton);
            //Append Start 2021/07/28 矢作
            WayInputButton.Visible = this.factory.Auth.IsVisible(WayInputButton);
            //Append End 2021/07/28 矢作

            if (this.factory.Auth.IsAdmin() == false)
            {
                // 関連課をチェックする
                this.RelationSectionCheckBox.Checked = true;

                // ステータスCLOSEのチェックを外す
                this.CloseCheckBox.Checked = false;

                // CAP確認チェックボックスを非表示
                this.SearchConditionTableLayoutPanel.SetColumnSpan(HidePanel2, 3);
            }
        }

        /// <summary>
        /// カラムヘッダの色指定
        /// </summary>
        private void SetColumnHeaderColor()
        {
            var columnHeader = this.CapMultiRow.ColumnHeaders[0];

            // 回答列
            //Update Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
            //var answers = new string[] { "ch_承認", "ch_専門部署名", "ch_対策予定", "ch_対策案", "ch_対策案編集者", "ch_事前把握", "ch_分類", "ch_評価レベル", "ch_完了日程", "ch_完了日程コンボボックス", "ch_供試品", "ch_出図日程", "ch_出図日程コンボボックス", "ch_織込時期" };
            var answers = new string[] { "ch_N_DRIVE_LINK", "ch_承認", "ch_専門部署名", "ch_対策予定", "ch_対策案", "ch_対策案編集者", "ch_事前把握", "ch_分類", "ch_評価レベル", "ch_完了日程", "ch_完了日程コンボボックス", "ch_供試品", "ch_出図日程", "ch_出図日程コンボボックス", "ch_織込時期" };
            //Update End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加

            // 情報列
            var info = new string[] { "ch_編集日", "ch_編集者", "ch_内線番号", "ch_回答期限設定日", "ch_承認日", "ch_承認者" };

            // 回答列の色設定
            foreach (var str in answers)
            {
                columnHeader.Cells[str].Style.BackColor = Color.Khaki;
                columnHeader.Cells[str].Style.ForeColor = Color.Black;
            }

            // 課題列の色設定
            foreach (var str in info)
            {
                columnHeader.Cells[str].Style.BackColor = Color.LightGray;
                columnHeader.Cells[str].Style.ForeColor = Color.Black;
            }
        }
        #endregion

        #region フォーム表示後
        /// <summary>
        /// フォーム表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapAndProductForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = GeneralCodeComboBox;
            GeneralCodeComboBox.Focus();
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            // 検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, new[] { this.AutoFitLinkLabel, this.GroupCountLabel }, 133);
        }
        #endregion

        #region クリアボタンクリック
        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // 初期設定
            this.SetClearConditon();
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
            FormControlUtil.FormWait(this, () =>
            {
                // 変更確認
                if (AskAndUpdate())
                {
                    // 設定
                    this.SearchCapList();
                }
            });
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SearchCapList(bool isKeepScroll = false)
        {
            // 検索のチェック
            if (this.IsCapSchedule() == false)
            {
                return;
            }

            // スクロール位置の退避
            var id = this.CapMultiRow.CurrentCell?.RowIndex > 0 ? (long?)this.CapMultiRow.Rows[this.CapMultiRow.CurrentCell.RowIndex].Cells["ID"].Value : null;
            var displayedCellIndex = this.CapMultiRow.FirstDisplayedCellPosition != null ? (int?)this.CapMultiRow.FirstDisplayedCellPosition.CellIndex : null;
            var currentCellellIndex = this.CapMultiRow.CurrentCell != null ? (int?)this.CapMultiRow.CurrentCell.CellIndex : null;

            // AD情報の取得
            this.ADUserDictionary = ADUserInfoData.Dictionary;

            // 取得
            this.CapItemList = this.GetCapItemList();
            
            // バインド中ON
            this.IsBind = true;

            try
            {
                // 一覧設定
                this.SetCapList();

                // スクロール維持の場合
                if (isKeepScroll)
                {
                    if (this.IsAllAutoFit)
                    {
                        // 行の全展開
                        this.AdjustmentRowsVertical();
                    }

                    if (this.CapItemList.Count > 0 && id != null)
                    {
                        var rowIndex = this.CapItemList.Select((val, idx) => new { val.ID, idx }).FirstOrDefault(x => x.ID == id).idx;

                        // スクロール調整（セル選択）
                        this.CapMultiRow.FirstDisplayedCellPosition = new CellPosition(rowIndex, (int)displayedCellIndex);
                        this.CapMultiRow.CurrentCellPosition = new CellPosition(rowIndex, (int)currentCellellIndex);

                        if (!this.IsAllAutoFit)
                        {
                            // 行の個別展開
                            this.CapMultiRow.Rows[this.CapMultiRow.CurrentCell.RowIndex].Cells["対策案"].PerformVerticalAutoFit();
                            //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                            this.CapMultiRow.Rows[this.CapMultiRow.CurrentCell.RowIndex].Cells["N_DRIVE_LINK"].PerformVerticalAutoFit();
                            //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                        }
                    }
                }

                // メッセージラベル
                this.SetMessageLabel(this.CapItemList.Any() ? string.Empty : Resources.KKM00005, Color.Red);
            }
            finally
            {
                // 編集中OFF
                this.IsEdit = false;

                // バインド中OFF
                this.IsBind = false;

                // 全展開フラグ
                this.IsAllAutoFit = isKeepScroll ? this.IsAllAutoFit : false;
            }
        }

        /// <summary>
        /// 一覧設定
        /// </summary>
        private void SetCapList()
        {
            this.CapMultiRow.DataSource = bindingSource;

            // データバインド
            this.customTemplate.SetDataSource(this.CapItemList, bindingSource);

            this.CapMultiRow.SuspendLayout();

            // レイアウト調整
            foreach (var row in CapMultiRow.Rows)
            {
                var isOpen = Convert.ToInt32(row.Cells["ステータス"].Value) == 0;
                var isYellow = this.factory.BackColor.IsYellow(row);

                //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                row.Cells["N_DRIVE_LINK"].Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加

                foreach (var name in AuthTargetCells)
                {
                    //権限設定
                    if (this.factory.Auth.IsCan(name, Convert.ToString(row.Cells["専門部名"].Value), Convert.ToString(row.Cells["専門部署名"].Value)) == false)
                    {
                        row.Cells[name].ReadOnly = true;
                    }

                    //背景色(クローズ：編集可列)
                    if (!isOpen)
                    {
                        this.factory.Status.SetClose(row.Cells[name]);
                    }
                }

                //Append Start 2021/06/21 矢作
                DateTime redDatetime = DateTime.Now.AddMonths(1);
                //Append End 2021/06/21 矢作


                // 背景色(クローズ：編集不可列)
                if (!isOpen)
                {
                    row.Cells["車種"].Style.BackColor = Color.LightGray;
                    row.Cells["対策案編集者"].Style.BackColor = Color.LightGray;
                    row.Cells["編集日"].Style.BackColor = Color.LightGray;
                    row.Cells["編集者"].Style.BackColor = Color.LightGray;
                    row.Cells["内線番号"].Style.BackColor = Color.LightGray;
                    row.Cells["回答期限設定日"].Style.BackColor = Color.LightGray;
                    row.Cells["承認日"].Style.BackColor = Color.LightGray;
                    row.Cells["承認者"].Style.BackColor = Color.LightGray;
                }
                // 背景色(赤色)
                else if (isYellow)
                {
                    //Update Start 2020/12/24 杉浦
                    //row.Cells["専門部署名"].Style.BackColor = Color.Yellow;
                    //row.Cells["対策案"].Style.BackColor = Color.Yellow;
                    //Append Start 2021/08/20 杉浦
                    if (row.Cells["対策案"].Value == null)
                    {
                        //Append End 2021/08/20 杉浦
                        row.Cells["専門部署名"].Style.BackColor = Color.Red;
                        row.Cells["対策案"].Style.BackColor = Color.Red;
                        //Append Start 2021/08/20 杉浦
                    }
                    else if (row.Cells["方向付け確定期限"].Value != null)
                    {
                        if (redList.Contains((string)row.Cells["フォロー状況"].Value) && (DateTime)(row.Cells["方向付け確定期限"].Value) <= redDatetime)
                        {
                            row.Cells["専門部署名"].Style.BackColor = Color.Yellow;
                            row.Cells["対策案"].Style.BackColor = Color.Yellow;

                        }
                    }
                    //Append End 2021/08/20 杉浦
                    //Update End 2020/12/24 杉浦
                }
                //Append Start 2021/06/21 矢作
                //Update Start 2021/08/20 杉浦
                //if (row.Cells["方向付け確定期限"].Value != null)
                else if (row.Cells["方向付け確定期限"].Value != null)
                //Update End 2021/08/20 杉浦
                {
                    if (redList.Contains((string)row.Cells["フォロー状況"].Value) && (DateTime)(row.Cells["方向付け確定期限"].Value) <= redDatetime)
                    {
                        row.Cells["専門部署名"].Style.BackColor = Color.Yellow;
                        row.Cells["対策案"].Style.BackColor = Color.Yellow;

                    }
                }
                //Append End 2021/06/21 矢作


                // 赤文字
                if (!string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["修正カラム"].Value)))
                {
                    this.factory.ForeColor.SetCell(row, row.Cells["専門部署名"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["対策予定"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["対策案"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["完了日程"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["供試品"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["出図日程"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["分類"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["評価レベル"]);
                }

                // 青文字
                if (Convert.ToString(row.Cells["種別"].Value) == "商品力")
                {
                    this.factory.ForeColor.SetCell(row, row.Cells["種別"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["重要度"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["項目"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["詳細"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["仕向地"]);
                    this.factory.ForeColor.SetCell(row, row.Cells["CAP確認結果"]);
                }

                // 評価車両
                this.factory.ForeColor.SetCell(row, row.Cells["評価車両"]);

                //完了日程
                if (Convert.ToDateTime(row.Cells["完了日程"].Value) == ConstDate)
                {
                    row.Cells["完了日程コンボボックス"].Value = ConstFinish;
                    row.Cells["完了日程"].Value = null;
                }

                //出図日程
                if (Convert.ToDateTime(row.Cells["出図日程"].Value) == ConstDate)
                {
                    row.Cells["出図日程コンボボックス"].Value = ConstFinish;
                    row.Cells["出図日程"].Value = null;
                }

                // 内線番号
                this.SetADUserInfo(row);
            }

            this.CapMultiRow.ResumeLayout();

            // グリッドを非選択
            this.CapMultiRow.ClearSelection();

            // 変更フラグ設定
            this.IsEdit = false;
        }
        #endregion

        #region CAP課題一覧取得
        /// <summary>
        /// CAP課題一覧取得
        /// </summary>
        /// <returns></returns>
        private List<CapModel> GetCapItemList()
        {
            //ステータスチェックボックス
            short? status = null;
            if (OpenCheckBox.Checked != CloseCheckBox.Checked)
            {
                if (CloseCheckBox.Checked)
                {
                    status = 1;
                }
                else if (OpenCheckBox.Checked)
                {
                    status = 0;
                }
            }
            //部署承認チェックボックス
            short? approval = null;
            if (SectionOffCheckBox.Checked != SectionOnCheckBox.Checked)
            {
                if (SectionOnCheckBox.Checked)
                {
                    approval = 1;
                }
                else if (SectionOffCheckBox.Checked)
                {
                    approval = 0;
                }
            }
            //CAP確認チェックボックス
            short? cap = null;
            if (CapOffCheckBox.Checked != CapOnCheckBox.Checked)
            {
                if (CapOnCheckBox.Checked)
                {
                    cap = 1;
                }
                else if (CapOffCheckBox.Checked)
                {
                    cap = 0;
                }
            }

            //検索条件
            var cond = new CapSearchModel
            {
                //車種
                GENERAL_CODE = string.IsNullOrEmpty(GeneralCodeComboBox.Text) || IsGeneralCodeAllSerach ? null : GeneralCodeComboBox.Text?.Split(','),

                //回答期限(From)
                回答期限FROM = this.FromNullableDateTimePicker.SelectedDate,

                //回答期限(To)
                回答期限TO = this.ToNullableDateTimePicker.SelectedDate,

                //専門部署名
                専門部署名 = string.IsNullOrEmpty(SectionComboBox.Text) || IsSectionCodeAllSerach ? null : SectionComboBox.Text?.Split(','),

                //重要度
                重要度 = ImportanceComboBox.Text == "" ? null : ImportanceComboBox.Text,

                //ステータス
                FLAG_CLOSE = status,

                //部署承認
                FLAG_上司承認 = approval,

                //CAP確認
                FLAG_CAP確認 = cap,

                //関連課
                FLAG_関連課 = this.RelationSectionCheckBox.Checked ? (short?)1 : 0
            };

            //前回検索条件保存
            this.SearchModel = cond;

            //返却
            return this.GetCapItemList(cond);
        }

        /// <summary>
        /// CAP課題一覧取得
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private List<CapModel> GetCapItemList(long ID)
        {
            //対応IDを指定し、前回の検索条件で検索
            this.SearchModel.項目_ID = ID;
            this.SearchModel.FLAG_最新 = 1;
            return this.GetCapItemList(this.SearchModel);
        }

        /// <summary>
        /// CAP課題一覧取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<CapModel> GetCapItemList(CapSearchModel cond)
        {
            // 閲覧可のもののみに絞る
            return new CapDB().Get(cond).FindAll((x) => factory.Auth.IsPermission(x.PERMIT_FLG));
        }
        #endregion

        #region CAPで課（専門部門）が使用している開発符号の閲覧権限リスト取得
        /// <summary>
        /// CAPで課（専門部門）が使用している開発符号の閲覧権限リスト取得
        /// </summary>
        /// <returns></returns>
        private List<CapSectionUseGeneralCodeOutModel> GetSectionUseGeneralCodeList()
        {
            //APIで取得
            var res = HttpUtil.GetResponse<CapSectionUseGeneralCodeInModel, CapSectionUseGeneralCodeOutModel>(
                                                                                ControllerType.CapSectionUseGeneralCode,
                                                                                new CapSectionUseGeneralCodeInModel() { PERSONEL_ID = SessionDto.UserId });

            //レスポンスが取得できたかどうか
            var list = new List<CapSectionUseGeneralCodeOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            //返却
            return list;
        }
        #endregion

        #region 検索のチェック
        /// <summary>
        /// 検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsCapSchedule()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = FromNullableDateTimePicker.SelectedDate;
            var end = ToNullableDateTimePicker.SelectedDate;

            if (start != null || end != null)
            {
                //回答期限の大小チェック
                map[this.ToNullableDateTimePicker] = (c, name) =>
                {
                    var errMsg = "";

                    //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                    if (start != null && end != null && start > end)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.FromNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.ToNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                    }

                    return errMsg;
                };
            }

            // 車種
            map[GeneralCodeComboBox] = (c, name) =>
            {
                var errMsg = "";

                if (string.IsNullOrEmpty(GeneralCodeComboBox.Text) && string.IsNullOrEmpty(SectionComboBox.Text))
                {
                    FormControlUtil.SetComboBoxBackColor(GeneralCodeComboBox, Const.ErrorBackColor);
                    FormControlUtil.SetComboBoxBackColor(SectionComboBox, Const.ErrorBackColor);

                    errMsg = string.Format(Resources.KKM00001, "車種または専門部署名").Replace("入力", "選択");
                }
                else if (string.IsNullOrEmpty(GeneralCodeComboBox.Text) && IsSectionCodeAllSerach)
                {
                    FormControlUtil.SetComboBoxBackColor(GeneralCodeComboBox, Const.ErrorBackColor);

                    errMsg = string.Format(Resources.KKM00001, "車種").Replace("入力", "選択");
                }
                else if (IsGeneralCodeAllSerach && IsSectionCodeAllSerach)
                {
                    FormControlUtil.SetComboBoxBackColor(GeneralCodeComboBox, Const.ErrorBackColor);
                    FormControlUtil.SetComboBoxBackColor(SectionComboBox, Const.ErrorBackColor);

                    errMsg = string.Format(Resources.KKM00001, "車種または専門部署名").Replace("入力", "20件以下で選択");
                }

                return errMsg;
            };

            // 専門部門名
            map[SectionComboBox] = (c, name) =>
            {
                var errMsg = "";

                if (string.IsNullOrEmpty(SectionComboBox.Text) && IsGeneralCodeAllSerach)
                {
                    FormControlUtil.SetComboBoxBackColor(SectionComboBox, Const.ErrorBackColor);

                    errMsg = string.Format(Resources.KKM00001, "専門部署名").Replace("入力", "選択");
                }

                return errMsg;
            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            return true;
        }
        #endregion

        #region メッセージ表示
        /// <summary>
        /// メッセージ表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        private void SetMessageLabel(string message, Color color)
        {
            MessageLabel.Text = message;
            MessageLabel.ForeColor = color;
        }
        #endregion

        #region 行高さ調整
        /// <summary>
        /// 行高さ調整
        /// </summary>
        private void AdjustmentRowsVertical()
        {
            foreach (var row in this.CapMultiRow.Rows)
            {
                row.Cells["対策案"].PerformVerticalAutoFit();
            }

            // 全展開フラグ
            this.IsAllAutoFit = true;
        }
        #endregion

        #region AD情報のセット
        /// <summary>
        /// AD情報のセット
        /// </summary>
        /// <param name="row"></param>
        private void SetADUserInfo(Row row)
        {
            if (ADUserDictionary == null)
            {
                return;
            }

            if (row.Cells["編集者_ID"].Value == null)
            {
                return;
            }

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
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //登録処理
            //Update Start 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加
            //AskAndUpdate();
            var result = AskAndUpdate();
            if (!result)
            {
                e.Cancel = true;
            }
            //Update End 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加
        }
        #endregion

        #region 車種(開発符号)コンボボックスクリック
        /// <summary>
        /// 車種(開発符号)コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeComboBox_Click(object sender, EventArgs e)
        {
            using (var form = new MultiSelectGeneralCodeForm() { GeneralCodes = string.IsNullOrEmpty(GeneralCodeComboBox.Text) ? null : GeneralCodeComboBox.Text.Split(',').ToList(), DIFF_DATA = 1, FunctionId = FunctionID.CAP, IsOtherSelectedAll = IsSectionCodeAllSerach })
            {
                //開発符号検索
                if (form.ShowDialog().Equals(DialogResult.OK))
                {
                    // 閲覧権限チェック
                    if (!form.GModels.All((x) => factory.Auth.IsPermission(x.PERMIT_FLG)))
                    {
                        Messenger.Info(Resources.KKM02009);
                        return;
                    }

                    //コンボボックス設定
                    GeneralCodeComboBox.Items.Clear();
                    GeneralCodeComboBox.Items.Add(string.Join(",", form.GeneralCodes));
                    GeneralCodeComboBox.SelectedIndex = 0;

                    // 全選択フラグ(開発符号)
                    IsGeneralCodeAllSerach = form.IsSelectedAll;

                    // 検索実行
                    FormControlUtil.FormWait(this, () =>
                    {
                        // 変更確認
                        if (AskAndUpdate())
                        {
                            // 設定
                            this.SearchCapList();
                        }
                    });
                }
            }
        }
        #endregion

        #region 専門部署(課)コンボボックスクリック
        /// <summary>
        /// 専門部署(課)コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionComboBox_Click(object sender, EventArgs e)
        {
            //課
            using (var form = new MultiSelectSectionForm { Sections = SectionComboBox.Text, FunctionId = FunctionID.CAP, IsOtherSelectedAll = IsGeneralCodeAllSerach })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var kaList = (form.Sections ?? "").Split(',').Select(x => x.Trim()).Where(x => string.IsNullOrWhiteSpace(x) == false).ToArray();
                    var isSelectedGeneralCode = !string.IsNullOrWhiteSpace(this.GeneralCodeComboBox.Text);

                    if (!isSelectedGeneralCode && !kaList.All((x) => factory.Auth.IsPermission(x)))
                    {
                        Messenger.Info(Resources.KKM02010);
                    }

                    //選択された課をカンマ区切りで表示
                    SectionComboBox.Items.Clear();
                    SectionComboBox.Items.Add(string.Join(",", kaList));
                    SectionComboBox.SelectedIndex = 0;

                    // 全選択フラグ(課)
                    IsSectionCodeAllSerach = form.IsSelectedAll;

                    RelationSectionCheckBox.Enabled = form.IsSelectedAll || kaList.Count() <= 0 ? false : true;

                    // 検索実行
                    FormControlUtil.FormWait(this, () =>
                    {
                        // 変更確認
                        if (AskAndUpdate())
                        {
                            // 設定
                            this.SearchCapList();
                        }
                    });
                }
            }
        }
        #endregion

        #region Excel出力（自部）
        /// <summary>
        /// Excel出力（自部）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentDownloadButton_Click(object sender, EventArgs e)
        {
            DownloadExcel(SessionDto.DepartmentID, null);
        }
        #endregion

        #region Excel出力（自課）
        /// <summary>
        /// Excel出力（自課）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionDownloadButton_Click(object sender, EventArgs e)
        {
            DownloadExcel(null, SessionDto.SectionID);
        }
        #endregion

        #region Excel出力
        /// <summary>
        /// Excel出力
        /// </summary>
        /// <param name="department"></param>
        /// <param name="section"></param>
        private void DownloadExcel(string department, string section)
        {
            //該当する課名を取得
            List<SectionModel> sectionList = new Section().Search(department, section);
            var sectionIDList = new List<String>();
            foreach (SectionModel item in sectionList)
            {
                sectionIDList.Add(item.SECTION_CODE);
            }

            List<Row> csvList = new List<Row>();
            //Append Start 2021/02/15 杉浦 
            List<string> csvItem = new List<string>();
            foreach (var row in this.CapMultiRow.Rows)
            {
                //該当する部、課のみ出力
                if (row.Cells["専門部署名"].Value != null &&
                     sectionIDList.Contains(row.Cells["専門部署名"].Value.ToString()) == true)
                {
                    if(csvItem.Contains(row.Cells["No"].Value.ToString()) == false)
                    {
                        csvItem.Add(row.Cells["No"].Value.ToString());
                    }
                }
            }
            //Append End 2021/02/15 杉浦 

            foreach (var row in this.CapMultiRow.Rows)
            {
                //Update Start 2021/02/15 杉浦 
                ////該当する部、課のみ出力
                //if (row.Cells["専門部署名"].Value != null &&
                //     sectionIDList.Contains(row.Cells["専門部署名"].Value.ToString()) == true)
                if (csvItem.Contains(row.Cells["No"].Value.ToString()) == true)
                //Update End 2021/02/15 杉浦 
                {
                    if (row.Cells["完了日程コンボボックス"].Value != null)
                    {
                        row.Cells["完了日程CSV"].Value = row.Cells["完了日程コンボボックス"].Value;
                    }
                    else if (row.Cells["完了日程"].Value != null)
                    {
                        row.Cells["完了日程CSV"].Value = row.Cells["完了日程"].Value;
                    }
                    else
                    {
                        row.Cells["完了日程CSV"].Value = null;
                    }

                    if (row.Cells["出図日程コンボボックス"].Value != null)
                    {
                        row.Cells["出図日程CSV"].Value = row.Cells["出図日程コンボボックス"].Value;
                    }
                    else if (row.Cells["出図日程"].Value != null)
                    {
                        row.Cells["出図日程CSV"].Value = row.Cells["出図日程"].Value;
                    }
                    else
                    {
                        row.Cells["出図日程CSV"].Value = null;
                    }

                    csvList.Add(row);
                }
            }

            if (csvList.Count == 0)
            {
                Messenger.Warn(Resources.TCM03008);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "Excel ブック (*.xlsx)|*.xlsx;", FileName = string.Format("{0}_{1:yyyyMMddHHmmss}", this.FormTitle, DateTime.Now) })
            {
                //保存先が選択されたかどうか
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var map = new Dictionary<int, string>();

                    foreach (var header in CsvHeaders)
                    {
                        var i = 0;

                        foreach (var col in this.CapMultiRow.Columns)
                        {
                            //対象の列なら追加
                            if (col.Name == header)
                            {
                                map[i] = header;
                                break;
                            }

                            i++;
                        }
                    }

                    //CSV出力用カラムのカラム名から"CSV"を削除
                    string[] outHeaders = (string[])CsvHeaders.Clone();
                    for (int i = 0; i < outHeaders.Length; i++)
                    {
                        outHeaders[i] = outHeaders[i].Replace("CSV", "");
                    }

                    // Excel出力
                    using (var xls = new XlsUtil())
                    {
                        var sheetName = "Sheet1";

                        var headers = outHeaders.Select((x, i) => new { Key = string.Format("{0}1", xls.GetColumnAddress(i)), Value = x }).ToArray();

                        var lastColumn = xls.GetColumnAddress(headers.Count() - 1);

                        var headerRange = string.Format("A1:{0}1", lastColumn);

                        //ヘッダーを書き込み
                        var headerMap = headers.ToDictionary(x => x.Key, x => x.Value == "No" ? "指摘No": x.Value);
                        xls.WriteSheet(sheetName, headerMap);

                        //罫線の設定
                        xls.SetBorder(sheetName, headerRange);
                        xls.CopyRow(sheetName, headerRange, string.Format("A2:{0}{1}", lastColumn, csvList.Count() + 1));

                        //シートに書き込み
                        xls.WriteSheet(sheetName, csvList,
                            (row =>
                            {
                                var list = new List<string>();

                                foreach (var kv in map)
                                {
                                    var index = kv.Key;
                                    var header = kv.Value;
                                    var cell = row.Cells[index];
                                    var value = cell.Value;

                                    //ヘッダーごとの分岐
                                    switch (header)
                                    {
                                        case "回答期限":
                                            if (cell.EditedFormattedValue != null)
                                            {
                                                value = Convert.ToDateTime(cell.EditedFormattedValue).ToString("yyyy/MM/dd");
                                            }
                                            break;

                                        case "完了日程CSV":
                                        case "出図日程CSV":
                                            if (value == null || value.ToString() == "済")
                                            {
                                                break;
                                            }
                                            value = DateTimeUtil.ConvertDateString(Convert.ToDateTime(value));
                                            break;

                                        case "承認":
                                            value = value == null ? "未" : "済";
                                            break;

                                        default:

                                            break;
                                    }

                                    list.Add(value == null ? "" : value.ToString());
                                }

                                return list.Select((x, i) => new { Key = xls.GetColumnAddress(i), Value = x }).ToDictionary(x => x.Key, x => x.Value);
                            }), 1);

                        //ヘッダーを中寄
                        xls.SetAlignmentCenter(sheetName, string.Format("A1:{0}1", lastColumn));

                        //列幅の自動調整
                        xls.AutoSizeColumn(sheetName, string.Format("A:{0}", lastColumn));

                        // フッターの設定
                        xls.SetFooter(sheetName, string.Format("社外転用禁止\n{0} {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), SessionDto.SectionCode, SessionDto.UserName));

                        //ブックの保存
                        xls.Save(sfd.FileName);
                    }
                }
            }
        }
        #endregion

        #region 文字色 黒 ⇔ 赤ボタン押下
        /// <summary>
        /// 文字色 黒 ⇔ 赤ボタン押下
        /// </summary>
        private void ForeColorButton_Click()
        {
            if (this.factory.ForeColor.SetCellForBtn(this.CapMultiRow.CurrentCell))
            {
                // 変更フラグを設定
                this.CapMultiRow.Rows[this.CapMultiRow.CurrentCell.RowIndex].Cells["変更"].Value = "1";

                if (this.CapMultiRow.CurrentCell.Name == "対策案")
                {
                    // 対策案編集フラグを設定
                    this.CapMultiRow.Rows[this.CapMultiRow.CurrentCell.RowIndex].Cells["対策案変更"].Value = "1";
                }

                // 変更フラグ設定
                this.IsEdit = true;
            }
        }
        #endregion

        #region 行追加ボタン押下
        /// <summary>
        /// 行追加ボタン押下
        /// </summary>
        private void AddButton_Click()
        {
            FormControlUtil.FormWait(this, () =>
            {
                //行番号
                var rowIndex = this.factory.SelectedCell.GetCell().RowIndex;

                //行追加
                var itemID = Convert.ToInt64(CapMultiRow.Rows[rowIndex].Cells["項目_ID"].Value);
                var sameItem = this.CapItemList.First((x) => x.項目_ID == itemID);

                var model = new CapModel()
                {
                    ID = tempId--,
                    項目_ID = itemID,
                    FLAG_CLOSE = sameItem.FLAG_CLOSE,
                    NO = sameItem.NO,
                    CAP種別 = sameItem.CAP種別,
                    重要度 = sameItem.重要度,
                    項目 = sameItem.項目,
                    詳細 = sameItem.詳細,
                    評価車両 = sameItem.評価車両,
                    仕向地 = sameItem.仕向地,
                    CAP確認結果 = sameItem.CAP確認結果,
                    //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    N_DRIVE_LINK = sameItem.N_DRIVE_LINK,
                    //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    フォロー状況 = sameItem.フォロー状況,
                    織込時期_項目 = sameItem.織込時期_項目,
                    GENERAL_CODE = sameItem.GENERAL_CODE,
                    PERMIT_FLG = sameItem.PERMIT_FLG,
                    編集者_ID = SessionDto.UserId,
                    FLAG_最新 = 1,
                    変更 = "1",
                };

                var nextRowIndex = rowIndex + 1;

                // 選択解除
                this.CapMultiRow.ClearSelection();

                // 行追加
                this.bindingSource.Insert(nextRowIndex, model);

                // 追加行選択
                this.CapMultiRow.Rows[nextRowIndex].Selected = true;

                // 変更フラグ設定
                this.IsEdit = true;
            });
        }
        #endregion

        #region 行削除ボタン押下
        /// <summary>
        /// 行削除ボタン押下
        /// </summary>
        private void DeleteButton_Click()
        {
            FormControlUtil.FormWait(this, () =>
            {
                var capDB = new CapDB();
                var deleteList = new List<CapModel>();
                var rowIndex = -1;

                foreach (var cell in CapMultiRow.SelectedCells)
                {
                    if (rowIndex == cell.RowIndex)
                    {
                        continue;
                    }

                    rowIndex = cell.RowIndex;

                    var row = CapMultiRow.Rows[rowIndex];

                    // 対象ID
                    var data = new CapModel()
                    {
                        ID = Convert.ToInt64(row.Cells["ID"].Value),
                        GENERAL_CODE = Convert.ToString(row.Cells["車種"].Value),
                        NO = Convert.ToInt64(row.Cells["No"].Value)
                    };

                    // 履歴チェック
                    if (data.ID > 0)
                    {
                        // 不正データは削除を行わない
                        if (string.IsNullOrEmpty(data.GENERAL_CODE) || data.NO == 0)
                        {
                            continue;
                        }

                        var list = capDB.Get(new CapSearchModel { ID = data.ID, PERSONEL_ID = SessionDto.UserId });

                        // 履歴がある場合は、メッセージを表示して以下を行わない
                        if (list.Count > 0 && 1 < Convert.ToInt64(list[0].過去履歴件数))
                        {
                            Messenger.Warn(Resources.KKM03029);
                            return;
                        }
                    }

                    deleteList.Add(data);
                }

                // 確認
                if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
                {
                    return;
                }

                // 削除
                if (deleteList.Count > 0)
                {
                    if (deleteList.Any(x => x.ID > 0))
                    {
                        // DBから削除
                        if (!capDB.Delete(deleteList.Where(x => x.ID > 0).ToList()))
                        {
                            return;
                        }
                    }

                    // 画面から削除
                    this.CapItemList.RemoveAll(x => deleteList.Any(y => y.ID == x.ID));
                    this.bindingSource.ResetBindings(false);

                    //削除メッセージ表示
                    Messenger.Info(Resources.KKM00003);
                }
            });
        }
        #endregion

        #region インポートボタン押下
        /// <summary>
        /// インポートボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_Click(object sender, EventArgs e)
        {
            new Import(this).Run();
        }
        #endregion

        #region エクスポートボタン押下
        /// <summary>
        /// エクスポートボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            new Export(this, CapMultiRow).Run();
        }
        #endregion

        #region 行調整リンクラベルクリック
        /// <summary>
        /// 行調整リンクラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoFitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 行高さ調整
            FormControlUtil.FormWait(this, AdjustmentRowsVertical);
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
            FormControlUtil.FormWait(this, () =>
            {
                //登録処理
                if (UpdateCapItem())
                {
                    //正常終了メッセージ
                    Messenger.Info(Resources.KKM00002);

                    // 検索のチェック
                    if (this.IsCapSchedule() == false)
                    {
                        return;
                    }

                    // 設定
                    this.SearchCapList(true);
                }
            });
        }
        #endregion

        #region 登録処理

        /// <summary>
        /// 変更の登録確認、及び登録処理（true：登録実行、false：変更がない・登録しない、・キャンセルを選択）
        /// 単発処理用
        /// </summary>
        /// <returns></returns>
        private bool IsUpdate()
        {
            //Update Start 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加
            //if (this.IsEdit && Messenger.ConfirmYesNoCancel(Resources.KKM00006) == DialogResult.Yes)
            //{
            //  EntryButton_Click(null, null);
            //  return true;
            //}
            if (this.IsEdit)
            {
                var dialogResult = Messenger.ConfirmYesNoCancel(Resources.KKM00006);
                if (dialogResult == DialogResult.Yes)
                {
                    EntryButton_Click(null, null);
                    return true;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }
            //Update End 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加

            return false;
        }

        /// <summary>
        /// 変更の登録確認と登録（true：変更なし・確認No・正常登録、false：登録失敗、キャンセルを選択）
        /// 後続処理続行用
        /// </summary>
        /// <returns></returns>
        private bool AskAndUpdate()
        {
            //Update Start 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加
            //if (this.IsEdit && Messenger.ConfirmYesNoCancel(Resources.KKM00006) == DialogResult.Yes)
            //{
            //    return UpdateCapItem();
            //}

            if (this.IsEdit)
            {
                var dialogResult = Messenger.ConfirmYesNoCancel(Resources.KKM00006);
                if (dialogResult == DialogResult.Yes)
                {
                    EntryButton_Click(null, null);
                    return true;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }
            //Update End 2021/05/14 杉浦 CAP_登録確認子画面に「キャンセル」を追加

            return true;
        }

        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns></returns>
        private bool EntryCheck()
        {
            // グリッド存在確認
            if (CapMultiRow.Rows.Count <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <returns></returns>
        private bool UpdateCapItem()
        {
            // 登録チェック
            if (EntryCheck() == false)
            {
                return false;
            }

            var ids = new List<long>();
            var list = new List<CapModel>();

            var db = new CapDB();

            foreach (var row in CapMultiRow.Rows)
            {
                // 編集がない場合は未処理
                if (row.Cells["変更"].Value == null || row.Cells["変更"].Value.ToString() != "1")
                {
                    continue;
                }

                var cap = this.CapItemList.FirstOrDefault(x => x.ID == Convert.ToInt64(row.Cells["ID"].Value));

                //完了日程、出図日程を編集
                if (row.Cells["完了日程コンボボックス"].Value != null &&
                    row.Cells["完了日程コンボボックス"].Value.ToString() == ConstFinish)
                {
                    cap.完了日程 = ConstDate;
                }
                if (row.Cells["出図日程コンボボックス"].Value != null &&
                    row.Cells["出図日程コンボボックス"].Value.ToString() == ConstFinish)
                {
                    cap.出図日程 = ConstDate;
                }

                //更新用リストに追加(最新)
                list.Add(cap);

                //履歴データの作成（新規追加以外、対策案変更の場合）
                if (cap.ID > 0 && ids.Contains(cap.項目_ID) == false && row.Cells["対策案変更"].Value?.ToString() == "1")
                {
                    //前回の検索条件に項目IDを追加して検索
                    var history = GetCapItemList(cap.項目_ID);

                    //FLAG_最新をOFF(0)にして追加登録
                    history.ForEach(x => x.FLAG_最新 = 0);

                    //登録
                    db.Post(history.ToList());

                    //処理済みの項目IDを保存
                    ids.Add(cap.項目_ID);
                }
            }

            if (list.Count <= 0)
            {
                return false;
            }

            return db.Put(list);
        }
        #endregion

        #region 重要度コンボボックスクリック
        /// <summary>
        /// 重要度コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportanceComboBox_Click(object sender, EventArgs e)
        {
            var list = (new[] { new CapImportanceModel() }).Concat(HttpUtil.GetResponse<CapImportanceModel>(ControllerType.CapImportance)?.Results).ToList();
            using (var form = new DataGridForm { TITLE = "重要度", DATASOURCE = list })
            {
                DialogResult result = form.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ImportanceComboBox.Items.Clear();
                    ImportanceComboBox.Items.Add(form.RETURNVALUE.Cells[0].Value == null ? "" : form.RETURNVALUE.Cells[0].Value.ToString());
                    ImportanceComboBox.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region 全チェックON/OFF処理
        /// <summary>
        /// 全チェックON/OFF処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                CapMultiRow.ClearSelection();

                // 表示されている行のみチェック設定する
                CapMultiRow.Rows
                .Where((x) =>
                    CapMultiRow.Rows.GetRowState(x.Index) == (MultiRowElementStates.Displayed | MultiRowElementStates.Visible) ||
                    CapMultiRow.Rows.GetRowState(x.Index) == MultiRowElementStates.Visible)
                .ToList()
                .ForEach((x) => x.Cells["CAP確認"].Value = Convert.ToInt16(CheckBoxAll.Checked));
            });
        }
        #endregion

        #region MultiRowイベント

        #region 不正値エラー検知時
        /// <summary>
        /// 不正値エラー検知時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapDataGridView_DataError(object sender, DataErrorEventArgs e)
        {
            if (e.CellName == "種別" ||
                e.CellName == "仕向地" ||
                e.CellName == "対策予定" ||
                e.CellName == "事前把握" ||
                e.CellName == "供試品" ||
                e.CellName == "織込時期" ||
                e.CellName == "フォロー状況" ||
                e.CellName == "CAP確認時期" ||
                e.CellName == "CAP確認" ||
                e.CellName == "承認" ||
                e.CellName == "ステータス")
            {
                //例外無視
                e.Cancel = false;
            }
        }
        #endregion

        #region セル描画時
        /// <summary>
        /// セル描画時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapDataGridView_CellPainting(object sender, CellPaintingEventArgs e)
        {
            // 無効な行か列の場合は終了
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
            if (this.CapMultiRow.Columns[e.CellIndex].Name == "評価車両詳細")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Left = new Line(LineStyle.None, Color.White);
            }
            if (this.CapMultiRow.Columns[e.CellIndex].Name == "評価車両")
            {
                Border b = (Border)e.CellStyle.Border;
                b.Right = new Line(LineStyle.None, Color.White);
            }

            // CAP確認列ヘッダーのみ処理を行う
            if ((e.CellName == "ch_CAP確認") && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - CheckBoxAll.Width) / 2, (bmp.Height - CheckBoxAll.Height) / 9 * 7);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    CheckBoxAll.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = Convert.ToInt32((e.CellBounds.Height - bmp.Height) / 2 * new DeviceUtil().GetScalingFactor());

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region セルクリック
        /// <summary>
        /// セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellClick(object sender, CellEventArgs e)
        {
            // 全選択チェックボックスクリックの場合
            if ((e.CellName == "ch_CAP確認") && (e.RowIndex == -1) && factory.Auth.IsCan("CAP確認", "", ""))
            {
                // 選択チェックボックスの表示を更新する
                this.CheckBoxAll.Checked = !this.CheckBoxAll.Checked;
            }

            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = ((GcMultiRow)sender).Rows[e.RowIndex];
            var cell = row.Cells[e.CellIndex];

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                // 高さ自動調整
                row.Cells["対策案"].PerformVerticalAutoFit();
            }
        }
        #endregion

        #region マウスキーダウンイベント
        /// <summary>
        /// マウスキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            // 通常の左クリック以外受け付けない
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // 承認チェックボックス処理
            factory.ColApproval.Judge(e.RowIndex, e.CellName);

            // 編集モード中なら終了
            if (this.CapMultiRow.IsCurrentCellInEditMode)
            {
                return;
            }

            // 各ボタン処理
            FormControlUtil.FormWait(this, () => this.factory.GetBtnInstance(e.CellName)?.Action());
        }
        #endregion

        #region セル値変更
        /// <summary>
        /// セル値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapDataGridView_CellValueChanged(object sender, CellEventArgs e)
        {
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = CapMultiRow.Rows[e.RowIndex];

            if (factory.Auth.IsCan(e.CellName, Convert.ToString(row.Cells["専門部名"].Value), Convert.ToString(row.Cells["専門部署名"].Value)) == false)
            {
                return;
            }

            // バインド中フラグON
            this.IsBind = true;

            try
            {
                // 完了日程
                if (e.CellName == "完了日程" && row.Cells["完了日程"].Value != null)
                {
                    row.Cells["完了日程コンボボックス"].Value = null;
                }
                if (e.CellName == "完了日程コンボボックス" && row.Cells["完了日程コンボボックス"].Value != null)
                {
                    row.Cells["完了日程"].Value = null;
                }

                // 出図日程
                if (e.CellName == "出図日程" && row.Cells["出図日程"].Value != null)
                {
                    row.Cells["出図日程コンボボックス"].Value = null;
                }
                if (e.CellName == "出図日程コンボボックス" && row.Cells["出図日程コンボボックス"].Value != null)
                {
                    row.Cells["出図日程"].Value = null;
                }

                // 回答期限日・回答期限設定日
                if (e.CellName == "回答期限")
                {
                    row.Cells["回答期限設定日"].Value = DateTime.Now;

                    // 背景色を変更
                    this.factory.Status.SetRow(e.RowIndex);

                    // 自動セット対象（表示行・CAP確認済み・OPERN・回答期限日未入力）
                    var targets = CapMultiRow.Rows.Where(x => x.Visible == true && Convert.ToBoolean(x.Cells["CAP確認"].Value) == true && Convert.ToInt32(x.Cells["ステータス"].Value) == 0 && x.Cells[e.CellIndex].Value == null);

                    // 更新確認
                    if (!IsEditAnswerLimit && targets.Any() && Messenger.Confirm(Resources.KKM02013) == DialogResult.Yes)
                    {
                        // 背景色を黄色に
                        //Update Start 2020/12/24 杉浦
                        //row.Cells[e.CellIndex].Style.BackColor = Color.Yellow;
                        row.Cells[e.CellIndex].Style.BackColor = Color.Red;
                        //Update End 2020/12/24 杉浦

                        // 回答期限自動セット
                        foreach (var common in targets)
                        {
                            // 同一行は未処理
                            if (common.Index == e.RowIndex) continue;

                            // 未設定の場合は同一日をセット
                            common.Cells[e.CellIndex].Value = row.Cells[e.CellIndex].Value;
                            common.Cells["回答期限設定日"].Value = DateTime.Now;

                            // 背景色を変更
                            this.factory.Status.SetRow(common.Index);

                            // 変更セル設定
                            common.Cells["変更"].Value = "1";

                            // 背景色を黄色に
                            //Update Start 2020/12/24 杉浦
                            //common.Cells[e.CellIndex].Style.BackColor = Color.Yellow;
                            common.Cells[e.CellIndex].Style.BackColor = Color.Red;
                            //Update End 2020/12/24 杉浦
                        }

                        IsEditAnswerLimit = true;
                    }
                }

                // 対策案編集者
                if (e.CellName == "対策案")
                {
                    //Delete Start 2021/04/19 杉浦 背景色を戻す処理の追加
                    //row.Cells["対策案編集日"].Value = DateTime.Now;
                    //row.Cells["対策案編集者_ID"].Value = SessionDto.UserId;
                    //row.Cells["対策案編集者"].Value = SessionDto.UserName;
                    //row.Cells["対策案変更"].Value = "1";
                    //Delete End 2021/04/19 杉浦 背景色を戻す処理の追加
                    //Update Start 2021/04/19 杉浦 背景色を戻す処理の追加
                    //row.Cells["対策案"].Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                    //row.Cells["専門部署名"].Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                    var answerLimitValue = row.Cells["回答期限"].Value;
                    Boolean dateCheck = answerLimitValue == null ? false : (DateTime)answerLimitValue < DateTime.Now;
                    if (string.IsNullOrEmpty((string)row.Cells["対策案"].Value) && dateCheck)
                    {
                        row.Cells["対策案編集日"].Value = null;
                        row.Cells["対策案"].Style.BackColor = Color.Red;
                        row.Cells["専門部署名"].Style.BackColor = Color.Red;
                        row.Cells["対策案編集者_ID"].Value = null;
                        row.Cells["対策案編集者"].Value = null;
                        row.Cells["対策案変更"].Value = "0";
                    }
                    else
                    {
                        row.Cells["対策案編集日"].Value = DateTime.Now;
                        row.Cells["対策案"].Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                        row.Cells["専門部署名"].Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                        row.Cells["対策案編集者_ID"].Value = SessionDto.UserId;
                        row.Cells["対策案編集者"].Value = SessionDto.UserName;
                        row.Cells["対策案変更"].Value = "1";
                    }
                    //Update End 2021/04/19 杉浦 背景色を戻す処理の追加
                }

                // CAP確認時期
                if (e.CellName == "CAP確認時期")
                {
                    if ((string)row.Cells[e.CellIndex].Value == "ｸﾛｰｽﾞ")
                    {
                        row.Cells["ステータス"].Value = (short?)1;
                    }
                }

                // 編集情報関連
                if (EditInfoTargetCells.Contains(e.CellName))
                {
                    //Update Start 2021/04/19 杉浦
                    ////編集日
                    //row.Cells["編集日"].Value = DateTime.Now;

                    ////編集者_ID
                    //row.Cells["編集者_ID"].Value = SessionDto.UserId;

                    ////編集者
                    //row.Cells["編集者"].Value = SessionDto.UserName;
                    var answerLimitValue = row.Cells["回答期限"].Value;
                    var id = (long)row.Cells["ID"].Value;
                    var editor = this.CapItemList.Where(x => x.ID == id).Select(x => new { x.BK_編集日, x.BK_編集者_ID, x.BK_編集者 }).ToList();
                    Boolean dateCheck = answerLimitValue == null ? false : (DateTime)answerLimitValue < DateTime.Now;
                    if (string.IsNullOrEmpty((string)row.Cells["対策案"].Value) && string.IsNullOrEmpty((string)row.Cells["専門部署名"].Value) && dateCheck)
                    {
                        //編集日
                        row.Cells["編集日"].Value = editor[0].BK_編集日;

                        //編集者_ID
                        row.Cells["編集者_ID"].Value = editor[0].BK_編集者_ID;

                        //編集者
                        row.Cells["編集者"].Value = editor[0].BK_編集者;
                    }else
                    {
                        //編集日
                        row.Cells["編集日"].Value = DateTime.Now;

                        //編集者_ID
                        row.Cells["編集者_ID"].Value = SessionDto.UserId;

                        //編集者
                        row.Cells["編集者"].Value = SessionDto.UserName;
                    }

                    //内線番号
                    SetADUserInfo(row);
                }

                //承認
                if (e.CellName == "承認")
                {
                    if (row.Cells[e.CellIndex].Value == null)
                    {
                        //OFF

                        //承認日
                        row.Cells["承認日"].Value = null;

                        //承認者_ID
                        row.Cells["承認者_ID"].Value = null;

                        //承認者
                        row.Cells["承認者"].Value = null;
                    }
                    else
                    {
                        //ON

                        //承認日
                        row.Cells["承認日"].Value = DateTime.Now;

                        //承認者_ID
                        row.Cells["承認者_ID"].Value = SessionDto.UserId;

                        //承認者
                        row.Cells["承認者"].Value = SessionDto.UserName;
                    }
                }

                // 承認以外の列を管理権限のない・職制でないユーザが更新した場合は承認をOFFにする
                if (e.CellName != "承認" && factory.Auth.IsAdmin() == false && factory.Auth.IsJobSys() == false)
                {
                    if (row.Cells["承認"].Value != null)
                    {
                        row.Cells["承認"].Value = null;
                    }
                }

                //「CAP項目」が更新された場合、同じ項目IDの情報も更新
                if (ItemHeaders.Contains(e.CellName) == true)
                {
                    foreach (var common in CapMultiRow.Rows.Where(x => x.Cells["項目_ID"].Value.Equals(row.Cells["項目_ID"].Value)))
                    {
                        common.Cells[e.CellIndex].Value = row.Cells[e.CellIndex].Value;

                        if (e.CellName == "種別")
                        {
                            // 文字色を変更
                            this.factory.ForeColor.SetRow(common.Index);
                        }

                        if (e.CellName == "ステータス")
                        {
                            // 背景色を変更
                            this.factory.Status.SetRow(common.Index);
                        }

                        if (e.CellName == "CAP確認時期")
                        {
                            if ((string)common.Cells[e.CellIndex].Value == "ｸﾛｰｽﾞ")
                            {
                                // ステータスをクローズに変更
                                common.Cells["ステータス"].Value = (short?)1;

                                // 背景色を変更
                                this.factory.Status.SetRow(common.Index);
                            }
                        }

                        //変更セル設定
                        common.Cells["変更"].Value = "1";

                        // 高さ自動調整
                        common.Cells["対策案"].PerformVerticalAutoFit();
                    }
                }


                //Append Start 2021/06/21 矢作

                // フォロー状況・方向付け確定期限に応じてセルの色を変更
                if (e.CellName == "フォロー状況" || e.CellName == "方向付け確定期限")
                {
                    string no = row.Cells["No"].Value != null ? row.Cells["No"].Value.ToString() : "";
                    string follow = row.Cells["フォロー状況"].Value != null ? row.Cells["フォロー状況"].Value.ToString() : "";
                    DateTime redDatetime = DateTime.Now.AddMonths(1);

                    //NOよりIDリストを作成
                    var idList = new List<string>();
                    foreach (var i in CapItemList.Where(x => x.NO.ToString() == no))
                    {
                        idList.Add(i.ID.ToString());
                    }

                    foreach (var id in idList)
                    {
                        if (CapItemList.FirstOrDefault(x => x.ID.ToString() == id).方向付け確定期限 <= redDatetime && redList.Contains(follow))
                        {
                            this.CapMultiRow.Rows.FirstOrDefault(x => (x.Cells["ID"].Value).ToString() == id).Cells["専門部署名"].Style.BackColor = Color.Yellow;
                            this.CapMultiRow.Rows.FirstOrDefault(x => (x.Cells["ID"].Value).ToString() == id).Cells["対策案"].Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            this.CapMultiRow.Rows.FirstOrDefault(x => (x.Cells["ID"].Value).ToString() == id).Cells["専門部署名"].Style.BackColor = Color.Beige;
                            this.CapMultiRow.Rows.FirstOrDefault(x => (x.Cells["ID"].Value).ToString() == id).Cells["対策案"].Style.BackColor = Color.Beige;
                        }
                    }
                }
                //Append End 2021/06/21 矢作

                //変更セル設定
                row.Cells["変更"].Value = "1";
            }
            finally
            {
                //変更フラグ設定
                this.IsEdit = true;

                // バインド中フラグOFF
                this.IsBind = false;
            }
        }
        #endregion

        #region チェックボックスセル変更時
        /// <summary>
        /// チェックボックスセル変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            // チェックボックスセルを変更すると内部バリデーションで値エラーが起こるのでエラーを無効化して値を強制設定する
            if (e.CellName == "CAP確認" || e.CellName == "承認")
            {
                if (Convert.ToBoolean(this.CapMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].EditedFormattedValue) == true)
                {
                    this.CapMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = (short?)1;
                }
                else
                {
                    this.CapMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = null;
                }
            }
        }
        #endregion

        #region セル結合時
        /// <summary>
        /// セル結合時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_QueryCellMergeState(object sender, QueryCellMergeStateEventArgs e)
        {
            if (e.ShouldMerge == true)
            {
                var itemIdName = "項目_ID";

                if (e.QueryCell.CellName != itemIdName)
                {
                    var newQueryValue = this.CapMultiRow.Rows[e.QueryCell.RowIndex].Cells[itemIdName].Value;
                    var newTargetValue = this.CapMultiRow.Rows[e.TargetCell.RowIndex].Cells[itemIdName].Value;

                    // 項目IDが同じ場合セル結合させる
                    e.ShouldMerge = newQueryValue?.ToString() == newTargetValue?.ToString();
                }
            }
        }
        #endregion

        #endregion

        #region CAP課題のDBを操作するクラス
        /// <summary>
        /// CAP課題のDBを操作するクラス
        /// </summary>
        private class CapDB
        {
            /// <summary>
            /// 取得
            /// </summary>
            /// <param name="cond"></param>
            /// <returns></returns>
            public List<CapModel> Get(CapSearchModel cond)
            {
                // 必須
                cond.PERSONEL_ID = SessionDto.UserId;

                //APIで取得
                var res = HttpUtil.GetResponse<CapSearchModel, CapModel>(ControllerType.Cap, cond);

                //レスポンスが取得できたかどうか
                var list = new List<CapModel>();
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);
                }

                //返却
                return list;
            }

            /// <summary>
            /// 登録
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public bool Post(List<CapModel> list)
            {
                var res = HttpUtil.PostResponse<CapModel>(ControllerType.Cap, list);
                return res != null && res.Status == Const.StatusSuccess;
            }

            /// <summary>
            /// 更新
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public bool Put(List<CapModel> list)
            {
                var res = HttpUtil.PutResponse<CapModel>(ControllerType.Cap, list);
                return res != null && res.Status == Const.StatusSuccess;
            }

            /// <summary>
            /// 削除
            /// </summary>
            /// <param name="list">IDと開発符号とNOは必須</param>
            /// <returns></returns>
            public bool Delete(List<CapModel> list)
            {
                var res = HttpUtil.DeleteResponse<CapModel>(ControllerType.Cap, list);
                return res != null && res.Status == Const.StatusSuccess;
            }
        }
        #endregion

        #region 権限管理クラス
        /// <summary>
        /// 権限管理クラス
        /// </summary>
        private class Authority
        {
            private UserAuthorityOutModel _auth;

            /// <summary>
            /// CAPで課（専門部門）が使用している開発符号の閲覧権限リスト
            /// </summary>
            private List<CapSectionUseGeneralCodeOutModel> _sectionPermits;

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="list"></param>
            /// <param name="model"></param>
            public Authority(UserAuthorityOutModel model, List<CapSectionUseGeneralCodeOutModel> list)
            {
                this._auth = model;
                _sectionPermits = list;
            }
            #endregion

            #region その列が使用できるか？
            /// <summary>
            /// その列が使用できるか？
            /// </summary>
            /// <param name="ColName">対象列名</param>
            /// <param name="departmentCode">部コード</param>
            /// <param name="sectionCode">課コード</param>
            /// <returns></returns>
            public bool IsCan(string ColName, string departmentCode, string sectionCode)
            {
                switch (ColName)
                {
                    case "CAP確認":
                    case "回答期限":
                    case "種別":
                    case "重要度":
                    case "項目":
                    case "詳細":
                    case "評価車両":
                    case "仕向地":
                    case "CAP確認結果":
                    case "専門部署名":
                    case "フォロー状況":
                    case "CAP確認時期":
                    case "ステータス":

                        // 管理者のみ
                        return IsAdmin();

                    //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    case "N_DRIVE_LINK":
                    //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    case "対策予定":
                    case "対策案":
                    case "事前把握":
                    case "分類":
                    case "評価レベル":
                    case "完了日程コンボボックス":
                    case "完了日程":
                    case "供試品":
                    case "出図日程コンボボックス":
                    case "出図日程":
                    case "織込時期":

                        // 管理者もしくは自課
                        return IsAdmin() || SessionDto.SectionCode == sectionCode;

                    case "承認":

                        return IsCanColApproval(departmentCode, sectionCode);

                    case "RowHeader":
                    case "No":
                    case "車種":
                    case "編集日":
                    case "編集者":
                    case "内線番号":
                    case "回答期限設定日":
                    case "承認日":
                    case "承認者":
                    case "編集者_ID":
                    case "承認者_ID":
                    case "項目_ID":
                    case "ID":
                    case "親対応_ID":
                    case "過去履歴件数":
                    case "変更":
                    case "修正カラム":
                    case "完了日程CSV":
                    case "出図日程CSV":
                    case "対策案編集者_ID":
                    case "対策案編集者":
                    case "専門部名":
                    case "対策案変更":
                    case "対策案編集日":

                        // 常に読み取り専用
                        return false;

                    //Append Start 2021/07/28 矢作
                    case "方向付け確定期限":
                        //return true;
                        return (IsAdmin() || this._auth.ROLL_ID_LIST.Contains("18"));
                    //Append End 2021/07/28 矢作

                    default:
                        // それ以外はすべて使用許可
                        return true;
                }
            }
            #endregion

            #region そのコントロールが表示できるか？ 
            /// <summary>
            /// そのコントロールが表示できるか？
            /// </summary>
            /// <param name="ctl">対象コントロール</param>
            /// <returns></returns>
            public bool IsVisible(Control ctl)
            {
                // 管理者は全てOK
                if (this._auth.MANAGEMENT_FLG == '1')
                {
                    return true;
                }

                switch (ctl.Name)
                {
                    case "DepartmentDownloadButton":

                        // 自部出力権限ある場合表示
                        return this._auth.JIBU_EXPORT_FLG == '1';

                    case "SectionDownloadButton":

                        // 出力権限がある場合表示
                        return this._auth.EXPORT_FLG == '1';

                    case "EntryButton":
                    case "ForeColorButton":

                        // 更新権限がある場合表示
                        return this._auth.UPDATE_FLG == '1';

                    case "AddButton":
                    case "DeleteButton":
                    case "ImportButton":
                    case "ExportButton":

                        return false;

                    //Append Start 2021/07/28 矢作
                    case "WayInputButton":

                        // CAP担当部署または管理権限がある場合表示
                        return this._auth.ROLL_ID_LIST.Contains("18");
                    //Append End 2021/07/28 矢作

                    default:

                        // その他は常に表示
                        return true;
                }
            }
            #endregion

            #region 閲覧していいか？
            /// <summary>
            /// 閲覧していいか？
            /// </summary>
            /// <param name="permitFlg">その開発符号の閲覧権限(0:閲覧権限なし 1:閲覧権限あり)</param>
            /// <returns></returns>
            public bool IsPermission(short permitFlg)
            {
                return permitFlg == 1 || _auth.ALL_GENERAL_CODE_FLG == '1';
            }

            /// <summary>
            /// 閲覧していいか？
            /// </summary>
            /// <param name="sectionName"></param>
            /// <returns></returns>
            public bool IsPermission(string sectionName)
            {
                if (_auth.ALL_GENERAL_CODE_FLG == '1')
                {
                    return true;
                }

                var targets = _sectionPermits.Where((x) => x.専門部署名 == sectionName);

                // その課では開発符号を１件も使用していない場合は閲覧可
                if (targets.Any() == false)
                {
                    return true;
                }

                // その課の利用している開発符号が１件でも閲覧可の場合
                return targets.Any((x) => x.PERMIT_FLG == 1);
            }
            #endregion

            #region 承認チェックボックスを使用できるか？
            /// <summary>
            /// 承認チェックボックスを使用できるか？
            /// </summary>
            /// <param name="departmentCode"></param>
            /// <param name="sectionCode"></param>
            /// <returns></returns>
            public bool IsCanColApproval(string departmentCode, string sectionCode)
            {
                if (IsAdmin())
                {
                    // 職制かつ自課
                    return IsJobSys() && IsMySection(sectionCode);
                }
                else
                {
                    // 職制かつ自部
                    return IsJobSys() && IsMyDepartment(departmentCode);
                }
            }
            #endregion

            #region 管理者か？
            /// <summary>
            /// 管理者か？
            /// </summary>
            /// <returns></returns>
            public bool IsAdmin()
            {
                return this._auth.MANAGEMENT_FLG == '1';
            }
            #endregion

            #region 職制か？
            /// <summary>
            /// 職制か？
            /// </summary>
            /// <returns></returns>
            public bool IsJobSys()
            {
                return string.IsNullOrEmpty(SessionDto.AccessLevel) == false && SessionDto.AccessLevel.Substring(0, 1) == "0";
            }
            #endregion

            #region 専門部署が自部か？
            /// <summary>
            /// 専門部署が自部か？
            /// </summary>
            /// <param name="departmentCode">部コード</param>
            /// <returns></returns>
            public bool IsMyDepartment(string departmentCode)
            {
                return SessionDto.DepartmentCode == departmentCode;
            }
            #endregion

            #region 専門部署が自課か？
            /// <summary>
            /// 専門部署が自課か？
            /// </summary>
            /// <param name="sectionCode">課コード</param>
            /// <returns></returns>
            public bool IsMySection(string sectionCode)
            {
                return SessionDto.SectionCode == sectionCode;
            }
            #endregion
        }
        #endregion

        #region ステータスセルを操作するクラス
        /// <summary>
        /// ステータスセルを操作するクラス
        /// </summary>
        private class StatusController
        {
            private GcMultiRow _multiRow;
            private Authority _auth;
            private BackColorController _backColor;

            public StatusController(GcMultiRow multiRow, Authority auth, BackColorController backColor)
            {
                this._multiRow = multiRow;
                this._auth = auth;
                this._backColor = backColor;
            }

            /// <summary>
            /// ステータスに適した行に設定（背景色・編集可否）します。
            /// </summary>
            /// <param name="rowIndex">対象行インデックス</param>
            public void SetRow(int rowIndex)
            {
                var targetRow = this._multiRow.Rows[rowIndex];

                var isOpen = Convert.ToInt32(targetRow.Cells["ステータス"].Value) == 0;

                foreach (var cell in targetRow.Cells)
                {
                    if (isOpen)
                    {
                        this.SetOpen(targetRow, cell);
                    }
                    else
                    {
                        this.SetClose(cell);
                    }
                }
            }

            /// <summary>
            /// クローズセルに設定します。
            /// </summary>
            /// <param name="targetCell"></param>
            public void SetClose(Cell targetCell)
            {
                if (targetCell.Name == "RowHeader")
                {
                    return;
                }

                // 背景
                this._backColor.SetCloseColor(targetCell);

                if (targetCell.Name == "CAP確認" || targetCell.Name == "ステータス")
                {
                    return;
                }

                // Update START 2020/12/07 杉浦
                //// 編集不可
                //if (!targetCell.ReadOnly)
                //{
                //    targetCell.ReadOnly = true;
                //}

                if (!targetCell.ReadOnly)
                {
                    if (_auth.IsAdmin())
                    {
                        targetCell.ReadOnly = false;
                    }else
                    {
                        targetCell.ReadOnly = true;
                    }
                }

                // Update END 2020/12/07 杉浦
            }

            /// <summary>
            /// オープンセルに設定します。
            /// </summary>
            /// <param name="targetRow"></param>
            /// <param name="targetCell"></param>
            public void SetOpen(Row targetRow, Cell targetCell)
            {
                if (targetCell.Name == "RowHeader")
                {
                    return;
                }

                // 背景
                this._backColor.SetOpenColor(targetRow, targetCell);

                if (targetCell.Name == "CAP確認" || targetCell.Name == "ステータス")
                {
                    return;
                }

                // 編集許可（権限があるもののみ）
                targetCell.ReadOnly = _auth.IsCan(targetCell.Name, Convert.ToString(targetRow.Cells["専門部名"].Value), Convert.ToString(targetRow.Cells["専門部署名"].Value)) == false;
            }
        }
        #endregion

        #region クラス生成管理クラス
        /// <summary>
        /// クラス生成管理クラス
        /// </summary>
        private class Factory
        {
            private GcMultiRow _multiRow;
            private List<BtnCell> _btns;

            #region 公開プロパティ

            /// <summary>
            /// 権限管理クラス
            /// </summary>
            public Authority Auth { get; private set; }

            /// <summary>
            /// ステータスセルを操作するクラス
            /// </summary>
            public StatusController Status { get; private set; }

            /// <summary>
            /// 文字色制御クラス
            /// </summary>
            public ForeColorController ForeColor { get; private set; }

            /// <summary>
            /// 背景色制御クラス
            /// </summary>
            public BackColorController BackColor { get; private set; }

            /// <summary>
            /// 選択セルクラス
            /// </summary>
            public SelectedCellController SelectedCell { get; private set; }

            /// <summary>
            /// 承認列クラス
            /// </summary>
            public ColApproval ColApproval { get; private set; }

            /// <summary>
            /// コマンドボタン共通処理クラス
            /// </summary>
            public CmdBtn CmdBtn { get; private set; }

            #endregion

            /// <summary>
            /// クラス生成クラスのコンストラクタ
            /// </summary>
            /// <param name="multiRow"></param>
            /// <param name="model"></param>
            /// <param name="list"></param>
            public Factory(GcMultiRow multiRow, UserAuthorityOutModel model, List<CapSectionUseGeneralCodeOutModel> list)
            {
                _multiRow = multiRow;

                Auth = new Authority(model, list);

                _btns = new List<BtnCell>()
                {
                    //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    new BtnNDrive(_multiRow,Auth),
                    //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
                    new BtnSpecialized(_multiRow, Auth),
                    new BtnImportance(_multiRow, Auth),
                    new BtnClassify(_multiRow, Auth),
                    new BtnLevel(_multiRow, Auth),
                    new BtnHis(_multiRow, Auth),
                    new BtnEvaluationCar(_multiRow, Auth),
                };

                BackColor = new BackColorController(_multiRow);
                Status = new StatusController(_multiRow, Auth, BackColor);
                ForeColor = new ForeColorController(_multiRow, Auth);
                SelectedCell = new SelectedCellController(_multiRow);
                CmdBtn = new CmdBtn(SelectedCell);
                ColApproval = new ColApproval(Auth, _multiRow);
            }

            /// <summary>
            /// ボタンセルクラスを取得します。
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public BtnCell GetBtnInstance(string name)
            {
                return _btns.SingleOrDefault((x) => x.colName == name);
            }
        }
        #endregion

        #region ボタンセルクラス

        #region ボタンセル基底クラス
        /// <summary>
        /// ボタンセル基底クラス
        /// </summary>
        private abstract class BtnCell
        {
            protected GcMultiRow _multiRow;
            protected Authority _auth;

            public BtnCell(GcMultiRow multiRow, Authority auth)
            {
                this._multiRow = multiRow;
                this._auth = auth;
            }

            /// <summary>
            /// ボタンセルの列名
            /// </summary>
            public abstract string colName { get; }

            /// <summary>
            /// ボタンが使用できるか？
            /// </summary>
            /// <returns></returns>
            protected virtual bool IsCan()
            {
                // 権限があるか？
                if (_auth.IsCan(colName, Convert.ToString(_multiRow.CurrentRow.Cells["専門部名"].Value), Convert.ToString(_multiRow.CurrentRow.Cells["専門部署名"].Value)) == false)
                {
                    return false;
                }

                // ステータスがOPENの場合、使用許可
                return IsOpen();
            }

            /// <summary>
            /// ステータスがOPENか？
            /// </summary>
            /// <returns></returns>
            protected bool IsOpen()
            {
                return Convert.ToInt32(_multiRow.CurrentRow.Cells["ステータス"].Value) == 0;
            }

            /// <summary>
            /// ボタン押下時の実装
            /// </summary>
            protected abstract void Get();

            /// <summary>
            /// ボタン押下時の処理
            /// </summary>
            public void Action()
            {
                // ボタンが使用できな場合は何もしない
                if (this.IsCan() == false)
                {
                    return;
                }

                Get();
            }

            /// <summary>
            /// 変更の登録確認、及び登録処理（true：登録実行、false：変更がない・登録しない・キャンセルを選択）
            /// </summary>
            public Func<bool> AskEntry { get; set; }
        }
        #endregion

        #region 専門部署名ボタンセルクラス
        /// <summary>
        /// 専門部署名ボタンセルクラス
        /// </summary>
        private class BtnSpecialized : BtnCell
        {
            public BtnSpecialized(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            { }

            public override string colName
            {
                get
                {
                    return "専門部署名";
                }
            }

            /// <summary>
            /// 専門部署名取得
            /// </summary>
            /// <returns></returns>
            protected override void Get()
            {
                // 課
                using (var form = new SectionListForm() { IS_ALL = true })
                {
                    // 課検索
                    DialogResult result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // 専門部署設定
                        this._multiRow.CurrentRow.Cells[colName].Value = form.DEPARTMENT_CODE;
                        this._multiRow.CurrentRow.Cells[colName].Value = form.SECTION_CODE;

                        // 承認の入力許可設定
                        this._multiRow.CurrentRow.Cells["承認"].ReadOnly = _auth.IsCan("承認", form.DEPARTMENT_CODE, form.SECTION_CODE) == false;
                    }
                }
            }
        }
        #endregion

        #region 重要度ボタンセルクラス
        /// <summary>
        /// 重要度ボタンセルクラス
        /// </summary>
        private class BtnImportance : BtnCell
        {
            public BtnImportance(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
            }

            public override string colName
            {
                get
                {
                    return "重要度";
                }
            }

            protected override void Get()
            {
                var list = HttpUtil.GetResponse<CapImportanceModel>(ControllerType.CapImportance).Results;
                using (var form = new DataGridForm { TITLE = "重要度", DATASOURCE = list })
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this._multiRow.CurrentRow.Cells[colName].Value = form.RETURNVALUE.Cells[0].Value.ToString();
                    }
                }
            }
        }
        #endregion

        #region 分類ボタンセルクラス
        /// <summary>
        /// 分類ボタンセルクラス
        /// </summary>
        private class BtnClassify : BtnCell
        {
            public BtnClassify(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
            }

            public override string colName
            {
                get
                {
                    return "分類";
                }
            }

            protected override void Get()
            {
                var list = HttpUtil.GetResponse<DocumentCodeModel>(ControllerType.DocumentCode).Results;
                using (var form = new DataGridForm { TITLE = "分類", DATASOURCE = list })
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this._multiRow.CurrentRow.Cells[colName].Value = form.RETURNVALUE.Cells[0].Value;
                    }
                }
            }
        }
        #endregion

        #region 評価レベルボタンセルクラス
        /// <summary>
        /// 評価レベルボタンセルクラス
        /// </summary>
        private class BtnLevel : BtnCell
        {
            public BtnLevel(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
            }

            public override string colName
            {
                get
                {
                    return "評価レベル";
                }
            }

            protected override void Get()
            {
                var list = HttpUtil.GetResponse<DocumentLevelModel>(ControllerType.DocumentLevel).Results;
                using (var form = new DataGridForm { TITLE = "評価レベル", DATASOURCE = list })
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        this._multiRow.CurrentRow.Cells[colName].Value = form.RETURNVALUE.Cells[0].Value.ToString();
                    }
                }
            }
        }
        #endregion

        #region クリック制御セル基底ボタンクラス
        /// <summary>
        /// クリック制御セル基底ボタンクラス
        /// </summary>
        private abstract class ControlCell : BtnCell
        {
            #region 内部変数

            /// <summary>
            /// ダブルクリック判定用タイマー
            /// </summary>
            private System.Windows.Forms.Timer doubleClickTimer = new System.Windows.Forms.Timer();

            private bool isFirstClick = true;
            private bool isDoubleClick = false;

            /// <summary>
            /// １回目クリックから２回目クリックまでの時間
            /// </summary>
            private int milliseconds = 0;

            #endregion

            public ControlCell(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
                doubleClickTimer.Interval = 100;
                doubleClickTimer.Tick += new EventHandler(doubleClickTimer_Tick);
            }

            /// <summary>
            /// シングルクリックの場合の処理
            /// </summary>
            protected abstract void SingleAction();

            /// <summary>
            /// DoubleAction
            /// </summary>
            protected abstract void DoubleAction();

            protected override void Get()
            {
                Judge();
            }

            #region 内部メソッド

            /// <summary>
            /// ダブルクリック判定（マウスダウン時）
            /// </summary>
            protected void Judge()
            {
                // １回目クリック時
                if (isFirstClick)
                {
                    isFirstClick = false;

                    // 計測開始
                    doubleClickTimer.Start();
                }

                // ２回目クリック時
                else
                {
                    // １回目クリックから２回目クリックまでの時間がダブルクリック設定時間内の場合
                    if (milliseconds < SystemInformation.DoubleClickTime)
                    {
                        isDoubleClick = true;
                    }
                }
            }

            /// <summary>
            /// タイマーイベント
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void doubleClickTimer_Tick(object sender, EventArgs e)
            {
                milliseconds += 100;

                // 計測時間がダブルクリック設定時間を超えている場合判定する
                if (milliseconds >= SystemInformation.DoubleClickTime)
                {
                    doubleClickTimer.Stop();

                    if (isDoubleClick)
                    {
                        DoubleAction();
                    }
                    else
                    {
                        SingleAction();
                    }

                    // 初期化
                    isFirstClick = true;
                    isDoubleClick = false;
                    milliseconds = 0;
                }
            }

            #endregion
        }
        #endregion

        #region No(履歴)ボタンセルクラス
        /// <summary>
        /// No(履歴)ボタンセルクラス
        /// </summary>
        private class BtnHis : BtnCell
        {
            #region 公開プロパティ

            public override string colName
            {
                get
                {
                    return "No";
                }
            }

            #endregion

            public BtnHis(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
            }

            protected override bool IsCan()
            {
                // 権限があるか？
                return true;
            }

            protected override void Get()
            {
                var itemId = Convert.ToString(_multiRow.Rows[_multiRow.CurrentRow.Index].Cells["項目_ID"].Value);

                //履歴画面表示
                var form = new CapAndProductHistoryForm { ITEM_ID = itemId };
                form.Show();
            }
        }
        #endregion

        #region 評価車両ボタンセルクラス
        /// <summary>
        /// 評価車両ボタンセルクラス
        /// </summary>
        private class BtnEvaluationCar : ControlCell
        {
            public BtnEvaluationCar(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            {
            }

            public override string colName
            {
                get
                {
                    return "評価車両";
                }
            }

            /// <summary>
            /// シングルクリック時
            /// </summary>
            protected override void SingleAction()
            {
                _multiRow.CurrentCell.ReadOnly = true;

                var row = _multiRow.CurrentRow;
                var list = new List<CapDetailModel>();

                if (row.Cells["評価車両"].Value != null)
                {
                    var paramCond = new CapDetailSearchModel
                    {
                        // 評価車両カラム値にある"#"以降の文字を取得
                        号車 = row.Cells["評価車両"].Value.ToString().Substring(row.Cells["評価車両"].Value.ToString().IndexOf("#") + 1),
                        開発符号 = Convert.ToString(row.Cells["車種"].Value),
                    };

                    if (!string.IsNullOrWhiteSpace(paramCond.号車) && !string.IsNullOrWhiteSpace(paramCond.開発符号))
                    {
                        list = HttpUtil.GetResponse<CapDetailSearchModel, CapDetailModel>(ControllerType.CapDetail, paramCond).Results.ToList();
                    }
                }

                using (var form = new DataGridForm { TITLE = "供試詳細", DATASOURCE = list, GRIDVIEW_CLICK = false })
                {
                    form.ShowDialog();
                }
            }

            /// <summary>
            /// ダブルクリック時
            /// </summary>
            protected override void DoubleAction()
            {
                if (IsOpen() && IsCan(true))
                {
                    _multiRow.CurrentCell.ReadOnly = false;
                    _multiRow.BeginEdit(true);
                }
            }

            protected override bool IsCan()
            {
                return true;
            }

            private bool IsCan(bool isCheck)
            {
                // 権限があるか？
                return this._auth.IsCan(colName, Convert.ToString(_multiRow.CurrentRow.Cells["専門部名"].Value), Convert.ToString(_multiRow.CurrentRow.Cells["専門部署名"].Value));
            }
        }
        #endregion

        //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
        #region 写真・動画ボタンセルクラス
        /// <summary>
        /// 写真・動画ボタンセルクラス
        /// </summary>
        private class BtnNDrive : BtnCell
        {
            public NDriveLinkListForm ndForm = null;

            public BtnNDrive(GcMultiRow multiRow, Authority auth) : base(multiRow, auth)
            { }

            public override string colName
            {
                get
                {
                    return "N_DRIVE_LINK";
                }
            }

            /// <summary>
            /// 写真・動画取得
            /// </summary>
            /// <returns></returns>
            protected override void Get()
            {
                var itemId = Convert.ToInt32(_multiRow.Rows[_multiRow.CurrentRow.Index].Cells["ID"].Value);

                //履歴画面表示
                if(this.ndForm == null || this.ndForm.IsDisposed)
                {
                    ndForm = new NDriveLinkListForm { NDriveLinkCapID = itemId };
                    ndForm.Show();
                }
            }
        }
        #endregion
        //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加

        #endregion

        #region 課検索クラス
        /// <summary>
        /// 課検索クラス
        /// </summary>
        private class Section
        {
            /// <summary>
            /// 課検索
            /// </summary>
            /// <param name="departmentid"></param>
            /// <param name="sectionid"></param>
            /// <returns></returns>
            public List<SectionModel> Search(string departmentid, string sectionid)
            {
                //パラメータ設定
                var itemCond = new SectionSearchModel
                {
                    DEPARTMENT_ID = departmentid,
                    SECTION_ID = sectionid,
                };

                return Get(itemCond);
            }

            /// <summary>
            /// 課取得
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            public List<SectionModel> Get(SectionSearchModel model)
            {
                var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, model);

                return (res.Results).ToList();
            }
        }
        #endregion

        #region 文字色制御クラス
        /// <summary>
        /// 文字色制御クラス
        /// </summary>
        private class ForeColorController
        {
            #region 内部変数

            /// <summary>
            /// 赤文字設定をするNoリスト
            /// </summary>
            private List<string> redSettings;

            private GcMultiRow _multiRow;
            private Authority _auth;

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

            /// <summary>
            /// 青文字対象列名
            /// </summary>
            private List<string> blueColList = new List<string>
            {
                "No",
                "種別",
                "重要度",
                "項目",
                "詳細",
                "評価車両",
                "仕向地",
                "CAP確認結果",
            };
            #endregion

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="multiRow"></param>
            /// <param name="auth"></param>
            public ForeColorController(GcMultiRow multiRow, Authority auth)
            {
                this._multiRow = multiRow;
                this.redSettings = new List<string>();
                _auth = auth;
            }

            /// <summary>
            /// 行に文字色設定します。
            /// </summary>
            /// <param name="rowIndex">対象行インデックス</param>
            public void SetRow(int rowIndex)
            {
                // 対象行
                var targetRow = this._multiRow.Rows[rowIndex];

                // 設定
                this.Init(targetRow);

                // 赤文字設定
                foreach (var no in this.redSettings)
                {
                    string colName;
                    if (this.redColList.TryGetValue(no, out colName))
                    {
                        targetRow.Cells[colName].Style.ForeColor = Color.Red;
                    }
                }

                // 青文字設定
                this.blueColList.ForEach((x) => targetRow.Cells[x].Style.ForeColor
                    = Convert.ToString(targetRow.Cells["種別"].Value) == "商品力" ? Color.Blue : customMultiRowCellStyle.DataCellStyle.ForeColor);
            }

            /// <summary>
            /// セルに文字色設定します。
            /// </summary>
            /// <param name="targetRow">対象行</param>
            /// <param name="targetCell">対象セル</param>
            public void SetCell(Row targetRow, Cell targetCell)
            {
                // 設定
                this.Init(targetRow);

                // 見た目リンクラベル列設定
                if (targetCell.Name == "評価車両")
                {
                    SetLinkFormat(targetCell);
                    return;
                }

                // 赤文字設定
                if (this.redColList.ContainsValue(targetCell.Name))
                {
                    var key = this.redColList.Single(x => x.Value == targetCell.Name).Key;
                    if (this.redSettings.Any(no => no == key))
                    {
                        targetCell.Style.ForeColor = Color.Red;
                    }
                }

                // 青文字設定
                if (Convert.ToString(targetRow.Cells["種別"].Value) == "商品力")
                {
                    if (this.blueColList.Contains(targetCell.Name))
                    {
                        targetCell.Style.ForeColor = Color.Blue;
                    }
                }
            }

            /// <summary>
            /// 「文字色 黒 ⇔ 赤ボタン」による対象セルに文字色設定します。
            /// </summary>
            /// <param name="tergetCell">対象セル</param>
            /// <returns>処理結果</returns>
            public bool SetCellForBtn(Cell tergetCell)
            {
                if (redColList.ContainsValue(tergetCell.Name) == false)
                {
                    // 対象列以外は何もしない。
                    return false;
                }

                // 対象行
                var targetRow = _multiRow.Rows[tergetCell.RowIndex];

                if (_auth.IsCan(tergetCell.Name, Convert.ToString(targetRow.Cells["専門部署名"].Value), Convert.ToString(targetRow.Cells["専門部署名"].Value)) == false)
                {
                    // 編集不可セルは対象外
                    return false;
                }

                // 設定値を取得
                this.Init(targetRow);

                // 文字色設定
                this.SetRedOrBlak(tergetCell);

                // 設定値を保存
                targetRow.Cells["修正カラム"].Value = string.Join(",", this.redSettings);

                return true;
            }

            /// <summary>
            /// 初期設定します。
            /// </summary>
            /// <param name="TargetRow"></param>
            private void Init(Row TargetRow)
            {
                this.redSettings = Convert.ToString(TargetRow.Cells["修正カラム"].Value).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            /// <summary>
            /// 適切な文字色（赤or黒）を設定します。
            /// </summary>
            /// <param name="tergetCell">設定対象セル</param>
            /// <returns></returns>
            private void SetRedOrBlak(Cell tergetCell)
            {
                var targetNo = this.redColList.Single((x) => x.Value == tergetCell.Name).Key;

                if (this.redSettings.Contains(targetNo) == true)
                {
                    this.redSettings.Remove(targetNo);
                    tergetCell.Style.ForeColor = Color.Black;
                }
                else
                {
                    this.redSettings.Add(targetNo);
                    tergetCell.Style.ForeColor = Color.Red;
                }
            }

            /// <summary>
            /// 見た目をリンクラベルに見えるように設定
            /// </summary>
            /// <param name="cell"></param>
            private void SetLinkFormat(Cell cell)
            {
                cell.Style.ForeColor = Color.Blue;
                cell.Style.Font = new Font(cell.Style.Font, FontStyle.Underline);
            }
        }
        #endregion

        #region 背景色制御クラス
        /// <summary>
        /// 背景色制御クラス
        /// </summary>
        private class BackColorController
        {
            private GcMultiRow _multiRow;

            /// <summary>
            /// 黄色対象列名
            /// </summary>
            private List<string> yellowColList = new List<string>()
            {
                "専門部署名",
                "対策案",
            };

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="multiRow"></param>
            public BackColorController(GcMultiRow multiRow)
            {
                this._multiRow = multiRow;
            }

            /// <summary>
            /// ステータスCLOSE色に設定します。
            /// </summary>
            /// <param name="cell"></param>
            public void SetCloseColor(Cell cell)
            {
                cell.Style.BackColor = Color.LightGray;
            }

            /// <summary>
            /// ステータスOPEN色に設定します。
            /// </summary>
            /// <param name="row"></param>
            /// <param name="cell"></param>
            public void SetOpenColor(Row row, Cell cell)
            {
                if (this.yellowColList.Any((x) => x == cell.Name) && this.IsYellow(row))
                {
                    //Update Start 2021/08/20 杉浦 CAP要望対応
                    //cell.Style.BackColor = Color.Yellow;
                    if (row.Cells["対策案"].Value == null)
                    {
                        cell.Style.BackColor = Color.Red;
                    }else
                    {
                        cell.Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                    }
                    //Update End 2021/08/20 杉浦 CAP要望対応
                }
                else
                {
                    cell.Style.BackColor = customMultiRowCellStyle.DataCellStyle.BackColor;
                }
            }

            /// <summary>
            /// 対象セルの背景色が黄色対象か？
            /// </summary>
            /// <param name="row">対象行</param>
            /// <returns></returns>
            public bool IsYellow(Row row)
            {
                //Delete Start 2021/08/20 杉浦 CAP要望対応
                //var updateValue = row.Cells["編集日"].Value;
                //Delete Start 2021/08/20 杉浦 CAP要望対応
                var answerSetValue = row.Cells["回答期限設定日"].Value;
                var answerLimitValue = row.Cells["回答期限"].Value;

                if (answerSetValue == null)
                {
                    return false;
                }

                //Delete Start 2021/08/20 杉浦 CAP要望対応
                //var answerLimitSetDate = Convert.ToDateTime(answerSetValue);
                //Delete Start 2021/08/20 杉浦 CAP要望対応
                var answerLimitDate = Convert.ToDateTime(answerLimitValue);

                // 回答期限が過ぎている場合
                //Update Start 2021/08/20 杉浦 CAP要望対応
                //if ((updateValue == null || DateTime.Compare(Convert.ToDateTime(updateValue), answerLimitSetDate) <= 0) &&
                //    DateTime.Compare(answerLimitDate.Date, DateTime.Today) < 0)
                if (DateTime.Compare(answerLimitDate.Date, DateTime.Today) < 0)
                //Update Start 2021/08/20 杉浦 CAP要望対応
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region 選択セルクラス
        /// <summary>
        /// 選択セルクラス
        /// </summary>
        private class SelectedCellController
        {
            private GcMultiRow _multiRow;

            public SelectedCellController(GcMultiRow multiRow)
            {
                this._multiRow = multiRow;
            }

            /// <summary>
            /// 有効な選択セルを取得します。無効なセルが選択されている場合や未選択の場合はNullが返却されます。
            /// </summary>
            /// <returns></returns>
            public Cell GetCell()
            {
                return this.IsSelected() ? this._multiRow.CurrentCell : null;
            }

            /// <summary>
            /// 有効なセルが選択されているか？
            /// </summary>
            /// <returns></returns>
            public bool IsSelected()
            {
                if (this._multiRow.SelectedCells.Count <= 0)
                {
                    return false;
                }

                if (this._multiRow.CurrentCell == null)
                {
                    return false;
                }

                if (this._multiRow.CurrentCell is FilteringTextBoxCell)
                {
                    return false;
                }

                if (this._multiRow.CurrentCell.CellIndex >= this._multiRow.Columns.Count)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// ステータスがOPENな行のセルを選択しているか？
            /// </summary>
            /// <returns></returns>
            public bool IsOpen()
            {
                return Convert.ToInt32(_multiRow.Rows[_multiRow.CurrentCell.RowIndex].Cells["ステータス"].Value) == 0;
            }
        }

        #endregion

        #region インポート処理クラス
        /// <summary>
        /// インポート処理クラス
        /// </summary>
        private class Import
        {
            #region 内部変数

            private Form _form;

            /// <summary>
            /// 読み込んだインポートファイルデータ
            /// </summary>
            private List<string[]> readData = new List<string[]>();

            /// <summary>
            /// 読み込み車種と同じCapデータ
            /// </summary>
            private List<CapModel> sameGcodeCaps;

            /// <summary>
            /// 読み込み車種と同じマスタデータ
            /// </summary>
            private List<GeneralCodeSearchOutModel> sameGeneralCode;

            /// <summary>
            /// 読み込み開始行インデックス
            /// </summary>
            private const int READ_START_ROW_INDEX = 4;

            //Update Start 2021/06/25 矢作
            /// <summary>
            /// 項目順
            /// </summary>
            private string[] orders = new string[] { "No", "車種", "項目", "詳細", "CAP種別", "重要度", "方向付け確定期限", "評価車両", "仕向地", };
            //Update End 2021/06/25 矢作

            #endregion

            #region コンストラクタ
            /// <summary>
            /// インポート処理クラスのコンストラクタ
            /// </summary>
            /// <param name="form"></param>
            public Import(Form form)
            {
                _form = form;
            }
            #endregion

            #region 実行
            /// <summary>
            /// 実行
            /// </summary>
            public void Run()
            {
                if (Read() && Load())
                {
                    FormControlUtil.FormWait(_form, () =>
                    {
                        if (Save())
                        {
                            Messenger.Info(Resources.KKM00002);
                        }
                    });
                }
            }
            #endregion

            #region 内部メソッド

            #region 読み込み
            /// <summary>
            /// 読み込み
            /// </summary>
            /// <returns></returns>
            private bool Read()
            {
                using (var dialog = new OpenFileDialog() { Filter = "インポートファイル|開発CAP_指摘入力用ｼｰﾄ.xlsx|Excel ブック (*.xlsx, *.xls)|*.xlsx;*.xls" })
                {
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return false;
                    }

                    // プロセスロック・Excel ファイル判定
                    if (!FileUtil.IsFileExcel(dialog.FileName))
                    {
                        Messenger.Warn(Resources.TCM03018); // ファイルアクセス失敗
                        return false;
                    }

                    using (var xls = new XlsUtil(dialog.FileName))
                    {
                        // ファイルフォーマットチェック

                        if (xls.GetCellStringValue(0, 1, 1) != "評価車種" || xls.GetCellStringValue(0, 1, 5) != "評価号車" ||
                            xls.GetCellStringValue(0, 3, 3) != "項目" || xls.GetCellStringValue(0, 3, 4) != "内容" ||
                            xls.GetCellStringValue(0, 3, 5) != "種別" || xls.GetCellStringValue(0, 3, 6) != "重要度" ||
                            //Update Start 2021/06/25 矢作
                            //xls.GetCellStringValue(0, 3, 9).Replace("\n", "") != "関連部署１")
                            xls.GetCellStringValue(0, 3, 10).Replace("\n", "") != "関連部署１")
                            //Update End 2021/06/25 矢作
                        {
                            Messenger.Warn(string.Format(Resources.KKM03038, "指定したインポートファイル"));
                            return false;
                        }

                        FormControlUtil.FormWait(_form, () =>
                        {
                            // 車種
                            var gCode = xls.GetCellStringValue(0, 1, 3);

                            // 評価車両
                            var evaluateCar = xls.GetCellStringValue(0, 1, 6);

                            // 行データ
                            var rowCounter = READ_START_ROW_INDEX;
                            while (string.IsNullOrEmpty(xls.GetCellStringValue(0, rowCounter, 3)) == false)
                            {
                                var obj = new string[]
                                {
                                        // No
                                        "",

                                        // 車種
                                        gCode,

                                        // 項目
                                        xls.GetCellStringValue(0, rowCounter, 3),

                                        // 詳細
                                        xls.GetCellStringValue(0, rowCounter, 4),

                                        // CAP種別
                                        xls.GetCellStringValue(0, rowCounter, 5),

                                        // 重要度
                                        xls.GetCellStringValue(0, rowCounter, 6),
                                        
                                        //Append Start 2021/06/25 矢作
                                        // 方向付け確定期限
                                        xls.GetCellStringValue(0, rowCounter, 9),
                                        //Append End 2021/06/25 矢作

                                        // 評価車両
                                        evaluateCar,

                                        // 仕向地
                                        "",
                                        
                                        //Update Start 2021/06/25 矢作
                                        // 専門部署名１～５
                                        //xls.GetCellStringValue(0, rowCounter, 9),
                                        //xls.GetCellStringValue(0, rowCounter, 10),
                                        //xls.GetCellStringValue(0, rowCounter, 11),
                                        //xls.GetCellStringValue(0, rowCounter, 12),
                                        //xls.GetCellStringValue(0, rowCounter, 13),
                                        xls.GetCellStringValue(0, rowCounter, 10),
                                        xls.GetCellStringValue(0, rowCounter, 11),
                                        xls.GetCellStringValue(0, rowCounter, 12),
                                        xls.GetCellStringValue(0, rowCounter, 13),
                                        xls.GetCellStringValue(0, rowCounter, 14),
                                        //Update End 2021/06/25 矢作
                                };

                                readData.Add(obj);

                                rowCounter++;
                            }
                        });
                    }

                    if (readData.Any() == false)
                    {
                        Messenger.Warn(Resources.KKM03040);
                        return false;
                    }
                }

                return true;
            }
            #endregion

            #region ロード
            /// <summary>
            /// ロード
            /// </summary>
            /// <returns></returns>
            private bool Load()
            {
                // 必須チェック
                if (!IsRequired())
                {
                    return false;
                }

                // DBデータの取得
                FormControlUtil.FormWait(_form, () => GetData());

                // 存在チェック
                if (!IsExist())
                {
                    return false;
                }

                // インポートデータ設定画面表示
                using (var form = new CapAndProductImportSettingForm() { No = Convert.ToString(sameGcodeCaps.Any() ? sameGcodeCaps.Max((x) => x.NO) + 1 : (long?)null) })
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return false;
                    }

                    // 設定
                    for (var i = 0; i < readData.Count; i++)
                    {
                        readData[i][Index("No")] = Convert.ToString(Convert.ToInt32(form.No) + i);
                        readData[i][Index("仕向地")] = form.Place;
                    }
                }

                // 入力チェック
                if (!IsEntry())
                {
                    return false;
                }

                return true;
            }
            #endregion

            #region DB登録
            /// <summary>
            /// DB登録
            /// </summary>
            /// <returns></returns>
            private bool Save()
            {
                return new CapDB().Post(ConvertDataToModel(readData));
            }
            #endregion

            #region 必須チェック
            /// <summary>
            /// 必須チェック
            /// </summary>
            /// <returns></returns>
            private bool IsRequired()
            {
                // 車種チェック
                if (readData.Any((x) => string.IsNullOrEmpty(x[Index("車種")])))
                {
                    Messenger.Warn(string.Format(Resources.KKM00001, "評価車種"));
                    return false;
                }

                // 専門部署名チェック
                var ngRowIndexs = readData
                    .Select((x, i) => x.Skip(orders.Length).All((y) => string.IsNullOrEmpty(y)) == true ? (int?)i : null)
                    .Where((x) => x != null)
                    .ToList();

                if (ngRowIndexs.Any())
                {
                    var param = string.Join(Const.CrLf, ngRowIndexs.Select((x) => "行番号：" + Convert.ToString(x + READ_START_ROW_INDEX + 1)));
                    Messenger.Warn(string.Format(Resources.KKM03041, param));
                    return false;
                }

                return true;
            }
            #endregion

            #region 存在チェック
            /// <summary>
            /// 存在チェック
            /// </summary>
            /// <returns></returns>
            private bool IsExist()
            {
                // 車種
                if (!sameGeneralCode.Any())
                {
                    Messenger.Warn(string.Format(Resources.KKM03046, "評価車種"));
                    return false;
                }

                return true;
            }
            #endregion

            #region 入力チェック
            /// <summary>
            /// 入力チェック
            /// </summary>
            /// <returns></returns>
            private bool IsEntry()
            {
                var same = readData.FirstOrDefault((x) => sameGcodeCaps.Any((y) => Convert.ToInt64(x[Index("No")]) == y.NO));

                // No
                if (same != null)
                {
                    // 重複エラー
                    Messenger.Warn(string.Format(Resources.KKM03042, same[Index("No")]));
                    return false;
                }

                return true;
            }
            #endregion

            #region 読み込みデータを各専門部署ごとのModelリストに変換
            /// <summary>
            /// 読み込みデータを各専門部署ごとのModelリストに変換します。
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            private List<CapModel> ConvertDataToModel(List<string[]> list)
            {
                var ret = new List<CapModel>();

                foreach (var obj in list)
                {
                    // 専門部署リスト
                    var sections = obj.Skip(orders.Length).Where((x) => string.IsNullOrEmpty(x) == false);

                    foreach (var section in sections)
                    {
                        //Append Start 2021/06/25 矢作
                        DateTime? dt = new DateTime();
                        if (!string.IsNullOrEmpty(obj[Index("方向付け確定期限")]))
                        {
                            dt = DateTime.Parse(obj[Index("方向付け確定期限")]);
                        }
                        else
                        {
                            dt = null;
                        }
                        //Append End 2021/06/25 矢作

                        var model = new CapModel()
                        {
                            NO = Convert.ToInt64(obj[Index("No")]),
                            GENERAL_CODE = obj[Index("車種")],
                            項目 = obj[Index("項目")],
                            詳細 = obj[Index("詳細")],
                            重要度 = obj[Index("重要度")],
                            評価車両 = obj[Index("評価車両")],
                            仕向地 = obj[Index("仕向地")],
                            CAP種別 = obj[Index("CAP種別")],
                            //Append Start 2021/06/25 矢作
                            方向付け確定期限 = dt,
                            //Append End 2021/06/25 矢作
                            専門部署名 = section,
                            FLAG_最新 = 1,
                        };

                        ret.Add(model);
                    }
                }

                return ret;
            }
            #endregion

            #region DBデータの取得
            /// <summary>
            /// DBデータの取得
            /// </summary>
            private void GetData()
            {
                // 読み込み車種と同じCapデータを取得
                sameGcodeCaps = new CapDB().Get(new CapSearchModel()
                {
                    GENERAL_CODE = new string[] { readData[0][Index("車種")] }
                });

                // 読み込み車種と同じマスタデータを取得
                sameGeneralCode = HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, new GeneralCodeSearchInModel
                {
                    // 検索区分
                    CLASS_DATA = 1,

                    // 開発符号
                    GENERAL_CODE = readData[0][Index("車種")],

                    // ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    // 開発フラグ(0:開発外 1:開発中 省略時は条件に含めない)
                    UNDER_DEVELOPMENT = "1"
                })?
                .Results?.ToList();
            }
            #endregion

            #region 各項目のインデックス
            /// <summary>
            /// 各項目のインデックス
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            private int Index(string name)
            {
                return orders.ToList().FindIndex((x) => x == name);
            }
            #endregion

            #endregion
        }
        #endregion

        #region エクスポート処理クラス
        /// <summary>
        /// エクスポート処理クラス
        /// </summary>
        private class Export
        {
            #region 内部変数

            private BaseForm _form;
            private GcMultiRow _mlr;

            /// <summary>
            /// 出力対象行
            /// </summary>
            private IEnumerable<Row> _targets;

            /// <summary>
            /// 課リスト
            /// </summary>
            private List<SectionModel> _Sections;

            #endregion

            #region コンストラクタ
            /// <summary>
            /// エクスポート処理クラスのコンストラクタ
            /// </summary>
            /// <param name="form"></param>
            /// <param name="mlr"></param>
            public Export(BaseForm form, GcMultiRow mlr)
            {
                _form = form;
                _mlr = mlr;
                _Sections = new Section().Get(new SectionSearchModel());
            }
            #endregion

            #region 実行
            /// <summary>
            /// 実行
            /// </summary>
            public void Run()
            {
                if (HasTarget() == false)
                {
                    Messenger.Warn(Resources.TCM03008);
                    return;
                }

                using (var dialog = new SaveFileDialog() { Filter = "Excel ブック (*.xlsx)|*.xlsx;", FileName = string.Format("{0}_{1:yyyyMMddHHmmss}", _form.FormTitle, DateTime.Now) })
                {
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    FormControlUtil.FormWait(_form, () =>
                    {
                        using (var xls = new CapExportXls(Properties.Resources.CapExport))
                        {
                            #region 報告書シート書き込み
                            // 報告書シート
                            var dataReport = GetReports();
                            Func<ReportSheet, IDictionary<string, string>> funcReport = (x) =>
                            {
                                return new Dictionary<string, string>()
                                            {
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.車種)), x.車種 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.No)), x.No },
                                                //Append Start 2020/12/24 杉浦
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.CAP種別)), x.CAP種別 },
                                                //Append End 2020/12/24 杉浦
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.重要度)), x.重要度 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.項目)), x.項目 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.詳細)), x.詳細 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.供試車)), x.供試車 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.CAP結果)), x.CAP結果 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.フォロー状況)), x.フォロー状況 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.CAP時期)), x.CAP時期 },
                                                //Append Start 2021/06/25 矢作
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.方向付け確定期限)), x.方向付け確定期限 },
                                                //Append End 2021/06/25 矢作
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.試験部署)), x.試験部署 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.試験対応策)), x.試験対応策 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.試験日程)), x.試験日程 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.設計部署)), x.設計部署 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.設計対応策)), x.設計対応策 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.供試品)), x.供試品 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.設計日程)), x.設計日程 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.織込時期)), x.織込時期 },
                                            };
                            };

                            xls.WriteSheet("報告書", dataReport, funcReport, 17);
                            xls.SetStyleReport(dataReport, funcReport, 17);
                            #endregion

                            #region 既存確認シート書き込み
                            // 既存確認シート
                            var dataExistConfirm = GetExistConfirms();
                            Func<ExistConfirmSheet, IDictionary<string, string>> funcExistConfirm = (x) =>
                            {
                                return new Dictionary<string, string>()
                                            {
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.車種)), x.車種 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.No)), x.No },
                                                //Append Start 2020/12/24 杉浦
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.CAP種別)), x.CAP種別 },
                                                //Append End 2020/12/24 杉浦
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.重要度)), x.重要度 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.項目)), x.項目 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.詳細)), x.詳細 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.供試車)), x.供試車 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.仕向地)), x.仕向地 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.CAP結果)), x.CAP結果 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.フォロー状況)), x.フォロー状況 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署1)), x.対応部署1 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署2)), x.対応部署2 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署3)), x.対応部署3 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署4)), x.対応部署4 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署5)), x.対応部署5 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署6)), x.対応部署6 },
                                                { xls.GetColumnAddress(x.GetColIndex(() => x.対応部署7)), x.対応部署7 },
                                            };
                            };
                            xls.WriteSheet("既存確認", dataExistConfirm, funcExistConfirm, 4);
                            xls.SetStyleExistConfirm(dataExistConfirm, funcExistConfirm, 4);
                            #endregion

                            #region 開発進度シート書き込み
                            // 開発進度
                            xls.WriteSheet(
                                "開発進度",
                                GetDevProgress(),
                                (x) =>
                                {
                                    return new Dictionary<string, double>()
                                    {
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3)), x.K3 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.PUG工試)), x.PUG工試 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.量産確認車)), x.量産確認車 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.認証車)), x.認証車 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.工試車)), x.工試車 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.FT1)), x.FT1 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.FT2)), x.FT2 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.生試車)), x.生試車 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.PPA)), x.PPA },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.PPB)), x.PPB },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.量先車)), x.量先車 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.PreSOP)), x.PreSOP },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.ランチェン)), x.ランチェン },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.クローズ)), x.クローズ },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.要調整)), x.要調整 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.空欄)), x.空欄 },
                                        //Update Start 2020/12/24 杉浦
                                        //{ xls.GetColumnAddress(x.GetColIndex(()=> x.調査中調査待ち)), x.調査中調査待ち },
                                        //{ xls.GetColumnAddress(x.GetColIndex(()=> x.現象再現しない)), x.現象再現しない },
                                        //{ xls.GetColumnAddress(x.GetColIndex(()=> x.本開発車ではこのままとしたい)), x.本開発車ではこのままとしたい },
                                        //{ xls.GetColumnAddress(x.GetColIndex(()=> x.本開発車ではこのままとする_クローズ)), x.本開発車ではこのままとする_クローズ },
                                        //{ xls.GetColumnAddress(x.GetColIndex(()=> x.その他)), x.その他 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3開発CAP_調査中調査待ち)), x.K3開発CAP_調査中調査待ち },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3開発CAP_現象再現しない)), x.K3開発CAP_現象再現しない },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3開発CAP_本開発車ではこのままとしたい)), x.K3開発CAP_本開発車ではこのままとしたい },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3開発CAP_本開発車ではこのままとする_クローズ)), x.K3開発CAP_本開発車ではこのままとする_クローズ },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.K3開発CAP_その他)), x.K3開発CAP_その他 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.図面CAP_改善する改善済み)), x.図面CAP_改善する改善済み },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.図面CAP_K3実車で確認)), x.図面CAP_K3実車で確認 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.図面CAP_検討中)), x.図面CAP_検討中 },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.図面CAP_本開発車ではこのままとしたい)), x.図面CAP_本開発車ではこのままとしたい },
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.図面CAP_本開発車ではこのままとする_クローズ)), x.図面CAP_本開発車ではこのままとする_クローズ },
                                        //Update End 2020/12/24 杉浦
                                        { xls.GetColumnAddress(x.GetColIndex(()=> x.対応状況空欄)), x.対応状況空欄 },
                                    };
                                },
                                4);
                            #endregion

                            // シート選択
                            xls.Select("報告書");
                            xls.Select("既存確認", false);
                            xls.Select("開発進度", false);

                            xls.Save(dialog.FileName);
                        }
                    });
                }
            }
            #endregion

            #region 内部メソッド

            #region 対象データがあるか？
            /// <summary>
            /// 対象データがあるか？
            /// </summary>
            /// <returns></returns>
            private bool HasTarget()
            {
                // CAP確認がONのもの
                _targets = _mlr.Rows.Where((x) => Convert.ToBoolean(x.Cells["CAP確認"].Value) == true);

                return _targets.Any();
            }
            #endregion

            #region 報告書シートデータを取得します。
            /// <summary>
            /// 報告書シートデータを取得します。
            /// </summary>
            /// <returns></returns>
            private List<ReportSheet> GetReports()
            {
                // 行⇒行クラス
                var rows = _targets.Select((x) => new ReportRow(_Sections, x));

                List<ReportSheet> sheet = new List<ReportSheet>();

                // No,車種ごとにマージ
                foreach (var grp in rows.Select(x => new { x.No, x.車種 }).Distinct())
                {
                    var _Nos = rows.Where((x) => x.No == grp.No && x.車種 == grp.車種);

                    // 試験部
                    var Exp = _Nos.Where((x) => x.IsExperiment() == true).ToList();

                    // 設計部
                    var Des = _Nos.Where((x) => x.IsExperiment() == false).ToList();

                    var count = Exp.Count > Des.Count ? Exp.Count : Des.Count;

                    var Nos = new ReportSheet[count];

                    // ヘッダー情報
                    var head = rows.First((x) => x.No == grp.No && x.車種 == grp.車種);
                    for (var i = 0; i < Nos.Length; i++)
                    {
                        Nos[i] = new ReportSheet();

                        if (Exp.Count > i)
                        {
                            Nos[i].試験部署 = Exp[i].部署;
                            Nos[i].試験対応策 = Exp[i].対応策;
                            Nos[i].試験日程 = Exp[i].試験日程;
                            //Append Start 2021/07/05 矢作
                            if (!string.IsNullOrEmpty(Exp[i].方向付け確定期限))
                            {
                                Nos[i].方向付け確定期限 = Convert.ToDateTime(Exp[i].方向付け確定期限).ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                Nos[i].方向付け確定期限 = " ";
                            }
                            //Append End 2021/07/05 矢作
                        }
                        else
                        {
                            Nos[i].試験部署 = "－";
                            Nos[i].試験対応策 = " ";
                            Nos[i].試験日程 = " ";
                            //Append Start 2021/07/05 矢作
                            Nos[i].方向付け確定期限 = " ";
                            //Append End 2021/07/05 矢作
                        }

                        if (Des.Count > i)
                        {
                            Nos[i].設計部署 = Des[i].部署;
                            Nos[i].設計対応策 = Des[i].対応策;
                            Nos[i].供試品 = Des[i].供試品;
                            Nos[i].設計日程 = Des[i].設計日程;
                            Nos[i].織込時期 = Des[i].織込時期;
                        }
                        else
                        {
                            Nos[i].設計部署 = "－";
                            Nos[i].設計対応策 = " ";
                            Nos[i].供試品 = " ";
                            Nos[i].設計日程 = " ";
                            Nos[i].織込時期 = " ";
                        }

                        Nos[i].車種 = head.車種;
                        Nos[i].No = head.No;
                        Nos[i].CAP種別 = head.CAP種別;
                        Nos[i].重要度 = head.重要度;
                        Nos[i].項目 = head.項目;
                        Nos[i].詳細 = head.詳細;
                        Nos[i].供試車 = head.供試車;
                        Nos[i].CAP結果 = head.CAP結果;
                        Nos[i].フォロー状況 = head.フォロー状況;
                        Nos[i].CAP時期 = head.CAP時期;
                    }

                    sheet.AddRange(Nos);
                }

                return sheet;
            }
            #endregion

            #region 既存確認シートデータを取得します。
            /// <summary>
            /// 既存確認シートデータを取得します。
            /// </summary>
            /// <returns></returns>
            private List<ExistConfirmSheet> GetExistConfirms()
            {
                // 行⇒行クラス
                var rows = _targets.Select((x) => new ExistConfirmRow(x));

                var sheets = new List<ExistConfirmSheet>();

                // No,車種ごとにマージ
                foreach (var grp in rows.Select((x) => new { x.No, x.車種 }).Distinct())
                {
                    // ヘッダー情報
                    var head = rows.First((x) => x.No == grp.No && x.車種 == grp.車種);

                    // 部署情報
                    var depts = rows.Where((x) => x.No == grp.No && x.車種 == grp.車種).Select((x) => x.部署 + Const.CrLf + x.対応策 + Const.CrLf + x.編集日 + Const.CrLf).ToList();

                    var sheet = new ExistConfirmSheet()
                    {
                        車種 = grp.車種,
                        No = grp.No,
                        //Append Start 2020/12/24 杉浦
                        CAP種別 = head.CAP種別,
                        //Append End 2020/12/24 杉浦
                        重要度 = head.重要度,
                        項目 = head.項目,
                        詳細 = head.詳細,
                        供試車 = head.供試車,
                        仕向地 = head.仕向地,
                        CAP結果 = head.CAP結果,
                        フォロー状況 = head.フォロー状況,
                        Depts = depts,
                    };

                    sheets.Add(sheet);
                }

                return sheets;
            }
            #endregion

            #region 開発進度シートデータを取得します。
            /// <summary>
            /// 開発進度シートデータを取得します。
            /// </summary>
            /// <returns></returns>
            private List<DevProgressSheet> GetDevProgress()
            {
                var devPrgS = new DevProgressSheet();
                var devPrgA = new DevProgressSheet();
                var devPrgB = new DevProgressSheet();
                var devPrgC = new DevProgressSheet();

                // No,車種毎のカウント
                foreach (var row in _targets.GroupBy(x => new { No = x.Cells["No"].Value, 車種 = x.Cells["車種"].Value }).SelectMany(grp => grp.Take(1)))
                {
                    switch (Convert.ToString(row.Cells["重要度"].Value))
                    {
                        case "S":
                            devPrgS.Add(row);
                            break;
                        case "A":
                            devPrgA.Add(row);
                            break;
                        case "B":
                            devPrgB.Add(row);
                            break;
                        case "C":
                            devPrgC.Add(row);
                            break;
                    }
                }

                return new List<DevProgressSheet>() { devPrgS, devPrgA, devPrgB, devPrgC };
            }
            #endregion

            #endregion

            #region 共通行クラス
            /// <summary>
            /// 共通行クラス
            /// </summary>
            private abstract class CommonRow
            {
                protected Row _row;

                public CommonRow(Row row)
                {
                    _row = row;
                }

                public virtual string 車種 => Convert.ToString(_row.Cells["車種"].Value);
                public virtual string No => Convert.ToString(_row.Cells["No"].Value);
                //Append Start 2020/12/24 杉浦
                public virtual string CAP種別 => Convert.ToString(_row.Cells["種別"].Value);
                //Append End 2020/12/24 杉浦
                public virtual string 重要度 => Convert.ToString(_row.Cells["重要度"].Value);
                public virtual string 項目 => Convert.ToString(_row.Cells["項目"].Value);
                public virtual string 詳細 => Convert.ToString(_row.Cells["詳細"].Value);
                public virtual string 供試車 => Convert.ToString(_row.Cells["評価車両"].Value);
                public virtual string 部署 => Convert.ToString(_row.Cells["専門部署名"].Value);
                public virtual string 対応策 => Convert.ToString(_row.Cells["対策案"].Value);
                public virtual string CAP結果 => Convert.ToString(_row.Cells["CAP確認結果"].Value);
                public virtual string フォロー状況 => Convert.ToString(_row.Cells["フォロー状況"].Value);
                public virtual string CAP時期 => Convert.ToString(_row.Cells["CAP確認時期"].Value);
                //Append Start 2021/06/25 矢作);
                public virtual string 方向付け確定期限 => Convert.ToString(_row.Cells["方向付け確定期限"].Value);
                //Append End 2020/06/25 矢作

                protected string ConvertDateToYYMMDD(Row row, string col)
                {
                    var target = row.Cells[col].Value;

                    // 特定変換
                    if (col == "完了日程" && Convert.ToString(row.Cells["完了日程コンボボックス"].Value) == ConstFinish)
                    {
                        return ConstFinish;
                    }

                    // 特定変換
                    if (col == "出図日程" && Convert.ToString(row.Cells["出図日程コンボボックス"].Value) == ConstFinish)
                    {
                        return ConstFinish;
                    }

                    // 日付型は文字列形式に変換
                    if (target is DateTime)
                    {
                        return Convert.ToDateTime(target).ToString("yy/MM/dd");
                    }

                    return Convert.ToString(target);
                }
            }
            #endregion

            #region 共通シートクラス
            /// <summary>
            /// 共通シートクラス
            /// </summary>
            private abstract class CommonSheet
            {
                /// <summary>
                /// 出力列順
                /// </summary>
                protected abstract List<string> Order { get; }

                /// <summary>
                /// Excel出力列インデックス取得
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="e"></param>
                /// <returns></returns>
                public virtual int GetColIndex<T>(Expression<Func<T>> e)
                {
                    var member = (MemberExpression)e.Body;

                    // B列から出力するため + 1
                    return Order.FindIndex((x) => x == member.Member.Name) + 1;
                }
            }
            #endregion

            #region 既存確認行クラス
            /// <summary>
            /// 既存確認行クラス
            /// </summary>
            private class ExistConfirmRow : CommonRow
            {
                public ExistConfirmRow(Row row) : base(row)
                {
                }

                public override string 詳細 => base.詳細 + Const.CrLf;
                public string 仕向地 => Convert.ToString(_row.Cells["仕向地"].Value);
                public string 編集日 => Convert.ToDateTime(_row.Cells["編集日"].Value).ToString("yy/MM/dd hh:mm:ss");
            }
            #endregion

            #region 既存確認シートクラス
            /// <summary>
            /// 既存確認シートクラス
            /// </summary>
            private class ExistConfirmSheet : CommonSheet
            {
                public List<string> Depts { get; set; }

                public string 車種 { get; set; }
                public string No { get; set; }
                //Append Start 2020/12/24 杉浦
                public string CAP種別 { get; set; }
                //Append End 2020/12/24 杉浦
                public string 重要度 { get; set; }
                public string 項目 { get; set; }
                public string 詳細 { get; set; }
                public string 供試車 { get; set; }
                public string 仕向地 { get; set; }
                public string CAP結果 { get; set; }
                public string フォロー状況 { get; set; }
                public string 対応部署1 => Depts.Count > 0 ? Depts[0] : "";
                public string 対応部署2 => Depts.Count > 1 ? Depts[1] : "";
                public string 対応部署3 => Depts.Count > 2 ? Depts[2] : "";
                public string 対応部署4 => Depts.Count > 3 ? Depts[3] : "";
                public string 対応部署5 => Depts.Count > 4 ? Depts[4] : "";
                public string 対応部署6 => Depts.Count > 5 ? Depts[5] : "";
                public string 対応部署7 => Depts.Count > 6 ? Depts[6] : "";

                private List<string> order = new List<string>()
                {
                    "車種",
                    "No",
                    //Append Start 2020/12/24 杉浦
                    "CAP種別",
                    //Append End 2020/12/24 杉浦
                    "重要度",
                    "項目",
                    "詳細",
                    "供試車",
                    "仕向地",
                    "CAP結果",
                    "フォロー状況",
                    "対応部署1",
                    "対応部署2",
                    "対応部署3",
                    "対応部署4",
                    "対応部署5",
                    "対応部署6",
                    "対応部署7",
                };

                protected override List<string> Order => order;
            }
            #endregion

            #region 報告書行クラス
            /// <summary>
            /// 報告書行クラス
            /// </summary>
            private class ReportRow : CommonRow
            {
                private List<SectionModel> _Sections;

                public ReportRow(List<SectionModel> sections, Row row) : base(row)
                {
                    _Sections = sections;
                }

                public override string 項目 => base.項目 + Const.CrLf + Const.CrLf;
                public override string 対応策 => base.対応策 + Const.CrLf + Const.CrLf;
                public string 試験日程 => ConvertDateToYYMMDD(_row, "完了日程");
                public string 供試品 => Convert.ToString(_row.Cells["供試品"].Value);
                public string 設計日程 => ConvertDateToYYMMDD(_row, "出図日程");
                public string 織込時期 => Convert.ToString(_row.Cells["織込時期"].Value);

                /// <summary>
                /// その課が実験か？
                /// </summary>
                /// <returns></returns>
                public bool IsExperiment()
                {
                    var item = _Sections.Where((x) => x.SECTION_CODE == 部署).ToList();
                    var sec = _Sections.SingleOrDefault((x) => x.SECTION_CODE == 部署);

                    return _Sections == null ? false : sec?.FLAG_KENJITSU == 1;
                }
            }
            #endregion

            #region 報告書シートクラス
            /// <summary>
            /// 報告書シートクラス
            /// </summary>
            private class ReportSheet : CommonSheet
            {
                public string 車種 { get; set; }
                public string No { get; set; }
                //Append Start 2020/12/24 杉浦
                public string CAP種別 { get; set; }
                //Append End 2020/12/24 杉浦
                public string 重要度 { get; set; }
                public string 項目 { get; set; }
                public string 詳細 { get; set; }
                public string 供試車 { get; set; }
                public string CAP結果 { get; set; }
                public string フォロー状況 { get; set; }
                public string CAP時期 { get; set; }
                //Append Start 2021/06/25 矢作
                public string 方向付け確定期限 { get; set; }
                //Append End 2020/06/25 矢作
                public string 試験部署 { get; set; }
                public string 試験対応策 { get; set; }
                public string 試験日程 { get; set; }
                public string 設計部署 { get; set; }
                public string 設計対応策 { get; set; }
                public string 供試品 { get; set; }
                public string 設計日程 { get; set; }
                public string 織込時期 { get; set; }

                private List<string> order = new List<string>()
                {
                    "車種",
                    "No",
                    //Append Start 2020/12/24 杉浦
                    "CAP種別",
                    //Append End 2020/12/24 杉浦
                    "重要度",
                    "項目",
                    "詳細",
                    "供試車",
                    "CAP結果",
                    "フォロー状況",
                    "CAP時期",
                    //Append Start 2021/06/25 矢作
                    "方向付け確定期限",
                    //Append End 2020/06/25 矢作
                    "試験部署",
                    "試験対応策",
                    "試験日程",
                    "設計部署",
                    "設計対応策",
                    "供試品",
                    "設計日程",
                    "織込時期",
                };

                protected override List<string> Order => order;
            }
            #endregion

            #region 開発進度シートクラス
            /// <summary>
            /// 開発進度シートクラス
            /// </summary>
            private class DevProgressSheet : CommonSheet
            {
                // CAP確認時期
                public double K3 { get; set; }
                public double PUG工試 { get; set; }
                public double 量産確認車 { get; set; }
                public double 認証車 { get; set; }
                public double 工試車 { get; set; }
                public double FT1 { get; set; }
                public double FT2 { get; set; }
                public double 生試車 { get; set; }
                public double PPA { get; set; }
                public double PPB { get; set; }
                public double 量先車 { get; set; }
                public double PreSOP { get; set; }
                public double ランチェン { get; set; }
                public double クローズ { get; set; }
                public double 要調整 { get; set; }
                public double 空欄 { get; set; }

                // フォロー状況
                //Update Start 2020/12/24 杉浦
                //public double 調査中調査待ち { get; set; }
                //public double 現象再現しない { get; set; }
                //public double 本開発車ではこのままとしたい { get; set; }
                //public double 本開発車ではこのままとする_クローズ { get; set; }
                //public double その他 { get; set; }
                public double K3開発CAP_調査中調査待ち { get; set; }
                public double K3開発CAP_現象再現しない { get; set; }
                public double K3開発CAP_本開発車ではこのままとしたい { get; set; }
                public double K3開発CAP_本開発車ではこのままとする_クローズ { get; set; }
                public double K3開発CAP_その他 { get; set; }
                public double 図面CAP_改善する改善済み { get; set; }
                public double 図面CAP_K3実車で確認 { get; set; }
                public double 図面CAP_検討中 { get; set; }
                public double 図面CAP_本開発車ではこのままとしたい { get; set; }
                public double 図面CAP_本開発車ではこのままとする_クローズ { get; set; }
                //Update End 2020/12/24 杉浦
                public double 対応状況空欄 { get; set; }

                private List<string> order = new List<string>()
                {
                    // CAP確認時期
                    "K3",
                    "PUG工試",
                    "量産確認車",
                    "認証車",
                    "工試車",
                    "FT1",
                    "FT2",
                    "生試車",
                    "PPA",
                    "PPB",
                    "量先車",
                    "PreSOP",
                    "ランチェン",
                    "クローズ",
                    "要調整",
                    "空欄",
                    
                    //Update Start 2020/12/24 杉浦
                    // フォロー状況
                    //"調査中調査待ち",
                    //"現象再現しない",
                    //"本開発車ではこのままとしたい",
                    //"本開発車ではこのままとする_クローズ",
                    //"その他",
                    "K3開発CAP_調査中調査待ち",
                    "K3開発CAP_現象再現しない",
                    "K3開発CAP_本開発車ではこのままとしたい",
                    "K3開発CAP_本開発車ではこのままとする_クローズ",
                    "K3開発CAP_その他",
                    "図面CAP_改善する改善済み",
                    "図面CAP_K3実車で確認",
                    "図面CAP_検討中",
                    "図面CAP_本開発車ではこのままとしたい",
                    "図面CAP_本開発車ではこのままとする_クローズ",
                    //Update End 2020/12/24 杉浦
                    "対応状況空欄",
                };

                /// <summary>
                /// Excel出力列インデックス取得
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="e"></param>
                /// <returns></returns>
                public override int GetColIndex<T>(Expression<Func<T>> e)
                {
                    var member = (MemberExpression)e.Body;

                    // D列から出力するため + 3
                    return Order.FindIndex((x) => x == member.Member.Name) + 3;
                }

                protected override List<string> Order => order;

                /// <summary>
                /// 改善対象フォーロー
                /// </summary>
                private string[] targetFollws = new string[] { "改善する（設変）", "改善する（ﾁｭｰﾆﾝｸﾞ）", "改善する（玉成）", "再評価（欠品･暫定品）", "改善案検討中", "改善を確認（ｸﾛｰｽﾞ）" };

                #region データ追加
                /// <summary>
                /// データ追加
                /// </summary>
                /// <param name="row"></param>
                public void Add(Row row)
                {
                    var capStage = Convert.ToString(row.Cells["CAP確認時期"].Value);
                    var follow = Convert.ToString(row.Cells["フォロー状況"].Value);

                    // 改善対象
                    if (targetFollws.Contains(follow))
                    {
                        // CAP確認時期
                        switch (capStage)
                        {
                            case "開発完了確認車":
                                K3 += 1;
                                break;
                            case "PUG工試":
                                PUG工試 += 1;
                                break;
                            case "量産確認車":
                                量産確認車 += 1;
                                break;
                            case "認証車":
                                認証車 += 1;
                                break;
                            case "工試車":
                                工試車 += 1;
                                break;
                            case "F/T-1":
                                FT1 += 1;
                                break;
                            case "F/T-2":
                                FT2 += 1;
                                break;
                            case "生試車":
                                生試車 += 1;
                                break;
                            case "PP-A":
                                PPA += 1;
                                break;
                            case "PP-B":
                                PPB += 1;
                                break;
                            case "量先車":
                                量先車 += 1;
                                break;
                            case "PreSOP":
                                PreSOP += 1;
                                break;
                            case "ﾗﾝﾁｪﾝ":
                                ランチェン += 1;
                                break;
                            case "ｸﾛｰｽﾞ":
                                クローズ += 1;
                                break;
                            case "要調整":
                                要調整 += 1;
                                break;
                            case " ":
                            case "":
                                空欄 += 1;
                                break;
                        }
                    }

                    // フォロー状況
                    switch (follow)
                    {
                        //Update Start 2020/12/24 杉浦
                        //case "調査中／調査待ち":
                        //    開発CAP_調査中調査待ち += 1;
                        //    break;
                        //case "現象再現しない":
                        //    開発CAP_現象再現しない += 1;
                        //    break;
                        //case "本開発車ではこのままとしたい":
                        //case "今後の改善課題としたい":
                        //    開発CAP_本開発車ではこのままとしたい += 1;
                        //    break;
                        //case "本開発車ではこのままとする（ｸﾛｰｽﾞ）":
                        //case "今後の改善課題（ｸﾛｰｽﾞ）":
                        //    開発CAP_本開発車ではこのままとする_クローズ += 1;
                        //    break;
                        //case "その他":
                        //    開発CAP_その他 += 1;
                        //    break;
                        //case " ":
                        //case "":
                        //    対応状況空欄 += 1;
                        //    break;
                        case "調査中／調査待ち":
                            K3開発CAP_調査中調査待ち += 1;
                            break;
                        case "現象再現しない":
                            K3開発CAP_現象再現しない += 1;
                            break;
                        case "本開発車ではこのままとしたい":
                        case "今後の改善課題としたい":
                            K3開発CAP_本開発車ではこのままとしたい += 1;
                            break;
                        case "本開発車ではこのままとする（ｸﾛｰｽﾞ）":
                        case "今後の改善課題（ｸﾛｰｽﾞ）":
                            K3開発CAP_本開発車ではこのままとする_クローズ += 1;
                            break;
                        case "その他":
                            K3開発CAP_その他 += 1;
                            break;
                        case "改善する／改善済み":
                            図面CAP_改善する改善済み += 1;
                            break;
                        case "K3実車で確認":
                            図面CAP_K3実車で確認 += 1;
                            break;
                        case "検討中":
                            図面CAP_検討中 += 1;
                            break;
                        case "本開発車ではこのままとしたい ":
                            図面CAP_本開発車ではこのままとしたい += 1;
                            break;
                        case "本開発車ではこのままとする（クローズ）":
                            図面CAP_本開発車ではこのままとする_クローズ += 1;
                            break;
                        //Update End 2020/12/24 杉浦
                        case " ":
                        case "":
                            対応状況空欄 += 1;
                            break;
                    }
                }
                #endregion
            }
            #endregion
        }
        #endregion

        #region コマンドボタン共通処理クラス
        /// <summary>
        /// コマンドボタン共通処理クラス
        /// </summary>
        private class CmdBtn
        {
            private SelectedCellController _SelectedCell;

            public CmdBtn(SelectedCellController selectedCell)
            {
                _SelectedCell = selectedCell;
            }

            /// <summary>
            /// クリックイベントに共通処理を追加して登録します。
            /// </summary>
            /// <param name="btn">対象ボタン</param>
            /// <param name="action">クリックイベント実行処理内容</param>
            public void SetEvent(Button btn, System.Action action)
            {
                btn.Click += (sender, e) => CommonProcess(action);
            }

            /// <summary>
            /// 共通処理
            /// </summary>
            /// <param name="action">クリックイベント実行処理内容</param>
            private void CommonProcess(System.Action action)
            {
                if (_SelectedCell.IsSelected() == false)
                {
                    // 選択行がなければ終了
                    Messenger.Warn(Resources.KKM00009);
                    return;
                }

                if (_SelectedCell.IsOpen())
                {
                    // ステータスがOPENの場合のみクリックイベントを実行します。
                    action.Invoke();
                }
            }
        }
        #endregion

        #region 承認列クラス
        /// <summary>
        /// 承認列クラス
        /// </summary>
        private class ColApproval
        {
            private Authority _auth;
            private GcMultiRow _multiRow;

            public ColApproval(Authority auth, GcMultiRow multiRow)
            {
                _auth = auth;
                _multiRow = multiRow;
            }

            /// <summary>
            /// 判定（警告メッセージを表示）
            /// </summary>
            /// <param name="rowIndex"></param>
            /// <param name="colName"></param>
            public void Judge(int rowIndex, string colName)
            {
                if (colName != "承認")
                {
                    return;
                }

                // 所属チェック
                if (_auth.IsAdmin())
                {
                    if (_auth.IsMySection(Convert.ToString(_multiRow.Rows[rowIndex].Cells["専門部署名"].Value)) == false)
                    {
                        Messenger.Warn(string.Format(Resources.KKM03047, "所属課"));
                        return;
                    }
                }
                else
                {
                    if (_auth.IsMyDepartment(Convert.ToString(_multiRow.Rows[rowIndex].Cells["専門部名"].Value)) == false)
                    {
                        Messenger.Warn(string.Format(Resources.KKM03047, "所属部"));
                        return;
                    }
                }

                // 職制チェック
                if (_auth.IsJobSys() == false)
                {
                    Messenger.Warn(string.Format(Resources.KKM03048, "職制"));
                    return;
                }
            }
        }
        #endregion

        #region MultiRow編集開始イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集開始イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.VerticalResize(100);
            }

            var gcMultiRow = CapMultiRow;
            CapMultiRow.EditingControlShowing -= new EventHandler<EditingControlShowingEventArgs>(CapMultiRow_EditingControlShowing);
            CapMultiRow.EditingControlShowing += new EventHandler<EditingControlShowingEventArgs>(CapMultiRow_EditingControlShowing);

        }
        #endregion

        #region MultiRow編集終了イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集終了イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapMultiRow_CellEndEdit(object sender, CellEndEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.PerformVerticalAutoFit();
            }
        }
        #endregion


        //Update Start 2020/12/24 杉浦
        private void CapMultiRow_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            if(e.Control is TextBoxEditingControl)
            {
                TextBoxEditingControl text = e.Control as TextBoxEditingControl;
                text.KeyDown -= new KeyEventHandler(text_KeyDown);
                text.KeyDown += new KeyEventHandler(text_KeyDown);
            }
        }

        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                TextBoxEditingControl editor = (TextBoxEditingControl)CapMultiRow.EditingControl;
                editor.SelectionStart = 0;
            }
        }

        //Update End 2020/12/24 杉浦


        //Append Start 2021/06/15 矢作

        private void WayInputButton_Click(object sender, EventArgs e)
        {
            var list = GeneralCodeComboBox.Text?.Split(',');

            //Append Start 2021/08/27 杉浦 CAP要望対応
            if (string.IsNullOrEmpty(list[0]) && CapItemList != null)
            {
                var array = CapItemList.Select(x => x.GENERAL_CODE).Distinct().ToList();
                array.RemoveAll(x => string.IsNullOrEmpty(x));
                list = array.ToArray();
            }
            //Append End 2021/08/27 杉浦 CAP要望対応

            //Append Start 2021/08/20 杉浦 CAP要望対応
            if (!string.IsNullOrEmpty(list[0]))
            {
                //Append End 2021/08/20 杉浦 CAP要望対応
                using (var form = new WayConfirmDateForm { DATASOURCE1 = list })
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var idList = new List<long>();
                        //Append Start 2021/08/12 杉浦 CAP要望対応
                        if (CapItemList.Any(x => x.CAP種別 == form.SelectKind && x.GENERAL_CODE == form.GeneralCode))
                        {
                            //Append End 2021/08/12 杉浦 CAP要望対応
                            foreach (var i in CapItemList.Where(x => x.CAP種別 == form.SelectKind && x.GENERAL_CODE == form.GeneralCode))
                            {
                                idList.Add(i.ID);
                            }
                            //Append Start 2021/08/12 杉浦 CAP要望対応
                        }
                        else
                        {
                            Messenger.Warn(Resources.KKM00054);
                        }
                        //Append End 2021/08/12 杉浦 CAP要望対応

                        this.CapMultiRow.Rows.Where(x => idList.Contains((long)x.Cells["ID"].Value)).Select(x => x.Cells["方向付け確定期限"].Value = form.SelectDate).ToList();

                        // 変更フラグ設定
                        this.IsEdit = true;
                    }
                }
                //Append Start 2021/08/20 杉浦 CAP要望対応
            }
            else
            {
                // 選択行がなければ終了
                //Update Start 2021/08/27 杉浦 CAP要望対応
                //Messenger.Warn(Resources.KKM00009);
                Messenger.Warn(string.Format(Resources.KKM00001, "車種または専門部署名").Replace("入力", "選択"));
                //Update End 2021/08/27 杉浦 CAP要望対応
                return;
            }
            //Append End 2021/08/20 杉浦 CAP要望対応
        }

        //Append End 2021/06/15 矢作
    }
}
