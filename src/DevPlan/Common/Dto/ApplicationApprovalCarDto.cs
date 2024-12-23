using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Dto
{
    #region 処理待ち車両リスト検索条件クラス
    /// <summary>表示種別</summary>
    public enum ViewType : int
    {
        /// <summary>管理責任部署用</summary>
        ManagementResponsibility = 1,

        /// <summary>管理部署用</summary>
        Management = 2,

        /// <summary>全て</summary>
        All = 3

    }


    /// <summary>取得種別</summary>
    public enum ApplicationApprovalCarTargetType : int
    {
        /// <summary>発行</summary>
        Issue = 1,

        /// <summary>試験着手日</summary>
        TestStartDay = 2,

        /// <summary>月例点検</summary>
        MonthlyInspection = 3,

        /// <summary>廃却申請</summary>
        DispositionApplication = 4,

        /// <summary>T技本内移管</summary>
        ToukyouTransfer = 5,

        /// <summary>G技本内移管</summary>
        GunmaTransfer = 6,

        /// <summary>G→T移管</summary>
        GtTransfer = 7,

        /// <summary>T→G移管</summary>
        TgTransfer = 8,

        /// <summary>全て</summary>
        All = 9

    }

    /// <summary>チェック結果種別</summary>
    public enum CheckResultType : int
    {
        /// <summary>なし</summary>
        None = 0,

        /// <summary>OK</summary>
        Ok = 1,

        /// <summary>承認不可</summary>
        NoApproval = 2,

        /// <summary>権限無し</summary>
        NoAuthority = 3,

        /// <summary>月例点検入力</summary>
        MonthlyInput = 4,

        /// <summary>更新</summary>
        Update = 5,

        /// <summary>中止不可</summary>
        NotStop = 6

    }


    /// <summary>
    /// 処理待ち車両リスト検索条件クラス
    /// </summary>
    public class ApplicationApprovalCarSearchModel : TestCarCommonSearchModel
    {
        #region プロパティ
        /// <summary>
        /// 承認状況
        /// </summary>
        public string 承認状況 { get; set; }

        /// <summary>
        /// 表示種別
        /// </summary>
        public ViewType VIEW_TYPE { get; set; }

        /// <summary>
        /// ユーザーのみ取得
        /// </summary>
        public bool? USER_ONLY { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// 月例入力
        /// </summary>
        public bool? MONTHLY_INPUT { get; set; }

        /// <summary>
        /// 期限前車両
        /// </summary>
        public bool? EARLY_CAR { get; set; }

        /// <summary>
        /// 取得種別
        /// </summary>
        public ApplicationApprovalCarTargetType TARGET_TYPE { get; set; }
        #endregion
    }
    #endregion

    #region 処理待ち車両リストクラス
    /// <summary>
    /// 処理待ち車両リストクラス
    /// </summary>
    public class ApplicationApprovalCarModel : TestCarCommonModel
    {
        #region　プロパティ
        /// <summary>
        /// SEQNO
        /// </summary>
        public int SEQNO { get; set; }

        /// <summary>
        /// 承認要件コード
        /// </summary>
        public string 承認要件コード { get; set; }

        /// <summary>
        /// 承認要件名
        /// </summary>
        public string 承認要件名 { get; set; }

        /// <summary>
        /// 駐車場指定
        /// </summary>
        public short? 駐車場指定 { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        public int? STEPNO { get; set; }

        /// <summary>
        /// 承認状況
        /// </summary>
        public string 承認状況 { get; set; }

        /// <summary>
        /// 承認者レベル
        /// </summary>
        public string 承認者レベル { get; set; }

        /// <summary>
        /// 管理部署承認
        /// </summary>
        public string 管理部署承認 { get; set; }

        /// <summary>
        /// 処理日
        /// </summary>
        public DateTime? 処理日 { get; set; }

        // <summary>
        /// 使用部署
        /// </summary>
        public string 使用部署 { get; set; }

        /// <summary>
        /// 使用課名
        /// </summary>
        public string 使用課名 { get; set; }

        /// <summary>
        /// 使用部署名
        /// </summary>
        public string 使用部署名 { get; set; }

        /// <summary>
        /// 試験内容
        /// </summary>
        public string 試験内容 { get; set; }

        /// <summary>
        /// 実走行距離
        /// </summary>
        public string 実走行距離 { get; set; }

        /// <summary>
        /// 編集日
        /// </summary>
        public DateTime? 編集日 { get; set; }

        /// <summary>
        /// 編集者
        /// </summary>
        public string 編集者 { get; set; }

        /// <summary>
        /// 移管先部署ID
        /// </summary>
        public string 移管先部署ID { get; set; }

        /// <summary>
        /// 移管先部署_部ID
        /// </summary>
        public string 移管先部署_DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 移管先部署_部コード
        /// </summary>
        public string 移管先部署_DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 移管先部署_課ID
        /// </summary>
        public string 移管先部署_SECTION_ID { get; set; }

        /// <summary>
        /// 移管先部署_課コード
        /// </summary>
        public string 移管先部署_SECTION_CODE { get; set; }

        /// <summary>
        /// 移管先部署_担当ID
        /// </summary>
        public string 移管先部署_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 移管先部署_担当コード
        /// </summary>
        public string 移管先部署_SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 入力駐車場番号
        /// </summary>
        public string 入力駐車場番号 { get; set; }

        /// <summary>
        /// 表示種別
        /// </summary>
        public ViewType VIEW_TYPE { get; set; }

        /// <summary>
        /// 取得種別
        /// </summary>
        public ApplicationApprovalCarTargetType TARGET_TYPE { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// アクセス権限
        /// </summary>
        public string ACCESS_LEVEL { get; set; }

        /// <summary>
        /// ユーザーのみ取得
        /// </summary>
        public bool? USER_ONLY { get; set; }

        /// <summary>
        /// チェック結果
        /// </summary>
        public CheckResultType CHECK_RESULT { get; set; }

        /// <summary>
        /// 登録種別
        /// </summary>
        public AddType ADD_TYPE { get; set; } = AddType.All;
        #endregion
    }
    #endregion

    #region 使用履歴情報登録種別
    /// <summary>使用履歴情報登録種別</summary>
    public enum AddType : int
    {
        /// <summary>使用履歴</summary>
        History = 1,

        /// <summary>使用履歴承認情報</summary>
        Apply = 2,

        /// <summary>全て</summary>
        All = 3

    }
    #endregion

    #region 試験車使用履歴クラス
    /// <summary>
    /// 試験車使用履歴クラス
    /// </summary>
    public class TestCarUseHistoryModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        public int 履歴NO { get; set; }

        /// <summary>
        /// SEQNO
        /// </summary>
        public int SEQNO { get; set; }

        /// <summary>
        /// 承認要件コード
        /// </summary>
        public string 承認要件コード { get; set; }

        /// <summary>
        /// 駐車場指定
        /// </summary>
        public short? 駐車場指定 { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        public int? STEPNO { get; set; }

        /// <summary>
        /// 承認状況
        /// </summary>
        public string 承認状況 { get; set; }

        /// <summary>
        /// 承認者レベル
        /// </summary>
        public string 承認者レベル { get; set; }

        /// <summary>
        /// 管理部署承認
        /// </summary>
        public string 管理部署承認 { get; set; }

        /// <summary>
        /// 処理日
        /// </summary>
        public DateTime? 処理日 { get; set; }

        /// <summary>
        /// 管理責任課名
        /// </summary>
        public string 管理責任課名 { get; set; }

        /// <summary>
        /// 管理責任部署名
        /// </summary>
        public string 管理責任部署名 { get; set; }

        /// <summary>
        /// 管理責任部署名
        /// </summary>
        public string 管理責任部署 { get; set; }

        /// <summary>
        /// 管理所在地
        /// </summary>
        public string ESTABLISHMENT { get; set; }

        /// <summary>
        /// 使用課名
        /// </summary>
        public string 使用課名 { get; set; }

        /// <summary>
        /// 使用部署名
        /// </summary>
        public string 使用部署名 { get; set; }

        // <summary>
        /// 使用部署
        /// </summary>
        public string 使用部署 { get; set; }

        /// <summary>
        /// 試験内容
        /// </summary>
        public string 試験内容 { get; set; }

        /// <summary>
        /// 工事区分NO
        /// </summary>
        public string 工事区分NO { get; set; }

        /// <summary>
        /// 実走行距離
        /// </summary>
        public string 実走行距離 { get; set; }

        /// <summary>
        /// 編集日
        /// </summary>
        public DateTime? 編集日 { get; set; }

        /// <summary>
        /// 編集者
        /// </summary>
        public string 編集者 { get; set; }

        /// <summary>
        /// 編集者_NAME
        /// </summary>
        public string 編集者_NAME { get; set; }

        /// <summary>
        /// 移管先部署ID
        /// </summary>
        public string 移管先部署ID { get; set; }

        /// <summary>
        /// 移管先部署_部ID
        /// </summary>
        public string 移管先部署_DEPARTMENT_ID { get; set; }

        /// <summary>
        /// 移管先部署_部コード
        /// </summary>
        public string 移管先部署_DEPARTMENT_CODE { get; set; }

        /// <summary>
        /// 移管先部署_課ID
        /// </summary>
        public string 移管先部署_SECTION_ID { get; set; }

        /// <summary>
        /// 移管先部署_課コード
        /// </summary>
        public string 移管先部署_SECTION_CODE { get; set; }

        /// <summary>
        /// 移管先部署_担当ID
        /// </summary>
        public string 移管先部署_SECTION_GROUP_ID { get; set; }

        /// <summary>
        /// 移管先部署_担当コード
        /// </summary>
        public string 移管先部署_SECTION_GROUP_CODE { get; set; }

        /// <summary>
        /// 駐車場番号
        /// </summary>
        public string 駐車場番号 { get; set; }

        /// <summary>
        /// 取得種別
        /// </summary>
        public ApplicationApprovalCarTargetType TARGET_TYPE { get; set; }

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string PERSONEL_ID { get; set; }

        /// <summary>
        /// アクセス権限
        /// </summary>
        public string ACCESS_LEVEL { get; set; }

        /// <summary>
        /// チェック結果
        /// </summary>
        public CheckResultType CHECK_RESULT { get; set; }

        /// <summary>
        /// 登録種別
        /// </summary>
        public AddType ADD_TYPE { get; set; } = AddType.All;
        #endregion
    }
    #endregion

    #region 試験車使用履歴承認状況クラス
    /// <summary>
    /// 試験車使用履歴承認状況クラス
    /// </summary>
    public class TestCarUseHistoryApprovalModel
    {
        #region プロパティ
        /// <summary>
        /// データID
        /// </summary>
        public int データID { get; set; }

        /// <summary>
        /// 履歴NO
        /// </summary>
        public int 履歴NO { get; set; }

        /// <summary>
        /// SEQNO
        /// </summary>
        public int SEQNO { get; set; }

        /// <summary>
        /// 承認要件コード
        /// </summary>
        public string 承認要件コード { get; set; }

        /// <summary>
        /// 承認要件名
        /// </summary>
        public string 承認要件名 { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        public int STEPNO { get; set; }

        /// <summary>
        /// 承認STEP名
        /// </summary>
        public string 承認STEP名 { get; set; }

        /// <summary>
        /// 承認者レベル
        /// </summary>
        public string 承認者レベル { get; set; }

        /// <summary>
        /// 管理部署承認
        /// </summary>
        public string 管理部署承認 { get; set; }

        /// <summary>
        /// 承認日
        /// </summary>
        public DateTime? 承認日 { get; set; }

        /// <summary>
        /// 承認者
        /// </summary>
        public string 承認者 { get; set; }

        /// <summary>
        /// 承認者_NAME
        /// </summary>
        public string 承認者_NAME { get; set; }

        /// <summary>
        /// 承認者所属課
        /// </summary>
        public string 承認者所属課 { get; set; }

        /// <summary>
        /// 承認者所属担当
        /// </summary>
        public string 承認者所属担当 { get; set; }
        #endregion
    }
    #endregion

    #region 承認ステップクラス
    /// <summary>
    /// 承認ステップクラス
    /// </summary>
    public class ApprovalStepModel
    {
        #region プロパティ
        /// <summary>
        /// 承認要件コード
        /// </summary>
        public string 承認要件コード { get; set; }

        /// <summary>
        /// STEPNO
        /// </summary>
        public int STEPNO { get; set; }

        /// <summary>
        /// STEP名
        /// </summary>
        public string STEP名 { get; set; }

        /// <summary>
        /// 承認者レベル
        /// </summary>
        public string 承認者レベル { get; set; }

        /// <summary>
        /// 管理部署承認
        /// </summary>
        public string 管理部署承認 { get; set; }

        /// <summary>
        /// 駐車場指定
        /// </summary>
        public short? 駐車場指定 { get; set; }
        #endregion
    }
    #endregion

}
