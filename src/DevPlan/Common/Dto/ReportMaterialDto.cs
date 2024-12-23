using System;

namespace DevPlan.UICommon.Dto
{
    #region 週報項目検索条件クラス
    /// <summary>
    /// 週報項目検索条件クラス
    /// </summary>
    [Serializable]
    public class InfoListSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 検索区分
        /// </summary>
        public int CLASS_DATA { get; set; }
        /// <summary>
        /// 検索開始日
        /// </summary>
        public DateTime? FIRST_DAY { get; set; }
        /// <summary>
        /// 検索終了日
        /// </summary>
        public DateTime? LAST_DAY { get; set; }
        /// <summary>
        /// 作成単位
        /// </summary>
        public string 作成単位 { get; set; }
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
        #endregion
    }
    #endregion

    #region 週報項目条件クラス
    /// <summary>
    /// 週報項目条件クラス
    /// </summary>
    [Serializable]
    public class InfoListModel
    {
        #region プロパティ
        /// <summary>
        /// 日付
        /// </summary>
        public DateTime? LISTED_DATE { get; set; }
        /// <summary>
        /// 課
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        public string SECTION_GROUP_CODE { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }
        /// <summary>
        /// 項目
        /// </summary>
        public string CATEGORY { get; set; }
        /// <summary>
        /// 現状
        /// </summary>
        public string CURRENT_SITUATION { get; set; }
        /// <summary>
        /// 今後の予定
        /// </summary>
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>
        /// 選択キーワード
        /// </summary>
        public object SELECT_KEYWORD { get; set; }

        /// <summary>
        /// 編集日時
        /// </summary>
        public object INPUT_DATETIME { get; set; }

        /// <summary>
        /// 編集者
        /// </summary>
        public object PERSONEL_NAME { get; set; }

        /// <summary>
        /// OPEN_CLOSE
        /// </summary>
        public object OPEN_CLOSE { get; set; }
        #endregion
    }
    #endregion
}
