using DevPlanWebAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 車系検索入力モデルクラス
    /// <summary>
    /// 車系検索入力モデルクラス
    /// </summary>
    public class CarGroupSearchInModel
    {
        /// <summary>
        /// 開発フラグ
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "開発フラグ")]
        public int? UNDER_DEVELOPMENT { get; set; }
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }

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
    }
    #endregion

    #region 車系検索出力モデルクラス
    /// <summary>
    /// 車系検索出力モデルクラス
    /// </summary>
    public class CarGroupSearchOutModel
    {
        /// <summary>
        /// 車系
        /// </summary>
        public string CAR_GROUP { get; set; }
        /// <summary>
        /// 並び順
        /// </summary>
        public int SORT_NUMBER { get; set; }
        /// <summary>
        /// 権限フラグ(0:閲覧権限なし, 1:閲覧権限あり)
        /// </summary>
        public int PERMIT_FLG { get; set; }
    }
    #endregion
}