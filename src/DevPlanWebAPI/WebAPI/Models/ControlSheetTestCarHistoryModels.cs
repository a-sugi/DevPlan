using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    /// <summary>
    /// 試験車履歴情報(管理票)検索入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarHistoryGetInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        public int 履歴NO { get; set; }
    }
    /// <summary>
    /// 試験車履歴情報(管理票)検索出力モデルクラス
    /// </summary>
    public class ControlSheetTestCarHistoryGetOutModel
    {
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        public int 履歴NO { get; set; }
        /// <summary>
        /// 管理票発行有無
        /// </summary>
        public string 管理票発行有無 { get; set; }
        /// <summary>
        /// 発行年月日
        /// </summary>
        public DateTime? 発行年月日 { get; set; }
        /// <summary>
        /// 開発符号
        /// </summary>
        public string 開発符号 { get; set; }
        /// <summary>
        /// 試作時期
        /// </summary>
        public string 試作時期 { get; set; }
        /// <summary>
        /// 号車
        /// </summary>
        public string 号車 { get; set; }
        /// <summary>
        /// 仕向地
        /// </summary>
        public string 仕向地 { get; set; }
        /// <summary>
        /// メーカー名
        /// </summary>
        public string メーカー名 { get; set; }
        /// <summary>
        /// 外製車名
        /// </summary>
        public string 外製車名 { get; set; }
        /// <summary>
        /// 名称備考
        /// </summary>
        public string 名称備考 { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        public string 車体番号 { get; set; }
        /// <summary>
        /// E_G番号
        /// </summary>
        public string E_G番号 { get; set; }
        /// <summary>
        /// E_G型式
        /// </summary>
        public string E_G型式 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        public string 排気量 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        public string トランスミッション { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        public string 駆動方式 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        public string グレード { get; set; }
        /// <summary>
        /// 車体色
        /// </summary>
        public string 車体色 { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        public string 試験目的 { get; set; }
        /// <summary>
        /// 受領日
        /// </summary>
        public DateTime? 受領日 { get; set; }
        /// <summary>
        /// 受領部署
        /// </summary>
        public string 受領部署 { get; set; }
        /// <summary>
        /// 受領部署名
        /// </summary>
        public string 受領部署名 { get; set; }
        /// <summary>
        /// 受領部署コード
        /// </summary>
        public string 受領部署コード { get; set; }
        /// <summary>
        /// 受領者
        /// </summary>
        public string 受領者 { get; set; }
        /// <summary>
        /// 受領者名
        /// </summary>
        public string 受領者名 { get; set; }
        /// <summary>
        /// 受領時走行距離
        /// </summary>
        public string 受領時走行距離 { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        public DateTime? 完成日 { get; set; }
        /// <summary>
        /// 管理責任部署
        /// </summary>
        public string 管理責任部署 { get; set; }
        /// <summary>
        /// 管理責任部署名
        /// </summary>
        public string 管理責任部署名 { get; set; }
        /// <summary>
        /// 管理責任部署コード
        /// </summary>
        public string 管理責任部署コード { get; set; }
        /// <summary>
        /// 研命ナンバー
        /// </summary>
        public string 研命ナンバー { get; set; }
        /// <summary>
        /// 研命期間
        /// </summary>
        public DateTime? 研命期間 { get; set; }
        /// <summary>
        /// 固定資産NO
        /// </summary>
        public string 固定資産NO { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// 車検登録日
        /// </summary>
        public DateTime? 車検登録日 { get; set; }
        /// <summary>
        /// 車検期限
        /// </summary>
        public DateTime? 車検期限 { get; set; }
        /// <summary>
        /// 廃艦日
        /// </summary>
        public DateTime? 廃艦日 { get; set; }
        /// <summary>
        /// 保険NO
        /// </summary>
        public string 保険NO { get; set; }
        /// <summary>
        /// 保険加入日
        /// </summary>
        public DateTime? 保険加入日 { get; set; }
        /// <summary>
        /// 保険解約日
        /// </summary>
        public DateTime? 保険解約日 { get; set; }
        /// <summary>
        /// 保険料
        /// </summary>
        public int? 保険料 { get; set; }
        /// <summary>
        /// 自動車税
        /// </summary>
        public int? 自動車税 { get; set; }
        /// <summary>
        /// 移管依頼NO
        /// </summary>
        public string 移管依頼NO { get; set; }
        /// <summary>
        /// 三鷹移管先部署
        /// </summary>
        public string 三鷹移管先部署 { get; set; }
        /// <summary>
        /// 三鷹移管年月
        /// </summary>
        public DateTime? 三鷹移管年月 { get; set; }
        /// <summary>
        /// 三鷹移管先固資NO
        /// </summary>
        public string 三鷹移管先固資NO { get; set; }
        /// <summary>
        /// 試験着手日
        /// </summary>
        public DateTime? 試験着手日 { get; set; }
        /// <summary>
        /// 試験着手証明文書
        /// </summary>
        public string 試験着手証明文書 { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string 工事区分NO { get; set; }
        /// <summary>
        /// FLAG_中古
        /// </summary>
        public short? FLAG_中古 { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        public short? FLAG_ETC付 { get; set; }
        /// <summary>
        /// EVデバイス
        /// </summary>
        public string EVデバイス { get; set; }
        /// <summary>
        /// 初年度登録年月
        /// </summary>
        public DateTime? 初年度登録年月 { get; set; }
        /// <summary>
        /// 自動車ﾘｻｲｸﾙ法
        /// </summary>
        public string 自動車ﾘｻｲｸﾙ法 { get; set; }

        /// <summary>
        /// A_C冷媒種類
        /// </summary>
        public string A_C冷媒種類 { get; set; }
    }
    /// <summary>
    /// 試験車履歴情報(管理票)登録入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarHistoryPostInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 管理票発行有無
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票発行有無")]
        public string 管理票発行有無 { get; set; }
        /// <summary>
        /// 発行年月日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "発行年月日")]
        public DateTime? 発行年月日 { get; set; }
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
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }
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
        /// 名称備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "名称備考")]
        public string 名称備考 { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }
        /// <summary>
        /// E_G番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "E_G番号")]
        public string E_G番号 { get; set; }
        /// <summary>
        /// E_G型式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "E_G型式")]
        public string E_G型式 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }
        /// <summary>
        /// 車体色
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(255)]
        [Display(Name = "試験目的")]
        public string 試験目的 { get; set; }
        /// <summary>
        /// 受領日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "受領日")]
        public DateTime? 受領日 { get; set; }
        /// <summary>
        /// 受領部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署")]
        public string 受領部署 { get; set; }
        /// <summary>
        /// 受領者
        /// </summary>
        [StringLength(50)]
        [Display(Name = "受領者")]
        public string 受領者 { get; set; }
        /// <summary>
        /// 受領時走行距離
        /// </summary>
        [StringLength(50)]
        [Display(Name = "受領時走行距離")]
        public string 受領時走行距離 { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "完成日")]
        public DateTime? 完成日 { get; set; }
        /// <summary>
        /// 管理責任部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "管理責任部署")]
        public string 管理責任部署 { get; set; }
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
        /// 固定資産NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// 車検登録日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検登録日")]
        public DateTime? 車検登録日 { get; set; }
        /// <summary>
        /// 車検期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検期限")]
        public DateTime? 車検期限 { get; set; }
        /// <summary>
        /// 廃艦日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃艦日")]
        public DateTime? 廃艦日 { get; set; }
        /// <summary>
        /// 保険NO
        /// </summary>
        [StringLength(50)]
        [Display(Name = "保険NO")]
        public string 保険NO { get; set; }
        /// <summary>
        /// 保険加入日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "保険加入日")]
        public DateTime? 保険加入日 { get; set; }
        /// <summary>
        /// 保険解約日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "保険解約日")]
        public DateTime? 保険解約日 { get; set; }
        /// <summary>
        /// 移管依頼NO
        /// </summary>
        [StringLength(50)]
        [Display(Name = "移管依頼NO")]
        public string 移管依頼NO { get; set; }
        /// <summary>
        /// 試験着手日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "試験着手日")]
        public DateTime? 試験着手日 { get; set; }
        /// <summary>
        /// 試験着手証明文書
        /// </summary>
        [StringLength(50)]
        [Display(Name = "試験着手証明文書")]
        public string 試験着手証明文書 { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "工事区分NO")]
        public string 工事区分NO { get; set; }
        /// <summary>
        /// FLAG_中古
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_中古")]
        public short? FLAG_中古 { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }
        /// <summary>
        /// EVデバイス
        /// </summary>
        [StringLength(50)]
        [Display(Name = "EVデバイス")]
        public string EVデバイス { get; set; }
        /// <summary>
        /// 初年度登録年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "初年度登録年月")]
        public DateTime? 初年度登録年月 { get; set; }
        /// <summary>
        /// 自動車ﾘｻｲｸﾙ法
        /// </summary>
        [Display(Name = "自動車ﾘｻｲｸﾙ法")]
        public string 自動車ﾘｻｲｸﾙ法 { get; set; }

        /// <summary>
        /// A_C冷媒種類
        /// </summary>
        [Display(Name = "A_C冷媒種類")]
        public string A_C冷媒種類 { get; set; }
    }
    /// <summary>
    /// 試験車履歴情報(管理票)更新入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarHistoryPutInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        public int 履歴NO { get; set; }
        /// <summary>
        /// 管理票発行有無
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票発行有無")]
        public string 管理票発行有無 { get; set; }
        /// <summary>
        /// 発行年月日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "発行年月日")]
        public DateTime? 発行年月日 { get; set; }
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
        /// 仕向地
        /// </summary>
        [StringLength(50)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }
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
        /// 名称備考
        /// </summary>
        [StringLength(255)]
        [Display(Name = "名称備考")]
        public string 名称備考 { get; set; }
        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }
        /// <summary>
        /// E_G番号
        /// </summary>
        [StringLength(50)]
        [Display(Name = "E_G番号")]
        public string E_G番号 { get; set; }
        /// <summary>
        /// E_G型式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "E_G型式")]
        public string E_G型式 { get; set; }
        /// <summary>
        /// 排気量
        /// </summary>
        [StringLength(50)]
        [Display(Name = "排気量")]
        public string 排気量 { get; set; }
        /// <summary>
        /// トランスミッション
        /// </summary>
        [StringLength(50)]
        [Display(Name = "トランスミッション")]
        public string トランスミッション { get; set; }
        /// <summary>
        /// 駆動方式
        /// </summary>
        [StringLength(50)]
        [Display(Name = "駆動方式")]
        public string 駆動方式 { get; set; }
        /// <summary>
        /// グレード
        /// </summary>
        [StringLength(50)]
        [Display(Name = "グレード")]
        public string グレード { get; set; }
        /// <summary>
        /// 車体色
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車体色")]
        public string 車体色 { get; set; }
        /// <summary>
        /// 試験目的
        /// </summary>
        [StringLength(255)]
        [Display(Name = "試験目的")]
        public string 試験目的 { get; set; }
        /// <summary>
        /// 受領日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "受領日")]
        public DateTime? 受領日 { get; set; }
        /// <summary>
        /// 受領部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署")]
        public string 受領部署 { get; set; }
        /// <summary>
        /// 受領者
        /// </summary>
        [StringLength(50)]
        [Display(Name = "受領者")]
        public string 受領者 { get; set; }
        /// <summary>
        /// 受領時走行距離
        /// </summary>
        [StringLength(50)]
        [Display(Name = "受領時走行距離")]        
        public string 受領時走行距離 { get; set; }
        /// <summary>
        /// 完成日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "完成日")]
        public DateTime? 完成日 { get; set; }
        /// <summary>
        /// 管理責任部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "管理責任部署")]
        public string 管理責任部署 { get; set; }
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
        /// 固定資産NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }
        /// <summary>
        /// 登録ナンバー
        /// </summary>
        [StringLength(50)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }
        /// <summary>
        /// 車検登録日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検登録日")]
        public DateTime? 車検登録日 { get; set; }
        /// <summary>
        /// 車検期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "車検期限")]
        public DateTime? 車検期限 { get; set; }
        /// <summary>
        /// 廃艦日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "廃艦日")]
        public DateTime? 廃艦日 { get; set; }
        /// <summary>
        /// 保険NO
        /// </summary>
        [StringLength(50)]
        [Display(Name = "保険NO")]
        public string 保険NO { get; set; }
        /// <summary>
        /// 保険加入日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "保険加入日")]
        public DateTime? 保険加入日 { get; set; }
        /// <summary>
        /// 保険解約日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "保険解約日")]
        public DateTime? 保険解約日 { get; set; }
        /// <summary>
        /// 移管依頼NO
        /// </summary>
        [StringLength(50)]
        [Display(Name = "移管依頼NO")]
        public string 移管依頼NO { get; set; }
        /// <summary>
        /// 試験着手日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "試験着手日")]
        public DateTime? 試験着手日 { get; set; }
        /// <summary>
        /// 試験着手証明文書
        /// </summary>
        [StringLength(50)]
        [Display(Name = "試験着手証明文書")]
        public string 試験着手証明文書 { get; set; }
        /// <summary>
        /// 工事区分NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "工事区分NO")]
        public string 工事区分NO { get; set; }
        /// <summary>
        /// FLAG_中古
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_中古")]
        public short? FLAG_中古 { get; set; }
        /// <summary>
        /// FLAG_ナビ付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ナビ付")]
        public short? FLAG_ナビ付 { get; set; }
        /// <summary>
        /// FLAG_ETC付
        /// </summary>
        [Range(0, 9)]
        [Display(Name = "FLAG_ETC付")]
        public short? FLAG_ETC付 { get; set; }
        /// <summary>
        /// EVデバイス
        /// </summary>
        [StringLength(50)]
        [Display(Name = "EVデバイス")]
        public string EVデバイス { get; set; }
        /// <summary>
        /// 初年度登録年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "初年度登録年月")]
        public DateTime? 初年度登録年月 { get; set; }
        /// <summary>
        /// 自動車ﾘｻｲｸﾙ法
        /// </summary>
        [Display(Name = "自動車ﾘｻｲｸﾙ法")]
        public string 自動車ﾘｻｲｸﾙ法 { get; set; }

        /// <summary>
        /// A_C冷媒種類
        /// </summary>
        [Display(Name = "A_C冷媒種類")]
        public string A_C冷媒種類 { get; set; }
    }
    /// <summary>
    /// 試験車履歴情報(管理票)削除入力モデルクラス
    /// </summary>
    public class ControlSheetTestCarHistoryDeleteInModel
    {
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int データID { get; set; }
        /// <summary>
        /// 履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        public int 履歴NO { get; set; }
    }
}