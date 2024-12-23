using DevPlan.UICommon.Enum;
using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace DevPlan.UICommon.Utils.Calendar.Templates
{
    /// <summary>
    /// 拡大２テンプレートクラス。
    /// </summary>
    public class Expansion2CalendarTemplate : CalendarTemplate, ICalendarTemplate
    {
        /// <summary>
        /// ヘッダへ表示する時間。
        /// </summary>
        public static readonly List<int> timeHeaderList = new List<int> { 6, 8, 10, 12, 14, 16, 18, 20, 22 };
        
        /// <summary>
        /// 時間セルが所持している時間帯（開始時間）
        /// </summary>
        /// <remarks>
        /// timeHeaderListとセットで考えます（例えばtimeHeaderListが6の場合、timeStartListが6、timeEndListが7）
        /// 後々Dictionary等で管理予定。
        /// </remarks>
        private readonly List<int> timeStartList = new List<int> { 6, 7, 9, 11, 13, 15, 17, 19, 21 };

        /// <summary>
        /// 時間セルが所持している時間帯（終了時間）
        /// </summary>
        /// <remarks>
        /// timeHeaderListとセットで考えます（例えばtimeHeaderListが6の場合、timeStartListが6、timeEndListが7）
        /// 後々Dictionary等で管理予定。
        /// </remarks>
        private readonly List<int> timeEndList = new List<int> { 7, 9, 11, 13, 15, 17, 19, 21, 22 };
       
        /// <summary>
        /// デフォルトボーダーライン。
        /// </summary>
        private CalendarBorderLine cellBorderLine = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);
        
        /// <summary>
        /// デフォルトコンストラクタ。
        /// </summary>
        /// <remarks>
        /// 使用禁止です。
        /// </remarks>
        private Expansion2CalendarTemplate() : base() { return; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// カレンダースタイル情報を元にカレンダーテンプレートを作成します。
        /// </remarks>
        /// <param name="calendarStyle">カレンダースタイル情報</param>
        public Expansion2CalendarTemplate(CalendarStyle calendarStyle)
        {
            ColumnCount = timeHeaderList.Count();
            ColumnHeaderRowCount = 3;

            var cornerHeaderCell = CornerHeader[0, 0];
            cornerHeaderCell.CellType = new CalendarHeaderCellType()
            {
                FlatStyle = FlatStyle.Flat
            };
            cornerHeaderCell.RowSpan = ColumnHeaderRowCount;
            cornerHeaderCell.ColumnSpan = 2;
            cornerHeaderCell.CellStyle.Font = ControlFont.DefaultPFont.Font;

            for (int col = 0; col < timeHeaderList.Count(); col++)
            {
                var yearMonthCell = ColumnHeader[0, col];
                yearMonthCell.CellType = new CalendarHeaderCellType();
                yearMonthCell.DateFormat = "yyyy/MM";
                yearMonthCell.DateFormatType = CalendarDateFormatType.DotNet;
                yearMonthCell.AutoMergeMode = AutoMergeMode.Horizontal;
                yearMonthCell.CellStyleName = "headerStyle";

                var dayCell = ColumnHeader[1, col];
                dayCell.CellType = new CalendarHeaderCellType();
                if (col == 0)
                {
                    dayCell.DateFormat = "%d";
                    dayCell.DateFormatType = CalendarDateFormatType.DotNet;
                    dayCell.ColumnSpan = timeHeaderList.Count();
                    dayCell.CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    dayCell.CellStyle.BottomBorder = cellBorderLine;
                    dayCell.CellStyle.LeftBorder = cellBorderLine;
                    dayCell.CellStyle.RightBorder = cellBorderLine;
                    dayCell.CellStyle.TopBorder = cellBorderLine;
                    dayCell.CellStyleName = "defaultStyle";
                }

                var timeCell = ColumnHeader[2, col];
                timeCell.CellType = new CalendarHeaderCellType();
                timeCell.CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                if (col == 0)
                {
                    timeCell.CellStyle.LeftBorder = cellBorderLine;
                }
                else if (col == timeHeaderList.Count())
                {
                    timeCell.CellStyle.RightBorder = cellBorderLine;
                }

                timeCell.Value = timeHeaderList[col];
                timeCell.CellStyle.Font = new Font(ControlFont.DefaultFont.Font.Name, 9);
            }

            var rowHeaderTable = RowHeader;
            rowHeaderTable.ColumnCount = 2;
            rowHeaderTable.Columns[0].Width = 40;
            rowHeaderTable[0, 0].CellType = new CalendarHeaderCellType();
            rowHeaderTable.Columns[1].Width = calendarStyle.ScheduleRowHeaderWidth;
            rowHeaderTable[0, 1].CellType = new CalendarHeaderCellType();

            var width = 18 + calendarStyle.HorizontalLengthUpdate;
            if (width <= 0)
            {
                throw new Exception("カレンダーを表示できません。列の幅が0以下になる不正な値が設定されています。（拡大２レイアウト）");
            }
            for (int col = 0; col < 9; col++)
            {
                Content.Columns[col].Width = width;
            }

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
            int startDateHour = GetStartDateHour(oldStartDate.Hour);
            int endDateHour = GetEndDateHour(oldEndDate.Hour);

            DateTime hikakuOldStartDate = new DateTime(oldStartDate.Year, oldStartDate.Month, oldStartDate.Day, startDateHour, 0, 0);
            DateTime hikakuOldEndDate = new DateTime(oldEndDate.Year, oldEndDate.Month, oldEndDate.Day, endDateHour, 0, 0);

            var startDate = new DateTime(
                cp.Date.Date.Year,
                cp.Date.Date.Month,
                cp.Date.Date.Day,
                timeStartList[cp.ColumnIndex], 0, 0);

            var dayCount = (cp.ColumnIndex + colSpan) / 9;//開始時間（のセル）＋結合せるの長さ / 9（時間セル）　何日またいでいるか算出。
            if (((cp.ColumnIndex + colSpan) / 9f) % 1 == 0)// 小数点がない場合はぴったり（６時２２時）ということで -1。
            {
                dayCount = dayCount - 1;
            }
            var enddate = startDate.AddDays(dayCount);
            enddate = new DateTime(enddate.Year, enddate.Month, enddate.Day, this.timeEndList[(colSpan - ((9 * dayCount) - cp.ColumnIndex)) - 1], 0, 0);

            var ret = new CalendarDate();

            //比較した結果、変わりないようならば時間が違っても変更なしとみなす（２ｈくぎりなので、表現できるセルが同じでも時間が違う）
            ret.StartDate = (startDate != hikakuOldStartDate) ? startDate : oldStartDate;
            ret.EndDate = (enddate != hikakuOldEndDate) ? enddate : oldEndDate;

            return ret;
        }

        /// <summary>
        /// 拡大２用の開始時間を取得。
        /// </summary>
        /// <remarks>
        /// 開始時間リストにない場合は-1をする（例えば8時セルは7時、8時を表現できる）
        /// </remarks>
        /// <param name="hour"></param>
        /// <returns></returns>
        private int GetStartDateHour(int hour)
        {
            return (timeStartList.Contains(hour)) ? hour : hour - 1;
        }

        /// <summary>
        /// 拡大２用の終了時間を取得。
        /// </summary>
        /// <remarks>
        /// 終了時間リストにない場合は+1をする（例えば10時セルは終了10時、11時を表現できる）
        /// </remarks>
        /// <param name="hour"></param>
        /// <returns></returns>
        private int GetEndDateHour(int hour)
        {
            return (timeEndList.Contains(hour)) ? hour : hour + 1;
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

            ret.StartDate = selectedCells[0].Date.AddHours(timeStartList[selectedCells[0].ColumnIndex]);
            ret.EndDate = selectedCells[end].Date.AddHours(timeEndList[selectedCells[end].ColumnIndex]);

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
            
            ret.StartDate = cp.Date.AddHours(timeStartList[cp.ColumnIndex]);
            ret.EndDate = cp.Date.AddHours(timeEndList[cp.ColumnIndex]);

            return ret;
        }

        /// <summary>
        /// 拡大２移動処理。ペースト処理と同様です。
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        /// <returns></returns>
        public CalendarDate CreateCalendarDateMove(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            return GetCalendarMoveDate(cp, oldStartDate, oldEndDate);
        }

        /// <summary>
        /// 拡大２ペースト処理。移動処理と同様です。
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="oldStartDate"></param>
        /// <param name="oldEndDate"></param>
        /// <returns></returns>
        public CalendarDate CreateCalendarDatePaste(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            return GetCalendarMoveDate(cp, oldStartDate, oldEndDate);
        }

        /// <summary>
        /// 開始日時、終了日時取得処理
        /// </summary>
        /// <remarks>
        /// 渡されたCalendarCellPositionおよび変更前の日時（時間差算出の為）から
        /// 新しい開始日時、終了日時を作成します。
        /// 計算した結果、２２時を超えた（次の日にまたいだ）場合は
        /// デフォルトを２２時にします。
        /// </remarks>
        /// <returns>CalendarDate</returns>
        private CalendarDate GetCalendarMoveDate(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate)
        {
            var ret = this.CreateCalendarDate(cp);
            var timeSpan = oldEndDate - oldStartDate;

            ret.EndDate = ret.StartDate.Value.AddHours(timeSpan.TotalHours);

            if (ret.EndDate.Value.Date.AddHours(22) < ret.EndDate.Value)
            {
                ret.EndDate = ret.EndDate.Value.Date.AddHours(22);
            }
            else if (ret.EndDate.Value.Date.AddHours(6) >= ret.EndDate.Value)
            {
                ret.EndDate = ret.EndDate.Value.Date.AddDays(-1).AddHours(22);
            }

            return ret;
        }

        /// <summary>
        /// カレンダーのColumnSpanの取得。
        /// </summary>
        /// <remarks>
        /// 指定された開始日時と終了日時を元に、カレンダーのColumnSpanを取得します。
        /// （開始と終了の日数(*１日のセル数）　＋　(開始時間リスト数 - 開始時間のセルIndex) + (終了時間のセルIndex + 1））
        /// </remarks>
        /// <param name="startDate">開始日時</param>
        /// <param name="endDate">終了日時</param>
        /// <returns>ColumnSpan</returns>
        public int CalculationColumnSpan(DateTime startDate, DateTime endDate)
        {
            int startDateHour = GetStartDateHour(startDate.Hour);
            int endDateHour = GetEndDateHour(endDate.Hour);

            DateTime calendarStartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDateHour, 0, 0);
            DateTime calendarEndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDateHour, 0, 0);

            var day = timeStartList.Count() * ((calendarEndDate.Date - calendarStartDate.Date).Days - 1);
            return day + (timeStartList.Count - timeStartList.IndexOf(calendarStartDate.Hour)) + (timeEndList.IndexOf(calendarEndDate.Hour) + 1);
        }

        /// <summary>
        /// スケジュールセル取得。
        /// </summary>
        /// <remarks>
        /// 指定されたCalendarRowと日時情報を元に、カレンダーの開始セルを特定します。
        /// </remarks>
        /// <param name="date">取得対象の日付</param>
        /// <param name="calendarRow">該当のカレンダー行情報</param>
        /// <returns>該当日時のセル</returns>
        public CalendarCell GetCalendarCell(DateTime date, CalendarRow calendarRow)
        {
            int startDateHour = GetStartDateHour(date.Hour);
            return calendarRow.Cells[timeStartList.IndexOf(startDateHour)];
        }
    }
}
