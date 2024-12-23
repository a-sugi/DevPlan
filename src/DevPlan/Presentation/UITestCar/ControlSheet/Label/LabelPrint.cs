using System;
using System.Linq;

using Microsoft.VisualBasic;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UITestCar.ControlSheet.Label
{
    /// <summary>
    /// ラベル印刷
    /// </summary>
    public class LabelPrint
    {
        /// <summary>
        /// テンプレートパス
        /// </summary>
        const string LabelTemplatePath = @".\UITestCar\ControlSheet\Label\Template\{0}\{1}";

        /// <summary>
        /// ラベル用文字列の取得
        /// </summary>
        Func<string, string> GetLabelString = (str) => { return (str ?? string.Empty); };

        #region 印刷実行
        /// <summary>
        /// 印刷実行
        /// </summary>
        /// <param name="data"></param>
        public void Print(TestCarCommonModel data)
        {
            // 印刷オブジェクト作成
            var doc = new BrssCom.Document();

            var index = 0;
            var affiliation = string.IsNullOrWhiteSpace(SessionDto.Affiliation) ? "g" : SessionDto.Affiliation;

            if (data.種別 == "固定資産")
            {
                // 固定資産
                //Update Start 2023/08/23 杉浦 フロンに関するラベル対応
                //if (doc.Open(string.Format(LabelTemplatePath, affiliation, "固定資産_直接印刷.lbl")) == false)
                //{
                //    //エラーメッセージ表示
                //    Messenger.Error(Resources.TCM03001, null);
                //    return;
                //}
                if (data.A_C冷媒種類 == "HFC 134a フロン" && data.自動車ﾘｻｲｸﾙ法 == "対象外")
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "固定資産_直接印刷_フロンあり.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }

                }
                else
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "固定資産_直接印刷.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }
                }
                //Update End 2023/08/23 杉浦 フロンに関するラベル対応

                index = doc.GetTextIndex("data_固定資産番号");
                doc.SetText(index, this.GetLabelString(data.固定資産NO));
            }
            else if (data.種別 == "資産外")
            {
                // 資産外
                //Update Start 2023/08/23 杉浦 フロンに関するラベル対応
                //if (doc.Open(string.Format(LabelTemplatePath, affiliation, "資産外_直接印刷.lbl")) == false)
                //{
                //    //エラーメッセージ表示
                //    Messenger.Error(Resources.TCM03001, null);
                //    return;
                //}
                if (data.A_C冷媒種類 == "HFC 134a フロン" && data.自動車ﾘｻｲｸﾙ法 == "対象外")
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "資産外_直接印刷_フロンあり.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }
                }
                else
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "資産外_直接印刷.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }
                }
                //Update End 2023/08/23 杉浦 フロンに関するラベル対応

                    index = doc.GetTextIndex("data_使用期限");
                if (data.使用期限 != null)
                {
                    doc.SetText(index, Convert.ToDateTime(data.使用期限).ToString("yyyy/MM/dd"));
                }
                else
                {
                    doc.SetText(index, "");
                }
            }
            else if (data.種別 == "リース")
            {
                //リース
                //Update Start 2023/08/23 杉浦 フロンに関するラベル対応
                //if (doc.Open(string.Format(LabelTemplatePath, affiliation, "リース_直接印刷.lbl")) == false)
                //{
                //    //エラーメッセージ表示
                //    Messenger.Error(Resources.TCM03001, null);
                //    return;
                //}
                if (data.A_C冷媒種類 == "HFC 134a フロン" && data.自動車ﾘｻｲｸﾙ法 == "対象外")
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "リース_直接印刷_フロンあり.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }
                }
                else
                {
                    if (doc.Open(string.Format(LabelTemplatePath, affiliation, "リース_直接印刷.lbl")) == false)
                    {
                        //エラーメッセージ表示
                        Messenger.Error(Resources.TCM03001, null);
                        return;
                    }
                }
                //Update End 2023/08/23 杉浦 フロンに関するラベル対応

                index = doc.GetTextIndex("data_リース期限");
                if (data.リース満了日 != null)
                {
                    doc.SetText(index, Convert.ToDateTime(data.リース満了日).ToString("yyyy/MM/dd"));
                }
                else
                {
                    doc.SetText(index, "");
                }

                //三鷹の場合のみ文言変更
                if (affiliation == "t")
                {
                    doc.SetText(index, "");
                    doc.SetText(doc.GetTextIndex("使用期限："), "リース");
                }
            }
            else
            {
                return;
            }
            index = doc.GetTextIndex("data_管理表番号");
            doc.SetText(index, this.GetLabelString(data.管理票NO));

            index = doc.GetTextIndex("data_車体番号");
            if (string.IsNullOrWhiteSpace(data.車体番号) == false)
            {
                if (data.車体番号.Contains("-") == true)
                {
                    var list = data.車体番号.Split('-');
                    if (list != null)
                    {
                        if (list.Count() < 2)
                        {
                            doc.SetText(index, this.GetLabelString(data.車体番号));
                        }
                        else
                        {
                            doc.SetText(index, list[1]);
                        }
                    }
                }
                else
                {
                    doc.SetText(index, this.GetLabelString(data.車体番号));
                }
            }
            else
            {
                doc.SetText(index, "");
            }

            index = doc.GetTextIndex("data_所属");
            doc.SetText(index, this.GetLabelString(data.SECTION_CODE) + "  " + this.GetLabelString(data.SECTION_GROUP_CODE));

            index = doc.GetTextIndex("data_駐車場番号");
            doc.SetText(index, this.GetLabelString(data.駐車場番号));

            index = doc.GetTextIndex("data_車両名");
            var carName = this.GetGoshaForLabel(data);
            doc.SetText(index, carName);

            index = doc.GetTextIndex("data_GPS_1");
            var gpsText = "";
            if (data.XEYE_EXIST == "あり")
            {
                gpsText = "★";
            }
            doc.SetText(index, gpsText);

            index = doc.GetTextIndex("data_GPS_2");
            gpsText = "";
            if (data.XEYE_EXIST == "あり")
            {
                gpsText = "★";
            }
            doc.SetText(index, gpsText);

            index = doc.GetTextIndex("data_GPS_3");
            gpsText = "";
            if (data.XEYE_EXIST == "あり")
            {
                gpsText = "★★";
            }
            doc.SetText(index, gpsText);

            index = doc.GetTextIndex("data_GPS_4");
            gpsText = "";
            if (data.XEYE_EXIST == "あり")
            {
                gpsText = "★★";
            }
            doc.SetText(index, gpsText);

            doc.SetBarcodeData(0, data.管理票NO);

            //印刷実行
            doc.DoPrint(BrssCom.PrintOptionConstants.bpoContinue, "0");

            //テンプレートクローズ
            doc.Close();
        }
        #endregion

        #region 車両名作成
        /// <summary>
        /// 車両名作成
        /// </summary>
        /// <param name="data">行データ</param>
        /// <returns></returns>
        private string GetGoshaForLabel(TestCarCommonModel data)
        {
            // 開発符号がない場合（外製車）
            if (string.IsNullOrWhiteSpace(data.開発符号))
            {
                // 外製車名を車両名とする
                return GetLabelString(data.外製車名);
            }

            // 開発符号、試作時期、号車から車両名を作成
            //Update Start 2021/10/27 松前谷 マスキング処理において「P-」から始まる開発符号の場合→「P-」の後ろの1文字をマスキングできるよう処理を追記
            // return GetGoshaForLabel(data.開発符号.Substring(0, 1)) + data.開発符号.Substring(1) +
            // "-" + GetLabelString(data.試作時期) + "-" + GetLabelString(data.号車);
            //
            if (data.開発符号.StartsWith("P-"))
            {
                // "P-"から始まる開発符号の場合
                return data.開発符号.Substring(0, 2) + GetGoshaForLabel(data.開発符号.Substring(2, 1)) + data.開発符号.Substring(3) +
                                        "-" + GetLabelString(data.試作時期) + "-" + GetLabelString(data.号車);
            }
            else
            {
                // "P-"以外から始まる開発符号の場合
                return GetGoshaForLabel(data.開発符号.Substring(0, 1)) + data.開発符号.Substring(1) +
                                        "-" + GetLabelString(data.試作時期) + "-" + GetLabelString(data.号車);
            }
            //Update End 2021/10/28 松前谷 マスキング処理において「P-」から始まる開発符号の場合→「P-」の後ろの1文字をマスキングできるよう処理を追記
        }
        #endregion

        #region 開発符号置換
        /// <summary>
        /// 開発符号置換
        /// </summary>
        /// <param name="alp">開発符号先頭文字</param>
        /// <returns></returns>
        private string GetGoshaForLabel(string alp)
        {
            //半角に変換
            var key = Strings.StrConv(alp, VbStrConv.Narrow);
            switch (key)
            {
                case "A":
                    return "01";
                case "B":
                    return "02";
                case "C":
                    return "03";
                case "D":
                    return "04";
                case "E":
                    return "05";
                case "F":
                    return "06";
                case "G":
                    return "07";
                case "H":
                    return "08";
                case "I":
                    return "09";
                case "J":
                    return "10";
                case "K":
                    return "11";
                case "L":
                    return "12";
                case "M":
                    return "13";
                case "N":
                    return "14";
                case "O":
                    return "15";
                case "P":
                    return "16";
                case "Q":
                    return "17";
                case "R":
                    return "18";
                case "S":
                    return "19";
                case "T":
                    return "20";
                case "U":
                    return "21";
                case "V":
                    return "22";
                case "W":
                    return "23";
                case "X":
                    return "24";
                case "Y":
                    return "25";
                case "Z":
                    return "26";
                default:
                    return "";
            }
        }
        #endregion
    }
}
