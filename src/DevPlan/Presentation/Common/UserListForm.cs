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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// ユーザー一覧検索
    /// </summary>
    public partial class UserListForm : BaseSubForm
    {
        #region インスタンス
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "ユーザー一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>部コード</summary>
        public string DepartmentCode
        {
            get { return this.DepartmentCodeTextBox.Text; }
            set { this.DepartmentCodeTextBox.Text = value; }

        }

        /// <summary>課コード</summary>
        public string SectionCode
        {
            get { return this.SectionCodeTextBox.Text; }
            set { this.SectionCodeTextBox.Text = value; }

        }

        /// <summary>担当コード</summary>
        public string SectionGroupCode
        {
            get { return this.SectionGroupCodeTextBox.Text; }
            set { this.SectionGroupCodeTextBox.Text = value; }

        }

        /// <summary>名前</summary>
        public string PersonelName
        {
            get { return this.PersonelNameTextBox.Text; }
            set { this.PersonelNameTextBox.Text = value; }


        }

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> UserList { get; set; }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>ユーザー</summary>
        public UserSearchOutModel User { get; set; }

        /// <summary>自部課制限フラグ</summary>
        public bool IsAffiliationLimit { get; set; } = false;
        
        /// <summary>ユーザーステータスコード</summary>
        public string StatusCode { get; set; }

        /// <summary>検索条件制限フラグ</summary>
        public bool IsSearchLimit { get; set; } = true;

        /// <summary>大小文字区別フラグ</summary>
        public bool IsDistinct { get; set; } = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserListForm()
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
        private void UserListForm_Load(object sender, EventArgs e)
        {
            //初期表示フォーカス
            this.ActiveControl = PersonelNameTextBox;

            // 画面の初期化
            this.InitForm();

            //初期表示時に、検索結果を一覧表示
            this.SetGridData();
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            this.CustomTemplate = new CustomTemplate() { MultiRow = this.UserListMultiRow, RowCountLabel = this.RowCountLabel };

            this.UserListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new UserListMultiRowTemplate());

            this.UserListMultiRow.CellClick += this.UserListMultiRow_CellClick;

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // 自部課制限有・管理権限なし・更新権限ありの場合は自部課のみ
            if (IsAffiliationLimit && !isManagement && isUpdate)
            {
                // 部
                this.DepartmentCodeTextBox.Text = SessionDto.DepartmentName;
                this.DepartmentCodeTextBox.Enabled = false;

                // 課
                this.SectionCodeTextBox.Text = SessionDto.SectionName;
                this.SectionCodeTextBox.Enabled = false;
            }
        }       
        #endregion

        #region 検索ボタンのクリック
        /// <summary>
        /// 検索ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListSearchButton_Click(object sender, EventArgs e)
        {
            //検索可能かどうか
            if (this.IsSearch() == true)
            {
                this.SetGridData();

            }

        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearch()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            if (IsSearchLimit)
            {
                map[this.PersonelNameTextBox] = (c, name) =>
                {
                    var errMsg = "";

                    var list = (new[] { this.DepartmentCodeTextBox, this.SectionCodeTextBox, this.SectionGroupCodeTextBox, this.PersonelNameTextBox }).ToList();

                    //すべて未入力ならエラー
                    if (list.All(x => string.IsNullOrEmpty(x.Text) == true) == true)
                    {
                        list.ForEach(x => x.BackColor = Const.ErrorBackColor);
                        errMsg = Resources.KKM00004;
                    }

                    return errMsg;
                };
            }

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
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            // グリッドのデータソースをAPIから受け取るデータに設定
            this.UserList = GetGridDataList();

            // データバインド
            this.CustomTemplate.SetDataSource(this.UserList);

            //一覧が取得できたかどうか
            this.ListDataLabel.Text = this.UserList.Any() == false ? Resources.KKM00005 : "";

            //一覧を未選択状態に設定
            this.UserListMultiRow.CurrentCell = null;

        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<UserSearchOutModel> GetGridDataList()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // パラメータ設定
            var itemCond = new UserSearchModel
            {
                // 部コード
                DEPARTMENT_NAME = this.DepartmentCodeTextBox.Text,

                // 部ID
                DEPARTMENT_ID = IsAffiliationLimit && !isManagement && isUpdate
                    ? SessionDto.DepartmentID : null,

                // 課コード
                SECTION_NAME = this.SectionCodeTextBox.Text,

                // 課ID
                SECTION_ID = IsAffiliationLimit && !isManagement && isUpdate
                    ? SessionDto.SectionID : null,

                // 担当コード
                SECTION_GROUP_NAME = this.SectionGroupCodeTextBox.Text,

                // 名前
                PERSONEL_NAME = this.PersonelNameTextBox.Text,

                // ユーザステータスコード
                STATUS_CODE = this.StatusCode,

                // 大小文字区別フラグ
                DISTINCT_FLG = this.IsDistinct

            };

            // Get実行
            var res = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>(ControllerType.User, itemCond);

            return res.Results.ToList();

        }
        #endregion

        #region グリッドのセルクリック
        /// <summary>
        /// グリッドのセルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = this.UserListMultiRow;

                this.User = this.UserList.First(x => x.PERSONEL_ID == (string)grid.CurrentRow.Cells["PersonelIDColumn"].Value);

                base.FormOkClose();

            }

        }
        #endregion

        /// <summary>
        /// 検索条件クリア処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.DepartmentCodeTextBox.Text = string.Empty;
            this.PersonelNameTextBox.Text = string.Empty;
            this.SectionCodeTextBox.Text = string.Empty;
            this.SectionGroupCodeTextBox.Text = string.Empty;

            this.ActiveControl = PersonelNameTextBox;
        }
    }
}
