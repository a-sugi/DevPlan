using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.Presentation.UIDevPlan.OuterCar;
using DevPlan.UICommon;
using DevPlan.UICommon.Attributes;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Util;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using DevPlan.Presentation.UIDevPlan.TruckSchedule;

namespace DevPlan.Presentation.UIDevPlan.CarShare
{
    /// <summary>
    /// 車両検索
    /// </summary>
    public partial class CarShareForm : BaseForm
    {
        #region メンバ変数
        private int CondHeight = 100;

        private Dictionary<Control, ControlPropaties> ControlIinitialPropaties = new Dictionary<Control, ControlPropaties>();

        private StringBuilder HelpMessage = new StringBuilder();

        private bool IsDraging = false;
        private Point? DiffPoint = null;

        private CustomTemplate CarDataCarShareTemplate = null;
        private CustomTemplate CarDataOuter1Template = null;
        private CustomTemplate CarDataOuter2Template = null;
        private CustomTemplate CarDataOtherTemplate = null;
        private CustomTemplate CarDataExclusiveTemplate = null;
        private CustomTemplate CarDataAllTemplate = null;

        private CustomTemplate ReserveCarDataTemplate = null;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>
        /// MultiRowデータテーブル。
        /// </summary>
        /// <remarks>
        /// データテーブルのレコードを元にMultiRowのテンプレートを作成するために内部フィールドとして定義しています。
        /// </remarks>
        private DataTable dt;

        /// <summary>
        /// 表示調整コントローラ初期値
        /// </summary>
        private class ControlPropaties
        {
            /// <summary>位置</summary>
            public Point Location { get; set; }
            /// <summary>サイズ</summary>
            public Size Size { get; set; }
        }

        //CSV出力項目
        private readonly string[] CsvHeaders =
        {
            "今どこ",
            "CAR_GROUP",
            "GENERAL_CODE",
            "登録ナンバー",
            "駐車場番号",
            "所在地",
            "FLAG_要予約許可",
            "予約現況",
            "FLAG_空時間貸出可",
            "FLAG_ETC付",
            "FLAG_ナビ付",
            "仕向地",
            "排気量",
            "E_G型式",
            "駆動方式",
            "トランスミッション",
            "車型",
            "グレード",
            "型式符号",
            "車体色",
            "試作時期",
            "リース満了日",
            "処分予定年月",
            "管理票NO",
            "車体番号",
            "E_G番号",
            "固定資産NO",
            "リースNO",
            "研命ナンバー",
            "研命期間",
            "車検登録日",
            "車検期限",
            "車検期限まで残り",
        };
        //Append Start 2022/02/21 杉浦 トラック予約一覧を追加
        public string beforeChecked { get; set; }
        //Append End 2022/02/21 杉浦 トラック予約一覧を追加
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "車両検索"; } }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CarShareForm()
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
            // 権限
            this.UserAuthority = base.GetFunction(FunctionID.CarShare);

            // 画面初期化
            this.InitForm();

            // 初期プロパティ退避
            this.SetControlPropaties();

            // MultiRowの初期化(車両検索)
            this.InitCarDataMultiRow();

            // MultiRowの初期化(予約済み一覧)
            this.InitReserveMultiRow();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            HelpMessage.AppendLine("外製車1(SJSB-G管理No付)");
            HelpMessage.AppendLine("　・予約方法に\"仮予約・・・本予約\"と記載がある車両は、ダブルクリックで対象車系の予約画面を表示します。");
            HelpMessage.AppendLine("　・予約方法に\"所属主査にTEL\"と記載ある車両を使用したい場合は、所属主査にお問い合わせ下さい。");
            HelpMessage.AppendLine("");
            HelpMessage.AppendLine("外製車2（参考）");
            HelpMessage.AppendLine("　・スバル車以外でナンバー無しの車両を表示します。");
            HelpMessage.AppendLine("　・利用可否は不明です。必要な場合は、直接所属部署にお問い合わせ下さい。");
            HelpMessage.AppendLine("");
            HelpMessage.AppendLine("その他外製車（参考）");
            HelpMessage.AppendLine("　・SJSB-G各車系担当者管理外の外製車リストです。（外製車1以外の外製車）参考までに掲載します。");
            HelpMessage.AppendLine("　・利用可否は不明です。必要な場合は、直接所属部署にお問い合わせ下さい。");
            HelpMessage.AppendLine("");
            HelpMessage.AppendLine("専用車（参考）");
            HelpMessage.AppendLine("　・各部署専用車のリストです。　");
            HelpMessage.AppendLine("　・本システムでは予約できません。利用したい場合は所属部署と相談してく下さい。");

#if DEBUG
            // タブ背景色(個別対応)
            this.tabPage1.BackColor
                = this.tabPage2.BackColor = Color.FromArgb(255, 230, 255);
