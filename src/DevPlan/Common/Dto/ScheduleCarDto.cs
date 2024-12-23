namespace DevPlan.UICommon.Dto
{
    #region スケジュール利用車検索条件クラス
    /// <summary>
    /// スケジュール利用車検索条件クラス
    /// </summary>
    public class ScheduleCarSearchModel
    {
        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }
        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }
    }
    #endregion

    #region スケジュール利用車項目クラス
    /// <summary>
    /// スケジュール利用車項目クラス
    /// </summary>
    public class ScheduleCarModel
    {
        /// <summary>ID</summary>
        public int ID { get; set; }
        /// <summary>カテゴリーID</summary>
        public long? CATEGORY_ID { get; set; }
        /// <summary>GENERAL_CODE</summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>管理票番号</summary>
        public string 管理票番号 { get; set; }
        /// <summary>利用可能部署_ID</summary>
        public string 利用可能部署_ID { get; set; }
        /// <summary>部署単位</summary>
        public string 部署単位 { get; set; }
        /// <summary>FLAG_要予約許可</summary>
        public short FLAG_要予約許可 { get; set; }
        /// <summary>FLAG_鍵別管理</summary>
        public short FLAG_鍵別管理 { get; set; }

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        /// <summary>TESTCAR_ITEM_ID</summary>
        public long? TESTCAR_ITEM_ID { get; set; }
        /// <summary>TESTCAR_ITEM_CODE</summary>
        public string TESTCAR_ITEM_CODE { get; set; }
        /// <summary>OUTERCAR_ITEM_ID</summary>
        public long? OUTERCAR_ITEM_ID { get; set; }
        /// <summary>OUTERCAR_ITEM_CODE</summary>
        public string OUTERCAR_ITEM_CODE { get; set; }
        /// <summary>CARSHARING_ITEM_ID</summary>
        public long? CARSHARING_ITEM_ID { get; set; }
        /// <summary>GENERAL_CODE</summary>
        public string CARSHARING_ITEM_CODE { get; set; }
        //Append End 2022/02/03 杉浦 試験車日程の車も登録する
    }
    #endregion
}