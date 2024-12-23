using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 設計チェック詳細検索入力モデルクラス
    /// </summary>
    public class DesignCheckDetailGetInModel
    {
        /// <summary>
        /// 開催日（開始）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "OPEN_START_DATE")]
        public DateTime? OPEN_START_DATE { get; set; }
        /// <summary>
        /// 開催日（終了）
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "OPEN_END_DATE")]
        public DateTime? OPEN_END_DATE { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        [StringLength(100)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }
        /// <summary>
        /// オープンフラグ
        /// </summary>
        [Display(Name = "OPEN_FLG")]
        public bool? OPEN_FLG { get; set; }
        /// <summary>
        /// 担当課コード
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当課名")]
        public string 担当課名 { get; set; }
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
        /// 指摘部品
        /// </summary>
        [StringLength(100)]
        [Display(Name = "部品")]
        public string 部品 { get; set; }
    }

    /// <summary>
    /// 設計チェック詳細検索出力モデルクラス
    /// </summary>
    public class DesignCheckDetailGetOutModel
    {
        /// <summary>
        /// 開催日
        /// </summary>
        public DateTime? 開催日 { get; set; }
        /// <summary>
        /// 設計チェック名称
        /// </summary>
        public string 名称 { get; set; }
        /// <summary>
        /// 指摘NO
        /// </summary>
        //UPDATE Start 2021/08/02 杉浦 設計チェック修正対応
        //public int 指摘NO { get; set; }
        public int? 指摘NO { get; set; }
        //UPDATE End 2021/08/02 杉浦 設計チェック修正対応
        //APPEND Start 2021/08/02 杉浦 設計チェック修正対応
        /// <summary>
        /// 試作管理NO
        /// </summary>
        //public int 指摘NO { get; set; }
        public int? 試作管理NO { get; set; }
        //APPEND End 2021/08/02 杉浦 設計チェック修正対応
        /// <summary>
        /// ステータス
        /// </summary>
        public string ステータス { get; set; }
        /// <summary>
        /// 指摘部品
        /// </summary>
        public string 部品 { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        public string 状況 { get; set; }
        /// <summary>
        /// FLAG_処置不要
        /// </summary>
        public int? FLAG_処置不要 { get; set; }
        /// <summary>
        /// 処置課
        /// </summary>
        public string 処置課 { get; set; }
        /// <summary>
        /// 処置対象
        /// </summary>
        public string 処置対象 { get; set; }
        /// <summary>
        /// 処置方法
        /// </summary>
        public string 処置方法 { get; set; }
        /// <summary>
        /// FLAG_調整済
        /// </summary>
        public int? FLAG_調整済 { get; set; }
        /// <summary>
        /// 処置調整
        /// </summary>
        public string 処置調整 { get; set; }
        /// <summary>
        /// 織込日程
        /// </summary>
        public DateTime? 織込日程 { get; set; }
        /// <summary>
        /// FLAG_試作改修
        /// </summary>
        public int? FLAG_試作改修 { get; set; }
        /// <summary>
        /// 部品納入日
        /// </summary>
        public DateTime? 部品納入日 { get; set; }
        /// <summary>
        /// 担当課名
        /// </summary>
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当者_ID
        /// </summary>
        public string 担当者_ID { get; set; }
        /// <summary>
        /// 担当者名
        /// </summary>
        public string 担当者名 { get; set; }
        /// <summary>
        /// 担当者_TEL
        /// </summary>
        public string 担当者_TEL { get; set; }
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// 状況記号
        /// </summary>
        public string 状況記号 { get; set; }
        /// <summary>
        /// 指摘ID
        /// </summary>
        public int 指摘ID { get; set; }
        /// <summary>
        /// 試験車_ID
        /// </summary>
        public int? 試験車_ID { get; set; }
    }
}