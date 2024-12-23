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
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.UIDevPlan.FunctionAuthority
{
    /// <summary>
    /// 機能権限(ロール)設定
    /// </summary>
    public partial class FunctionAuthorityRollForm : BaseForm
    {
        #region メンバ変数
        private BindingSource SinkPersonelDataSource = new BindingSource();
        private BindingSource SinkDepartmentDataSource = new BindingSource();

        private BindingSource SourcePersonelDataSource = new BindingSource();
        private BindingSource SourceDepartmentDataSource = new BindingSource();
        private BindingSource SourceOfficialPositionDataSource = new BindingSource();

        private CustomTemplate SinkPersonelTemplate = new CustomTemplate();
        private CustomTemplate SinkDepartmentTemplate = new CustomTemplate();

        private CustomTemplate SourcePersonelTemplate = new CustomTemplate();
        private CustomTemplate SourceDepartmentTemplate = new CustomTemplate();
        private CustomTemplate SourceOfficialPositionTemplate = new CustomTemplate();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "機能権限設定"; } }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }
        /// <summary>編集フラグ(人)</summary>
        private bool IsEditPersonel { get; set; } = false;
        /// <summary>編集フラグ(部)</summary>
        private bool IsEditDepartment { get; set; } = false;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>初期アクセス</summary>
        private bool IsFirstPersonel { get; set; } = true;
        private bool IsFirstDepartment { get; set; } = true;

        /// <summary>検索条件</summary>
        private RollAuthorityGetInModel ListSearchPersonelCond { get; set; } = new RollAuthorityGetInModel();
        private RollAuthorityGetInModel ListSearchDepartmentCond { get; set; } = new RollAuthorityGetInModel();

        /// <summary>ロール所属リスト(人)</summary>
        private List<RollAuthorityGetOutModel> SinkPersonelDataList { get; set; } = new List<RollAuthorityGetOutModel>();
        /// <summary>ロール所属リスト(部)</summary>
        private List<RollAuthorityGetOutModel> SinkDepartmentDataList { get; set; } = new List<RollAuthorityGetOutModel>();

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> PersonelDataList { get; set; } = new List<UserSearchOutModel>();
        /// <summary>部署リスト</summary>
        private List<SectionGroupModel> DepartmentDataList { get; set; } = new List<SectionGroupModel>();
        /// <summary>役職リスト</summary>
        private List<OfficialPositionModel> OfficialPositionDataList { get; set; } = new List<OfficialPositionModel>();

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> SourcePersonelDataList { get; set; } = new List<UserSearchOutModel>();
        /// <summary>部署リスト</summary>
        private List<SectionGroupModel> SourceDepartmentDataList { get; set; } = new List<SectionGroupModel>();
        /// <summary>役職リスト</summary>
        private List<OfficialPositionModel> SourceOfficialPositionDataList { get; set; } = new List<OfficialPositionModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FunctionAuthorityRollForm()
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
        private void FunctionAuthorityRollForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.FunctionAuthority);

                //初期化:人
                this.InitPersonelProcess();

                //初期アクセス:人
                this.IsFirstPersonel = false;
            });
        }

        /// <summary>
        /// 初期化:人
        /// </summary>
        private void InitPersonelProcess()
        {
#if DEBUG
            // タブ背景色(個別対応)
            this.PersonelTabPage.BackColor 
                = this.DepartmentTabPage.BackColor 
                = Color.FromArgb(255, 230, 255);
#endif
            // データグリッドビューの初期化
            this.InitPersonelDataGridView();

            // 画面初期化:人
            this.InitPersonelForm();

            // 検索条件設定:人
            this.SetSearchPersonelCond();

            // 一覧設定:人
            this.SearchPersonel();
        }

        /// <summary>
        /// 初期化:部
        /// </summary>
        private void InitDepartmentProcess()
        {
            //データグリッドビューの初期化
            this.InitDepartmentDataGridView();

            //画面初期化:部
            this.InitDepartmentForm();

            //検索条件設定:部
            this.SetSearchDepartmentCond();

            //一覧設定:部
            this.SearchDepartment();
        }

        /// <summary>
        /// データグリッドビューの初期化:人
        /// </summary>
        private void InitPersonelDataGridView()
        {
            // プロパティ
            this.SourcePersonelTemplate.MultiRow = this.SourcePersonelMultiRow;
            this.SourcePersonelTemplate.RowCountLabel = this.SourcePersonelRowCountLabel;
            this.SinkPersonelTemplate.MultiRow = this.SinkPersonelMultiRow;
            this.SinkPersonelTemplate.RowCountLabel = this.SinkPersonelRowCountLabel;

            // テンプレート
            this.SourcePersonelMultiRow.Template = this.SourcePersonelTemplate.SetContextMenuTemplate(new SourcePersonelMultiRowTemplate());
            this.SinkPersonelMultiRow.Template = this.SinkPersonelTemplate.SetContextMenuTemplate(new SinkPersonelMultiRowTemplate());

            // データーソース
            this.SourcePersonelMultiRow.DataSource = this.SourcePersonelDataSource;
            this.SinkPersonelMultiRow.DataSource = this.SinkPersonelDataSource;
        }

        /// <summary>
        /// データグリッドビューの初期化:部
        /// </summary>
        private void InitDepartmentDataGridView()
        {
            // プロパティ
            this.SourceDepartmentTemplate.MultiRow = this.SourceDepartmentMultiRow;
            this.SourceDepartmentTemplate.RowCountLabel = this.SourceDepartmentRowCountLabel;
            this.SourceOfficialPositionTemplate.MultiRow = this.SourceOfficialPositionMultiRow;
            this.SourceOfficialPositionTemplate.RowCountLabel = this.SourceOfficialPositionRowCountLabel;
            this.SinkDepartmentTemplate.MultiRow = this.SinkDepartmentMultiRow;
            this.SinkDepartmentTemplate.RowCountLabel = this.SinkDepartmentRowCountLabel;

            // テンプレート
            this.SourceDepartmentMultiRow.Template = this.SourceDepartmentTemplate.SetContextMenuTemplate(new SourceDepartmentMultiRowTemplate());
            this.SourceOfficialPositionMultiRow.Template = this.SourceOfficialPositionTemplate.SetContextMenuTemplate(new SourceOfficialPositionMultiRowTemplate());
            this.SinkDepartmentMultiRow.Template = this.SinkDepartmentTemplate.SetContextMenuTemplate(new SinkDepartmentMultiRowTemplate());

            // データーソース
            this.SourceDepartmentMultiRow.DataSource = this.SourceDepartmentDataSource;
            this.SourceOfficialPositionMultiRow.DataSource = this.SourceOfficialPositionDataSource;
            this.SinkDepartmentMultiRow.DataSource = this.SinkDepartmentDataSource;
        }

        /// <summary>
        /// 画面初期化:人
        /// </summary>
        private void InitPersonelForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //バインド中ON
            this.IsBind = true;

            try
            {
                // ロール選択リスト
                FormControlUtil.SetComboBoxItem(this.PersonelRollComboBox, "ROLL_ID", "ROLL_NAME", GetRollNameList(), false);

                // 追加(>>)ボタン
                this.PersonelAddButton.Visible = isUpdate || isManagement;

                // 削除(<<)ボタン
                this.PersonelDeleteButton.Visible = isUpdate || isManagement;

                // 選択解除ボタン
                this.PersonelClearButton.Visible = isUpdate || isManagement;

                // 登録ボタン
                this.EntryButton.Visible = isUpdate || isManagement;
            }
            finally
            {
                // 編集中OFF
                this.IsEditPersonel = false;

                // バインド中OFF
                this.IsBind = false;
            }
        }
        /// <summary>
        /// 画面初期化:部
        /// </summary>
        private void InitDepartmentForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            //バインド中ON
            this.IsBind = true;

            try
            {
                // ロール選択リスト
                FormControlUtil.SetComboBoxItem(this.DepartmentRollComboBox, "ROLL_ID", "ROLL_NAME", GetRollNameList(), false);

                // 追加(>>)ボタン
                this.DepartmentAddButton.Visible = isUpdate || isManagement;

                // 削除(<<)ボタン
                this.DepartmentDeleteButton.Visible = isUpdate || isManagement;

                // 選択解除ボタン
                this.DepartmentClearButton.Visible = isUpdate || isManagement;

                // 登録ボタン
                this.EntryButton.Visible = isUpdate || isManagement;
            }
            finally
            {
                // 編集中OFF
                this.IsEditDepartment = false;

                // バインド中OFF
                this.IsBind = false;
            }
        }
        #endregion

        #region 検索条件設定
        /// <summary>
        /// 検索条件設定:人
        /// </summary>
        private void SetSearchPersonelCond()
        {
            // ロールID
            this.ListSearchPersonelCond.ROLL_ID = (long?)this.PersonelRollComboBox.SelectedValue > 0 ? (long?)this.PersonelRollComboBox.SelectedValue : null;
        }
        /// <summary>
        /// 検索条件設定:部
        /// </summary>
        private void SetSearchDepartmentCond()
        {
            // ロールID
            this.ListSearchDepartmentCond.ROLL_ID = (long?)this.DepartmentRollComboBox.SelectedValue > 0 ? (long?)this.DepartmentRollComboBox.SelectedValue : null;
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// 一覧設定:人
        /// </summary>
        private void SearchPersonel()
        {
            this.SearchSinkPersonel();
            this.SearchSourcePersonel();

            this.IsEditPersonel = false;
        }
        /// <summary>
        /// 一覧設定:人
        /// </summary>
        private void SearchDepartment()
        {
            this.SearchSinkDepartment();
            this.SearchSourceDepartment();
            this.SearchOfficialPosition();

            this.IsEditDepartment = false;
        }

        /// <summary>
        /// ロール権限(人)一覧設定
        /// </summary>
        private void SearchSinkPersonel()
        {
            //ロール権限(人)リスト取得
            this.SinkPersonelDataList = this.GetFunctionAuthorityRollPersonelList();

            //ロール権限(人)一覧設定
            this.SetDataGridView(this.SinkPersonelDataSource, this.SinkPersonelMultiRow, this.SinkPersonelDataList);
        }
        /// <summary>
        /// ロール権限(部)一覧設定
        /// </summary>
        private void SearchSinkDepartment()
        {
            //ロール権限(部)リスト取得
            this.SinkDepartmentDataList = this.GetFunctionAuthorityRollDepartmentList();

            //ロール権限(部)一覧設定
            this.SetDataGridView(this.SinkDepartmentDataSource, this.SinkDepartmentMultiRow, this.SinkDepartmentDataList);
        }
        /// <summary>
        /// ユーザー一覧設定
        /// </summary>
        private void SearchSourcePersonel()
        {
            //ユーザーリスト取得
            this.PersonelDataList = this.GetPersonelList();

            //複製の作成
            this.SourcePersonelDataList = new List<UserSearchOutModel>(this.PersonelDataList);

            //重複ユーザー削除
            this.SourcePersonelDataList.RemoveAll(x => this.SinkPersonelDataList.Any(y => y.PERSONEL_ID == x.PERSONEL_ID));

            //ユーザー一覧設定
            this.SetDataGridView(this.SourcePersonelDataSource, this.SourcePersonelMultiRow, this.SourcePersonelDataList);
        }
        /// <summary>
        /// 部署一覧設定
        /// </summary>
        private void SearchSourceDepartment()
        {
            //部署リスト取得
            this.DepartmentDataList = this.GetDepartmentList();

            //複製の作成
            this.SourceDepartmentDataList = new List<SectionGroupModel>(this.DepartmentDataList);

            //重複部署削除
            this.SourceDepartmentDataList.RemoveAll(x => this.SinkDepartmentDataList.Any(y => y.DEPARTMENT_ID == x.DEPARTMENT_ID && y.SECTION_ID == x.SECTION_ID && y.SECTION_GROUP_ID == x.SECTION_GROUP_ID && string.IsNullOrWhiteSpace(y.OFFICIAL_POSITION)));
            this.SourceDepartmentDataList.RemoveAll(x => this.SinkDepartmentDataList.Any(y => y.DEPARTMENT_ID == x.DEPARTMENT_ID && y.SECTION_ID == x.SECTION_ID && string.IsNullOrWhiteSpace(y.SECTION_GROUP_ID) && string.IsNullOrWhiteSpace(y.OFFICIAL_POSITION)));
            this.SourceDepartmentDataList.RemoveAll(x => this.SinkDepartmentDataList.Any(y => y.DEPARTMENT_ID == x.DEPARTMENT_ID && string.IsNullOrWhiteSpace(y.SECTION_ID) && string.IsNullOrWhiteSpace(y.SECTION_GROUP_ID) && string.IsNullOrWhiteSpace(y.OFFICIAL_POSITION)));

            //部署一覧設定
            this.SetDataGridView(this.SourceDepartmentDataSource, this.SourceDepartmentMultiRow, this.SourceDepartmentDataList);
        }
        /// <summary>
        /// 役職一覧設定
        /// </summary>
        private void SearchOfficialPosition()
        {
            //役職リスト取得
            this.OfficialPositionDataList = this.GetOfficialPositionList();

            //複製の作成
            this.SourceOfficialPositionDataList = new List<OfficialPositionModel>(this.OfficialPositionDataList);

            //重複役職削除
            this.SourceOfficialPositionDataList.RemoveAll(x => this.SinkDepartmentDataList.Any(y => string.IsNullOrWhiteSpace(y.DEPARTMENT_ID) && string.IsNullOrWhiteSpace(y.SECTION_ID) && string.IsNullOrWhiteSpace(y.SECTION_GROUP_ID) && y.OFFICIAL_POSITION == x.OFFICIAL_POSITION));

            //役職一覧設定
            this.SetDataGridView(this.SourceOfficialPositionDataSource, this.SourceOfficialPositionMultiRow, this.SourceOfficialPositionDataList);
        }

        /// <summary>
        /// 一覧設定(汎用)
        /// </summary>
        private void SetDataGridView<T>(BindingSource ds, GcMultiRow dw, List<T> list)
        {
            // データのバインド
            if (dw.Template is SourcePersonelMultiRowTemplate)
            {
                this.SourcePersonelTemplate.SetDataSource(list, ds);
            }
            if (dw.Template is SinkPersonelMultiRowTemplate)
            {
                this.SinkPersonelTemplate.SetDataSource(list, ds);
            }
            if (dw.Template is SourceDepartmentMultiRowTemplate)
            {
                this.SourceDepartmentTemplate.SetDataSource(list, ds);
            }
            if (dw.Template is SourceOfficialPositionMultiRowTemplate)
            {
                this.SourceOfficialPositionTemplate.SetDataSource(list, ds);
            }
            if (dw.Template is SinkDepartmentMultiRowTemplate)
            {
                this.SinkDepartmentTemplate.SetDataSource(list, ds);
            }

            // 一覧を未選択状態に設定
            dw.CurrentCell = null;
        }
        #endregion

        #region データの操作

        #region 登録データのチェック
        /// <summary>
        /// 登録データ(人)のチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsEntryPersonelRollAuth()
        {
            //編集していなければ終了
            if (this.IsEditPersonel == false)
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

            //対象が選択されているかどうか
            if (this.ListSearchPersonelCond.ROLL_ID == null)
            {
                //選択されていない場合はエラー
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            //対象が存在しているかどうか
            if (this.GetRollNameList(new RollGetInModel { ROLL_ID = (long)this.ListSearchPersonelCond.ROLL_ID }) == null)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 登録データ(部)のチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsEntryDepartmentRollAuth()
        {
            //編集していなければ終了
            if (this.IsEditDepartment == false)
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

            //対象が選択されているかどうか
            if (this.ListSearchDepartmentCond.ROLL_ID == null)
            {
                //選択されていない場合はエラー
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            //対象が存在しているかどうか
            if (this.GetRollNameList(new RollGetInModel { ROLL_ID = (long)this.ListSearchDepartmentCond.ROLL_ID }) == null)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;
            }

            return true;
        }
        #endregion

        #region 登録データの取得
        /// <summary>
        /// 登録データ(人)の取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<RollAuthorityPostInModel> GetEntryPersonelRollAuth()
        {
            var list = new List<RollAuthorityPostInModel>();

            foreach (var row in this.SinkPersonelMultiRow.Rows)
            {
                list.Add(new RollAuthorityPostInModel()
                {
                    // ロールID
                    ROLL_ID = (long)row.Cells["SinkPersonelRollIdColumn"].Value,
                    // ユーザーID
                    PERSONEL_ID = (string)row.Cells["SinkPersonelSyokubanColumn"].Value,
                    // ユーザーID(登録者)
                    INPUT_PERSONEL_ID = SessionDto.UserId,
                    // 種別(0:人, 1:部署・役職)
                    TYPE = 0
                });
            }

            return list;
        }
        /// <summary>
        /// 登録データ(部)の取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<RollAuthorityPostInModel> GetEntryDepartmentRollAuth()
        {
            var list = new List<RollAuthorityPostInModel>();

            foreach (var row in this.SinkDepartmentMultiRow.Rows)
            {
                list.Add(new RollAuthorityPostInModel()
                {
                    // ロールID
                    ROLL_ID = (long)row.Cells["SinkDepartmentRollIdColumn"].Value,
                    // 部ID
                    DEPARTMENT_ID = (string)row.Cells["SinkDepartmentBuIdColumn"].Value,
                    // 課ID
                    SECTION_ID = (string)row.Cells["SinkDepartmentKaIdColumn"].Value,
                    // 担当ID
                    SECTION_GROUP_ID = (string)row.Cells["SinkDepartmentTantoIdColumn"].Value,
                    // 役職
                    OFFICIAL_POSITION = (string)row.Cells["SinkDepartmentYakusyokuColumn"].Value,
                    // ユーザーID(登録者)
                    INPUT_PERSONEL_ID = SessionDto.UserId,
                    // 種別(0:人, 1:部署・役職)
                    TYPE = 1
                });
            }

            return list;
        }
        #endregion

        #region ロール権限の登録
        /// <summary>
        /// ロール権限(人)の登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryPersonelRollAuth()
        {
            // 入力チェック
            if (this.IsEntryPersonelRollAuth() == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryPersonelRollAuth();

            // 登録対象がない場合
            if (list == null || list.Count <= 0)
            {
                // 削除
                return this.DeletePersonelRollAuth();
            }

            // Post実行
            var res = HttpUtil.PostResponse(ControllerType.RollAuthority, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 編集フラグ初期化
            this.IsEditPersonel = false;

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        /// <summary>
        /// ロール権限(部)の登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryDepartmentRollAuth()
        {
            // 入力チェック
            if (this.IsEntryDepartmentRollAuth() == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryDepartmentRollAuth();

            // 登録対象がない場合
            if (list == null || list.Count <= 0)
            {
                // 削除
                return this.DeleteDepartmentRollAuth();
            }

            // Post実行
            var res = HttpUtil.PostResponse(ControllerType.RollAuthority, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 編集フラグ初期化
            this.IsEditDepartment = false;

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region ロール権限の削除
        /// <summary>
        /// ロール権限(人)の削除
        /// </summary>
        /// <returns>実行結果</returns>
        private bool DeletePersonelRollAuth()
        {
            // 条件設定
            var cond = new RollAuthorityDeleteInModel()
            {
                // ロールID
                ROLL_ID = this.ListSearchPersonelCond.ROLL_ID,
                // 種別(0:人, 1:部署・役職)
                TYPE = 0
            };

            // Delete実行
            var res = HttpUtil.DeleteResponse<RollAuthorityDeleteInModel>(ControllerType.RollAuthority, cond);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 編集フラグ初期化
            this.IsEditPersonel = false;

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        /// <summary>
        /// ロール権限(部)の削除
        /// </summary>
        /// <returns>実行結果</returns>
        private bool DeleteDepartmentRollAuth()
        {
            // 条件設定
            var cond = new RollAuthorityDeleteInModel()
            {
                // ロールID
                ROLL_ID = this.ListSearchDepartmentCond.ROLL_ID,
                // 種別(0:人, 1:部署・役職)
                TYPE = 1
            };

            // Delete実行
            var res = HttpUtil.DeleteResponse<RollAuthorityDeleteInModel>(ControllerType.RollAuthority, cond);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 編集フラグ初期化
            this.IsEditDepartment = false;

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region ロール権限の登録（変更フラグチェック）
        /// <summary>
        /// ロール権限(人)の登録（変更フラグチェック）
        /// </summary>
        /// <returns></returns>
        private bool IsEditPersonelRollAuthEntry()
        {
            // 画面を変更していないか登録するかどうか
            if (this.IsEditPersonel == false || (this.IsEditPersonel == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;
            }

            // 更新
            return this.EntryPersonelRollAuth();
        }
        /// <summary>
        /// ロール権限(部)の登録（変更フラグチェック）
        /// </summary>
        /// <returns></returns>
        private bool IsEditDepartmentRollAuthEntry()
        {
            // 画面を変更していないか登録するかどうか
            if (this.IsEditDepartment == false || (this.IsEditDepartment == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;
            }

            // 更新
            return this.EntryDepartmentRollAuth();
        }
        #endregion

        #endregion

        #region データの取得

        #region ロール名の取得
        /// <summary>
        /// ロール名の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollModel> GetRollNameList(RollGetInModel cond = null)
        {
            return HttpUtil.GetResponse<RollGetInModel, RollModel>(ControllerType.RollName, cond)?.Results?.ToList();
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
            if (!this.IsFirstPersonel)
            {
                return this.PersonelDataList;
            }

            // 条件設定(全検索)
            var cond = new UserSearchInModel();

            //APIで取得
            var res = HttpUtil.GetResponse<UserSearchInModel, UserSearchOutModel>(ControllerType.User, cond);

            return res.Results?.ToList();
        }
        #endregion

        #region 部署リストの取得
        /// <summary>
        /// 部署リストの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<SectionGroupModel> GetDepartmentList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstDepartment)
            {
                return this.DepartmentDataList;
            }

            // 条件設定(全検索)
            var cond = new SectionGroupSearchModel();

            //APIで取得
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, cond);

            return res.Results?.ToList();
        }
        #endregion

        #region 役職リストの取得
        /// <summary>
        /// 役職リストの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<OfficialPositionModel> GetOfficialPositionList()
        {
            // 初回アクセス以外は再検索しない
            if (!this.IsFirstDepartment)
            {
                return this.OfficialPositionDataList;
            }

            //APIで取得
            var res = HttpUtil.GetResponse<OfficialPositionGetInModel, OfficialPositionModel>(ControllerType.OfficialPosition, null);

            return res.Results?.ToList();
        }
        #endregion

        #region ロール権限(人)の取得
        /// <summary>
        /// ロール権限(人)の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollAuthorityGetOutModel> GetFunctionAuthorityRollPersonelList()
        {
            // 条件設定
            var cond = new RollAuthorityGetInModel()
            {
                ROLL_ID = this.PersonelRollComboBox.SelectedValue == null ? 0 : (long)this.PersonelRollComboBox.SelectedValue,
                TYPE = 0    // 人
            };

            //APIで取得
            var res = HttpUtil.GetResponse<RollAuthorityGetInModel, RollAuthorityGetOutModel>(ControllerType.RollAuthority, cond);

            return res.Results?.ToList();
        }
        #endregion

        #region ロール権限(部)の取得
        /// <summary>
        /// ロール権限(部)の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollAuthorityGetOutModel> GetFunctionAuthorityRollDepartmentList()
        {
            // 条件設定
            var cond = new RollAuthorityGetInModel()
            {
                ROLL_ID = this.DepartmentRollComboBox.SelectedValue == null ? 0 : (long)this.DepartmentRollComboBox.SelectedValue,
                TYPE = 1    // 部署・役職
            };

            //APIで取得
            var res = HttpUtil.GetResponse<RollAuthorityGetInModel, RollAuthorityGetOutModel>(ControllerType.RollAuthority, cond);

            return res.Results?.ToList();
        }
        #endregion

        #endregion

        #region イベント

        #region タブの選択
        /// <summary>
        /// タブの選択
        /// </summary>
        private void FunctionAuthorityRollTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            var tabIndex = FunctionAuthorityRollTabControl.SelectedIndex;

            // 人タブ
            if (tabIndex == 0)
            {
                // 部署・役職タブのクローズ処理
                FormControlUtil.FormWait(this, () =>
                {
                    // グリッドの編集は終了
                    this.SinkDepartmentMultiRow.EndEdit();

                    this.SourceDepartmentMultiRow.EndEdit();
                    this.SourceOfficialPositionMultiRow.EndEdit();

                    // 画面を変更していて登録するかどうか
                    if (!this.IsEditDepartmentRollAuthEntry())
                    {
                        // 登録に失敗した場合は閉じさせない
                        e.Cancel = true;
                    }
                });
            }
            // 部署・役職タブ
            else if (tabIndex == 1)
            {
                // 人タブのクローズ処理
                FormControlUtil.FormWait(this, () =>
                {
                    // グリッドの編集は終了
                    this.SinkPersonelMultiRow.EndEdit();
                    this.SourcePersonelMultiRow.EndEdit();

                    // 画面を変更していて登録するかどうか
                    if (!this.IsEditPersonelRollAuthEntry())
                    {
                        // 登録に失敗した場合は閉じさせない
                        e.Cancel = true;
                    }
                });
            }
        }
        #endregion

        #region タブの切り替え
        /// <summary>
        /// タブの切り替え
        /// </summary>
        private void FunctionAuthorityRollTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabIndex = FunctionAuthorityRollTabControl.SelectedIndex;

            if (tabIndex == 1 && this.IsFirstDepartment)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    //初期化:部
                    this.InitDepartmentProcess();

                    //初期アクセス:部
                    this.IsFirstDepartment = false;
                });
            }
        }
        #endregion

        #region 画面クローズ
        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionAuthorityRollForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var tabIndex = FunctionAuthorityRollTabControl.SelectedIndex;

            // 人タブ
            if (tabIndex == 0)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // グリッドの編集は終了
                    this.SinkPersonelMultiRow.EndEdit();
                    this.SourcePersonelMultiRow.EndEdit();

                    // 画面を変更していて登録するかどうか
                    if (!this.IsEditPersonelRollAuthEntry())
                    {
                        // 登録に失敗した場合は閉じさせない
                        e.Cancel = true;
                    }
                });
            }
            // 部署・役職タブ
            else if (tabIndex == 1)
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // グリッドの編集は終了
                    this.SinkDepartmentMultiRow.EndEdit();

                    this.SourceDepartmentMultiRow.EndEdit();
                    this.SourceOfficialPositionMultiRow.EndEdit();

                    // 画面を変更していて登録するかどうか
                    if (!this.IsEditDepartmentRollAuthEntry())
                    {
                        // 登録に失敗した場合は閉じさせない
                        e.Cancel = true;
                    }
                });
            }
        }
        #endregion

        #region ロールコンボボックス選択変更
        /// <summary>
        /// ロールコンボボックス(人)選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonelRollComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 変更反映確認
                if (this.IsEditPersonelRollAuthEntry())
                {
                    // 検索条件設定
                    this.SetSearchPersonelCond();

                    // 一覧設定
                    this.SearchPersonel();
                }
            });
        }
        /// <summary>
        /// ロールコンボボックス(部)選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentRollComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 変更反映確認
                if (this.IsEditDepartmentRollAuthEntry())
                {
                    // 検索条件設定
                    this.SetSearchDepartmentCond();

                    // 一覧設定
                    this.SearchDepartment();
                }
            });
        }
        #endregion

        #region 余白のクリック
        /// <summary>
        /// 余白(人)のクリック
        /// </summary>
        private void PersonelPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.ClearPersonelSelectedRows();
        }
        /// <summary>
        /// 余白(部)のクリック
        /// </summary>
        private void DepartmentPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.ClearDepartmentSelectedRows();
        }
        #endregion

        #region ユーザー一覧のセルダブルクリック
        /// <summary>
        /// ユーザー一覧のセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourcePersonelMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.SourcePersonelMultiRow.Rows[e.RowIndex];

            // 機能権限詳細(人)の起動
            using (var form = new FunctionAuthorityRollPersonelForm() { UserAuthority = this.UserAuthority })
            {
                var val = this.PersonelDataList.FirstOrDefault(x => x.PERSONEL_ID == row.Cells["SourcePersonelSyokubanColumn"]?.Value?.ToString());

                // 検索条件
                form.ListSearchCond = new RollAuthorityModel()
                {
                    DEPARTMENT_ID = val.DEPARTMENT_ID,
                    SECTION_ID = val.SECTION_ID,
                    SECTION_GROUP_ID = val.SECTION_GROUP_ID,
                    OFFICIAL_POSITION = val.OFFICIAL_POSITION,
                    PERSONEL_ID = val.PERSONEL_ID,
                    NAME = val.NAME
                };

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, this.SearchPersonel);
                }
            }
        }

        #endregion

        #region 部署一覧のセルダブルクリック
        /// <summary>
        /// 部署一覧のセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDepartmentMultiRow_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var col = this.SourceDepartmentMultiRow.Columns[e.CellIndex];
            var name = col.Name;

            var row = this.SourceDepartmentMultiRow.Rows[e.RowIndex];

            // 機能権限詳細(部)の起動
            using (var form = new FunctionAuthorityRollDepartmentForm() { UserAuthority = this.UserAuthority })
            {
                // 検索条件
                form.ListSearchCond = new RollAuthorityModel()
                {
                    DEPARTMENT_ID = row.Cells["SourceDepartmentBuIdColumn"]?.Value?.ToString(),
                    DEPARTMENT_CODE = row.Cells["SourceDepartmentBuColumn"]?.Value?.ToString()
                };

                // 課・担当の場合は課情報を追加
                if (name == "SourceDepartmentKaColumn" || name == "SourceDepartmentTantoColumn")
                {
                    form.ListSearchCond.SECTION_ID = row.Cells["SourceDepartmentKaIdColumn"]?.Value?.ToString();
                    form.ListSearchCond.SECTION_CODE = row.Cells["SourceDepartmentKaColumn"]?.Value?.ToString();
                }

                // 担当の場合は担当情報を追加
                if (name == "SourceDepartmentTantoColumn")
                {
                    form.ListSearchCond.SECTION_GROUP_ID = row.Cells["SourceDepartmentTantoIdColumn"]?.Value?.ToString();
                    form.ListSearchCond.SECTION_GROUP_CODE = row.Cells["SourceDepartmentTantoColumn"]?.Value?.ToString();
                }

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, this.SearchDepartment);
                }
            }
        }
        #endregion

        #region 役職一覧のセルダブルクリック
        /// <summary>
        /// 役職一覧のセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceOfficialPositionMultiRow_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.SourceOfficialPositionMultiRow.Rows[e.RowIndex];

            // 機能権限詳細(部)の起動
            using (var form = new FunctionAuthorityRollDepartmentForm() { UserAuthority = this.UserAuthority })
            {
                // 検索条件
                form.ListSearchCond = new RollAuthorityModel()
                {
                    OFFICIAL_POSITION = row.Cells["SourceOfficialPositionYakusyokuColumn"]?.Value?.ToString()
                };

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, this.SearchDepartment);
                }
            }
        }
        #endregion

        #region ロール権限(人)一覧のセルダブルクリック
        /// <summary>
        /// ロール権限(人)一覧のセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinkPersonelMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.SinkPersonelMultiRow.Rows[e.RowIndex];

            // 機能権限詳細(人)の起動
            using (var form = new FunctionAuthorityRollPersonelForm() { UserAuthority = this.UserAuthority })
            {
                var val = this.PersonelDataList.FirstOrDefault(x => x.PERSONEL_ID == row.Cells["SinkPersonelSyokubanColumn"]?.Value?.ToString());

                // 検索条件
                form.ListSearchCond = new RollAuthorityModel()
                {
                    DEPARTMENT_ID = val.DEPARTMENT_ID,
                    SECTION_ID = val.SECTION_ID,
                    SECTION_GROUP_ID = val.SECTION_GROUP_ID,
                    OFFICIAL_POSITION = val.OFFICIAL_POSITION,
                    PERSONEL_ID = val.PERSONEL_ID,
                    NAME = val.NAME
                };

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, this.SearchPersonel);
                }
            }
        }
        #endregion

        #region ロール権限(部)一覧のセルダブルクリック
        /// <summary>
        /// ロール権限(部)一覧のセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinkDepartmentMultiRow_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.SinkDepartmentMultiRow.Rows[e.RowIndex];

            // 機能権限詳細(部)の起動
            using (var form = new FunctionAuthorityRollDepartmentForm() { UserAuthority = this.UserAuthority })
            {
                // 検索条件
                form.ListSearchCond = new RollAuthorityModel()
                {
                    DEPARTMENT_ID = row.Cells["SinkDepartmentBuIdColumn"]?.Value?.ToString(),
                    DEPARTMENT_CODE = row.Cells["SinkDepartmentBuColumn"]?.Value?.ToString(),
                    SECTION_ID = row.Cells["SinkDepartmentKaIdColumn"]?.Value?.ToString(),
                    SECTION_CODE = row.Cells["SinkDepartmentKaColumn"]?.Value?.ToString(),
                    SECTION_GROUP_ID = row.Cells["SinkDepartmentTantoIdColumn"]?.Value?.ToString(),
                    SECTION_GROUP_CODE = row.Cells["SinkDepartmentTantoColumn"]?.Value?.ToString(),
                    OFFICIAL_POSITION = row.Cells["SinkDepartmentYakusyokuColumn"]?.Value?.ToString()
                };

                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, this.SearchDepartment);
                }
            }
        }
        #endregion

        #region グリッドのキーダウン
        /// <summary>
        /// グリッド(人：候補一覧)のキーダウン
        /// </summary>
        private void SourcePersonelMultiRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (var row in this.SourcePersonelMultiRow.Rows)
                {
                    row.Selected = row.Visible;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// グリッド(人：登録一覧)のキーダウン
        /// </summary>
        private void SinkPersonelMultiRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (var row in this.SinkPersonelMultiRow.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.Selected = cell.Visible;
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// グリッド(部署：候補一覧)のキーダウン
        /// </summary>
        private void SourceDepartmentMultiRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (var row in this.SourceDepartmentMultiRow.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.Selected = cell.Visible;
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// グリッド(役職：候補一覧)のキーダウン
        /// </summary>
        private void SourceOfficialPositionMultiRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (var row in this.SourceOfficialPositionMultiRow.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.Selected = cell.Visible;
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// グリッド(部：候補一覧)のキーダウン
        /// </summary>
        private void SinkDepartmentMultiRow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (var row in this.SinkDepartmentMultiRow.Rows)
                {
                    foreach (var cell in row.Cells)
                    {
                        cell.Selected = cell.Visible;
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region 人タブ追加(＞＞)ボタンクリック
        /// <summary>
        /// 人タブ追加(＞＞)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonelAddButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                this.AddPersonelSelectedRows();

                this.ClearPersonelSelectedRows();
            });
        }
        #endregion

        #region 人タブ削除(＜＜)ボタンクリック
        /// <summary>
        /// 人タブ削除(＜＜)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonelDeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                this.DeletePersonelSelectedRows();

                this.ClearPersonelSelectedRows();
            });
        }
        #endregion

        #region 部タブ追加(＞＞)ボタンクリック
        /// <summary>
        /// 部タブ追加(＞＞)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentAddButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                this.AddDepartmentSelectedRows();

                this.ClearDepartmentSelectedRows();
            });
        }
        #endregion

        #region 部タブ削除(＜＜)ボタンクリック
        /// <summary>
        /// 部タブ削除(＜＜)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentDeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                this.DeleteDepartmentSelectedRows();

                this.ClearDepartmentSelectedRows();
            });
        }
        #endregion

        #region 選択解除ボタンのクリック
        /// <summary>
        /// 選択解除ボタン(人)のクリック
        /// </summary>
        private void PersonelClearButton_Click(object sender, EventArgs e)
        {
            this.ClearPersonelSelectedRows();
        }
        /// <summary>
        /// 選択解除ボタン(部)のクリック
        /// </summary>
        private void DepartmentClearButton_Click(object sender, EventArgs e)
        {
            this.ClearDepartmentSelectedRows();
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
            var tabIndex = FunctionAuthorityRollTabControl.SelectedIndex;

            // 人タブ
            if (tabIndex == 0)
            {
                // ロール権限(人)の登録
                FormControlUtil.FormWait(this, () => this.EntryPersonelRollAuth());
            }
            // 部署・役職タブ
            else if (tabIndex == 1)
            {
                // ロール権限(部)の登録
                FormControlUtil.FormWait(this, () => this.EntryDepartmentRollAuth());
            }
        }
        #endregion

        #endregion

        #region グリッド操作

        #region 選択行(人)の追加
        /// <summary>
        /// 選択行(人)の追加
        /// </summary>
        private void AddPersonelSelectedRows()
        {
            var src = this.SourcePersonelMultiRow;
            var sink = this.SinkPersonelMultiRow;

            var src_ds = this.SourcePersonelDataSource;
            var sink_ds = this.SinkPersonelDataSource;

            var src_del = new List<object>();

            var idx = -1;

            // 現在のセル位置を退避
            var position = sink.FirstDisplayedCellPosition;

            foreach (var cell in src.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
            {
                // フィルタリング関連セルの場合
                if (cell is FilteringTextBoxCell) continue;

                // 同一行の場合
                if (cell.RowIndex == idx) continue;

                // 行番号の退避
                idx = cell.RowIndex;

                // 行取得
                var row = src.Rows[idx];

                // 行追加
                sink_ds.Add(new RollAuthorityGetOutModel()
                {
                    ROLL_ID = (long)this.ListSearchPersonelCond.ROLL_ID,
                    PERSONEL_ID = (string)row.Cells["SourcePersonelSyokubanColumn"].Value,
                    NAME = (string)row.Cells["SourcePersonelShimeiColumn"].Value
                });

                // 削除行の退避
                src_del.Add(row.DataBoundItem);

                // 編集フラグON
                this.IsEditPersonel = true;
            }

            // 行削除
            foreach (var del in src_del) src_ds.Remove(del);

            if (position.RowIndex > 0)
            {
                // MultiRowバグ対策
                var maxCellIndex = sink.Template.Row.Cells["SinkPersonelShimeiColumn"].CellIndex;

                // 退避したセル位置に移動
                sink.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
            }

            // フィルタリング更新
            src.UpdateFiltering();
            sink.UpdateFiltering();

            // カウントラベル更新
            this.SourcePersonelTemplate.SetCountLabel();
            this.SinkPersonelTemplate.SetCountLabel();
        }
        #endregion

        #region 選択行(人)の削除
        /// <summary>
        /// 選択行(人)の削除
        /// </summary>
        private void DeletePersonelSelectedRows()
        {
            var src = this.SourcePersonelMultiRow;
            var sink = this.SinkPersonelMultiRow;

            var src_ds = this.SourcePersonelDataSource;
            var sink_ds = this.SinkPersonelDataSource;

            var sink_del = new List<object>();

            var idx = -1;

            // 現在のセル位置を退避
            var position = src.FirstDisplayedCellPosition;

            foreach (var cell in sink.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
            {
                // フィルタリング関連セルの場合
                if (cell is FilteringTextBoxCell) continue;

                // 同一行の場合
                if (cell.RowIndex == idx) continue;

                // 行番号の退避
                idx = cell.RowIndex;

                // 行取得
                var row = sink.Rows[idx];

                // 情報の取得
                var val = this.PersonelDataList?.FirstOrDefault(x => x.PERSONEL_ID == row.Cells["SinkPersonelSyokubanColumn"].Value.ToString());

                if (val != null)
                {
                    // 行追加
                    src_ds.Add(val);
                }

                // 削除行の退避
                sink_del.Add(row.DataBoundItem);

                // 編集フラグON
                this.IsEditPersonel = true;
            }

            // 行削除
            foreach (var del in sink_del) sink_ds.Remove(del);

            if (position.RowIndex > 0)
            {
                // MultiRowバグ対策
                var maxCellIndex = src.Template.Row.Cells["SourcePersonelYakusyokuColumn"].CellIndex;

                // 退避したセル位置に移動
                src.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
            }

            // フィルタリング更新
            src.UpdateFiltering();
            sink.UpdateFiltering();

            // カウントラベル更新
            this.SourcePersonelTemplate.SetCountLabel();
            this.SinkPersonelTemplate.SetCountLabel();
        }
        #endregion

        #region 選択行(部)の追加
        /// <summary>
        /// 選択行(部)の追加
        /// </summary>
        private void AddDepartmentSelectedRows()
        {
            var src1 = this.SourceDepartmentMultiRow;   // 部署
            var src2 = this.SourceOfficialPositionMultiRow; // 役職
            var sink = this.SinkDepartmentMultiRow;

            var src1_ds = this.SourceDepartmentDataSource;  // 部署
            var src2_ds = this.SourceOfficialPositionDataSource;    // 役職
            var sink_ds = this.SinkDepartmentDataSource;

            var src1_del = new List<object>();
            var src2_del = new List<object>();
            var sink_del = new List<object>();

            var src1_idx = -1;

            // 現在のセル位置を退避
            var position = sink.FirstDisplayedCellPosition;

            // 部署・役職
            if (src1.SelectedCells.Count > 0 && src2.SelectedCells.Count > 0)
            {
                // 部署
                foreach (var src1selectedcell in src1.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
                {
                    // フィルタリング関連セルの場合
                    if (src1selectedcell is FilteringTextBoxCell) continue;

                    // 同一行の場合
                    if (src1selectedcell.RowIndex == src1_idx) continue;

                    // 行番号の退避
                    src1_idx = src1selectedcell.RowIndex;

                    // 行取得
                    var src1selectedrow = src1.Rows[src1_idx];

                    // 選択カラム名
                    var colname = src1.Columns[src1selectedcell.CellIndex].Name;

                    var src2_idx = -1;

                    // 役職
                    foreach (var src2selectedcell in src2.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
                    {
                        // フィルタリング関連セルの場合
                        if (src2selectedcell is FilteringTextBoxCell) continue;

                        // 同一行の場合
                        if (src2selectedcell.RowIndex == src2_idx) continue;

                        // 行番号の退避
                        src2_idx = src2selectedcell.RowIndex;

                        // 行取得
                        var src2selectedrow = src2.Rows[src2_idx];

                        // 追加フラグ
                        var isAdd = true;

                        // 追加行の作成
                        var val = new RollAuthorityGetOutModel()
                        {
                            ROLL_ID = (long)this.ListSearchDepartmentCond.ROLL_ID,
                            DEPARTMENT_CODE = (string)src1selectedrow.Cells["SourceDepartmentBuColumn"].Value,
                            DEPARTMENT_ID = (string)src1selectedrow.Cells["SourceDepartmentBuIdColumn"].Value
                        };

                        // 課・担当選択の場合は課追加
                        if (colname == "SourceDepartmentKaColumn" || colname == "SourceDepartmentTantoColumn")
                        {
                            val.SECTION_CODE = (string)src1selectedrow.Cells["SourceDepartmentKaColumn"].Value;
                            val.SECTION_ID = (string)src1selectedrow.Cells["SourceDepartmentKaIdColumn"].Value;
                        }

                        // 担当選択の場合は担当追加
                        if (colname == "SourceDepartmentTantoColumn")
                        {
                            val.SECTION_GROUP_CODE = (string)src1selectedrow.Cells["SourceDepartmentTantoColumn"].Value;
                            val.SECTION_GROUP_ID = (string)src1selectedrow.Cells["SourceDepartmentTantoIdColumn"].Value;
                        }

                        val.OFFICIAL_POSITION = (string)src2selectedrow.Cells["SourceOfficialPositionYakusyokuColumn"].Value;

                        // 部署(部・課・担当)IDの退避
                        var buid = (string)src1selectedrow.Cells["SourceDepartmentBuIdColumn"].Value;
                        var kaid = (string)src1selectedrow.Cells["SourceDepartmentKaIdColumn"].Value;
                        var tantoid = (string)src1selectedrow.Cells["SourceDepartmentTantoIdColumn"].Value;
                        var yakusyoku = (string)src2selectedrow.Cells["SourceOfficialPositionYakusyokuColumn"].Value;

                        // 部選択の場合
                        if (colname == "SourceDepartmentBuColumn")
                        {
                            // 移動先
                            foreach (var sinkrow in sink.Rows)
                            {
                                // 同一部かつ、課がある行は、同一役職の場合は削除
                                if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value != null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    // 削除行の退避
                                    sink_del.Add(sinkrow.DataBoundItem);
                                }
                                // 同一部かつ、課と役職がない行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                                {
                                    isAdd = false;
                                }
                                // 同一行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                            }
                        }
                        // 課選択の場合
                        else if (colname == "SourceDepartmentKaColumn")
                        {
                            // 移動先
                            foreach (var sinkrow in sink.Rows)
                            {
                                // 同一課かつ、担当がある行は、同一役職の場合は削除
                                if ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value != null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    // 削除行の退避
                                    sink_del.Add(sinkrow.DataBoundItem);
                                }
                                // 同一部かつ、課がない行が、同一役職ですでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                                // 同一部かつ、課と役職がない行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                                {
                                    isAdd = false;
                                }
                                // 同一行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                            }
                        }
                        // 担当選択の場合
                        else
                        {
                            // 移動先
                            foreach (var sinkrow in sink.Rows)
                            {
                                // 同一部かつ、課がない行が、同一役職ですでにある場合は追加しない
                                if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                                // 同一部かつ、課と役職がない行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                                {
                                    isAdd = false;
                                }
                                // 同一課かつ、担当がない行が、同一役職ですでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                                // 同一課かつ、担当と役職がない行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                                {
                                    isAdd = false;
                                }
                                // 同一行がすでにある場合は追加しない
                                else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                    (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                    (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == tantoid &&
                                    (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku)
                                {
                                    isAdd = false;
                                }
                            }
                        }

                        // 行追加
                        if (isAdd) sink_ds.Add(val);

                        // 編集フラグON
                        this.IsEditDepartment = true;
                    }
                }

                // 行削除
                foreach (var del in sink_del) sink_ds.Remove(del);
            }
            // 部署
            else if (src1.SelectedCells.Count > 0)
            {
                foreach (var src1selectedcell in src1.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
                {
                    // フィルタリング関連セルの場合
                    if (src1selectedcell is FilteringTextBoxCell) continue;

                    // 同一行の場合
                    if (src1selectedcell.RowIndex == src1_idx) continue;

                    // 行番号の退避
                    src1_idx = src1selectedcell.RowIndex;

                    // 行取得
                    var src1selectedrow = src1.Rows[src1_idx];

                    // 選択カラム名
                    var colname = src1.Columns[src1selectedcell.CellIndex].Name;

                    // 追加フラグ
                    var isAdd = true;

                    // 追加行の作成
                    var val = new RollAuthorityGetOutModel()
                    {
                        ROLL_ID = (long)this.ListSearchDepartmentCond.ROLL_ID,
                        DEPARTMENT_CODE = (string)src1selectedrow.Cells["SourceDepartmentBuColumn"].Value,
                        DEPARTMENT_ID = (string)src1selectedrow.Cells["SourceDepartmentBuIdColumn"].Value
                    };

                    // 課・担当選択の場合は課追加
                    if (colname == "SourceDepartmentKaColumn" || colname == "SourceDepartmentTantoColumn")
                    {
                        val.SECTION_CODE = (string)src1selectedrow.Cells["SourceDepartmentKaColumn"].Value;
                        val.SECTION_ID = (string)src1selectedrow.Cells["SourceDepartmentKaIdColumn"].Value;
                    }

                    // 担当選択の場合は担当追加
                    if (colname == "SourceDepartmentTantoColumn")
                    {
                        val.SECTION_GROUP_CODE = (string)src1selectedrow.Cells["SourceDepartmentTantoColumn"].Value;
                        val.SECTION_GROUP_ID = (string)src1selectedrow.Cells["SourceDepartmentTantoIdColumn"].Value;
                    }

                    // 部署(部・課・担当)IDの退避
                    var buid = (string)src1selectedrow.Cells["SourceDepartmentBuIdColumn"].Value;
                    var kaid = (string)src1selectedrow.Cells["SourceDepartmentKaIdColumn"].Value;
                    var tantoid = (string)src1selectedrow.Cells["SourceDepartmentTantoIdColumn"].Value;

                    // 部選択の場合
                    if (colname == "SourceDepartmentBuColumn")
                    {
                        // 移動元
                        foreach (var src1row in src1.Rows)
                        {
                            // 同一部の行は削除
                            if ((string)src1row.Cells["SourceDepartmentBuIdColumn"].Value == buid)
                            {
                                // 削除行の退避
                                src1_del.Add(src1row.DataBoundItem);
                            }
                        }

                        // 移動先
                        foreach (var sinkrow in sink.Rows)
                        {
                            // 同一部かつ、課または役職がある行は削除
                            if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                               ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value != null ||
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value != null))
                            {
                                // 削除行の退避
                                sink_del.Add(sinkrow.DataBoundItem);
                            }
                            // 同一行がすでにある場合は追加しない
                            else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                        }
                    }
                    // 課選択の場合
                    else if (colname == "SourceDepartmentKaColumn")
                    {
                        // 移動元
                        foreach (var src1row in src1.Rows)
                        {
                            // 同一課の行は削除
                            if ((string)src1row.Cells["SourceDepartmentKaIdColumn"].Value == kaid)
                            {
                                // 削除行の退避
                                src1_del.Add(src1row.DataBoundItem);
                            }
                        }

                        // 移動先
                        foreach (var sinkrow in sink.Rows)
                        {
                            // 同一課かつ、担当または役職がある行は削除
                            if ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                               ((string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value != null ||
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value != null))
                            {
                                // 削除行の退避
                                sink_del.Add(sinkrow.DataBoundItem);
                            }
                            // 同一部かつ、課かつ役職がない行がすでにある場合は追加しない
                            else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                            // 同一行がすでにある場合は追加しない
                            else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                        }
                    }
                    // 担当選択の場合
                    else
                    {
                        // 行削除
                        src1_del.Add(src1selectedrow.DataBoundItem);

                        // 移動先
                        foreach (var sinkrow in sink.Rows)
                        {
                            // 同一担当かつ、役職がある行は削除
                            if ((string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == tantoid &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value != null)
                            {
                                // 削除行の退避
                                sink_del.Add(sinkrow.DataBoundItem);
                            }
                            // 同一部かつ、課かつ役職がない行がすでにある場合は追加行しない
                            else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                            // 同一課かつ、担当かつ役職がない行がすでにある場合は追加しない
                            else if ((string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                            // 同一行がすでにある場合は追加しない
                            else if ((string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == buid &&
                                (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == kaid &&
                                (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == tantoid &&
                                (string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == null)
                            {
                                isAdd = false;
                            }
                        }
                    }

                    // 行追加
                    if (isAdd) sink_ds.Add(val);

                    // 編集フラグON
                    this.IsEditDepartment = true;
                }

                // 行削除
                foreach (var del in src1_del) src1_ds.Remove(del);
                foreach (var del in sink_del) sink_ds.Remove(del);
            }
            // 役職
            else if (src2.SelectedCells.Count > 0)
            {
                foreach (var src2selectedcell in src2.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
                {
                    // フィルタリング関連セルの場合
                    if (src2selectedcell is FilteringTextBoxCell) continue;

                    // 同一行の場合
                    if (src2selectedcell.RowIndex == src1_idx) continue;

                    // 行番号の退避
                    src1_idx = src2selectedcell.RowIndex;

                    // 行取得
                    var src2selectedrow = src2.Rows[src1_idx];

                    // 追加フラグ
                    var isAdd = true;

                    // 追加行の作成
                    var val = new RollAuthorityGetOutModel()
                    {
                        ROLL_ID = (long)this.ListSearchDepartmentCond.ROLL_ID,
                        OFFICIAL_POSITION = (string)src2selectedrow.Cells["SourceOfficialPositionYakusyokuColumn"].Value
                    };

                    // 役職の退避
                    var yakusyoku = (string)src2selectedrow.Cells["SourceOfficialPositionYakusyokuColumn"].Value;

                    // 削除行の退避
                    src2_del.Add(src2selectedrow.DataBoundItem);

                    // 移動先
                    foreach (var sinkrow in sink.Rows)
                    {
                        // 同一役職かつ、部がある行は削除
                        if ((string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku &&
                            (string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value != null)
                        {
                            // 削除行の退避
                            sink_del.Add(sinkrow.DataBoundItem);
                        }
                        // 同一行がすでにある場合は追加しない
                        else if ((string)sinkrow.Cells["SinkDepartmentYakusyokuColumn"].Value == yakusyoku &&
                            (string)sinkrow.Cells["SinkDepartmentBuIdColumn"].Value == null &&
                            (string)sinkrow.Cells["SinkDepartmentKaIdColumn"].Value == null &&
                            (string)sinkrow.Cells["SinkDepartmentTantoIdColumn"].Value == null)
                        {
                            isAdd = false;
                        }
                    }

                    // 行追加
                    if (isAdd) sink_ds.Add(val);

                    // 編集フラグON
                    this.IsEditDepartment = true;
                }

                // 行削除
                foreach (var del in src2_del) src2_ds.Remove(del);
                foreach (var del in sink_del) sink_ds.Remove(del);
            }

            if (position.RowIndex > 0)
            {
                // MultiRowバグ対策
                var maxCellIndex = sink.Template.Row.Cells["SinkDepartmentYakusyokuColumn"].CellIndex;

                // 退避したセル位置に移動
                sink.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
            }

            // フィルタリング更新
            src1.UpdateFiltering();
            src2.UpdateFiltering();
            sink.UpdateFiltering();

            // カウントラベル更新
            this.SourceDepartmentTemplate.SetCountLabel();
            this.SourceOfficialPositionTemplate.SetCountLabel();
            this.SinkDepartmentTemplate.SetCountLabel();
        }
        #endregion

        #region 選択行(部)の削除
        /// <summary>
        /// 選択行(部)の削除
        /// </summary>
        private void DeleteDepartmentSelectedRows()
        {
            var src1 = this.SourceDepartmentMultiRow;   // 部署
            var src2 = this.SourceOfficialPositionMultiRow; // 役職
            var sink = this.SinkDepartmentMultiRow;

            var src1_ds = this.SourceDepartmentDataSource;   // 部署
            var src2_ds = this.SourceOfficialPositionDataSource; // 役職
            var sink_ds = this.SinkDepartmentDataSource;

            var sink_del = new List<object>();

            var idx = -1;

            // 現在のセル位置を退避
            var position1 = src1.FirstDisplayedCellPosition;
            var position2 = src2.FirstDisplayedCellPosition;

            foreach (var sinkselectedcell in sink.SelectedCells.OfType<Cell>().OrderBy(x => x.RowIndex).ThenBy(x => x.CellIndex))
            {
                // フィルタリング関連セルの場合
                if (sinkselectedcell is FilteringTextBoxCell) continue;

                // 同一行の場合
                if (sinkselectedcell.RowIndex == idx) continue;

                // 行番号の退避
                idx = sinkselectedcell.RowIndex;

                // 行取得
                var sinkselectedrow = sink.Rows[idx];

                // 退避
                var buid = sinkselectedrow.Cells["SinkDepartmentBuIdColumn"].Value;
                var kaid = sinkselectedrow.Cells["SinkDepartmentKaIdColumn"].Value;
                var tantoid = sinkselectedrow.Cells["SinkDepartmentTantoIdColumn"].Value;
                var op = sinkselectedrow.Cells["SinkDepartmentYakusyokuColumn"].Value;

                // 部署・役職
                if ((buid != null || kaid != null || tantoid != null) && op != null)
                {
                    // 未処理
                }
                // 部署
                else if (buid != null || kaid != null || tantoid != null)
                {
                    // 情報の取得
                    var list = new List<SectionGroupModel>(this.DepartmentDataList).Where(x => x.DEPARTMENT_ID == buid.ToString());

                    if (kaid != null) list = list.Where(x => x.SECTION_ID == kaid.ToString());
                    if (tantoid != null) list = list.Where(x => x.SECTION_GROUP_ID == tantoid.ToString());

                    foreach (var val in list)
                    {
                        // 行追加
                        src1_ds.Add(val);
                    }
                }
                // 役職
                else if (op != null)
                {
                    // 行追加
                    src2_ds.Add(new OfficialPositionModel() { OFFICIAL_POSITION = (string)op });
                }

                // 削除行の退避
                sink_del.Add(sinkselectedrow.DataBoundItem);

                // 編集フラグON
                this.IsEditDepartment = true;
            }

            // 行削除
            foreach (var del in sink_del) sink_ds.Remove(del);

            if (position1.RowIndex > 0)
            {
                // MultiRowバグ対策
                var maxCellIndex1 = src1.Template.Row.Cells["SourceDepartmentTantoColumn"].CellIndex;

                // 退避したセル位置に移動
                src1.FirstDisplayedCellPosition = new CellPosition(position1.RowIndex, maxCellIndex1);
            }

            if (position2.RowIndex > 0)
            {
                // MultiRowバグ対策
                var maxCellIndex2 = src2.Template.Row.Cells["SourceOfficialPositionYakusyokuColumn"].CellIndex;

                // 退避したセル位置に移動
                src2.FirstDisplayedCellPosition = new CellPosition(position2.RowIndex, maxCellIndex2);
            }

            // フィルタリング更新
            src1.UpdateFiltering();
            src2.UpdateFiltering();
            sink.UpdateFiltering();

            // カウントラベル更新
            this.SourceDepartmentTemplate.SetCountLabel();
            this.SourceOfficialPositionTemplate.SetCountLabel();
            this.SinkDepartmentTemplate.SetCountLabel();
        }
        #endregion

        #region 選択行(人)のクリア
        /// <summary>
        /// 選択行(人)のクリア
        /// </summary>
        private void ClearPersonelSelectedRows()
        {
            this.SourcePersonelMultiRow.ClearSelection();
            this.SinkPersonelMultiRow.ClearSelection();
        }
        #endregion

        #region 選択行(部)のクリア
        /// <summary>
        /// 選択行(部)のクリア
        /// </summary>
        private void ClearDepartmentSelectedRows()
        {
            this.SourceDepartmentMultiRow.ClearSelection();
            this.SourceOfficialPositionMultiRow.ClearSelection();
            this.SinkDepartmentMultiRow.ClearSelection();
        }
        #endregion

        #endregion
    }
}