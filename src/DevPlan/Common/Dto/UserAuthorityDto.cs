using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// KKA00230 機能権限検索モデルクラス（入力）
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
    /// KKA00230 機能権限検索モデルクラス（出力）
    /// </summary>
    public class UserAuthorityOutModel
    {
        /// <summary>
        /// 機能ID
        /// </summary>
        public int FUNCTION_ID { get; set; }

        /// <summary>
        /// 参照フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char READ_FLG { get; set; }

        /// <summary>
        /// 更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char UPDATE_FLG { get; set; }

        /// <summary>
        /// 出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char EXPORT_FLG { get; set; }

        /// <summary>
        /// 管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char MANAGEMENT_FLG { get; set; }

        /// <summary>
        /// プリントスクリーンフラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char PRINTSCREEN_FLG { get; set; }

        /// <summary>
        /// カーシェア事務所フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char CARSHARE_OFFICE_FLG { get; set; }

        /// <summary>
        /// 全閲覧権限フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char ALL_GENERAL_CODE_FLG { get; set; }

        /// <summary>
        /// SKSフラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char SKS_FLG { get; set; }

        /// <summary>
        /// 自部署編集フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char JIBU_UPDATE_FLG { get; set; }

        /// <summary>
        /// 自部署印刷フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char JIBU_EXPORT_FLG { get; set; }

        /// <summary>
        /// 自部署管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public char JIBU_MANAGEMENT_FLG { get; set; }

        /// <summary>
        /// ロールID(カンマ区切り)
        /// </summary>
        public string ROLL_IDS { get; set; }

        /// <summary>
        /// ロールIDリスト
        /// </summary>
        public List<string> ROLL_ID_LIST { get; set; } = new List<string>();
    }
}