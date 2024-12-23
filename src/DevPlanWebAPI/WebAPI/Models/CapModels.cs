using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using DevPlanWebAPI.Common;

namespace DevPlanWebAPI.Models
{
    #region CAP課題検索条件クラス
    /// <summary>
    /// CAP課題検索条件クラス
    /// </summary>
    [Serializable]
    public class CapSearchModel
    {
        /// <summary>項目_ID</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "項目_ID")]
        public long? 項目_ID { get; set; }

        /// <summary>対応_ID</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "対応_ID")]
        public long? 対応_ID { get; set; }

        /// <summary>車種</summary>
        //[StringLength(15)]
        [Display(Name = "車種")]
        public string[] GENERAL_CODE { get; set; }

        /// <summary>回答期限FROM</summary>
        [DataType(DataType.Date)]
        [Display(Name = "回答期限FROM")]
        public DateTime? 回答期限FROM { get; set; }

        /// <summary>回答期限TO</summary>
        [DataType(DataType.Date)]
        [Display(Name = "回答期限TO")]
        public DateTime? 回答期限TO { get; set; }

        /// <summary>専門部署</summary>
        //[StringLength(10)]
        [Display(Name = "専門部署")]
        public string[] 専門部署名 { get; set; }

        /// <summary>重要度</summary>
        [StringLength(10)]
        [Display(Name = "重要度")]
        public string 重要度 { get; set; }

        /// <summary>ステータス</summary>
        [Range(0, 1)]
        [Display(Name = "ステータス")]
        public short? FLAG_CLOSE { get; set; }

        /// <summary>部署承認</summary>
        [Range(0, 1)]
        [Display(Name = "部署承認")]
        public short? FLAG_上司承認 { get; set; }

        /// <summary>CAP確認</summary>
        [Range(0, 1)]
        [Display(Name = "CAP確認")]
        public short? FLAG_CAP確認 { get; set; }

        /// <summary>種別</summary>
        [StringLength(10)]
        [Display(Name = "種別")]
        public string CAP種別 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(10)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>完了日程</summary>
        [Range(0, 1)]
        [Display(Name = "完了日程")]
        public short? 完了日程 { get; set; }

        /// <summary>出図日程</summary>
        [Range(0, 1)]
        [Display(Name = "出図日程")]
        public short? 出図日程 { get; set; }


        //Append Start 2021/06/15 矢作
        /// <summary>方向付け確定期限</summary>
        [Range(0, 1)]
        [Display(Name = "方向付け確定期限")]
        public short? 方向付け確定期限 { get; set; }
        //Append End 2021/06/15 矢作


        /// <summary>対策予定</summary>
        [StringLength(30)]
        [Display(Name = "対策予定")]
        public string 対策予定 { get; set; }

        /// <summary>指摘分類</summary>
        [StringLength(30)]
        [Display(Name = "指摘分類")]
        public string 指摘分類 { get; set; }

        /// <summary>供試品</summary>
        [StringLength(30)]
        [Display(Name = "供試品")]
        public string 供試品 { get; set; }

        /// <summary>織込時期</summary>
        [StringLength(30)]
        [Display(Name = "織込時期")]
        public string 織込時期 { get; set; }

        /// <summary>FLAG_最新</summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_最新")]
        public short? FLAG_最新 { get; set; }

        /// <summary>ID</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "ID")]
        public long? ID { get; set; }