#endif
            
            //空車期間の設定
            this.SetDateRange();

            //予約者プルダウン作成
            this.ReservationComboBox.Items.Add(SessionDto.DepartmentCode + " " + SessionDto.SectionCode + " " + SessionDto.UserName);
            this.ReservationComboBox.Tag = SessionDto.UserId;
            this.ReservationComboBox.SelectedIndex = 0;

            // ラジオボタンのデフォルトセット
            this.CarShareRadioButton.Checked = true;
        }

        /// <summary>
        /// MultiRowの初期化(車両検索)
        /// </summary>
        private void InitCarDataMultiRow()
        {
            //MultiRowのヘッダ生成
            dt = new DataTable("Car");
            dt.Columns.Add("今どこ");
            dt.Columns.Add("分類");
            dt.Columns.Add("CAR_GROUP");
            dt.Columns.Add("GENERAL_CODE");
            dt.Columns.Add("メーカー名");
            dt.Columns.Add("外製車名");
            dt.Columns.Add("DEPARTMENT_CODE");
            dt.Columns.Add("SECTION_CODE");
            dt.Columns.Add("SECTION_GROUP_CODE");
            dt.Columns.Add("予約方法");
            dt.Columns.Add("登録ナンバー");
            dt.Columns.Add("駐車場番号");
            dt.Columns.Add("所在地");
            dt.Columns.Add("FLAG_要予約許可");
            dt.Columns.Add("FLAG_空時間貸出可");
            dt.Columns.Add("予約現況");
            dt.Columns.Add("FLAG_ETC付");
            dt.Columns.Add("FLAG_ナビ付");
            dt.Columns.Add("仕向地");
            dt.Columns.Add("排気量");
            dt.Columns.Add("E_G型式");
            dt.Columns.Add("駆動方式");
            dt.Columns.Add("トランスミッション");
            dt.Columns.Add("車型");
            dt.Columns.Add("グレード");
            dt.Columns.Add("型式符号");
            dt.Columns.Add("車体色");
            dt.Columns.Add("試作時期");
            dt.Columns.Add("リース満了日");
            dt.Columns.Add("処分予定年月");
            dt.Columns.Add("管理票NO");
            dt.Columns.Add("車体番号");
            dt.Columns.Add("E_G番号");
            dt.Columns.Add("固定資産NO");
            dt.Columns.Add("リースNO");
            dt.Columns.Add("研命ナンバー");
            dt.Columns.Add("研命期間");
            dt.Columns.Add("車検登録日");
            dt.Columns.Add("車検期限");
            dt.Columns.Add("車検期限まで残り");
            dt.Columns.Add("廃艦日");
            dt.Columns.Add("号車");
            dt.Columns.Add("名称備考");
            dt.Columns.Add("試験目的");
            dt.Columns.Add("メモ");
            dt.Columns.Add("FLAG_CLASS");
            dt.Columns.Add("ID");

            var dataList = new Dictionary<string, CustomMultiRowCellStyle>();
            foreach (var col in dt.Columns)
            {
                var style = new CustomMultiRowCellStyle();
                style.Width = 50;
                style.Type = MultiRowCellType.TEXT;
                style.HeaderText = col.ToString();
                style.Visible = true;

                dataList.Add(col.ToString(), style);
            }

            dataList["今どこ"].HeaderText = "今どこ";
            dataList["ID"].HeaderText = "ID";
            dataList["CAR_GROUP"].HeaderText = "車系";
            dataList["GENERAL_CODE"].HeaderText = "開発\r\n符号";
            dataList["DEPARTMENT_CODE"].HeaderText = "部";
            dataList["SECTION_CODE"].HeaderText = "課";
            dataList["SECTION_GROUP_CODE"].HeaderText = "担当";
            dataList["FLAG_要予約許可"].HeaderText = "予約許可";
            dataList["FLAG_空時間貸出可"].HeaderText = "空き時間";
            dataList["FLAG_ETC付"].HeaderText = "ETC";
            dataList["FLAG_ナビ付"].HeaderText = "ナビ";
            dataList["E_G型式"].HeaderText = "EG\r\n型式";
            dataList["トランスミッション"].HeaderText = "T/M";
            dataList["処分予定年月"].HeaderText = "固定資産\r\n処分予定年月";
            dataList["管理票NO"].HeaderText = "管理票No";
            dataList["E_G番号"].HeaderText = "EG番号";
            dataList["固定資産NO"].HeaderText = "固定資産\r\nNo";
            dataList["リースNO"].HeaderText = "リースNo";
            dataList["車検期限"].HeaderText = "次回車検\r\n期限";

            dataList["駐車場番号"].HeaderText = "駐車場\r\n番号";
            dataList["FLAG_要予約許可"].HeaderText = "予約\r\n許可";
            dataList["駆動方式"].HeaderText = "駆動\r\n方式";
            dataList["研命ナンバー"].HeaderText = "研命\r\nナンバー";
            dataList["車検登録日"].HeaderText = "車検\r\n登録日";

            dataList["今どこ"].Width = 50;
            dataList["ID"].Width = 50;

            dataList["分類"].Width = 80;
            dataList["登録ナンバー"].Width = 120;
            dataList["駐車場番号"].Width = 60;
            dataList["FLAG_空時間貸出可"].Width = 60;
            dataList["FLAG_ETC付"].Width = 40;
            dataList["FLAG_ナビ付"].Width = 40;
            dataList["E_G型式"].Width = 46;
            dataList["駆動方式"].Width = 46;
            dataList["トランスミッション"].Width = 46;
            dataList["車型"].Width = 46;
            dataList["グレード"].Width = 100;
            dataList["型式符号"].Width = 120;
            dataList["車体色"].Width = 100;
            dataList["試作時期"].Width = 60;
            dataList["リース満了日"].Width = 80;
            dataList["処分予定年月"].Width = 84;
            dataList["管理票NO"].Width = 60;
            dataList["車体番号"].Width = 100;
            dataList["E_G番号"].Width = 80;
            dataList["固定資産NO"].Width = 60;
            dataList["リースNO"].Width = 120;
            dataList["研命ナンバー"].Width = 70;
            dataList["研命期間"].Width = 80;
            dataList["車検登録日"].Width = 80;
            dataList["車検期限"].Width = 80;
            dataList["車検期限まで残り"].Width = 80;

            dataList["メーカー名"].Width = 100;
            dataList["外製車名"].Width = 120;
            dataList["SECTION_GROUP_CODE"].Width = 80;
            dataList["予約方法"].Width = 100;
            dataList["廃艦日"].Width = 80;

            dataList["メモ"].Width = 500;
            dataList["試験目的"].Width = 300;
            dataList["予約現況"].Width = 400;

            dataList["予約現況"].DataCellStyle.Multiline = MultiRowTriState.True;

            dataList["予約方法"].DataCellStyle.Multiline = MultiRowTriState.True;

            dataList["試験目的"].DataCellStyle.Multiline = MultiRowTriState.True;
            dataList["メモ"].DataCellStyle.Multiline = MultiRowTriState.True;

            dataList["リース満了日"].Type = MultiRowCellType.DATETIME;
            dataList["研命期間"].Type = MultiRowCellType.DATETIME;
            dataList["車検登録日"].Type = MultiRowCellType.DATETIME;
            dataList["車検期限"].Type = MultiRowCellType.DATETIME;
            dataList["処分予定年月"].Type = MultiRowCellType.DATETIME;
            dataList["処分予定年月"].CustomFormat = "yyyy/MM";
            dataList["廃艦日"].Type = MultiRowCellType.DATETIME;

            dataList["今どこ"].Type = MultiRowCellType.LINKLABEL;
            dataList["今どこ"].DataCellStyle.TextAlign = MultiRowContentAlignment.MiddleCenter;

            var formName = this.FormTitle + this.CarShareTabControl.SelectedIndex + FormControlUtil.GetRadioButtonValue(this.StatusPanel);
            var configDisplayList = base.GetUserDisplayConfiguration(formName, dataList);

            // テンプレートの設定
            if (this.CarShareRadioButton.Checked)
            {
                // カーシェア車
                this.CarDataCarShareTemplate = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataCarShareTemplate;
            }
            if (this.Out1RadioButton.Checked)
            {
                // 外製車1
                this.CarDataOuter1Template = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataOuter1Template;
            }
            if (this.Out2RadioButton.Checked)
            {
                // 外製車2
                this.CarDataOuter2Template = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataOuter2Template;
            }
            if (this.OtherRadioButton.Checked)
            {
                // その他外製車
                this.CarDataOtherTemplate = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataOtherTemplate;
            }
            if (this.ExclusiveRadioButton.Checked)
            {
                // 専用車
                this.CarDataExclusiveTemplate = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataExclusiveTemplate;
            }
            if (this.AllRadioButton.Checked)
            {
                // 全保有車
                this.CarDataAllTemplate = new CustomTemplate(SetSortNumber(dataList), true, this.CarDataMultiRow, this.SearchRowCountLabel, configDisplayList);
                this.CarDataMultiRow.Template = this.CarDataAllTemplate;
            }
        }

        /// <summary>
        /// MultiRowの初期化(予約済み一覧)
        /// </summary>
        private void InitReserveMultiRow()
        {
            var dataList = new Dictionary<string, CustomMultiRowCellStyle>();
            foreach (var col in typeof(CarShareReserveOutModel).GetProperties())
            {
                var style = new CustomMultiRowCellStyle((CellSettingAttribute)Attribute.GetCustomAttribute(col, typeof(CellSettingAttribute)), col.Name);

                style.Width = 50;
                style.Type = MultiRowCellType.TEXT;

                dataList.Add(col.Name, style);
            }

            dataList["開発符号"].Width = 60;
            dataList["メーカー名"].Width = 100;
            dataList["車名"].Width = 120;
            dataList["種別"].Width = 100;
            dataList["駐車場番号"].Width = 70;
            //Update Start 2022/02/21 杉浦 トラック予約一覧を追加
            //dataList["START_DATE"].Width = 110;
            //dataList["END_DATE"].Width = 110;
            dataList["START_DATE"].Width = 120;
            dataList["END_DATE"].Width = 120;
            //Update End 2022/02/21 杉浦 トラック予約一覧を追加
            dataList["NAME"].Width = 90;
            dataList["依頼者"].Width = 90;
            dataList["発送者"].Width = 90;
            dataList["受領者"].Width = 90;
            dataList["運転者A"].Width = 90;
            dataList["運転者B"].Width = 90;
            dataList["DESCRIPTION"].Width = 150;
            dataList["行先"].Width = 80;
            dataList["管理票番号"].Width = 60;           
            dataList["ID"].Width = 0;

            //Update Start 2022/02/21 杉浦 トラック予約一覧を追加
            //dataList["START_DATE"].CustomFormat = "yyyy/MM/dd HH時";
            //dataList["END_DATE"].CustomFormat = "yyyy/MM/dd HH時";
            dataList["START_DATE"].CustomFormat = "yyyy/MM/dd HH時mm分";
            dataList["END_DATE"].CustomFormat = "yyyy/MM/dd HH時mm分";
            //Update End 2022/02/21 杉浦 トラック予約一覧を追加

            dataList["START_DATE"].Type = MultiRowCellType.DATETIME_HOUR;
            dataList["END_DATE"].Type = MultiRowCellType.DATETIME_HOUR;
            dataList["XEYE_EXIST"].Type = MultiRowCellType.LINKLABEL;
            dataList["XEYE_EXIST"].DataCellStyle.TextAlign = MultiRowContentAlignment.MiddleCenter;

            var formName = this.FormTitle + this.CarShareTabControl.SelectedIndex;
            var configDisplayList = base.GetUserDisplayConfiguration(formName, dataList);

            // テンプレートの設定
            this.ReserveCarDataTemplate = new CustomTemplate(dataList, true, ReservationCarDataMultiRow, this.ReserveRowCountLabel, configDisplayList);

            this.ReservationCarDataMultiRow.Template = this.ReserveCarDataTemplate;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareForm_Shown(object sender, EventArgs e)
        {
            // 一覧を未選択状態に設定
            this.CarDataMultiRow.CurrentCell = null;

            this.ActiveControl = this.StartDateNullableDateTimePicker;
            this.StartDateNullableDateTimePicker.Focus();
        }
        #endregion

        #region 表示調整コントローラの初期プロパティの退避
        /// <summary>
        /// 表示調整コントローラの初期プロパティの退避
        /// </summary>
        private void SetControlPropaties()
        {
            new List<Control> { this.SearchConditionTableLayoutPanel, this.SearchButton, this.ClearButton, this.GridViewLabel, this.MainPanel, this.KusyaKikanLabel }
            .ForEach(x => ControlIinitialPropaties.Add(x, new ControlPropaties { Location = x.Location, Size = x.Size }));
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
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, CondHeight);

            this.SearchButton.Visible = this.SearchConditionTableLayoutPanel.Visible;
            this.ClearButton.Visible = this.SearchConditionTableLayoutPanel.Visible;
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
            //空車期間
            this.SetDateRange();
        }
        #endregion

        #region 空車期間設定
        /// <summary>
        /// 空車期間設定
        /// </summary>
        private void SetDateRange()
        {
            // 空車期間(From)
            this.StartDateNullableDateTimePicker.Value = null;
            this.StartHourComboBox.SelectedIndex = 0;

            // 空車期間(To)
            this.EndDateNullableDateTimePicker.Value = null;
            this.EndHourComboBox.SelectedIndex = this.EndHourComboBox.Items.Count - 1;

            // カラー設定
            SetErrorStyleDateTime(this.StartDateNullableDateTimePicker, this.StartHourComboBox, Const.DefaultBackColor);
            SetErrorStyleDateTime(this.EndDateNullableDateTimePicker, this.EndHourComboBox, Const.DefaultBackColor);
        }
        #endregion

        #region ヘルプ表示
        /// <summary>
        /// ヘルプボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageButton_Click(object sender, EventArgs e)
        {
            // 表示されている場合は非表示にして終了
            if (this.HelpLabel.Visible)
            {
                //非表示
                this.HelpLabel.Visible = false;
                return;
            }

            var btn = (Button)sender;

            var x = this.SearchConditionTableLayoutPanel.Location.X + btn.Location.X + btn.Size.Width + 2;
            var y = this.SearchConditionTableLayoutPanel.Location.Y + btn.Location.Y + 2;

            // 整形
            this.HelpLabel.Text = this.HelpMessage.ToString();
            this.HelpLabel.Location = new Point(x, y);
                
            //表示
            this.HelpLabel.Visible = true;
        }

        /// <summary>
        /// マウスダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var ctl = (Label)sender;

            Cursor.Current = Cursors.Hand;
            IsDraging = true;
            DiffPoint = e.Location;
        }

        /// <summary>
        /// マウスムーブイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDraging)
            {
                return;
            }

            var ctl = (Label)sender;

            int x = ctl.Location.X + e.X - DiffPoint.Value.X;
            int y = ctl.Location.Y + e.Y - DiffPoint.Value.Y;

            if (x <= 0) x = 0;
            if (y <= 0) y = 0;

            ctl.Location = new Point(x, y);
        }

        /// <summary>
        /// マウスアップイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpLabel_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Default;
            IsDraging = false;

            if (e.Button != MouseButtons.Left)
            {
                return;
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
            FormControlUtil.FormWait(this, () =>
            {
                //必須チェック
                if (false == CheckKind())
                {
                    return;
                }

                //情報取得
                var items = this.GetList();
                if (items == null)
                {
                    return;
                }

                //グリッド表示
                SetGridData(items);

            });

        }
        #endregion

        #region 必須チェック

        /// <summary>
        /// 日付＆時間→日時変換処理。
        /// </summary>
        /// <remarks>
        /// 渡された時間とテキストを日時形式に変換します。
        /// </remarks>
        /// <param name="time">日付</param>
        /// <param name="timerText">時間テキスト</param>
        /// <returns>日時</returns>
        private DateTime? GetDateTime(DateTime? time, string timerText)
        {
            if (time == null || string.IsNullOrWhiteSpace(timerText) == true)
            {
                return null;
            }

            return time.Value.AddHours(int.Parse(timerText));
        }
        
        /// <summary>
        /// 必須チェック
        /// </summary>
        private bool CheckKind()
        {
            SetErrorStyleDateTime(this.StartDateNullableDateTimePicker, this.StartHourComboBox, Const.DefaultBackColor);
            SetErrorStyleDateTime(this.EndDateNullableDateTimePicker, this.EndHourComboBox, Const.DefaultBackColor);

            var selectStartDate = this.StartDateNullableDateTimePicker.SelectedDate;
            var selectEndDate = this.EndDateNullableDateTimePicker.SelectedDate;

            var startDateTime = this.GetDateTime(selectStartDate, this.StartHourComboBox.Text);
            var endDateTime = this.GetDateTime(selectEndDate, this.EndHourComboBox.Text);

            //空車期間Toのみ入力かどうか
            if (startDateTime == null && endDateTime != null)
            {
                this.StartDateNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                this.EndDateNullableDateTimePicker.BackColor = Const.ErrorBackColor;

                Messenger.Warn(Resources.KKM03015);
                return false;
            }

            //空車期間Fromのみ入力かどうか
            if (startDateTime != null && endDateTime == null)
            {
                this.StartDateNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                this.EndDateNullableDateTimePicker.BackColor = Const.ErrorBackColor;

                Messenger.Warn(Resources.KKM03015);
                return false;
            }

            //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
            if (startDateTime != null && endDateTime != null && startDateTime > endDateTime)
            {
                SetErrorStyleDateTime(this.StartDateNullableDateTimePicker, this.StartHourComboBox, Const.ErrorBackColor);
                SetErrorStyleDateTime(this.EndDateNullableDateTimePicker, this.EndHourComboBox, Const.ErrorBackColor);

                Messenger.Warn(Resources.KKM00018);
                return false;
            }

            return true;

        }

        /// <summary>
        /// 時間コントロール背景色変更処理。
        /// </summary>
        /// <param name="picker">NullableDateTimePicker</param>
        /// <param name="timerComboBox">ComboBox</param>
        /// <param name="color">設定カラー</param>
        private void SetErrorStyleDateTime(NullableDateTimePicker picker, ComboBox timerComboBox, Color color)
        {
            picker.BackColor = color;
            FormControlUtil.SetComboBoxBackColor(timerComboBox, color);
        }

        /// <summary>
        /// 日時作成
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        private DateTime CheckDate(NullableDateTimePicker date, ComboBox hour)
        {
            DateTime dt = (DateTime)(DateTimeUtil.ConvertDateStringToDateTime(date.Text));
            return dt.AddHours(Convert.ToInt32(hour.Text));
        }
        #endregion

        #region グリッド表示
        /// <summary>
        /// グリッド表示
        /// </summary>
        /// <param name="items"></param>
        private void SetGridData(List<CarOutModel> items)
        {
            //フォーカス
            this.ActiveControl = CarDataMultiRow;

            //行選択を行わない
            CarDataMultiRow.CurrentCell = null;

            dt.Clear();
            foreach (var item in items)
            {
                var row = dt.NewRow();

                if (item.今どこ != null)
                    row["今どこ"] = " Map";
                else
                    row["今どこ"] = null;
                row["分類"] = item.分類;
                row["CAR_GROUP"] = item.車系;
                row["GENERAL_CODE"] = item.開発符号;
                row["メーカー名"] = item.メーカー名;
                row["外製車名"] = item.外製車名;

                row["DEPARTMENT_CODE"] = item.DEPARTMENT_CODE;

                row["SECTION_CODE"] = item.SECTION_CODE;
                row["SECTION_GROUP_CODE"] = item.SECTION_GROUP_CODE;
                if (item.FLAG_CLASS != null && 0 < item.FLAG_CLASS.Length)
                {
                    if (item.FLAG_要予約許可 == 1)
                    {
                        row["予約方法"] = "仮予約 … 本sys" + Environment.NewLine + "本予約 … TEL";
                    }
                    else
                    {
                        row["予約方法"] = "本予約SYS";
                    }
                }
                else
                {
                    row["予約方法"] = "所属主査にTEL";
                }
                row["登録ナンバー"] = item.登録ナンバー;
                row["駐車場番号"] = item.駐車場番号;
                row["所在地"] = item.所在地;
                row["FLAG_要予約許可"] = item.FLAG_要予約許可 == 1 ? "必要" : "";
                if (item.FLAG_CLASS != "カーシェア日程")
                {
                    row["FLAG_空時間貸出可"] = "-";
                    row["予約現況"] = "-";
                }
                else
                {
                    row["FLAG_空時間貸出可"] = item.FLAG_空時間貸出可 == 1 ? "貸出可" : "";
                    row["予約現況"] = item.予約現況;
                }
                row["FLAG_ETC付"] = item.FLAG_ETC付 == 1 ? "有" : "無";
                row["FLAG_ナビ付"] = item.FLAG_ナビ付 == 1 ? "有" : "無";
                row["仕向地"] = item.仕向地;
                row["排気量"] = item.排気量;
                row["E_G型式"] = item.E_G型式;
                row["駆動方式"] = item.駆動方式;
                row["トランスミッション"] = item.トランスミッション;
                row["車型"] = item.車型;
                row["グレード"] = item.グレード;
                row["型式符号"] = item.型式符号;
                row["車体色"] = item.車体色;
                row["試作時期"] = item.試作時期;
                row["リース満了日"] = item.リース満了日;
                row["処分予定年月"] = item.処分予定年月;
                row["管理票NO"] = item.管理票番号;
                row["車体番号"] = item.車体番号;
                row["E_G番号"] = item.E_G番号;
                row["固定資産NO"] = item.固定資産NO;
                row["リースNO"] = item.リースNO;
                row["研命ナンバー"] = item.研命ナンバー;
                row["研命期間"] = item.研命期間;
                row["車検登録日"] = item.車検登録日;
                row["車検期限"] = item.車検期限;
                row["車検期限まで残り"] = item.車検期限まで残り;
                row["廃艦日"] = item.廃艦日;
                row["号車"] = item.号車;
                row["名称備考"] = item.名称備考;
                row["試験目的"] = item.試験目的;
                row["メモ"] = item.メモ;
                row["FLAG_CLASS"] = item.FLAG_CLASS;
                row["ID"] = item.ID;

                dt.Rows.Add(row);
            }

            // データバインド
            ((CustomTemplate)this.CarDataMultiRow.Template).SetDataSource(dt);

            for (int i = 0; i < this.CarDataMultiRow.RowCount; i++)
            {
                this.CarDataMultiRow.Rows[i][1].PerformVerticalAutoFit();
            }
            this.CarDataMultiRow.ResumeLayout();
            this.CarDataMultiRow.CurrentCell = null;
        }

        private Dictionary<string, CustomMultiRowCellStyle> SetSortNumber(Dictionary<string, CustomMultiRowCellStyle> dataList)
        {
            // カーシェア車
            if (this.CarShareRadioButton.Checked)
            {
                dataList["CAR_GROUP"].DisplayIndex = 0;
                dataList["GENERAL_CODE"].DisplayIndex = 1;
                dataList["登録ナンバー"].DisplayIndex = 2;
                dataList["駐車場番号"].DisplayIndex = 3;
                dataList["所在地"].DisplayIndex = 4;
                dataList["FLAG_要予約許可"].DisplayIndex = 5;
                dataList["予約現況"].DisplayIndex = 6;
                dataList["FLAG_空時間貸出可"].DisplayIndex = 7;
                dataList["FLAG_ETC付"].DisplayIndex = 8;
                dataList["FLAG_ナビ付"].DisplayIndex = 9;
                dataList["仕向地"].DisplayIndex = 10;
                dataList["排気量"].DisplayIndex = 11;
                dataList["E_G型式"].DisplayIndex = 12;
                dataList["駆動方式"].DisplayIndex = 13;
                dataList["トランスミッション"].DisplayIndex = 14;
                dataList["車型"].DisplayIndex = 15;
                dataList["グレード"].DisplayIndex = 16;
                dataList["型式符号"].DisplayIndex = 17;
                dataList["車体色"].DisplayIndex = 18;
                dataList["試作時期"].DisplayIndex = 19;
                dataList["リース満了日"].DisplayIndex = 20;
                dataList["処分予定年月"].DisplayIndex = 21;
                dataList["管理票NO"].DisplayIndex = 22;
                dataList["車体番号"].DisplayIndex = 23;
                dataList["E_G番号"].DisplayIndex = 24;
                dataList["固定資産NO"].DisplayIndex = 25;
                dataList["リースNO"].DisplayIndex = 26;
                dataList["研命ナンバー"].DisplayIndex = 27;
                dataList["研命期間"].DisplayIndex = 28;
                dataList["車検登録日"].DisplayIndex = 29;
                dataList["車検期限"].DisplayIndex = 30;
                dataList["車検期限まで残り"].DisplayIndex = 31;

                dataList["分類"].Visible = false;
                dataList["メーカー名"].Visible = false;
                dataList["外製車名"].Visible = false;
                dataList["SECTION_CODE"].Visible = false;
                dataList["SECTION_GROUP_CODE"].Visible = false;
                dataList["予約方法"].Visible = false;
                dataList["廃艦日"].Visible = false;
                dataList["号車"].Visible = false;
                dataList["名称備考"].Visible = false;
                dataList["試験目的"].Visible = false;
                dataList["メモ"].Visible = false;
                dataList["FLAG_CLASS"].Visible = false;
                dataList["DEPARTMENT_CODE"].Visible = false;

                dataList["所在地"].HeaderText = "所在地";
            }

            // 外製車1
            if (this.Out1RadioButton.Checked)
            {
                dataList["メーカー名"].DisplayIndex = 0;
                dataList["外製車名"].DisplayIndex = 1;
                dataList["SECTION_GROUP_CODE"].DisplayIndex = 2;
                dataList["予約方法"].DisplayIndex = 3;
                dataList["登録ナンバー"].DisplayIndex = 4;
                dataList["廃艦日"].DisplayIndex = 5;
                dataList["FLAG_ナビ付"].DisplayIndex = 6;
                dataList["FLAG_ETC付"].DisplayIndex = 7;
                dataList["仕向地"].DisplayIndex = 8;
                dataList["排気量"].DisplayIndex = 9;
                dataList["E_G型式"].DisplayIndex = 10;
                dataList["駆動方式"].DisplayIndex = 11;
                dataList["トランスミッション"].DisplayIndex = 12;
                dataList["車型"].DisplayIndex = 13;
                dataList["車体色"].DisplayIndex = 14;
                dataList["グレード"].DisplayIndex = 15;
                dataList["リース満了日"].DisplayIndex = 16;
                dataList["処分予定年月"].DisplayIndex = 17;
                dataList["管理票NO"].DisplayIndex = 18;
                dataList["車体番号"].DisplayIndex = 19;
                dataList["E_G番号"].DisplayIndex = 20;
                dataList["固定資産NO"].DisplayIndex = 21;
                dataList["リースNO"].DisplayIndex = 22;
                dataList["研命ナンバー"].DisplayIndex = 23;
                dataList["研命期間"].DisplayIndex = 24;
                dataList["車検登録日"].DisplayIndex = 25;
                dataList["車検期限"].DisplayIndex = 26;
                dataList["車検期限まで残り"].DisplayIndex = 27;

                dataList["分類"].Visible = false;
                dataList["FLAG_CLASS"].Visible = false;
                dataList["CAR_GROUP"].Visible = false;
                dataList["GENERAL_CODE"].Visible = false;
                dataList["SECTION_CODE"].Visible = false;
                dataList["駐車場番号"].Visible = false;
                dataList["所在地"].Visible = false;
                dataList["FLAG_要予約許可"].Visible = false;
                dataList["予約現況"].Visible = false;
                dataList["FLAG_空時間貸出可"].Visible = false;
                dataList["型式符号"].Visible = false;
                dataList["試作時期"].Visible = false;
                dataList["号車"].Visible = false;
                dataList["名称備考"].Visible = false;
                dataList["試験目的"].Visible = false;
                dataList["メモ"].Visible = false;
                dataList["DEPARTMENT_CODE"].Visible = false;

                dataList["SECTION_GROUP_CODE"].HeaderText = "所属主査";
                dataList["所在地"].HeaderText = "所在地";
            }

            // 外製車2, その他外製車
            if (this.Out2RadioButton.Checked || this.OtherRadioButton.Checked)
            {
                dataList["メーカー名"].DisplayIndex = 0;
                dataList["外製車名"].DisplayIndex = 1;
                dataList["登録ナンバー"].DisplayIndex = 2;
                dataList["廃艦日"].DisplayIndex = 3;
                dataList["所在地"].DisplayIndex = 4;

                dataList["DEPARTMENT_CODE"].DisplayIndex = 5;

                dataList["SECTION_CODE"].DisplayIndex = 6;
                dataList["SECTION_GROUP_CODE"].DisplayIndex = 7;
                dataList["仕向地"].DisplayIndex = 8;
                dataList["排気量"].DisplayIndex = 9;
                dataList["E_G型式"].DisplayIndex = 10;
                dataList["駆動方式"].DisplayIndex = 11;
                dataList["トランスミッション"].DisplayIndex = 12;
                dataList["車型"].DisplayIndex = 13;
                dataList["車体色"].DisplayIndex = 14;
                dataList["グレード"].DisplayIndex = 15;
                dataList["リース満了日"].DisplayIndex = 16;
                dataList["処分予定年月"].DisplayIndex = 17;
                dataList["管理票NO"].DisplayIndex = 18;
                dataList["車体番号"].DisplayIndex = 19;
                dataList["E_G番号"].DisplayIndex = 20;
                dataList["固定資産NO"].DisplayIndex = 21;
                dataList["リースNO"].DisplayIndex = 22;
                dataList["研命ナンバー"].DisplayIndex = 23;
                dataList["研命期間"].DisplayIndex = 24;
                dataList["車検登録日"].DisplayIndex = 25;
                dataList["車検期限"].DisplayIndex = 26;
                dataList["車検期限まで残り"].DisplayIndex = 27;

                dataList["分類"].Visible = false;
                dataList["FLAG_CLASS"].Visible = false;
                dataList["CAR_GROUP"].Visible = false;
                dataList["予約方法"].Visible = false;
                dataList["駐車場番号"].Visible = false;
                dataList["FLAG_要予約許可"].Visible = false;
                dataList["予約現況"].Visible = false;
                dataList["FLAG_空時間貸出可"].Visible = false;
                dataList["FLAG_ETC付"].Visible = false;
                dataList["FLAG_ナビ付"].Visible = false;
                dataList["型式符号"].Visible = false;
                dataList["試作時期"].Visible = false;
                dataList["号車"].Visible = false;
                dataList["名称備考"].Visible = false;
                dataList["試験目的"].Visible = false;
                dataList["メモ"].Visible = false;
                dataList["GENERAL_CODE"].Visible = false;

                dataList["SECTION_GROUP_CODE"].HeaderText = "担当";
                dataList["所在地"].HeaderText = "所在地";
            }

            // 専用車
            if (this.ExclusiveRadioButton.Checked)
            {
                dataList["CAR_GROUP"].DisplayIndex = 0;
                dataList["GENERAL_CODE"].DisplayIndex = 1;
                dataList["登録ナンバー"].DisplayIndex = 2;
                dataList["SECTION_CODE"].DisplayIndex = 3;
                dataList["SECTION_GROUP_CODE"].DisplayIndex = 4;
                dataList["駐車場番号"].DisplayIndex = 5;
                dataList["FLAG_ETC付"].DisplayIndex = 6;
                dataList["FLAG_ナビ付"].DisplayIndex = 7;
                dataList["仕向地"].DisplayIndex = 8;
                dataList["排気量"].DisplayIndex = 9;
                dataList["E_G型式"].DisplayIndex = 10;
                dataList["駆動方式"].DisplayIndex = 11;
                dataList["トランスミッション"].DisplayIndex = 12;
                dataList["車型"].DisplayIndex = 13;
                dataList["グレード"].DisplayIndex = 14;
                dataList["型式符号"].DisplayIndex = 15;
                dataList["車体色"].DisplayIndex = 16;
                dataList["試作時期"].DisplayIndex = 17;
                dataList["リース満了日"].DisplayIndex = 18;
                dataList["処分予定年月"].DisplayIndex = 19;
                dataList["管理票NO"].DisplayIndex = 20;
                dataList["車体番号"].DisplayIndex = 21;
                dataList["E_G番号"].DisplayIndex = 22;
                dataList["固定資産NO"].DisplayIndex = 23;
                dataList["リースNO"].DisplayIndex = 24;
                dataList["研命ナンバー"].DisplayIndex = 25;
                dataList["研命期間"].DisplayIndex = 26;
                dataList["車検登録日"].DisplayIndex = 27;
                dataList["車検期限"].DisplayIndex = 28;
                dataList["車検期限まで残り"].DisplayIndex = 29;

                dataList["分類"].Visible = false;
                dataList["FLAG_CLASS"].Visible = false;
                dataList["メーカー名"].Visible = false;
                dataList["外製車名"].Visible = false;
                dataList["予約方法"].Visible = false;
                dataList["所在地"].Visible = false;
                dataList["FLAG_要予約許可"].Visible = false;
                dataList["予約現況"].Visible = false;
                dataList["FLAG_空時間貸出可"].Visible = false;
                dataList["廃艦日"].Visible = false;
                dataList["号車"].Visible = false;
                dataList["名称備考"].Visible = false;
                dataList["試験目的"].Visible = false;
                dataList["メモ"].Visible = false;
                dataList["DEPARTMENT_CODE"].Visible = false;

                dataList["SECTION_GROUP_CODE"].HeaderText = "担当";
                dataList["所在地"].HeaderText = "所在地";
            }

            // 全保有車
            if (this.AllRadioButton.Checked)
            {
                dataList["分類"].DisplayIndex = 0;
                dataList["所在地"].DisplayIndex = 1;
                dataList["管理票NO"].DisplayIndex = 2;
                dataList["CAR_GROUP"].DisplayIndex = 3;
                dataList["GENERAL_CODE"].DisplayIndex = 4;
                dataList["試作時期"].DisplayIndex = 5;
                dataList["号車"].DisplayIndex = 6;
                dataList["メーカー名"].DisplayIndex = 7;
                dataList["外製車名"].DisplayIndex = 8;
                dataList["名称備考"].DisplayIndex = 9;
                dataList["登録ナンバー"].DisplayIndex = 10;
                dataList["廃艦日"].DisplayIndex = 11;
                dataList["車型"].DisplayIndex = 12;
                dataList["仕向地"].DisplayIndex = 13;
                dataList["排気量"].DisplayIndex = 14;
                dataList["E_G型式"].DisplayIndex = 15;
                dataList["駆動方式"].DisplayIndex = 16;
                dataList["トランスミッション"].DisplayIndex = 17;
                dataList["グレード"].DisplayIndex = 18;
                dataList["型式符号"].DisplayIndex = 19;
                dataList["車体色"].DisplayIndex = 20;
                dataList["試験目的"].DisplayIndex = 21;
                dataList["FLAG_ETC付"].DisplayIndex = 22;
                dataList["FLAG_ナビ付"].DisplayIndex = 23;
                dataList["車体番号"].DisplayIndex = 24;
                dataList["E_G番号"].DisplayIndex = 25;
                dataList["SECTION_CODE"].DisplayIndex = 26;
                dataList["SECTION_GROUP_CODE"].DisplayIndex = 27;
                dataList["駐車場番号"].DisplayIndex = 28;
                dataList["リース満了日"].DisplayIndex = 29;
                dataList["処分予定年月"].DisplayIndex = 30;
                dataList["固定資産NO"].DisplayIndex = 31;
                dataList["リースNO"].DisplayIndex = 32;
                dataList["研命ナンバー"].DisplayIndex = 33;
                dataList["研命期間"].DisplayIndex = 34;
                dataList["車検登録日"].DisplayIndex = 35;
                dataList["車検期限"].DisplayIndex = 36;
                dataList["車検期限まで残り"].DisplayIndex = 37;
                dataList["メモ"].DisplayIndex = 38;
                dataList["FLAG_CLASS"].DisplayIndex = 39;

                dataList["FLAG_CLASS"].Visible = false;
                dataList["予約方法"].Visible = false;
                dataList["FLAG_要予約許可"].Visible = false;
                dataList["予約現況"].Visible = false;
                dataList["FLAG_空時間貸出可"].Visible = false;
                dataList["DEPARTMENT_CODE"].Visible = false;

                dataList["SECTION_GROUP_CODE"].HeaderText = "担当";
                dataList["所在地"].HeaderText = "G or T";
            }

            dataList["ID"].Visible = true;
            dataList["ID"].Width = 0;

            return dataList;
        }

        #endregion

        #region 情報取得
        /// <summary>
        /// 情報取得
        /// </summary>
        private List<CarOutModel> GetList()
        {
            //車両区分
            int carClass = Convert.ToInt32(AllRadioButton.Tag.ToString());

            foreach (var r in this.StatusPanel.Controls)
            {
                //選択しているラジオボタンの値を取得
                if (r is RadioButton)
                {
                    var radio = r as RadioButton;
                    if (radio.Checked == true)
                    {
                        carClass = Convert.ToInt32(radio.Tag.ToString());
                        break;
                    }
                }
            }

            var paramCond = new CarInModel
            {
                //車両区分
                CAR_CLASS = carClass,

                //空車期間
                EMPTY_DATE_FROM = (StartDateNullableDateTimePicker.Value) == null ? (DateTime?)null :
                                    new DateTime(((DateTime)StartDateNullableDateTimePicker.Value).Year,
                                                ((DateTime)StartDateNullableDateTimePicker.Value).Month,
                                                ((DateTime)StartDateNullableDateTimePicker.Value).Day,
                                                Convert.ToInt32(StartHourComboBox.Text),
                                                0, 0),

                EMPTY_DATE_TO = (EndDateNullableDateTimePicker.Value) == null ? (DateTime?)null :
                                    new DateTime(((DateTime)EndDateNullableDateTimePicker.Value).Year,
                                                ((DateTime)EndDateNullableDateTimePicker.Value).Month,
                                                ((DateTime)EndDateNullableDateTimePicker.Value).Day,
                                                Convert.ToInt32(EndHourComboBox.Text),
                                                0, 0)
            };

            //APIで取得
            var res2 = HttpUtil.GetResponse<CarInModel, CarOutModel>(ControllerType.Car, paramCond);

            // バインド初期化
            this.CarDataMultiRow.DataSource = null;
            
            //レスポンスが取得できたかどうか
            var list2 = new List<CarOutModel>();
            if (res2 == null || res2.Status != Const.StatusSuccess)
            {
                MessageLabel.Text = Resources.KKM00005;
                MessageLabel.ForeColor = Color.Red;
            }
            else
            {
                MessageLabel.Text = "";
                MessageLabel.ForeColor = Color.Black;
                list2.AddRange(res2.Results);
            }

            //返却
            return list2;
        }
        #endregion

        #region セルダブルクリック
        /// <summary>
        /// セルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarDataMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            //カーシェア車、外製車１以外の場合は画面遷移を行わない。
            if (CarShareRadioButton.Checked == false && Out1RadioButton.Checked == false)
            {
                return;
            }

            if (e.Scope == CellScope.Row)
            {
                var id = ((GcMultiRow)sender).Rows[e.RowIndex].Cells["ID"].Value.ToString();
                if (string.IsNullOrWhiteSpace(id) == false)
                {
                    var selectData = ((DataTable)(((GcMultiRow)sender).DataSource)).Select(string.Format("ID = '{0}'", id));

                    this.OpenCarForm(selectData[0].Field<string>("CAR_GROUP"), selectData[0].Field<string>("FLAG_CLASS"), MessageLabel, selectData[0].Field<string>("ID"));
                }
            }
        }
        #endregion

        #region カーシェア日程 or 外製車日程フォーム表示処理
        /// <summary>
        /// カーシェア日程 or 外製車日程フォーム表示処理。
        /// </summary>
        /// <remarks>
        /// 指定された車系およびFLAG_CLASSを元に、該当の日程フォームを表示します。
        /// </remarks>
        /// <param name="carGroup">車系</param>
        /// <param name="flagClass">FLAG_CLASS</param>
        /// <param name="id">項目ID</param>
        /// <param name="selectData">期間開始日</param>
        /// <param name="msgLabel">開けなかった場合等メッセージ表示対象ラベルコントロール</param>
        private void OpenCarForm(string carGroup, string flagClass, Label msgLabel, string id, CarShareReserveOutModel selectData = null)
        {
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            if (!string.IsNullOrEmpty(carGroup)){
                //利用可能な開発符号が存在しない場合、画面遷移は行わない
                var generalCodeAuthorityCond = new GeneralCodeSearchInModel
                {
                    CLASS_DATA = 1,
                    CAR_GROUP = carGroup,
                    PERSONEL_ID = SessionDto.UserId
                };

                var generalCodeAuthority = HttpUtil.GetResponse<GeneralCodeSearchInModel,
                    GeneralCodeSearchOutModel>
                    (ControllerType.GeneralCode, generalCodeAuthorityCond);

                if (generalCodeAuthority.Status != Const.StatusSuccess)
                {
                    msgLabel.Text = Resources.KKM03012;
                    msgLabel.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    msgLabel.Text = "";
                    msgLabel.ForeColor = Color.Black;
                }
            }
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加

            if (flagClass != null)
            {
                if (flagClass == "カーシェア日程")
                {
                    if (this.IsFunctionEnable(FunctionID.CarShare) == false)
                    {
                        msgLabel.Text = Resources.TCM03009;
                        msgLabel.ForeColor = Color.Red;
                        return;
                    }

                    var calendarFirstDate = DateTime.Today;
                    if (selectData != null)
                    {
                        var checkStart = (selectData.START_DATE != null) ? selectData.START_DATE : null;
                        var checkEnd = (selectData.END_DATE != null) ? selectData.END_DATE : null;
                        if (checkStart == null || checkEnd == null) { return; }//遷移ができないので何も行わない（本来なら無いはず）
                        calendarFirstDate = checkStart.Value;
                    }

                    var form = new CarShareScheduleForm
                    {
                        CarShareScheduleSearchCond = new CarShareScheduleSearchModel
                        {
                            CAR_GROUP = carGroup
                        }
                    };
                    if (id != "") form.CalendarCategoryId = long.Parse(id);
                    form.CalendarFirstDate = calendarFirstDate;
                    form.Show();
                }
                else if (flagClass == "外製車日程")
                {
                    if (this.IsFunctionEnable(FunctionID.OuterCar) == false)
                    {
                        msgLabel.Text = Resources.TCM03009;
                        msgLabel.ForeColor = Color.Red;
                        return;
                    }
                    var form = new OuterCarForm
                    {
                        OuterCarScheduleSearchCond = new OuterCarScheduleItemGetInModel
                        {
                            車系 = carGroup
                        }
                    };
                    if (id != "") form.CalendarCategoryId = long.Parse(id);
                    form.CalendarFirstDate = (selectData == null || selectData.START_DATE == null) ? DateTime.Now : selectData.START_DATE.Value;
                    form.Show();
                }
            }
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            else
            {
                if (this.IsFunctionEnable(FunctionID.Truck) == false)
                {
                    msgLabel.Text = Resources.TCM03009;
                    msgLabel.ForeColor = Color.Red;
                    return;
                }

                var calendarFirstDate = DateTime.Today;
                var calendarEndDate = DateTime.Today;
                if (selectData != null)
                {
                    var checkStart = (selectData.START_DATE != null) ? selectData.START_DATE : null;
                    DateTime? checkEnd = (selectData.END_DATE != null) ? selectData.END_DATE : checkStart.HasValue ? checkStart.Value.AddDays(1) : (DateTime?)null;
                    if (checkStart == null || checkEnd == null) { return; }
                    calendarFirstDate = checkStart.Value;
                    calendarEndDate = checkEnd.Value;
                }

                var form = new TruckScheduleForm
                {
                    TruckScheduleItemSearchCond = new TruckScheduleItemSearchModel
                    {
                        BEFORE_DATE = null,
                        AFTER_DATE = null,
                        EMPTY_START_DATE = calendarFirstDate,
                        EMPTY_END_DATE = calendarEndDate,
                        INPUT_PERSONEL_ID = SessionDto.UserId
                    }
                };
                if (id != "") form.CalendarCategoryId = long.Parse(id);
                form.CalendarFirstDate = calendarFirstDate;
                form.Show();
            }
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加
        }
        #endregion

        //予約済一覧
        #region 予約者コンボボックスクリック
        /// <summary>
        /// 予約者コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            using (var form = new UserListForm { DepartmentCode = SessionDto.DepartmentCode, SectionCode = SessionDto.SectionCode, UserAuthority = this.UserAuthority, StatusCode = "a" })
            {
                //ユーザー検索画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //ユーザーをセット
                    this.ReservationComboBox.Items.Clear();
                    this.ReservationComboBox.Items.Add(form.User.DEPARTMENT_CODE + " " + form.User.SECTION_CODE + " " + form.User.NAME);
                    this.ReservationComboBox.Tag = form.User.PERSONEL_ID;
                    this.ReservationComboBox.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region 特定しないチェックボックスクリック
        /// <summary>
        /// 特定しないチェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //予約者コンボボックスクリックの操作可/不可を反転
            ReservationComboBox.Enabled = !ReservationComboBox.Enabled;

            if (ReservationCheckBox.Checked)
            {
                rdoStatusAll.Checked = false;
                rdoStatusAll.Enabled = false;
                rdoStatusToday.Checked = true;
                //Append Start 2022/02/21 杉浦 トラック予約一覧を追加
                if (rdoReserver.Checked) beforeChecked = "rdoReserver";
                else if (rdoOrder.Checked) beforeChecked = "rdoOrder";
                else if (rdoSender.Checked) beforeChecked = "rdoSender";
                else if (rdoServer.Checked) beforeChecked = "rdoServer";
                else if (rdoDriverA.Checked) beforeChecked = "rdoDriverA";
                else if (rdoDriverB.Checked) beforeChecked = "rdoDriverB";
                rdoReserver.Checked = true;
                rdoOrder.Enabled = false;
                rdoSender.Enabled = false;
                rdoServer.Enabled = false;
                rdoDriverA.Enabled = false;
                rdoDriverB.Enabled = false;
                //Append End 2022/02/21 杉浦 トラック予約一覧を追加
            }
            else
            {
                rdoStatusAll.Enabled = true;
                //Append Start 2022/02/21 杉浦 トラック予約一覧を追加
                if (beforeChecked == "rdoReserver") rdoReserver.Checked = true;
                else if (beforeChecked == "rdoOrder") rdoOrder.Checked = true;
                else if (beforeChecked == "rdoSender") rdoSender.Checked = true;
                else if (beforeChecked == "rdoServer") rdoServer.Checked = true;
                else if (beforeChecked == "rdoDriverA") rdoDriverA.Checked = true;
                else if (beforeChecked == "rdoDriverB") rdoDriverB.Checked = true;
                rdoOrder.Enabled = true;
                rdoSender.Enabled = true;
                rdoServer.Enabled = true;
                rdoDriverA.Enabled = true;
                rdoDriverB.Enabled = true;
                //Append End 2022/02/21 杉浦 トラック予約一覧を追加
            }
        }
        #endregion

        #region 検索ボタンクリック
        /// <summary>
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationSearchButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //予約済み情報取得
                var items = this.GetReservationList();
                if (items == null)
                {
                    return;
                }

                //グリッド表示
                SetReservationGridData(items);

            });

        }
        #endregion

        #region 予約済み情報取得
        /// <summary>
        /// 予約済み情報取得
        /// </summary>
        private List<CarShareReserveOutModel> GetReservationList()
        {
            //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            var list = new List<CarShareReserveOutModel>();
            if (this.rdoReserver.Checked)
            {
                //Append End 2022/01/11 杉浦 トラック予約一覧を追加
                //予約者
                string reservation = null;
                if (ReservationCheckBox.Checked == false)
                {
                    reservation = ReservationComboBox.Tag.ToString();
                }

                var paramCond = new CarShareReserveInModel
                {
                    //予約者ID
                    予約者_ID = reservation,

                    FLAG_RESERVE = rdoStatusToday.Checked == false ? 0 : 1

                };

                //APIで取得
                var res = HttpUtil.GetResponse<CarShareReserveInModel, CarShareReserveOutModel>(ControllerType.CarShareReserve, paramCond);

                //レスポンスが取得できたかどうか
                //Delete Start 2022/01/11 杉浦 トラック予約一覧を追加
                //var list = new List<CarShareReserveOutModel>();
                //Delete End 2022/01/11 杉浦 トラック予約一覧を追加
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    ReservationMessageLabel.Text = Resources.KKM00005;
                    ReservationMessageLabel.ForeColor = Color.Red;
                }
                else
                {
                    ReservationMessageLabel.Text = "";
                    ReservationMessageLabel.ForeColor = Color.Black;
                    list.AddRange(res.Results);
                }
                //Append Start 2022/01/11 杉浦 トラック予約一覧を追加
            }else
            {
                //ユーザーID取得
                string userId = null;
                if (ReservationCheckBox.Checked == false)
                {
                    userId = ReservationComboBox.Tag.ToString();
                }
                var paramCond = new TruckReserveInModel();
                if (this.rdoReserver.Checked) paramCond.予約者_ID = userId;
                if (this.rdoOrder.Checked) paramCond.依頼者_ID = userId;
                if (this.rdoSender.Checked) paramCond.発送者_ID = userId;
                if (this.rdoServer.Checked) paramCond.受領者_ID = userId;
                if (this.rdoDriverA.Checked) paramCond.運転者A_ID = userId;
                if (this.rdoDriverB.Checked) paramCond.運転者B_ID = userId;
                paramCond.FLAG_RESERVE = rdoStatusToday.Checked == false ? 0 : 1;

                //APIで取得
                var res = HttpUtil.GetResponse<TruckReserveInModel, CarShareReserveOutModel>(ControllerType.TruckReserve, paramCond);

                //レスポンスが取得できたかどうか
                //Delete Start 2022/01/11 杉浦 トラック予約一覧を追加
                //var list = new List<CarShareReserveOutModel>();
                //Delete End 2022/01/11 杉浦 トラック予約一覧を追加
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    ReservationMessageLabel.Text = Resources.KKM00005;
                    ReservationMessageLabel.ForeColor = Color.Red;
                }
                else
                {
                    ReservationMessageLabel.Text = "";
                    ReservationMessageLabel.ForeColor = Color.Black;
                    list.AddRange(res.Results);
                }
            }
            //Append End 2022/01/11 杉浦 トラック予約一覧を追加

            //返却
            return list;
        }
        #endregion

        #region グリッド表示
        /// <summary>
        /// グリッド表示
        /// </summary>
        /// <param name="items"></param>
        private void SetReservationGridData(List<CarShareReserveOutModel> items)
        {
            //フォーカス
            this.ActiveControl = ReservationCarDataMultiRow;

            // データバインド
            this.ReserveCarDataTemplate.SetDataSource(items);
            
            // 行選択解除
            this.ReservationCarDataMultiRow.CurrentCell = null;
        }
        #endregion

        #region セルダブルクリック
        /// <summary>
        /// MultiRowセルダブルクリックイベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservationCarDataMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                //Update Start 2022/02/21 杉浦 トラック予約一覧を追加
                //var id = Convert.ToInt64(((GcMultiRow)sender).Rows[e.RowIndex].Cells["ID"].Value);
                //var selectData = ((List<CarShareReserveOutModel>)(((GcMultiRow)sender).DataSource)).Find(x => x.ID == id);
                var selectData = ((List<CarShareReserveOutModel>)(((GcMultiRow)sender).DataSource))[e.RowIndex];
                //Update End 2022/02/21 杉浦 トラック予約一覧を追加

                this.OpenCarForm(selectData.CAR_GROUP, selectData.FLAG_CLASS, ReservationMessageLabel, selectData.ID.ToString(), selectData);
            }
        }
        #endregion

        #region 作業履歴ボタンの表示切替
        /// <summary>
        /// 作業履歴ボタンの表示切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CarShareTabControl.SelectedIndex == 0)
            {
                HistoryButton.Visible = false;

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = this.CarShareRadioButton.Checked;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            }
            else
            {
                HistoryButton.Visible = true;

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            }
        }
        #endregion

        #region 作業履歴ボタンクリック
        /// <summary>
        /// 作業履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryButton_Click(object sender, EventArgs e)
        {
            //選択行がない場合メッセージを表示して終了
            if (ReservationCarDataMultiRow.SelectedRows.Count == 0)
            {
                ReservationMessageLabel.Text = Resources.KKM00009;
                ReservationMessageLabel.ForeColor = Color.Red;
                return;
            }

            var selectRow = (CarShareReserveOutModel)ReservationCarDataMultiRow.SelectedRows[0].DataBoundItem;

            //作業履歴画面表示
            if (selectRow.FLAG_CLASS == "カーシェア日程")
            {
                CarShareScheduleItemModel item = new CarShareScheduleItemModel();
                item.ID = selectRow.ID;

                new FormUtil(new CarShareHistoryForm { ScheduleItem = item, UserAuthority = base.GetFunction(FunctionID.CarShare) }).SingleFormShow(this);
            }
            else if (selectRow.FLAG_CLASS == "外製車日程")
            {
                OuterCarScheduleItemGetOutModel item = new OuterCarScheduleItemGetOutModel();
                item.CATEGORY_ID = selectRow.ID;

                new FormUtil(new OuterCarHistoryForm { ScheduleItem = item, UserAuthority = base.GetFunction(FunctionID.OuterCar) }).SingleFormShow(this);
            }
        }
        #endregion

        #region カーシェア車ラジオボタンボタンクリック
        /// <summary>
        /// カーシェア車ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarShareRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.CarShareRadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataCarShareTemplate == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataCarShareTemplate;

                    // データバインド
                    this.CarDataCarShareTemplate.SetDataSource(null);
                }

                this.SearchControlVisible(true);

                this.SetItems();

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = true;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理

            });
        }
        #endregion

        #region 外製車1ラジオボタンボタンクリック
        /// <summary>
        /// 外製車1ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Out1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Out1RadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataOuter1Template == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataOuter1Template;

                    // データバインド
                    this.CarDataOuter1Template.SetDataSource(null);
                }

                this.SearchControlVisible(true);

                this.SetItems();

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            });
        }
        #endregion

        #region 外製車2ラジオボタンボタンクリック
        /// <summary>
        /// 外製車2ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Out2RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.Out2RadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                this.SetItems();

                this.SearchControlVisible(false);

                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataOuter2Template == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataOuter2Template;

                    // データバインド
                    this.CarDataOuter2Template.SetDataSource(null);
                }

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            });
        }
        #endregion

        #region その他外製車ラジオボタンボタンクリック
        /// <summary>
        /// その他外製車ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.OtherRadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                this.SetItems();

                this.SearchControlVisible(false);

                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataOtherTemplate == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataOtherTemplate;

                    // データバインド
                    this.CarDataOtherTemplate.SetDataSource(null);
                }

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            });
        }
        #endregion

        #region 専用車ラジオボタンボタンクリック
        /// <summary>
        /// 専用車ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExclusiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.ExclusiveRadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                this.SetItems();

                this.SearchControlVisible(false);

                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataExclusiveTemplate == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataExclusiveTemplate;

                    // データバインド
                    this.CarDataExclusiveTemplate.SetDataSource(null);
                }

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            });
        }
        #endregion

        #region 全保有車両ラジオボタンボタンクリック
        /// <summary>
        /// 全保有車両ラジオボタンボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!AllRadioButton.Checked) return;

            FormControlUtil.FormWait(this, () =>
            {
                this.SetItems();

                this.SearchControlVisible(false);

                if (this.dt != null)
                {
                    // テンプレート設定
                    if (this.CarDataAllTemplate == null)
                    {
                        this.InitCarDataMultiRow();
                    }

                    this.CarDataMultiRow.Template = this.CarDataAllTemplate;

                    // データバインド
                    this.CarDataAllTemplate.SetDataSource(null);
                }

                //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
                this.DownloadButton.Visible = false;
                //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理
            });
        }

        /// <summary>
        /// 検索条件コントロール表示非表示処理。
        /// </summary>
        /// <remarks>
        /// 渡された条件がTrueの場合はコントロールを表示し、
        /// Falseの場合はコントロールを非表示にします。
        /// </remarks>
        /// <param name="isVisible"></param>
        private void SearchControlVisible(bool isVisible)
        {
            var diff = 0;
            var addj = 0;

            if (isVisible)
            {
                if (this.CondHeight != 100)
                {
                    diff = ControlIinitialPropaties[this.KusyaKikanLabel].Size.Height + 1;
                    addj = ControlIinitialPropaties[this.GridViewLabel].Size.Height;

                    this.MainPanel.Location = ControlIinitialPropaties[this.MainPanel].Location;
                    this.MainPanel.Size = new Size(this.MainPanel.Size.Width, this.MainPanel.Size.Height - diff - addj);

                    this.SearchButton.Location = ControlIinitialPropaties[this.SearchButton].Location;
                    this.ClearButton.Location = ControlIinitialPropaties[this.ClearButton].Location;
                    this.ClearButton.Visible = true;

                    this.SearchConditionTableLayoutPanel.Size = new Size(this.SearchConditionTableLayoutPanel.Size.Width, ControlIinitialPropaties[this.SearchConditionTableLayoutPanel].Size.Height);

                    this.CondHeight = 100;
                }
            }
            else
            {
                if (this.CondHeight != 56)
                {
                    diff = ControlIinitialPropaties[this.KusyaKikanLabel].Size.Height + 1;
                    addj = ControlIinitialPropaties[this.GridViewLabel].Size.Height;

                    this.SearchConditionTableLayoutPanel.Size = new Size(this.SearchConditionTableLayoutPanel.Size.Width, ControlIinitialPropaties[this.SearchConditionTableLayoutPanel].Size.Height - diff);

                    this.SearchButton.Location = new Point(ControlIinitialPropaties[this.SearchButton].Location.X, ControlIinitialPropaties[this.SearchButton].Location.Y - diff);
                    this.ClearButton.Location = new Point(ControlIinitialPropaties[this.ClearButton].Location.X, ControlIinitialPropaties[this.ClearButton].Location.Y - diff);
                    this.ClearButton.Visible = false;

                    this.MainPanel.Location = new Point(ControlIinitialPropaties[this.MainPanel].Location.X, ControlIinitialPropaties[this.MainPanel].Location.Y - diff - addj);
                    this.MainPanel.Size = new Size(this.MainPanel.Size.Width, this.MainPanel.Size.Height + diff + addj);

                    this.CondHeight = 56;
                }
            }
        }
        #endregion

        #region 検索条件設定
        /// <summary>
        /// 検索条件設定
        /// </summary>
        private void SetItems()
        {
            //全保有車検索
            if (AllRadioButton.Checked == true)
            {
                this.SetSearchItems(false);
            }
            else
            {
                this.SetSearchItems(true);
            }

            //カーシェア車、外製車１以外の場合は画面遷移を行わない。（グリッドメッセージ）
            if (CarShareRadioButton.Checked == false && Out1RadioButton.Checked == false)
            {
                this.GridViewLabel.Visible = false;
            }
            else
            {
                this.GridViewLabel.Visible = true;
            }

            //メッセージラベル初期化
            this.MessageLabel.Text = "";
        }
        #endregion

        #region 検索条件設定
        /// <summary>
        /// 検索条件設定
        /// </summary>
        /// <param name="enable"></param>
        private void SetSearchItems(bool enable)
        {
            //検索条件
            this.StartDateNullableDateTimePicker.Enabled = enable;
            this.StartHourComboBox.Enabled = enable;
            this.EndDateNullableDateTimePicker.Enabled = enable;
            this.EndHourComboBox.Enabled = enable;
        }
        #endregion

        #region ファイルリンククリック
        /// <summary>
        /// 群馬ファイルリンク。
        /// </summary>
        /// <remarks>
        /// AppConfigからSKCの駐車場PDFのURLを取得し、開きます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GunmaLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = new AppConfigAccessor().GetAppSetting("gunmaPdfFileUrl");
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Messenger.Error(string.Format(Resources.KKM00029, filePath), null);
            }
        }

        /// <summary>
        /// SKCファイルリンク。
        /// </summary>
        /// <remarks>
        /// AppConfigからSKCの駐車場PDFのURLを取得し、開きます。
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkcLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filePath = new AppConfigAccessor().GetAppSetting("skcPdfFileUrl");
            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                Messenger.Error(string.Format(Resources.KKM00029, filePath), null);
            }
        }
        #endregion


        #region MultiRow 共通操作

        /// <summary>
        /// MulutiRow設定アイコンクリック(車両検索)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConfigPictureBox_Click(object sender, EventArgs e)
        {
            var formName = this.FormTitle + this.CarShareTabControl.SelectedIndex + FormControlUtil.GetRadioButtonValue(this.StatusPanel);

            // 表示設定画面表示
            base.ShowDisplayForm(formName, (CustomTemplate)this.CarDataMultiRow.Template);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック(車両検索)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm((CustomTemplate)this.CarDataMultiRow.Template);
        }

        /// <summary>
        /// MulutiRow設定アイコンクリック(予約済み一覧)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReserveConfigPictureBox_Click(object sender, EventArgs e)
        {
            var formName = this.FormTitle + this.CarShareTabControl.SelectedIndex;

            // 表示設定画面表示
            base.ShowDisplayForm(formName, (CustomTemplate)this.ReservationCarDataMultiRow.Template);
        }

        /// <summary>
        /// MulutiRowソートアイコンクリック(予約済み一覧)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReserveSortPictureBox_Click(object sender, EventArgs e)
        {
            // ソート指定画面表示
            base.ShowSortForm((CustomTemplate)this.ReservationCarDataMultiRow.Template);
        }

        #endregion

        private void CarDataMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if(e.CellName == "今どこ")
            {
                if (!string.IsNullOrEmpty(this.CarDataMultiRow.Rows[e.RowIndex].Cells["今どこ"].Value.ToString()))
                {
                    //管理票Noを取得する
                    ScheduleToXeyeSearchModel searchModel = new ScheduleToXeyeSearchModel();
                    searchModel.物品名2 = this.CarDataMultiRow.Rows[e.RowIndex].Cells["管理票NO"].Value.ToString();

                    // XeyeのIDを取得する
                    var res = HttpUtil.GetResponse<ScheduleToXeyeSearchModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, searchModel);
                    var xeyeId = new List<ScheduleToXeyeOutModel>();
                    xeyeId.AddRange(res.Results);

                    // Xeyeページに接続するフォームを起動
                    base.RunXeye(xeyeId[0].備考);
                }
            }
        }

        private void ReservationCarDataMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.CellName == "XEYE_EXIST")
            {
                if (!string.IsNullOrEmpty(this.ReservationCarDataMultiRow.Rows[e.RowIndex].Cells["XEYE_EXIST"].Value.ToString()))
                {
                    //管理票Noを取得する
                    ScheduleToXeyeSearchModel searchModel = new ScheduleToXeyeSearchModel();
                    searchModel.物品名2 = this.ReservationCarDataMultiRow.Rows[e.RowIndex].Cells["管理票番号"].Value.ToString();

                    // XeyeのIDを取得する
                    var res = HttpUtil.GetResponse<ScheduleToXeyeSearchModel, ScheduleToXeyeOutModel>(ControllerType.Xeye, searchModel);
                    var xeyeId = new List<ScheduleToXeyeOutModel>();
                    xeyeId.AddRange(res.Results);

                    // Xeyeページに接続するフォームを起動
                    base.RunXeye(xeyeId[0].備考);
                }
            }
        }

        //Append Start 2021/10/12 杉浦 カーシェア車Excel出力処理
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            //FormControlUtil.FormWait(this, () =>
            //{
            //    new MultiRowUtil(this.CarDataMultiRow).Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
            //});

            List<Row> csvList = new List<Row>();

            foreach (var row in this.CarDataMultiRow.Rows)
            {
                csvList.Add(row);
            }

            if (csvList.Count == 0)
            {
                Messenger.Warn(Resources.TCM03008);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "Excel ブック (*.xlsx)|*.xlsx;", FileName = string.Format("{0}_{1:yyyyMMddHHmmss}", this.FormTitle, DateTime.Now) })
            {
                //保存先が選択されたかどうか
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var map = new Dictionary<int, string>();

                    foreach (var header in CsvHeaders)
                    {
                        var i = 0;

                        foreach (var col in this.CarDataMultiRow.Columns)
                        {
                            //対象の列なら追加
                            if (col.Name == header)
                            {
                                map[i] = header;
                                break;
                            }

                            i++;
                        }
                    }

                    //CSV出力用カラムのカラム名から"CSV"を削除
                    string[] outHeaders = (string[])CsvHeaders.Clone();

                    // Excel出力
                    using (var xls = new XlsUtil())
                    {
                        var sheetName = "Sheet1";

                        var headers = outHeaders.Select((x, i) => new { Key = string.Format("{0}1", xls.GetColumnAddress(i)), Value = x }).ToArray();

                        var lastColumn = xls.GetColumnAddress(headers.Count() - 1);

                        var headerRange = string.Format("A1:{0}1", lastColumn);

                        //ヘッダーを書き込み
                        var headerMap = headers.ToDictionary(x => x.Key, x => x.Value == "CAR_GROUP" ? "車系" : x.Value == "GENERAL_CODE" ? "開発符号" : x.Value == "FLAG_要予約許可" ? "予約許可" : x.Value == "FLAG_空時間貸出可" ? "空き時間" 
                                                                    : x.Value == "FLAG_ETC付" ? "ETC" : x.Value == "FLAG_ナビ付" ? "ナビ" : x.Value == "E_G型式" ? "EG型式" : x.Value == "トランスミッション" ? "T/M" : x.Value == "処分予定年月" ? "固定資産処分予定年月"
                                                                     : x.Value == "管理票NO" ? "管理票No" : x.Value == "E_G番号" ? "EG番号" : x.Value == "固定資産NO" ? "固定資産No" : x.Value == "リースNO" ? "リースNo" : x.Value == "車検期限" ? "次回車検期限" : x.Value);
                        xls.WriteSheet(sheetName, headerMap);

                        //罫線の設定
                        xls.SetBorder(sheetName, headerRange);
                        xls.CopyRow(sheetName, headerRange, string.Format("A2:{0}{1}", lastColumn, csvList.Count() + 1));

                        //シートに書き込み
                        xls.WriteSheet(sheetName, csvList,
                            (row =>
                            {
                                var list = new List<string>();

                                foreach (var kv in map)
                                {
                                    var index = kv.Key;
                                    var header = kv.Value;
                                    var cell = row.Cells[index];
                                    var value = cell.Value;

                                    list.Add(value == null ? "" : value.ToString());
                                }

                                return list.Select((x, i) => new { Key = xls.GetColumnAddress(i), Value = x }).ToDictionary(x => x.Key, x => x.Value);
                            }), 1);

                        //ヘッダーを中寄
                        xls.SetAlignmentCenter(sheetName, string.Format("A1:{0}1", lastColumn));

                        //列幅の自動調整
                        xls.AutoSizeColumn(sheetName, string.Format("A:{0}", lastColumn));

                        // フッターの設定
                        xls.SetFooter(sheetName, string.Format("社外転用禁止\n{0} {1} {2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), SessionDto.SectionCode, SessionDto.UserName));

                        //ブックの保存
                        xls.Save(sfd.FileName);
                    }
                }
            }

        }
        //Append End 2021/10/12 杉浦 カーシェア車Excel出力処理

    }
}
