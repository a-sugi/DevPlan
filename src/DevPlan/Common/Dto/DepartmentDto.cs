using System;

namespace DevPlan.UICommon.Dto
{
    #region 部検索条件クラス
    /// <summary>
    ///部検索条件クラス
    /// </summary>
    [Serializable]
    public class DepartmentSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 部コード
        /// </summary>
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public string ESTABLISHMENT { get; set; }
        #endregion
    }
    #endregion

    #region 部項目クラス
    /// <summary>
    /// 部項目クラス
    /// </summary>
    [Serializable]
    public class DepartmentModel
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
        /// </summary>
        /// ソートNO
        /// </summary>
        public long SORT_NO { get; set; }
        #endregion
    }
    #endregion
}
