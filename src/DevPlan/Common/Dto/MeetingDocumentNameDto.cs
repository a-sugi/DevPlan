using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region 検討会資料名検索条件クラス
    /// <summary>
    /// 検討会資料名検索条件クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentNameSearchModel
    {
        #region プロパティ
        #endregion

    }
    #endregion

    #region 検討会資料名クラス
    /// <summary>
    /// 検討会資料名クラス
    /// </summary>
    [Serializable]
    public class MeetingDocumentNameModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        public int ID { get; set; }

        /// <summary>資料名</summary>
        public string 資料名 { get; set; }
        #endregion

    }
    #endregion

}
