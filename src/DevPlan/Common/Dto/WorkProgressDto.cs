using System;

namespace DevPlan.UICommon.Dto
{
    #region 車種別進捗状況一覧項目検索条件クラス
    /// <summary>
    /// 車種別進捗状況一覧項目検索条件クラス
    /// </summary>
    [Serializable]
    public class WorkProgressItemSearchModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long? ID { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

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

        /// <summary>ステータス</summary>
        public string OPEN_CLOSE { get; set; }
        #endregion
    }
    #endregion

    #region 車種別進捗状況一覧項目クラス
    /// <summary>
    /// 車種別進捗状況一覧項目クラス
    /// </summary>
    [Serializable]
    public class WorkProgressItemModel
    {
        #region プロパティ
        /// <summary>担当名</summary>
        public string SECTION_GROUP_CODE { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>項目名</summary>
        public string CATEGORY { get; set; }

        /// <summary>ステータス</summary>
        public string OPEN_CLOSE { get; set; }

        /// <summary>終了日</summary>
        public DateTime? CLOSED_DATE { get; set; }

        /// <summary>日付</summary>
        public DateTime? LISTED_DATE { get; set; }

        /// <summary>現状</summary>
        public string CURRENT_SITUATION { get; set; }

        /// <summary>今後の予定</summary>
        public string FUTURE_SCHEDULE { get; set; }

        /// <summary>キーワード</summary>
        public string SELECT_KEYWORD { get; set; }

        /// <summary>編集者</summary>
        public string NAME { get; set; }

        /// <summary>編集日時</summary>
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>ソート順</summary>
        public int? SORT_NO { get; set; }
        #endregion
    }
    #endregion
}
