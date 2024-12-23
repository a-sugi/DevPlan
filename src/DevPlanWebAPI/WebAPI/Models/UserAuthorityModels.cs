namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 機能権限検索モデルクラス（入力）
    /// </summary>
    public class UserAuthorityInModel
    {
        /// <summary>
        /// 部ID(必須)
        /// </summary>
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 課ID(必須)
        /// </summary>
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID(必須)
        /// </summary>
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 役職(必須)
        /// </summary>
        public string OFFICIAL_POSITION { get; set; }

        /// <summary>
        /// ユーザーID(必須)
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 機能ID(省略可)
        /// </summary>
        public int? FUNCTION_ID { get; set; }
    }

    /// <summary>
    /// 機能権限検索モデルクラス（出力）
    /// </summary>
    public class UserAuthorityOutModel
    {
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
        /// プリントスクリーンフラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string PRINTSCREEN_FLG { get; set; }

        /// <summary>
        /// カーシェア事務所フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string CARSHARE_OFFICE_FLG { get; set; }

        /// <summary>
        /// 全閲覧権限フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string ALL_GENERAL_CODE_FLG { get; set; }

        /// <summary>
        /// SKSフラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string SKS_FLG { get; set; }

        /// <summary>
        /// 自部署編集フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_UPDATE_FLG { get; set; }

        /// <summary>
        /// 自部署出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_EXPORT_FLG { get; set; }

        /// <summary>
        /// 自部署管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_MANAGEMENT_FLG { get; set; }

        /// <summary>
        /// ロールID(カンマ区切り)
        /// </summary>
        public string ROLL_IDS { get; set; }
    }
}