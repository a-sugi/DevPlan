using System;

namespace DevPlan.UICommon.Dto
{
    #region 課検索条件クラス
    /// <summary>
    ///課検索条件クラス
    /// </summary>
    [Serializable]
    public class SectionSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        #endregion
    }
    #endregion

    #region 課項目クラス
    /// <summary>
    /// 課項目クラス
    /// </summary>
    [Serializable]
    public class SectionModel
    {
        #region プロパティ
        /// <summary>
        /// 部名
        /// </summary>
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 課名
        /// </summary>
        public string SECTION_NAME { get; set; }
        /// <summary>
        /// 課コード
        /// </summary>
        public string SECTION_CODE { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get; set; }
        /// <summary>
        /// ソートNO（SECTION_DATA）
        /// </summary>
        public short SORT_NO_SECTION_DATA { get; set; }
        /// <summary>
        /// ソートNO（DEPARTMENT_DATA）
        /// </summary>
        public long SORT_NO_DEPARTMENT_DATA { get; set; }
        /// <summary>
        /// 研実フラグ（1：実験、2：設計、3：その他）
        /// </summary>
        public short? FLAG_KENJITSU { get; set; }
        #endregion
    }
    #endregion
}
