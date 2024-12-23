namespace DevPlan.UICommon.Dto
{
    #region スケジュール利用車詳細情報検索条件クラス
    /// <summary>
    /// スケジュール利用車詳細情報検索条件クラス
    /// </summary>
    public class ScheduleCarDetailSearchModel
    {
        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }
        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }
    }
    #endregion

    #region スケジュール利用車詳細情報項目クラス
    /// <summary>
    /// スケジュール利用車詳細情報項目クラス
    /// </summary>
    public class ScheduleCarDetailModel
    {
        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }
        /// <summary>車両区分</summary>
        public short? CAR_CLASS { get; set; }
        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }
        /// <summary>車型</summary>
        public string 車型 { get; set; }
        /// <summary>型式符号</summary>
        public string 型式符号 { get; set; }
        /// <summary>メーカー名</summary>
        public string メーカー名 { get; set; }
        /// <summary>外製車名</summary>
        public string 外製車名 { get; set; }
        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }
        /// <summary>リースNO</summary>
        public string リースNO { get; set; }
        /// <summary>開発符号</summary>
        public string 開発符号 { get; set; }
        /// <summary>試作時期</summary>
        public string 試作時期 { get; set; }
        /// <summary>号車</summary>
        public string 号車 { get; set; }
        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }
        /// <summary>E_G型式</summary>
        public string E_G型式 { get; set; }
        /// <summary>排気量</summary>
        public string 排気量 { get; set; }
        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }
        /// <summary>駆動方式</summary>
        public string 駆動方式 { get; set; }
        /// <summary>グレード</summary>
        public string グレード { get; set; }
        /// <summary>車体色</summary>
        public string 車体色 { get; set; }
        /// <summary>固定資産NO</summary>
        public string 固定資産NO { get; set; }
        /// <summary>処分予定年月</summary>
        public string 処分予定年月 { get; set; }
        /// <summary>リース満了日</summary>
        public string リース満了日 { get; set; }
        /// <summary>登録ナンバー</summary>
        public string 登録ナンバー { get; set; }
        /// <summary>FLAG_ナビ付</summary>
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>FLAG_ETC付</summary>
        public short? FLAG_ETC付 { get; set; }
        /// <summary>登録ナンバー</summary>
        public string 備考 { get; set; }
        /// <summary>部ID</summary>
        public string INPUT_DEPARTMENT_ID { get; set; }
        /// <summary>課ID</summary>
        public string INPUT_SECTION_ID { get; set; }
        /// <summary>担当ID</summary>
        public string INPUT_SECTION_GROUP_ID { get; set; }
        /// <summary>ユーザーID</summary>
        public string PERSONEL_ID { get; set; }
    }
    #endregion
}