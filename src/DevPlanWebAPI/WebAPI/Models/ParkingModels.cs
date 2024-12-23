using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 駐車場検索クラス
    /// <summary>
    /// 駐車場検索クラス
    /// </summary>
    [Serializable]
    public class ParkingSearchModel
    {
        /// <summary>LOCATION_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "LOCATION_NO")]
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "AREA_NO")]
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "SECTION_NO")]
        public int? SECTION_NO { get; set; }

        /// <summary>STATUS</summary>
        //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
        //[Range(0, 1)]
        [Range(0, 2)]
        //Update Start 2021/11/09 杉浦 全エリア表示時にエリアの内容も含める
        [Display(Name = "STATUS")]
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
        [Range(0, 9999999999)]
        [Display(Name = "LOCATION_NO")]
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "AREA_NO")]
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "SECTION_NO")]
        public int? SECTION_NO { get; set; }

        /// <summary>NAME</summary>
        [StringLength(100)]
        [Display(Name = "NAME")]
        public string NAME { get; set; }

        /// <summary>LOCATION</summary>
        [StringLength(100)]
        [Display(Name = "LOCATION")]
        public string LOCATION { get; set; }

        /// <summary>MAP_PDF</summary>
        [StringLength(1000)]
        [Display(Name = "MAP_PDF")]
        public string MAP_PDF { get; set; }

        /// <summary>SORT_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "SORT_NO")]
        public int? SORT_NO { get; set; }

        /// <summary>STATUS</summary>
        [Range(0, 1)]
        [Display(Name = "STATUS")]
        public short? STATUS { get; set; }

        /// <summary>INPUT_DATETIME</summary>
        [Display(Name = "INPUT_DATETIME")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>INPUT_PERSONEL_ID</summary>
        [StringLength(20)]
        [Display(Name = "INPUT_PERSONEL_ID")]
        public string INPUT_PERSONEL_ID { get; set; }

        /// <summary>STATUS_CODE</summary>
        [StringLength(100)]
        [Display(Name = "STATUS_CODE")]
        public string STATUS_CODE { get; set; }

        /// <summary>ESTABLISHMENT</summary>
        [StringLength(10)]
        [Display(Name = "ESTABLISHMENT")]
        public string ESTABLISHMENT { get; set; }

        /// <summary>データID</summary>
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int? データID { get; set; }

        /// <summary>管理票NO</summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }

        //Append Start 2021/10/07 矢作
        /// <summary>NAME</summary>
        [StringLength(100)]
        [Display(Name = "AREA_NAME")]
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
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
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
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int? データID { get; set; }

        /// <summary>LOCATION_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "LOCATION_NO")]
        public int? LOCATION_NO { get; set; }

        /// <summary>AREA_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "AREA_NO")]
        public int? AREA_NO { get; set; }

        /// <summary>SECTION_NO</summary>
        [Range(0, 9999999999)]
        [Display(Name = "SECTION_NO")]
        public int? SECTION_NO { get; set; }

        /// <summary>INPUT_DATETIME</summary>
        [Display(Name = "INPUT_DATETIME")]
        public DateTime? INPUT_DATETIME { get; set; }

        /// <summary>INPUT_PERSONEL_ID</summary>
        [StringLength(20)]
        [Display(Name = "INPUT_PERSONEL_ID")]
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
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
    }

    /// <summary>
    /// 駐車カウントクラス
    /// </summary>
    [Serializable]
    public class ParkingCountModel
    {
        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>駐車カウント</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "COUNT")]
        public long COUNT { get; set; }
    }
    #endregion

    //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
    #region 試験車ダミークラス
    /// <summary>
    /// 試験車ダミークラス
    /// </summary>
    [Serializable]
    public class TestCarDummyModel
    {
        /// <summary>データID</summary>
        [Display(Name = "データID")]
        public string データID { get; set; }

        /// <summary>削除フラグ</summary>
        [Display(Name = "DELETE_FLG")]
        public string DELETE_FLG { get; set; }

        /// <summary>登録日</summary>
        [Display(Name = "INPUT_DATETIME")]
        public DateTime INPUT_DATETIME { get; set; }

        /// <summary>登録者</summary>
        [Display(Name = "INPUT_PERSONEL_ID")]
        public string INPUT_PERSONEL_ID { get; set; }
    }
    #endregion
    //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
}