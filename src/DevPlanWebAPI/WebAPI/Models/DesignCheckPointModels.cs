using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 設計チェック指摘検索入力モデルクラス
    /// </summary>
    public class DesignCheckPointGetInModel
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
        /// <summary>
        /// 指摘NO
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "指摘NO")]
        public int? 指摘NO { get; set; }

        //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試作管理NO
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "試作管理NO")]
        public int? 試作管理NO { get; set; }
        //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// <summary>
        /// 試験車ID
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "試験車_ID")]
        public int? 試験車_ID { get; set; }
        /// <summary>
        /// 状況記号
        /// </summary>
        [StringLength(2)]
        [Display(Name = "状況記号")]
        public string 状況記号 { get; set; }
        /// <summary>
        /// 担当課コード
        /// </summary>
        [StringLength(10)]
        [Display(Name = "担当課名")]
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当課長承認フラグ
        /// </summary>
        [Display(Name = "APPROVAL_FLG")]
        public bool? APPROVAL_FLG { get; set; }
        /// <summary>
        /// オープンフラグ
        /// </summary>
        [Display(Name = "OPEN_FLG")]
        public bool? OPEN_FLG { get; set; }
        /// <summary>
        /// 最新フラグ
        /// </summary>
        [Display(Name = "NEW_FLG")]
        public bool? NEW_FLG { get; set; }
    }
    /// <summary>
    /// 設計チェック指摘検索出力モデルクラス
    /// </summary>
    public class DesignCheckPointGetOutModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 指摘NO
        /// </summary>
        public int? 指摘NO { get; set; }

        //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試作管理NO
        /// </summary>
        public int? 試作管理NO { get; set; }
        //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// FLAG_CLOSE
        /// </summary>
        public int? FLAG_CLOSE { get; set; }

        //Append Start 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)
        /// <summary>
        /// FLAG_試作CLOSE
        /// </summary>
        public int? FLAG_試作CLOSE { get; set; }
        //Append End 2021/06/29 張晋華 開発計画表設計チェック機能改修(保守対応)

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
        /// 完了確認日
        /// </summary>
        public DateTime? 完了確認日 { get; set; }
        /// <summary>
        /// FLAG_上司承認
        /// </summary>
        public int? FLAG_上司承認 { get; set; }
        /// <summary>
        /// 試験車名
        /// </summary>
        public string 試験車名 { get; set; }
        /// <summary>
        /// 試験車_ID
        /// </summary>
        public int? 試験車_ID { get; set; }
        /// <summary>
        /// 対象車両_ID
        /// </summary>
        public int? 対象車両_ID { get; set; }
        /// <summary>
        /// 状況記号
        /// </summary>
        public string 状況記号 { get; set; }
        /// <summary>
        /// 担当課名
        /// </summary>
        public string 担当課名 { get; set; }
        /// <summary>
        /// 担当課_ID
        /// </summary>
        public string 担当課_ID { get; set; }
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
        /// 編集者日
        /// </summary>
        public DateTime? 編集者日 { get; set; }
        /// <summary>
        /// 編集者_ID
        /// </summary>
        public string 編集者_ID { get; set; }
        /// <summary>
        /// 編集者名
        /// </summary>
        public string 編集者名 { get; set; }
        /// <summary>
        /// 履歴作成日
        /// </summary>
        public DateTime? 履歴作成日 { get; set; }
        /// <summary>
        /// FLAG_最新
        /// </summary>
        public int? FLAG_最新 { get; set; }
        /// <summary>
        /// 履歴件数
        /// </summary>
        public long? HISTORY_COUNT { get; set; }
    }
    /// <summary>
    /// 設計チェック指摘登録入力モデルクラス
    /// </summary>
    public class DesignCheckPointPostInModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Range(-99999, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 開催日ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "開催日_ID")]
        public int 開催日_ID { get; set; }
        /// <summary>
        /// 指摘NO
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "指摘NO")]
        public int? 指摘NO { get; set; }

        //Append Start 2021/06/03 張晋華 開発計画表設計チェック機能改修
        /// <summary>
        /// 試作管理NO
        /// </summary>
        [Range(0, 99999)]
        [Display(Name = "試作管理NO")]
        public int? 試作管理NO { get; set; }
        //Append End 2021/06/03 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// FLAG_CLOSE
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_CLOSE")]
        public int? FLAG_CLOSE { get; set; }
        /// <summary>
        /// 指摘部品
        /// </summary>
        [StringLength(100)]
        [Display(Name = "部品")]
        public string 部品 { get; set; }
        /// <summary>
        /// 状況
        /// </summary>
        //Update Start 2021/08/05 杉浦 設計チェックインポート
        //[StringLength(500)]
        [StringLength(800)]
        //Update End 2021/08/05 杉浦 設計チェックインポート
        [Display(Name = "状況")]
        public string 状況 { get; set; }
        /// <summary>
        /// FLAG_処置不要
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_CLOSE")]
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
        //Update Start 2021/08/05 杉浦 設計チェックインポート
        //[StringLength(100)]
        [StringLength(800)]
        //Update End 2021/08/05 杉浦 設計チェックインポート
        [Display(Name = "処置対象")]
        public string 処置対象 { get; set; }
        /// <summary>
        /// 処置方法
        /// </summary>
        //Update Start 2021/08/05 杉浦 設計チェックインポート
        //[StringLength(500)]
        [StringLength(800)]
        //Update End 2021/08/05 杉浦 設計チェックインポート
        [Display(Name = "処置方法")]
        public string 処置方法 { get; set; }
        /// <summary>
        /// FLAG_調整済
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_CLOSE")]
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
        [Display(Name = "FLAG_CLOSE")]
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
        [Display(Name = "FLAG_CLOSE")]
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
        /// 編集者_ID
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "編集者_ID")]
        public string 編集者_ID { get; set; }
        /// <summary>
        /// 状況（対象）リスト
        /// </summary>
        [Display(Name = "状況（対象）リスト")]
        public List<DesignCheckProgressPostInModel> PROGRESS_LIST { get; set; } = new List<DesignCheckProgressPostInModel>();
    }
    /// <summary>
    /// 設計チェック指摘登録出力モデルクラス
    /// </summary>
    public class DesignCheckPointPostOutModel: DesignCheckPointGetOutModel
    {
        /// <summary>
        /// 仮指摘ID
        /// </summary>
        public int TEMP_ID { get; set; }
    }

    /// <summary>
    /// 設計チェック指摘削除入力モデルクラス
    /// </summary>
    public class DesignCheckPointDeleteInModel
    {
        /// <summary>
        /// 指摘ID
        /// </summary>
        [Required]
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int ID { get; set; }
    }
}