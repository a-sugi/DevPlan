using System;

namespace DevPlan.UICommon.Dto
{
    #region CAP課題検索条件クラス
    /// <summary>
    /// CAP課題検索条件クラス
    /// </summary>
    [Serializable]
    public class CapSearchModel
    {
        #region プロパティ
        /// <summary>項目_ID</summary>
        public long? 項目_ID { get; set; }

        /// <summary>対応_ID</summary>
        public long? 対応_ID { get; set; }

        /// <summary>車種</summary>
        public string[] GENERAL_CODE { get; set; }

        /// <summary>回答期限FROM</summary>
        public DateTime? 回答期限FROM { get; set; }

        /// <summary>回答期限TO</summary>
        public DateTime? 回答期限TO { get; set; }

        /// <summary>専門部名</summary>
        public string[] 専門部名 { get; set; }

        /// <summary>専門部署名</summary>
        public string[] 専門部署名 { get; set; }

        /// <summary>重要度</summary>
        public string 重要度 { get; set; }

        /// <summary>ステータス</summary>
        public short? FLAG_CLOSE { get; set; }

        /// <summary>部署承認</summary>
        public short? FLAG_上司承認 { get; set; }

        /// <summary>CAP確認</summary>
        public short? FLAG_CAP確認 { get; set; }

        /// <summary>種別</summary>
        public string CAP種別 { get; set; }

        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }

        /// <summary>完了日程</summary>
        public short? 完了日程 { get; set; }

        /// <summary>出図日程</summary>
        public short? 出図日程 { get; set; }

        /// <summary>対策予定</summary>
        public string 対策予定 { get; set; }

        /// <summary>指摘分類</summary>
        public string 指摘分類 { get; set; }

        /// <summary>供試品</summary>
        public string 供試品 { get; set; }

        /// <summary>織込時期</summary>
        public string 織込時期 { get; set; }

        /// <summary>FLAG_最新</summary>
        public short? FLAG_最新 { get; set; }

        /// <summary>ID</summary>
        public long? ID { get; set; }

        /// <summary>社員コード（パーソナルID）</summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>関連課フラグ</summary>
        public short? FLAG_関連課 { get; set; }
        #endregion
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
        public long ID { get; set; }

        /// <summary>CAP確認</summary>
        public short? FLAG_CAP確認 { get; set; }

        /// <summary>回答期限</summary>
        public DateTime? 回答期限 { get; set; }

        /// <summary>承認</summary>
        public short? FLAG_上司承認 { get; set; }

        /// <summary>専門部名</summary>
        public string 専門部名 { get; set; }
        
        /// <summary>専門部署名</summary>
        public string 専門部署名 { get; set; }

        /// <summary>対策予定</summary>
        public string 対策予定 { get; set; }

        /// <summary>対策案</summary>
        public string 対応策 { get; set; }

        /// <summary>対策案編集日</summary>
        public DateTime? 対策案編集日 { get; set; }

        /// <summary>対策案編集者_ID</summary>
        public string 対策案編集者_ID { get; set; }

        /// <summary>対策案編集者</summary>
        public string 対策案編集者 { get; set; }

        /// <summary>事前把握</summary>
        public string 事前把握有無 { get; set; }

        /// <summary>分類</summary>
        public short? 分類 { get; set; }

        /// <summary>分類意味</summary>
        public string 分類意味 { get; set; }

        /// <summary>評価レベル</summary>
        public string 評価レベル { get; set; }

        /// <summary>評価意味</summary>
        public string 評価意味 { get; set; }

        /// <summary>評価項目</summary>
        public string 評価項目 { get; set; }

        /// <summary>レベル基準</summary>
        public string レベル基準 { get; set; }

        /// <summary>判断イメージ</summary>
        public string 判断イメージ { get; set; }

        /// <summary>完了日程</summary>
        public DateTime? 完了日程 { get; set; }

        /// <summary>供試品</summary>
        public string 供試品 { get; set; }

        /// <summary>出図日程</summary>
        public DateTime? 出図日程 { get; set; }

        /// <summary>織込時期</summary>
        public string 織込時期 { get; set; }

        /// <summary>編集日</summary>
        public DateTime? 編集日 { get; set; }

        /// <summary>編集者_ID</summary>
        public string 編集者_ID { get; set; }

        /// <summary>編集者</summary>
        public string 編集者 { get; set; }

        //Append Start 2021/04/19 杉浦 背景色を戻す処理の追加
        /// <summary>BK_編集日</summary>
        public DateTime? BK_編集日 { get; set; }

        /// <summary>BK_編集者_ID</summary>
        public string BK_編集者_ID { get; set; }

