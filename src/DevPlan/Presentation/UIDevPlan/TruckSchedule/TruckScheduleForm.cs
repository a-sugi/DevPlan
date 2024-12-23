using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC;
using DevPlan.Presentation.UIDevPlan.TruckSchedule.Reports;
using DevPlan.Presentation.UIDevPlan.TruckSchedule.ToolTips;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar;
using DevPlan.UICommon.Utils.Calendar.Templates;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule
{
    /// <summary>
    /// トラック予約画面
    /// </summary>
    public partial class TruckScheduleForm : BaseForm
    {
        /// <summary>フォームタイトル名</summary>
        public override string FormTitle { get { return "トラック予約"; } }

        /// <summary>カレンダーグリッドオブジェクト</summary>
        private CalendarGridUtil<TruckScheduleItemModel, TruckScheduleModel> gridUtil;

        /// <summary>スケジュール項目リスト</summary>
        private List<TruckScheduleItemModel> ScheduleItemList { get; set; }

        /// <summary>スケジュールリスト</summary>
        private List<TruckScheduleModel> ScheduleList { get; set; }
        
        /// <summary>定期便発着地リスト</summary>
        /// <remarks>カレンダー情報を更新するたびにリセットされます。</remarks>
        internal static List<DeparturePoint> departurePointList { get; private set; }

        /// <summary>同期をとるカレンダーグリッド</summary>
        public GcCalendarGrid SyncCalendarGrid { get; internal set; }

        /// <summary>同期をとるカレンダースタイル</summary>
        public StringCollection SyncCalendarStyle { get; internal set; }

        /// <summary>カレンダーの同期をとる場合True</summary>
        public bool SyncCheck { get; internal set; }

        /// <summary>カレンダー同期開始フラグ</summary>
        public bool SyncFlag { get; set; }

        /// <summary>CKリスト</summary>
        private List<TruckScheduleModel> ckList;

        /// <summary>定時間日の設定情報</summary>
        private List<FixedTimeDaySettingModel> fixList;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>
        /// 管理者かどうか
        /// </summary>
        private bool adminUser;

        private Func<DateTime?, string, DateTime?> getDateTime = (dt, time) => dt == null ? null : (DateTime?)dt.Value.AddHours(int.Parse(time == string.Empty ? "0" : time));

        /// <summary>運休コメント</summary>
        private string HolidayComment = string.Empty;

        //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
        /// <summary>カーシェアスケジュール検索条件</summary>
        public TruckScheduleItemSearchModel TruckScheduleItemSearchCond { get; set; }

        /// <summary>
        /// カレンダーコントロールの左側へ設定したい日付（デフォルトはシステム日付）
        private DateTime? _calendarFirstDate = null;
        /// </summary>
        public DateTime CalendarFirstDate
        {
            private get
            {
                return (_calendarFirstDate == null) ? DateTime.Now.Date : _calendarFirstDate.Value;
            }
            set
            {
                _calendarFirstDate = value;
            }
        }

        /// <summary>
        /// カレンダーコントロールの一番上へ設定したい項目（ID）
        /// </summary>
        public long? CalendarCategoryId { get; internal set; }
        //Append End 2022/01/11 杉浦 トラック予約一覧を追加

        /// <summary>
        /// 現在カレンダーへ設定されているヘッダテキストリスト
        /// </summary>
        private Dictionary<CalendarScheduleColorEnum, string[]> calendarHeaderTextList;
        
        /// <summary>選択可能スケジュール最大日</summary>
        public DateTime SelectMaxDate { private get; set; }

        /// <summary>選択可能スケジュール最小日</summary>
        public DateTime SelectMinDate { private get; set; }

        /// <summary>
        /// 連携フォームリフレッシュイベントデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SyncFormRefreshEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 連携フォームリフレッシュイベント
        /// </summary>
        public event SyncFormRefreshEventHandler SyncFormRefreshEvent;

        /// <summary>
        /// 連携フォーム更新イベントデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void SyncFormStyleSaveEventHandler(object sender, EventArgs e);

        /// <summary>
        /// 連携フォーム更新イベント
        /// </summary>
        public event SyncFormStyleSaveEventHandler SyncFormStyleSaveEvent;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TruckScheduleForm()
        {
            InitializeComponent();   
        }
        
        /// <summary>
        /// フォームロード処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleForm_Load(object sender, EventArgs e)
        {
            this.UserAuthority = base.GetFunction(FunctionID.Truck);
            adminUser = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            //コメントデータ
            var commentRes = HttpUtil.GetResponse<TruckCommentModel>(ControllerType.TruckComment);
            var commentList = new List<TruckCommentModel>();
            if (commentRes != null && commentRes.Status == Const.StatusSuccess)
            {
                commentList.AddRange(commentRes.Results);
                var d = commentList.Where(x => x.コメント種別 == "定期便休止時間帯").FirstOrDefault();
                if (d != null)
                {
                    HolidayComment = d.タイトル + " " + d.コメント;
                }
            }
            
            this.Shown += TruckScheduleForm_Shown;

            this.EmptyStartDayDateTimePicker.Value = null;
            this.EmptyStartTimeComboBox.SelectedIndex = 0;
            this.EmptyEndDayDateTimePicker.Value = null;
            this.EmptyEndTimeComboBox.SelectedIndex = this.EmptyEndTimeComboBox.Items.Count - 1;

            this.YoyakuTableLayoutPanel.Visible = adminUser;
            ReservationAvailableCheckBox.Checked = true;
            ReservationsNotAcceptedCheckBox.Checked = false;

            #region 右クリックメニュー設定（固定）

            var cornerHeaderMenu = new Dictionary<string, Action>();
            if (adminUser)
            {
                cornerHeaderMenu["管理者変更"] = () => ChangeManagementUser();
                cornerHeaderMenu["項目追加"] = () => OpenScheduleItemForm(new ScheduleItemModel<TruckScheduleItemModel>(), ScheduleItemEditType.Insert);
            }
            var rowHeaderMenu = new Dictionary<string, Action<ScheduleItemModel<TruckScheduleItemModel>>>();
            if (adminUser)
            {
                rowHeaderMenu["項目追加"] = (item => OpenScheduleItemForm(item, ScheduleItemEditType.Insert));//ソート順連携が必要なのでnewではない
                rowHeaderMenu["項目編集"] = (item => OpenScheduleItemForm(item, ScheduleItemEditType.Update));
                rowHeaderMenu["項目削除"] = item => DeleteScheduleItem(item);
            }

            var scheduleMenu = new Dictionary<string, Action<ScheduleModel<TruckScheduleModel>>>();
            scheduleMenu["予約解除"] = item => DeleteSchedule(item);

            #endregion

            var config =
                new CalendarGridConfigModel<TruckScheduleItemModel, TruckScheduleModel>(
                    this.TruckScheduleCalendarGrid,
                    adminUser,
                    isUpdate,
                    cornerHeaderMenu,
                    rowHeaderMenu,
                    scheduleMenu);

            config.CalendarSettings =
                (this.SyncCalendarStyle != null) ?
                new CalendarSettings(this.SyncCalendarStyle) : new CalendarSettings(Properties.Settings.Default.TrackCalendarStyle);

            config.MouseWheelCount = 45;           
            config.GetRowHeaderContexMenuItems = (item) =>
            {
                #region スケジュール行ヘッダーのコンテキストメニューの項目を取得するデリゲート

                var list = new List<ToolStripItem>();
                
                foreach (var kv in rowHeaderMenu)
                {
                    var menu = new ToolStripMenuItem { Text = kv.Key };
                    menu.Click += (sender2, e2) => kv.Value?.Invoke(item);                    
                    list.Add(menu);
                }
                
                if (item.ScheduleItem.FLAG_定期便 == 1 && adminUser)
                {
                    list.Add(new ToolStripSeparator());
                    var menu = new ToolStripMenuItem { Text = "メール原文修正" };
                    menu.Click += (sender2, e2) => {
                        using (var form = new RegularMailDetailForm() { ScheduleItem = item })
                        {
                            if (form.ShowDialog(this) == DialogResult.OK)
                            {

                            }
                        }
                    };
                    list.Add(menu);
                    list.Add(new ToolStripSeparator());
                }

                return list.ToArray();

                #endregion
            };
            
            //スケジュール表示期間変更可否
            config.IsScheduleViewPeriodChange = adminUser;
            config.IsScheduleViewPeriodChangeMessage = Resources.KKM01010;

            Action<NullableDateTimePicker> setDataMinMax = dtp =>
            {
                //管理権限がなければ表示期間内のみの入力制限を設定
                if (adminUser == false)
                {
                    //当月を含む３ヶ月のみ予約が可能。カレンダー共通にも同様の制限が入っている。
                    DateTime toDay = DateTime.Today;
                    DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
                    DateTime end = start.AddMonths(3).AddDays(-1);

                    dtp.MinDate = start;
                    dtp.MaxDate = end;
                }
            };
            setDataMinMax(this.EmptyEndDayDateTimePicker);
            setDataMinMax(this.EmptyStartDayDateTimePicker);

            config.GetContentContexMenuItems = (schedule) =>
            {
                #region スケジュールコンテキストメニュー再設定

                var list = new List<ToolStripItem>();

                foreach (var kv in scheduleMenu)
                {
                    var menu = new ToolStripMenuItem { Text = kv.Key };
                    menu.Click += (sender2, e2) => kv.Value?.Invoke(schedule);
                    list.Add(menu);
                }

                if (schedule.Schedule.FLAG_定期便 == 1)
                {
                    ToolStripMenuItem menu;
                    if (adminUser)
                    {
                        menu = new ToolStripMenuItem { Text = "定期便予約確認メール" };
                        menu.Click += (sender2, e2) => {
                            OpenRegularMailForm(schedule);
                        };
                        list.Add(menu);

                        menu = new ToolStripMenuItem { Text = "搬送依頼書印刷" };
                        menu.Click += (sender2, e2) => {
                            PrintTransportRequest(schedule);
                        };
                        list.Add(menu);
                    }

                    menu = new ToolStripMenuItem { Text = "送り状印刷" };
                    menu.Click += (sender2, e2) => {
                        PrintInvoice(schedule);
                    };
                    if (schedule.Schedule.FLAG_仮予約 == 1)
                    {
                        menu.Enabled = false;
                    }
                    list.Add(menu);
                }

                return list.ToArray();

                #endregion
            };

            config.ScheduleDoubleClickLabelText = "";
            config.IsRowHeaderSysFilter = false;
            config.IsSortContextMenuVisible = false;

            //カレンダーグリッドの初期設定
            this.gridUtil = new CalendarGridUtil<TruckScheduleItemModel, TruckScheduleModel>(config);
            
            //スケジュール項目の背景色を取得するデリゲート
            config.GetScheduleItemBackColor = x => SetScheduleItemBackColor(x.ScheduleItem);

            //Update Start 2021/10/15 杉浦 トラック予約_トラック予約表の予約可能期間を変更したい
            config.ViewRange = 4;
            //Update End 2021/10/15 杉浦 トラック予約_トラック予約表の予約可能期間を変更したい

            //スケジュール表示期間の変更後
            this.gridUtil.ScheduleViewPeriodChangedAfter += (start, end) => FormControlUtil.FormWait(this, () => this.SetSchedule(start, end));

            //スケジュール項目の並び順変更後のデリゲート
            this.gridUtil.ScheduleItemSortChangedAfter += (sourceItem, destItem) => FormControlUtil.FormWait(this, () => this.UpdateScheduleItemSort(sourceItem, destItem));

            //スケジュールのシングルクリック
            this.gridUtil.ScheduleSingleClick += (schedule, mouseButtons) => this.ScheduleSingleClick(schedule, mouseButtons);

            //スケジュールのダブルクリック
            this.gridUtil.ScheduleDoubleClick += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Update);

            //スケジュールの日付範囲の変更後のデリゲート
            this.gridUtil.ScheduleDayRangeChangedAfter += schedule => FormControlUtil.FormWait(this, () => this.RangeChangedSchedule(schedule));
            
            //スケジュールの空白領域をドラッグ後のデリゲート
            this.gridUtil.ScheduleEmptyDragAfter += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Insert);
            
            //スケジュール空白領域をダブルクリックのデリゲート
            this.gridUtil.ScheduleEmptyDoubleClick += schedule => this.ShowScheduleDetailForm(schedule, ScheduleEditType.Insert);

            //スケジュール削除のデリゲート
            this.gridUtil.ScheduleDelete += schedule => FormControlUtil.FormWait(this, () => this.DeleteSchedule(schedule));

            //スケジュール貼り付けのデリゲート
            this.gridUtil.SchedulePaste += (copySchedule, schedule) => FormControlUtil.FormWait(this, () => this.PasteSchedule(copySchedule, schedule));

            //カレンダーレイアウト状態保存デリゲート
            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;

            //カレンダーテンプレート描画処理デリゲート
            this.gridUtil.CalendarCellPaint += (editCell, scheduleModel, colIndex, rowIndex) => this.CalendarCellPaint(editCell, scheduleModel, colIndex, rowIndex);

            //カレンダー項目描画後処理デリゲート
            this.gridUtil.CalendarScheduleCellPaint += (item) => this.CalendarScheduleCellPaint(item);

            config.GetScheduleBackColor = (schedule, colorEnum) =>
            {
                #region スケジュールの背景色取得
                      
                if (schedule.Schedule.FLAG_定期便 == 1)
                {
                    colorEnum = CalendarScheduleColorEnum.GetValues(schedule.Schedule.ItemColor, schedule.Schedule.FontColor);
                }
                else
                {
                    colorEnum = CalendarScheduleColorEnum.DefaultColor;
                }

                return colorEnum;
                #endregion
            };

            //カレンダーの表示期間変更
            DateTime date;
            if (this.SyncCalendarGrid != null)
            {
                date = this.SyncCalendarGrid.FirstDateInView;
            }
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            else if (this.TruckScheduleItemSearchCond != null)
            {
                date = this.TruckScheduleItemSearchCond.EMPTY_START_DATE.Value;
            }
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
            else
            {
                date = DateTime.Now;                
            }
            this.gridUtil.SetCalendarViewPeriod(new DateTime(date.Year, date.Month, 1));

            //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
            //SetSchedule(syncGridUpdate: false);
            //SetScheduleMostDayFirst();
            //検索条件があるかどうか
            if (this.TruckScheduleItemSearchCond != null)
            {
                //スケジュール一覧設定
                SetSchedule(syncGridUpdate: false, itemCond: TruckScheduleItemSearchCond);
            }else
            {
                SetSchedule(syncGridUpdate: false);
            }
            if (this.CalendarCategoryId != null)
            {
                this.gridUtil.SetScheduleRowHeaderFirst(this.CalendarCategoryId);
                this.gridUtil.SetScheduleMostDayFirst(this.CalendarFirstDate);

                this.CalendarCategoryId = null;
            }else
            {
                SetScheduleMostDayFirst();
            }
            //Update End 2022/01/11 杉浦 トラック予約一覧を追加

            this.gridUtil.UndoRedo += (schedule) => this.UndoRedo(schedule);
        }

        /// <summary>
        /// カレンダーグリッドスケジュールシングルクリック処理
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <param name="mouseButtons">マウスボタン</param>
        private void ScheduleSingleClick(ScheduleModel<TruckScheduleModel> schedule, MouseButtons mouseButtons)
        {
            if (mouseButtons == MouseButtons.Left)
            {
                var state = UserInfo.CheckScheduleEdit(schedule.Schedule, this.UserAuthority);
                if (state.IsEdit == false)
                {
                    schedule.IsEdit = false;
                }
            }
            else
            {
                schedule.IsEdit = true;
            }
        }

        /// <summary>
        /// マウスホイール処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // TODO : まずトラックのみ。
            //当フォーム(Formオブジェクト)はマウスホイール禁止とする
            return;
        }

        /// <summary>
        /// 項目順番入れ替え処理
        /// </summary>
        /// <param name="sourceItem"></param>
        /// <param name="destItem"></param>
        private void UpdateScheduleItemSort(ScheduleItemModel<TruckScheduleItemModel> sourceItem, ScheduleItemModel<TruckScheduleItemModel> destItem)
        {
            List<TruckScheduleItemModel> updateList;
            if ((sourceItem.ScheduleItem.FLAG_定期便 == 1 || destItem.ScheduleItem.FLAG_定期便 == 1) && 
                sourceItem.ScheduleItem.REGULAR_TIME_ID != destItem.ScheduleItem.REGULAR_TIME_ID)
            {
                double startSortNo = 0;
                List<TruckScheduleItemModel> itemList;
                if (sourceItem.SortNo > destItem.SortNo)
                {
                    itemList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                    {
                        MinSortNo = (int)sourceItem.SortNo,
                        IsRegular = true
                    });
                    if (itemList.Any())
                    {
                        startSortNo = itemList.Max(x => x.SORT_NO);
                    }
                }
                else
                {
                    itemList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                    {
                        MaxSortNo = (int)sourceItem.SortNo,
                        IsRegular = true
                    });
                    if (itemList.Any())
                    {
                        startSortNo = itemList.Min(x => x.SORT_NO) - 1;
                    }
                }

                if (sourceItem.ScheduleItem.FLAG_定期便 == 1)
                {
                    updateList = this.GetScheduleItem(new TruckScheduleItemSearchModel()
                    {
                        REGULAR_TIME_ID = sourceItem.ScheduleItem.REGULAR_TIME_ID
                    });
                }
                else
                {
                    updateList = new List<TruckScheduleItemModel>();
                    updateList.Add(sourceItem.ScheduleItem);
                }

                foreach (var item in updateList.OrderBy(x => x.SORT_NO))
                {
                    item.SORT_NO = startSortNo + 0.1D;
                    startSortNo += 0.1D;
                }
            }
            else
            {
                sourceItem.ScheduleItem.SORT_NO = sourceItem.SortNo;

                updateList = new List<TruckScheduleItemModel>();
                updateList.Add(sourceItem.ScheduleItem);
            }

            var res = HttpUtil.PutResponse(ControllerType.TruckScheduleItem, updateList.ToArray());           
            if (res != null && res.Status == Const.StatusSuccess)
            {
                this.MessageLabel.Text = Resources.KKM00002;
                SetSchedule();
            }
        }

        /// <summary>
        /// スケジュール貼り付け処理
        /// </summary>
        /// <param name="copySchedule">コピー元スケジュール</param>
        /// <param name="schedule">コピー先スケジュール</param>
        private void PasteSchedule(ScheduleModel<TruckScheduleModel> copySchedule, ScheduleModel<TruckScheduleModel> schedule)
        {
            if (this.ScheduleItemList.Where(x => x.ID == schedule.CategoryID).First().FLAG_定期便 != copySchedule.Schedule.FLAG_定期便 ||
                (copySchedule.Schedule.FLAG_定期便 == 1 && copySchedule.CategoryID != schedule.CategoryID))
            {
                SetSchedule();
                return;
            }

            schedule.ScheduleEdit = ScheduleEditType.Paste;

            if (schedule.Schedule.FLAG_定期便 == 1)
            {
                if (this.gridUtil.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.EXPANSION1)
                {
                    SetSchedule();
                    return;
                }
                UpdateRegTime(schedule, copySchedule);
            }

            if (this.IsEntrySchedule(schedule) == false)
            {
                SetSchedule();
                return;
            }

            var departureTime = departurePointList.Where(x =>
                x.TruckId == schedule.CategoryID &&
                x.Key == Expansion2CalendarTemplate.timeHeaderList.IndexOf(schedule.StartDate.Value.Hour)).FirstOrDefault();

            var model = new TruckScheduleModel
            {
                トラック_ID = schedule.CategoryID,
                FLAG_定期便 = schedule.Schedule.FLAG_定期便,
                FLAG_機密車 = schedule.Schedule.FLAG_機密車,
                PARALLEL_INDEX_GROUP = schedule.RowNo,
                SectionList = schedule.Schedule.SectionList,
                ShipperRecipientUserList = schedule.Schedule.ShipperRecipientUserList,
                予約終了時間 = schedule.EndDate,
                予約者_ID = schedule.Schedule.予約者_ID,
                予約者名 = schedule.Schedule.予約者名,
                予約開始時間 = schedule.StartDate,
                使用目的 = schedule.Schedule.使用目的,
                備考 = schedule.Schedule.備考,
                定期便依頼者_ID = schedule.Schedule.定期便依頼者_ID,
                定期便依頼者_TEL = schedule.Schedule.定期便依頼者_TEL,
                定期便依頼者名 = schedule.Schedule.定期便依頼者名,
                搬送車両名 = schedule.Schedule.搬送車両名,
                空き時間状況 = schedule.Schedule.空き時間状況,
                運転者A_ID = schedule.Schedule.運転者A_ID,
                運転者A_TEL = schedule.Schedule.運転者A_TEL,
                運転者A名 = schedule.Schedule.運転者A名,
                運転者B_ID = schedule.Schedule.運転者B_ID,
                運転者B_TEL = schedule.Schedule.運転者B_TEL,
                運転者B名 = schedule.Schedule.運転者B名,

                予約修正日時 = DateTime.Now,
                修正者名 = UserInfo.GetUserSectionFullName(SessionDto.SectionCode, SessionDto.UserName),
                修正者_ID = SessionDto.UserId,

                DEPARTURE_TIME = departureTime?.DepartureTime               
            };

            if (model.FLAG_定期便 == 1)
            {
                model.FLAG_仮予約 = 1;
            }

            schedule.Schedule = model;

            var res = HttpUtil.PostResponse(ControllerType.TruckSchedule, new[] { model });
            if (res != null && res.Status == Const.StatusSuccess)
            {
                SetMessage(schedule);
                SetSchedule();

                schedule.Schedule = res.Results.OfType<TruckScheduleModel>().FirstOrDefault();
                this.gridUtil.UndoRedoManager.Do(schedule);
            }
        }

        /// <summary>
        /// 定期便予約データ補正処理。
        /// </summary>
        /// <remarks>
        /// 標準の場合は変更前の開始・終了時間をそのまま利用します。
        /// 標準以外の場合は変更前の開始・終了時間に＋１時間を行います。
        /// </remarks>
        /// <param name="updateSchedule">更新対象スケジュール</param>
        /// <param name="orgSchedule">更新の元となるスケジュール</param>
        private void UpdateRegTime(ScheduleModel<TruckScheduleModel> updateSchedule, ScheduleModel<TruckScheduleModel> orgSchedule)
        {
            if (this.gridUtil.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.DEFAULT)
            {
                if((orgSchedule.EndDate.Value.Hour - orgSchedule.StartDate.Value.Hour) != 1)
                {
                    updateSchedule.StartDate = updateSchedule.StartDate.Value.Date.AddHours(orgSchedule.StartDate.Value.Hour);
                    updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours(orgSchedule.EndDate.Value.Hour);
                }else
                {
                    updateSchedule.StartDate = updateSchedule.StartDate.Value.Date.AddHours(orgSchedule.StartDate.Value.Hour);
                    updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours(orgSchedule.EndDate.Value.Hour + 1);
                }

            }
            else
            {
                if (Expansion2CalendarTemplate.timeHeaderList.IndexOf(updateSchedule.StartDate.Value.Hour) > 0)
                {
                    return;
                }
                if((updateSchedule.EndDate.Value.Hour - updateSchedule.StartDate.Value.Hour) != 1)
                {
                    updateSchedule.StartDate = updateSchedule.StartDate.Value.Date.AddHours((updateSchedule.StartDate.Value.Hour + 1));
                    if(updateSchedule.StartDate.Value.Hour != 22)
                    {
                        updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours((updateSchedule.EndDate.Value.Hour + 1));
                    }else
                    {
                        updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours((updateSchedule.EndDate.Value.Hour - 1));
                    }
                }else
                {
                    updateSchedule.StartDate = updateSchedule.StartDate.Value.Date.AddHours((updateSchedule.StartDate.Value.Hour + 1));
                    if (updateSchedule.StartDate.Value.Hour != 22)
                    {
                        updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours((updateSchedule.EndDate.Value.Hour + 2));
                    }else
                    {
                        updateSchedule.EndDate = updateSchedule.EndDate.Value.Date.AddHours((updateSchedule.EndDate.Value.Hour + 1));
                    }
                }

            }
        }

        /// <summary>
        /// トラック予約フォーム表示後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleForm_Shown(object sender, EventArgs e)
        {
            if (this.SyncCalendarGrid != null)
            {
                int h = Screen.GetWorkingArea(this).Height;
                int w = Screen.GetWorkingArea(this).Width;

                int top = 0;
                int bnd = h / (3 - 1);

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm != this && frm != Owner) continue;

                    var bfrm = frm as BaseForm;

                    bfrm.WindowState = FormWindowState.Normal;
                    bfrm.Width = w;
                    bfrm.Height = bnd >= bfrm.FormMinHeight ? bnd : bfrm.FormMinHeight;
                    bfrm.Top = top;
                    bfrm.Left = 0;
                    top += bnd;
                    frm.Activate();
                }
                this.ScrollControlIntoView(this.TruckScheduleCalendarGrid);
                this.SyncFlag = true;

                if (this.SearchConditionTableLayoutPanel.Visible == true)
                {
                    this.SearchConditionButton_Click(null, null);
                }
            }
        }
        
        /// <summary>
        /// カレンダー水平スクロールバー変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TruckScheduleCalendarGrid_HorizontalScrollBarOffsetChanged(object sender, EventArgs e)
        {
            if (IsTruckSync())
            {
                if (this.SyncCalendarGrid.HorizontalScrollBarOffset != this.TruckScheduleCalendarGrid.HorizontalScrollBarOffset)
                {
                    this.SyncCalendarGrid.HorizontalScrollBarOffset = this.TruckScheduleCalendarGrid.HorizontalScrollBarOffset;
                }
            }
        }

        /// <summary>
        /// トラックカレンダーグリッドイベント削除
        /// </summary>
        public void TruckCalendarGridEventDelete()
        {
            this.TruckScheduleCalendarGrid.HorizontalScrollBarOffsetChanged -= TruckScheduleCalendarGrid_HorizontalScrollBarOffsetChanged;
        }

        /// <summary>
        /// トラックカレンダーグリッドイベント追加
        /// </summary>
        public void TruckCalendarGridEventAdd()
        {
            this.TruckScheduleCalendarGrid.HorizontalScrollBarOffsetChanged += TruckScheduleCalendarGrid_HorizontalScrollBarOffsetChanged;
        }

        /// <summary>
        /// トラック予約カレンダーグリッド同期判定処理
        /// </summary>
        /// <returns></returns>
        private bool IsTruckSync()
        {
            return this.SyncCalendarGrid != null && SyncCheck == true && SyncFlag;
        }
        
        /// <summary>
        /// カレンダーグリッドスケジュールセル描画処理
        /// </summary>
        /// <param name="model">スケジュール項目データ</param>
        private void CalendarScheduleCellPaint(ScheduleItemModel<TruckScheduleItemModel> model)
        {
            var item = model.ScheduleItem;
            if (item.FLAG_CHECK == 1 && this.TruckScheduleCalendarGrid.Template.GetType() == typeof(Expansion2CalendarTemplate))
            {
                foreach (var ck in ckList.Where(x => x.トラック_ID == item.ID))
                {
                    foreach (var row in this.TruckScheduleCalendarGrid[ck.予約開始時間.Value.Date].Rows)
                    {
                        var modelData = (ScheduleItemModel<TruckScheduleItemModel>)row.Tag;
                        if (modelData.ScheduleItem.ID != item.ID)
                        {
                            continue;
                        }

                        var cell = row.Cells[0];

                        cell.CellType = new CalendarLabelCellType();
                        cell.Value = "CK";

                        cell.CellStyle.ForeColor = Color.Gray;
                        cell.CellStyle.BackColor = CalendarScheduleColorEnum.HolidayColor.MainColor;
                        cell.CellStyle.TextAdjustment = CalendarTextAdjustment.Center;
                    }
                }
            }

            //定期便じゃない場合は除外。
            if (item.FLAG_定期便 != 1 ||
                this.TruckScheduleCalendarGrid.Template.GetType() != typeof(Expansion2CalendarTemplate)) { return; }

            //運休リストを取得する。
            var unkyulist = fixList.Where(x =>
                                x.トラック_ID == item.ID &&
                                (x.時間帯 != null || x.FLAG_運休日 == 1));

            if (unkyulist.Count() > 0)
            {
                foreach (var data in unkyulist)
                {
                    foreach (var row in this.TruckScheduleCalendarGrid[data.DATE].Rows)
                    {
                        var modelData = (ScheduleItemModel<TruckScheduleItemModel>)row.Tag;
                        if (modelData.ScheduleItem.ID != item.ID)
                        {
                            continue;
                        }

                        if (data.TimeList.Any())
                        {
                            foreach (var jikan in data.TimeList)
                            {
                                if (jikan != "" && Expansion2CalendarTemplate.timeHeaderList.Contains(int.Parse(jikan)))
                                {
                                    var cell = row.Cells[Expansion2CalendarTemplate.timeHeaderList.IndexOf(int.Parse(jikan))];

                                    if (cell.CellType.GetType() != typeof(CalendarAppointmentCellType))
                                    {
                                        cell.CellStyle.BackColor = CalendarScheduleColorEnum.HolidayColor.MainColor;
                                        cell.Value = null;
                                        cell.ToolTipText = HolidayComment;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var cell in row.Cells)
                            {
                                if (cell.CellType.GetType() != typeof(CalendarAppointmentCellType))
                                {
                                    cell.CellStyle.BackColor = CalendarScheduleColorEnum.HolidayColor.MainColor;
                                    cell.Value = null;
                                    cell.ToolTipText = HolidayComment;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// （カレンダー）日付範囲変更処理（移動）
        /// </summary>
        /// <param name="schedule"></param>
        private void RangeChangedSchedule(ScheduleModel<TruckScheduleModel> schedule)
        {
            schedule.ScheduleEdit = ScheduleEditType.Update;

            if (schedule.Schedule.FLAG_定期便 == 1)
            {
            
                if (this.gridUtil.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.EXPANSION1 ||
                   ( (schedule.EndDate.Value - schedule.StartDate.Value).Hours < 1 || 
                   (schedule.EndDate.Value - schedule.StartDate.Value).Hours > 2))
                {
                    SetSchedule();
                    return;
                }

                UpdateRegTime(schedule, schedule);
            }
            
            if (this.IsEntrySchedule(schedule) == false)
            {
                SetSchedule();
                return;
            }

            var list = new[] { schedule.Schedule };

            schedule.Schedule.PARALLEL_INDEX_GROUP = schedule.RowNo;
            schedule.Schedule.予約開始時間 = schedule.StartDate;
            schedule.Schedule.予約終了時間 = schedule.EndDate;

            var departureTime = departurePointList.Where(x =>
               x.TruckId == schedule.CategoryID &&
               x.Key == Expansion2CalendarTemplate.timeHeaderList.IndexOf(schedule.StartDate.Value.Hour)).FirstOrDefault();
            schedule.Schedule.DEPARTURE_TIME = departureTime?.DepartureTime;
            
            var res = HttpUtil.PutResponse(ControllerType.TruckSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                SetMessage(schedule);
                SetSchedule();
            }
        }

        /// <summary>
        /// 日程表状態保存処理
        /// </summary>
        /// <param name="style"></param>
        private void SaveCalendarUserData(StringCollection style)
        {
            if (IsTruckSync())
            {                
                this.SyncCalendarStyle = style;
                this.SyncFormStyleSaveEvent(style, new EventArgs());
            }

            if (this.SyncCalendarGrid == null)
            {
                Properties.Settings.Default.TrackCalendarStyle = style;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 登録完了メッセージ設定
        /// </summary>
        /// <param name="schedule"></param>
        private void SetMessage(ScheduleModel<TruckScheduleModel> schedule)
        {
            if (schedule.Schedule.FLAG_定期便 == 1)
            {
                this.MessageLabel.Text = string.Format("定期便 {0}発のスケジュールを登録しました。", schedule.Schedule.DEPARTURE_TIME);
            }
            else
            {
                Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
                this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(schedule.Schedule.予約開始時間), getYMDH(schedule.Schedule.予約終了時間), Resources.KKM00002);
            }
        }

        /// <summary>
        /// トラック予約スケジュール削除処理
        /// </summary>
        /// <param name="item"></param>
        private void DeleteSchedule(ScheduleModel<TruckScheduleModel> item)
        {
            string message = "";

            var status = UserInfo.CheckScheduleEdit(item.Schedule, this.UserAuthority);

            if (item.Schedule.FLAG_定期便 != 1)
            {
                if (status.IsDelete == false)
                {
                    Messenger.Info(Resources.KKM01015);
                    return;
                }
                else
                {
                    message = string.Format(Resources.KKM02007,
                        item.Schedule.車両名,
                        item.Schedule.予約開始時間.Value.ToString("M/d（ddd）HH:ss"),
                        item.Schedule.予約終了時間.Value.ToString("M/d（ddd）HH:ss")
                        );
                }
            }
            else
            {
                if (status.IsDelete == false)
                {
                    if (item.Schedule.FLAG_仮予約 != 1)
                    {
                        UserInfo.SetTruckManageUser();
                        Messenger.Info(string.Format(Resources.KKM01013, UserInfo.UserName, UserInfo.UserTel));
                        return;
                    }
                    else
                    {
                        Messenger.Info(Resources.KKM01014);
                        return;
                    }
                }
                else
                {
                    message = string.Format(Resources.KKM02006,
                        item.Schedule.車両名,
                        item.Schedule.DEPARTURE_TIME,
                        departurePointList.
                        Where(x => x.TruckId == item.Schedule.トラック_ID && x.DepartureTime == item.Schedule.DEPARTURE_TIME).First().Text4
                        );
                }
            }

            if (MessageBox.Show(
                   message, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            
            item.ScheduleEdit = ScheduleEditType.Delete;
            if (this.IsEntrySchedule(item) == false)
            {
                SetSchedule();
                return;
            }
            
            var list = new[] { item.Schedule };

            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.TruckSchedule, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                if (item.Schedule.FLAG_定期便 == 1)
                {
                    this.MessageLabel.Text = string.Format("定期便 {0}発のスケジュールを削除しました。", item.Schedule.DEPARTURE_TIME);
                }
                else
                {
                    Func<DateTime?, string> getYMDH = dt => { return dt != null ? ((DateTime)dt).ToString("yyyy/MM/dd H時") : string.Empty; };
                    this.MessageLabel.Text = string.Format(Resources.KKM01019, getYMDH(item.Schedule.予約開始時間), getYMDH(item.Schedule.予約終了時間), Resources.KKM00003);
                }

                this.gridUtil.UndoRedoManager.Do(item);
                SetSchedule();
            }
        }

        /// <summary>
        /// トラック予約項目削除処理
        /// </summary>
        /// <param name="item"></param>
        private void DeleteScheduleItem(ScheduleItemModel<TruckScheduleItemModel> item)
        {
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;
            }
            
            item.ScheduleItemEdit = ScheduleItemEditType.Delete;
            if (this.IsEntryScheduleItem(item) == false)
            {
                SetSchedule();
                return;
            }

            var list = new[] { item.ScheduleItem };

            var res = HttpUtil.DeleteResponse(ControllerType.TruckScheduleItem, list);
            if (res != null && res.Status == Const.StatusSuccess)
            {
                Messenger.Info(Resources.KKM00003);
                SetSchedule();
            }
        }

        /// <summary>
        /// スケジュールの入力チェック
        /// </summary>
        /// <param name="schedule">スケジュール</param>
        /// <returns></returns>
        private bool IsEntrySchedule(ScheduleModel<TruckScheduleModel> schedule)
        {
            var itemList = GetScheduleItem(new TruckScheduleItemSearchModel() { ID = schedule.CategoryID });
            if (IsExistItemSchedule(itemList, schedule) == false)
            {
                return false;
            }

            var status = UserInfo.CheckScheduleEdit(schedule.Schedule, this.UserAuthority);

            if (status.IsEdit == false && schedule.ScheduleEdit != ScheduleEditType.Delete)
            {
                return false;
            }

            var item = itemList.FirstOrDefault();
            if (item.FLAG_定期便 == 1 && schedule.ScheduleEdit != ScheduleEditType.Delete)
            {
                if (TruckData.CheckRegSchedule(departurePointList, fixList, this.gridUtil.CalendarSetting.CalendarMode, schedule, adminUser) == false)
                {
                    return false;
                }
            }

            if (item.FLAG_定期便 == 1 && schedule.ScheduleEdit != ScheduleEditType.Delete)
            {
                TruckData.IsZangyo(departurePointList, schedule, schedule.StartDate.Value.Hour, this.UserAuthority);
            }

            return true;
        }
        
        /// <summary>
        /// スケジュール項目有無チェック
        /// </summary>
        /// <returns></returns>
        private bool IsExistItemSchedule(List<TruckScheduleItemModel> itemList, ScheduleModel<TruckScheduleModel> schedule)
        {
            if (itemList == null || itemList.Any() == false)
            {
                Messenger.Warn(Resources.KKM00021);
                return false;
            }

            #region 変更対象スケジュールの有無チェック

            switch (schedule.ScheduleEdit)
            {
                case ScheduleEditType.Update:
                case ScheduleEditType.Delete:

                    var checkList = TruckData.GetSchedule(new TruckScheduleSearchModel()
                    {
                        ID = schedule.Schedule.ID
                    });

                    if (checkList == null || checkList.Any() == false)
                    {
                        Messenger.Warn(Resources.KKM00021);
                        return false;
                    }
                    else
                    {
                        schedule.Schedule = checkList.First();
                    }
                    break;
            }

            #endregion

            #region 重複チェック

            //スケジュール編集区分ごとの分岐
            switch (schedule.ScheduleEdit)
            {
                case ScheduleEditType.Insert:
                case ScheduleEditType.Update:
                case ScheduleEditType.Paste:

                    var date = GetChangeDateTime();
                    if (adminUser)
                    {
                        this.SelectMaxDate = this.TruckScheduleCalendarGrid.MaxDate;
                        this.SelectMinDate = this.TruckScheduleCalendarGrid.MinDate;
                    }
                    else
                    {
                        this.SelectMaxDate = date[1];
                        this.SelectMinDate = date[0];
                    }

                    if (TruckData.UpdateScheduleCheck(schedule, ckList, adminUser, this.SelectMaxDate, this.SelectMinDate) == false)
                    {
                        return false;
                    }

                    break;
            }

            #endregion

            return true;
        }

        /// <summary>
        /// 項目の入力チェック
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsEntryScheduleItem(ScheduleItemModel<TruckScheduleItemModel> item)
        {
            switch (item.ScheduleItemEdit)
            {
                case ScheduleItemEditType.Insert:
                    var msg = Validator.GetFormInputErrorMessage(this);
                    if (msg != "")
                    {
                        Messenger.Warn(msg);
                        return false;
                    }
                    break;

                case ScheduleItemEditType.Update:
                case ScheduleItemEditType.Delete:
                    var checkList = GetScheduleItem(new TruckScheduleItemSearchModel() { ID = item.ID });
                    if (checkList == null || checkList.Any() == false)
                    {
                        Messenger.Warn(Resources.KKM00021);
                        return false;
                    }
                    else
                    {
                        item.ScheduleItem = checkList.First();
                    }
                    break;

            }

            switch (item.ScheduleItemEdit)
            {
                case ScheduleItemEditType.Delete:

                    var res = HttpUtil.GetResponse<TruckScheduleSearchModel, TruckScheduleCountModel>(ControllerType.TruckScheduleCount,
                        new TruckScheduleSearchModel()
                        {
                            TruckId = item.ID
                        });

                    var modelList = new List<TruckScheduleCountModel>();
                    if (res != null && res.Status == Const.StatusSuccess)
                    {
                        modelList.AddRange(res.Results);
                    }

                    if (modelList.First().SCHEDULE_COUNT > 0)
                    {
                        Messenger.Warn(Resources.KKM00033);
                        return false;
                    }
                    break;
            }

            return true;
        }

        /// <summary>
        /// スケジュール項目背景色算出処理。
        /// </summary>
        /// <param name="scheduleItem"></param>
        /// <returns></returns>
        private CalendarScheduleColorEnum SetScheduleItemBackColor(TruckScheduleItemModel scheduleItem)
        {
            //Append Start 2022/02/24 杉浦 トラック予約一覧を追加
            if (this.CalendarCategoryId != null && scheduleItem.ID == this.CalendarCategoryId)
            {
                return CalendarScheduleColorEnum.CheckItemColor;
            }
            //Append End 2022/02/24 杉浦 トラック予約一覧を追加
            if (scheduleItem.FLAG_定期便 == 1)
            {
                return scheduleItem.ItemColor;
            }
            else
            {
                return CalendarScheduleColorEnum.TruckNormalColor;
            }
        }

        /// <summary>
        /// スケジュール情報取得処理
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="syncGridUpdate"></param>
        //Update Start 2022/01/11 杉浦 トラック予約一覧を追加
        //private void SetSchedule(DateTime? start = null, DateTime? end = null, bool syncGridUpdate = true)
        private void SetSchedule(DateTime? start = null, DateTime? end = null, bool syncGridUpdate = true, TruckScheduleItemSearchModel itemCond = null)
        //Update End 2022/01/11 杉浦 トラック予約一覧を追加
        {
            DateTime selectStartDate = start == null ? this.TruckScheduleCalendarGrid.FirstDateInView : start.Value;
            DateTime selectEndDate = end == null ? this.TruckScheduleCalendarGrid.LastDateInView : end.Value;

            #region 運休情報再取得
            var fixres = HttpUtil.GetResponse<FixedTimeDaySettingSearchModel, FixedTimeDaySettingModel>(
               ControllerType.FixedTimeDaySetting, new FixedTimeDaySettingSearchModel()
               {
                   START_DATE = selectStartDate,
                   END_DATE = selectEndDate
               });
            fixList = new List<FixedTimeDaySettingModel>();
            if (fixres != null && fixres.Status == Const.StatusSuccess)
            {
                fixList.AddRange(fixres.Results);
            }
            #endregion

            itemCond = new TruckScheduleItemSearchModel();
            if (ReservationsNotAcceptedCheckBox.Checked == false && ReservationAvailableCheckBox.Checked)
            {
                itemCond.BEFORE_DATE = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else if (ReservationsNotAcceptedCheckBox.Checked && ReservationAvailableCheckBox.Checked == false)
            {
                itemCond.AFTER_DATE = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else
            {
                itemCond.BEFORE_DATE = null;
                itemCond.AFTER_DATE = null;
            }

            #region 空車期間
            itemCond.EMPTY_START_DATE = getDateTime(this.EmptyStartDayDateTimePicker.SelectedDate, this.EmptyStartTimeComboBox.Text);
            itemCond.EMPTY_END_DATE = getDateTime(this.EmptyEndDayDateTimePicker.SelectedDate, this.EmptyEndTimeComboBox.Text);
            itemCond.INPUT_PERSONEL_ID = SessionDto.UserId;
            #endregion

            this.ScheduleItemList = GetScheduleItem(itemCond);

            #region 項目にあわせた定期便文字列生成

            var headerRes = HttpUtil.GetResponse<TruckRegularTimeModel>(ControllerType.TruckRegularTime);
            var regularTimeList = new List<TruckRegularTimeModel>();
            if (headerRes != null && headerRes.Status == Const.StatusSuccess)
            {
                regularTimeList.AddRange(headerRes.Results);
            }
            departurePointList = new List<DeparturePoint>();
            foreach (var item in ScheduleItemList.Where(x => x.FLAG_定期便 == 1))
            {
                string[] setText1;
                string[] setText2;
                string[] setText3;
                string[] setText4;
                if (item.始発場所 == "群馬")
                {
                    setText1 = new string[] { "群馬", "SKC" };
                    setText2 = new string[] { "群→" + Environment.NewLine + "SKC", "SKC→" + Environment.NewLine + "群" };
                    setText3 = new string[] { "群馬→SKC", "SKC→群馬" };
                    setText4 = new string[] { "群→SKC", "SKC→群" };
                }
                else
                {
                    setText1 = new string[] { "SKC", "群馬" };
                    setText2 = new string[] { "SKC→" + Environment.NewLine + "群", "群→" + Environment.NewLine + "SKC" };
                    setText3 = new string[] { "SKC→群馬", "群馬→SKC" };
                    setText4 = new string[] { "SKC→群", "群→SKC" };
                }

                var tlist = regularTimeList.Where(x => x.REGULAR_ID == item.REGULAR_TIME_ID && x.IS_RESERVATION == 1).OrderBy(x => x.TIME_ID).ToList();
                for (int i = 0; i < tlist.Count(); i++)
                {
                    departurePointList.Add(new DeparturePoint()
                    {
                        TruckId = item.ID,
                        Key = Expansion2CalendarTemplate.timeHeaderList.IndexOf(tlist[i].TIME_ID),
                        DepartureTime = tlist[i].DEPARTURE_TIME,
                        Text1 = setText1[i % 2],
                        Text2 = setText2[i % 2],
                        Text3 = setText3[i % 2],
                        Text4 = setText4[i % 2]
                    });
                }
            }
            #endregion

            #region 定期便連番ヘッダ設定
            
            if (this.TruckScheduleCalendarGrid.Template.GetType() == typeof(Expansion2CalendarTemplate))
            {
                this.TruckScheduleCalendarGrid.SuspendRender();

                calendarHeaderTextList = new Dictionary<CalendarScheduleColorEnum, string[]>();

                foreach (var item in this.ScheduleItemList.Where(x => x.FLAG_定期便 == 1).OrderBy(x => x.SORT_NO))
                {
                    var teikiList = regularTimeList.Where(x => x.REGULAR_ID == item.REGULAR_TIME_ID);
                    var colorKey = CalendarScheduleColorEnum.GetValues(teikiList.First().ItemColor, teikiList.First().FontColor);

                    if (calendarHeaderTextList.ContainsKey(colorKey) == false)
                    {
                        calendarHeaderTextList.Add(colorKey, teikiList.Select(x => x.DEPARTURE_TIME).ToArray());
                        calendarHeaderTextList[colorKey][0] = string.Empty;
                    }
                    calendarHeaderTextList[colorKey][0] += item.Serial;
                }

                var columnHeader = this.TruckScheduleCalendarGrid.Template.ColumnHeader;
                if (columnHeader.RowCount > 3) { columnHeader.RemoveRow(3, columnHeader.RowCount - 3); }
                if (calendarHeaderTextList.Any()) { columnHeader.AddRow(calendarHeaderTextList.Count()); }

                this.TruckScheduleCalendarGrid.Template.CornerHeader[0, 0].RowSpan = 3 + calendarHeaderTextList.Count;

                for (int col = 0; col < Expansion2CalendarTemplate.timeHeaderList.Count(); col++)
                {
                    foreach (var headerItem in calendarHeaderTextList.Select((Value, Index) => new { Value, Index }))
                    {
                        columnHeader.Rows[3 + headerItem.Index].Height = 17;
                        var timeCell2 = columnHeader[3 + headerItem.Index, col];

                        timeCell2.CellType = new CalendarHeaderCellType()
                        {
                            FlatStyle = FlatStyle.Flat
                        };
                        timeCell2.CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                        var cellBorderLine = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);

                        if (col == 0)
                        {
                            timeCell2.CellStyle.LeftBorder = cellBorderLine;
                        }
                        else if (col == Expansion2CalendarTemplate.timeHeaderList.Count())
                        {
                            timeCell2.CellStyle.RightBorder = cellBorderLine;
                        }

                        timeCell2.Value = headerItem.Value.Value[col];

                        var timeCell = columnHeader[2, col];
                        timeCell2.CellStyle.Font = timeCell.CellStyle.Font;
                        timeCell2.CellStyle.BackColor = headerItem.Value.Key.MainColor;
                        timeCell2.CellStyle.ForeColor = headerItem.Value.Key.FontColor;
                    }
                }
                
                this.TruckScheduleCalendarGrid.ResumeRender();
                this.TruckScheduleCalendarGrid.PerformRender();
            }

            UpdateAdmin();

            #endregion
            
            this.ScheduleList = TruckData.GetSchedule(new TruckScheduleSearchModel()
            {
                START_DATE = this.TruckScheduleCalendarGrid.FirstDateInView,
                END_DATE = this.TruckScheduleCalendarGrid.LastDateInView
            });

            #region 定期便用空行作成
            foreach (var item in this.ScheduleItemList.Where(x => x.FLAG_定期便 == 1))
            {
                var teikiList2 = regularTimeList.Where(x => x.REGULAR_ID == item.REGULAR_TIME_ID).ToList();
                var timeList = teikiList2.Where(x => x.DEPARTURE_TIME != "-").ToList();
                for (int i = 1; i < timeList.Count; i++)
                {
                    this.ScheduleList.Add(new TruckScheduleModel()
                    {
                        PARALLEL_INDEX_GROUP = i,
                        トラック_ID = item.ID
                    });
                }
            }
            #endregion

            ckList = TruckData.GetCKList(selectStartDate, selectEndDate, ScheduleItemList);

            Func<TruckScheduleModel, string> getSubTitle = schedule => GetSubTitle(schedule);

            TruckData.AllScheculeList = this.ScheduleList.Where(x => x.予約開始時間 != null).ToList();

            this.gridUtil.Bind(this.ScheduleItemList, this.ScheduleList, x => x.ID, y => y.トラック_ID,
                (x) => CreateScheduleItem(x),
                (y, x) => CreateSchedule(x, y, getSubTitle));

            if (syncGridUpdate && this.SyncCalendarGrid != null)
            {
                this.SyncFormStyleSaveEvent(this.SyncCalendarStyle, new EventArgs());
            }
        }

        /// <summary>
        /// カレンダー日付設定処理
        /// </summary>
        private void SetScheduleMostDayFirst()
        {
            if (this.SyncCalendarGrid != null)
            {
                this.gridUtil.SetScheduleMostDayFirst(this.SyncCalendarGrid.FirstDisplayedCellPosition.Date);
            }
            else
            {
                var emptyStartDate = this.EmptyStartDayDateTimePicker.SelectedDate;
                if (emptyStartDate != null)
                {
                    this.gridUtil.SetScheduleMostDayFirst(emptyStartDate.Value);

                }
                else
                {
                    this.gridUtil.SetScheduleMostDayFirst(DateTime.Now);
                }
            }

            this.ActiveControl = TruckScheduleCalendarGrid;
        }

        /// <summary>
        /// スケジュールタイトル取得処理
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private static string GetSubTitle(TruckScheduleModel schedule)
        {
            string retTitle = "";
            if (schedule.FLAG_定期便 == 1)
            {
                if (ReservationStautsEnum.FlagOf(schedule.FLAG_仮予約.ToString()) == ReservationStautsEnum.KARI_YOYAKU)
                {
                    retTitle = "(仮)";
                }
                var list = departurePointList.Where(x => x.TruckId == schedule.トラック_ID && x.DepartureTime == schedule.DEPARTURE_TIME);
                if (list.Count() > 0)
                {
                    retTitle += list.First().Text5 + schedule.定期便依頼者名;
                }
            }
            else
            {
                retTitle = schedule.運転者A名;
            }

            return retTitle;
        }

        /// <summary>
        /// トラック予約セルカスタマイズ処理
        /// </summary>
        /// <param name="editCell">カスタマイズを行うカレンダーセル</param>
        /// <param name="scheduleModel">表示対象のスケジュールデータ</param>
        /// <param name="colIndex">編集中のカレンダーセルの列位置</param>
        /// <param name="rowIndex">編集中のカレンダーセルの行位置</param>
        private void CalendarCellPaint(CalendarCell editCell, CalendarItemModel<TruckScheduleItemModel, TruckScheduleModel> scheduleModel, int colIndex, int rowIndex)
        {
            var item = scheduleModel.ScheduleItem.ScheduleItem;
            if (item.FLAG_定期便 == 1 && this.gridUtil.CalendarSetting.CalendarMode == CalendarTemplateTypeSafeEnum.EXPANSION2)
            {
                var moji = departurePointList.Where(x => x.TruckId == item.ID && x.Key == colIndex).ToList();

                if (moji.Count() <= 0 || rowIndex != colIndex) { return; }

                var type = new CalendarLabelCellType();
                type.WordWrap = true;
                type.Multiline = true;

                editCell.CellType = type;
                editCell.Value = moji.First().Text2;

                editCell.CellStyle.ForeColor = Color.Gray;
            }
        }
        
        /// <summary>
        /// トラック予約スケジュール情報作成処理
        /// </summary>
        /// <returns></returns>
        private ScheduleModel<TruckScheduleModel> CreateSchedule(ScheduleItemModel<TruckScheduleItemModel> x, TruckScheduleModel y, Func<TruckScheduleModel, string> getSubTitle)
        {
            bool isEdit = true;//トラック予約は右クリックメニュー表示が必須のためTrue固定。権限確認は別途実施。

            var schedule = new ScheduleModel<TruckScheduleModel>
                (y.ID,
                y.トラック_ID,
                y.PARALLEL_INDEX_GROUP,
                getSubTitle(y),
                y.予約開始時間,
                y.予約終了時間,
                y.予約修正日時,
                null,
                false,
                isEdit,
                y,
                ReservationStautsEnum.FlagOf(y.FLAG_仮予約.ToString()).Key);

            if (y.FLAG_定期便 == 1)
            {
                schedule.IsResizeHandler = false;
            }

            if (y.FLAG_定期便 == 1)
            {
                schedule.ToolTip = new RegularToolTip(y, departurePointList);
            }
            else
            {
                schedule.ToolTip = new EachTrackToolTip(y);
            }

            return schedule;
        }

        /// <summary>
        /// トラック予約項目情報作成処理
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static ScheduleItemModel<TruckScheduleItemModel> CreateScheduleItem(TruckScheduleItemModel x)
        {
            var item = new ScheduleItemModel<TruckScheduleItemModel>(
                x.ID,
                x.ID.ToString(),
                x.CalendarCarName,
                (x.FLAG_定期便 == 1) ?
                "予約許可必要" : "予約許可不要", 0, x.SORT_NO, null, x);
            
            item.ToolTip = new TrackItemToolTip(x);

            if (x.FLAG_定期便 == 1)
            {
                item.IsInputNewRow = false;
            }

            return item;
        }

        /// <summary>
        /// 定期便・各トラック予約画面起動処理
        /// </summary>
        /// <param name="schedule">スケジュールデータ</param>
        /// <param name="editType">スケジュール編集区分</param>
        private void ShowScheduleDetailForm(ScheduleModel<TruckScheduleModel> schedule, ScheduleEditType editType)
        {
            var selectItem = this.ScheduleItemList.Where(x => x.ID == schedule.CategoryID).First();
            var mode = this.gridUtil.CalendarSetting.CalendarMode;

            if (editType == ScheduleEditType.Insert)
            {
                if(selectItem.FLAG_定期便 == 1)
                {
                    if (TruckScheduleCalendarGrid.SelectedCells.Count != 1)
                    {
                        return;
                    }
                    UpdateRegTime(schedule, schedule);
                    
                    if (TruckData.CheckRegSchedule(departurePointList, fixList, mode, schedule, adminUser) == false)
                    {
                        return;
                    }
                }
            }

            var itemList = GetScheduleItem(new TruckScheduleItemSearchModel() { ID = schedule.CategoryID });
            if (IsExistItemSchedule(itemList, schedule) == false)
            {
                SetSchedule();
                return;
            }
            
            //編集モードが追加の場合はスケジュールの初期化
            if (editType == ScheduleEditType.Insert)
            {
                schedule.Schedule = new TruckScheduleModel
                {
                    トラック_ID = schedule.CategoryID,
                    PARALLEL_INDEX_GROUP = schedule.RowNo,
                    予約開始時間 = schedule.StartDate,
                    予約終了時間 = schedule.EndDate,
                    FLAG_定期便 = selectItem.FLAG_定期便,
                    FLAG_仮予約 = 0,
                    SectionList = new List<TruckScheduleSectionModel>(),
                    ShipperRecipientUserList = new List<TruckShipperRecipientUserModel>()
                };
            }

            if (selectItem.FLAG_定期便 == 1)
            {

                using (var form = new TruckScheduleRegularDetailForm()
                {   
                    CalendarMode = mode,
                    FixList = fixList,
                    DeparturePointList = departurePointList,
                    Schedule = schedule,
                    ScheduleItem = selectItem,
                    DeleteMessage = (editType == ScheduleEditType.Insert) ? "" : string.Format(Resources.KKM02006,
                    schedule.Schedule.車両名,
                    schedule.Schedule.DEPARTURE_TIME,
                    departurePointList.Where(x => x.TruckId == schedule.CategoryID && x.DepartureTime == schedule.Schedule.DEPARTURE_TIME).First().Text4
                    )
                })
                {
                    form.SelectMaxDate = this.SelectMaxDate;
                    form.SelectMinDate = this.SelectMinDate;

                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(form.ReturnMessage)) this.MessageLabel.Text = form.ReturnMessage;
                        SetSchedule();

                        if (schedule.ScheduleEdit != ScheduleEditType.Update)
                        {
                            this.gridUtil.UndoRedoManager.Do(schedule);
                        }
                    }
                    else
                    {
                        if (editType == ScheduleEditType.Paste)
                        {
                            SetSchedule();
                        }
                    }
                }
            }
            else
            {
                using (var form = new TruckScheduleDetailForm() { Schedule = schedule, ScheduleItem = selectItem, CKList = ckList })
                {
                    form.SelectMaxDate = this.SelectMaxDate;
                    form.SelectMinDate = this.SelectMinDate;

                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (!string.IsNullOrWhiteSpace(form.ReturnMessage)) this.MessageLabel.Text = form.ReturnMessage;
                        SetSchedule();

                        if (schedule.ScheduleEdit != ScheduleEditType.Update)
                        {
                            this.gridUtil.UndoRedoManager.Do(schedule);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 日付制限取得処理。
        /// </summary>
        /// <remarks>
        /// ３ヶ月制限用の日付を算出します。
        /// Minが配列の[0]、Maxが[1]へ設定されます。
        /// </remarks>
        /// <returns></returns>
        private DateTime[] GetChangeDateTime()
        {
            // TODO 後々カーシェアと統合したい処理
            var array = new DateTime[2];

            DateTime toDay = DateTime.Today;
            DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
            //Update Start 2021/10/15 杉浦 トラック予約_トラック予約表の予約可能期間を変更したい
            //DateTime end = start.AddMonths(3).AddDays(-1);
            DateTime end = toDay.AddMonths(3);
            //Update End 2021/10/15 杉浦 トラック予約_トラック予約表の予約可能期間を変更したい

            array[0] = start;
            array[1] = end;

            return array;
        }

        /// <summary>
        /// 定期便予約確認メールフォーム表示処理
        /// </summary>
        private void OpenRegularMailForm(ScheduleModel<TruckScheduleModel> item)
        {
            using (var form = new RegularMailForm() { RegularDate = item.StartDate.Value, TruckId = item.Schedule.トラック_ID, DeparturePointList = departurePointList })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 項目詳細起動処理
        /// </summary>
        /// <param name="item"></param>
        private void OpenScheduleItemForm(ScheduleItemModel<TruckScheduleItemModel> item, ScheduleItemEditType type)
        {
            item.ScheduleItemEdit = type;
            
            using (var form = new TruckScheduleItemDetailForm() { ScheduleItem = item })
            {
                var ret = form.ShowDialog(this);
                if (ret == DialogResult.OK)
                {
                    SetSchedule();
                }
                else
                {
                    if (form.UpdateCheck)
                    {
                        SetSchedule();
                    }
                }
            }
        }

        /// <summary>
        /// 管理者情報変更画面起動処理
        /// </summary>
        private void ChangeManagementUser()
        {
            // 管理者修正画面の表示。
            using (var form = new AdminChangeForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateAdmin();
                }
            }
        }

        /// <summary>
        /// 管理者情報最新化処理
        /// </summary>
        private void UpdateAdmin()
        {
            UserInfo.SetTruckManageUser();
            this.gridUtil.CornerHeaderText = "連絡先:" + UserInfo.UserName + "\n   (" + UserInfo.UserTel + ")";
        }

        /// <summary>
        /// 検索・更新ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            // 検索条件チェック
            if (!IsSearchSchedule())
            {
                return;
            }

            FormControlUtil.FormWait(this, () => SetSchedule());

            if (this.EmptyStartDayDateTimePicker.SelectedDate != null && this.EmptyEndDayDateTimePicker.SelectedDate != null)
            {
                SetScheduleMostDayFirst();
            }
        }

        /// <summary>
        /// 日時の取得
        /// </summary>
        /// <param name="dtp">日付</param>
        /// <param name="cmb">時刻</param>
        /// <returns>日時</returns>
        private DateTime? GetDateTime(NullableDateTimePicker dtp, ComboBox cmb)
        {
            //日時が入力されているかどうか
            if (dtp.SelectedDate == null || string.IsNullOrWhiteSpace(cmb.Text) == true)
            {
                return null;
            }
            return dtp.SelectedDate.Value.AddHours(int.Parse(cmb.Text));
        }

        /// <summary>
        /// スケジュール検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchSchedule()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.GetDateTime(this.EmptyStartDayDateTimePicker, this.EmptyStartTimeComboBox);
            var end = this.GetDateTime(this.EmptyEndDayDateTimePicker, this.EmptyEndTimeComboBox);

            //空車期間のチェック
            map[this.EmptyStartDayDateTimePicker] = (c, name) =>
            {
                //空車期間Toのみ入力かどうか
                if (start == null && end != null)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;

                    //エラーメッセージ
                    return Resources.KKM03015;
                }

                //空車期間Fromのみ入力かどうか
                if (start != null && end == null)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;

                    //エラーメッセージ
                    return Resources.KKM03015;
                }

                //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    //背景色を変更
                    this.EmptyStartDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.EmptyStartTimeComboBox, Const.ErrorBackColor);
                    this.EmptyEndDayDateTimePicker.BackColor = Const.ErrorBackColor;
                    FormControlUtil.SetComboBoxBackColor(this.EmptyEndTimeComboBox, Const.ErrorBackColor);

                    //エラーメッセージ
                    return Resources.KKM00018;
                }

                return string.Empty;
            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;
        }

        /// <summary>
        /// 凡例ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegendButton_Click(object sender, EventArgs e)
        {
            string formName = typeof(TruckLegendForm).Name + "_TruckScheduleForm";

            var openform = Application.OpenForms[formName];

            if (openform == null)
            {
                var frm = new TruckLegendForm();
                frm.Name = formName;

                var bs = this.LegendButton.PointToScreen(new Point(0, 0));
                var cp = new Point(bs.X - frm.Width, bs.Y);
                frm.Location = cp;
                frm.StartPosition = FormStartPosition.Manual;
                frm.Show(this);
            }
            else
            {
                openform.Close();
            }
        }

        /// <summary>
        /// クリアボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // 空車期間(From)
            this.EmptyStartDayDateTimePicker.Value = null;
            this.EmptyStartDayDateTimePicker.BackColor = Const.DefaultBackColor;
            this.EmptyStartTimeComboBox.SelectedIndex = 0;
            FormControlUtil.SetComboBoxBackColor(this.EmptyStartTimeComboBox, Const.DefaultBackColor);

            // 空車期間(To)
            this.EmptyEndDayDateTimePicker.Value = null;
            this.EmptyEndDayDateTimePicker.BackColor = Const.DefaultBackColor;
            this.EmptyEndTimeComboBox.SelectedIndex = this.EmptyEndTimeComboBox.Items.Count - 1;
            FormControlUtil.SetComboBoxBackColor(this.EmptyEndTimeComboBox, Const.DefaultBackColor);

            // 予約済み
            this.ReservationAvailableCheckBox.Checked = true;
            this.ReservationsNotAcceptedCheckBox.Checked = false;
        }

        /// <summary>
        /// 検索条件ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, 70);
        }

        /// <summary>
        /// 送り状印刷
        /// </summary>
        /// <param name="item"></param>
        private void PrintInvoice(ScheduleModel<TruckScheduleModel> item)
        {
            var report = new Invoice();
            report.Print(departurePointList, item.Schedule);
        }

        /// <summary>
        /// 搬送依頼書印刷
        /// </summary>
        /// <param name="item"></param>
        private void PrintTransportRequest(ScheduleModel<TruckScheduleModel> item)
        {
            var report = new TransportRequest();
            
            var printDataList = new List<TruckScheduleModel>();

            var scheduleList = TruckData.GetSchedule(new TruckScheduleSearchModel()
            {
                START_DATE = item.Schedule.予約開始時間.Value.Date,
                END_DATE = item.Schedule.予約開始時間.Value.Date,
                IsRegular = true,
                IsGetKettei = true,
                TruckId = item.Schedule.トラック_ID
            });

            foreach (var teiki in departurePointList.Where(x => x.TruckId == item.Schedule.トラック_ID))
            {
                var model = scheduleList.Where(x => x.DEPARTURE_TIME == teiki.DepartureTime);
                if (model.Count() > 0)
                {
                    printDataList.Add(model.First());
                }
                else
                {
                    printDataList.Add(new TruckScheduleModel()
                    {
                        DEPARTURE_TIME = teiki.DepartureTime,
                        トラック_ID = teiki.TruckId,
                        車両名 = item.Schedule.車両名,
                        予約開始時間 = item.Schedule.予約開始時間.Value,
                        ShipperRecipientUserList = new List<TruckShipperRecipientUserModel>(),
                        SectionList = new List<TruckScheduleSectionModel>()
                    });
                }
            }

            report.Print(departurePointList, printDataList);           
        }
      
        /// <summary>
        /// トラックカレンダーグリッドリフレッシュ
        /// </summary>
        internal void RefreshCalendarTemplate()
        {
            if (this.gridUtil == null) { return; }

            this.SyncFlag = false;
            this.gridUtil.CalendarSetting.SaveCalendarUserData -= this.SaveCalendarUserData;
            
            var oldFirstDateInView = this.TruckScheduleCalendarGrid.FirstDateInView;
            this.TruckScheduleCalendarGrid.FirstDateInView = this.SyncCalendarGrid.FirstDateInView;

            var setting = new CalendarSettings(this.SyncCalendarStyle);

            if (this.gridUtil.ChangeTemplateSettings(setting) || oldFirstDateInView != this.SyncCalendarGrid.FirstDateInView)
            {
                this.gridUtil.SetCalendarViewPeriod(new DateTime(this.TruckScheduleCalendarGrid.FirstDateInView.Year, this.TruckScheduleCalendarGrid.FirstDateInView.Month, 1));
                this.gridUtil.SetTemplateHeader();
                SetSchedule(this.TruckScheduleCalendarGrid.FirstDateInView, this.SyncCalendarGrid.LastDateInView);
            }
            this.gridUtil.SetScheduleMostDayFirst(this.SyncCalendarGrid.FirstDisplayedCellPosition.Date);
            this.TruckScheduleCalendarGrid.HorizontalScrollBarOffset = this.SyncCalendarGrid.HorizontalScrollBarOffset;

            this.gridUtil.CalendarSetting.SaveCalendarUserData += this.SaveCalendarUserData;
            this.SyncFlag = true;
        }

        /// <summary>
        /// スケジュール項目データ取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        private List<TruckScheduleItemModel> GetScheduleItem(TruckScheduleItemSearchModel cond)
        {   
            var res = HttpUtil.GetResponse<TruckScheduleItemSearchModel, TruckScheduleItemModel>(ControllerType.TruckScheduleItem, cond);

            var modelList = new List<TruckScheduleItemModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                modelList.AddRange(res.Results);
            }
            return modelList;
        }

        /// <summary>
        /// フォームクローズ処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            if(this.SyncCalendarGrid != null)
            {
                this.SyncFormRefreshEvent(this, new EventArgs());
            }
            base.OnClosed(e);
        }

        /// <summary>
        /// スクロールオフセット再設定
        /// </summary>
        /// <remarks>
        /// スクロールのトリガーのコントロールがカレンダーコントロールに存在する
        /// コントロールの場合、スクロールをしないよう制御します。
        /// </remarks>
        /// <param name="activeControl"></param>
        /// <returns></returns>
        protected override Point ScrollToControl(Control activeControl)
        {
            if (this.gridUtil.CanScrollToControl(activeControl.GetType()) == false)
            {
                return new Point(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
            }
            else
            {
                return base.ScrollToControl(activeControl);
            }
        }

        /// <summary>
        /// 元に戻す・やり直し処理
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool UndoRedo(ScheduleModel<TruckScheduleModel> schedule)
        {
            if (schedule.ScheduleEdit == ScheduleEditType.Update)
            {
                var res = HttpUtil.PutResponse(ControllerType.TruckSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    SetSchedule();
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Delete)
            {
                //削除の時はinsert、insertの時はdelete。
                var res = HttpUtil.PostResponse(ControllerType.TruckSchedule, new[] { schedule.Schedule });
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    schedule.Schedule = res.Results.OfType<TruckScheduleModel>().FirstOrDefault();
                    SetSchedule();
                    return true;
                }
                return false;
            }
            else if (schedule.ScheduleEdit == ScheduleEditType.Insert || schedule.ScheduleEdit == ScheduleEditType.Paste)
            {
                var list = new[] { schedule.Schedule };

                var res = HttpUtil.DeleteResponse(ControllerType.TruckSchedule, list);
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    SetSchedule();
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// 定期便発着地文言管理クラス
    /// </summary>
    public class DeparturePoint
    {
        /// <summary>
        /// 定期便時間帯（時間帯文言）
        /// </summary>
        public string DepartureTime { get; internal set; }

        /// <summary>
        /// 時間帯のセルインデックス
        /// </summary>
        public int Key { get; internal set; }

        /// <summary>
        /// トラックID
        /// </summary>
        public long TruckId { get; internal set; }

        /// <summary>
        /// 表示発着地名
        /// </summary>
        public string Text1 { get; internal set; }

        /// <summary>
        /// 表示発着地名（発）
        /// </summary>
        public string Text5 { get { return Text1 + "発"; } }

        /// <summary>
        /// 表示発着地記号（群→SKC）改行あり
        /// </summary>
        public string Text2 { get; internal set; }

        /// <summary>
        /// 表示発着地記号（群馬→SKC）
        /// </summary>
        public string Text3 { get; internal set; }

        /// <summary>
        /// 表示発着地記号（群→SKC）改行なし
        /// </summary>
        public string Text4 { get; internal set; }
    }

    /// <summary>
    /// スケジュールデータ管理クラス
    /// </summary>
    public class TruckData
    {
        /// <summary>
        /// スケジュールデータ
        /// </summary>
        public static List<TruckScheduleModel> AllScheculeList { get; internal set; }

        /// <summary>
        /// スケジュールデータ取得処理
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public static List<TruckScheduleModel> GetSchedule(TruckScheduleSearchModel cond)
        {
            var res = HttpUtil.GetResponse<TruckScheduleSearchModel, TruckScheduleModel>(ControllerType.TruckSchedule, cond);

            var modelList = new List<TruckScheduleModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                modelList.AddRange(res.Results);
            }
            return modelList;
        }

        /// <summary>
        /// 残業便チェック処理
        /// </summary>
        /// <param name="departurePointList">定期便発着マスタリスト</param>
        /// <param name="schedule">チェック対象スケジュールデータ</param>
        /// <param name="timer">選択された定期便時間帯</param>
        public static void IsZangyo(List<DeparturePoint> departurePointList, ScheduleModel<TruckScheduleModel> schedule, int timer, UserAuthorityOutModel userAuthority)
        {
            if (userAuthority.MANAGEMENT_FLG == '1') { return; }

            var time = departurePointList.Where(x =>
                x.TruckId == schedule.CategoryID &&
                x.Key == Expansion2CalendarTemplate.timeHeaderList.IndexOf(timer)).FirstOrDefault();

            if (time != null)
            {
                var start = DateTime.Parse(time.DepartureTime);
                var maxDateTime = schedule.StartDate.Value.Date.AddHours(start.Hour).AddMinutes(start.Minute).AddHours(1).AddMinutes(30);
                if (maxDateTime >= schedule.StartDate.Value.Date.AddHours(17))
                {
                    Messenger.Warn(Resources.KKM02005);
                }
            }
        }

        /// <summary>
        /// 定期便入力チェック
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static bool CheckRegSchedule(List<DeparturePoint> departurePointList, List<FixedTimeDaySettingModel> fixList, CalendarTemplateTypeSafeEnum mode, ScheduleModel<TruckScheduleModel> schedule, bool adminUser, bool showWarnMessage = false)
        {
            var unkyuDate = fixList.Where(x =>
                x.トラック_ID == schedule.CategoryID &&
                (x.時間帯 != null || x.FLAG_運休日 == 1) && x.DATE == schedule.StartDate.Value.Date).FirstOrDefault();

            if (unkyuDate != null)
            {
                if (unkyuDate.FLAG_運休日 == 1)
                {
                    if (showWarnMessage) { Messenger.Warn("運休日です。予約できません。"); }
                    return false;
                }

                foreach (var hour in unkyuDate.TimeList)
                {
                    if (hour == schedule.StartDate.Value.Hour.ToString())
                    {
                        if (showWarnMessage) { Messenger.Warn("運休の時間帯です。予約できません。"); }
                        return false;
                    }
                }
            }

            if (mode == CalendarTemplateTypeSafeEnum.EXPANSION2)
            {
                if (departurePointList.Any(x =>
                        x.TruckId == schedule.CategoryID &&
                        x.Key == Expansion2CalendarTemplate.timeHeaderList.IndexOf(schedule.StartDate.Value.Hour)) == false)
                {
                    if (showWarnMessage) { Messenger.Warn("その時間帯には予約できません。"); }
                    return false;
                }

                if (schedule.RowNo != Expansion2CalendarTemplate.timeHeaderList.IndexOf(schedule.StartDate.Value.Hour))
                {
                    if (showWarnMessage) { Messenger.Warn("その時間帯・行には予約できません。"); }
                    return false;
                }
            }

            if (adminUser == false && schedule.ScheduleEdit != ScheduleEditType.Delete)
            {
                var hour = schedule.StartDate.Value.Hour;
                var today = DateTime.Now;
                var checkTimeIndex = Expansion2CalendarTemplate.timeHeaderList.IndexOf(schedule.StartDate.Value.Hour);
                var nextTimeHour = Expansion2CalendarTemplate.timeHeaderList[checkTimeIndex == 8 ? checkTimeIndex : checkTimeIndex + 1];
                var prevTimeHour = Expansion2CalendarTemplate.timeHeaderList[checkTimeIndex - 1];

                var nextData = AllScheculeList.Where(x => 
                                    x.トラック_ID == schedule.CategoryID && 
                                    x.ID != schedule.ID && 
                                    x.予約開始時間.Value.Date == schedule.StartDate.Value.Date && 
                                    x.予約開始時間.Value.Hour == nextTimeHour);

                var prevData = AllScheculeList.Where(x =>
                    x.トラック_ID == schedule.CategoryID &&
                    x.ID != schedule.ID &&
                    x.予約開始時間.Value.Date == schedule.StartDate.Value.Date &&
                    x.予約開始時間.Value.Hour == prevTimeHour);

                long nextScheduleId = 0;
                long prevScheduleId = 0;
                if (nextData.FirstOrDefault() != null)
                {
                    nextScheduleId = nextData.FirstOrDefault().ID;
                }

                if (prevData.FirstOrDefault() != null)
                {
                    prevScheduleId = prevData.FirstOrDefault().ID;
                }

                bool error = true;
                if (hour == 8 || hour == 12)
                {
                    if (nextScheduleId != 0) { error = false; }
                }
                if (hour == 10 || hour == 14)
                {
                    if (prevScheduleId != 0) { error = false; }
                }

                if (error)
                {
                    var time = departurePointList.Where(x =>
                            x.TruckId == schedule.CategoryID &&
                            x.Key == Expansion2CalendarTemplate.timeHeaderList.IndexOf(hour)).FirstOrDefault();

                    DateTime checkTime;
                    if (time != null)
                    {
                        checkTime = DateTime.Parse(time.DepartureTime);

                        if ((checkTime.TimeOfDay < new TimeSpan(12, 0, 0) && today > schedule.StartDate.Value.Date.AddDays(-1).AddHours(17).AddMinutes(0).AddSeconds(59)) ||
                            (checkTime.TimeOfDay >= new TimeSpan(12, 0, 0) && today > schedule.StartDate.Value.Date.AddHours(11).AddMinutes(40).AddSeconds(59)))
                        {
                            error = true;
                        }
                        else
                        {
                            error = false;
                        }
                    }
                }
                if (error)
                {
                    Messenger.Info(Resources.KKM01018);
                    return false;
                }                
            }

            return true;
        }

        public static bool UpdateScheduleCheck(ScheduleModel<TruckScheduleModel> schedule, List<TruckScheduleModel> ckList, bool adminUser, DateTime SelectMaxDate, DateTime SelectMinDate)
        {
            var checkList = GetSchedule(new TruckScheduleSearchModel()
            {
                TruckId = schedule.CategoryID,
                START_DATE = schedule.StartDate?.Date,
                END_DATE = schedule.EndDate?.Date
            });

            var start = schedule.StartDate;
            var end = schedule.EndDate;
            if(start.Value.Hour == 21 && end.Value.Hour == 22)
            {
                start = start.Value.AddHours(1);
                end = end.Value.AddHours(1);
            }

            //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
            // メイン画面のほうの条件を直すのも忘れず。
            if (adminUser == false &&
                (start.Value.Date < SelectMinDate || start.Value.Date > SelectMaxDate || end.Value.Date < SelectMinDate || end.Value.Date > SelectMaxDate))
            //Update Start 2021/10/15 杉浦 一般権限の予約可能期間を操作日から2ヶ月になるようにしてほしい
            {
                Messenger.Warn(string.Format(Resources.KKM01009, 3));
                return false;
            }
            if (checkList.Where(x => x.ID != schedule.ID && x.PARALLEL_INDEX_GROUP == schedule.RowNo).Any(x =>
                                            (x.予約開始時間.Value.Date <= start.Value.Date && start.Value.Date <= x.予約終了時間.Value.Date) ||
                                            (x.予約開始時間.Value.Date <= end.Value.Date && end.Value.Date <= x.予約終了時間.Value.Date) ||
                                            (start.Value.Date <= x.予約開始時間.Value.Date && x.予約開始時間.Value.Date <= end.Value.Date) ||
                                            (start.Value.Date <= x.予約終了時間.Value.Date && x.予約終了時間.Value.Date <= end.Value.Date)) == true)
            {
                Messenger.Warn(Resources.KKM03017);
                return false;
            }
            else if (checkList.Where(x => x.ID != schedule.ID).Any(x =>
                 (x.予約開始時間 <= start && start < x.予約終了時間) || (x.予約開始時間 < end && end <= x.予約終了時間) ||
                 (start <= x.予約開始時間 && x.予約開始時間 < end) || (start < x.予約終了時間 && x.予約終了時間 <= end)) == true)
            {
                //スケジュールで重複した期間が存在する場合はエラー
                Messenger.Warn(Resources.KKM03005);
                return false;
            }
            else if (ckList.Any(x => x.トラック_ID == schedule.CategoryID &&
             ((x.予約開始時間 <= schedule.StartDate && schedule.StartDate < x.予約終了時間) ||
             (x.予約開始時間 < schedule.EndDate && schedule.EndDate <= x.予約終了時間) ||
             (schedule.StartDate <= x.予約開始時間 && x.予約開始時間 < schedule.EndDate) ||
             (schedule.StartDate < x.予約終了時間 && x.予約終了時間 <= schedule.EndDate))
             )
             )
            {
                Messenger.Warn(Resources.KKM03043);
                return false;
            };

            return true;
        }

        /// <summary>
        /// CKリスト取得
        /// </summary>
        /// <param name="selectStartDate"></param>
        /// <param name="selectEndDate"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static List<TruckScheduleModel> GetCKList(DateTime selectStartDate, DateTime selectEndDate, List<TruckScheduleItemModel> itemList)
        {
            var ret = new List<TruckScheduleModel>();
            var resCalendarKadou = HttpUtil.GetResponse<CalendarKadouGetInModel, CalendarKadouGetOutModel>(ControllerType.CalendarKadou,
                new CalendarKadouGetInModel()
                {
                    FIRST_DATE = selectStartDate,
                    LAST_DATE = selectEndDate
                });
            if (resCalendarKadou != null && resCalendarKadou.Status == Const.StatusSuccess)
            {
                var kadouList = resCalendarKadou.Results.ToList().
                    Where(x => x.KADOBI_KBN == "1" && DateTime.ParseExact(x.CALENDAR_DATE, "yyyyMMdd", null).Day >= 20);

                if (kadouList.Count() > 0)
                {
                    var d = kadouList.GroupBy(x => DateTime.ParseExact(x.CALENDAR_DATE, "yyyyMMdd", null).Month);

                    foreach (var item in itemList.Where(x => x.FLAG_CHECK == 1))
                    {
                        foreach (var dd in d)
                        {
                            if (dd.ElementAt(0) != null)
                            {
                                var kadouDate = DateTime.ParseExact(dd.ElementAt(0).CALENDAR_DATE, "yyyyMMdd", null);

                                ret.Add(new TruckScheduleModel()
                                {
                                    トラック_ID = item.ID,
                                    予約開始時間 = kadouDate.Date.AddHours(6),
                                    予約終了時間 = kadouDate.Date.AddHours(7),
                                    運転者A名 = "CK",
                                    使用目的 = "CK",
                                    予約者名 = UserInfo.UserName + "(" + UserInfo.UserTel + ")",
                                    FLAG_定期便 = 0,
                                    FLAG_仮予約 = 0,
                                    PARALLEL_INDEX_GROUP = 1,
                                    SectionList = new List<TruckScheduleSectionModel>(),
                                    ShipperRecipientUserList = new List<TruckShipperRecipientUserModel>()
                                });
                            }
                        }
                    }
                }
            }

            return ret;
        }
    }
}
