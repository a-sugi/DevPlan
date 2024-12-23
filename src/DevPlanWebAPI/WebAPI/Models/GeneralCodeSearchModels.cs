using DevPlanWebAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 開発符号検索モデルクラス（入力）
    /// </summary>
    public class GeneralCodeSearchInModel
    {
        /// <summary>
        /// 検索区分(0:部分一致検索、1:完全一致検索 省略時は0と解釈)
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "検索区分")]
        public int CLASS_DATA { get; set; } = 0;

        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "開発符号")]
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 開発フラグ(0:開発外 1:開発中 省略時は条件に含めない)
        /// </summary>
        [StringLength(1)]
        [Display(Name = "開発フラグ")]
        public string UNDER_DEVELOPMENT { get; set; }

        /// <summary>
        /// 社員コード（パーソナルID）
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 先開フラグ(0:先開含めない（＜＞'先開'） 1:先開含める（条件なし） 省略時は条件に含めない)
        /// </summary>
        [StringLength(1)]
        [Display(Name = "先開フラグ")]
        public string INCLUDE_PRECEDING_DEVELOPMENT { get; set; }

        /// <summary>
        /// 機能区分
        /// </summary>
        /// <remarks>
        /// 00:デフォルト
        /// 01:業務計画表
        /// 02:月次計画表
        /// 03:試験車日程
        /// 04:カーシェア日程
        /// 05:外製車日程
        /// 06:進捗履歴
        /// 07:作業履歴(試験車)
        /// 08:作業履歴(外製車)
        /// 09:作業履歴(カーシェア車)
        /// </remarks>
        [StringLength(2)]
        [Display(Name = "機能区分")]
        public string FUNCTION_CLASS { get; set; } = Const.ClassDefault;

        /// <summary>
        /// 文字種別(0：区別する、1：区別しない 省略時は0と解釈)
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "文字種別")]
        public int DIFF_DATA { get; set; } = 0;

        /// <summary>
        /// 課コード（CAPで使用済の開発符号検索の時に使用）
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }
    }

    /// <summary>
    /// 開発符号検索モデルクラス（出力）
    /// </summary>
    public class GeneralCodeSearchOutModel
    {
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 並び順
        /// </summary>
        public long SORT_NUMBER { get; set; }

        /// <summary>
        /// 開発フラグ(0:開発外 1:開発中)
        /// </summary>
        public string UNDER_DEVELOPMENT { get; set; }

        /// <summary>
        /// 開発符号（ベース）
        /// </summary>
        public string BASE_GENERAL_CODE { get; set; }

        /// <summary>
        /// 権限フラグ(0:閲覧権限なし 1:閲覧権限あり)
        /// </summary>
        public short PERMIT_FLG { get; set; }

    }
}