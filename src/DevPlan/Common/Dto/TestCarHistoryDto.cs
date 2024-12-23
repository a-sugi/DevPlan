using System;
using System.Collections.Generic;

namespace DevPlan.UICommon.Dto
{
    #region 試験車使用履歴検索クラス
    /// <summary>
    /// 試験車使用履歴検索クラス
    /// </summary>
    public class TestCarUseHistorySearchModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        public int 履歴NO { get; set; }

        /// <summary>
        /// SEQNO
        /// </summary>
        public int? SEQNO { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        public int? STEPNO { get; set; }
        #endregion
    }
    #endregion

    #region 試験車履歴クラス
    /// <summary>
    /// 試験車履歴クラス
    /// </summary>
    public class TestCarHistoryModel
    {
        #region プロパティ
        /// <summary>試験車</summary>
        public TestCarCommonModel TestCar { get; set; }

        /// <summary>試験車使用履歴</summary>
        public IEnumerable<TestCarUseHistoryModel> TestCarUseHistoryList { get; set; }
        #endregion
    }
    #endregion

}