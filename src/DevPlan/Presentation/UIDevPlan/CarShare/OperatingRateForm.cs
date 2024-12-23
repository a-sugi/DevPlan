using DevPlan.Presentation.Base;
using DevPlan.UICommon;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア管理　稼働率算出フォーム。
    /// </summary>
    public partial class OperatingRateForm : BaseSubForm
    {
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "稼働率算出"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <remarks>
        /// 画面を初期化します。
        /// </remarks>
        public OperatingRateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 稼働率算出ファイル作成。
        /// </summary>
        /// <remarks>
        /// 指定された年月を元にデータを取得し、稼働率算出ファイルへデータを出力します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            if(this.PrintYerarMonthDateTimePicker == null)
            {
                Messenger.Warn(string.Format(UICommon.Properties.Resources.KKM00001, "対象年月"));
                return;
            }

            var startDate = new DateTime(this.PrintYerarMonthDateTimePicker.Value.Year,
                this.PrintYerarMonthDateTimePicker.Value.Month,
                1, 0, 0, 0);

            var paramCond = new CarShareManagementRatePrintSearchModel()
            {
                SearchStartDate = startDate,
                SearchEndDate = startDate.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59)
            };

            var res = HttpUtil.GetResponse<CarShareManagementRatePrintSearchModel, CarShareManagementRatePrintOutModel>(
                ControllerType.CarShareManagementRatePrint, paramCond);

            var list = new List<CarShareManagementRatePrintOutModel>();
            if (res == null || res.Status != Const.StatusSuccess)
            {
                Messenger.Warn(UICommon.Properties.Resources.KKM00005);
                return;
            }
            else
            {
                list = res.Results.ToList();
            }
            
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel ブック (*.xlsx)|*.xlsx;";
                dialog.FileName = "稼働率算出（" + this.PrintYerarMonthDateTimePicker.Value.ToString("yyyyMM") + "）_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using (var excel = new ExcelPackage(new MemoryStream(Properties.Resources.CarShareManagementRateTemplate)))
                    {
                        var sheet = excel.Workbook.Worksheets[1];

                        sheet.Cells[1, 2].Value = sheet.Cells[1, 2].Value.ToString().Replace("{yyyy}", startDate.Year.ToString()).Replace("{MM}", startDate.Month.ToString());
                        sheet.Cells[2, 2].Value = sheet.Cells[2, 2].Value.ToString().Replace("{m}", startDate.Month.ToString()).Replace("{d1}", "1").Replace("{d2}", paramCond.SearchEndDate.Day.ToString());

                        int rowCount = 4;
                        foreach (var data in list)
                        {
                            sheet.Cells[rowCount, 3].Value = data.RateDate;
                            sheet.Cells[rowCount, 4].Value = data.KadouCount;
                            sheet.Cells[rowCount, 5].Value = data.HoyuuCount;
                            sheet.Cells[rowCount, 6].Value = data.KadouRate;
                            sheet.Cells[rowCount, 7].Value = data.KadouGunmaCount;
                            sheet.Cells[rowCount, 8].Value = data.HoyuuGunmaCount;
                            sheet.Cells[rowCount, 9].Value = data.KadouGunmaRate;
                            sheet.Cells[rowCount, 10].Value = data.KadouSKCCount;
                            sheet.Cells[rowCount, 11].Value = data.HoyuuSKCCount;
                            sheet.Cells[rowCount, 12].Value = data.KadouSKCRate;

                            rowCount++;
                        }

                        // 時間があったらCustomExcelPackage : IDisposableといった感じでExcelPackageをラップ
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
    }

    /// <summary>
    /// 稼働率算出取得データ格納クラス。
    /// </summary>
    public class CarShareManagementRatePrintOutModel
    {
        /// <summary>
        /// 稼働率年月（日）
        /// </summary>
        public DateTime RateDate { get; set; }

        /// <summary>
        /// 稼働台数（全体）
        /// </summary>
        public int KadouCount { get; set; }

        /// <summary>
        /// 保有台数（全体）
        /// </summary>
        public int HoyuuCount { get; set; }

        /// <summary>
        /// 稼働率（全体）
        /// </summary>
        public double KadouRate { get; set; }

        /// <summary>
        /// 稼働台数（群馬）
        /// </summary>
        public int KadouGunmaCount { get; set; }

        /// <summary>
        /// 保有台数（群馬）
        /// </summary>
        public int HoyuuGunmaCount { get; set; }

        /// <summary>
        /// 稼働率（群馬）
        /// </summary>
        public double KadouGunmaRate { get; set; }

        /// <summary>
        /// 稼働台数（SKC）
        /// </summary>
        public int KadouSKCCount { get; set; }

        /// <summary>
        /// 保有台数（SKC）
        /// </summary>
        public int HoyuuSKCCount { get; set; }

        /// <summary>
        /// 稼働率（SKC）
        /// </summary>
        public double KadouSKCRate { get; set; }
    }

    /// <summary>
    /// カーシェア管理稼働率算出検索クラス。
    /// </summary>
    [Serializable]
    public class CarShareManagementRatePrintSearchModel
    {
        /// <summary>検索対象開始日</summary>
        public DateTime SearchStartDate { get; set; }

        /// <summary>検索対象終了日</summary>
        public DateTime SearchEndDate { get; set; }
    }
}