        /// <summary>BK_編集者</summary>
        public string BK_編集者 { get; set; }
        //Append Start 2021/04/19 杉浦 背景色を戻す処理の追加

        /// <summary>回答期限設定日</summary>
        public DateTime? 回答期限設定日 { get; set; }

        /// <summary>承認日</summary>
        public DateTime? 承認日 { get; set; }

        /// <summary>承認者_ID</summary>
        public string 承認者_ID { get; set; }

        /// <summary>承認者</summary>
        public string 承認者 { get; set; }

        /// <summary>FLAG_最新</summary>
        public short? FLAG_最新 { get; set; }

        /// <summary>作成日（履歴作成日時）</summary>
        public DateTime? 作成日 { get; set; }

        /// <summary>項目_ID</summary>
        public long 項目_ID { get; set; }

        /// <summary>ステータス</summary>
        public short? FLAG_CLOSE { get; set; }

        /// <summary>No.</summary>
        public long NO { get; set; }

        /// <summary>種別</summary>
        public string CAP種別 { get; set; }

        /// <summary>重要度</summary>
        public string 重要度 { get; set; }

        /// <summary>重要度説明</summary>
        public string 説明 { get; set; }

        /// <summary>項目</summary>
        public string 項目 { get; set; }

        /// <summary>詳細</summary>
        public string 詳細 { get; set; }

        /// <summary>評価車両</summary>
        public string 評価車両 { get; set; }

        /// <summary>仕向地</summary>
        public string 仕向地 { get; set; }

        /// <summary>CAP確認結果</summary>
        public string CAP確認結果 { get; set; }

        //Append Start 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加
        /// <summary>CAP確認結果</summary>
        public string N_DRIVE_LINK { get; set; }
        //Append End 2021/05/18 杉浦 CAP確認結果に「写真・動画」列を追加

        /// <summary>フォロー状況</summary>
        public string フォロー状況 { get; set; }

        /// <summary>CAP確認時期</summary>
        public string 織込時期_項目 { get; set; }

        /// <summary>指摘分類</summary>
        public string 指摘分類 { get; set; }

        /// <summary>EG型式</summary>
        public string E_G型式 { get; set; }

        /// <summary>排気量</summary>
        public string 排気量 { get; set; }

        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }

        /// <summary>駆動方式</summary>
        public string 駆動方式 { get; set; }

        /// <summary>グレード</summary>
        public string グレード { get; set; }

        /// <summary>車体番号</summary>
        public string 車体番号 { get; set; }

        /// <summary>過去履歴件数</summary>
        public long 過去履歴件数 { get; set; }

        /// <summary>親対応_ID</summary>
        public int? 親対応_ID { get; set; }

        /// <summary>修正カラム</summary>
        public string 修正カラム { get; set; }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get; set; }

        /// <summary>権限フラグ(0:閲覧権限なし 1:閲覧権限あり)</summary>
        public short PERMIT_FLG { get; set; }

        /// <summary>変更（DTO用）</summary>
        public string 変更 { get; set; }

        /// <summary>対策案変更（DTO用）</summary>
        public string 対策案変更 { get; set; }

        /// <summary>方向付け確定期限</summary>
        public DateTime? 方向付け確定期限 { get; set; }
        #endregion
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
        public string 重要度 { get; set; }

        /// <summary>説明</summary>
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
        public int? ID { get; set; }
        /// <summary>織込時期</summary>
        public string 織込時期 { get; set; }
        /// <summary>SORT_NO</summary>
        public int? SORT_NO { get; set; }
        /// <summary>FLAG_DISP</summary>
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
        public short 分類コード { get; set; }

        /// <summary>意味</summary>
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
        public string 評価レベル { get; set; }

        /// <summary>項目</summary>
        public string 項目 { get; set; }

        /// <summary>レベル基準</summary>
        public string レベル基準 { get; set; }

        /// <summary>判断イメージ</summary>
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
        public string 開発符号 { get; set; }

        /// <summary>号車</summary>
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
        public string E_G型式 { get; set; }

        /// <summary>排気量</summary>
        public string 排気量 { get; set; }

        /// <summary>トランスミッション</summary>
        public string トランスミッション { get; set; }

        /// <summary>駆動方式</summary>
        public string 駆動方式 { get; set; }

        /// <summary>車体番号</summary>
        public string 車体番号 { get; set; }

        /// <summary>グレード</summary>
        public string グレード { get; set; }
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
        public string SECTION_ID { get; set; }

        /// <summary>SECTION_GROUP_ID</summary>
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
        public string DEPQRTMENT_ID { get; set; }
        #endregion
    }
    #endregion
}
