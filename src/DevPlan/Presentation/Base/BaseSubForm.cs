using System;
using System.Drawing;
using System.Deployment.Application;
using System.Windows.Forms;

using log4net;

using DevPlan.Presentation.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Utils;
using System.Collections.Generic;
using DevPlan.UICommon.Enum;
using System.Linq;
using DevPlan.UICommon;

namespace DevPlan.Presentation.Base
{
    /// <summary>
    /// 基底クラス（サブウィンドウ）
    /// </summary>
    public partial class BaseSubForm : Form
    {
        #region プロパティ
        /// <summary>システム名</summary>
        public string SystemName { get { return Resources.SystemName; } }

        /// <summary>画面名</summary>
        public virtual string FormTitle { get { return "タイトルが設定されていません"; } }

        /// <summary>横幅</summary>
        public virtual int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public virtual int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public virtual bool IsFormSizeFixed { get { return false; } }

        /// <summary>横幅(最小)</summary>
        public virtual int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public virtual int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>横幅(最大)</summary>
        public virtual int FormMaxWidth { get { return this.MaximumSize.Width; } }

        /// <summary>縦幅(最大)</summary>
        public virtual int FormMaxHeight { get { return this.MaximumSize.Height; } }

        protected ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseSubForm()
        {
            InitializeComponent();

            //ダブルバァッファリング有効化
            this.DoubleBuffered = true;

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// BaseListForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseListForm_Load(object sender, EventArgs e)
        {
            this.Init();
            FormControlUtil.InitLabel(this);
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //画面名の初期化
            this.Text = this.FormTitle;
            this.ListFormTitleLabel.Text = this.FormTitle;

            //フォームのサイズの初期化
            base.Size = new Size(this.FormWidth, this.FormHeight);

            //フォームサイズ固定かどうか
            if (this.IsFormSizeFixed == true)
            {
                //フォームサイズ固定の場合はリサイズ不可
                base.MinimumSize = base.MaximumSize = base.Size;

            }
            else
            {
                //最小サイズ
                base.MinimumSize = new Size(this.FormMinWidth, this.FormMinHeight);

                //最大サイズの設定があれば設定
                if (this.FormMaxWidth > 0 || this.FormMaxHeight > 0)
                {
                    base.MaximumSize = new Size(this.FormMaxWidth, this.FormMaxHeight);

                }

            }

        }
        #endregion

        #region 閉じるボタン押下
        /// <summary>
        /// 閉じるボタン押下
        /// </summary>
        public virtual void CloseButton_Click(object sender, EventArgs e)
        {
            //フォームクローズ(Cancel)
            this.FormCancelClose();

        }
        #endregion

        #region フォームをダイアログ結果を設定してからクローズ
        /// <summary>
        /// フォームクローズ(OK)
        /// </summary>
        protected void FormOkClose()
        {
            this.FormClose(DialogResult.OK);

        }

        /// <summary>
        /// フォームクローズ(Cancel)
        /// </summary>
        protected void FormCancelClose()
        {
            this.FormClose(DialogResult.Cancel);

        }

        /// <summary>
        /// フォームクローズ
        /// </summary>
        /// <param name="result">ダイアログ結果</param>
        protected void FormClose(DialogResult result)
        {
            //ダイアログ結果
            this.DialogResult = result;

            this.Close();

        }
        #endregion

        #region API
        /// <summary>
        /// 機能権限一覧取得
        /// </summary>
        /// <param name="id">機能ID</param>
        /// <returns></returns>
        public List<UserAuthorityOutModel> GetFunctionList(FunctionID? id = null)
        {
            // ログインセッション情報がある場合
            if (SessionDto.UserAuthorityList != null)
            {
                return id == null
                    ? SessionDto.UserAuthorityList
                    : SessionDto.UserAuthorityList.Where(x => x.FUNCTION_ID == (int)id)?.ToList();
            }

            var list = new List<UserAuthorityOutModel>();

            var cond = new UserAuthorityInModel
            {
                //部ID
                DEPARTMENT_ID = SessionDto.DepartmentID,

                //課ID
                SECTION_ID = SessionDto.SectionID,

                //担当ID
                SECTION_GROUP_ID = SessionDto.SectionGroupID,

                //役職
                OFFICIAL_POSITION = SessionDto.OfficialPosition,

                //ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                //機能ID
                FUNCTION_ID = id == null ? null : (int?)id.Value

            };

            //APIで取得
            var res = HttpUtil.GetResponse<UserAuthorityInModel, UserAuthorityOutModel>(ControllerType.UserAuthority, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 権限の取得
        /// </summary>
        /// <param name="id">機能ID</param>
        /// <returns></returns>
        protected UserAuthorityOutModel GetFunction(FunctionID id)
        {
            return this.GetFunctionList(id).Select(x => { x.ROLL_ID_LIST = x.ROLL_IDS.Split(',').ToList(); return x; }).FirstOrDefault() ?? new UserAuthorityOutModel
            {
                //機能ID
                FUNCTION_ID = (int)id,

                //参照フラグ
                READ_FLG = '0',

                //更新フラグ
                UPDATE_FLG = '0',

                //出力フラグ
                EXPORT_FLG = '0',

                //管理フラグ
                MANAGEMENT_FLG = '0',

                //プリントスクリーンフラグ
                PRINTSCREEN_FLG = '0',

                //カーシェア事務所フラグ
                CARSHARE_OFFICE_FLG = '0',

                //全閲覧権限フラグ
                ALL_GENERAL_CODE_FLG = '0',

                //SKSフラグ
                SKS_FLG = '0',
                
                //自部署編集フラグ
                JIBU_UPDATE_FLG = '0',

                //自部署印刷フラグ
                JIBU_EXPORT_FLG = '0',

                //自部署管理フラグ
                JIBU_MANAGEMENT_FLG = '0',

                //ロールID(カンマ区切り)
                ROLL_IDS = string.Empty
            };

        }
        #endregion
    }
}
