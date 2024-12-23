using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevPlan.UICommon.Dto;
using System.Drawing;
using System.Drawing.Printing;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.Reports
{
    /// <summary>
    /// 送り状
    /// </summary>
    public class Invoice : BaseReports
    {
        /// <summary>
        /// 定期便マスタ情報
        /// </summary>
        List<DeparturePoint> departurePointList;

        /// <summary>
        /// 印刷データ
        /// </summary>
        TruckScheduleModel printData;

        /// <summary>
        /// 印刷実行
        /// </summary>
        /// <param name="departurePointList"></param>
        /// <param name="printData"></param>
        internal void Print(List<DeparturePoint> departurePointList, TruckScheduleModel printData)
        {
            using (PrintDocument doc = new PrintDocument())
            {
                doc.DocumentName = "送り状_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                doc.DefaultPageSettings.Landscape = true;

                foreach (PaperSize ps in doc.PrinterSettings.PaperSizes)
                {
                    if (ps.Kind == PaperKind.A4)
                    {
                        doc.DefaultPageSettings.PaperSize = ps;
                        break;
                    }
                }
                this.printData = printData;
                this.departurePointList = departurePointList;

                doc.PrintPage += new PrintPageEventHandler(doc_PrintInvoice);
                doc.Print();
            }
        }

        /// <summary>
        /// 送り状オブジェクト作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doc_PrintInvoice(object sender, PrintPageEventArgs e)
        {
            Pen p = new Pen(Color.Black, 5);

            //黒い枠線
            e.Graphics.DrawRectangle(p, 60, 52, 1050, 700);

            //文字フォーマット
            StringFormat format = new StringFormat(StringFormat.GenericTypographic);
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            // トラック名
            e.Graphics.DrawString(printData.車両名,
                GetPrintFont(70),
                Brushes.Black, 570, 150, format);

            //トラック名の下線
            e.Graphics.DrawLine(p, 60, 240, 1110, 240);
            e.Graphics.DrawLine(p, 60, 410, 1110, 410);
            e.Graphics.DrawLine(p, 60, 610, 1110, 610);

            Pen p2 = new Pen(Color.Black, 1);
            e.Graphics.DrawLine(p2, 557, 240, 557, 610);

            //搬送日
            e.Graphics.DrawString("搬送日", GetPrintFont(23), Brushes.Black, 70, 260);
            e.Graphics.DrawString(printData.予約開始時間.Value.ToString("yyyy年M月d日(ddd)"),
                GetPrintFont(30), Brushes.Black, 145, 310);

            //出発時間
            e.Graphics.DrawString("出発時間", GetPrintFont(23), Brushes.Black, 560, 260);
            e.Graphics.DrawString(printData.DEPARTURE_TIME + " " +
                departurePointList.Where(x =>
                x.TruckId == printData.トラック_ID &&
                x.DepartureTime == printData.DEPARTURE_TIME).First().Text5,
                GetPrintFont(30), Brushes.Black, 700, 310);

            StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);

            //発送者タイトル
            e.Graphics.DrawString("発送者", GetPrintFont(32), Brushes.Black, 70, 440, sf);
            e.Graphics.DrawLine(p2, 140, 410, 140, 610);
            e.Graphics.DrawLine(p2, 230, 410, 230, 610);
            e.Graphics.DrawLine(p2, 234, 410, 234, 610);
            e.Graphics.DrawLine(p2, 140, 510, 555, 510);//発送者の横線

            //受領者タイトル
            e.Graphics.DrawString("受領者", GetPrintFont(32), Brushes.Black, 570, 440, sf);
            e.Graphics.DrawLine(p2, 640, 410, 640, 610);
            e.Graphics.DrawLine(p2, 730, 410, 730, 610);
            e.Graphics.DrawLine(p2, 734, 410, 734, 610);
            e.Graphics.DrawLine(p2, 640, 510, 1110, 510);//受領者の横線

            //発送者名
            e.Graphics.DrawString("所属", GetPrintFont(22), Brushes.Black, 146, 430);
            e.Graphics.DrawString("氏名", GetPrintFont(22), Brushes.Black, 146, 460);
            e.Graphics.DrawString(printData.ShipperRecipientUserList[0].発送者名,
                GetPrintFont(22),
                Brushes.Black, 240, 420);

            //受領者名
            e.Graphics.DrawString("所属", GetPrintFont(22), Brushes.Black, 650, 430);
            e.Graphics.DrawString("氏名", GetPrintFont(22), Brushes.Black, 650, 460);
            e.Graphics.DrawString(printData.ShipperRecipientUserList[0].受領者名,
                GetPrintFont(22),
                Brushes.Black, 744, 420);

            //発送者TEL
            e.Graphics.DrawString("TEL", GetPrintFont(22), Brushes.Black, 153, 544);
            e.Graphics.DrawString(printData.ShipperRecipientUserList[0].発送者_TEL,
                GetPrintFont(22),
                Brushes.Black, 240, 530);

            //受領者TEL
            e.Graphics.DrawString("TEL", GetPrintFont(22), Brushes.Black, 653, 544);
            e.Graphics.DrawString(printData.ShipperRecipientUserList[0].受領者_TEL,
                GetPrintFont(22),
                Brushes.Black, 744, 530);

            //搬送車両名
            e.Graphics.DrawString("搬送車両名", GetPrintFont(22), Brushes.Black, 70, 630);
            e.Graphics.DrawString(printData.GetFullCarName(), GetPrintFont(30), Brushes.Black, 80, 680);

            SetFooter(e, 1090, 770);
        }
    }
}
