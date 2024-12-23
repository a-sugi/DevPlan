using System;
using System.Collections.Generic;
using System.Drawing;

using GrapeCity.Win.CalendarGrid;

using Newtonsoft.Json;
using DevPlan.UICommon.Enum;
using System.Windows.Forms;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Config;

namespace DevPlan.UICommon.Dto
{
    #region スケジュール項目編集区分
    /// <summary>スケジュール編集区分</summary>
    public enum ScheduleItemEditType : int
    {
        /// <summary>追加</summary>
        Insert,

        /// <summary>更新</summary>
        Update,

        /// <summary>削除</summary>
        Delete,

        /// <summary>行追加</summary>
        RowAdd,

        /// <summary>行削除</summary>
        RowDelete,

        /// <summary>移譲</summary>
        Transfer

    }
    #endregion

    #region スケジュール編集区分
    /// <summary>スケジュール編集区分</summary>
    public enum ScheduleEditType : int
    {
        /// <summary>追加</summary>
        Insert,

        /// <summary>更新</summary>
        Update,

        /// <summary>削除</summary>
        Delete,

        /// <summary>貼り付け</summary>
        Paste

    }
    #endregion

    #region カレンダーグリッド設定クラス
    /// <summary>
    /// カレンダーグリッド設定クラス
    /// </summary>
    /// <typeparam name="Item">スケジュール項目の型</typeparam>
    /// <typeparam name="Schedule">スケジュールの型</typeparam>
    public class CalendarGridConfigModel<Item, Schedule>
        where Item : class, new()
        where Schedule : class, new()
    {
        #region プロパティ
        /// <summary>カレンダーグリッド</summary>
        public GcCalendarGrid CalendarGrid { get; set; }

        /// <summary>行ヘッダー編集可否</summary>
        public bool IsRowHeaderEdit { get; set; }

        /// <summary>試験車管理SYSフィルタ利用可否</summary>
        public bool IsRowHeaderSysFilter { get; set; }

        /// <summary>スケジュール編集可否</summary>
        public bool IsScheduleEdit { get; set; }

        ///// <summary>スケジュールツールチップ日時書式可否</summary>
        //public bool IsScheduleToolTipDateTimeFormat { get; set; }

        /// <summary>スケジュール表示期間変更可否</summary>
        public bool IsScheduleViewPeriodChange { get; set; }

        private string _isScheduleViewPeriodChangeMessage = Resources.KKM01003;
        /// <summary>スケジュール表示期間変更可否メッセージ</summary>
        public string IsScheduleViewPeriodChangeMessage
        {
            get { return _isScheduleViewPeriodChangeMessage; }
            set { _isScheduleViewPeriodChangeMessage = value; }
        }

        /// <summary>コーナーヘッダーの表題</summary>
        public string CornerHeaderValue { get; set; }

        /// <summary>コーナーヘッダーのコンテキストメニューの連想配列</summary>
        public IDictionary<string, Action> CornerHeaderContextMenuMap { get; set; }

        /// <summary>スケジュール行ヘッダーのコンテキストメニューの連想配列</summary>
        public IDictionary<string, Action<ScheduleItemModel<Item>>> ScheduleRowHeaderContextMenuMap { get; set; }

        /// <summary>スケジュールのコンテキストメニューの連想配列</summary>
        public IDictionary<string, Action<ScheduleModel<Schedule>>> ScheduleContextMenuMap { get; set; }

        // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきコメントアウト。業務計画表、月次計画表リリースと共に修正。
        ///// <summary>その他の行ヘッダーの値を取得するデリゲート</summary>
        //public Func<ScheduleItemModel<Item>, int, string> GetRowHeaderOtherValue { get; set; }        
        ///// <summary>追加行ヘッダー情報の連想配列</summary>
        //public IDictionary<string, RowHeaderModel> AddRowHeaderColumnMap { get; set; }

        /// <summary>スケジュール行ヘッダーのコンテキストメニューの項目を取得するデリゲート</summary>
        public Func<ScheduleItemModel<Item>, ToolStripItem[]> GetRowHeaderContexMenuItems { get; set; }

        /// <summary>スケジュール行ヘッダーの共通コンテキストメニューの項目を取得するデリゲート</summary>
        public Func<ScheduleItemModel<Item>, ToolStripItem[]> GetRowHeaderCommonContexMenuItems { get; set; }

        /// <summary>スケジュールのコンテキストメニューの項目を取得するデリゲート</summary>
        public Func<ScheduleModel<Schedule>, ToolStripItem[]> GetContentContexMenuItems { get; set; }

        /// <summary>スケジュールの背景色を取得するデリゲート</summary>
        public Func<ScheduleModel<Schedule>, CalendarScheduleColorEnum, CalendarScheduleColorEnum> GetScheduleBackColor { get; set; }

        /// <summary>セルの背景色を取得するデリゲート</summary>
        public Func<ScheduleItemModel<Item>, DateTime, CalendarScheduleColorEnum, CalendarScheduleColorEnum> GetCellBackColor { get; set; }

        /// <summary>行ヘッダー</summary>
        public RowHeaderModel BaseRowHeader { get; set; }

        /// <summary>スケジュールダブルクリックテキスト</summary>
        public string ScheduleDoubleClickLabelText { get; set; }

        /// <summary>スケジュールのツールチップテキストのタイトルを取得するデリゲート</summary>
        public Func<ScheduleModel<Schedule>, string> GetScheduleToolTipTitle { get; set; }

        /// <summary>スケジュールの追加するツールチップテキストを取得するデリゲート</summary>
        public Func<ScheduleModel<Schedule>, string> GetScheduleAddToolTipText { get; set; }

        /// <summary>その他スケジュール</summary>
        public OtherScheduleModel<Item> OtherSchedule { get; set; }

        /// <summary>スケジュール項目の背景色を取得するデリゲート</summary>
        public Func<ScheduleItemModel<Item>, CalendarScheduleColorEnum> GetScheduleItemBackColor { get; set; }

        /// <summary>幅やフォント等カレンダー設定情報</summary>
        public CalendarSettings CalendarSettings { get; set; }

        /// <summary>読み取り専用開始行番号</summary>
        public int ReadOnlyRowCount { get; set; }

        private bool _isSortContextMenuVisible = true;
        /// <summary>ソートコンテキストメニュー表示有無</summary>
        public bool IsSortContextMenuVisible
        {
            get
            {
                return _isSortContextMenuVisible;
            }
            set
            {
                _isSortContextMenuVisible = value;
            }
        }

        private int _mouseWheelCount = new GridAppConfigAccessor().GetCalendarGridMouseWheelCount();
        /// <summary>カレンダグリッドマウスホイール量</summary>
        public int MouseWheelCount
        {
            get
            {
                return _mouseWheelCount;
            }
            set
            {
                _mouseWheelCount = value;
            }
        }

        //Append Start 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理
        public int ViewRange { get; set; } = 3;
        //Append End 2021/09/27 杉浦 カーシェア日程表示期間変更に伴う処理

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CalendarGridConfigModel()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="calendarGrid">カレンダーグリッド</param>
        /// <param name="isRowHeaderEdit">行ヘッダー編集可否</param>
        /// <param name="isScheduleEdit">スケジュール編集可否</param>
        /// <param name="cornerHeaderContextMenuMap">コーナーヘッダーのコンテキストメニューの連想配列</param>
        /// <param name="scheduleRowHeaderContextMenuMap">スケジュール行ヘッダーのコンテキストメニューの連想配列</param>
        /// <param name="scheduleContextMenuMap">スケジュールのコンテキストメニューの連想配列</param>
        /// <param name="addRowHeaderColumnConfig">追加行ヘッダーコンフィグ情報の連想配列</param>
        /// <param name="getRowHeaderOtherValue">追加行ヘッダーコンフィグ情報の連想配列</param>
        public CalendarGridConfigModel(GcCalendarGrid calendarGrid,
            bool isRowHeaderEdit, bool isScheduleEdit,
            IDictionary<string, Action> cornerHeaderContextMenuMap,
            IDictionary<string, Action<ScheduleItemModel<Item>>> scheduleRowHeaderContextMenuMap,
            IDictionary<string, Action<ScheduleModel<Schedule>>> scheduleContextMenuMap
            //IDictionary<string, RowHeaderModel> addRowHeaderColumnConfig = null,
            //Func<ScheduleItemModel<Item>, int, string> getRowHeaderOtherValue = null
            )
            : this(calendarGrid, isRowHeaderEdit, isScheduleEdit, null, cornerHeaderContextMenuMap, scheduleRowHeaderContextMenuMap, scheduleContextMenuMap)
        {
            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきaddRowHeaderColumnConfig,getRowHeaderOtherValue,isScheduleToolTipDateTimeFormat(true固定)はコメントアウト＆thisの引数から削除。業務計画表、月次計画表リリースと共に修正。
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="calendarGrid">カレンダーグリッド</param>
        /// <param name="isRowHeaderEdit">行ヘッダー編集可否</param>
        /// <param name="isScheduleEdit">スケジュール編集可否</param>
        /// <param name="cornerHeaderValue">コーナーヘッダーの表題</param>
        /// <param name="cornerHeaderContextMenuMap">コーナーヘッダーのコンテキストメニューの連想配列</param>
        /// <param name="scheduleRowHeaderContextMenuMap">スケジュール行ヘッダーのコンテキストメニューの連想配列</param>
        /// <param name="scheduleContextMenuMap">スケジュールのコンテキストメニューの連想配列</param>
        /// <param name="addRowHeaderColumnConfig">追加行ヘッダーコンフィグ情報の連想配列</param>
        /// <param name="getRowHeaderOtherValue">その他の行ヘッダーの値を取得するデリゲート</param>
        public CalendarGridConfigModel(GcCalendarGrid calendarGrid,
            bool isRowHeaderEdit, bool isScheduleEdit,
            //bool isScheduleToolTipDateTimeFormat, 
            string cornerHeaderValue,
            IDictionary<string, Action> cornerHeaderContextMenuMap,
            IDictionary<string, Action<ScheduleItemModel<Item>>> scheduleRowHeaderContextMenuMap,
            IDictionary<string, Action<ScheduleModel<Schedule>>> scheduleContextMenuMap
            //IDictionary<string, RowHeaderModel> addRowHeaderColumnConfig = null,
            //Func<ScheduleItemModel<Item>, int, string> getRowHeaderOtherValue = null
            )
        {
            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきaddRowHeaderColumnConfig,getRowHeaderOtherValue,isScheduleToolTipDateTimeFormatはコメントアウト。業務計画表、月次計画表リリースと共に修正。

            //カレンダーグリッド
            this.CalendarGrid = calendarGrid;

            //行ヘッダー編集可否
            this.IsRowHeaderEdit = isRowHeaderEdit;

            //スケジュール編集可否
            this.IsScheduleEdit = isScheduleEdit;

            ////スケジュールツールチップ日時書式可否
            //this.IsScheduleToolTipDateTimeFormat = isScheduleToolTipDateTimeFormat;

            //スケジュール表示期間変更可否
            this.IsScheduleViewPeriodChange = true;

            //コーナーヘッダーの表題
            this.CornerHeaderValue = cornerHeaderValue;

            //コーナーヘッダーのコンテキストメニューの連想配列
            if (isRowHeaderEdit)
            {
                this.CornerHeaderContextMenuMap = cornerHeaderContextMenuMap;
            }

            //スケジュール行ヘッダーのコンテキストメニューの連想配列
            this.ScheduleRowHeaderContextMenuMap = scheduleRowHeaderContextMenuMap;

            //スケジュールのコンテキストメニューの連想配列
            this.ScheduleContextMenuMap = scheduleContextMenuMap;

            // TODO : ■テストを行えない機能（2018年上期リリース対象外）につきコメントアウト。業務計画表、月次計画表リリースと共に修正。
            ////追加行ヘッダーコンフィグ情報の連想配列
            //this.AddRowHeaderColumnMap = addRowHeaderColumnConfig;
            ////その他の行ヘッダーの値を取得するデリゲート
            //this.GetRowHeaderOtherValue = getRowHeaderOtherValue;

            //スケジュール行ヘッダーのコンテキストメニュー項目を取得するデリゲート
            this.GetRowHeaderContexMenuItems = null;

            //スケジュール行ヘッダーのコンテキストメニュー項目を取得するデリゲート
            this.GetRowHeaderCommonContexMenuItems = null;

            //スケジュールの背景色取得のデリゲート
            this.GetScheduleBackColor = null;

            //セルの背景色を取得するデリゲート
            this.GetCellBackColor = null;

            //行ヘッダー
            this.BaseRowHeader = null;

            //スケジュールダブルクリックテキスト
            this.ScheduleDoubleClickLabelText = "※項目ダブルクリックで作業履歴を表示します。";

            //スケジュールのツールチップテキストのタイトルを取得するデリゲート
            this.GetScheduleToolTipTitle = null;

            //スケジュールの追加するツールチップテキストを取得するデリゲート
            this.GetScheduleAddToolTipText = null;

            //その他スケジュール
            this.OtherSchedule = null;

            //スケジュール項目の背景色を取得するデリゲート
            this.GetScheduleItemBackColor = null;

        }
        #endregion

    }
    #endregion

    #region カレンダー項目クラス
    /// <summary>
    /// カレンダー項目クラス
    /// </summary>
    /// <typeparam name="Item">スケジュール項目の型</typeparam>
    /// <typeparam name="Schedule">スケジュールの型</typeparam>
    public class CalendarItemModel<Item, Schedule>
        where Item : class, new()
        where Schedule : class, new()
    {
        #region プロパティ
        /// <summary>スケジュール項目</summary>
        public ScheduleItemModel<Item> ScheduleItem { get; set; }

        /// <summary>スケジュール</summary>
        public IEnumerable<ScheduleModel<Schedule>> ScheduleList { get; set; }
        #endregion
    }
    #endregion

    #region スケジュール項目クラス
    /// <summary>
    /// スケジュール項目クラス
    /// </summary>
    /// <typeparam name="T">スケジュール項目の型</typeparam>
    public class ScheduleItemModel<T> where T : class, new()
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }

