using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 試験車検索入力モデルクラス
    /// </summary>
    public class TestCarSearchInModel
    {
    }

    /// <summary>
    /// 試験車検索出力モデルクラス
    /// </summary>
    public class TestCarSearchOutModel
    {
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        [StringLength(20)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }
        /// <summary>
        /// E_G型式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "E_G型式")]
        public string E_G型式 { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }
        /// <summary>
        /// 車体色
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }
        /// <summary>
        /// リース満了日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "リース満了日")]
        public DateTime? リース満了日 { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// ナビ
        /// </summary>
        [StringLength(2)]
        [Display(Name = "ナビ")]
        public string ナビ { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }
        /// <summary>
        /// ETC
        /// </summary>
        [StringLength(2)]
        [Display(Name = "ETC")]
        public string ETC { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }
        /// <summary>
        /// GorT
        /// </summary>
        [StringLength(50)]
        [Display(Name = "GorT")]
        public string ESTABLISHMENT { get; set; }
        /// <summary>
        /// 分類
        /// </summary>
        [StringLength(50)]
        [Display(Name = "分類")]
        public string 分類 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "外製車名")]
        public string 外製車名 { get; set; }
        /// <summary>
        /// 課
        /// </summary>
        [StringLength(40)]
        [Display(Name = "課")]
        public string 課 { get; set; }
        /// <summary>
        /// 担当
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当")]
        public string 担当 { get; set; }//受領者(NAME)
        /// <summary>
        /// 型式符号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "型式符号")]
        public string 型式符号 { get; set; }
        /// <summary>
        /// 固定資産処分予定年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "処分予定年月")]
        public DateTime? 処分予定年月 { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }
        /// <summary>
        /// EG番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "EG番号")]
        public string E_G番号 { get; set; }
        /// <summary>
        /// 固定資産NO.
        /// </summary>
        [StringLength(10)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }
        /// <summary>
        /// リースNO.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "リースNO")]
        public string リースNO { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "研命ナンバー")]
        public string 研命ナンバー { get; set; }
        /// <summary>
        /// 研命期間
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "研命期間")]
        public DateTime? 研命期間 { get; set; }
        /// <summary>
        /// 車検登録日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検登録日")]
        public DateTime? 車検登録日 { get; set; }
        /// <summary>
        /// 次回車検期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "次回車検期限")]
        public DateTime? 車検期限 { get; set; }
        /// <summary>
        /// 車検期限まで残り
        /// </summary>
        [Display(Name = "車検期限まで残り")]
        public string 車検期限まで残り { get; set; }
        /// <summary>
        /// 廃艦日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃艦日")]
        public DateTime? 廃艦日 { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "名称備考")]
        public string 名称備考 { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(255)]
        [Display(Name = "試験目的")]
        public string 試験目的 { get; set; }
        /// <summary>
        /// メモ
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "メモ")]
        public string メモ { get; set; }
    }
}