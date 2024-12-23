using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region スケジュール利用車詳細情報検索条件クラス
    /// <summary>
    /// スケジュール利用車詳細情報検索条件クラス
    /// </summary>
    public class ScheduleCarDetailSearchModel
    {
        /// <summary>カテゴリーID</summary>
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public long? CATEGORY_ID { get; set; }
        /// <summary>管理票番号</summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }
    }
    #endregion

    #region スケジュール利用車詳細情報項目クラス
    /// <summary>
    /// スケジュール利用車詳細情報項目クラス
    /// </summary>
    public class ScheduleCarDetailModel
    {
        /// <summary>カテゴリーID</summary>
        [Required]
        [Range(Const.ScheduleIdMin, Const.ScheduleIdMax)]
        [Display(Name = "カテゴリーID")]
        public long CATEGORY_ID { get; set; }
        /// <summary>車両区分</summary>
        [Range(0, 99)]
        [Display(Name = "車両区分")]
        public short? CAR_CLASS { get; set; }
        /// <summary>管理票番号</summary>
        [StringLength(10)]
        [Display(Name = "管理票番号")]
        public string 管理票番号 { get; set; }
        /// <summary>車型</summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }
        /// <summary>型式符号</summary>
        [StringLength(50)]
        [Display(Name = "型式符号")]
        public string 型式符号 { get; set; }
        /// <summary>メーカー名</summary>
        [StringLength(50)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }
        /// <summary>外製車名</summary>
        [StringLength(50)]
        [Display(Name = "外製車名")]
        public string 外製車名 { get; set; }
        /// <summary>駐車場番号</summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
        /// <summary>リースNO</summary>
        [StringLength(20)]
        [Display(Name = "リースNO")]
        public string リースNO { get; set; }
        /// <summary>開発符号</summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }
        /// <summary>試作時期</summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }
        /// <summary>号車</summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }
        /// <summary>仕向地</summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }
        /// <summary>E_G型式</summary>
        [StringLength(50)]
        [Display(Name = "E_G型式")]
        public string E_G型式 { get; set; }
        /// <summary>排気量</summary>
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }
        /// <summary>トランスミッション</summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }
        /// <summary>駆動方式</summary>
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }
        /// <summary>グレード</summary>
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }
        /// <summary>車体色</summary>
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }
        /// <summary>固定資産NO</summary>
        [StringLength(10)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }
        /// <summary>処分予定年月</summary>
        [StringLength(10)]
        [Display(Name = "処分予定年月")]
        public string 処分予定年月 { get; set; }
        /// <summary>リース満了日</summary>
        [StringLength(10)]
        [Display(Name = "リース満了日")]
        public string リース満了日 { get; set; }
        /// <summary>登録ナンバー</summary>
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }
        /// <summary>FLAG_ナビ付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>FLAG_ETC付</summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }
        /// <summary>登録ナンバー</summary>
        [StringLength(1000)]
        [Display(Name = "備考")]
        public string 備考 { get; set; }
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "部ID")]
        public string INPUT_DEPARTMENT_ID { get; set; }
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課ID")]
        public string INPUT_SECTION_ID { get; set; }
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当ID")]
        public string INPUT_SECTION_GROUP_ID { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }
    }
    #endregion
}