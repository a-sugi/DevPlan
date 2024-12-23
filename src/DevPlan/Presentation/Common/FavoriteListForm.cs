using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// お気に入り編集削除
    /// </summary>
    public partial class FavoriteListForm : BaseSubForm
    {
        #region メンバ変数
        private const int ClmIndexDel = 0;
        private const int ClmIndexNam = 1;
        private const int ClmIndexDay = 2;
        private const int ClmIndexId  = 3;
        private const int ClmIndexKbn = 4;
        #endregion

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// お気に入り検索条件モデル
        /// </summary>
        public FavoriteSearchInModel FAVORITE { get; set; }
        /// <summary>
        /// ログイン時のユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "お気に入り編集"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return false; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FavoriteListForm(FavoriteSearchInModel favorite)
        {

            InitializeComponent();

            //お気に入り[データ区分]
            this.FAVORITE = favorite;
            //ユーザーID
            this.FAVORITE.PERSONEL_ID = SessionDto.UserId;

            //FavoriteListDataGridViewの3番目の列「登録日」を読み取り専用にする
            FavoriteListDataGridView.Columns[ClmIndexDay].ReadOnly = true;
        }
        #endregion

        #region Form Load
        /// <summary>
        /// FavoriteListForm_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteListForm_Load(object sender, EventArgs e)
        {
            //初期表示処理
            this.SetGridData();
        }
        #endregion

        #region グリッドデータのセット
        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            //カラムの自動生成抑止
            this.FavoriteListDataGridView.AutoGenerateColumns = false;

            //IDカラムバインド
            this.FavoriteIDTextBoxColumn.DataPropertyName = "ID";
            //データ区分
            this.FavoriteClassIDTextBoxColumn.DataPropertyName = "CLASS_DATA";
            //お気に入り名カラムバインド
            this.FavoriteNameTextBoxColumn.DataPropertyName = "TITLE";
            //登録日カラムバインド
            this.InputDateTextBoxColumn.DataPropertyName = "INPUT_DATETIME";
            //登録日の表示形式を yyyy/MM/dd に設定
            this.InputDateTextBoxColumn.DefaultCellStyle.Format = Const.FormatDate;

            //グリッドのデータソースをAPIから受け取るデータに設定
            this.FavoriteListDataGridView.DataSource = GetGridDataList();
        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        public List<FavoriteSearchOutModel> GetGridDataList()
        {
            //パラメータ設定
            var itemCond = new FavoriteSearchInModel
            {
                //ユーザーID
                PERSONEL_ID = FAVORITE.PERSONEL_ID,
                //データ区分
                CLASS_DATA = FAVORITE.CLASS_DATA,
            };

            //Get実行
            var res = HttpUtil.GetResponse<FavoriteSearchInModel, FavoriteSearchOutModel>(ControllerType.Favorite, itemCond);

            return res.Results.ToList();
        }
        #endregion

        #region 更新ボタンのクリック
        /// <summary>
        /// 更新ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteListUpdateButton_Click(object sender, EventArgs e)
        {
            var list = new List<FavoriteUpdateModel>();

            //全リスト処理
            foreach (DataGridViewRow dgr in FavoriteListDataGridView.Rows)
            {
                if(Convert.ToString(dgr.Cells[ClmIndexNam].Value) == "")
                {
                    Messenger.Warn(string.Format(Resources.KKM00001,"お気に入り名"));
                    return;
                }
                list.Add(new FavoriteUpdateModel
                {
                    //ID
                    ID = Convert.ToInt64(dgr.Cells[ClmIndexId].Value),

                    //タイトル
                    TITLE = Convert.ToString(dgr.Cells[ClmIndexNam].Value),

                    //ユーザーID
                    PERSONEL_ID = SessionDto.UserId,

                    //データ区分
                    CLASS_DATA = Convert.ToString(dgr.Cells[ClmIndexKbn].Value)

                });
            }
            
            var msg = Resources.KKM00002;

            //更新
            var res = HttpUtil.PutResponse<FavoriteUpdateModel>(ControllerType.Favorite, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //更新後メッセージ
                Messenger.Info(Resources.KKM00002); // 登録（更新）完了

                //フォームクローズ
                base.FormOkClose();

            }

        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoriteListDeleteButton_Click(object sender, EventArgs e)
        {
            var list = new List<FavoriteDeleteModel>();

            //削除指定チェック
            foreach (DataGridViewRow dgr in FavoriteListDataGridView.Rows)
            {
                var chk = dgr.Cells[0] as DataGridViewCheckBoxCell;

                //選択している行のお気に入りは削除対象
                if (chk.Value != null && Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(new FavoriteDeleteModel
                    {
                        //ID
                        ID = Convert.ToInt64(dgr.Cells[ClmIndexId].Value),

                        //データ区分
                        CLASS_DATA = Convert.ToString(dgr.Cells[ClmIndexKbn].Value)

                    });

                }

            }

            //削除対象があるかどうか
            if (list.Any() == true)
            {
                //削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
                {
                    return;

                }

                //お気に入り項目の削除
                var res = HttpUtil.DeleteResponse<FavoriteDeleteModel>(ControllerType.Favorite, list);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    //レスポンスが取得できたかどうか
                    if (res.Status.Equals(Const.StatusSuccess))
                    {
                        //登録後メッセージ
                        Messenger.Info(Resources.KKM00003);

                        //フォームクローズ
                        base.FormOkClose();

                    }

                }

            }
            else
            {
                //選択項目なしメッセージ
                Messenger.Warn(Resources.KKM00009);

            }
        }
        #endregion
    }
}
