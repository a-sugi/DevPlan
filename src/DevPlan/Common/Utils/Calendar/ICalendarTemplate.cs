using GrapeCity.Win.CalendarGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevPlan.UICommon.Dto;
using System.Collections.ObjectModel;

namespace DevPlan.UICommon.Utils.Calendar
{
    /// <summary>
    /// カレンダーレイアウトテンプレートインターフェース。
    /// </summary>
    public interface ICalendarTemplate
    {
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
        CalendarDate CreateCalendarDate(CalendarCellPosition cp, int colSpan, DateTime oldStartDate, DateTime oldEndDate);

        /// <summary>
        /// 開始日時、終了日時取得処理（複数指定）
        /// </summary>
        /// <remarks>
        /// 指定された複数のCalendarCellPositionを元に、開始日時～終了日時を生成します。
        /// </remarks>
        /// <param name="selectedCells">選択された複数のCalendarCellPosition</param>
        /// <returns>CalendarDate</returns>
        CalendarDate CreateCalendarDate(ReadOnlyCollection<CalendarCellPosition> selectedCells);

        /// <summary>
        /// 開始日時、終了日時取得処理（１日単位）
        /// </summary>
        /// <remarks>
        /// CalendarCellPositionを元に、１日単位の開始日時～終了日時を生成します。
        /// </remarks>
        /// <param name="cp">選択されたCalendarCellPosition</param>
        /// <returns>CalendarDate</returns>
        CalendarDate CreateCalendarDate(CalendarCellPosition cp);

        /// <summary>
        /// 開始日時、終了日時取得処理（TimeSpan利用）
        /// </summary>
        /// <remarks>
        /// 渡されたCalendarCellPositionおよび開始日時、終了日時を元に、
        /// 新しい開始日時、終了日時を作成します。
        /// 計算した結果、終了日時がカレンダーで表現できない時間帯となった場合は
        /// デフォルトを２２時にします。
        /// </remarks>
        /// <param name="cp">開始日時にあたるCalendarCellPosition</param>
        /// <param name="oldStartDate">変更前開始日時</param>
        /// <param name="oldEndDate">変更後開始日時</param>
        /// <returns>CalendarDate</returns>
        CalendarDate CreateCalendarDateMove(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate);

        CalendarDate CreateCalendarDatePaste(CalendarCellPosition cp, DateTime oldStartDate, DateTime oldEndDate);

        /// <summary>
        /// カレンダーのColumnSpanの取得。
        /// </summary>
        /// <remarks>
        /// 指定された開始日時と終了日時を元に、カレンダーのColumnSpanを取得します。
        /// </remarks>
        /// <param name="startDate">開始日時</param>
        /// <param name="endDate">終了日時</param>
        /// <returns>ColumnSpan</returns>
        int CalculationColumnSpan(DateTime startDate, DateTime endDate);

        /// <summary>
        /// スケジュールセル取得。
        /// </summary>
        /// <remarks>
        /// 指定されたCalendarRowと日時情報を元に、カレンダーのセルを特定します。
        /// </remarks>
        /// <param name="date">取得対象の日付</param>
        /// <param name="calendarRow">該当のカレンダー行情報</param>
        /// <returns>該当日時のセル</returns>
        CalendarCell GetCalendarCell(DateTime date, CalendarRow calendarRow);
    }

    public class CalendarDate
    {
        /// <summary>
        /// スケジュール変更後の開始日時。
        /// </summary>
        /// <remarks>
        /// 変更を行った後の開始日時を保存する場合は当プロパティを参照してください。
        /// </remarks>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// スケジュール変更後の終了日時。
        /// </summary>
        /// <remarks>
        /// 変更を行った後の終了日時を保存する場合は当プロパティを参照してください。
        /// </remarks>
        public DateTime? EndDate { get; set; }
    }
}
