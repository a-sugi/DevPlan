using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Attributes;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// カーシェア管理一覧
    /// </summary>
    public partial class CarShareManagementForm : BaseForm
    {
        private const int CondHeight = 60;

        /// <summary>
        /// 検索ボタン押下時の検索情報保持内部フィールド。
        /// </summary>
        private CarShareManagementInModel SearchCondition;

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "カーシェア管理一覧"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>背景色(貸出リスト)</summary>
        private Color RendBackColor { get; set; } = Color.FromArgb(230, 255, 255);

        /// <summary>背景色(貸出リスト)</summary>
        private Color ReturnBackColor { get; set; } = Color.FromArgb(255, 255, 159);

        //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
        /// <summary>背景色(翌日貸出リスト)</summary>
        private Color TommorowRendBackColor { get; set; } = Color.FromArgb(252, 217, 151);
        //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

        /// <summary>テンプレートのロードを行う場合True</summary>
        private bool LoadTemplate = true;

        /// <summary>表示件数フォーマット</summary>
        private const string RowCountFormat = "表示件数： {0:#,0}/{1:#,0} 件";

        //Append Start 2021/08/25 矢作
        /// <summary>編集前データ</summary>
        private List<CarShareManagementOutModel> BeforeModify { get; set; } = new List<CarShareManagementOutModel>();
        //Append End 2021/08/25 矢作

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarShareManagementForm()
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
        private void CarShareForm_Load(object sender, EventArgs e)
        {
            //権限
            this.UserAuthority = base.GetFunction(FunctionID.CarShareOffice);

            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //出力権限のないログインユーザの場合、ダウンロードボタンは表示しない。
            if (this.UserAuthority.EXPORT_FLG != '1')
            {
                DownloadButton.Visible = false;
                OperatingRateButton.Visible = false;
            }

            //更新権限のないログインユーザの場合、登録ボタンは表示しない。
            if (this.UserAuthority.UPDATE_FLG != '1')
            {
                EntryButton.Visible = false;
            }

            // 環境依存背景色の退避
            this.RendBackColor = this.ContentsPanel.BackColor;

            //グリッド更新
            SearchItems();
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareManagementForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.CarDataManagementMultiRow.CurrentCell = null;

            this.ActiveControl = this.ReservationTextBox;
            this.ReservationTextBox.Focus();
        }
        #endregion

        #region 検索条件ボタンクリック
        /// <summary>
        /// 検索条件ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionButton_Click(object sender, EventArgs e)
        {
            //検索条件表示設定
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionBottomTableLayoutPanel, this.MainPanel, CondHeight);

            if (this.ClearButton.Visible == false)
            {
                this.SearchButton.Location = new Point(
                    this.SearchButton.Location.X,
                    this.ClearButton.Location.Y);

                this.ClearButton.Visible = true;
            }
            else
            {
                this.SearchButton.Location = new Point(
                    this.SearchButton.Location.X,
                    this.SearchConditionTopTableLayoutPanel.Location.Y + this.SearchConditionTopTableLayoutPanel.Height + 
                    (this.ClearButton.Location.Y -
                    (this.SearchConditionBottomTableLayoutPanel.Location.Y + this.SearchConditionBottomTableLayoutPanel.Height)));

                this.ClearButton.Visible = false;
            }
        }
        #endregion

        #region クリアボタンクリック
        /// <summary>
        /// クリアボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            //検索条件を初期化
            this.ClearSearchCondition();
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearSearchCondition()
        {
            //所在地
            GunmaRadioButton.Checked = true;

            //検索日付
            RendListRadioButton.Checked = true;
            SearchDateNullableDateTimePicker.Value = DateTime.Now;

            //準備状況
            PreparationOffCheckBox.Checked = true;
            PreparationOnCheckBox.Checked = true;

            //貸出状況
            RendOffCheckBox.Checked = true;
            RendOnCheckBox.Checked = true;

            //返却状況
            ReturnOffCheckBox.Checked = true;
            ReturnOnCheckBox.Checked = true;

            //給油状況
            RefuelingOffCheckBox.Checked = true;
            RefuelingOnCheckBox.Checked = true;

            //予約者
            ReservationTextBox.Text = "";

            //駐車場番号
            ParkingNoTextBox.Text = "";
        }
        #endregion

        #region リストのラジオボタン変更
        /// <summary>
        /// リストのラジオボタン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // 選択されている場合は終了
            if ((sender as RadioButton).Checked == true)
            {
                return;
            }

            //登録処理
            EntryItems(true);
            
            var tempWidthDic = new Dictionary<string, int>();

            foreach (var item in this.CarDataManagementMultiRow.ColumnHeaders[0].Cells)
            {
                tempWidthDic.Add(item.Name, item.Width);
            }

            if (this.RendListRadioButton.Checked == true)
            {
                this.ContentsPanel.BackColor = this.RendBackColor;
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                if (tempWidthDic.ContainsKey("Header返却備考"))
                {
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                    tempWidthDic.Add("Header貸出備考", tempWidthDic["Header返却備考"]);
                    tempWidthDic.Remove("Header返却備考");
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                }
                else if (tempWidthDic.ContainsKey("Header翌日貸出備考"))
                {
                    tempWidthDic.Add("Header貸出備考", tempWidthDic["Header翌日貸出備考"]);
                    tempWidthDic.Remove("Header翌日貸出備考");
                }
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                this.LoadTemplate = true;
            }
            else if (this.ReturnListRadioButton.Checked == true)
            {
                this.ContentsPanel.BackColor = this.ReturnBackColor;
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                if (tempWidthDic.ContainsKey("Header貸出備考"))
                {
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                    tempWidthDic.Add("Header返却備考", tempWidthDic["Header貸出備考"]);
                    tempWidthDic.Remove("Header貸出備考");
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                }
                else if (tempWidthDic.ContainsKey("Header翌日貸出備考"))
                {
                    tempWidthDic.Add("Header貸出備考", tempWidthDic["Header翌日貸出備考"]);
                    tempWidthDic.Remove("Header翌日貸出備考");
                }
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                this.LoadTemplate = true;
            }
            //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
            else if (this.TommorowRendListRadioButton.Checked == true)
            {
                this.ContentsPanel.BackColor = this.TommorowRendBackColor;
                if(tempWidthDic.ContainsKey("Header返却備考"))
                {
                    tempWidthDic.Add("Header翌日貸出備考", tempWidthDic["Header返却備考"]);
                    tempWidthDic.Remove("Header返却備考");
                }
                else if (tempWidthDic.ContainsKey("Header貸出備考"))
                {
                    tempWidthDic.Add("Header翌日貸出備考", tempWidthDic["Header貸出備考"]);
                    tempWidthDic.Remove("Header貸出備考");
                }
                this.LoadTemplate = true;
            }
            //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

            FormControlUtil.FormWait(this, () =>
            {
                //情報表示
                this.SearchItems();
            });

            foreach (var item in tempWidthDic)
            {
                if (this.CarDataManagementMultiRow.ColumnHeaders[0].Cells.Any(x => x.Name == item.Key))
                {
                    var targetCell = this.CarDataManagementMultiRow.ColumnHeaders[0][item.Key];
                    if (targetCell.Visible)
                    {
                        targetCell.HorizontalResize(item.Value - targetCell.Width);
                    }
                }
            }
        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {

            //登録処理
            EntryItems(true);

            FormControlUtil.FormWait(this, () =>
            {
                //情報表示
                this.SearchItems();
            });

        }
        #endregion

        #region 情報取得
        /// <summary>
        /// 情報取得
        /// </summary>
        private void SearchItems()
        {
            //所在地
            string location = null;
            foreach (var r in LocationPanel.Controls)
            {
                //選択しているラジオボタンの値を取得
                if (r is RadioButton)
                {
                    var radio = r as RadioButton;
                    if (radio.Checked == true)
                    {
                        location = radio.Text;
                        break;
                    }
                }
            }

            #region MultiRowセル設定

            if (this.LoadTemplate)
            {
                var dataList = new Dictionary<string, CustomMultiRowCellStyle>();

                foreach (var col in typeof(CarShareManagementOutModel).GetProperties())
                {
                    var style = new CustomMultiRowCellStyle((CellSettingAttribute)Attribute.GetCustomAttribute(col, typeof(CellSettingAttribute)), col.Name);

                    style.Width = 50;
                    style.Type = MultiRowCellType.TEXT;

                    dataList.Add(col.Name, style);
                }

                dataList["SCHEDULE_ID"].Width = 0;
                dataList["START_DATE"].Width = 62;
                dataList["END_DATE"].Width = 62;
                dataList["登録ナンバー"].Width = 120;
                dataList["DESCRIPTION"].Width = 100;
                dataList["FLAG_準備済"].Width = 36;
                dataList["FLAG_実使用"].Width = 36;
                dataList["FLAG_返却済"].Width = 36;
                dataList["FLAG_給油済"].Width = 36;
                dataList["返却備考"].Width = 110;
                dataList["貸出備考"].Width = 110;
                dataList["NAME"].Width = 100;
                dataList["FLAG_ETC付"].Width = 40;
                dataList["FLAG_ナビ付"].Width = 40;
                dataList["グレード"].Width = 180;
                dataList["車体色"].Width = 120;
                dataList["管理票番号"].Width = 60;
                //Append Start 2021/10/12 杉浦 カーシェア一覧追加要望
                dataList["PREV_RESERVE"].Width = 180;
                //Append End 2021/10/12 杉浦 カーシェア一覧追加要望
                //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                dataList["入れ替え中車両"].Width = 180;
                //Append End 2022/01/17 杉浦 入れ替え中車両の処理

                dataList["FLAG_準備済"].Type = MultiRowCellType.CHECKBOX;
                dataList["FLAG_実使用"].Type = MultiRowCellType.CHECKBOX;
                dataList["FLAG_返却済"].Type = MultiRowCellType.CHECKBOX;
                dataList["FLAG_給油済"].Type = MultiRowCellType.CHECKBOX;

                dataList["START_DATE"].Type = MultiRowCellType.DATETIME_HOUR;
                dataList["START_DATE"].CustomFormat = "M/d H時";
                dataList["END_DATE"].Type = MultiRowCellType.DATETIME_HOUR;
                dataList["END_DATE"].CustomFormat = "M/d H時";

                dataList["FLAG_準備済"].FilterItem = new CheckBoxFilterItem("準備済", "準備されていない");
                dataList["FLAG_実使用"].FilterItem = new CheckBoxFilterItem("貸出済", "貸出されていない");
                dataList["FLAG_返却済"].FilterItem = new CheckBoxFilterItem("返却済", "返却されていない");
                dataList["FLAG_給油済"].FilterItem = new CheckBoxFilterItem("給油済", "給油されていない");

                dataList["FLAG_準備済"].HeaderCellStyle.BackColor = Color.LightBlue;
                dataList["FLAG_実使用"].HeaderCellStyle.BackColor = Color.LightBlue;
                dataList["FLAG_返却済"].HeaderCellStyle.BackColor = Color.LightBlue;
                dataList["FLAG_給油済"].HeaderCellStyle.BackColor = Color.LightBlue;
                dataList["貸出備考"].HeaderCellStyle.BackColor = Color.LightBlue;
                dataList["返却備考"].HeaderCellStyle.BackColor = Color.LightBlue;
                //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                dataList["入れ替え中車両"].HeaderCellStyle.BackColor = Color.LightBlue;
                //Append End 2022/01/17 杉浦 入れ替え中車両の処理

                dataList["FLAG_準備済"].HeaderCellStyle.ForeColor = Color.Black;
                dataList["FLAG_実使用"].HeaderCellStyle.ForeColor = Color.Black;
                dataList["FLAG_返却済"].HeaderCellStyle.ForeColor = Color.Black;
                dataList["FLAG_給油済"].HeaderCellStyle.ForeColor = Color.Black;
                dataList["貸出備考"].HeaderCellStyle.ForeColor = Color.Black;
                dataList["返却備考"].HeaderCellStyle.ForeColor = Color.Black;
                //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                dataList["入れ替え中車両"].HeaderCellStyle.ForeColor = Color.Black;
                //Append End 2022/01/17 杉浦 入れ替え中車両の処理

                if (RendListRadioButton.Checked == true)
                {
                    dataList["貸出備考"].Visible = true;
                    dataList["返却備考"].Visible = false;
                    //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                    dataList["翌日貸出備考"].Visible = false;
                    //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

                }
                else if (ReturnListRadioButton.Checked == true)
                {
                    dataList["貸出備考"].Visible = false;
                    dataList["返却備考"].Visible = true;
                    //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                    dataList["翌日貸出備考"].Visible = false;
                    //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                }
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                else if (TommorowRendListRadioButton.Checked == true)
                {
                    dataList["貸出備考"].Visible = false;
                    dataList["返却備考"].Visible = false;
                    dataList["翌日貸出備考"].Visible = true;
                }
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

                var formName = this.FormTitle + FormControlUtil.GetRadioButtonValue(this.ListRadioButtonPanel);
                var configDisplayList = base.GetUserDisplayConfiguration(formName, dataList);

                this.CarDataManagementMultiRow.Template = new CustomTemplate(dataList, true, this.CarDataManagementMultiRow, this.RowCountLabel, configDisplayList);
                this.LoadTemplate = false;
            }

            #endregion

            //検索日付
            DateTime? start = null;
            DateTime? end = null;
            if (RendListRadioButton.Checked == true)
            {
                //貸出リスト
                start = Convert.ToDateTime(this.SearchDateNullableDateTimePicker.Value.Date);
            }
            else if (ReturnListRadioButton.Checked == true)
            {
                //返却リスト
                end = Convert.ToDateTime(this.SearchDateNullableDateTimePicker.Value.Date);
            }
            //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
            else if (TommorowRendListRadioButton.Checked == true)
            {
                //翌日貸出リスト
                start = Convert.ToDateTime(this.SearchDateNullableDateTimePicker.Value.Date).AddDays(1);
            }
            //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

            //準備状況
            short? preparation = null;
            if (PreparationOffCheckBox.Checked != PreparationOnCheckBox.Checked)
            {
                preparation = (short?)(PreparationOffCheckBox.Checked == true ? 0 : 1);
            }

            //貸出状況
            short? rend = null;
            if (RendOffCheckBox.Checked != RendOnCheckBox.Checked)
            {
                rend = (short?)(RendOffCheckBox.Checked == true ? 0 : 1);
            }

            //返却状況
            short? returnf = null;
            if (ReturnOffCheckBox.Checked != ReturnOnCheckBox.Checked)
            {
                returnf = (short?)(ReturnOffCheckBox.Checked == true ? 0 : 1);
            }

            //給油状況
            short? refueling = null;
            if (RefuelingOffCheckBox.Checked != RefuelingOnCheckBox.Checked)
            {
                refueling = (short?)(RefuelingOffCheckBox.Checked == true ? 0 : 1);
            }

            var paramCond = new CarShareManagementInModel
            {
                //所在地
                所在地 = location,

                //検索日付(貸出リスト)
                START_DATE = start,

                //検索日付(返却リスト)
                END_DATE = end,

                //準備状況
                FLAG_準備済 = preparation,

                //貸出状況
                FLAG_実使用 = rend,

                //返却状況
                FLAG_返却済 = returnf,

                //給油状況
                FLAG_給油済 = refueling,

                //予約者
                NAME = ReservationTextBox.Text == "" ? null : ReservationTextBox.Text,

                //駐車場番号
                駐車場番号 = ParkingNoTextBox.Text == "" ? null : ParkingNoTextBox.Text,
            };

            //検索条件の保持
            this.SearchCondition = paramCond;

            //APIで取得
            var res = HttpUtil.GetResponse<CarShareManagementInModel, CarShareManagementOutModel>(ControllerType.CarShareManagement, paramCond);

            //レスポンスが取得できたかどうか
            CarDataManagementMultiRow.DataSource = null;
            var list = new List<CarShareManagementOutModel>();
            if (res == null || res.Status != Const.StatusSuccess)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
                return;
            }

            //Append Start 2021/08/25 矢作
            foreach (var result in res.Results)
            {
                this.BeforeModify.Add(new CarShareManagementOutModel() { SCHEDULE_ID = result.SCHEDULE_ID, FLAG_返却済 = result.FLAG_返却済 });
            }
            //Append End 2021/08/25 矢作

            //取得した情報を画面表示
            ((CustomTemplate)CarDataManagementMultiRow.Template).SetDataSource((res.Results).ToList());

            for (int i = 0; i < CarDataManagementMultiRow.Rows.Count; i++)
            {
                CarDataManagementMultiRow.Rows[i][0].PerformVerticalAutoFit();

                CarShareManagementOutModel data = (CarShareManagementOutModel)CarDataManagementMultiRow.Rows[i].DataBoundItem;

                if (data.FLAG_ETC付 == "1")
                {
                    CarDataManagementMultiRow.Rows[i]["FLAG_ETC付"].Value = "有";
                }
                else
                {
                    CarDataManagementMultiRow.Rows[i]["FLAG_ETC付"].Value = "無";
                }

                //ナビ表示
                if (data.FLAG_ナビ付 == "1")
                {
                    CarDataManagementMultiRow.Rows[i]["FLAG_ナビ付"].Value = "有";
                }
                else
                {
                    CarDataManagementMultiRow.Rows[i]["FLAG_ナビ付"].Value = "無";
                }

                //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望
                ////Append Start 2021/06/14 杉浦
                ////予約者所属課
                //if (string.IsNullOrEmpty(data.PREV_SECTION_CODE))
                //{
                //    CarDataManagementMultiRow.Rows[i]["SECTION_CODE"].Value = data.SECTION_CODE;
                //}
                //else
                //{
                //    CarDataManagementMultiRow.Rows[i]["SECTION_CODE"].Value = data.SECTION_CODE + "\n(" + data.PREV_SECTION_CODE + ")";
                //}
                ////予約者
                //if (string.IsNullOrEmpty(data.PREV_NAME))
                //{
                //    CarDataManagementMultiRow.Rows[i]["NAME"].Value = data.NAME;
                //}
                //else
                //{
                //    CarDataManagementMultiRow.Rows[i]["NAME"].Value = data.NAME + "\n(" + data.PREV_NAME + ")";
                //}
                ////使用者Tel
                //if (string.IsNullOrEmpty(data.PREV_TEL))
                //{
                //    CarDataManagementMultiRow.Rows[i]["TEL"].Value = data.TEL;
                //}
                //else
                //{
                //    CarDataManagementMultiRow.Rows[i]["TEL"].Value = data.TEL + "\n(" + data.PREV_TEL + ")";
                //}
                ////Append End 2021/06/14 杉浦
                //Delete Start 2021/10/12 杉浦 カーシェア一覧追加要望

            }

            CarDataManagementMultiRow.ResumeLayout();

            //メッセージラベルを初期化
            SetMessageLabel("", Color.Black);

            //グリッドを非選択
            CarDataManagementMultiRow.CurrentCell = null;
        }
        #endregion

        #region メッセージ表示
        /// <summary>
        /// メッセージ表示
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        private void SetMessageLabel(string message, Color color)
        {
            MessageLabel.Text = message;
            MessageLabel.ForeColor = color;
        }
        #endregion

        #region セルダブルクリック       
        private void CarDataManagementMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            GcMultiRow g = sender as GcMultiRow;

            if (g != null)
            {
                int row = e.RowIndex;

                if (e.Scope == CellScope.Row)
                {
                    //Update Start 2022/02/21 杉浦 入れ替え中車両の処理
                    //if (e.CellName == "貸出備考" || e.CellName == "返却備考") { return; }
                    if (e.CellName == "貸出備考" || e.CellName == "返却備考" || e.CellName == "入れ替え中車両") { return; }
                    //Update End 2022/02/21 杉浦 入れ替え中車両の処理

                    //カーシェア日程
                    if (this.IsFunctionEnable(FunctionID.CarShare) == false)
                    {
                        SetMessageLabel(Resources.TCM03009, Color.Red);
                        return;
                    }

                    var scheduleid = Convert.ToInt64(((GcMultiRow)sender).Rows[e.RowIndex].Cells["SCHEDULE_ID"].Value);
                    var selectData = ((List<CarShareManagementOutModel>)(((GcMultiRow)sender).DataSource)).FirstOrDefault(x => x.SCHEDULE_ID == scheduleid);

                    //当月を含む３ヶ月のみ予約が可能。カレンダー共通、カーシェア日程にも同様の制限が入っている。
                    DateTime toDay = DateTime.Today;
                    DateTime start = new DateTime(toDay.Year, toDay.Month, 1);
                    DateTime end = start.AddMonths(3).AddDays(-1);
                    var checkStart = (selectData.START_DATE != null) ? selectData.START_DATE : null;
                    var checkEnd = (selectData.END_DATE != null) ? selectData.END_DATE : null;

                    //Delete Start 2021/07/20 矢作

                    //if (checkStart == null || checkEnd == null) { return; }//遷移ができないので何も行わない（本来なら無いはず）
                    //if (base.GetFunction(FunctionID.CarShare).MANAGEMENT_FLG == '0')
                    //{
                    //    if (checkEnd.Value.Date >= start && checkStart.Value.Date < start) { checkStart = start; }

                    //    if ((checkStart.Value.Date < start || checkStart.Value.Date > end))
                    //    {
                    //        SetMessageLabel(Resources.KKM01007, Color.Red);
                    //        return;
                    //    }
                    //}

                    //Delete End 2021/07/20 矢作

                    CarShareScheduleForm form = new CarShareScheduleForm();
                    CarShareScheduleSearchModel model = new CarShareScheduleSearchModel();

                    model.CAR_GROUP = selectData.車系;

                    form.CarShareScheduleSearchCond = model;
                    form.CalendarCategoryId = selectData.CATEGORY_ID;
                    form.CalendarFirstDate = checkStart.Value;
                    form.Show();
                    
                }
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
            //Append Start 2022/03/02 杉浦 入れ替え中車両の処理
            //登録前チェック処理
            if (CheckItems())
            {
                //Append End 2022/03/02 杉浦 入れ替え中車両の処理
                //登録処理
                EntryItems();
                //Append Start 2022/03/02 杉浦 入れ替え中車両の処理
            }
            //Append End 2022/03/02 杉浦 入れ替え中車両の処理

        }
        #endregion

        //Append Start 2022/03/02 杉浦 入れ替え中車両の処理
        #region 登録前チェック
        private bool CheckItems()
        {
            List<CarShareManagementOutModel> entryList = new List<CarShareManagementOutModel>();
            bool errFlg = false;

            for (int i = 0; i < CarDataManagementMultiRow.RowCount; i++)
            {
                CarShareManagementOutModel data = (CarShareManagementOutModel)CarDataManagementMultiRow.Rows[i].DataBoundItem;
                if (data.入れ替え中車両 != null && data.入れ替え中車両.Length > 50)
                {
                    errFlg = true;
                    CarDataManagementMultiRow.Rows[i].Cells[21].Style.BackColor = Color.Yellow;
                }else
                {
                    CarDataManagementMultiRow.Rows[i].Cells[21].Style.BackColor = Color.Beige;
                }
            }
            if (errFlg)
            {
                Messenger.Warn("入れ替え中車両は50文字以内で記入してください。");
                return false;
            }
            return true;
        }
        #endregion
        //Append End 2022/03/02 杉浦 入れ替え中車両の処理

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        private void EntryItems(bool closing = false)
        {
            List<CarShareManagementOutModel> entryList = new List<CarShareManagementOutModel>();

            for (int i = 0; i < CarDataManagementMultiRow.RowCount; i++)
            {
                if (CarDataManagementMultiRow.Rows[i].Modified)
                {
                    CarShareManagementOutModel data = (CarShareManagementOutModel)CarDataManagementMultiRow.Rows[i].DataBoundItem;

                    var itemCond = new CarShareManagementOutModel
                    {
                        SCHEDULE_ID = data.SCHEDULE_ID,
                        FLAG_準備済 = data.FLAG_準備済 == 1 ? data.FLAG_準備済 : null,
                        FLAG_実使用 = data.FLAG_実使用 == 1 ? data.FLAG_実使用 : null,
                        FLAG_返却済 = data.FLAG_返却済 == 1 ? data.FLAG_返却済 : null,
                        FLAG_給油済 = data.FLAG_給油済 == 1 ? data.FLAG_給油済 : null,
                        貸出備考 = data.貸出備考 == null ? null : data.貸出備考,
                        //Update Start 2022/01/17 杉浦 入れ替え中車両の処理
                        //返却備考 = data.返却備考 == null ? null : data.返却備考
                        返却備考 = data.返却備考 == null ? null : data.返却備考,
                        入れ替え中車両 = data.入れ替え中車両,
                        CATEGORY_ID = data.CATEGORY_ID
                        //Update End 2022/01/17 杉浦 入れ替え中車両の処理
                    };

                    //リストに追加
                    entryList.Add(itemCond);
                }
            }

            if (0 < entryList.Count)
            {
                //更新の確認
                if (closing == true)
                {
                    //変更があれば確認ダイアログ表示
                    if (Messenger.Confirm(Resources.KKM00006) == DialogResult.No)
                    {
                        // 確認NOの場合終了
                        return;
                    }
                }

                //登録
                var res = HttpUtil.PutResponse<WeeklyReportModel>(ControllerType.CarShareManagement, entryList);

                if ((res == null || res.Status != Const.StatusSuccess))
                {
                    // NGの場合は終了
                    return;
                }

                //Append Start 2021/08/25 矢作

                //スケジュールを更新するIDリストを取得
                List<long> list = new List<long>();

                foreach (var entry in entryList)
                {
                    var addModel = this.BeforeModify.FirstOrDefault(x => x.SCHEDULE_ID == entry.SCHEDULE_ID && (x.FLAG_返却済 == 0 || x.FLAG_返却済 == null) && entry.FLAG_返却済 == 1);
                    if (addModel != null)
                    {
                        list.Add(addModel.SCHEDULE_ID);
                    }
                }

                if(list != null  && list.Count() != 0)
                {
                    CarShareScheduleSearchListModel model = new CarShareScheduleSearchListModel();
                    model.IDList = list.ToArray();

                    //更新するスケジュールデータのリスト
                    List<CarShareScheduleModel> updateDataList = new List<CarShareScheduleModel>();

                    // スケジュールデータの取得
                    var res2 = HttpUtil.GetResponse<CarShareScheduleSearchListModel, CarShareScheduleModel>(ControllerType.CarShareScheduleFromList, model);
                    updateDataList.AddRange(res2.Results);

                    // それぞれのデータの終了時間を現在時刻に変更
                    foreach (var updateData in updateDataList)
                    {
                        var nowDate = DateTime.Now;
                        var timeSpan = TimeSpan.FromHours(1);

                        //Update Start 2022/10/28 杉浦 終了日付の処理変更
                        //updateData.END_DATE = nowDate.AddTicks(-(nowDate.Ticks % timeSpan.Ticks));
                        var now = nowDate.AddTicks(-(nowDate.Ticks % timeSpan.Ticks));

                        if(updateData.END_DATE > now)
                        {
                            if(updateData.START_DATE >= now)
                            {
                                updateData.END_DATE = updateData.START_DATE.Value.AddHours(1);
                            }else
                            {
                                updateData.END_DATE = now;
                            }
                        }
                        //Update End 2022/10/28 杉浦 終了日付の処理変更

                    }

                    // スケジュールデータの更新
                    var res3 = HttpUtil.PutResponse(ControllerType.CarShareSchedule, updateDataList);

                    if ((res3 == null || res3.Status != Const.StatusSuccess))
                    {
                        // NGの場合は終了
                        return;
                    }
                }
                //Append End 2021/08/25 矢作

                //登録完了メッセージ表示
                this.CarDataManagementMultiRow.Modified = false;
                SetMessageLabel(Resources.KKM00002, Color.Red);
            }
        }
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //登録処理
            EntryItems(true);
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            // 出力件数チェック
            if (this.CarDataManagementMultiRow.Rows.GetRowCount(MultiRowElementStates.Visible) <= 0)
            {
                Messenger.Warn(Resources.KKM03039);
                return;
            }

            //保存先の設定
            this.CsvSaveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            MemoryStream file;
            string fileName;

            string fileNameTemplate = "{0}リスト（{1}）_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (this.SearchCondition.START_DATE != null)
            {
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                if (this.RendListRadioButton.Checked)
                {
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
                    fileName = string.Format(fileNameTemplate, "貸出", this.SearchCondition.START_DATE.Value.ToString("yyyyMMdd"));
                    file = new MemoryStream(DevPlan.Presentation.Properties.Resources.CarShareManagementKasiTemplate);
                //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                }
                else
                {
                    //Update Start 2021/10/29 翌日貸出リストの名称を変更する
                    //fileName = string.Format(fileNameTemplate, "翌日貸出", this.SearchCondition.START_DATE.Value.ToString("yyyyMMdd"));
                    fileName = string.Format(fileNameTemplate, "検索日＋１の貸出", this.SearchCondition.START_DATE.Value.ToString("yyyyMMdd"));
                    //Update End 2021/10/29 翌日貸出リストの名称を変更する
                    file = new MemoryStream(DevPlan.Presentation.Properties.Resources.CarShareManagementKasiTemplate);
                }
                //Append End 2021/05/25 杉浦 翌日貸出リストを追加する
            }
            else if (this.SearchCondition.END_DATE != null)
            {
                fileName = string.Format(fileNameTemplate, "返却", this.SearchCondition.END_DATE.Value.ToString("yyyyMMdd"));
                file = new MemoryStream(DevPlan.Presentation.Properties.Resources.CarShareManagementHenkyakuTemplate);
            }
            else
            {
                throw new Exception("貸出・返却のいずれかの取得が出来ません。");
            }

            var outputData = new Dictionary<int, List<string>>();

            for (int rowIndex = 0; rowIndex < this.CarDataManagementMultiRow.RowCount; rowIndex++)
            {
                if ((this.CarDataManagementMultiRow.GetState(rowIndex) & MultiRowElementStates.Visible) == MultiRowElementStates.Visible)
                {
                    var list = new List<string>();

                    CarShareManagementOutModel rowData = (CarShareManagementOutModel)this.CarDataManagementMultiRow.Rows[rowIndex].DataBoundItem;

                    list.Add(rowData.START_DATE.Value.ToString("yyyy/MM/dd HH:mm"));
                    list.Add(rowData.END_DATE.Value.ToString("yyyy/MM/dd HH:mm"));
                    list.Add(rowData.駐車場番号);
                    list.Add(rowData.予約種別);
                    list.Add(rowData.GENERAL_CODE);
                    list.Add(rowData.登録ナンバー);
                    list.Add(rowData.DESCRIPTION);
                    list.Add(rowData.SECTION_CODE + rowData.NAME + rowData.TEL);
                    list.Add(rowData.PREV_RESERVE);
                    //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
                    list.Add(rowData.入れ替え中車両);
                    //Append End 2022/01/17 杉浦 入れ替え中車両の処理
                    list.Add(rowData.FLAG_準備済.ToString());
                    list.Add(rowData.FLAG_実使用.ToString());
                    list.Add(rowData.FLAG_返却済.ToString());
                    list.Add(rowData.FLAG_給油済.ToString());

                    if (RendListRadioButton.Checked)
                    {
                        list.Add(rowData.貸出備考);
                    }

                    if (ReturnListRadioButton.Checked)
                    {
                        list.Add(rowData.返却備考);
                    }
                    //Append Start 2021/05/25 杉浦 翌日貸出リストを追加する
                    if (TommorowRendListRadioButton.Checked)
                    {
                        list.Add(rowData.翌日貸出備考);
                    }
                    //Append End 2021/05/25 杉浦 翌日貸出リストを追加する

                    outputData.Add(rowIndex, list);
                }
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel ブック (*.xlsx)|*.xlsx;";
                dialog.FileName = fileName;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using (var excel = new ExcelPackage(file))
                    {
                        var sheet = excel.Workbook.Worksheets[1];

                        if (outputData.Count > 0)
                        {
                            sheet.InsertRow(2, outputData.Count - 1, 1);

                            int rowCount = 2;
                            foreach (var data in outputData)
                            {
                                for (int i = 1; i < data.Value.ToList().Count + 1; i++)
                                {
                                    sheet.Cells[rowCount, i].Value = data.Value.ToList()[i - 1];

                                    if (i == 1 || i == 2)//１行目or２行目・・。一部の右寄せをしたいがテンプレートで対応できなかったので。
                                    {
                                        sheet.Cells[rowCount, i].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    }
                                }
                                rowCount++;
                            }
                        }

                        sheet.Cells.AutoFitColumns(8);

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
        #endregion

        #region 稼働率算出ボタンクリック
        /// <summary>
        /// 稼働率算出ボタン押下処理。
        /// </summary>
        /// <remarks>
        /// 稼働率算出ウィンドウを表示します。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OperatingRateButton_Click(object sender, EventArgs e)
        {
            using (var form = new OperatingRateForm())
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region MultiRow 共通操作

        /// <summary>
        /// MulutiRow設定アイコンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConfigPictureBox_Click(object sender, EventArgs e)
        {
            // Modify通知
            ((PictureBox)sender).Focus();

            // 登録処理
            this.EntryItems(true);

            var formName = this.FormTitle + FormControlUtil.GetRadioButtonValue(this.ListRadioButtonPanel);

            // 表示設定画面表示
            base.ShowDisplayForm(formName, (CustomTemplate)this.CarDataManagementMultiRow.Template);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック(車両検索)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm((CustomTemplate)this.CarDataManagementMultiRow.Template);
        }

        #endregion

        private void WorkHistoryButton_Click(object sender, EventArgs e)
        {
            string location = null;
            foreach (var r in LocationPanel.Controls)
            {
                //選択しているラジオボタンの値を取得
                if (r is RadioButton)
                {
                    var radio = r as RadioButton;
                    if (radio.Checked == true)
                    {
                        location = radio.Text;
                        break;
                    }
                }
            }

            FormControlUtil.FormWait(this, () =>
            {
                using (var form = new CarShareUseHistoryInputForm() { Establish = location, UserAuthority = this.UserAuthority })
                {
                    form.ShowDialog(this);
                }
            });
        }
    }
}