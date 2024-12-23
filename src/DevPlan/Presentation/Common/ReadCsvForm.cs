using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using System.IO;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 受領票選択
    /// </summary>
    public partial class ReadCsvForm : BaseSubForm
    {
        #region メンバ変数
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "Xeye用CSV取込"; } }
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>Excelフィルター</summary>
        private const string CsvlFilter = "CSVファイル(*.csv)|*.csv";

        /// <summary>インポートリスト</summary>
        private List<ScheduleToXeyeOutModel> ImportList { get; set; } = new List<ScheduleToXeyeOutModel>();

        #endregion

        public ReadCsvForm()
        {
            InitializeComponent();
        }

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCopyMoveForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            InitForm();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            
        }
        #endregion

        #region 取込ボタンクリック
        /// <summary>
        /// 取込ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            //選択項目設定
            this.SetPrint();

            //閉じる
            base.FormOkClose();
        }
        #endregion

        #region CSV取込処理
        /// <summary>
        /// CSV取込処理
        /// </summary>
        private void SetPrint()
        {
            var frm = new OpenFileDialog() { Filter = CsvlFilter, FilterIndex = 1 };
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                //ファイルの読み込み
                if (this.ImportFile(frm.FileName))
                {
                    // 登録実行
                    if (this.PostImportList() != true)
                    {
                        return;
                    }

                    Messenger.Info(Resources.KKM00002); // 登録完了
                }
            });
        }
        #endregion

        #region ファイルの読み込み
        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="path"></param>
        private bool ImportFile(string path)
        {
            Func<string, DateTime?> getDateTime = dt => string.IsNullOrWhiteSpace(dt) ? null : (DateTime?)Convert.ToDateTime(dt);
            Func<int, string, string> cnvLineFormat = (no, msg) => string.Concat(String.Format("{0, 3}", no), "行：", msg);

            var list = new List<ScheduleToXeyeOutModel>();
            var exculude = new StringBuilder();
            var error = new StringBuilder();
            List<string> lists = new List<string>();
            var rowno = 1;

            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("shift_jis"));
            {
                //CSVファイルの1行目を読み飛ばす
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    rowno++;
                    //CSVファイルを1行を読み込む
                    string line = sr.ReadLine();
                    //読み込んだ一行をカンマごとに分けてリストに格納する
                    List<string> tempLists = new List<string>();
                    tempLists = line.Split(',').ToList();

                    // 空行は行カウントのみでスキップ
                    if (!tempLists.Any(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        continue;
                    }

                    // 入力チェック
                    var tuple = TupleImportDataCheck(tempLists);

                    //重複チェック
                    if(list.Any(x => x.物品コード == tempLists[0]))
                    {
                        tuple.Add("物品コードの" + Resources.KKM03044);
                    }

                    // エラー
                    if (tuple.Any() == true)
                    {
                        tuple.ForEach(x => error.AppendLine(cnvLineFormat(rowno, x)));

                        continue;
                    }

                    list.Add(new ScheduleToXeyeOutModel
                    {
                        物品コード = tempLists[0],
                        物品名 = tempLists[1],
                        物品名2 = tempLists[2],
                        物品名カナ = tempLists[3],
                        物品区分コード = tempLists[4],
                        備考 = tempLists[5],
                        ソート順 = tempLists[6]
                    });
                }
            }

            // エラー
            if (error.Length > 0)
            {
                Messenger.Warn(error.ToString());

                return false;
            }

            if (list.Any() == false)
            {
                Messenger.Warn(string.Concat(Resources.TCM03008, exculude.Length > 0 ? Const.CrLf + Const.CrLf + exculude.ToString() : ""));

                return false;
            }

            // インポートリストのセット
            this.ImportList = list;

            return true;
        }
        #endregion

        #region インポートデータのチェック
        /// <summary>
        /// インポートデータのチェック
        /// </summary>
        private List<string> TupleImportDataCheck(List<string> cell)
        {
            var result = new List<string>();

            #region チェックデリゲード

            // 必須チェック
            Func<string, string, bool> isRequired = (name, value) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result.Add(string.Format(Resources.KKM00001, name));

                    return false;
                }

                return true;
            };

            // 全角半角チェック（Byte）
            Func<string, string, bool> isByte = (name, value) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (StringUtil.SjisByteLength(value) != value.Length)
                    {
                        result.Add(name + "は半角英数字で入力してください。");

                        return false;
                    }
                }

                return true;
            };

            // 桁数チェック（Byte）
            Func<string, string, int, bool> isWide = (name, value, length) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (StringUtil.SjisByteLength(value) > length)
                    {
                        result.Add(string.Concat(string.Format(Resources.KKM00027, name), "半角英数字で入力してください。"));

                        return false;
                    }
                }

                return true;
            };

            #endregion

            // 数値・桁数・形式チェック
            isRequired("物品コード", cell[0]);
            isByte("物品コード", cell[0]);
            isWide("物品コード", cell[0], 10);
            isWide("物品名", cell[1], 200);
            isByte("物品名2", cell[2]);
            isWide("物品名2", cell[2], 10);
            isWide("物品名カナ", cell[3], 200);
            isWide("物品区分コード", cell[4], 2);
            isByte("備考", cell[5]);
            isWide("備考", cell[5], 200);
            isByte("ソート順", cell[6]);
            isWide("ソート順", cell[6], 10);

            return result;
        }
        #endregion

        #region データの操作
        /// <summary>
        /// インポートデータの登録（更新）
        /// </summary>
        private bool PostImportList()
        {
            var res = HttpUtil.PostResponse<ScheduleToXeyeOutModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, this.ImportList);

            this.ImportList = res?.Results?.ToList();

            return res?.Results.Count() > 0;
        }
        #endregion
    }
}
