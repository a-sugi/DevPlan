using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region 車検・点検リスト発行検索条件クラス
    /// <summary>
    /// 車検・点検リスト発行検索条件クラス
    /// </summary>
    public class CarInspectionSearchModel : TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 点検区分
        /// </summary>
        public short?[] 点検区分 { get; set; }

        /// <summary>
        /// 車検区分1
        /// </summary>
        public string[] 車検区分1 { get; set; }
        #endregion
    }
    #endregion

    #region 車検・点検リスト発行クラス
    /// <summary>
    /// 車検・点検リスト発行検索条件クラス
    /// </summary>
    public class CarInspectionModel : TestCarCommonModel
    {
        #region プロパティ
        /// <summary>
        /// 点検区分
        /// </summary>
        public short? 点検区分 { get; set; }

        /// <summary>
        /// 点検区分名
        /// </summary>
        public string 点検区分名 { get; set; }

        /// <summary>
        /// 点検期限
        /// </summary>
        public DateTime? 点検期限 { get; set; }
        #endregion
    }
    #endregion

}
