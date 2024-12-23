using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{

    //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

    /// <summary>
    /// 設計チェック指摘インポートモデルクラス
    /// </summary>
    public class DesignCheckImportModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int? 開催日_ID { get; set; }

        //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 指摘NO
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "指摘NO")]
        public int? 指摘NO { get; set; }
        //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

        //Append Start 2021/06/11 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試験車_ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "試験車_ID")]
        public int? 試験車_ID { get; set; }

        //Update Start 2021/06/15 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試験車名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "試験車名")]
        public string 試験車名 { get; set; }
        //Update End 2021/06/15 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// 試作管理NO
        /// </summary> 
        [Range(0, 99999)]
        [Display(Name = "試作管理NO")]
        public int? 試作管理NO { get; set; }
        /// <summary>
        /// FLAG_CLOSE
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_CLOSE")]
        public int? FLAG_CLOSE { get; set; }

        //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
        /// <summary>
        /// FLAG_試作CLOSE
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_試作CLOSE")]
        public int? FLAG_試作CLOSE { get; set; }
        //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

        /// <summary>
        /// 指摘部品
        /// </summary>
        [StringLength(100)]
        [Display(Name = "部品")]
        public string 部品 { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        //Update Start 2021/07/29 杉浦 設計チェックインポート
        //[StringLength(500)]
        [StringLength(800)]
        //Update End 2021/07/29 杉浦 設計チェックインポート
        [Display(Name = "状況")]
        public string 状況 { get; set; }
        /// <summary>
        /// FLAG_処置不要
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_処置不要")]
        public int? FLAG_処置不要 { get; set; }
        /// <summary>
        /// 処置課
        /// </summary>
        [StringLength(100)]
        [Display(Name = "処置課")]
        public string 処置課 { get; set; }
        /// <summary>
        /// 処置対象
        /// </summary>
        //Update Start 2021/07/28 杉浦 設計チェックインポート
        //[StringLength(100)]
        [StringLength(800)]
        //Update End 2021/07/28 杉浦 設計チェックインポート
        [Display(Name = "処置対象")]
        public string 処置対象 { get; set; }
        /// <summary>
        /// 処置方法
        /// </summary>
        //Update Start 2021/07/29 杉浦 設計チェックインポート
        //[StringLength(500)]
        [StringLength(800)]
        //Update End 2021/07/29 杉浦 設計チェックインポート
        [Display(Name = "処置方法")]
        public string 処置方法 { get; set; }
        /// <summary>
        /// FLAG_調整済
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_調整済")]
        public int? FLAG_調整済 { get; set; }
        /// <summary>
        /// 処置調整
        /// </summary>
        [StringLength(200)]
        [Display(Name = "処置調整")]
        public string 処置調整 { get; set; }
        /// <summary>
        /// 織込日程
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "織込日程")]
        public DateTime? 織込日程 { get; set; }
        /// <summary>
        /// FLAG_試作改修
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_試作改修")]
        public int? FLAG_試作改修 { get; set; }
        /// <summary>
        /// 部品納入日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "部品納入日")]
        public DateTime? 部品納入日 { get; set; }
        /// <summary>
        /// 完了確認日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "完了確認日")]
        public DateTime? 完了確認日 { get; set; }
        /// <summary>
        /// FLAG_上司承認
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_上司承認")]
        public int? FLAG_上司承認 { get; set; }
        /// <summary>
        /// 担当課名
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当課名")]
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当課_ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当課_ID")]
        public string 担当課_ID { get; set; }
        /// <summary>
        /// 担当者_ID
        /// </summary>
        [StringLength(40)]
        [Display(Name = "担当者_ID")]
        public string 担当者_ID { get; set; }
        /// <summary>
        /// 担当者_TEL
        /// </summary>
        [StringLength(40)]
        [Display(Name = "担当者_TEL")]
        public string 担当者_TEL { get; set; }
        /// <summary>
        /// 編集者日
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? 編集者日 { get; set; }
        /// <summary>
        /// 編集者_ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "編集者_ID")]
        public string 編集者_ID { get; set; }
        /// <summary>
        /// 履歴作成日
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? 履歴作成日 { get; set; }
        /// <summary>
        /// FLAG_最新
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_最新")]
        public int? FLAG_最新 { get; set; }
    }
    //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

    //Append Start 2021/08/20 杉浦 設計チェック請負
    public class DesignCheckPathModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        [Display(Name = "PATH")]
        public string PATH { get; set; }
    }
    //Append End 2021/08/20 杉浦 設計チェック請負
}