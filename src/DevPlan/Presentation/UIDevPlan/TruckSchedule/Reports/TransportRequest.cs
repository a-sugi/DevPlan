using DevPlan.UICommon.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlan.Presentation.UIDevPlan.TruckSchedule.Reports
{
    /// <summary>
    /// 搬送依頼書
    /// </summary>
    public class TransportRequest : BaseReports
    {
        private int pageCounter = 1;

        /// <summary>
        /// 印刷情報（複数ページ）
        /// </summary>
        Dictionary<int, List<TruckScheduleModel>> dic;

        /// <summary>
        /// 印刷情報（１ページ）
        /// </summary>
        List<TruckScheduleModel> printDataList;

        /// <summary>
        /// 定期便マスタ情報
        /// </summary>
        List<DeparturePoint> departurePointList;

        /// <summary>
        /// 印刷実行
        /// </summary>
        /// <param name="departurePointList"></param>
        /// <param name="printDataList"></param>
        internal void Print(List<DeparturePoint> departurePointList, List<TruckScheduleModel> printDataList)
        {
            using (PrintDocument doc = new PrintDocument())
            {
                doc.DocumentName = "搬送依頼書_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                doc.DefaultPageSettings.Landscape = false;

                foreach (PaperSize ps in doc.PrinterSettings.PaperSizes)
                {
                    if (ps.Kind == PaperKind.A4)
                    {
                        doc.DefaultPageSettings.PaperSize = ps;
                        break;
                    }
                }

                this.printDataList = printDataList;
                this.departurePointList = departurePointList;

                doc.PrintPage += new PrintPageEventHandler(doc_PrintTransportRequest);

                pageCounter = 1;

                doc.Print();
            }
        }

        /// <summary>
        /// 搬送依頼書オブジェクト作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doc_PrintTransportRequest(object sender, PrintPageEventArgs e)
        {
            if (pageCounter == 1)
            {
                dic = new Dictionary<int, List<TruckScheduleModel>>();
                int counter = 1;
                int userCount = 0;
                foreach (var printData in printDataList)
                {        
                    if (printData.ShipperRecipientUserList.Count() + userCount >= 14)
                    {
                        counter += 1;
                        userCount = 0;
                    }

                    userCount += printData.ShipperRecipientUserList.Count();

                    if (dic.ContainsKey(counter) == false)
                    {
                        var list = new List<TruckScheduleModel>();
                        list.Add(printData);
                        dic.Add(counter, list);
                    }
                    else
                    {
                        dic[counter].Add(printData);
                    }
                }
            }

            StringFormat format = new StringFormat(StringFormat.GenericTypographic);
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(
                "定期便【" + dic.First().Value.First().車両名 + "】　搬送依頼書　　搬送日…" +
                dic.First().Value.First().予約開始時間.Value.ToString("yyyy年M月d日(dddd)"),
                   GetPrintFont(11), Brushes.Black, 40, 40);

            int mainhosei = 0;

            foreach (var printData in dic[pageCounter])
            {
                Pen p = new Pen(Color.Black, 1);

                //タイトル
                e.Graphics.DrawRectangle(p, 40, 70 + mainhosei, 730, 70);

                //時間
                e.Graphics.DrawString(
                    printData.DEPARTURE_TIME + " " +
                departurePointList.Where(x =>
                x.TruckId == printData.トラック_ID &&
                x.DepartureTime == printData.DEPARTURE_TIME).First().Text5,
                    GetPrintFont(17),
                Brushes.Black, 60, 90 + mainhosei);

                //群馬発の横の縦線
                e.Graphics.DrawLine(p, 230, 70 + mainhosei, 230, 130 + mainhosei);
                //横線
                e.Graphics.DrawLine(p, 230, 100 + mainhosei, 770, 100 + mainhosei);

                if (!string.IsNullOrWhiteSpace(printData.定期便依頼者名))
                {
                    e.Graphics.DrawString("依頼者 … " + printData.定期便依頼者名 + "　Tel " + printData.定期便依頼者_TEL,
                        GetPrintFont(11),
                        Brushes.Black, 240, 77 + mainhosei);

                    e.Graphics.DrawString("搬送車両名 … " + printData.GetFullCarName(),
                        GetPrintFont(11),
                        Brushes.Black, 240, 107 + mainhosei);
                }

                //発送者受領者
                //枠線描画
                //背景塗りつぶし
                e.Graphics.FillRectangle(Brushes.LightGray, 40, 130 + mainhosei, 730, 20);
                e.Graphics.DrawRectangle(p, 40, 130 + mainhosei, 730, 20);

                //縦線（発送者名の横）
                e.Graphics.DrawLine(p, 230, 130 + mainhosei, 230, 150 + mainhosei);
                //縦線（発送者TELの横）
                e.Graphics.DrawLine(p, 410, 130 + mainhosei, 410, 150 + mainhosei);
                //縦線（受領者名の横
                e.Graphics.DrawLine(p, 600, 130 + mainhosei, 600, 150 + mainhosei);

                e.Graphics.DrawString("発送者名",
                    GetPrintFont(10), Brushes.Black, 130, 140 + mainhosei, format);

                e.Graphics.DrawString("発送者 Tel",
                    GetPrintFont(10), Brushes.Black, 318, 140 + mainhosei, format);

                e.Graphics.DrawString("受領者名",
                    GetPrintFont(10), Brushes.Black, 500, 140 + mainhosei, format);

                e.Graphics.DrawString("受領者 Tel",
                    GetPrintFont(10), Brushes.Black, 680, 140 + mainhosei, format);

                int hosei = mainhosei;
                if (printData.ShipperRecipientUserList == null || printData.ShipperRecipientUserList.Count() <= 0)
                {
                    hosei = hosei + 20;

                    e.Graphics.DrawRectangle(p, 40, 130 + hosei, 730, 20);

                    //縦線（発送者名の横）
                    e.Graphics.DrawLine(p, 230, 130 + hosei, 230, 150 + hosei);
                    //縦線（発送者TELの横）
                    e.Graphics.DrawLine(p, 410, 130 + hosei, 410, 150 + hosei);
                    //縦線（受領者名の横
                    e.Graphics.DrawLine(p, 600, 130 + hosei, 600, 150 + hosei);
                }
                else
                {
                    foreach (var user in printData.ShipperRecipientUserList)
                    {
                        hosei = hosei + 20;

                        e.Graphics.DrawRectangle(p, 40, 130 + hosei, 730, 20);

                        //縦線（発送者名の横）
                        e.Graphics.DrawLine(p, 230, 130 + hosei, 230, 150 + hosei);
                        //縦線（発送者TELの横）
                        e.Graphics.DrawLine(p, 410, 130 + hosei, 410, 150 + hosei);
                        //縦線（受領者名の横
                        e.Graphics.DrawLine(p, 600, 130 + hosei, 600, 150 + hosei);

                        e.Graphics.DrawString(user.発送者名,
                            GetPrintFont(10), Brushes.Black, 130, 140 + hosei, format);

                        e.Graphics.DrawString(user.発送者_TEL,
                            GetPrintFont(10), Brushes.Black, 318, 140 + hosei, format);

                        e.Graphics.DrawString(user.受領者名,
                            GetPrintFont(10), Brushes.Black, 500, 140 + hosei, format);

                        e.Graphics.DrawString(user.受領者_TEL,
                            GetPrintFont(10), Brushes.Black, 680, 140 + hosei, format);
                    }
                }

                e.Graphics.DrawString(printData.備考,
                    GetPrintFont(10), Brushes.Black, 60, 155 + hosei);
                e.Graphics.DrawRectangle(p, 40, 130 + hosei, 730, 50);

                mainhosei = hosei + 125;
            }

            SetFooter(e, 750, 1090);

            if (pageCounter == dic.Count())
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }

            pageCounter++;
        }
    }
}
