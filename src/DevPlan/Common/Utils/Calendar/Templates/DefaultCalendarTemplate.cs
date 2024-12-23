using DevPlan.UICommon.Enum;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace DevPlan.UICommon.Utils.Calendar.Templates
{
    /// <summary>
    /// 標準レイアウト。
    /// </summary>
    /// <remarks>
    /// 標準カレンダーレイアウトです。日ごとになっており、時間の概念はありません。
    /// </remarks>
    public class DefaultCalendarTemplate : CalendarTemplate, ICalendarTemplate
    {
        /// <summary>
        /// 標準レイアウトヘッダへ表示する日付。
        /// </summary>
        private List<int> dayList = new List<int> { 5, 10, 15, 20, 25 };

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
        /// <remarks>
        /// 使用禁止です。
        /// </remarks>
        private DefaultCalendarTemplate() { return; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// カレンダースタイル情報を元にカレンダーテンプレートを作成します。
        /// </remarks>
        /// <param name="calendarStyle">カレンダースタイル情報</param>
        public DefaultCalendarTemplate(CalendarStyle calendarStyle)
        {
            this.defaultStartTime = calendarStyle.DefaultStartTime;
            this.defaultEndTime = calendarStyle.DefaultEndTime;

            ColumnHeaderRowCount = 2;

            var cornerHeaderCell = CornerHeader[0, 0];
            cornerHeaderCell.CellType = new CalendarHeaderCellType()
            {
                FlatStyle = FlatStyle.Flat
            };
            cornerHeaderCell.RowSpan = ColumnHeaderRowCount;
            cornerHeaderCell.ColumnSpan = 2;
            cornerHeaderCell.CellStyle.Font = ControlFont.DefaultPFont.Font;

            var yearMonthCell = ColumnHeader[0, 0];
            yearMonthCell.CellType = new CalendarHeaderCellType();
            yearMonthCell.DateFormat = "yyyy/MM";
            yearMonthCell.DateFormatType = CalendarDateFormatType.DotNet;
            yearMonthCell.AutoMergeMode = AutoMergeMode.Horizontal;
            yearMonthCell.CellStyleName = "headerStyle";

            var dayHeaderCell = ColumnHeader[1, 0];
            dayHeaderCell.CellType = new CalendarHeaderCellType();
            dayHeaderCell.DateFormat = "{Day}";
            dayHeaderCell.CellStyle.Alignment = CalendarGridContentAlignment.MiddleRight;
            dayHeaderCell.AutoMergeMode = AutoMergeMode.Horizontal;
            dayHeaderCell.CellStyleName = "headerStyle";
            
            var rowHeaderTable = RowHeader;
            rowHeaderTable.ColumnCount = 2;
            rowHeaderTable.Columns[0].Width = 40;
            rowHeaderTable[0, 0].CellType = new CalendarHeaderCellType();
            rowHeaderTable.Columns[1].Width = calendarStyle.ScheduleRowHeaderWidth;
            rowHeaderTable[0, 1].CellType = new CalendarHeaderCellType();

            var width = 20 + calendarStyle.HorizontalLengthUpdate;
            if (width <= 0)
            {
                throw new Exception("カレンダーを表示できません。列の幅が0以下になる不正な値が設定されています。（標準レイアウト）");
            }
            Content.Columns[0].Width = width;

            if (RowCount > 0)
            {
                RemoveRow(0, RowCount);
            }

            Content.CellStyle.Font = ControlFont.GetDefaultFont(float.Parse(calendarStyle.FontSize));
        }

        /// <summary>
        /// ボーダー作成。
        /// </summary>
        /// <remarks>
        /// 当レイアウト用のボーダーを生成します。
        /// ※日付が設定されている状態でないとボーダーの位置が決定できないため、初期化とは別に用意してあります。
        /// </remarks>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="header"></param>
        /// <param name="content"></param>
        public void SetBorder(DateTime startDate, DateTime endDate, CalendarColumnHeader header, CalendarContent content)
        {
            var dateRowHeaderCount = 0;
            foreach (DateTime day in EachDay(startDate, endDate))
            {
                var headerCell1 = header[dateRowHeaderCount][1, 0];

                for (int i = 0; i < dayList.Count; i++)
                {
                    headerCell1.DateFormatType = CalendarDateFormatType.DotNet;

                    if (day.Day <= dayList[i])
                    {
                        headerCell1.DateFormat = dayList[i].ToString();
                        break;
                    }
                    else
                    {
                        headerCell1.DateFormat = " ";
                    }
                }

                dateRowHeaderCount++;

                if (day.Day != 1 && dayList.Contains(day.Day - 1) == false)
                {
                    content[new DateTime(day.Year, day.Month, day.Day)].CellStyle.LeftBorder = new CalendarBorderLine(Color.Empty, BorderLineStyle.None);
                }
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        /// <summary>
        /// スケジュール変更後の計算処理。
        /// </summary>
        /// <remarks>
        /// CalendarCellPositionをもとに、変更されたスケジュールの開始日時と終了日時を設定します。
        /// </remarks>
        /// <param name="cp"></param>
        /// <param name="currentCell"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        public CalendarDate CreateCalendarDate(CalendarCellPosition cp, int colSpan, DateTime oldStartDate, DateTime oldEndDate)
        {
            var ret = new CalendarDate();
            
            if (oldStartDate.Date != cp.Date.Date || ((oldEndDate.Date - oldStartDate.Date).Days + 1) != colSpan)
            {
                ret.StartDate = cp.Date.Date.AddHours(oldStartDate.Hour);
                ret.EndDate = cp.Date.AddDays(colSpan - 1).AddHours(oldEndDate.Hour);                
            }
            else
            {
                ret.StartDate = oldStartDate;
                ret.EndDate = oldEndDate;
            }

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

            ret.StartDate = selectedCells[0].Date.AddHours(this.defaultStartTime);
            ret.EndDate = selectedCells[end].Date.AddHours(this.defaultEndTime);

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

            ret.StartDate = cp.Date.AddHours(this.defaultStartTime);
            ret.EndDate = cp.Date.AddHours(this.defaultEndTime);

            return ret;
        }

        public CalendarDate CreateCalendarDateMove(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            //標準１レイアウトの移動の場合は時間は「登録通り」spanは移動後のSPAN。
            var span = CalculationColumnSpan(oldStartDate, oldEndDate);
            CalendarDate ret = this.CreateCalendarDate(cp, span, oldStartDate, oldEndDate);

            ret.StartDate = ret.StartDate.Value.Date.AddHours(oldStartDate.Hour);
            ret.EndDate = ret.EndDate.Value.Date.AddHours(oldEndDate.Hour);

            return ret;
        }

        public CalendarDate CreateCalendarDatePaste(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            //標準１レイアウトのペーストの場合はデフォルト。
            var span = CalculationColumnSpan(oldStartDate, oldEndDate);
            CalendarDate ret = this.CreateCalendarDate(cp, span, oldStartDate, oldEndDate);

            ret.StartDate = ret.StartDate.Value.Date.AddHours(this.defaultStartTime);
            ret.EndDate = ret.EndDate.Value.Date.AddHours(this.defaultEndTime);

            return ret;
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
            return (endDate.Date - startDate.Date).Days + 1;
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
            return calendarRow.Cells[0];
        }
    }
}
