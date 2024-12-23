using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;

using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.BrowsingAuthority
{
    /// <summary>
    /// 閲覧権限設定
    /// </summary>
    public partial class BrowsingAuthorityForm : BaseForm
    {
        #region メンバ変数
        private BindingSource GeneralCodeAuthorityDataSource = new BindingSource();

        private CustomTemplate GeneralCodeAuthorityTemplate = new CustomTemplate();
        #endregion

        #region メンバ定数
        private readonly string[] BPDescription =
        {
            "派遣の方に「他部署閲覧設定」と「派遣・委託閲覧設定」の両方を設定すると",
            "派遣でも他部署の情報を閲覧することが可能となる。",
            "派遣の方が共同書き込み可能部署のデータを編集する必要がある場合に利用する。",
            "本機能はシステム管理者のみが利用できる。"
        };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "閲覧権限設定"; } }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }
        /// <summary>編集フラグ(他部署閲覧)</summary>
        private bool IsEditGeneralCode { get; set; } = false;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>初期アクセス</summary>
        private bool IsFirstGeneralCode { get; set; } = true;

        /// <summary>選択部ID</summary>
        private string DepartmentID { get; set; } = SessionDto.DepartmentID;

        /// <summary>検索条件(開発符号)</summary>
        public GeneralCodeSearchInModel ListSearchGeneralCodeCond { get; set; } = new GeneralCodeSearchInModel();
        
        /// <summary>検索条件(ユーザー)</summary>
        public UserSearchInModel ListSearchPersonelCond { get; set; } = new UserSearchInModel();
        
        /// <summary>検索条件(他部署閲覧)</summary>
        private GeneralCodeAuthorityInModel ListSearchGeneralCodeAuthorityCond { get; set; } = new GeneralCodeAuthorityInModel();

        /// <summary>開発符号リスト</summary>
        private List<GeneralCodeSearchOutModel> GeneralCodeDataList { get; set; } = new List<GeneralCodeSearchOutModel>();

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> PersonelDataList { get; set; } = new List<UserSearchOutModel>();

        /// <summary>他部署閲覧リスト</summary>
        private List<GeneralCodeAuthorityOutModel> GeneralCodeAuthorityDataList { get; set; } = new List<GeneralCodeAuthorityOutModel>();

        /// <summary>他部署閲覧マージリスト</summary>
        private List<GeneralCodeAuthorityOutModel> GeneralCodeAuthorityMergeDataList { get; set; } = new List<GeneralCodeAuthorityOutModel>();

        /// <summary>登録データリスト(他部署閲覧)</summary>
        private List<GeneralCodeAuthorityEntryModel> GeneralCodeAuthorityEntryDataList { get; set; } = new List<GeneralCodeAuthorityEntryModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BrowsingAuthorityForm()
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
        private void GeneralCodeAuthorityForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.BrowsingAuthority);

                //初期化:他部署閲覧
                this.InitGeneralCodeAuthorityProcess();

                //初期アクセス:他部署閲覧
                this.IsFirstGeneralCode = false;
            });
        }

        /// <summary>
        /// 初期化:他部署閲覧
        /// </summary>
        private void InitGeneralCodeAuthorityProcess()
        {
#if DEBUG
            // タブ背景色(個別対応)
            this.GeneralCodeAuthorityTabPage.BackColor
                = Color.FromArgb(255, 230, 255);
#endif

            // データグリッドビューの初期化
            this.InitGeneralCodeAuthorityDataGridView();

            // 画面初期化:他部署閲覧
            this.InitGeneralCodeAuthorityForm();

            // 検索条件設定:他部署閲覧
            this.SetSearchGeneralCodeAuthorityCond();

            // 一覧設定:他部署閲覧
            this.SearchGeneralCodeAuthority();
        }

        /// <summary>
        /// データグリッドビューの初期化:他部署閲覧
        /// </summary>
        private void InitGeneralCodeAuthorityDataGridView()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // バインド中ON
            this.IsBind = true;

            try
            {
                // テンプレート設定
                this.GeneralCodeAuthorityTemplate.MultiRow = this.GeneralCodeAuthorityMultiRow;
                this.GeneralCodeAuthorityTemplate.RowCountLabel = this.RowCountLabel;

                // テンプレート
                this.GeneralCodeAuthorityMultiRow.Template = this.GeneralCodeAuthorityTemplate.SetContextMenuTemplate(new GeneralCodeAuthorityTemplate());

                // データソース
                this.GeneralCodeAuthorityMultiRow.DataSource = this.GeneralCodeAuthorityDataSource;
            }
            finally
            {
                // 編集中OFF
                this.IsEditGeneralCode = false;

                // バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 画面初期化:他部署閲覧
        /// </summary>
        private void InitGeneralCodeAuthorityForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 現在スコープ外の内容が含まれているため2018/9/7時点では非表示
            this.DescriptionLinkLabel.Visible = false;

            // バインド中ON
            this.IsBind = true;

            try
            {
                // 担当課コンボボックス
                this.SectionComboBox.Visible = isManagement;
                this.SetSectionComboBox(SessionDto.DepartmentCode, SessionDto.SectionCode, SessionDto.SectionID);

                // 登録ボタン
                this.EntryButton.Visible = isUpdate || isManagement;

                // 一括設定ボタン
                this.WizardButton.Visible = isUpdate || isManagement;
            }
            finally
            {
                // 編集中OFF
                this.IsEditGeneralCode = false;

                // バインド中OFF
                this.IsBind = false;
            }
        }
        #endregion

        #region 検索条件設定
        /// <summary>
        /// 検索条件設定:他部署閲覧
        /// </summary>
        private void SetSearchGeneralCodeAuthorityCond()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 検索条件(開発符号権限)
            this.ListSearchGeneralCodeAuthorityCond = new GeneralCodeAuthorityInModel()
            {
                // 課ID
                SECTION_ID = !isManagement ? SessionDto.SectionID : this.SectionComboBox.SelectedValue.ToString(),
                // 請負関係課ID
                SECTION_RELATIONAL_ID = !isManagement ? SessionDto.SectionID : this.SectionComboBox.SelectedValue.ToString(),
                // ステータスコード
                STATUS_CODE = "a",
                // 開発フラグ
                UNDER_DEVELOPMENT = null,
                // 先開フラグ
                PRE_FLG = true,
                // 外注・派遣フラグ
                BP_FLG = true
            };

            // 検索条件(開発符号)
            this.ListSearchGeneralCodeCond = new GeneralCodeSearchInModel()
            {
                // 開発フラグ
                UNDER_DEVELOPMENT = null,
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId
            };

            // 検索条件(ユーザー)
            this.ListSearchPersonelCond = new UserSearchInModel()
            {
                // 課ID
                SECTION_ID = !isManagement ? new string[] { SessionDto.SectionID } : new string[] { this.SectionComboBox.SelectedValue.ToString() },
                // 請負関係課ID
                SECTION_RELATIONAL_ID = !isManagement ? new string[] { SessionDto.SectionID } : new string[] { this.SectionComboBox.SelectedValue.ToString() },
                // ステータスコード
                STATUS_CODE = "a"
            };
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 一覧設定:他部署閲覧
        /// </summary>
        private void SearchGeneralCodeAuthority()
        {
            //マージリスト取得
            this.GeneralCodeAuthorityMergeDataList = this.GetGeneralCodeAuthorityMergeList();

            //複製の作成
            var copyList = new List<GeneralCodeAuthorityOutModel>(this.GeneralCodeAuthorityMergeDataList);

            //調整
            copyList.RemoveAll(x =>
                x.UNDER_DEVELOPMENT == (!this.UnderDevelopmentCheckBox.Checked ? "XX" : "0") ||
                x.CAR_GROUP == (this.PreCheckBox.Checked ? "XX" : "先開") ||
                x.ACCESS_LEVEL == (this.BPCheckBox.Checked ? "XX" : "50") ||
                x.ACCESS_LEVEL == "00");

            //ユーザー一覧設定
            this.SetDataGridView(this.GeneralCodeAuthorityDataSource, this.GeneralCodeAuthorityMultiRow, copyList);

            // 編集フラグの初期化
            this.IsEditGeneralCode = false;
        }

        /// <summary>
        /// 一覧設定(汎用)
        /// </summary>
        private void SetDataGridView<T>(BindingSource ds, GcMultiRow mw, List<T> list)
        {
            //データのバインド
            this.GeneralCodeAuthorityTemplate.SetDataSource(list, ds);

            //一覧を未選択状態に設定
            mw.CurrentCell = null;
        }
        #endregion

        #region データの操作

        #region 表示データの取得
        /// <summary>
        /// 表示データ(他部署閲覧)の取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<GeneralCodeAuthorityOutModel> GetGeneralCodeAuthorityMergeList()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 初回アクセス以外は再検索しない
            if (!this.IsFirstGeneralCode)
            {
                return this.GeneralCodeAuthorityMergeDataList;
            }

            // 開発符号権限リストの取得
            this.GeneralCodeAuthorityDataList = this.GetGeneralCodeAuthorityList();

            // ユーザーリストの取得
            this.PersonelDataList = this.GetPersonelList();

            // 開発符号リストの取得
            this.GeneralCodeDataList = this.GetGeneralCodeList();

            // 他部署閲覧権限リストの初期化
            var list = new List<GeneralCodeAuthorityOutModel>();

            var keyFormat = "{0}-{1}";

            // 開発符号権限リストへのアクセスコスト最適化
            Dictionary<string, GeneralCodeAuthorityOutModel> authDictionary = 
                GeneralCodeAuthorityDataList.ToDictionary(x => string.Format(keyFormat, x.PERSONEL_ID, x.GENERAL_CODE));

            // ユーザーリストループ
            foreach (var personel in this.PersonelDataList)
            {
                // 開発符号リストループ
                foreach (var generalcode in this.GeneralCodeDataList)
                {
                    var val = new GeneralCodeAuthorityOutModel();
                    authDictionary.TryGetValue(string.Format(keyFormat, personel.PERSONEL_ID, generalcode.GENERAL_CODE), out val);

                    if (val == null)
                    {
                        val = new GeneralCodeAuthorityOutModel()
                        {
                            CAR_GROUP = generalcode.CAR_GROUP,
                            GENERAL_CODE = generalcode.GENERAL_CODE,
                            PERMISSION_PERIOD_START = null,
                            PERMISSION_PERIOD_END = null,
                            UNDER_DEVELOPMENT = generalcode.UNDER_DEVELOPMENT,
                            DEPARTMENT_ID = personel.DEPARTMENT_ID,
                            DEPARTMENT_CODE = personel.DEPARTMENT_CODE,
                            DEPARTMENT_NAME = personel.DEPARTMENT_NAME,
                            SECTION_ID = personel.SECTION_ID,
                            SECTION_CODE = personel.SECTION_CODE,
                            SECTION_NAME = personel.SECTION_NAME,
                            SECTION_GROUP_ID = personel.SECTION_GROUP_ID,
                            SECTION_GROUP_CODE = personel.SECTION_GROUP_CODE,
                            SECTION_GROUP_NAME = personel.SECTION_GROUP_NAME,
                            PERSONEL_ID = personel.PERSONEL_ID,
                            NAME = personel.NAME,
                            COMPANY = string.Empty, // 未使用
                            ACCESS_LEVEL = personel.ACCESS_LEVEL,
                            OFFICIAL_POSITION = personel.OFFICIAL_POSITION,
                            STATUS_CODE = personel.STATUS_CODE
                        };
                    }

                    list.Add(val);
                }
            }

            return list;
        }
        #endregion

        #region 登録データのチェック
        /// <summary>
        /// 登録データ(他部署閲覧)のチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsEntryGeneralCodeAuth()
        {
            //編集していなければ終了
            if (this.IsEditGeneralCode == false)
            {
                return false;
            }

            //登録対象が無ければ終了
            if (this.GeneralCodeAuthorityEntryDataList.Count <= 0)
            {
                return false;
            }

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            return true;
        }
        #endregion

        #region 登録データの取得
        /// <summary>
        /// 登録データ(他部署閲覧)の取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<GeneralCodeAuthorityEntryModel> GetEntryGeneralCodeAuth()
        {
            return this.GeneralCodeAuthorityEntryDataList;
        }
        #endregion

        #region 権限の登録
        /// <summary>
        /// 権限(他部署閲覧)の登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryGeneralCodeAuth()
        {
            // 入力チェック
            if (this.IsEntryGeneralCodeAuth() == false)
            {
                return false;
            }

            // Post実行
            var res = HttpUtil.PutResponse(ControllerType.GeneralCodeAuthority, this.GetEntryGeneralCodeAuth());

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 編集フラグ初期化
            this.IsEditGeneralCode = false;

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region 権限の登録（変更フラグチェック）
        /// <summary>
        /// 権限(他部署閲覧)の登録（変更フラグチェック）
        /// </summary>
        /// <returns></returns>
        private bool IsEditGeneralCodeAuthEntry()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 更新・管理権限なしは確認や更新処理を行わない。
            if (!isUpdate && !isManagement)
            {
                return true;
            }

            // 画面を変更していないか登録するかどうか
            if (this.IsEditGeneralCode == false || (this.IsEditGeneralCode == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;
            }

            // 更新
            return this.EntryGeneralCodeAuth();
        }
        #endregion

        #endregion

        #region データの取得

        #region 開発符号権限リストの取得
        /// <summary>
        /// 開発符号権限リストの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<GeneralCodeAuthorityOutModel> GetGeneralCodeAuthorityList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstGeneralCode)
            {
                return this.GeneralCodeAuthorityDataList;
            }

            //APIで取得
            return HttpUtil.GetResponse<GeneralCodeAuthorityInModel, GeneralCodeAuthorityOutModel>
                (ControllerType.GeneralCodeAuthority, this.ListSearchGeneralCodeAuthorityCond)?.Results?.ToList();
        }
        #endregion

        #region ユーザーリストの取得
        /// <summary>
        /// ユーザーリストの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<UserSearchOutModel> GetPersonelList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstGeneralCode)
            {
                return this.PersonelDataList;
            }

            //APIで取得
            return HttpUtil.GetResponse<UserSearchInModel, UserSearchOutModel>
                (ControllerType.User, this.ListSearchPersonelCond)?.Results?.ToList();
        }
        #endregion

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

            return HttpUtil.GetResponse<GeneralCodeSearchInModel, GeneralCodeSearchOutModel>
                (ControllerType.GeneralCode, this.ListSearchGeneralCodeCond)?.Results?.ToList();
        }
        #endregion

        #endregion

        #region イベント

        #region タブの選択
        /// <summary>
        /// タブの選択
        /// </summary>
        private void GeneralCodeAuthorityTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 現在未使用
        }
        #endregion

        #region タブの切り替え
        /// <summary>
        /// タブの切り替え
        /// </summary>
        private void GeneralCodeAuthorityTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 現在未使用
        }
        #endregion

        #region 画面クローズ
        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 他部署閲覧権限
            if (tabIndex == 0)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // グリッドの編集は終了
                    this.GeneralCodeAuthorityMultiRow.EndEdit();

                    // 画面を変更していて登録するかどうか
                    if (!this.IsEditGeneralCodeAuthEntry())
                    {
                        // 登録に失敗した場合は閉じさせない
                        e.Cancel = true;
                    }
                });
            }
        }
        #endregion

        #region フォームクリック
        /// <summary>
        /// フォームクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowsingAuthorityForm_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;
        }
        #endregion

        #region 検索条件パネルクリック
        /// <summary>
        /// 検索条件パネル(他部署閲覧)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonConditionPanel_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;
        }
        #endregion

        #region タブページクリック
        /// <summary>
        /// タブページ(他部署閲覧)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityTabPage_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;
        }
        #endregion

        #region データグリッドクリック
        /// <summary>
        /// データグリッド(他部署閲覧)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityMultiRow_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;

            if (((GcMultiRow)sender).CurrentCell != null && ((GcMultiRow)sender).CurrentCell is FilteringTextBoxCell)
            {
                ((GcMultiRow)sender).BeginEdit(true);
            }
        }
        #endregion

        #region メインパネルクリック
        /// <summary>
        /// メインパネル(他部署閲覧)クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityMainPanel_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;
        }
        #endregion

        #region 開発チェックボックスクリック
        /// <summary>
        /// 開発チェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnderDevelopmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 検索条件設定:他部署閲覧
                this.SetSearchGeneralCodeAuthorityCond();

                // 一覧設定:他部署閲覧
                this.SearchGeneralCodeAuthority();
            });
        }
        #endregion

        #region 先開チェックボックスクリック
        /// <summary>
        /// 先開チェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 検索条件設定:他部署閲覧
                this.SetSearchGeneralCodeAuthorityCond();

                // 一覧設定:他部署閲覧
                this.SearchGeneralCodeAuthority();
            });
        }
        #endregion

        #region 担当課マウスクリック
        /// <summary>
        /// 担当課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            using (var form = new SectionListForm { DEPARTMENT_ID = this.DepartmentID })
            {
                //担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // 選択部IDをセット
                    this.DepartmentID = form.DEPARTMENT_ID;

                    // 担当課をセット
                    this.SetSectionComboBox(form.DEPARTMENT_CODE, form.SECTION_CODE, form.SECTION_ID);

                    var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

                    // 他部署閲覧タブ
                    if (tabIndex == 0)
                    {
                        FormControlUtil.FormWait(this, () =>
                        {
                            this.IsFirstGeneralCode = true;

                            // 検索条件設定:他部署閲覧
                            this.SetSearchGeneralCodeAuthorityCond();

                            // 一覧設定:他部署閲覧
                            this.SearchGeneralCodeAuthority();
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 担当課を設定
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <param name="sectionCode"></param>
        /// <param name="sectionId"></param>
        private void SetSectionComboBox(string departmentCode, string sectionCode, string sectionId)
        {
            var value = new ComboBoxDto
            {
                ID = sectionId,
                NAME = string.Format("{0} {1}", departmentCode, sectionCode)
            };

            //担当をセット
            FormControlUtil.SetComboBoxItem(this.SectionComboBox, new[] { value }, false);
            this.SectionComboBox.SelectedIndex = 0;
        }
        #endregion

        #region 外注・派遣チェックボックスクリック
        /// <summary>
        /// 外注・派遣チェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 他部署閲覧タブ
            if (tabIndex == 0)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // 検索条件設定:他部署閲覧
                    this.SetSearchGeneralCodeAuthorityCond();

                    // 一覧設定:他部署閲覧
                    this.SearchGeneralCodeAuthority();
                });
            }
        }
        #endregion

        #region 説明リンクラベルクリック
        /// <summary>
        /// 説明リンクラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescriptionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DescriptionLabel.Visible = !this.DescriptionLabel.Visible;

            this.DescriptionLabel.Text = string.Join(Const.CrLf + Const.CrLf, BPDescription);
        }
        #endregion

        #region 説明ラベルクリック
        /// <summary>
        /// 説明ラベルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DescriptionLabel_Click(object sender, EventArgs e)
        {
            this.DescriptionLabel.Visible = false;
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
            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 他部署閲覧タブ
            if (tabIndex == 0)
            {
                // 権限(他部署閲覧)の登録
                FormControlUtil.FormWait(this, () => this.EntryGeneralCodeAuth());
            }
        }
        #endregion

        #region 一括設定ボタンクリック
        /// <summary>
        /// 一括設定ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WizardButton_Click(object sender, EventArgs e)
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var tabIndex = BrowsingAuthorityTabControl.SelectedIndex;

            // 他部署閲覧タブ
            if (tabIndex == 0)
            {
                // 一括設定ウィザードの起動
                using (var form = new BrowsingAuthorityBulkWizardForm() { UserAuthority = this.UserAuthority })
                {
                    form.ListSearchPersonelCond.SECTION_ID = !isManagement ? null : new string[] { this.SectionComboBox.SelectedValue.ToString() };

                    //OKかどうか
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        // 初回アクセスフラグ
                        this.IsFirstGeneralCode = true;

                        FormControlUtil.FormWait(this, () =>
                        {
                            // 現在のセル位置を退避
                            var position = this.GeneralCodeAuthorityMultiRow.FirstDisplayedCellPosition;

                            // 検索条件設定:他部署閲覧
                            this.SetSearchGeneralCodeAuthorityCond();

                            // 一覧設定:他部署閲覧
                            this.SearchGeneralCodeAuthority();

                            if (position.RowIndex > 0)
                            {
                                // MultiRowバグ対策
                                var maxCellIndex = this.GeneralCodeAuthorityMultiRow.Template.Row.Cells["EndDateColumn"].CellIndex;

                                // 退避したセル位置に移動
                                this.GeneralCodeAuthorityMultiRow.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
                            }
                        });
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 一覧のイベント
        /// <summary>
        /// セル値の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // 編集行の格納
            this.SetEntryDataList(((GcMultiRow)sender).Rows[e.RowIndex]);

            // 編集フラグON
            this.IsEditGeneralCode = true;
        }

        /// <summary>
        /// セルバリデーション
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeAuthorityMultiRow_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // バインド中は終了
            if (IsBind)
            {
                return;
            }

            var grid = ((GcMultiRow)sender);

            var col = grid.Columns[e.CellIndex];
            var name = col.Name;

            var row = grid.Rows[e.RowIndex];
            var cell = row.Cells[name];

            // 許可期間(開始)、許可期間(終了)以外
            if (name != "StartDateColumn" && name != "EndDateColumn")
            {
                return;
            }

            var startdate = name == "StartDateColumn" ? e.FormattedValue : row.Cells["StartDateColumn"].Value;
            var enddate = name == "EndDateColumn" ? e.FormattedValue : row.Cells["EndDateColumn"].Value;

            // 期間の大小チェック（両方が入力されている場合のみ）
            if (startdate != null && enddate != null)
            {
                if (Convert.ToDateTime(startdate) > Convert.ToDateTime(enddate))
                {
                    // 入力エラー文言表示
                    Messenger.Warn(Resources.KKM00018);

                    // キャンセル
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region データの操作
        /// <summary>
        /// 登録データリストの設定
        /// </summary>
        /// <param name="row"></param>
        private void SetEntryDataList(Row row)
        {
            var val = new GeneralCodeAuthorityEntryModel()
            {
                PERSONEL_ID = (string)row.Cells["SyokubanColumn"].Value,
                GENERAL_CODE = (string)row.Cells["GeneralCodeColumn"].Value,
                PERMISSION_PERIOD_START = (DateTime?)row.Cells["StartDateColumn"].Value,
                PERMISSION_PERIOD_END = (DateTime?)row.Cells["EndDateColumn"].Value
            };

            //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする
            val.ACCESS_LEVEL = this.PersonelDataList.Where(x => x.PERSONEL_ID == val.PERSONEL_ID).Select(x => x.ACCESS_LEVEL).FirstOrDefault();
            //Append Start 2021/06/08 杉浦 刷新版からも派遣者閲覧可能車種の設定をする

            // 重複行の削除
            this.GeneralCodeAuthorityEntryDataList.RemoveAll(x => x.PERSONEL_ID == val.PERSONEL_ID && x.GENERAL_CODE == val.GENERAL_CODE);

            // 編集行の追加
            this.GeneralCodeAuthorityEntryDataList.Add(val);
        }
        #endregion
    }
}