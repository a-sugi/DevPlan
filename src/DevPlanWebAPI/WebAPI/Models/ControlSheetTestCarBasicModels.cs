using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 試験車基本情報(管理票)検索入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicGetInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)検索出力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicGetOutModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        public string 管理票NO { get; set; }
        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        public int 管理ラベル発行有無 { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        public string 車系 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        public string 車型 { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        public string 型式符号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// リースNO
        /// </summary>
        public string リースNO { get; set; }
        /// <summary>
        /// リース満了日
        /// </summary>
        public DateTime? リース満了日 { get; set; }
        /// <summary>
        /// 研実管理廃却申請受理日
        /// </summary>
        public DateTime? 研実管理廃却申請受理日 { get; set; }
        /// <summary>
        /// 廃却見積日
        /// </summary>
        public DateTime? 廃却見積日 { get; set; }
        /// <summary>
        /// 廃却決済承認年月
        /// </summary>
        public DateTime? 廃却決済承認年月 { get; set; }
        /// <summary>
        /// 車両搬出日
        /// </summary>
        public DateTime? 車両搬出日 { get; set; }
        /// <summary>
        /// 廃却見積額
        /// </summary>
        public int 廃却見積額 { get; set; }
        /// <summary>
        /// 貸与先
        /// </summary>
        public string 貸与先 { get; set; }
        /// <summary>
        /// 貸与返却予定期限
        /// </summary>
        public DateTime? 貸与返却予定期限 { get; set; }
        /// <summary>
        /// 貸与返却日
        /// </summary>
        public DateTime? 貸与返却日 { get; set; }
        /// <summary>
        /// メモ
        /// </summary>
        public string メモ { get; set; }
        /// <summary>
        /// 正式取得日
        /// </summary>
        public DateTime? 正式取得日 { get; set; }
        /// <summary>
        /// 棚卸実施日
        /// </summary>
        public DateTime? 棚卸実施日 { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)更新入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicPutInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }
        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "管理ラベル発行有無")]
        public int 管理ラベル発行有無 { get; set; }
        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }
        /// <summary>
        /// 車型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "車型")]
        public string 車型 { get; set; }
        /// <summary>
        /// 型式符号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "型式符号")]
        public string 型式符号 { get; set; }
        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }
        /// <summary>
        /// リースNO
        /// </summary>
        [StringLength(20)]
        [Display(Name = "リースNO")]
        public string リースNO { get; set; }
        /// <summary>
        /// リース満了日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "リース満了日")]
        public DateTime? リース満了日 { get; set; }
        /// <summary>
        /// 研実管理廃却申請受理日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "研実管理廃却申請受理日")]
        public DateTime? 研実管理廃却申請受理日 { get; set; }
        /// <summary>
        /// 廃却見積日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃却見積日")]
        public DateTime? 廃却見積日 { get; set; }
        /// <summary>
        /// 廃却決済承認年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃却決済承認年月")]
        public DateTime? 廃却決済承認年月 { get; set; }
        /// <summary>
        /// 車両搬出日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車両搬出日")]
        public DateTime? 車両搬出日 { get; set; }
        /// <summary>
        /// 貸与先
        /// </summary>
        [StringLength(50)]
        [Display(Name = "貸与先")]
        public string 貸与先 { get; set; }
        /// <summary>
        /// 貸与返却予定期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "貸与返却予定期限")]
        public DateTime? 貸与返却予定期限 { get; set; }
        /// <summary>
        /// 貸与返却日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "貸与返却日")]
        public DateTime? 貸与返却日 { get; set; }
        /// <summary>
        /// メモ
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "メモ")]
        public string メモ { get; set; }
        /// <summary>
        /// 正式取得日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "正式取得日")]
        public DateTime? 正式取得日 { get; set; }
        /// <summary>
        /// 棚卸実施日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "棚卸実施日")]
        public DateTime? 棚卸実施日 { get; set; }
    }
    /// <summary>
    /// 試験車基本情報(管理票)削除入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarBasicDeleteInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
    }
}