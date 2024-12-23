using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region 車両管理担当検索条件クラス
    /// <summary>
    /// 車両管理担当検索条件クラス
    /// </summary>
    public class CarManagerSearchModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>カテゴリID</summary>
        public string CATEGORY_ID { get; set; }

        /// <summary>STATUS</summary>
        public string STATUS { get; set; }

        /// <summary>機能ID</summary>
        public int FUNCTION_ID { get; set; }

        #endregion
    }
    #endregion

    #region 車両管理担当クラス
    /// <summary>
    /// 車両管理担当クラス
    /// </summary>
    public class CarManagerModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [StringLength(Const.GeneralCodeLength)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>課コード</summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }

        /// <summary>ユーザーID</summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }

        /// <summary>名前</summary>
        [StringLength(20)]
        [Display(Name = "名前")]
        public string NAME { get; set; }

        /// <summary>電話番号</summary>
        [StringLength(20)]
        [Display(Name = "電話番号")]
        public string TEL { get; set; }

        /// <summary>正副</summary>
        [StringLength(20)]
        [Display(Name = "STATUS")]
        public string STATUS { get; set; }

        /// <summary>ID</summary>
        [Display(Name = "ID")]
        public string ID { get; set; }

        /// <summary>カテゴリID</summary>
        public string CATEGORY_ID { get; set; }

        /// <summary>備考</summary>
        [StringLength(1000)]
        public string REMARKS { get; set; }

        /// <summary>機能ID</summary>
        public int FUNCTION_ID { get; set; }
        #endregion
    }
    #endregion
}