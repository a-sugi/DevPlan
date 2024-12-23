using System;

namespace DevPlan.UICommon.Dto
{
    #region 部長名検索クラス
    /// <summary>
    /// 部長名検索クラス
    /// </summary>
    [Serializable]
    public class ManagerNameSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get; set; }
        #endregion
    }
    #endregion

    #region 部長名クラス
    /// <summary>
    /// 部長名クラス
    /// </summary>
    [Serializable]
    public class ManagerNameModel
    {
        #region プロパティ
        /// <summary>
        /// 部長名
        /// </summary>
        public string MANAGER_NAME { get; set; }
        #endregion
    }
    #endregion
}