        /// <summary>
        /// 社員コード（パーソナルID）
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "ユーザーID")]
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 関連課フラグ
        /// </summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_関連課")]
        public short? FLAG_関連課 { get; set; }
    }
    #endregion

    #region CAP課題クラス
    /// <summary>
    /// CAP課題クラス
    /// </summary>
    [Serializable]
    public class CapModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Range(long.MinValue, long.MaxValue)]
        [Display(Name = "ID")]
        public long ID { get; set; }

        /// <summary>CAP確認</summary>
        [Range(0, 1)]
        [Display(Name = "CAP確認")]
        public short? FLAG_CAP確認 { get; set; }

        /// <summary>回答期限</summary>
        [DataType(DataType.Date)]
        [Display(Name = "回答期限")]
        public DateTime? 回答期限 { get; set; }

        /// <summary>承認</summary>
        [Range(0, 1)]
        [Display(Name = "承認")]
        public short? FLAG_上司承認 { get; set; }

        /// <summary>専門部名</summary>
        [StringLength(40)]
        [Display(Name = "専門部名")]
        public string 専門部名 { get; set; }

        /// <summary>専門部署名</summary>
        [StringLength(10)]
        [Display(Name = "専門部署名")]
        public string 専門部署名 { get; set; }

        /// <summary>対策予定</summary>
        [StringLength(30)]
        [Display(Name = "対策予定")]
        public string 対策予定 { get; set; }

        /// <summary>対策案</summary>
        [StringLength(2000)]
        [Display(Name = "対策案")]
        public string 対応策 { get; set; }

        /// <summary>対策案編集日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "対策案編集日")]
        public DateTime? 対策案編集日 { get; set; }

        /// <summary>対策案編集者_ID</summary>
        [StringLength(10)]
        [Display(Name = "対策案編集者_ID")]
        public string 対策案編集者_ID { get; set; }

        /// <summary>対策案編集者</summary>
        [StringLength(20)]
        [Display(Name = "対策案編集者")]
        public string 対策案編集者 { get; set; }

        /// <summary>事前把握</summary>
        [StringLength(10)]
        [Display(Name = "事前把握")]
        public string 事前把握有無 { get; set; }

        /// <summary>分類</summary>
        [Range(0, short.MaxValue)]
        [Display(Name = "分類")]
        public short? 分類 { get; set; }

        /// <summary>分類意味</summary>
        [StringLength(50)]
        [Display(Name = "分類意味")]
        public string 分類意味 { get; set; }

        /// <summary>評価レベル</summary>
        [StringLength(10)]
        [Display(Name = "評価レベル")]
        public string 評価レベル { get; set; }

        /// <summary>評価意味</summary>
        [StringLength(50)]
        [Display(Name = "評価意味")]
        public string 評価意味 { get; set; }

        /// <summary>評価項目</summary>
        [StringLength(50)]
        [Display(Name = "評価項目")]
        public string 評価項目 { get; set; }

        /// <summary>レベル基準</summary>
        [StringLength(50)]
        [Display(Name = "レベル基準")]
        public string レベル基準 { get; set; }

        /// <summary>判断イメージ</summary>
        [StringLength(50)]
        [Display(Name = "判断イメージ")]
        public string 判断イメージ { get; set; }

        /// <summary>完了日程</summary>
        [DataType(DataType.Date)]
        [Display(Name = "完了日程")]
        public DateTime? 完了日程 { get; set; }

        /// <summary>供試品</summary>
        [StringLength(50)]
        [Display(Name = "供試品")]
        public string 供試品 { get; set; }

        /// <summary>出図日程</summary>
        [DataType(DataType.Date)]
        [Display(Name = "出図日程")]
        public DateTime? 出図日程 { get; set; }

        /// <summary>織込時期</summary>
        [StringLength(30)]
        [Display(Name = "織込時期")]
        public string 織込時期 { get; set; }

        /// <summary>編集日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "編集日")]
        public DateTime? 編集日 { get; set; }

        /// <summary>編集者_ID</summary>
        [StringLength(10)]
        [Display(Name = "編集者_ID")]
        public string 編集者_ID { get; set; }

        /// <summary>編集者</summary>
        [StringLength(20)]
        [Display(Name = "編集者")]
        public string 編集者 { get; set; }

        //Append Start 2021/04/19 杉浦 背景色を戻す処理の追加
        /// <summary>BK_編集日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "BK_編集日")]
        public DateTime? BK_編集日 { get; set; }

        /// <summary>BK_編集者_ID</summary>
        [StringLength(10)]
        [Display(Name = "BK_編集者_ID")]
        public string BK_編集者_ID { get; set; }

        /// <summary>BK_編集者</summary>
        [StringLength(20)]
        [Display(Name = "BK_編集者")]
        public string BK_編集者 { get; set; }
        //Append End 2021/04/19 杉浦 背景色を戻す処理の追加

        /// <summary>回答期限設定日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "回答期限設定日")]
        public DateTime? 回答期限設定日 { get; set; }

        /// <summary>承認日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "承認日")]
        public DateTime? 承認日 { get; set; }

        /// <summary>承認者_ID</summary>
        [StringLength(10)]
        [Display(Name = "承認者_ID")]
        public string 承認者_ID { get; set; }

        /// <summary>承認者</summary>
        [StringLength(20)]
        [Display(Name = "承認者")]
        public string 承認者 { get; set; }

        /// <summary>FLAG_最新</summary>
        [Range(0, 1)]
        [Display(Name = "FLAG_最新")]
        public short? FLAG_最新 { get; set; }

        /// <summary>作成日</summary>
        [DataType(DataType.Date)]
        [Display(Name = "履歴作成日時")]
        public DateTime? 作成日 { get; set; }

        /// <summary>項目_ID</summary>
        [Required]
        [Range(0, long.MaxValue)]
        [Display(Name = "項目_ID")]
        public long 項目_ID { get; set; }

        /// <summary>ステータス</summary>
        [Range(0, 1)]
        [Display(Name = "ステータス")]
        public short? FLAG_CLOSE { get; set; }

        /// <summary>No.</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "No.")]
        public long NO { get; set; }

        /// <summary>種別</summary>
        [StringLength(10)]
        [Display(Name = "種別")]
        public string CAP種別 { get; set; }

        /// <summary>重要度</summary>
        [StringLength(10)]
        [Display(Name = "重要度")]
        public string 重要度 { get; set; }

        /// <summary>重要度説明</summary>
        [StringLength(100)]
        [Display(Name = "重要度説明")]
        public string 説明 { get; set; }

        /// <summary>項目</summary>
        [StringLength(300)]
        [Display(Name = "項目")]
        public string 項目 { get; set; }

        /// <summary>詳細</summary>
        [StringLength(1000)]
        [Display(Name = "詳細")]
        public string 詳細 { get; set; }

        /// <summary>評価車両</summary>
        [StringLength(20)]
        [Display(Name = "評価車両")]
        public string 評価車両 { get; set; }

        /// <summary>仕向地</summary>
        [StringLength(20)]
        [Display(Name = "仕向地")]
        public string 仕向地 { get; set; }

        /// <summary>CAP確認結果</summary>
        [StringLength(2000)]
        [Display(Name = "CAP確認結果")]
        public string CAP確認結果 { get; set; }

        /// <summary>フォロー状況</summary>
        [StringLength(50)]
        [Display(Name = "フォロー状況")]
        public string フォロー状況 { get; set; }

        /// <summary>CAP確認時期</summary>
        [StringLength(30)]
        [Display(Name = "CAP確認時期")]
        public string 織込時期_項目 { get; set; }

        //Append Start 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加
        /// <summary>N_DRIVE_LINK</summary>
        [Display(Name = "N_DRIVE_LINK")]
        public string N_DRIVE_LINK { get; set; }
        //Append End 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加

        /// <summary>指摘分類</summary>
        [StringLength(30)]
        [Display(Name = "指摘分類")]
        public string 指摘分類 { get; set; }

        /// <summary>EG型式</summary>
        [StringLength(50)]
        [Display(Name = "EG型式")]
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

        /// <summary>車体番号</summary>
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }

        /// <summary>過去履歴件数</summary>
        [Range(0, long.MaxValue)]
        [Display(Name = "過去履歴件数")]
        public long 過去履歴件数 { get; set; }

        /// <summary>親対応_ID</summary>
        [Range(0, int.MaxValue)]
        [Display(Name = "親対応_ID")]
        public int? 親対応_ID { get; set; }

        /// <summary>修正カラム</summary>
        [StringLength(200)]
        [Display(Name = "修正カラム")]
        public string 修正カラム { get; set; }

        /// <summary>
        /// 開発符号
        /// </summary>
        [StringLength(15)]
        [Display(Name = "GENERAL_CODE")]
        public string GENERAL_CODE { get; set; }

        /// <summary>
        /// 権限フラグ(0:閲覧権限なし 1:閲覧権限あり)
        /// </summary>
        public short PERMIT_FLG { get; set; }
        #endregion

        //Append Start 2021/06/15 矢作
        /// <summary>方向付け確定期限</summary>
        [DataType(DataType.Date)]
        [Display(Name = "方向付け確定期限")]
        public DateTime? 方向付け確定期限 { get; set; }
        //Append End 2021/06/15 矢作
    }
    #endregion

    #region CAP重要度クラス
    /// <summary>
    /// CAP重要度クラス
    /// </summary>
    [Serializable]
    public class CapImportanceModel
    {
        #region プロパティ
        /// <summary>重要度</summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "重要度")]
        public string 重要度 { get; set; }

        /// <summary>説明</summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "説明")]
        public string 説明 { get; set; }
        #endregion
    }
    #endregion

    #region CAP種別クラス
    /// <summary>
    /// CAP種別クラス
    /// </summary>
    [Serializable]
    public class CapKindModel
    {
        #region プロパティ
        /// <summary>種別</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "種別")]
        public string 種別 { get; set; }
        #endregion
    }
    #endregion

    #region CAP供試品クラス
    /// <summary>
    /// CAP供試品クラス
    /// </summary>
    [Serializable]
    public class CapSampleModel
    {
        #region プロパティ
        /// <summary>供試品</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "供試品")]
        public string 供試品 { get; set; }
        #endregion
    }
    #endregion

    #region CAP織込時期クラス
    /// <summary>
    /// CAP織込時期クラス
    /// </summary>
    [Serializable]
    public class CapStageModel
    {
        #region プロパティ
        /// <summary>ID</summary>
        [Range(0, 99999)]
        [Display(Name = "ID")]
        public int? ID { get; set; }
        /// <summary>織込時期</summary>
        [StringLength(30)]
        [Display(Name = "織込時期")]
        public string 織込時期 { get; set; }
        /// <summary>SORT_NO</summary>
        [Range(0, 99999)]
        [Display(Name = "SORT_NO")]
        public int? SORT_NO { get; set; }
        /// <summary>FLAG_DISP</summary>
        [Range(0, 99)]
        [Display(Name = "FLAG_DISP")]
        public int? FLAG_DISP { get; set; }
        #endregion
    }
    #endregion

    #region CAP指摘分類クラス
    /// <summary>
    /// CAP指摘分類クラス
    /// </summary>
    [Serializable]
    public class CapIdentificationModel
    {
        #region プロパティ
        /// <summary>指摘分類</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "指摘分類")]
        public string 指摘分類 { get; set; }
        #endregion
    }
    #endregion

    #region CAPフォロー状況クラス
    /// <summary>
    /// CAPフォロー状況クラス
    /// </summary>
    [Serializable]
    public class CapFollowModel
    {
        #region プロパティ
        /// <summary>フォロー状況</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "フォロー状況")]
        public string フォロー状況 { get; set; }
        #endregion
    }
    #endregion

    #region CAP仕向地クラス
    /// <summary>
    /// CAP仕向地クラス
    /// </summary>
    [Serializable]
    public class CapLocationModel
    {
        #region プロパティ
        /// <summary>仕向</summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "仕向")]
        public string 仕向 { get; set; }
        #endregion
    }
    #endregion

    #region CAP対策予定クラス
    /// <summary>
    /// CAP対策予定クラス
    /// </summary>
    [Serializable]
    public class CapScheduleModel
    {
        #region プロパティ
        /// <summary>対策予定</summary>
        [Required]
        [StringLength(30)]
        [Display(Name = "対策予定")]
        public string 対策予定 { get; set; }
        #endregion
    }
    #endregion

    #region 資料分類コードクラス
    /// <summary>
    /// 資料分類コードクラス
    /// </summary>
    [Serializable]
    public class DocumentCodeModel
    {
        #region プロパティ
        /// <summary>分類コード</summary>
        [Required]
        [Range(0, short.MaxValue)]
        [Display(Name = "分類コード")]
        public short 分類コード { get; set; }

        /// <summary>意味</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "意味")]
        public string 意味 { get; set; }
        #endregion
    }
    #endregion

    #region 資料評価レベルクラス
    /// <summary>
    /// 資料評価レベルクラス
    /// </summary>
    [Serializable]
    public class DocumentLevelModel
    {
        #region プロパティ
        /// <summary>評価レベル</summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "評価レベル")]
        public string 評価レベル { get; set; }

        /// <summary>項目</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "項目")]
        public string 項目 { get; set; }

        /// <summary>レベル基準</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "レベル基準")]
        public string レベル基準 { get; set; }

        /// <summary>判断イメージ</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "判断イメージ")]
        public string 判断イメージ { get; set; }
        #endregion
    }
    #endregion

    #region CAP評価車両詳細検索条件クラス
    /// <summary>
    /// CAP評価車両詳細検索条件クラス
    /// </summary>
    [Serializable]
    public class CapDetailSearchModel
    {
        #region プロパティ
        /// <summary>開発符号</summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "開発符号")]
        public string 開発符号 { get; set; }

        /// <summary>号車</summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "号車")]
        public string 号車 { get; set; }
        #endregion
    }
    #endregion

    #region CAP評価車両詳細クラス
    /// <summary>
    /// CAP評価車両詳細クラス
    /// </summary>
    [Serializable]
    public class CapDetailModel
    {
        #region プロパティ
        /// <summary>EG型式</summary>
        [StringLength(50)]
        [Display(Name = "EG型式")]
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

        /// <summary>車体番号</summary>
        [StringLength(30)]
        [Display(Name = "車体番号")]
        public string 車体番号 { get; set; }
        #endregion
    }
    #endregion

    #region CAP部署クラス
    /// <summary>
    /// CAP部署クラス
    /// </summary>
    [Serializable]
    public class CapSectionModel
    {
        #region プロパティ
        /// <summary>SECTION_ID</summary>
        [StringLength(20)]
        [Display(Name = "SECTION_ID")]
        public string SECTION_ID { get; set; }

        /// <summary>SECTION_GROUP_ID</summary>
        [StringLength(20)]
        [Display(Name = "SECTION_GROUP_ID")]
        public string SECTION_GROUP_ID { get; set; }
        #endregion
    }
    #endregion

    #region SQB部署クラス
    /// <summary>
    /// SQB部署クラス
    /// </summary>
    [Serializable]
    public class SqbSectionModel
    {
        #region プロパティ
        /// <summary>DEPQRTMENT_ID</summary>
        [StringLength(20)]
        [Display(Name = "DEPQRTMENT_ID")]
        public string DEPQRTMENT_ID { get; set; }
        #endregion
    }
    #endregion
}