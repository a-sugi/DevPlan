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
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MonthlyPlan
{
    /// <summary>
    /// 月次計画表お気に入り登録
    /// </summary>
    public partial class MonthlyPlanFavoriteForm : BaseSubForm
    {
        #region メンバ変数
        private const string FavoriteClassData = Const.FavoriteMonthlyWork;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "お気に入り登録"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>スケジュール情報</summary>
        public MonthlyWorkFavoriteItemModel FAVORITE { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonthlyPlanFavoriteForm()
        {
            InitializeComponent();
        }
        #endregion

        #region 画面ロード
        /// <summary>
        /// MonthlyPlanDetailForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyPlanDetailForm_Load(object sender, EventArgs e)
        {
            // 初期化
            this.Init();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            if (this.FAVORITE == null) return;
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
            //スケジュール項目のチェック
            if (this.IsEntryFavorite())
            {
                var res = this.PostData();

                if (res.Status.Equals(Const.StatusSuccess))
                {
                    Messenger.Info(Resources.KKM00002); // 登録（更新）完了

                    base.FormOkClose();
                }
            }
        }
        #endregion

        #region お気に入りのチェック
        /// <summary>
        /// お気に入りのチェック
        /// </summary>
        /// <returns>チェック可否</returns>
        private bool IsEntryFavorite()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);

            if (msg != "")
            {
                Messenger.Warn(msg);

                return false;
            }

            //お気に入りが上限件数まで登録済かどうか
            if (this.GetFavoriteList()?.Count() >= Const.FavoriteEntryMax)
            {
                Messenger.Warn(Resources.KKM00016);

                return false;
            }

            return true;
        }
        #endregion

        #region データ登録処理
        /// <summary>
        /// データ登録処理
        /// </summary>
        private ResponseDto<MonthlyWorkFavoriteItemModel> PostData()
        {
            var data = new MonthlyWorkFavoriteItemModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,
                // タイトル
                TITLE = this.TitleTextBox.Text,
                // 開発符号
                GENERAL_CODE = this.FAVORITE.GENERAL_CODE,
                // 所属区分
                CLASS_KBN = this.FAVORITE.CLASS_KBN,
                // 所属ID
                CLASS_ID = this.FAVORITE.CLASS_ID,
                // ステータス_OPENフラグ
                STATUS_OPEN_FLG = this.FAVORITE.STATUS_OPEN_FLG,
                // ステータス_CLOSEフラグ
                STATUS_CLOSE_FLG = this.FAVORITE.STATUS_CLOSE_FLG,
                // 指定月
                TARGET_MONTH = this.FAVORITE.TARGET_MONTH,
                // 表示対象
                DISPLAY_KBN = this.FAVORITE.DISPLAY_KBN
            };

            return HttpUtil.PostResponse<MonthlyWorkFavoriteItemModel>(ControllerType.MonthlyWorkFavorite, data);
        }
        #endregion

        #region お気に入り検索
        /// <summary>
        /// お気に入り検索
        /// </summary>
        private List<FavoriteSearchOutModel> GetFavoriteList()
        {
            //パラメータ設定
            var itemCond = new FavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,
                // データ区分
                CLASS_DATA = FavoriteClassData
            };

            //Get実行
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, itemCond);

            return (res.Results).ToList();
        }
        #endregion
    }
}
