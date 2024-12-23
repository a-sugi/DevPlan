using System;

namespace DevPlan.UICommon.Dto
{
    #region 駐車場検索クラス
    /// <summary>
    /// 駐車場検索クラス
    /// </summary>
    [Serializable]
    public class ParkingSearchModel
    {
        /// <summary>LOCATION_NO</summary>
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        public int? SECTION_NO { get; set; }

        /// <summary>STATUS</summary>
        public short? STATUS { get; set; }
    }
    #endregion

    #region 駐車場所在地クラス
    /// <summary>
    /// 駐車場所在地クラス
    /// </summary>
    [Serializable]
    public class ParkingModel
    {
        /// <summary>LOCATION_NO</summary>
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        public int? SECTION_NO { get; set; }

        /// <summary>NAME</summary>
        public string NAME { get; set; }

        /// <summary>LOCATION</summary>
        public string LOCATION { get; set; }

        /// <summary>MAP_PDF</summary>
        public string MAP_PDF { get; set; }

        /// <summary>SORT_NO</summary>
        public int? SORT_NO { get; set; }

        /// <summary>STATUS</summary>
        public short? STATUS { get; set; }

        /// <summary>INPUT_DATETIME</summary>
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>INPUT_PERSONEL_ID</summary>
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>STATUS_CODE</summary>
        public string STATUS_CODE { get; set; }

        /// <summary>ESTABLISHMENT</summary>
        public string ESTABLISHMENT { get; set; }

        /// <summary>データID</summary>
        public string データID { get; set; }

        /// <summary>管理票NO</summary>
        public string 管理票NO { get; set; }


        //Append Start 2021/10/07 矢作
        /// <summary>AREA_NAME</summary>
        public string AREA_NAME { get; set; }
        //Append End 2021/10/07 矢作
    }
    #endregion

    #region 駐車場管理クラス
    /// <summary>
    /// 駐車場管理クラス
    /// </summary>
    [Serializable]
    public class ParkingUseSearchModel
    {
        /// <summary>データID</summary>
        public int? データID { get; set; }
    }
    #endregion

    #region 駐車場管理クラス
    /// <summary>
    /// 駐車場管理クラス
    /// </summary>
    [Serializable]
    public class ParkingUseModel
    {
        /// <summary>データID</summary>
        public int? データID { get; set; }

        /// <summary>LOCATION_NO</summary>
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        public int? SECTION_NO { get; set; }

        /// <summary>INPUT_DATETIME</summary>
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>INPUT_PERSONEL_ID</summary>
        public string INPUT_PERSONEL_ID { get; set; }
    }
    #endregion

    #region 駐車カウントクラス
    /// <summary>
    /// 駐車カウント検索クラス
    /// </summary>
    [Serializable]
    public class ParkingCountSearchModel
    {
        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }
    }

    /// <summary>
    /// 駐車カウントクラス
    /// </summary>
    [Serializable]
    public class ParkingCountModel
    {
        /// <summary>駐車場番号</summary>
        public string 駐車場番号 { get; set; }

        /// <summary>駐車カウント</summary>
        public long COUNT { get; set; }
    }
    #endregion
}
