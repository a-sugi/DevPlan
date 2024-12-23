using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevPlanWebAPI.Models
{
    #region 試験車共通検索条件クラス
    /// <summary>
    /// 試験車共通検索条件クラス
    /// </summary>
    public class TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        public int? データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        public int? 履歴NO { get; set; }

        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "管理票NO")]
        public string 管理票NO { get; set; }

        /// <summary>
        /// 固定資産NO
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "固定資産NO")]
        public string 固定資産NO { get; set; }

        /// <summary>
        /// 保険NO
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "保険NO")]
        public string 保険NO { get; set; }

        /// <summary>
        /// 車系
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "車系")]
        public string 車系 { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }

        /// <summary>
        /// 試作時期
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "試作時期")]
        public string 試作時期 { get; set; }

        /// <summary>
        /// 号車
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }

        /// <summary>
        /// 車体番号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }

        /// <summary>
        /// 登録ナンバー
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "登録ナンバー")]
        public string 登録ナンバー { get; set; }

        /// <summary>
        /// 駐車場番号
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "駐車場番号")]
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 仕向地
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>
        /// メーカー名
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "メーカー名")]
        public string メーカー名 { get; set; }

        /// <summary>
        /// 外製車名
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "外製車名")]
        public string 外製車名 { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }

        /// <summary>
        /// 管理票発行有無
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "管理票発行有無")]
        public string 管理票発行有無 { get; set; }

        /// <summary>
        /// 廃却フラグ
        /// </summary>
        [Display(Name = "廃却フラグ")]
        public bool? 廃却フラグ { get; set; }

        /// <summary>
        /// 期間(From)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(From)")]
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// 期間(To)
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "期間(To)")]
        public DateTime? END_DATE { get; set; }

        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "SECTION_ID")]
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "所属")]
        public string ESTABLISHMENT { get; set; }

        /// <summary>
        /// 申請状況
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "申請状況")]
        public string APPLICATION_STATUS { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "種別")]
        public string 種別 { get; set; }

        /// <summary>
        /// XEYE_EXIST
        /// </summary>
        [StringLength(10)]
        [Display(Name = "XEYE_EXIST")]
        public string XEYE_EXIST { get; set; }

        //Append Start 2022/01/11 杉浦 使用履歴設定画面の追加
        /// <summary>
        /// 管理責任部署
        /// </summary>
        [Display(Name = "管理責任部署")]
        public string[] 管理責任部署 { get; set; }
        //Append End 2022/01/11 杉浦 使用履歴設定画面の追加
        #endregion
    }
    #endregion

    #region 試験車共通クラス
    /// <summary>
    /// 試験車共通クラス
    /// </summary>
    public class TestCarCommonModel : TestCarCommonBaseModel
    {
        #region 主キー
        /// <summary>
        /// データID
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        override public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        [Required]
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        override public int 履歴NO { get; set; }
        #endregion
    }

    /// <summary>
    /// 試験車共通（基底）クラス
    /// </summary>
    public class TestCarCommonBaseModel
    {
        #region 主キー
        /// <summary>
        /// データID
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "データID")]
        virtual public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "履歴NO")]
        virtual public int 履歴NO { get; set; }
        #endregion

        #region 試験車基本情報
        /// <summary>
        /// 管理票NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理票NO")]
        virtual public string 管理票NO { get; set; }

        /// <summary>
        /// 管理ラベル発行有無
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "管理ラベル発行有無")]
        public int? 管理ラベル発行有無 { get; set; }

        /// <summary>
        /// 管理ラベル発行
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理ラベル発行")]
        public string 管理ラベル発行 { get; set; }

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
        /// 廃却見積額
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "廃却見積額")]
        public int? 廃却見積額 { get; set; }

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

        /// <summary>
        /// 月例点検省略有無
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "月例点検省略有無")]
        public int? 月例点検省略有無 { get; set; }

        //Append Start 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい
        /// <summary>
        /// 衝突試験済
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "衝突試験済")]
        public int? 衝突試験済 { get; set; }
        //Append End 2021/05/27 杉浦 要望対応_衝突試験済のチェックボックスを追加したい

        //Append Start 2022/03/24 杉浦 月例点検一括省略
        /// <summary>
        /// 月例点検省略有無
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "月例点検省略有無_BASE")]
        public int? 月例点検省略有無_BASE { get; set; }

        /// <summary>
        /// 月例点検省略対象部署
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "月例点検省略対象部署")]
        public int? 月例点検省略対象部署 { get; set; }
        //Append End 2022/03/24 杉浦 月例点検一括省略
        #endregion

        #region 試験車履歴情報
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
        /// 保険料
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "保険料")]
        public int? 保険料 { get; set; }

        /// <summary>
        /// 自動車税
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "自動車税")]
        public int? 自動車税 { get; set; }

        /// <summary>
        /// 移管依頼NO
        /// </summary>
        [StringLength(50)]
        [Display(Name = "移管依頼NO")]
        public string 移管依頼NO { get; set; }

        /// <summary>
        /// 三鷹移管先部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "三鷹移管先部署")]
        public string 三鷹移管先部署 { get; set; }

        /// <summary>
        /// 三鷹移管年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "三鷹移管年月")]
        public DateTime? 三鷹移管年月 { get; set; }

        /// <summary>
        /// 三鷹移管先固資NO
        /// </summary>
        [StringLength(10)]
        [Display(Name = "三鷹移管先固資NO")]
        public string 三鷹移管先固資NO { get; set; }

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
        /// 資産種別
        /// </summary>
        [Display(Name = "資産種別")]
        public short? 資産種別 { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [Display(Name = "種別")]
        public string 種別 { get; set; }

        /// <summary>
        /// 使用期限
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "使用期限")]
        public DateTime? 使用期限 { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "名称")]
        public string 名称 { get; set; }

        /// <summary>
        /// 廃却勧告
        /// </summary>
        [Display(Name = "廃却勧告")]
        public string 廃却勧告 { get; set; }

        //Append Start 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
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
        //Append End 2024/04/24 杉浦 項目追加(自動車ﾘｻｲｸﾙ法とA/C冷媒種類)
        #endregion

        #region 固定資産情報
        /// <summary>
        /// 勘定科目
        /// </summary>
        [StringLength(50)]
        [Display(Name = "勘定科目")]
        public string 勘定科目 { get; set; }

        /// <summary>
        /// 子資産
        /// </summary>
        [StringLength(10)]
        [Display(Name = "子資産")]
        public string 子資産 { get; set; }

        /// <summary>
        /// 所得年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "所得年月")]
        public DateTime? 所得年月 { get; set; }

        /// <summary>
        /// 設置場所
        /// </summary>
        [StringLength(50)]
        [Display(Name = "設置場所")]
        public string 設置場所 { get; set; }

        /// <summary>
        /// 耐用年数
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "耐用年数")]
        public int? 耐用年数 { get; set; }

        /// <summary>
        /// 取得価額
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "取得価額")]
        public int? 取得価額 { get; set; }

        /// <summary>
        /// 減価償却累計額
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "減価償却累計額")]
        public int? 減価償却累計額 { get; set; }

        /// <summary>
        /// 期末簿価
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "期末簿価")]
        public int? 期末簿価 { get; set; }

        /// <summary>
        /// 資産タイプ
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "資産タイプ")]
        public int? 資産タイプ { get; set; }

        /// <summary>
        /// 固定資産税
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "固定資産税")]
        public int? 固定資産税 { get; set; }

        /// <summary>
        /// 原価部門
        /// </summary>
        [StringLength(20)]
        [Display(Name = "原価部門")]
        public string 原価部門 { get; set; }

        /// <summary>
        /// 管理部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "管理部署")]
        public string 管理部署 { get; set; }

        /// <summary>
        /// 資産計上部署
        /// </summary>
        [StringLength(20)]
        [Display(Name = "資産計上部署")]
        public string 資産計上部署 { get; set; }

        /// <summary>
        /// 事業所コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "事業所コード")]
        public string 事業所コード { get; set; }

        /// <summary>
        /// 処分コード
        /// </summary>
        [StringLength(10)]
        [Display(Name = "処分コード")]
        public string 処分コード { get; set; }

        /// <summary>
        /// 処分予定年月
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "処分予定年月")]
        public DateTime? 処分予定年月 { get; set; }

        /// <summary>
        /// 処分数
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "処分数")]
        public int? 処分数 { get; set; }

        /// <summary>
        /// 処分区分
        /// </summary>
        [StringLength(50)]
        [Display(Name = "処分区分")]
        public string 処分区分 { get; set; }

        /// <summary>
        /// 除却年度
        /// </summary>
        [StringLength(10)]
        [Display(Name = "除却年度")]
        public string 除却年度 { get; set; }

        /// <summary>
        /// 除却明細名称
        /// </summary>
        [StringLength(50)]
        [Display(Name = "除却明細名称")]
        public string 除却明細名称 { get; set; }
        #endregion

        #region 開本内移管履歴情報
        /// <summary>
        /// 開本内移管履歴NO
        /// </summary>
        [Range(0, 99999999)]
        [Display(Name = "開本内移管履歴NO")]
        public int? 開本内移管履歴NO { get; set; }

        /// <summary>
        /// 開本内移管日
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "開本内移管日")]
        public DateTime? 開本内移管日 { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(255)]
        [Display(Name = "内容")]
        public string 内容 { get; set; }
        #endregion

        #region 開発符号情報
        /// <summary>
        /// 車名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車名")]
        public string 車名 { get; set; }
        #endregion

        #region 車系情報
        /// <summary>
        /// 車区分
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車区分")]
        public string 車区分 { get; set; }

        /// <summary>
        /// 車検区分1
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車検区分1")]
        public string 車検区分1 { get; set; }

        /// <summary>
        /// 車検区分2
        /// </summary>
        [StringLength(50)]
        [Display(Name = "車検区分2")]
        public string 車検区分2 { get; set; }
        #endregion

        #region 部
        /// <summary>
        /// 部ID
        /// </summary>
        [StringLength(10)]
        [Display(Name = "DEPARTMENT_ID")]
        public string DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 部コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "部コード")]
        public string DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 管理所在地
        /// </summary>
        [StringLength(10)]
        [Display(Name = "管理所在地")]
        public string ESTABLISHMENT { get; set; }
        #endregion

        #region 課
        /// <summary>
        /// 課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_ID")]
        public string SECTION_ID { get; set; }

        /// <summary>
        /// 課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "課コード")]
        public string SECTION_CODE { get; set; }
        #endregion

        #region 担当
        /// <summary>
        /// 担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 担当コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "担当コード")]
        public string SECTION_GROUP_CODE { get; set; }
        #endregion

        #region 受領
        /// <summary>
        /// 受領部署_部ID
        /// </summary>
        [StringLength(40)]
        [Display(Name = "受領部署_部ID")]
        public string 受領部署_DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 受領部署_部コード
        /// </summary>
        [StringLength(40)]
        [Display(Name = "受領部署_部コード")]
        public string 受領部署_DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 受領部署_課ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署_課ID")]
        public string 受領部署_SECTION_ID { get; set; }

        /// <summary>
        /// 受領部署_課コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署_課コード")]
        public string 受領部署_SECTION_CODE { get; set; }

        /// <summary>
        /// 受領部署_担当ID
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署_担当ID")]
        public string 受領部署_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 受領部署_担当コード
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領部署_担当コード")]
        public string 受領部署_SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 受領者_NAME
        /// </summary>
        [StringLength(20)]
        [Display(Name = "受領者_NAME")]
        public string 受領者_NAME { get; set; }
        #endregion

        #region GPS搭載区分
        /// <summary>
        /// XEYE_EXIST
        /// </summary>
        [StringLength(10)]
        [Display(Name = "XEYE_EXIST")]
        public string XEYE_EXIST { get; set; }
        #endregion
    }
    #endregion

    #region 共通マスタ
    /// <summary>
    /// 共通マスタ
    /// </summary>
    public class CommonMasterModel
    {
        #region プロパティ
        /// <summary>
        /// コード
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        #endregion
    }
    #endregion
}