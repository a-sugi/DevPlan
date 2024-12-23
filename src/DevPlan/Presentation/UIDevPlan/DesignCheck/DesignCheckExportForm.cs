using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 指摘エクスポート
    /// </summary>
    public partial class DesignCheckExportForm : BaseForm
    {
        #region メンバ変数

        private const int CondHeight = 105;

        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private MultiRowContoller GridContoller;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>
        /// 期間コントロール管理クラス
        /// </summary>
        private BetweenController BetweenController;

        /// <summary>
        /// バインド中可否
        /// </summary>
        private bool IsBind { get; set; } = false;

        #endregion

        #region プロパティ

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指摘エクスポート"; } }

        /// <summary>
        /// 初期表示用From開催日
        /// </summary>
        public DateTime? FromOpenDate { get; set; }

        /// <summary>
        /// 初期表示用To開催日
        /// </summary>
        public DateTime? ToOpenDate { get; set; }

        /// <summary>
        /// 初期表示用設計チェック名
        /// </summary>
        public string DesignCheckName { get; set; }

        /// <summary>
        /// 初期表示用オープン値
        /// </summary>
        public bool StatusOpen { get; set; }

        /// <summary>
        /// 初期表示用クローズ値
        /// </summary>
        public bool StatusClose { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckExportForm()
        {
            InitializeComponent();
        }
        #endregion

        #region ロード時
        /// <summary>
        /// ロード時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckExportForm_Load(object sender, EventArgs e)
        {
            // 権限
            this.UserAuthority = base.GetFunction(FunctionID.DesignCheck);

            // 初期化
            this.GridContoller = new MultiRowContoller(this.PointMultiRow, RowCountLabel);
            this.BetweenController = new BetweenController(FromNullableDateTimePicker, ToNullableDateTimePicker);

            // 画面初期化
            this.InitForm();

            // 検索実行
            this.SearchButton_Click(null, null);
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            // バインドフラグ
            this.IsBind = true;

            try
            {
                // 遷移元からの引継ぎデータに初期化
                if (FromOpenDate == null && ToOpenDate != null)
                {
                    BetweenController.SetEnd(ToOpenDate.Value);
                }
                else if (FromOpenDate != null && ToOpenDate == null)
                {
                    BetweenController.SetSart(FromOpenDate.Value);
                }
                else
                {
                    this.FromNullableDateTimePicker.Value = FromOpenDate;
                    this.ToNullableDateTimePicker.Value = ToOpenDate;
                }

                this.DesignCheckNameTextBox.Text = DesignCheckName;
                this.StatusOpenCheckBox.Checked = StatusOpen;
                this.StatusCloseCheckBox.Checked = StatusClose;

                // 他はクリア
                FormControlUtil.SetComboBoxItem(this.DepartmentComboBox, new List<ComboBoxDto>(), false);
                this.GeneralCodeTextBox.Text = "";
                this.TrialProductionSeasonTextBox.Text = "";
                this.CarTextBox.Text = "";

                // Hiddenパネルの背景色設定
                this.HidePanel1.BackColor = this.ContentsPanel.BackColor;
            }
            finally
            {
                // バインドフラグ
                this.IsBind = false;
            }
        }
        #endregion

        #region 画面表示後
        /// <summary>
        /// 画面表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckExportForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = FromNullableDateTimePicker;
            FromNullableDateTimePicker.Focus();
        }
        #endregion

        #region 検索条件ボタン押下
        /// <summary>
        /// 検索条件ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            var flg = !this.SearchConditionTableLayoutPanel.Visible;

            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, new[] { this.RowCountLabel }, CondHeight);

            //検索結果メッセージ
            this.ListDataLabel.Visible = flg;
        }
        #endregion

        #region 検索ボタン押下
        /// <summary>
        /// 検索ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (IsSearchDesignCheck())
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // 初期化
                    this.CheckBoxAll.Checked = false;

                    bool? status = null;

                    // Openのみ選択かどうか
                    if (this.StatusOpenCheckBox.Checked == true && this.StatusCloseCheckBox.Checked == false)
                    {
                        status = this.StatusOpenCheckBox.Checked;
                    }
                    // Closeのみ選択かどうか
                    else if (this.StatusOpenCheckBox.Checked == false && this.StatusCloseCheckBox.Checked == true)
                    {
                        status = !this.StatusCloseCheckBox.Checked;
                    }

                    var cond = new DesignCheckDetailGetInModel()
                    {
                        OPEN_START_DATE = this.FromNullableDateTimePicker.SelectedDate,
                        OPEN_END_DATE = this.ToNullableDateTimePicker.SelectedDate,
                        OPEN_FLG = status,
                        名称 = this.DesignCheckNameTextBox.Text,
                        開発符号 = GeneralCodeTextBox.Text,
                        試作時期 = TrialProductionSeasonTextBox.Text,
                        号車 = CarTextBox.Text,
                        担当課名 = this.DepartmentComboBox.SelectedIndex == -1 ? "" : this.DepartmentComboBox.SelectedValue.ToString(),
                    };

                    this.GridContoller.GetData(cond);

                    this.GridContoller.Bind();

                    this.ListDataLabel.Text = this.GridContoller.ModelList.Any() == true ? "" : Resources.KKM00005;
                });
            }
        }
        #endregion

        #region 検索条件のチェック
        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchDesignCheck()
        {
            var msg = Validator.GetFormInputErrorMessage(this, BetweenController.GetRule());

            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            return true;
        }
        #endregion

        #region クリアボタン押下
        /// <summary>
        /// クリアボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.InitForm();
        }
        #endregion

        #region 担当課マウスクリック
        /// <summary>
        /// 担当課マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            //左クリック以外は終了
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            using (var form = new SectionListForm() { IS_ALL = true })
            {
                //担当検索画面がOKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //担当課をセット
                    var value = new ComboBoxDto
                    {
                        CODE = form.SECTION_CODE,
                        NAME = string.Format("{0} {1}", form.DEPARTMENT_CODE, form.SECTION_CODE)
                    };

                    //担当をセット
                    FormControlUtil.SetComboBoxItem(this.DepartmentComboBox, new[] { value }, false);
                    this.DepartmentComboBox.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelOutButton_Click(object sender, EventArgs e)
        {
            if (PointMultiRow.Rows.Any((x) => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value)) == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            var util = new MultiRowUtil(this.PointMultiRow);
            util.Excel.GetCols = () => this.PointMultiRow.Columns.Cast<Column>().Where(x => x.Name != "TargetCheckBoxColumn" && x.Name != "RowHeader");
            util.Excel.GetHeads = () => util.Excel.GetCols().Select((x) => this.PointMultiRow.ColumnHeaders[0].Cells[x.Index].Value.ToString().Replace(Const.CrLf, " ")).ToList();
            util.Excel.GetRows = () => this.PointMultiRow.Rows.Cast<Row>().Where((x) => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value) == true);
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        #region 全チェックON/OFF処理
        /// <summary>
        /// 全チェックON/OFF処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                PointMultiRow.CurrentCell = null;

                foreach (var row in PointMultiRow.Rows)
                {
                    row.Cells["TargetCheckBoxColumn"].Value = CheckBoxAll.Checked;
                }
            });
        }
        #endregion

        #region MultiRowイベント

        #region セルクリック
        /// <summary>
        /// セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellClick(object sender, CellEventArgs e)
        {
            //チェックボックスの表示を更新する
            if ((e.CellName == "ch_Target") && (e.RowIndex == -1))
            {
                this.CheckBoxAll.Checked = !this.CheckBoxAll.Checked;
            }
        }
        #endregion

        #region 一覧描画（全選択チェックボックス描画）
        /// <summary>
        /// 一覧描画
        /// （全選択チェックボックス描画）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellPainting(object sender, CellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - CheckBoxAll.Width) / 2, (bmp.Height - CheckBoxAll.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    CheckBoxAll.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #endregion

        #region MultiRowを制御するクラス
        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private class MultiRowContoller
        {
            #region 内部変数

            private GcMultiRow _mlr;

            /// <summary>
            /// 設計チェック詳細モデル全リスト
            /// </summary>
            private List<DesignCheckDetailGetOutModel> _ModelAllList;

            /// <summary>
            /// 車リスト
            /// </summary>
            private List<Car> _CarList;

            /// <summary>
            /// カスタムテンプレート
            /// </summary>
            private CustomTemplate CustomTemplate = new CustomTemplate();

            #endregion

            #region 公開プロパティ

            /// <summary>
            /// データソースの元になる内部保持の設計チェック詳細モデルリスト
            /// </summary>
            public List<DesignCheckDetailGetOutModel> ModelList
            {
                get
                {
                    return this._ModelAllList;
                }

                set
                {
                    this._ModelAllList = value;
                }
            }

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mlr">MultiRow</param>
            /// <param name="rowCountLabel">行カウント表示ラベル</param>
            public MultiRowContoller(GcMultiRow mlr, Label rowCountLabel)
            {
                _mlr = mlr;
                _ModelAllList = new List<DesignCheckDetailGetOutModel>();
                _CarList = new List<Car>();

                // テンプレート設定
                this.CustomTemplate.ColumnHeaderHeight = 70;
                this.CustomTemplate.RowCountLabel = rowCountLabel;
                this.CustomTemplate.MultiRow = this._mlr;
                this._mlr.Template = this.CustomTemplate.SetContextMenuTemplate(new DesignCheckExportMultiRowTemplete());

                // ダブルバッファリング有効
                this._mlr.GetType().InvokeMember(
                   "DoubleBuffered",
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                   null,
                   this._mlr,
                   new object[] { true });
            }
            #endregion

            #region データソースにバインドします。
            /// <summary>
            /// データソースにバインドします。
            /// </summary>
            public void Bind()
            {
                // フィルタリングクリア
                this._mlr.ClearAllFilters();

                // 初期化
                this._mlr.Rows.Clear();

                // テンプレート設定
                this.SetTemplate();

                // データセット
                this.SetData();

                // 表示件数ラベル更新
                this.CustomTemplate.SetCountLabel();

                // 行の高さ調整
                this._mlr.Rows.ToList().ForEach((x) => x.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit());

                // 一覧を未選択状態に設定
                this._mlr.CurrentCell = null;

                // グリッドリフレッシュ
                this._mlr.Refresh();
            }
            #endregion

            #region 設計チェック詳細データを取得します。
            /// <summary>
            /// 設計チェック詳細データを取得します。
            /// </summary>
            /// <param name="cond"></param>
            public void GetData(DesignCheckDetailGetInModel cond)
            {
                // 初期化
                this._ModelAllList.Clear();
                this._CarList.Clear();

                // APIで取得
                var res = HttpUtil.GetResponse<DesignCheckDetailGetInModel, DesignCheckDetailGetOutModel>(ControllerType.DesignCheckDetail, cond);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    this._ModelAllList.AddRange(res.Results);
                    this._CarList = this._ModelAllList
                                            .Where((x) => x.試験車_ID != null)
                                            .Select((x) => new { x.試験車_ID, x.試験車名 })
                                            .Distinct()
                                            .Select((x) => new Car() { ID = x.試験車_ID, Name = x.試験車名 })
                                            .OrderBy((x) => x.ID)
                                            .ToList();
                }
            }
            #endregion

            #region 内部メソッド

            #region テンプレートに車列を追加して設定します。
            /// <summary>
            /// テンプレートに車列を追加して設定します。
            /// </summary>
            private void SetTemplate()
            {
                var tmp = new DesignCheckExportMultiRowTemplete();
                var cellSize = new Size(60, 20);

                var beforeCell = tmp.Row.Cells["UserTelTextBoxColumn"];
                var beforeCol = tmp.ColumnHeaders[0].Cells["ch_UserTel"];
                foreach (var car in _CarList)
                {
                    // 行セルの追加
                    TextBoxCell newCell = new TextBoxCell();
                    newCell.Name = "CarProgress" + (tmp.Row.Cells.Count);
                    newCell.Location = new Point(beforeCell.Right, newCell.Top);
                    newCell.ReadOnly = true;
                    newCell.Size = cellSize;
                    newCell.Style.Border = new Border(new Line(LineStyle.Thin, Color.DarkGray));
                    newCell.Style.TextAlign = MultiRowContentAlignment.MiddleCenter;
                    newCell.TabIndex = beforeCell.TabIndex + 1;
                    tmp.Row.Cells.Add(newCell);

                    // テンプレートの幅の変更
                    tmp.Width += newCell.Width;

                    // 列ヘッダの追加
                    ColumnHeaderCell newHeaderCell = new ColumnHeaderCell();
                    newHeaderCell.Name = "ch_CarProgress" + (tmp.ColumnHeaders[0].Cells.Count).ToString();
                    newHeaderCell.Location = new Point(beforeCol.Right, newHeaderCell.Top);
                    newHeaderCell.Size = cellSize;
                    newHeaderCell.Value = car.Name.Replace(" ", Const.CrLf);
                    tmp.ColumnHeaders[0].Cells.Add(newHeaderCell);

                    // 退避
                    beforeCell = newCell;
                    beforeCol = newHeaderCell;
                }

                // テンプレート設定
                this._mlr.Template = this.CustomTemplate.SetContextMenuTemplate(tmp);

                // 選択列のフィルターアイテム設定
                var cellCheck = _mlr.Template.ColumnHeaders[0].Cells["ch_Target"] as ColumnHeaderCell;
                cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
                cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：処置しない
                var cellTreatment = _mlr.Template.ColumnHeaders[0].Cells["ch_Treatment"] as ColumnHeaderCell;
                cellTreatment.DropDownContextMenuStrip.Items.RemoveAt(cellTreatment.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatment.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("処置しない", "処置する") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：調整：済
                var cellTreatmentOK = _mlr.Template.ColumnHeaders[0].Cells["ch_TreatmentOK"] as ColumnHeaderCell;
                cellTreatmentOK.DropDownContextMenuStrip.Items.RemoveAt(cellTreatmentOK.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatmentOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：要試作改修
                var cellRepair = _mlr.Template.ColumnHeaders[0].Cells["ch_Repair"] as ColumnHeaderCell;
                cellRepair.DropDownContextMenuStrip.Items.RemoveAt(cellRepair.DropDownContextMenuStrip.Items.Count - 1);
                cellRepair.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("要", "否") { MaxCount = CustomTemplate.FilterItemMaxCount });
            }
            #endregion

            #region 取得データを表示用に加工してバインドします。
            /// <summary>
            /// 取得データを表示用に加工してバインドします。
            /// </summary>
            private void SetData()
            {
                // 車以外の列数
                var otherColCount = 19;

                // 指摘ID単位で表示行を作成
                foreach (var id in this._ModelAllList.Select((x) => x.指摘ID).Distinct())
                {
                    var row = new object[otherColCount + _CarList.Count];

                    var model = this._ModelAllList.First((x) => x.指摘ID == id);

                    row[0] = false;
                    row[1] = model.開催日;
                    row[2] = model.名称;
                    row[3] = model.指摘NO;
                    row[4] = model.ステータス;
                    row[5] = model.部品;
                    row[6] = model.状況;
                    row[7] = model.FLAG_処置不要;
                    row[8] = model.処置課;
                    row[9] = model.処置対象;
                    row[10] = model.処置方法;
                    row[11] = model.FLAG_調整済;
                    row[12] = model.処置調整;
                    row[13] = model.織込日程;
                    row[14] = model.FLAG_試作改修;
                    row[15] = model.部品納入日;
                    row[16] = model.担当課名;
                    row[17] = model.担当者名;
                    row[18] = model.担当者_TEL;

                    var cars = this._ModelAllList.Where((x) => x.指摘ID == id && x.試験車_ID != null).ToList();

                    foreach (var car in cars)
                    {
                        var carIndex = _CarList.FindIndex((x) => x.ID == car.試験車_ID);
                        row[otherColCount + carIndex] = car.状況記号;
                    }

                    // バインド
                    _mlr.Rows.Add(row);
                }
            }
            #endregion

            #endregion
        }

        #endregion

        #region 試験車クラス
        /// <summary>
        /// 試験車クラス
        /// </summary>
        private class Car
        {
            /// <summary>
            /// 試験車_ID
            /// </summary>
            public int? ID { get; set; }

            /// <summary>
            /// 試験車名
            /// </summary>
            public string Name { get; set; }
        }




        #endregion
    }
}
