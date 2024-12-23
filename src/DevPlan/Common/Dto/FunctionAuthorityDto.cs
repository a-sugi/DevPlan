//
// 業務計画表システム
// KKA15010 機能権限マスタ編集API
// 作成者 : 岸　義将
// 作成日 : 2017/02/21

namespace DevPlan.UICommon.Dto
{
    #region 機能権限検索モデル（入力）
    /// <summary>
    /// 機能権限マスタ編集モデルクラス（入力）
    /// </summary>
    public class FunctionAuthorityInModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        public string[] DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string[] SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string[] SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string[] PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID
        /// </summary>
        public long?[] FUNCTION_ID { get; set; }
    }
    #endregion

    #region 機能権限検索モデル（出力）
    /// <summary>
    /// 機能権限検索モデルクラス（出力）
    /// </summary>
    public class FunctionAuthorityOutModel
    {
        /// <summary>
        /// 機能名
        /// </summary>
        public string FUNCTION_NAME { get; set; }

        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }

        /// <summary>
        /// 参照フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string READ_FLG { get; set; }

        /// <summary>
        /// 更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string UPDATE_FLG { get; set; }

        /// <summary>
        /// 出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string EXPORT_FLG { get; set; }

        /// <summary>
        /// 管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string MANAGEMENT_FLG { get; set; }

    }
    #endregion

    #region 機能権限登録モデル
    /// <summary>
    /// 機能権限登録モデルクラス
    /// </summary>
    public class FunctionAuthorityRegistModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }

        /// <summary>
        /// 参照フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string READ_FLG { get; set; }

        /// <summary>
        /// 更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string UPDATE_FLG { get; set; }

        /// <summary>
        /// 出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string EXPORT_FLG { get; set; }

        /// <summary>
        /// 管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string MANAGEMENT_FLG { get; set; }

        /// <summary>
        /// 更新者ユーザーID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }

    }
    #endregion

    #region 機能権限更新モデル
    /// <summary>
    /// 機能権限更新モデルクラス
    /// </summary>
    public class FunctionAuthorityUpdateModel
    {
        /// <summary>
        /// 参照フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string READ_FLG { get; set; }

        /// <summary>
        /// 更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string UPDATE_FLG { get; set; }

        /// <summary>
        /// 出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string EXPORT_FLG { get; set; }

        /// <summary>
        /// 管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string MANAGEMENT_FLG { get; set; }

        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }

        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 更新者ユーザーID
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    #endregion

    #region 機能権限削除モデル
    /// <summary>
    /// 機能権限削除モデルクラス
    /// </summary>
    public class FunctionAuthorityDeleteModel
    {
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }
    }
    #endregion
}