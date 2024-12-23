using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.BrowsingAuthority
{
    /// <summary>
    /// 一括設定(他部署閲覧)画面
    /// </summary>
    public partial class BrowsingAuthorityBulkWizardForm : BaseWizardForm
    {
        #region メンバ変数
        private BindingSource GeneralCodeDataSource = new BindingSource();
        private BindingSource PersonelDataSource = new BindingSource();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "閲覧権限一括設定ウィザード"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>初期アクセス</summary>
        private bool IsFirstGeneralCode { get; set; } = true;
        private bool IsFirstPersonel { get; set; } = true;

        /// <summary>検索条件(開発符号)</summary>
        public GeneralCodeSearchInModel ListSearchGeneralCodeCond { get; set; } = new GeneralCodeSearchInModel();
        /// <summary>検索条件(ユーザー)</summary>
        public UserSearchInModel ListSearchPersonelCond { get; set; } = new UserSearchInModel();

        /// <summary>開発符号リスト</summary>
        private List<GeneralCodeSearchOutModel> GeneralCodeDataList { get; set; } = new List<GeneralCodeSearchOutModel>();

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> PersonelDataList { get; set; } = new List<UserSearchOutModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BrowsingAuthorityBulkWizardForm()
        {
            InitializeComponent();

            this.InitWizardForm();
        }
        #endregion

        #region ウィザードフォーム初期化
        /// <summary>
        /// ウィザードフォーム初期化
        /// </summary>
        private void InitWizardForm()
        {
            // 親要素の変更
            this.WizardPanel1.Parent = base.WizardFormMainPanel;
            this.WizardPanel2.Parent = base.WizardFormMainPanel;
            this.WizardPanel3.Parent = base.WizardFormMainPanel;
            this.WizardPanel4.Parent = base.WizardFormMainPanel;
            this.WizardPanel5.Parent = base.WizardFormMainPanel;

            // タブの非表示
            this.WizardControl.Visible = false;
        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowsingAuthorityBulkWizardForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.BrowsingAuthority);

                //初期化
                this.InitWizard1Process();
            });
        }

        /// <summary>
        /// 初期化:ウィザード１(導入)
        /// </summary>
        private void InitWizard1Process()
        {
            //画面初期化:ウィザード１(導入)
            this.InitWizard1Form();
        }
        /// <summary>
        /// 初期化:ウィザード２(開発符号設定)
        /// </summary>
        private void InitWizard2Process()
        {
            //画面初期化:ウィザード２(開発符号設定)
            this.InitWizard2Form();

            // 初回のみ
            if (this.IsFirstGeneralCode)
            {
                //データグリッドビューの初期化:開発符号
                this.InitGeneralCodeDataGridView();

                //検索条件設定:開発符号
                this.SetSearchGeneralCodeCond();

                //一覧設定: 開発符号
                this.SearchGeneralCode();

                this.IsFirstGeneralCode = false;
            }
        }
        /// <summary>
        /// 初期化:ウィザード３(ユーザー設定)
        /// </summary>
        private void InitWizard3Process()
        {
            //画面初期化:ウィザード３(ユーザー設定)
            this.InitWizard3Form();

            // 初回のみ
            if (this.IsFirstPersonel)
            {
                //データグリッドビューの初期化:ユーザー
                this.InitPersonelDataGridView();

                //検索条件設定:ユーザー
                this.SetSearchPersonelCond();

                //一覧設定: ユーザー
                this.SearchPersonel();

                this.IsFirstPersonel = false;
            }
        }
        /// <summary>
        /// 初期化:ウィザード４(許可期間設定)
        /// </summary>
        private void InitWizard4Process()
        {
            //画面初期化:ウィザード４(許可期間設定)
            this.InitWizard4Form();
        }
        /// <summary>
        /// 初期化:ウィザード５(完了)
        /// </summary>
        private void InitWizard5Process()
        {
            //画面初期化:ウィザード５(完了)
            this.InitWizard5Form();
        }

        /// <summary>
        /// データグリッドビューの初期化:開発符号
        /// </summary>
        private void InitGeneralCodeDataGridView()
        {
            //列の自動生成可否
            this.GeneralCodeDataGridView.AutoGenerateColumns = false;

            //データーソース
            this.GeneralCodeDataGridView.DataSource = this.GeneralCodeDataSource;
        }

        /// <summary>
        /// データグリッドビューの初期化:ユーザー
        /// </summary>
        private void InitPersonelDataGridView()
        {
            //列の自動生成可否
            this.PersonelDataGridView.AutoGenerateColumns = false;

            //データーソース
            this.PersonelDataGridView.DataSource = this.PersonelDataSource;
        }

        /// <summary>
        /// 画面初期化:ウィザード１(導入)
        /// </summary>
        private void InitWizard1Form()
        {
            // パネル
            this.WizardPanel1.Visible = true;
            this.WizardPanel2.Visible = this.WizardPanel3.Visible = this.WizardPanel4.Visible = this.WizardPanel5.Visible = false;

            // 戻るボタン
            this.BeforeButton.Visible = false;

            // 次へボタン
            this.ForwardButton.Visible = true;

            // 完了ボタン
            this.EntryButton.Enabled = false;
        }
        /// <summary>
        /// 画面初期化:ウィザード２(開発符号設定)
        /// </summary>
        private void InitWizard2Form()
        {
            // パネル
            this.WizardPanel2.Visible = true;
            this.WizardPanel1.Visible = this.WizardPanel3.Visible = this.WizardPanel4.Visible = this.WizardPanel5.Visible = false;

            // 戻るボタン
            this.BeforeButton.Visible = true;

            // 次へボタン
            this.ForwardButton.Visible = true;

            // 完了ボタン
            this.EntryButton.Enabled = false;
        }
        /// <summary>
        /// 画面初期化:ウィザード３(ユーザー設定)
        /// </summary>
        private void InitWizard3Form()
        {
            // パネル
            this.WizardPanel3.Visible = true;
            this.WizardPanel1.Visible = this.WizardPanel2.Visible = this.WizardPanel4.Visible = this.WizardPanel5.Visible = false;

            // 戻るボタン
            this.BeforeButton.Visible = true;

            // 次へボタン
            this.ForwardButton.Visible = true;

            // 完了ボタン
            this.EntryButton.Enabled = false;
        }
        /// <summary>
        /// 画面初期化:ウィザード４(許可期間設定)
        /// </summary>
        private void InitWizard4Form()
        {
            // パネル
            this.WizardPanel4.Visible = true;
            this.WizardPanel1.Visible = this.WizardPanel2.Visible = this.WizardPanel3.Visible = this.WizardPanel5.Visible = false;

            // 戻るボタン
            this.BeforeButton.Visible = true;

            // 次へボタン
            this.ForwardButton.Visible = true;

            // 完了ボタン
            this.EntryButton.Enabled = false;
        }
        /// <summary>
        /// 画面初期化:ウィザード５(完了)
        /// </summary>
        private void InitWizard5Form()
        {
            // パネル
            this.WizardPanel5.Visible = true;
            this.WizardPanel1.Visible = this.WizardPanel2.Visible = this.WizardPanel3.Visible = this.WizardPanel4.Visible = false;

            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // 戻るボタン
            this.BeforeButton.Visible = true;

            // 次へボタン
            this.ForwardButton.Visible = false;

            // 完了ボタン
            this.EntryButton.Enabled = isUpdate;
        }
        #endregion

        #region 検索条件設定
        /// <summary>
        /// 検索条件設定:開発符号
        /// </summary>
        private void SetSearchGeneralCodeCond()
        {
            // 開発フラグ
            this.ListSearchGeneralCodeCond = new GeneralCodeSearchInModel()
            {
                // 開発フラグ
                UNDER_DEVELOPMENT = null,

                // ユーザーID
                PERSONEL_ID = SessionDto.UserId
            };
        }
        /// <summary>
        /// 検索条件設定:ユーザー
        /// </summary>
        private void SetSearchPersonelCond()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            this.ListSearchPersonelCond = new UserSearchInModel()
            {
                // 課ID
                SECTION_ID = !isManagement ? new string[] { SessionDto.SectionID } : this.ListSearchPersonelCond.SECTION_ID,

                // 請負関係課ID
                SECTION_RELATIONAL_ID = !isManagement ? new string[] { SessionDto.SectionID } : this.ListSearchPersonelCond.SECTION_ID,

                // ステータスコード
                STATUS_CODE = "a"
            };
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 一覧設定:開発符号
        /// </summary>
        private void SearchGeneralCode()
        {
            //開発符号リスト取得
            this.GeneralCodeDataList = this.GetGeneralCodeList();

            //複製の作成
            var copyList = new List<GeneralCodeSearchOutModel>(this.GeneralCodeDataList);

            //調整
            copyList.RemoveAll(x => x.UNDER_DEVELOPMENT == (!this.UnderDevelopmentCheckBox.Checked ? "XX" : "0"));

            //開発符号一覧設定
            this.SetDataGridView(this.GeneralCodeDataSource, this.GeneralCodeDataGridView, copyList);
        }

        /// <summary>
        /// 一覧設定:ユーザー
        /// </summary>
        private void SearchPersonel()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //ユーザーリスト取得
            this.PersonelDataList = this.GetPersonelList();

            //複製の作成
            var copyList = new List<UserSearchOutModel>(this.PersonelDataList);

            //調整
            copyList.RemoveAll(x => x.ACCESS_LEVEL == "00");

            //ユーザー一覧設定
            this.SetDataGridView(this.PersonelDataSource, this.PersonelDataGridView, copyList);
        }

        /// <summary>
        /// 一覧設定(汎用)
        /// </summary>
        private void SetDataGridView<T>(BindingSource ds, DataGridView dw, List<T> list)
        {
            //元の設定値を取得
            var autoSizeColumnsMode = dw.AutoSizeColumnsMode;
            var autoSizeRowsMode = dw.AutoSizeRowsMode;
            var columnHeadersHeightSizeMode = dw.ColumnHeadersHeightSizeMode;

            //行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない
            dw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dw.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            //グリッドクリア
            dw.Rows.Clear();

            //データのバインド
            ds.DataSource = list;

            //元の設定値を復元
            dw.AutoSizeColumnsMode = autoSizeColumnsMode;
            dw.AutoSizeRowsMode = autoSizeRowsMode;
            dw.ColumnHeadersHeightSizeMode = columnHeadersHeightSizeMode;

            //一覧を未選択状態に設定
            dw.CurrentCell = null;
        }
        #endregion

        #region データの操作

        #region 登録データのチェック
        /// <summary>
        /// 登録データのチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsEntryForward(int tabIndex)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            switch (tabIndex)
            {
                // ウィザード２(開発符号設定)
                case 1:

                    //対象が選択されているかどうか
                    if (this.GeneralCodeDataGridView.SelectedRows.Count <= 0)
                    {
                        //選択されていない場合はエラー
                        Messenger.Warn(Resources.KKM00009);
                        return false;
                    }
                    break;

                // ウィザード３(ユーザー設定)
                case 2:

                    //対象が選択されているかどうか
                    if (this.PersonelDataGridView.SelectedRows.Count <= 0)
                    {
                        //選択されていない場合はエラー
                        Messenger.Warn(Resources.KKM00009);
                        return false;
                    }
                    break;

                // ウィザード４(許可期間設定)
                case 3:

                    //期間の大小チェック
                    map[this.EndDayDateTimePicker] = (c, name) =>
                    {
                        var errMsg = "";

                        //期間Fromと期間Toがすべて入力してある場合のみチェック
                        if (this.StartDayDateTimePicker.Value != null && this.EndDayDateTimePicker.Value != null)
                        {
                            //開始日が終了日より大きい場合はエラー
                            if (Convert.ToDateTime(this.StartDayDateTimePicker.Value) 
                                > Convert.ToDateTime(this.EndDayDateTimePicker.Value))
                            {
                                //エラーメッセージ
                                errMsg = Resources.KKM00018;

                                //背景色を変更
                                this.StartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                                this.EndDayDateTimePicker.BackColor = Const.ErrorBackColor;
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
                    break;
            }

            return true;
        }
        #endregion

        #region 登録データの取得
        /// <summary>
        /// 登録データの取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<GeneralCodeAuthorityEntryModel> GetEntryGeneralCodeAuth()
        {
            //Append Start 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理
            //ユーザーリスト取得
            var personelList = this.GetPersonelList();
            //Append End 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理

            var list = new List<GeneralCodeAuthorityEntryModel>();

            foreach (DataGridViewRow row1 in this.GeneralCodeDataGridView.SelectedRows)
            {

                foreach (DataGridViewRow row2 in this.PersonelDataGridView.SelectedRows)
                {
                    //Append Start 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理
                    //LINQでアクセスレベル取得
                    var accessLevel = personelList
                       .Where(x => x.PERSONEL_ID == (string)row2.Cells[this.PersonelIdColumn.Name].Value)
                       .Select(x => x.ACCESS_LEVEL)
                       .FirstOrDefault();
                    //Append End 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理

                    // 追加
                    list.Add(new GeneralCodeAuthorityEntryModel()
                    {
                        // 開発符号
                        GENERAL_CODE = (string)row1.Cells[this.GeneralCodeColumn.Name].Value,
                        // 許可期間(開始)
                        PERMISSION_PERIOD_START = (DateTime)this.StartDayDateTimePicker.Value,
                        // 許可期間(終了)
                        PERMISSION_PERIOD_END = (DateTime)this.EndDayDateTimePicker.Value,
                        // ユーザーID
                        PERSONEL_ID = (string)row2.Cells[this.PersonelIdColumn.Name].Value,

                        //Append Start 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理
                        //アクセスレベル
                        ACCESS_LEVEL = accessLevel
                        //Append End 2024/09/06 早見 派遣者閲覧可能車種への追加登録用の処理
                    });
                }
            }

            return list;
        }
        #endregion

        #region データの登録
        /// <summary>
        /// データの登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryGeneralCodeAuth()
        {
            // 登録対象の取得
            var list = this.GetEntryGeneralCodeAuth();

            // Put実行
            var res = HttpUtil.PutResponse(ControllerType.GeneralCodeAuthority, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #endregion

        #region データの取得

        #region 開発符号の取得
        /// <summary>
        /// 開発符号の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<GeneralCodeSearchOutModel> GetGeneralCodeList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstGeneralCode)
            {
                return this.GeneralCodeDataList;
            }

            return HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>(ControllerType.GeneralCode, this.ListSearchGeneralCodeCond)?.Results?.ToList();
        }
        #endregion

        #region ユーザーの取得
        /// <summary>
        /// ユーザーの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<UserSearchOutModel> GetPersonelList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstPersonel)
            {
                return this.PersonelDataList;
            }

            return HttpUtil.GetResponse<UserSearchInModel, UserSearchOutModel>(ControllerType.User, this.ListSearchPersonelCond)?.Results?.ToList();
        }
        #endregion

        #endregion

        #region イベント

        #region 開発中チェックボックス値変更
        /// <summary>
        /// 開発中チェックボックス値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderDevelopmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //一覧設定: 開発符号
                this.SearchGeneralCode();
            });
        }
        #endregion

        #region 戻るボタンクリック
        /// <summary>
        /// 戻るボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeforeButton_Click(object sender, EventArgs e)
        {
            var tabIndex = this.WizardControl.SelectedIndex;

            switch (tabIndex)
            {
                // ウィザード２(開発符号設定)
                case 1:
                    this.InitWizard1Process();
                    break;
                // ウィザード３(ユーザー設定)
                case 2:
                    this.InitWizard2Process();
                    break;
                // ウィザード４(許可期間設定)
                case 3:
                    this.InitWizard3Process();
                    break;
                // ウィザード５(完了)
                case 4:
                    this.InitWizard4Process();
                    break;
            }

            this.WizardControl.SelectedIndex = tabIndex - 1;
        }
        #endregion

        #region 次へボタンクリック
        /// <summary>
        /// 次へボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForwardButton_Click(object sender, EventArgs e)
        {
            var tabIndex = this.WizardControl.SelectedIndex;

            // 入力チェック
            if (!this.IsEntryForward(tabIndex))
            {
                return;
            }

            switch (tabIndex)
            {
                // ウィザード１(導入)
                case 0:
                    this.InitWizard2Process();
                    break;
                // ウィザード２(開発符号設定)
                case 1:
                    this.InitWizard3Process();
                    break;
                // ウィザード３(ユーザー設定)
                case 2:
                    this.InitWizard4Process();
                    break;
                // ウィザード４(許可期間設定)
                case 3:
                    this.InitWizard5Process();
                    break;
            }

            this.WizardControl.SelectedIndex = tabIndex + 1;
        }
        #endregion

        #region 完了ボタンクリック
        /// <summary>
        /// 完了ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 登録実行
                if (this.EntryGeneralCodeAuth())
                {
                    // フォームクローズ
                    base.FormOkClose();
                }
            });
        }
        #endregion

        #endregion
    }
}