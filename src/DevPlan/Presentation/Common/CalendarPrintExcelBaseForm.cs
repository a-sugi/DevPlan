using DevPlan.Presentation.Base;
using DevPlan.Presentation.Properties;
using DevPlan.UICommon;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Utils.Calendar;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.Common
{
    public partial class CalendarPrintExcelBaseForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "Excel出力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        protected DateTime StartDate { get; private set; }

        protected DateTime EndDate { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面初期化を行います。
        /// </remarks>
        public CalendarPrintExcelBaseForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 選択期間チェック。
        /// </summary>
        /// <remarks>
        /// 選択した期間が１年以内か確認を行います。
        /// </remarks>
        /// <returns>１年期間内の場合はTrue、期間外の場合はFalseを返却</returns>
        protected bool CanOutputPeriod()
        {
            if (this.StartDateTimePicker.Value == null || this.EndDateTimePicker.Value == null) { return false; }

            DateTime startDate = DateTime.Parse(this.StartDateTimePicker.Value.ToString());
            DateTime endDate = DateTime.Parse(this.EndDateTimePicker.Value.ToString());

            var ret = (startDate.AddYears(1).Date >= endDate.Date);

            if (ret)
            {
                this.StartDate = startDate;
                this.EndDate = endDate;
            }

            return ret;
        }

        /// <summary>
        /// Excelファイル出力。
        /// </summary>
        /// <remarks>
        /// 指定された出力フォーマット、項目、スケジュールと検索条件を元にExcelへ情報出力を行います。
        /// </remarks>
        /// <param name="templateType">出力フォーマット</param>
        /// <param name="outItem">項目リスト</param>
        /// <param name="outSchedule">スケジュール</param>
        /// <param name="closeItemDic"></param>
        public void Output(CalendarTemplateTypeSafeEnum templateType, Dictionary<long, string> outItem, Dictionary<long, List<ExcelCalendarInfo>> outSchedule, Dictionary<long, DateTime> closeItemDic)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel ブック (*.xlsx)|*.xlsx;";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var stream = new ExcelCalendarTemplateFactory().CreateFileStream(templateType);

                    using (var excel = new ExcelPackage(stream))
                    {
                        var sheet = excel.Workbook.Worksheets["Sheet1"];
                        
                        var dayList = EachDay(this.StartDate, this.EndDate);

                        //日のみ一括挿入。
                        if (templateType == CalendarTemplateTypeSafeEnum.DEFAULT)
                        {
                            sheet.InsertColumn(3, EachDay(this.StartDate, this.EndDate).Count() - 1, 2);
                        }

                        int outputMonth = 0;
                        int startCel = 0;
                        int outputCel = 2;

                        foreach (DateTime day in dayList)//日付ループ
                        {
                            #region 日付のいろいろ（日付設定とか色設定とか）

                            if (outputCel != 2)//1日目のときはコピーする必要なし
                            {
                                if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2)
                                {
                                    sheet.Cells[1, 2, 4, 10].Copy(sheet.Cells[1, outputCel]);//1行目の2列目から4行目の10列めをコピーして1行目のoutputCel列目に張り付け
                                }
                                else if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION1)
                                {
                                    sheet.Cells[1, 2, 3, 3].Copy(sheet.Cells[1, outputCel]);
                                }
                            }

                            sheet.Cells[2, outputCel].Value = day.Day;//先に日。

                            //if (day == this.StartDate.Date || day.Day == 1)
                            //{
                            //    startCel = outputCel;
                            //}
                            //if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2)
                            //{
                            //    outputCel += 9;
                            //}
                            //else if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION1)
                            //{
                            //    outputCel += 2;
                            //}
                            //else
                            //{
                            //    outputCel++;
                            //}

                            if (outputMonth != 0 && outputMonth != day.Month || dayList.Last() == day)
                            {                
                                // TODO : a-naka 修正予定。
                                if(this.StartDate.Date < this.EndDate.Date)
                                {
                                    sheet.Cells[1, startCel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    sheet.Cells[1, startCel].Value = day.AddDays(-1).ToString("yyyy/MM");
                                    sheet.Cells[1, startCel, 1, (dayList.Last() == day && this.StartDate.Date != this.EndDate.Date) ? outputCel : outputCel - 1].Merge = true;
                                }
                            }

                            outputMonth = day.Month;
                            if (day == this.StartDate.Date || day.Day == 1)
                            {
                                startCel = outputCel;
                            }

                            if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2)
                            {
                                outputCel += 9;
                            }
                            else if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION1)
                            {
                                outputCel += 2;
                            }
                            else
                            {
                                outputCel++;
                            }

                            #endregion
                        }

                        //行の設定
                        var plusHeaderCount = (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2) ? 4 : 3;

                        int huetabun = 0;

                        int rowCounter = 0;
                        foreach (var iiii in outItem)
                        {
                            List<int> syuturyokusaki;
                            if (outSchedule.ContainsKey(iiii.Key) == false)
                            {
                                syuturyokusaki = new List<int>();
                                syuturyokusaki.Add(1);
                            }
                            else
                            {
                                var scheduleList = outSchedule[iiii.Key].OrderBy(x => x.RowNo);

                                syuturyokusaki = scheduleList.Select(v => v.RowNo).Distinct().ToList();
                                if (syuturyokusaki.Count == 0) { syuturyokusaki.Add(1); }
                            }

                            var row = rowCounter + plusHeaderCount + huetabun + 1;//+1というのは行ヘッダレイアウトのコピーをしたいため。最後にレイアウト行を削除する予定。

                            sheet.InsertRow(row, syuturyokusaki.Count, plusHeaderCount);
                            
                            #region 行設定

                            sheet.Cells[row, 1].Value = iiii.Value;

                            if (syuturyokusaki.Count > 1)
                            {
                                sheet.Cells[row, 1, row + syuturyokusaki.Count - 1, 1].Merge = true;

                                huetabun += syuturyokusaki.Count - 1;
                            }

                            if (closeItemDic.ContainsKey(iiii.Key))
                            {
                                var closeDate = closeItemDic[iiii.Key];

                                sheet.Cells[row, (closeDate.Date - this.StartDate.Date).Days * 9 + 2].Value = "本車両の使用期限を過ぎています";

                                var cell = sheet.Cells[
                                    row, (closeDate.Date - this.StartDate.Date).Days * 9 + 2,
                                    row + syuturyokusaki.Count - 1, ((this.EndDate - this.StartDate).Days + 1) * 9 + 1];

                                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 0, 0));
                                cell.Style.Font.Color.SetColor(Color.White);
                                cell.Style.ShrinkToFit = false;
                                cell.Style.WrapText = false;
                                cell.Style.Border.Bottom.Style = ExcelBorderStyle.None;
                                cell.Style.Border.Left.Style = ExcelBorderStyle.None;
                                cell.Style.Border.Right.Style = ExcelBorderStyle.None;
                                cell.Style.Border.Top.Style = ExcelBorderStyle.None;
                            }

                            #endregion

                            rowCounter++;
                        }
                        //終了後、行ヘッダレイアウトは削除。
                        sheet.DeleteRow(plusHeaderCount);

                        //以下はスケジュールの設定

                        var rowPlus = 0;//行の増加分を記録。

                        rowCounter = 0;
                        foreach (var iiii in outItem)
                        {
                            List<int> syuturyokusaki;
                            if (outSchedule.ContainsKey(iiii.Key) == false)
                            {
                                syuturyokusaki = new List<int>();
                                syuturyokusaki.Add(1);
                            }
                            else
                            {
                                var scheduleList = outSchedule[iiii.Key].OrderBy(x => x.RowNo);

                                syuturyokusaki = scheduleList.Select(v => v.RowNo).Distinct().ToList();
                                if (syuturyokusaki.Count == 0) { syuturyokusaki.Add(1); }

                                var rowNoCount = 0;
                                var oldRowNo = 0;

                                foreach (var schedule in scheduleList)
                                {
                                    DateTime scheduleStartDate = schedule.StartDate;
                                    DateTime scheduleEndDate = schedule.EndDate;

                                    #region 開始日と終了日を調整。表示できない範囲はそれぞれ開始日、終了日とする。

                                    if (scheduleStartDate.Date < this.StartDate)
                                    {
                                        scheduleStartDate = new DateTime(this.StartDate.Year, this.StartDate.Month, this.StartDate.Day).AddHours(6);
                                    }
                                    if (scheduleEndDate > this.EndDate)
                                    {
                                        scheduleEndDate = new DateTime(this.EndDate.Year, this.EndDate.Month, this.EndDate.Day).AddHours(22);
                                    }

                                    #endregion

                                    if (closeItemDic.ContainsKey(iiii.Key))
                                    {
                                        var closeDate = closeItemDic[iiii.Key];

                                        if (closeDate <= scheduleStartDate) { continue; }//最終予約可能日以上の開始日だったら非表示（次のスケジュールへ）
                                    }

                                    if (oldRowNo == 0) { oldRowNo = schedule.RowNo; }
                                    if (oldRowNo != schedule.RowNo)
                                    {
                                        oldRowNo = schedule.RowNo;
                                        rowNoCount++;//RowNoが変わったタイミングで+1する。
                                    }

                                    //row///////////////////
                                    //var row = (rowCounter + rowPlus) + plusHeaderCount + syuturyokusaki[rowNoCount] - 1;
                                    var row = (rowCounter + rowPlus) + plusHeaderCount + syuturyokusaki.IndexOf(schedule.RowNo);

                                    //col///////////////////
                                    int col = 0;
                                    if (templateType == CalendarTemplateTypeSafeEnum.DEFAULT)
                                    {
                                        col = (scheduleStartDate.Date - this.StartDate.Date).Days + 2;
                                    }
                                    if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION1)
                                    {
                                        if (scheduleStartDate.TimeOfDay >= new TimeSpan(12, 00, 00))
                                        {
                                            col = (scheduleStartDate.Date - this.StartDate).Days * 2 + 3;
                                        }
                                        else
                                        {
                                            col = (scheduleStartDate.Date - this.StartDate).Days * 2 + 2;
                                        }
                                    }
                                    if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2)
                                    {
                                        col = ((scheduleStartDate.Date - this.StartDate.Date).Days + (8 * (scheduleStartDate.Date - this.StartDate.Date).Days)) + 2;

                                        List<int> timeStartList = new List<int> { 6, 8, 10, 12, 14, 16, 18, 20, 22 };
                                        col = col + ((timeStartList.Contains(scheduleStartDate.Hour)) ?
                                            timeStartList.IndexOf(scheduleStartDate.Hour) : timeStartList.IndexOf(scheduleStartDate.Hour + 1));
                                    }

                                    //マージ////////////////////////////
                                    int mergeCount = 0;
                                    if (templateType == CalendarTemplateTypeSafeEnum.DEFAULT)
                                    {
                                        mergeCount = (scheduleEndDate.Date - scheduleStartDate.Date).Days;
                                    }
                                    else if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION1)
                                    {
                                        var minusTime = 0;
                                        if (scheduleStartDate.TimeOfDay >= new TimeSpan(12, 00, 00))
                                        {
                                            minusTime++;
                                        }
                                        if (scheduleEndDate.TimeOfDay <= new TimeSpan(12, 00, 00))
                                        {
                                            minusTime++;
                                        }
                                        mergeCount = ((scheduleEndDate.Date - scheduleStartDate.Date).Days) * 2 + 1 - minusTime;
                                    }
                                    else if (templateType == CalendarTemplateTypeSafeEnum.EXPANSION2)
                                    {
                                        //テンプレートのほうと同じ出し方。

                                        List<int> timeStartList = new List<int> { 6, 8, 10, 12, 14, 16, 18, 20, 22 };
                                        List<int> timeEndList = new List<int> { 7, 9, 11, 13, 15, 17, 19, 21, 22 };

                                        #region 時間を拡大２用にする

                                        DateTime hikakuOldStartDate;
                                        hikakuOldStartDate = new DateTime(scheduleStartDate.Year, scheduleStartDate.Month, scheduleStartDate.Day,
                                            (timeStartList.Contains(scheduleStartDate.Hour)) ? scheduleStartDate.Hour : scheduleStartDate.Hour + 1, 0, 0);

                                        DateTime hikakuOldEndDate;
                                        hikakuOldEndDate = new DateTime(scheduleEndDate.Year, scheduleEndDate.Month, scheduleEndDate.Day,
                                            (timeEndList.Contains(scheduleEndDate.Hour)) ? scheduleEndDate.Hour : scheduleEndDate.Hour + 1, 0, 0);

                                        #endregion

                                        var minusCells = 3 * (hikakuOldEndDate.Date - hikakuOldStartDate.Date).Days;//１日と１日の間は３セル分あるので、dayCountから除く。（３日だったらその間の２回分除く）
                                        var totalHours = ((int)(hikakuOldEndDate - hikakuOldStartDate).TotalHours) / 2;

                                        mergeCount = (totalHours - minusCells);
                                    }

                                    sheet.Cells[row, col].Value = schedule.SubTitle;
                                    
                                    //背景色付け                        
                                    sheet.Cells[row, col, row, col + mergeCount].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    sheet.Cells[row, col, row, col + mergeCount].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(248, 203, 173));

                                    using(var range = sheet.Cells[row, col, row, col + mergeCount])
                                    {
                                        range.Style.Border.Left.Style = ExcelBorderStyle.None;
                                        range.Style.Border.Right.Style = ExcelBorderStyle.None;
                                    }

                                    //コメントも付ける。
                                    if(sheet.Cells[row, col].Comment == null)
                                    {
                                        var comment = sheet.Cells[row, col].AddComment(schedule.ToolTip, "test");
                                        comment.Visible = true;
                                        comment.Font.FontName = "MS P ゴシック";
                                        comment.Font.Size = 9;
                                        comment.AutoFit = true;
                                        comment.From.Column = col + mergeCount;
                                        comment.To.Column = col + mergeCount;
                                    }
                                }
                            }

                            if (syuturyokusaki.Count > 1)
                            {
                                //行の増加分を記録しておく。
                                rowPlus += (syuturyokusaki.Count - 1);
                            }

                            rowCounter++;
                        }

                        sheet.HeaderFooter.OddFooter.RightAlignedText =
                            "社外転用禁止\n" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " " +
                            UICommon.Dto.SessionDto.SectionCode + " " + UICommon.Dto.SessionDto.UserName;

                        var saveFilePath = dialog.FileName;
                        if (FileUtil.IsFileLocked(saveFilePath) == true)
                        {
                            Messenger.Warn(UICommon.Properties.Resources.KKM00044);
                            return;
                        }
                        
                        excel.SaveAs(new FileInfo(saveFilePath));
                    }
                }
            }
        }
        
        //どこかに移す。。
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        /// <summary>
        /// Shown処理。
        /// </summary>
        /// <remarks>
        /// 日付コントロールを初期化します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarPrintExcelBaseForm_Shown(object sender, EventArgs e)
        {
            this.StartDateTimePicker.Value = DateTime.Today;
            this.EndDateTimePicker.Value = DateTime.Today.AddMonths(1);
        }
    }

    /// <summary>
    /// Excelへ出力する情報。
    /// </summary>
    public class ExcelCalendarInfo
    {
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public ExcelCalendarInfo()
        {
            return;
        }

        public int RowNo { get; internal set; }
        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public string SubTitle { get; internal set; }
        public string ToolTip { get; internal set; }
    }

    /// <summary>
    /// カレンダーテンプレートファクトリークラス。
    /// </summary>
    /// <remarks>
    /// カレンダーテンプレートオブジェクトを管理します。
    /// </remarks>
    public class ExcelCalendarTemplateFactory
    {
        public MemoryStream CreateFileStream(CalendarTemplateTypeSafeEnum type)
        {
            if (type == CalendarTemplateTypeSafeEnum.DEFAULT)
            {
                return new MemoryStream(Resources.DefaultCalendarTemplate);
            }
            else if (type == CalendarTemplateTypeSafeEnum.EXPANSION1)
            {
                return new MemoryStream(Resources.Expansion1CalendarTemplate);
            }
            else if (type == CalendarTemplateTypeSafeEnum.EXPANSION2)
            {
                return new MemoryStream(Resources.Expansion2CalendarTemplate);
            }
            else
            {
                throw new Exception("Excelリソースが見つかりませんでした。");
            }
        }

        //public IExcelTemplate CreateExcelTemplate(CalendarTemplateTypeSafeEnum type)
        //{
        //    if (type == CalendarTemplateTypeSafeEnum.DEFAULT)
        //    {
        //        return new DefaultExcelTemplate();
        //    }
        //    else if (type == CalendarTemplateTypeSafeEnum.EXPANSION1)
        //    {
        //        return new Expansion1ExcelTemplate();
        //    }
        //    else if (type == CalendarTemplateTypeSafeEnum.EXPANSION2)
        //    {
        //        return new Expansion2ExcelTemplate();
        //    }
        //    else
        //    {
        //        throw new Exception("Excelリソースが見つかりませんでした。");
        //    }
        //}
    }

    //public class Expansion2ExcelTemplate : IExcelTemplate
    //{
    //    public void SetHeader(ExcelWorksheet sheet, IEnumerable<DateTime> dateList)
    //    {

    //    }
    //}

    //public interface IExcelTemplate
    //{
    //    /// <summary>
    //    /// ヘッダ生成。
    //    /// </summary>
    //    /// <param name="sheet"></param>
    //    /// <param name="dateList"></param>
    //    void SetHeader(ExcelWorksheet sheet, IEnumerable<DateTime> dateList);
    //}

    //public class Expansion1ExcelTemplate : IExcelTemplate
    //{
    //}

    //public class DefaultExcelTemplate : IExcelTemplate
    //{

    //}
}
