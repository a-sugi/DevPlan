using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MeetingDocument
{
    /// <summary>
    /// 影響部品・出図日程入力
    /// </summary>
    public partial class InfluencePartsExitScheduleForm : BaseSubForm
    {
        #region メンバ変数
        private int RowMax = 20;

        private List<string> sumColumn;

        private Dictionary<string, int> maxLengthMap;

        private List<DataGridViewComboBoxColumn> comboBoxList = new List<DataGridViewComboBoxColumn>();

        private readonly Dictionary<int, string> NoMap = new Dictionary<int, string>
        {
            { 0 ,"①" },
            { 1 ,"②" },
            { 2 ,"③" },
            { 3 ,"④" },
            { 4 ,"⑤" },
            { 5 ,"⑥" },
            { 6 ,"⑦" },
            { 7 ,"⑧" },
            { 8 ,"⑨" },
            { 9 ,"⑩" },
            { 10 ,"⑪" },
            { 11 ,"⑫" },
            { 12 ,"⑬" },
            { 13 ,"⑭" },
            { 14 ,"⑮" },
            { 15 ,"⑯" },
            { 16 ,"⑰" },
            { 17 ,"⑱" },
            { 18 ,"⑲" },
            { 19 ,"⑳" }

        };
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "影響部品・出図日程入力"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>検討会資料詳細</summary>
        public MeetingDocumentDetailModel MeetingDocumentDetai { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InfluencePartsExitScheduleForm()
        {
            InitializeComponent();

        }
        #endregion

        #region フォームロード
        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfluencePartsExitScheduleForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            //影響部品・出図日程の設定
            this.SetAffectedPartsDrawingScheduleList();

            //影響部品・出図日程の合計の設定
            this.SetSumAffectedPartsDrawingSchedule();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //列の自動生成なし
            this.AffectedPartsDrawingScheduleDataGridView.AutoGenerateColumns = false;

            //設計部署(部)
            this.DesignDepartmentColumn.ValueMember = "DEPARTMENT_CODE";
            this.DesignDepartmentColumn.DisplayMember = "DEPARTMENT_CODE";
            this.DesignDepartmentColumn.DataSource = (new[] { new DepartmentModel() }).Concat(HttpUtil.GetResponse<DepartmentModel>(ControllerType.Department)?.Results).ToList();

            //ヘッダーの改行コードを置き換え
            foreach (DataGridViewColumn col in this.AffectedPartsDrawingScheduleDataGridView.Columns)
            {
                col.HeaderText = col.HeaderText.Replace(@"\n", Const.CrLf);

                //コンボボックスの列かどうか
                if (col is DataGridViewComboBoxColumn)
                {
                    this.comboBoxList.Add(col as DataGridViewComboBoxColumn);

                }

            }

            //合計産出対象の列
            this.sumColumn = new List<string>
            {
                //コスト変動見通し(円)
                this.CostColumn.Name,

                //質量変動見通し(g)
                this.MassColumn.Name,

                //投資変動見通し(百万円)
                this.InvestmentColumn.Name

            };

            //入力チェック
            this.maxLengthMap = new Dictionary<string, int>
            {
                //設計部署(部)
                { this.DesignDepartmentColumn.Name, 100 },

                //影響する(しそうな)部品
                { this.AffectedPartsColumn.Name, 500 },

                //出図日程
                { this.DrawingScheduleColumn.Name, 300 },

                //コスト変動見通し(円)
                { this.CostColumn.Name, 150 },

                //質量変動見通し(g)
                { this.MassColumn.Name, 150 },

                //投資変動見通し(百万円)
                { this.InvestmentColumn.Name, 150 }

            };

        }

        /// <summary>
        /// 影響部品・出図日程の設定
        /// </summary>
        private void SetAffectedPartsDrawingScheduleList()
        {
            Func<string, string[]> split = value => (value ?? "").Trim().Replace(Const.CrLf, Const.Lf).Replace(Const.Cr, Const.Lf).Split(new[] { Const.Lf }, StringSplitOptions.None).Select(x => string.IsNullOrWhiteSpace(x) == true ? "" : x.Substring(1).Trim()).ToArray();

            Func<string[], int, string> getValue = (list, index) => index >= 0 && list.Length > index ? list[index] : "";

            Func<string[], int, DateTime?> getDate = (list, index) =>
            {
                var value = getValue(list, index);

                DateTime? day = null;

                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    day = DateTimeUtil.ConvertDateStringToDateTime(value);

                }

                return day;

            };

            //改行コードで分割
            var sekkeiBusyo = split(this.MeetingDocumentDetai.関連設計);
            var eikyouBuhin = split(this.MeetingDocumentDetai.影響部品);
            var syutuzuNittei = split(this.MeetingDocumentDetai.出図日程);
            var cost = split(this.MeetingDocumentDetai.コスト変動);
            var situryou = split(this.MeetingDocumentDetai.質量変動);
            var tousi = split(this.MeetingDocumentDetai.投資変動);

            //分割したデータを一覧にセット
            for (var i = 0; i < sekkeiBusyo.Length; i++)
            {
                this.AffectedPartsDrawingScheduleDataGridView.Rows.Add(new object[]
                {
                    //No
                    "",

                    //設計部署(部)
                    getValue(sekkeiBusyo, i),

                    //影響する(しそうな)部品
                    getValue(eikyouBuhin, i),

                    //出図日程
                    getDate(syutuzuNittei, i),

                    //コスト変動見通し(円)
                    getValue(cost, i),

                    //質量変動見通し(g)
                    getValue(situryou, i),

                    //投資変動見通し(百万円)
                    getValue(tousi, i)

                });

            }
        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfluencePartsExitScheduleForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.AffectedPartsDrawingScheduleDataGridView.CurrentCell = null;

        }
        #endregion

        #region 行追加ボタンクリック
        /// <summary>
        /// 行追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            var cnt = this.AffectedPartsDrawingScheduleDataGridView.RowCount;

            //行の最大数を超えているかどうか
            if (cnt < RowMax)
            {
                //行追加
                this.AffectedPartsDrawingScheduleDataGridView.Rows.Add();

            }
            else
            {
                Messenger.Warn(Resources.KKM03027);

            }

        }
        #endregion

        #region 行削除ボタンクリック
        /// <summary>
        /// 行削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            var cell = this.AffectedPartsDrawingScheduleDataGridView.CurrentCell;

            //一覧を選択しているかどうか
            if (cell == null)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;

            }

            //行削除
            this.AffectedPartsDrawingScheduleDataGridView.Rows.RemoveAt(cell.RowIndex);

            //影響部品・出図日程の合計の設定
            this.SetSumAffectedPartsDrawingSchedule();

            //削除完了メッセージ
            Messenger.Info(Resources.KKM00003);

        }
        #endregion

        #region 影響部品・出図日程一覧
        /// <summary>
        /// 影響部品・出図日程一覧の行追加時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffectedPartsDrawingScheduleDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //影響部品・出図日程一覧に行番号を設定
            this.SetAffectedPartsDrawingScheduleRowNo();

        }

        /// <summary>
        /// 影響部品・出図日程一覧の行削除時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffectedPartsDrawingScheduleDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //影響部品・出図日程一覧に行番号を設定
            this.SetAffectedPartsDrawingScheduleRowNo();

        }

        /// <summary>
        /// 影響部品・出図日程一覧の値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffectedPartsDrawingScheduleDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行の場合は何もしない
            if (e.RowIndex < 0)
            {
                return;

            }

            //合計算出対象の列かどうか
            var name = this.AffectedPartsDrawingScheduleDataGridView.Columns[e.ColumnIndex].Name;
            if (this.sumColumn.Contains(name) == true)
            {
                //影響部品・出図日程の合計の設定
                this.SetSumAffectedPartsDrawingSchedule();

            }

        }

        /// <summary>
        ///  影響部品・出図日程一覧の値エラー時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffectedPartsDrawingScheduleDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //コンボボックスの列かどうか
            var col = this.AffectedPartsDrawingScheduleDataGridView.Columns[e.ColumnIndex];
            if (this.comboBoxList.Any(x => x.Name == col.Name) == true)
            {
                //例外は無視
                e.ThrowException = false;

                //キャンセル
                e.Cancel = true;

            }
            else
            {
                //例外はスロー
                e.ThrowException = true;

            }

        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            //影響部品・出図日程が設定できたかどうか
            if (this.SetAffectedPartsDrawingSchedule() == true)
            {
                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 影響部品・出図日程を設定
        /// </summary>
        /// <returns></returns>
        private bool SetAffectedPartsDrawingSchedule()
        {
            var sekkeiBusyoList = new List<string>();
            var eikyouBuhinList = new List<string>();
            var syutuzuNitteiList = new List<string>();
            var costList = new List<string>();
            var situryouList = new List<string>();
            var tousiList = new List<string>();

            var syutuzuNitteiDayList = new List<DateTime?>();

            var msgList = new List<string>();

            Func<int, string, string> getValue = (index, name) =>
            {
                var cell = this.AffectedPartsDrawingScheduleDataGridView[name, index];

                var value = cell.Value;

                return value == null ? "" : value.ToString();

            };

            Func<int, string, string> getDate = (index, name) =>
            {
                var cell = this.AffectedPartsDrawingScheduleDataGridView[name, index];

                var value = cell.Value as DateTime?;

                if (value != null)
                {
                    syutuzuNitteiDayList.Add(value);

                }

                return DateTimeUtil.ConvertDateString(value);

            };

            Func<IEnumerable<string>, string> join = list => (list == null || list.Any() == false) ? "" : string.Join(Const.CrLf, list.ToArray());

            Func<string, string, bool> isLength = (name, value) =>
            {
                var flg = true;

                if (string.IsNullOrEmpty(value) == false)
                {
                    flg = StringUtil.SjisByteLength(value) <= this.maxLengthMap[name];

                }

                if (flg == false)
                {
                    var header = this.AffectedPartsDrawingScheduleDataGridView.Columns[name].HeaderText;
                    msgList.Add(string.Format(Resources.KKM00027, header.Replace(Const.Cr, "").Replace(Const.Lf, "")));

                }

                return flg;

            };

            //行のデータを取得
            var targetList = this.AffectedPartsDrawingScheduleDataGridView.Rows.Cast<DataGridViewRow>()
                .Select((row, index) => new
                {
                    No = NoMap[index],
                    SekkeiBusyo = getValue(index, this.DesignDepartmentColumn.Name),
                    EikyouBuhin = getValue(index, this.AffectedPartsColumn.Name),
                    SyutuzuNittei = getDate(index, this.DrawingScheduleColumn.Name),
                    Cost = getValue(index, this.CostColumn.Name),
                    Situryou = getValue(index, this.MassColumn.Name),
                    Tousi = getValue(index, this.InvestmentColumn.Name)

                }).ToArray();

            //入力されている最後の行を取得
            var take = 0;
            for (var i = targetList.Count(); i > 0; i--)
            {
                var row = targetList[i - 1];

                //行の入力がされているかどうか
                if ((new[] { row.SekkeiBusyo, row.EikyouBuhin, row.SyutuzuNittei, row.Cost, row.Situryou, row.Tousi }).All(x => string.IsNullOrWhiteSpace(x) == true) == false)
                {
                    take = i;
                    break;

                }

            }

            //設定する行があるかどうか
            if (take > 0)
            {
                foreach (var row in targetList.Take(take))
                {
                    //設計部署(部)
                    sekkeiBusyoList.Add(row.No + row.SekkeiBusyo);

                    //影響する(しそうな)部品
                    eikyouBuhinList.Add(row.No + row.EikyouBuhin);

                    //出図日程
                    syutuzuNitteiList.Add(row.No + row.SyutuzuNittei);

                    //コスト変動見通し(円)
                    costList.Add(row.No + row.Cost);

                    //質量変動見通し(g)
                    situryouList.Add(row.No + row.Situryou);

                    //投資変動見通し(百万円)
                    tousiList.Add(row.No + row.Tousi);

                }

                //合計を追記
                costList.Add(string.Format("計{0}", this.CostLabel.Text));
                situryouList.Add(string.Format("計{0}", this.MassLabel.Text));
                tousiList.Add(string.Format("計{0}", this.InvestmentLabel.Text));

                //出図日程の入力した日付があるかどうか
                if (syutuzuNitteiDayList.Any() == true)
                {
                    //出図日程
                    syutuzuNitteiList.Add(string.Format("始{0}", DateTimeUtil.ConvertDateString(syutuzuNitteiDayList.Min())));
                    syutuzuNitteiList.Add(string.Format("終{0}", DateTimeUtil.ConvertDateString(syutuzuNitteiDayList.Max())));

                }

            }

            var sekkeiBusyo = join(sekkeiBusyoList);
            var eikyouBuhin = join(eikyouBuhinList);
            var syutuzuNittei = join(syutuzuNitteiList);
            var cost = join(costList);
            var situryou = join(situryouList);
            var tousi = join(tousiList);

            //入力値のチェック
            var inputList = new List<bool>();
            inputList.Add(isLength(this.DesignDepartmentColumn.Name, sekkeiBusyo));
            inputList.Add(isLength(this.AffectedPartsColumn.Name, eikyouBuhin));
            inputList.Add(isLength(this.DrawingScheduleColumn.Name, syutuzuNittei));
            inputList.Add(isLength(this.CostColumn.Name, cost));
            inputList.Add(isLength(this.MassColumn.Name, situryou));
            inputList.Add(isLength(this.InvestmentColumn.Name, tousi));

            //全てOKかどうか
            if (inputList.Any(x => x == false))
            {
                Messenger.Warn(string.Join(Const.CrLf, msgList.ToArray()));
                return false;

            }

            //設計部署(部)
            this.MeetingDocumentDetai.関連設計 = sekkeiBusyo;

            //影響する(しそうな)部品
            this.MeetingDocumentDetai.影響部品 = eikyouBuhin;

            //出図日程
            this.MeetingDocumentDetai.出図日程 = syutuzuNittei;

            //コスト変動見通し(円)
            this.MeetingDocumentDetai.コスト変動 = cost;

            //質量変動見通し(g)
            this.MeetingDocumentDetai.質量変動 = situryou;

            //投資変動見通し(百万円)
            this.MeetingDocumentDetai.投資変動 = tousi;

            return true;

        }
        #endregion

        #region 影響部品・出図日程一覧に行番号を設定
        /// <summary>
        /// 影響部品・出図日程一覧に行番号を設定
        /// </summary>
        private void SetAffectedPartsDrawingScheduleRowNo()
        {
            for (var i = 0; i < this.AffectedPartsDrawingScheduleDataGridView.RowCount; i++)
            {
                //行番号設定
                var row = this.AffectedPartsDrawingScheduleDataGridView.Rows[i];
                var cell = row.Cells[this.NoColumn.Name];
                cell.Value = i + 1;

            }

        }
        #endregion

        #region 影響部品・出図日程の合計の設定
        /// <summary>
        /// 影響部品・出図日程の合計の設定
        /// </summary>
        private void SetSumAffectedPartsDrawingSchedule()
        {
            var costSum = 0M;
            var situryouSum = 0M;
            var tousiSum = 0M;

            Func<DataGridViewRow, string, decimal> getValue = (row, name) =>
            {
                var value = 0M;

                var cellValue = row.Cells[name].Value;
                var s = cellValue == null ? "" : cellValue.ToString();

                return decimal.TryParse(s, out value) == false ? 0 : value;

            };

            Func<decimal, string> getSum = sum => sum == Math.Truncate(sum) ? sum.ToString("0") : sum.ToString();

            foreach (DataGridViewRow row in this.AffectedPartsDrawingScheduleDataGridView.Rows)
            {
                //合計値に加算
                costSum += getValue(row, this.CostColumn.Name);
                situryouSum += getValue(row, this.MassColumn.Name);
                tousiSum += getValue(row, this.InvestmentColumn.Name);

            }

            //コスト変動見通し(円)
            this.CostLabel.Text = getSum(costSum);

            //質量変動見通し(g)
            this.MassLabel.Text = getSum(situryouSum);

            //投資変動見通し(百万円)
            this.InvestmentLabel.Text = getSum(tousiSum);

        }
        #endregion
    }
}
