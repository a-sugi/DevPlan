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

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// お気に入り登録
    /// </summary>
    public partial class FavoriteEntryForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "お気に入り登録"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>お気に入り</summary>
        public object Favorite { get; set; }

        /// <summary>データ区分</summary>
        private string ClassData { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FavoriteEntryForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="favorite">お気に入り(試験車)</param>
        public FavoriteEntryForm(TestCarFavoriteItemModel favorite) : this()
        {
            //お気に入り
            this.Favorite = favorite;

            //データ区分
            this.ClassData = Const.FavoriteTestCar;

            //初期表示フォーカス
            this.ActiveControl = TitleTextBox;

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="favorite">お気に入り(カーシェア)</param>
        public FavoriteEntryForm(CarShareFavoriteItemModel favorite) : this()
        {
            //お気に入り
            this.Favorite = favorite;

            //データ区分
            this.ClassData = Const.FavoriteCarShare;

            //初期表示フォーカス
            this.ActiveControl = TitleTextBox;

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="favorite">お気に入り(外製車)</param>
        public FavoriteEntryForm(OuterCarFavoriteItemModel favorite) : this()
        {
            //お気に入り
            this.Favorite = favorite;

            //データ区分
            this.ClassData = Const.FavoriteOuterCar;

            //初期表示フォーカス
            this.ActiveControl = TitleTextBox;

        }

        /// <summary>
        /// コンストラクタ(作業履歴)
        /// </summary>
        /// <param name="favorite">お気に入り(カーシェア)</param>
        public FavoriteEntryForm(HistoryFavoriteItemModel favorite) : this()
        {
            var classData = "";

            switch (favorite.HISTORY_CODE)
            {
                //進捗履歴
                case Const.HistoryWorkProgress:
                    classData = Const.FavoriteWorkProgressHistory;
                    break;

                //作業履歴(試験車)
                case Const.HistoryTestCar:
                    classData = Const.FavoriteTestCarWorkHistory;
                    break;

                //作業履歴(外製車)
                case Const.HistoryOuterCar:
                    classData = Const.FavoriteOuterCarWorkHistory;
                    break;

                //作業履歴(カーシェア車)
                case Const.HistoryCarShare:
                    classData = Const.FavoriteCarShareWorkHistory;
                    break;
            }

            //お気に入り
            this.Favorite = favorite;

            //データ区分
            this.ClassData = classData;

            //初期表示フォーカス
            this.ActiveControl = TitleTextBox;

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
            //お気に入りのチェック
            if (this.IsEntryFavorite() == true)
            {
                //お気に入りの登録
                this.EntryFavorite();

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
            if (this.GetFavoriteList().Count() >= Const.FavoriteEntryMax)
            {
                Messenger.Warn(Resources.KKM00016);
                return false;

            }

            return true;

        }
        #endregion

        #region データの取得
        /// <summary>
        /// お気に入りの取得
        /// </summary>
        private IEnumerable<FavoriteSearchOutModel> GetFavoriteList()
        {
            //パラメータ設定
            var cond = new FavoriteSearchInModel
            {
                // ユーザーID
                PERSONEL_ID = SessionDto.UserId,

                // データ区分
                CLASS_DATA = this.ClassData

            };

            //APIで取得
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, cond);

            //レスポンスが取得できたかどうか
            var list = new List<FavoriteSearchOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region お気に入りの登録
        /// <summary>
        /// スケジュールの登録
        /// </summary>
        private void EntryFavorite()
        {
            var title = this.TitleTextBox.Text;

            var favorite = this.Favorite;

            ControllerType controller;

            //データ区分
            switch (this.ClassData)
            {
                //お気に入り(業務計画表)
                case Const.FavoriteWork:

                    var workFavorite = favorite as WorkFavoriteItemModel;

                    // ユーザーID
                    workFavorite.PERSONEL_ID =  SessionDto.UserId;

                    //タイトル
                    workFavorite.TITLE = title;


                    favorite = workFavorite;

                    //コントローラー
                    controller = ControllerType.WorkFavorite;
                    break;

                ////////お気に入り(月次計画表)
                //////case Const.FavoriteMonthlyWork:
                //////    break;

                //お気に入り(試験車日程)
                case Const.FavoriteTestCar:
                    var testCarFavorite = favorite as TestCarFavoriteItemModel;

                    // ユーザーID
                    testCarFavorite.PERSONEL_ID = SessionDto.UserId;

                    //タイトル
                    testCarFavorite.TITLE = title;

                    favorite = testCarFavorite;

                    //コントローラー
                    controller = ControllerType.TestCarFavorite;
                    break;

                //お気に入り(カーシェア)
                case Const.FavoriteCarShare:
                    var carShareFavorite = favorite as CarShareFavoriteItemModel;

                    // ユーザーID
                    carShareFavorite.PERSONEL_ID = SessionDto.UserId;

                    //タイトル
                    carShareFavorite.TITLE = title;


                    favorite = carShareFavorite;

                    //コントローラー
                    controller = ControllerType.CarShareFavorite;
                    break;

                //お気に入り(外製車日程)
                case Const.FavoriteOuterCar:
                    var outerCarFavorite = favorite as OuterCarFavoriteItemModel;

                    // ユーザーID
                    outerCarFavorite.PERSONEL_ID = SessionDto.UserId;

                    //タイトル
                    outerCarFavorite.TITLE = title;

                    favorite = outerCarFavorite;

                    //コントローラー
                    controller = ControllerType.OuterCarFavorite;
                    break;

                //お気に入り(進捗履歴)
                //お気に入り(作業履歴(試験車))
                //お気に入り(作業履歴(外製車))
                //お気に入り(作業履歴(カーシェア車))
                case Const.FavoriteWorkProgressHistory:
                case Const.FavoriteTestCarWorkHistory:
                case Const.FavoriteOuterCarWorkHistory:
                case Const.FavoriteCarShareWorkHistory:
                    var historyFavorite = favorite as HistoryFavoriteItemModel;

                    // ユーザーID
                    historyFavorite.PERSONEL_ID = historyFavorite.INPUT_PERSONEL_ID = SessionDto.UserId;

                    //タイトル
                    historyFavorite.TITLE = title;

                    favorite = historyFavorite;

                    //コントローラー
                    controller = ControllerType.HistoryFavorite;
                    break;

                default:
                    return;
            }

            //登録
            var res = HttpUtil.PostResponse<FavoriteSearchOutModel>(controller, favorite);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //登録後メッセージ
                Messenger.Info(Resources.KKM00002);

                //フォームクローズ
                base.FormOkClose();

            }

        }
        #endregion
    }
}
