namespace DevPlan.UICommon.Dto
{
    /// <summary>
    /// ロールマスタ検索入力モデルクラス
    /// </summary>
    public class RollGetInModel
    {
        /// <summary>
        /// ロールID
        /// </summary>
        public long? ROLL_ID { get; set; }
    }
    /// <summary>
    /// ロールマスタ検索出力モデルクラス
    /// </summary>
    public class RollModel
    {
        /// <summary>
        /// ロールID
        /// </summary>
        public long? ROLL_ID { get; set; }
        /// <summary>
        /// ロール名
        /// </summary>
        public string ROLL_NAME { get; set; }
        /// <summary>
        /// 機能ID
        /// </summary>
        public long FUNCTION_ID { get; set; }
        /// <summary>
        /// 機能名
        /// </summary>
        public string FUNCTION_NAME { get; set; }
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
        /// 自部更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_UPDATE_FLG { get; set; }
        /// <summary>
        /// 自部出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_EXPORT_FLG { get; set; }
        /// <summary>
        /// 自部管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_MANAGEMENT_FLG { get; set; }
    }
    /// <summary>
    /// ロールマスタ登録入力モデルクラス
    /// </summary>
    public class RollEntryInModel
    {
        /// <summary>
        /// ロールID
        /// </summary>
        public long? ROLL_ID { get; set; }
        /// <summary>
        /// ロール名
        /// </summary>
        public string ROLL_NAME { get; set; }
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
        /// 自部更新フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_UPDATE_FLG { get; set; }
        /// <summary>
        /// 自部出力フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_EXPORT_FLG { get; set; }
        /// <summary>
        /// 自部管理フラグ(0:権限なし 1:権限あり)
        /// </summary>
        public string JIBU_MANAGEMENT_FLG { get; set; }
        /// <summary>
        /// ユーザーID(登録者)
        /// </summary>
        public string INPUT_PERSONEL_ID { get; set; }
        /// <summary>
        /// ユーザーID(更新者)
        /// </summary>
        public string CHANGE_PERSONEL_ID { get; set; }
    }
    /// <summary>
    /// ロールマスタ削除入力モデルクラス
    /// </summary>
    public class RollDeleteInModel
    {
        /// <summary>
        /// ロールID
        /// </summary>
        public long ROLL_ID { get; set; }
    }
}