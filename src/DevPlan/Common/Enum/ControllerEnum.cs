using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.UICommon.Enum
{
    #region コントローラ
    /// <summary>コントローラ</summary>
    public enum ControllerType : int
    {
        /// <summary>車系</summary>
        CarGroup,

        /// <summary>部検索</summary>
        Department,

        /// <summary>開発符号検索</summary>
        GeneralCode,

        /// <summary>車両管理担当</summary>
        CarManager,

        /// <summary>ログイン</summary>
        Login,

        /// <summary>課検索</summary>
        Section,

        /// <summary>所在地</summary>
        Location,

        /// <summary>担当検索</summary>
        SectionGroup,

        /// <summary>試験車スケジュール</summary>
        TestCarSchedule,

        /// <summary>試験車スケジュール項目</summary>
        TestCarScheduleItem,
        
        /// <summary>試験車スケジュール　コピー元選択</summary>
        TestCarScheduleCopy,

        /// <summary>試験車スケジュール一括本予約</summary>
        TestCarScheduleReserve,

        /// <summary>試験車スケジュール 本予約コピー</summary>
        TestCarScheduleKetteiCopy,

        /// <summary>試験車スケジュール 簡易入力</summary>
        TestCarHistoryComplete,

        /// <summary>ユーザー情報検索</summary>
        User,

        /// <summary>週報</summary>
        WeeklyReport,

        /// <summary>週報承認</summary>
        WeeklyReportApproval,

        /// <summary>月報</summary>
        MonthlyReport,

        /// <summary>部長名設定</summary>
        ManagerName,

        /// <summary>情報元一覧</summary>
        ReportMaterial,

        /// <summary>カーシェア一覧検索</summary>
        CarShareManagement,

        /// <summary>カーシェア予約済み一覧</summary>
        CarShareReserve,

        /// <summary>業務スケジュール</summary>
        WorkSchedule,

        /// <summary>業務スケジュール項目</summary>
        WorkScheduleItem,

        /// <summary>車型検索</summary>
        CarModel,

        /// <summary>仕向地検索</summary>
        Destination,

        /// <summary>TM検索</summary>
        Transmission,

        /// <summary>メーカー名検索</summary>
        MakerName,

        /// <summary>目的検索</summary>
        Purpose,

        /// <summary>行先検索</summary>
        Goal,

        /// <summary>種別検索</summary>
        Class,

        /// <summary>機能権限</summary>
        UserAuthority,

        /// <summary>お知らせ</summary>
        Information,

        /// <summary>車両使用者検索</summary>
        Car,

        /// <summary>項目コピー</summary>
        ScheduleItemCopy,

        /// <summary>項目移動</summary>
        ScheduleItemMove,

        /// <summary>目標進度リスト</summary>
        TargetProgressList,

        /// <summary>目標進度リスト名</summary>
        TargetProgressListName,

        /// <summary>試験車注意喚起</summary>
        TestCarReminder,

        /// <summary>試験車検索</summary>
        TestCar,

        /// <summary>お気に入り検索</summary>
        Favorite,

        /// <summary>お気に入り(業務計画)</summary>
        WorkFavorite,

        /// <summary>お気に入り(試験車)</summary>
        TestCarFavorite,

        /// <summary>お気に入り(カーシェア)</summary>
        CarShareFavorite,

        /// <summary>お気に入り(外製車)</summary>
        OuterCarFavorite,

        /// <summary>カーシェア外製車検索</summary>
        CarShareOuter,

        /// <summary>カーシェア内製車検索</summary>
        CarShareInner,

        /// <summary>外製車スケジュール</summary>
        OuterCarSchedule,

        /// <summary>外製車スケジュール項目</summary>
        OuterCarScheduleItem,

        /// <summary>カーシェアスケジュール</summary>
        CarShareSchedule,

        /// <summary>カーシェアスケジュール項目</summary>
        CarShareScheduleItem,

        /// <summary>機能マスタ一覧</summary>
        Function,

        /// <summary>機能権限一覧検索</summary>
        FunctionAuthorityName,

        /// <summary>性能名一覧検索</summary>
        PerformanceName,

        /// <summary>お気に入り(履歴関連)</summary>
        HistoryFavorite,

        /// <summary>機能権限</summary>
        FunctionAuthority,

        /// <summary>ロール権限</summary>
        RollAuthority,

        /// <summary>ロール</summary>
        Roll,

        /// <summary>ロール名</summary>
        RollName,

        /// <summary>役職</summary>
        OfficialPosition,

        /// <summary>作業履歴</summary>
        WorkHistory,

        /// <summary>月次スケジュール</summary>
        MonthlyWorkSchedule,

        /// <summary>月次スケジュール項目</summary>
        MonthlyWorkScheduleItem,

        /// <summary>お気に入り(月次計画)</summary>
        MonthlyWorkFavorite,

        /// <summary>月次計画承認</summary>
        MonthlyWorkApproval,

        /// <summary>月次計画同期</summary>
        MonthlyWorkSynchronize,

        /// <summary>作業履歴(試験車)</summary>
        TestCarWorkHistory,

        /// <summary>作業履歴(カーシェア予約)</summary>
        CarShareWorkHistory,

        /// <summary>作業履歴(カーシェア予約)</summary>
        OuterCarWorkHistory,

        /// <summary>種別別進捗状況一覧</summary>
        WorkProgress,

        /// <summary>進捗履歴</summary>
        WorkProgressHistory,

        /// <summary>開発符号権限</summary>
        GeneralCodeAuthority,

        /// <summary>目標進度リストマスター</summary>
        TargetProgressListMaster,

        /// <summary>検討会資料</summary>
        MeetingDocument,

        /// <summary>検討会資料名</summary>
        MeetingDocumentName,

        /// <summary>検討会資料詳細</summary>
        MeetingDocumentDetail,

        /// <summary>検討会資料部記号</summary>
        MeetingDocumentDept,

        /// <summary>CAP課題</summary>
        Cap,

        /// <summary>CAP重要度</summary>
        CapImportance,

        /// <summary>CAP種別</summary>
        CapKind,

        /// <summary>CAP供試品</summary>
        CapSample,

        /// <summary>CAP織込時期クラス</summary>
        CapStage,

        /// <summary>CAP指摘分類</summary>
        CapIdentification,

        /// <summary>CAPフォロー状況</summary>
        CapFollow,

        /// <summary>CAP仕向地</summary>
        CapLocation,

        /// <summary>CAP対策予定</summary>
        CapSchedule,

        /// <summary>資料分類コード</summary>
        DocumentCode,

        /// <summary>資料評価レベル</summary>
        DocumentLevel,

        /// <summary>評価車両詳細</summary>
        CapDetail,

        /// <summary>CAP部署</summary>
        CapSection,

        /// <summary>SQB部署</summary>
        SqbSection,

        /// <summary>設計チェック</summary>
        DesignCheck,

        /// <summary>設計チェック指摘</summary>
        DesignCheckPoint,

        /// <summary>設計チェック参加者</summary>
        DesignCheckUser,

        /// <summary>設計チェック対象車</summary>
        DesignCheckCar,

        /// <summary>設計チェック状況</summary>
        DesignCheckProgress,

        /// <summary>試験車（試験車管理）</summary>
        DesignCheckSystemTestCar,

        /// <summary>設計チェック指摘・状況</summary>
        DesignCheckPointProg,

        /// <summary>試験車（設計チェック）</summary>
        DesignCheckTestCar,

        /// <summary>状況記号（設計チェック）</summary>
        DesignCheckProgressSymbol,

        /// <summary>PC端末権限</summary>
        PCAuthority,

        /// <summary>試験車(管理票)</summary>
        ControlSheetTestCar,

        /// <summary>試験車基本情報(管理票)</summary>
        ControlSheetTestCarBasic,

        /// <summary>試験車履歴情報(管理票)</summary>
        ControlSheetTestCarHistory,

        /// <summary>受領先情報</summary>
        Recipient,

        /// <summary>廃却期限リスト</summary>
        DisposalPeriod,

        /// <summary>廃却車両搬出日入力</summary>
        DisposalCarryout,

        /// <summary>車系情報</summary>
        CarGroupInfo,

        /// <summary>グレード情報</summary>
        GradeInfo,

        /// <summary>エンジン型式情報</summary>
        EngineModelInfo,

        /// <summary>車型情報</summary>
        CarModelInfo,

        /// <summary>駐車場情報</summary>
        ParkingNumberInfo,

        /// <summary>開発符号情報</summary>
        GeneralCodeInfo,

        /// <summary>試作時期情報</summary>
        PrototypeSeasonInfo,

        /// <summary>メーカー名情報</summary>
        MakerNameInfo,

        /// <summary>仕向地情報</summary>
        DestinationInfo,

        /// <summary>排気量情報</summary>
        DisplacementInfo,

        /// <summary>トランスミッション情報</summary>
        TransmissionInfo,

        /// <summary>駆動方式情報</summary>
        DriveMethodInfo,

        /// <summary>車体色情報</summary>
        CarBodyColorInfo,

        /// <summary>所属</summary>
        Affiliation,

        /// <summary>ユーザー表示設定情報</summary>
        UserDisplayConfiguration,

        /// <summary>試験車衝突車管理部署</summary>
        TestCarCollisionCarManagementDepartment,

        /// <summary>ユーザー検索項目</summary>
        ControlSheetSearchItem,

        /// <summary>ユーザー検索条件</summary>
        ControlSheetSearchCondition,

        /// <summary>ユーザー検索マスター</summary>
        ControlSheetSearchMaster,

        /// <summary>管理票検索</summary>
        ControlSheet,

        /// <summary>管理票ラベル印刷</summary>
        ControlSheetLabelPrint,

        /// <summary>製作車</summary>
        ProductionCar,

        /// <summary>開発符号情報(製作車)検索</summary>
        ProductionCarGeneralCodeInfo,

        /// <summary>処理待ち車両リスト</summary>
        ApplicationApprovalCar,

        /// <summary>承認状況</summary>
        ApprovalStatus,

        /// <summary>試験車使用履歴権限チェック</summary>
        TestCarUseHistoryAuthorityCheck,

        /// <summary>試験車使用履歴入力チェック</summary>
        TestCarUseHistoryInputCheck,

        /// <summary>試験車使用履歴変更チェック</summary>
        TestCarUseHistoryChangeCheck,

        /// <summary>試験車使用履歴承認チェック</summary>
        TestCarUseHistoryApprovalCheck,

        /// <summary>試験車履歴</summary>
        TestCarHistory,

        /// <summary>試験車使用履歴</summary>
        TestCarUseHistory,

        /// <summary>試験車使用履歴存在チェック</summary>
        TestCarUseHistoryExistCheck,

        /// <summary>試験車使用履歴承認状況</summary>
        TestCarUseHistoryApproval,

        /// <summary>車両管理</summary>
        CarManagement,

        /// <summary>指定月台数リスト</summary>
        DesignatedMonthNumber,

        /// <summary>台数集計結果</summary>
        NumberAggregate,

        /// <summary>車検・点検リスト</summary>
        CarInspection,

        /// <summary>駐車場所在地</summary>
        ParkingLocation,

        /// <summary>駐車場エリア</summary>
        ParkingArea,

        /// <summary>駐車場区画</summary>
        ParkingSection,

        /// <summary>駐車場管理</summary>
        ParkingUse,

        /// <summary>駐車カウント</summary>
        ParkingCount,

        /// <summary>ファイル</summary>
        File,

        /// <summary>スケジュール利用車</summary>
        ScheduleCar,

        /// <summary>スケジュール利用車詳細</summary>
        ScheduleCarDetail,

        /// <summary>カレンダー稼働日</summary>
        CalendarKadou,

        /// <summary>閲覧権限状況</summary>
        BrowsingAuthorityStatus,

        /// <summary>稼働率算出</summary>
        CarShareManagementRatePrint,

        /// <summary>操作ログ</summary>
        OperationLog,

        /// <summary>設計チェック詳細</summary>
        DesignCheckDetail,

        /// <summary>CAPで課（専門部門）が使用している開発符号の閲覧権限</summary>
        CapSectionUseGeneralCode,

        /// <summary>トラック予約よく使う目的地</summary>
        FrequentlyUsedDestinations,

        /// <summary>トラック予約メール原文</summary>
        RegularMailDetail,

        /// <summary>トラック予約メール特殊文字</summary>
        RegularMailDetailMaster,

        /// <summary>トラック予約スケジュール項目</summary>
        TruckScheduleItem,

        /// <summary>トラック予約スケジュール</summary>
        TruckSchedule,

        /// <summary>トラック行き先マスタ</summary>
        TruckSection,

        /// <summary>トラック管理者情報</summary>
        TruckManagementUser,

        /// <summary>トラック予約定期便時間帯</summary>
        TruckRegularTime,

        /// <summary>トラック定時間日</summary>
        FixedTimeDaySetting,

        /// <summary>トラック予約コメント</summary>
        TruckComment,

        /// <summary>トラックスケジュール数カウント</summary>
        TruckScheduleCount,

        /// <summary>Xeyeデータ情報</summary>
        Xeye,

        /// <summary>XeyeID情報</summary>
        XeyeData,

        /// <summary>処理待ち車両</summary>
        PendingCar,

        //Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
        /// <summary>写真・動画検索</summary>
        NDriveLink,
        //Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修
        /// <summary>設計チェックインポート</summary>
        DesignCheckImport,
        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

        //Append Start 2021/06/24 張晋華 開発計画表設計チェック機能改修
        /// <summary>設計チェックEXCEL取込判断</summary>
        DesignCheckExcelInput,
        //Append End 2021/06/24 張晋華 開発計画表設計チェック機能改修

        //Append Start 2021/08/20 杉浦 設計チェック請負
        /// <summary>設計チェックEXCEL取込パス取得</summary>
        DesignCheckImportGetPath,
        //Append End 2021/08/20 杉浦 設計チェック請負

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        /// <summary>トラック予約一覧パス取得</summary>
        TruckReserve,
        //Append End 2022/01/11 杉浦 トラック予約一覧を追加

        //Append Start 2022/01/11 杉浦 カーシェア用使用履歴簡易入力を追加
        /// <summary>カーシェア用使用履歴パス取得</summary>
        CarSharingWorkHistory,
        //Append End 2022/01/11 杉浦 カーシェア用使用履歴簡易入力を追加

        //Append Start 2022/01/31 杉浦 項目を変動できるようにする
        /// <summary>ユーザー表示設定情報</summary>
        ScheduleItemDisplayConfiguration,
        //Append End 2022/01/31 杉浦 項目を変動できるようにする

        //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
        /// <summary>全スケジュールチェック情報</summary>
        AllSchedule,
        //Append End 2022/02/03 杉浦 試験車日程の車も登録する

        //Append Start 2022/02/04 杉浦 月例点検一括省略
        /// <summary>月例点検一括省略情報</summary>
        ExceptMonthlyInspection,
        //Append End 2022/02/04 杉浦 月例点検一括省略

        //Append Start 2021/08/25 矢作
        /// <summary>カーシェア管理画面_終了日更新対象取得</summary>
        CarShareScheduleFromList,
        //Append Start 2021/08/25 矢作
    }
    #endregion
}
