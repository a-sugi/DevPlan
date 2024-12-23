using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UITestCar.ControlSheet;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Util;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UITestCar.Othe.CarInspection
{
    /// <summary>
    /// 車検・点検リスト発行
    /// </summary>
    public partial class CarInspectionForm : BaseTestCarForm
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
        private bool IsBind = false;

        /// <summary>
        /// 検索フラグ
        /// </summary>
        private bool IsSearch = true;

        /// <summary>
        /// 検索条件
        /// </summary>
        private CarInspectionSearchModel SearchCondition = new CarInspectionSearchModel();
        #endregion

        #region 定義
        private const int CondHeight = 120;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "車検・点検リスト発行"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarInspectionForm()
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
        private void CarInspectionForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.IsBind = true;

                //権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);

                //画面初期化
                this.InitForm();

                //MultiRow初期化
                this.InitMultiRow();

                //試験車一覧設定
                this.SetTestCarList();

                //バインド可否
                this.IsBind = false;
            });

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            //期間
            var next = DateTimeUtil.GetFirstDay().Value.AddMonths(1);
            this.StartDateTimePicker.Value = next;
            this.EndDateTimePicker.Value = DateTimeUtil.GetLastDay(next);

            //管理所在地
            FormControlUtil.SetComboBoxItem(this.AffiliationComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.Affiliation).Results, false);
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;

            //開発符号
            FormControlUtil.SetComboBoxItem(this.GeneralCodeComboBox, HttpUtil.GetResponse<CommonMasterModel>(ControllerType.GeneralCodeInfo).Results);

            //ダウンロードボタン
            this.DownloadButton.Visible = isExport;
        }

        /// <summary>
        /// MultiRow初期化
        /// </summary>
        private void InitMultiRow()
        {
            var template = new CarInspectionMultiRowTemplate();

            // カスタムテンプレート適用
            this.CustomTemplate.RowCountLabel = this.RowCountLabel;
            this.CustomTemplate.MultiRow = this.TestCarListMultiRow;
            this.CustomTemplate.ConfigDispLayList = base.GetUserDisplayConfiguration(template);
            this.TestCarListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(template);

            // データーソース
            this.TestCarListMultiRow.DataSource = this.DataSource;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarInspectionForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;

            this.ActiveControl = this.StartDateTimePicker;
            this.StartDateTimePicker.Focus();
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
            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, null, new List<Control> { this.ButtonPanel }, CondHeight);
        }
        #endregion

        #region 管理所在地選択
        /// <summary>
        /// 管理所在地選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.IsBind == true)
            {
                return;

            }

            //試験車一覧設定
            this.SetTestCarList();

        }
        #endregion

        #region 課マウスクリック
        /// <summary>
        /// 課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionListForm { DEPARTMENT_ID = SessionDto.DepartmentID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_ID,
                        NAME = form.SECTION_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionComboBox, new[] { value }, false);
                    this.SectionComboBox.SelectedIndex = 0;

                }

            }

        }
        #endregion

        #region 担当マウスクリック
        /// <summary>
        /// 担当マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;

            }

            using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_GROUP_ID,
                        NAME = form.SECTION_GROUP_CODE
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.SectionGroupComboBox, new[] { value }, false);
                    this.SectionGroupComboBox.SelectedIndex = 0;

                }

            }

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
            //試験車一覧設定
            this.SetTestCarList();

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
            //管理所在地
            this.AffiliationComboBox.SelectedValue = SessionDto.Affiliation;

            //期間
            var next = DateTimeUtil.GetFirstDay().Value.AddMonths(1);
            this.StartDateTimePicker.Value = next;
            this.EndDateTimePicker.Value = DateTimeUtil.GetLastDay(next);

            //管理票NO
            this.ManagementNoTextBox.Text = "";

            //課
            FormControlUtil.ClearComboBoxDataSource(this.SectionComboBox);

            //担当
            FormControlUtil.ClearComboBoxDataSource(this.SectionGroupComboBox);

            //開発符号
            this.GeneralCodeComboBox.Text = "";

            //外製車名
            this.OuterCarNameTextBox.Text = "";

            //試作時期
            this.TrialProductionSeasonTextBox.Text = "";

            //号車
            this.CarTextBox.Text = "";

            //駐車場番号
            this.ParkingNumberTextBox.Text = "";

            //登録ナンバー
            this.EntryNoTextBox.Text = "";

            //点検区分
            this.SearchCondition.点検区分 = null;

            //車検区分1
            this.SearchCondition.車検区分1 = null;

        }
        #endregion

        #region 抽出条件の設定ボタンクリック
        /// <summary>
        /// 抽出条件の設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionButton_Click(object sender, EventArgs e)
        {
            using (var form = new ExtractionConditionForm { SearchCondition = this.SearchCondition, IsSearch = this.IsSearch })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //検索するかどうか
                    this.IsSearch = form.IsSearch;
                    if (this.IsSearch == true)
                    {
                        //試験車一覧設定
                        this.SetTestCarList();

                    }

                }

            }

        }
        #endregion

        #region 試験車一覧
        /// <summary>
        /// 試験車一覧セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //無効な行の場合は終了
            if (e.RowIndex < 0)
            {
                return;
            }

            //試験車情報画面表示
            this.ShowTestCarForm(this.GetTestCarByMultiRow(e.RowIndex));
        }

        /// <summary>
        /// MultiRowから試験車を取得
        /// </summary>
        /// <param name="rowIndex">行インデックス</param>
        /// <returns></returns>
        private TestCarCommonModel GetTestCarByMultiRow(int rowIndex)
        {
            return this.TestCarListMultiRow.Rows[rowIndex].DataBoundItem as TestCarCommonModel;
        }

        /// <summary>
        /// 試験車情報画面表示
        /// </summary>
        /// <param name="testCar">試験車</param>
        private void ShowTestCarForm(TestCarCommonModel testCar)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new FormUtil(new ControlSheetIssueForm { TestCar = testCar, UserAuthority = this.UserAuthority }).SingleFormShow(this);
            });
        }
        #endregion

        #region エクセル出力ボタンクリック
        /// <summary>
        /// エクセル出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                new MultiRowUtil(this.TestCarListMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            });
        }
        #endregion

        #region 試験車一覧設定
        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetTestCarList(bool isKeepScroll = false)
        {
            // 検索条件のチェック
            if (!this.IsSearchCondition()) return;

            // 検索条件のセット
            this.SetSearchCondition();

            // 描画停止
            this.TestCarListMultiRow.SuspendLayout();

            // データの取得
            var list = this.GetTestCarList(this.SearchCondition);

            // バインドフラグ
            this.IsBind = true;

            // データバインド
            this.CustomTemplate.SetDataSource(list, this.DataSource);

            // バインドフラグ
            this.IsBind = false;

            // 描画再開
            this.TestCarListMultiRow.ResumeLayout();

            // 検索結果文言
            this.SearchResultLabel.Text = (list == null || list.Any() == false) ? Resources.KKM00005 : "";

            if (isKeepScroll)
            {
                return;
            }

            // スクロールの初期化
            this.TestCarListMultiRow.FirstDisplayedLocation = new Point(0, 0);

            // 一覧を未選択状態に設定
            this.TestCarListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// 検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchCondition()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            //期間の大小チェック
            map[this.StartDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toが入力してある場合のみチェック
                if (this.StartDateTimePicker.Value != null && this.EndDateTimePicker.Value != null)
                {
                    //開始日が終了日より大きい場合はエラー
                    if (this.StartDateTimePicker.SelectedDate > this.EndDateTimePicker.SelectedDate)
                    {
                        //エラーメッセージ
                        errMsg = Resources.KKM00018;

                        //背景色を変更
                        this.StartDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.EndDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
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


        /// <summary>
        /// 検索条件のセット
        /// </summary>
        /// <returns></returns>
        private void SetSearchCondition()
        {
            Func<ComboBox, string> getValue = cmb => cmb.SelectedIndex < 0 || cmb.SelectedValue == null ? null : cmb.SelectedValue.ToString();

            //期間(From)
            this.SearchCondition.START_DATE = this.StartDateTimePicker.SelectedDate;

            //期間(To)
            this.SearchCondition.END_DATE = this.EndDateTimePicker.SelectedDate;

            //管理所在地
            this.SearchCondition.ESTABLISHMENT = getValue(this.AffiliationComboBox);

            //管理票NO
            this.SearchCondition.管理票NO = this.ManagementNoTextBox.Text;

            //課ID
            this.SearchCondition.SECTION_ID = getValue(this.SectionComboBox);

            //担当ID
            this.SearchCondition.SECTION_GROUP_ID = getValue(this.SectionGroupComboBox);

            //開発符号
            this.SearchCondition.開発符号 = this.GeneralCodeComboBox.Text;

            //外製車名
            this.SearchCondition.外製車名 = this.OuterCarNameTextBox.Text;

            //試作時期
            this.SearchCondition.試作時期 = this.TrialProductionSeasonTextBox.Text;

            //号車
            this.SearchCondition.号車 = this.CarTextBox.Text;

            //駐車場番号
            this.SearchCondition.駐車場番号 = this.ParkingNumberTextBox.Text;

            //登録ナンバー
            this.SearchCondition.登録ナンバー = this.EntryNoTextBox.Text;
        }
        #endregion

        #region API
        /// <summary>
        /// 試験車取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private List<CarInspectionModel> GetTestCarList(CarInspectionSearchModel cond)
        {
            //APIで取得
            var res = HttpUtil.PostResponse<CarInspectionModel>(ControllerType.CarInspection, cond);

            //レスポンスが取得できたかどうか
            var list = new List<CarInspectionModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region MultiRow 共通操作

        /// <summary>
        /// MulutiRow設定アイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListConfigPictureBox_Click(object sender, EventArgs e)
        {
            // 表示設定画面表示
            base.ShowDisplayForm(this.CustomTemplate);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm(this.CustomTemplate);
        }

        #endregion
    }
}