        /// <summary>タイトル</summary>
        public string Title { get; set; }

        /// <summary>ツールチップ</summary>
        public string ToolTipText { get; set; }

        /// <summary>ツールチップオブジェクト</summary>
        public ToolTip ToolTip { get; set; }

        /// <summary>ソート順</summary>
        public double SortNo { get; set; }

        /// <summary>行数</summary>
        public int RowCount { get; set; }

        /// <summary>スケジュール項目編集区分</summary>
        public ScheduleItemEditType ScheduleItemEdit { get; set; }

        /// <summary>スケジュール項目</summary>
        public T ScheduleItem { get; set; }
        
        /// <summary>管理票番号</summary>
        public string KanriNo { get; private set; }

        /// <summary>新規登録行の生成を自動で行う場合はTrue</summary>
        private bool _isInputNewRow = true;
        public bool IsInputNewRow { get { return _isInputNewRow; } set { _isInputNewRow = value; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScheduleItemModel()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="generalCode">開発符号</param>
        /// <param name="title">タイトル</param>
        /// <param name="rowCount">行数</param>
        /// <param name="sortNo">ソート順</param>
        /// <param name="item">スケジュール項目</param>
        public ScheduleItemModel(long id, string generalCode, string title, int rowCount, double? sortNo, string kanriNo, T item)
            : this(id, generalCode, title, title, rowCount, sortNo, kanriNo, item)
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="generalCode">開発符号</param>
        /// <param name="title">タイトル</param>
        /// <param name="toolTip">ツールチップ</param>
        /// <param name="rowCount">行数</param>
        /// <param name="sortNo">ソート順</param>
        /// <param name="item">スケジュール項目</param>
        public ScheduleItemModel(long id, string generalCode, string title, string toolTip, int rowCount, double? sortNo, string kanriNo, T item)
        {
            //ID
            this.ID = id;

            //開発符号
            this.GeneralCode = generalCode;

            //管理票番号
            this.KanriNo = kanriNo;

            //タイトル
            this.Title = title;

            //ツールチップ
            this.ToolTipText = toolTip;

            //行数
            this.RowCount = rowCount;

            //ソート順
            this.SortNo = sortNo ?? 99999;

            //スケジュール編集区分
            this.ScheduleItemEdit = ScheduleItemEditType.Update;

            //スケジュール項目
            this.ScheduleItem = item;

        }
        #endregion

    }
    #endregion

    #region スケジュールクラス
    /// <summary>
    /// スケジュールクラス
    /// </summary>
    /// <typeparam name="T">スケジュールの型</typeparam>
    public class ScheduleModel<T> where T : class, new()
    {
        #region プロパティ
        /// <summary>ID</summary>
        public long ID { get; set; }

        /// <summary>カテゴリーID</summary>
        public long CategoryID { get; set; }

        /// <summary>開発符号</summary>
        public string GeneralCode { get; set; }

        /// <summary>行番号</summary>
        public int RowNo { get; set; }

        /// <summary>サブタイトル</summary>
        public string SubTitle { get; set; }

        /// <summary>ツールチップ</summary>
        public string ToolTipText { get; set; }

        /// <summary>ツールチップオブジェクト</summary>
        public ToolTip ToolTip { get; set; }

        /// <summary>開始日</summary>
        public DateTime? StartDate { get; set; }

        /// <summary>終了日</summary>
        public DateTime? EndDate { get; set; }

        /// <summary>登録日</summary>
        public DateTime? InputDate { get; set; }

        /// <summary>日数</summary>
        public int DayCount
        {
            get { return StartDate == null || EndDate == null ? 0 : (EndDate.Value.Date - StartDate.Value.Date).Days + 1; }
        }

        /// <summary>状況</summary>
        public int? Status { get; set; }

        /// <summary>クローズ可否</summary>
        public bool IsClose { get; set; }

        /// <summary>編集可否</summary>
        public bool IsEdit { get; set; }
        
        private bool? _IsResizeHandler = null;
        /// <summary>スケジュール選択ハンドラ表示有無</summary>
        public bool IsResizeHandler
        {
            get
            {
                if (_IsResizeHandler == null)
                {
                    return IsEdit;
                }
                else
                {
                    return _IsResizeHandler.Value;
                }
            }
            set
            {
                _IsResizeHandler = value;
            }
        }

        /// <summary>削除可否（特殊）</summary>
        public bool IsDelete { get; set; }

        /// <summary>スケジュール編集区分</summary>
        public ScheduleEditType ScheduleEdit { get; set; }

        /// <summary>スケジュール</summary>
        public T Schedule { get; set; }

        /// <summary>予約種別</summary>
        public ReservationStautsEnum ReservationStauts { get; private set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScheduleModel()
        {

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="categoryID">カテゴリーID</param>
        /// <param name="rowNo">行番号</param>
        /// <param name="subTitle">サブタイトル</param>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="inputDate">登録日</param>
        /// <param name="status">状況</param>
        /// <param name="isClose">クローズ可否</param>
        /// <param name="isEdit">編集可否</param>
        /// <param name="schedule">スケジュール</param>
        /// <param name="reservationStatus">予約種別</param>
        public ScheduleModel(long id, long categoryID, int rowNo, string subTitle, DateTime? startDate, DateTime? endDate, DateTime? inputDate, int? status, bool isClose, bool isEdit, T schedule, string reservationStatus = "")
            : this(id, categoryID, rowNo, subTitle, startDate, endDate, inputDate, status, isClose, isEdit, false, schedule, reservationStatus)
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="categoryID">カテゴリーID</param>
        /// <param name="rowNo">行番号</param>
        /// <param name="subTitle">サブタイトル</param>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="inputDate">登録日</param>
        /// <param name="status">状況</param>
        /// <param name="isClose">クローズ可否</param>
        /// <param name="isEdit">編集可否</param>
        /// <param name="isDelete">削除可否(特殊)</param>
        /// <param name="schedule">スケジュール</param>
        /// <param name="reservationStatus">予約種別</param>
        public ScheduleModel(long id, long categoryID, int rowNo, string subTitle, DateTime? startDate, DateTime? endDate, DateTime? inputDate, int? status, bool isClose, bool isEdit, bool isDelete, T schedule, string reservationStatus = "")
        {
            //ID
            this.ID = id;

            //カテゴリーID
            this.CategoryID = categoryID;

            //行番号
            this.RowNo = rowNo;

            //サブタイトル
            this.SubTitle = subTitle;

            //開始日
            this.StartDate = startDate;

            //終了日
            this.EndDate = endDate;

            //登録日
            this.InputDate = inputDate;

            //状況
            this.Status = status;

            //予約種別
            this.ReservationStauts = ReservationStautsEnum.KeyOf(reservationStatus);

            //スケジュール
            this.Schedule = schedule;

            //クローズ可否
            this.IsClose = isClose;

            //編集可否
            this.IsEdit = isEdit;

            //削除可否(特殊)
            this.IsDelete = isDelete;

            //スケジュール編集区分
            this.ScheduleEdit = ScheduleEditType.Update;

        }
        #endregion

        #region メソッド
        /// <summary>
        /// クローン
        /// </summary>
        /// <returns>ScheduleModel<T></returns>
        public ScheduleModel<T> Clone()
        {
            return JsonConvert.DeserializeObject<ScheduleModel<T>>(JsonConvert.SerializeObject(this));

        }
        #endregion

    }
    #endregion

    #region 行ヘッダクラス
    /// <summary>
    /// 行ヘッダクラス
    /// </summary>
    public class RowHeaderModel
    {
        #region プロパティ
        /// <summary>行ヘッダ幅（初期）</summary>
        public int Width { get; set; }

        /// <summary>行ヘッダ幅（最小）</summary>
        public int MinWidth { get; set; }

        /// <summary>行ヘッダ幅（最大）</summary>
        public int MaxWidth { get; set; }
        #endregion
    }
    #endregion

    #region その他スケジュールクラス
    /// <summary>
    /// その他スケジュールクラス
    /// </summary>
    /// <typeparam name="T">スケジュール項目の型</typeparam>
    public class OtherScheduleModel<T> where T : class, new()
    {
        #region プロパティ
        /// <summary>背景色</summary>
        public Color BackColor { get; set; }

        /// <summary>文字の色</summary>
        public Color ForeColor { get; set; }

        /// <summary>列数</summary>
        public int ColumnSpan { get; set; }

        /// <summary>値</summary>
        public string Value { get; set; }

        /// <summary>対象日を取得するデリゲート</summary>
        public Func<ScheduleItemModel<T>, DateTime?> GetTargetDay { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="backColor">背景色</param>
        /// <param name="foreColor">文字の色</param>
        /// <param name="columnSpan">列数</param>
        /// <param name="value">値</param>
        /// <param name="getTargetDay">対象日を取得するデリゲート</param>
        public OtherScheduleModel(Color backColor, Color foreColor, int columnSpan, string value, Func<ScheduleItemModel<T>, DateTime?> getTargetDay)
        {
            //背景色
            this.BackColor = backColor;

            //文字の色
            this.ForeColor = foreColor;

            //列数
            this.ColumnSpan = columnSpan;

            //値
            this.Value = value;

            //対象日取得
            this.GetTargetDay = getTargetDay;

        }
        #endregion
    }
    #endregion
}
