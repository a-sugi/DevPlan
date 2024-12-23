using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// Xeyeデータ検索入力モデルクラス
    /// </summary>
    public class XeyeSearchInModel
    {
        /// <summary>
        /// 物品名2
        /// </summary>
        [StringLength(10)]
        [Display(Name = "物品名2")]
        public string 物品名2 { get; set; }
    }

    /// <summary>
    /// Xeyeデータ検索出力モデルクラス
    /// </summary>
    public class XeyeSearchOutModel
    {
        /// <summary>
        /// 物品コード
        /// </summary>
        [StringLength(10)]
        [Display(Name = "物品コード")]
        public string 物品コード { get; set; }

        /// <summary>
        /// 物品名
        /// </summary>
        [StringLength(200)]
        [Display(Name = "物品名")]
        public string 物品名 { get; set; }

        /// <summary>
        /// 物品名2
        /// </summary>
        [StringLength(10)]
        [Display(Name = "物品名2")]
        public string 物品名2 { get; set; }

        /// <summary>
        /// 物品名カナ
        /// </summary>
        [StringLength(200)]
        [Display(Name = "物品名カナ")]
        public string 物品名カナ { get; set; }

        /// <summary>
        /// 物品区分コード
        /// </summary>
        [StringLength(2)]
        [Display(Name = "物品区分コード")]
        public string 物品区分コード { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [StringLength(200)]
        [Display(Name = "備考")]
        public string 備考 { get; set; }

        /// <summary>
        /// ソート順
        /// </summary>
        [StringLength(10)]
        [Display(Name = "ソート順")]
        public string ソート順 { get; set; }

        /// <summary>
        /// 有効無効
        /// </summary>
        [StringLength(1)]
        [Display(Name = "有効無効")]
        public string 有効無効 { get; set; }
        
    }

    /// <summary>
    /// XeyeID検索出力モデルクラス
    /// </summary>
    public class XeyeDataSearchOutModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [StringLength(20)]
        [Display(Name ="ID")]
        public string ID { get; set; }

        /// <summary>
        /// PASS
        /// </summary>
        [StringLength(20)]
        [Display(Name = "PASS")]
        public string PASS { get; set; }

        /// <summary>
        /// INPUT_DATETIME
        /// </summary>
        [Display(Name = "INPUT_DATETIME")]
        public DateTime INPUT_DATETIME { get; set; }

        /// <summary>
        /// CHANGE_DATETIME
        /// </summary>
        [Display(Name = "CHANGE_DATETIME")]
        public DateTime CHANGE_DATETIME { get; set; }
    }
}