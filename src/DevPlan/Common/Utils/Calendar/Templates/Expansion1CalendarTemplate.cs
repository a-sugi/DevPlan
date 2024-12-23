using DevPlan.UICommon.Enum;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DevPlan.UICommon.Utils.Calendar.Templates
{
    /// <summary>
    /// 拡大１テンプレートクラス。
    /// </summary>
    public class Expansion1CalendarTemplate : CalendarTemplate, ICalendarTemplate
    {
        /// <summary>
        /// 午前の時間帯のリスト。
        /// </summary>
        List<int> timeAmList;

        /// <summary>
        /// 午後の時間帯のリスト。
        /// </summary>
        List<int> timePmList;

        /// <summary>
        /// デフォルト開始時間内部保持フィールド。
        /// </summary>
        private int defaultStartTime;

        /// <summary>
        /// デフォルト終了時間内部保持フィールド。
        /// </summary>
        private int defaultEndTime;

        /// <summary>
        /// デフォルトコンストラクタ。
        /// </summary>
        /// <remarks>使用禁止です。</remarks>
        private Expansion1CalendarTemplate() : base() { return; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// カレンダースタイル情報を元にカレンダーテンプレートを作成します。
        /// </remarks>
        /// <param name="calendarStyle">カレンダースタイル情報</param>
        public Expansion1CalendarTemplate(CalendarStyle calendarStyle)
        {
            this.defaultStartTime = calendarStyle.DefaultStartTime;
            this.defaultEndTime = calendarStyle.DefaultEndTime;

            timeAmList = new List<int> { defaultStartTime, 12 };
            timePmList = new List<int> { 12, defaultEndTime };

            ColumnCount = 2;//午前と午後
            ColumnHeaderRowCount = 2;

            var cornerHeaderCell = CornerHeader[0, 0];
            cornerHeaderCell.CellType = new CalendarHeaderCellType()
            {
                FlatStyle = FlatStyle.Flat
            };
            cornerHeaderCell.RowSpan = ColumnHeaderRowCount;
            cornerHeaderCell.ColumnSpan = 2;
            cornerHeaderCell.CellStyle.Font = ControlFont.DefaultPFont.Font;

            for (int col = 0; col < ColumnCount; col++)
            {
                var yearMonthCell = ColumnHeader[0, col];
                yearMonthCell.CellType = new CalendarHeaderCellType();
                yearMonthCell.DateFormat = "yyyy/MM";
                yearMonthCell.DateFormatType = CalendarDateFormatType.DotNet;
                yearMonthCell.CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                yearMonthCell.AutoMergeMode = AutoMergeMode.Horizontal;
                yearMonthCell.CellStyleName = "headerStyle";

                var dayCell = ColumnHeader[1, col];
                dayCell.CellType = new CalendarHeaderCellType();
                dayCell.DateFormat = "{Day}";
                dayCell.ColumnSpan = ColumnCount;
                dayCell.CellStyle.Alignment = CalendarGridContentAlignment.MiddleRight;
                dayCell.CellStyleName = "defaultStyle";
            }

            var rowHeaderTable = RowHeader;
            rowHeaderTable.ColumnCount = 2;
            rowHeaderTable.Columns[0].Width = 40;
            rowHeaderTable[0, 0].CellType = new CalendarHeaderCellType();
            rowHeaderTable.Columns[1].Width = calendarStyle.ScheduleRowHeaderWidth;
            rowHeaderTable[0, 1].CellType = new CalendarHeaderCellType();

            var width = 15 + calendarStyle.HorizontalLengthUpdate;
            if (width <= 0)
            {
                throw new Exception("カレンダーを表示できません。列の幅が0以下になる不正な値が設定されています。（拡大１レイアウト）");
            }
            Content.Columns[0].Width = width;
            Content.Columns[1].Width = width;

            if (RowCount > 0)
            {
                RemoveRow(0, RowCount);
            }

            Content.CellStyle.Font = ControlFont.GetDefaultFont(float.Parse(calendarStyle.FontSize));
        }

        /// <summary>
        /// スケジュール変更後の計算処理。
        /// </summary>
        /// <remarks>
        /// CalendarCellPositionをもとに、変更されたスケジュールの開始日時と終了日時を設定します。
        /// </remarks>
        /// <param name="cp"></param>
        /// <param name="colSpan"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        public CalendarDate CreateCalendarDate(CalendarCellPosition cp, int colSpan, DateTime oldStartDate, DateTime oldEndDate)
        {
            #region oldの時間を12時くぎりへ変更

            DateTime oldStartDate12 = new DateTime(oldStartDate.Year, oldStartDate.Month, oldStartDate.Day,
                (oldStartDate.TimeOfDay >= new TimeSpan(12, 0, 0)) ? 12 : this.defaultStartTime, 0, 0);

            DateTime oldEndDate12;
            oldEndDate12 = new DateTime(oldEndDate.Year, oldEndDate.Month, oldEndDate.Day,
                (oldEndDate.TimeOfDay > new TimeSpan(12, 0, 0)) ? this.defaultEndTime : 12, 0, 0);

            #endregion

            CalendarDate ret = GetCalendarDate(cp, colSpan, oldStartDate, oldEndDate);

            ret.StartDate = (ret.StartDate != oldStartDate12) ? ret.StartDate : oldStartDate;
            ret.EndDate = (ret.EndDate != oldEndDate12) ? ret.EndDate : oldEndDate;

            return ret;
        }

        /// <summary>
        /// CalendarCellPosition、colSpanからの日付算出処理。
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="colSpan"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        /// <returns></returns>
        private CalendarDate GetCalendarDate(CalendarCellPosition cp, int colSpan, DateTime oldStartDate, DateTime oldEndDate)
        {
            DateTime newStartDate = new DateTime(cp.Date.Date.Year, cp.Date.Date.Month, cp.Date.Date.Day, (cp.ColumnIndex == 1) ? 12 : this.defaultStartTime, 0, 0);

            float nissuu = colSpan / 2f;
            var addday = ((cp.ColumnIndex + colSpan) / 2f) % 1 == 0 ? nissuu - 1 : nissuu;

            DateTime newEndDate = newStartDate.AddDays(addday);

            if (cp.ColumnIndex == 1) //startdateが午後
            {
                //span結果がちょうど割り切れている場合はaddDay&am                           
                newEndDate = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, (nissuu % 1 == 0) ? 12 : this.defaultEndTime, 0, 0);
            }
            else
            {
                //span結果がちょうど割り切れている場合はaddDay&pm
                newEndDate = new DateTime(newEndDate.Year, newEndDate.Month, newEndDate.Day, (nissuu % 1 == 0) ? this.defaultEndTime : 12, 0, 0);
            }

            var ret = new CalendarDate();
            ret.StartDate = newStartDate;
            ret.EndDate = newEndDate;
            return ret;
        }

        /// <summary>
        /// 開始日時、終了日時取得処理（複数指定）
        /// </summary>
        /// <remarks>
        /// 指定された複数のCalendarCellPositionを元に、開始日時～終了日時を生成します。
        /// </remarks>
        /// <param name="selectedCells">選択された複数のCalendarCellPosition</param>
        /// <returns>CalendarDate</returns>
        public CalendarDate CreateCalendarDate(ReadOnlyCollection<CalendarCellPosition> selectedCells)
        {
            var ret = new CalendarDate();

            var end = selectedCells.Count - 1;

            ret.StartDate = selectedCells[0].Date.AddHours(timeAmList[selectedCells[0].ColumnIndex]);
            ret.EndDate = selectedCells[end].Date.AddHours(timePmList[selectedCells[end].ColumnIndex]);

            return ret;        
        }

        /// <summary>
        /// 開始日時、終了日時取得処理（１日単位）
        /// </summary>
        /// <remarks>
        /// CalendarCellPositionを元に、１日単位の開始日時～終了日時を生成します。
        /// </remarks>
        /// <param name="cp">選択されたCalendarCellPosition</param>
        /// <returns>CalendarDate</returns>
        public CalendarDate CreateCalendarDate(CalendarCellPosition cp)
        {
            var ret = new CalendarDate();

            ret.StartDate = cp.Date.AddHours(timeAmList[cp.ColumnIndex]);
            ret.EndDate = cp.Date.AddHours(timePmList[cp.ColumnIndex]);

            return ret;
        }

        /// <summary>
        /// 拡大１スケジュール移動処理。
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        /// <returns></returns>
        public CalendarDate CreateCalendarDateMove(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            var span = CalculationColumnSpan(oldStartDate, oldEndDate);

            //拡大１の場合はスケジュール移動は固定
            return GetCalendarDate(cp, span, oldStartDate, oldEndDate);
        }

        /// <summary>
        /// 拡大１スケジュールペースト処理。
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        /// <returns></returns>
        public CalendarDate CreateCalendarDatePaste(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            var span = CalculationColumnSpan(oldStartDate, oldEndDate);

            //拡大１の場合はスケジュールペーストは固定
            return GetCalendarDate(cp, span, oldStartDate, oldEndDate);
        }

        /// <summary>
        /// カレンダーのColumnSpanの取得。
        /// </summary>
        /// <remarks>
        /// 指定された開始日時と終了日時を元に、カレンダーのColumnSpanを取得します。
        /// </remarks>
        /// <param name="startDate">開始日時</param>
        /// <param name="endDate">終了日時</param>
        /// <returns>ColumnSpan</returns>
        public int CalculationColumnSpan(DateTime startDate, DateTime endDate)
        {
            var minusTime = 0;

            if (startDate.TimeOfDay >= new TimeSpan(12, 00, 00))
            {
                minusTime++;
            }

            if (endDate.TimeOfDay <= new TimeSpan(12, 00, 00))
            {
                minusTime++;
            }

            return ((endDate.Date - startDate.Date).Days + 1) * 2 - minusTime;
        }

        /// <summary>
        /// スケジュールセル取得。
        /// </summary>
        /// <remarks>
        /// 指定されたCalendarRowと日時情報を元に、カレンダーのセルを特定します。
        /// </remarks>
        /// <param name="date">取得対象の日付</param>
        /// <param name="calendarRow">該当のカレンダー行情報</param>
        /// <returns>該当日時のセル</returns>
        public CalendarCell GetCalendarCell(DateTime date, CalendarRow calendarRow)
        {
            CalendarCell calendarCell;

            if (date.TimeOfDay >= new TimeSpan(12, 00, 00))
            {
                calendarCell = calendarRow.Cells[1];
            }
            else
            {
                calendarCell = calendarRow.Cells[0];
            }

            return calendarCell;
        }
    }
}
