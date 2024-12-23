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

namespace DevPlan.Presentation.UIDevPlan.ReportMaterial
{
    public partial class ReportMaterialForm : BaseForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "情報元一覧（進捗履歴・週報）"; } }

        /// <summary>
        /// 期間（開始日）
        /// </summary>
        public DateTime? FIRSTDAY { get { return FirstDay; } set { FirstDay = value; } }

        /// <summary>
        /// 期間（終了日）
        /// </summary>
        public DateTime? LASTDAY { get { return LastDay; } set { LastDay = value; } }

        /// <summary>
        /// 戻り値
        /// </summary>
        public List<InfoListModel> RETURN_LIST { get { return ReturnList; } }
        #endregion

        #region 内部変数

        /// <summary>
        /// 期間（開始日）
        /// </summary>
        public DateTime? FirstDay;

        /// <summary>
        /// 期間（終了日）
        /// </summary>
        public DateTime? LastDay;

        /// <summary>
        /// 戻り値
        /// </summary>
        public List<InfoListModel> ReturnList;

        // 前回選択した部署のID
        private string SelectSectionID;
        private string SelectSectionGroupID;

        /// <summary>
        /// 検索結果リスト
        /// </summary>
        private List<InfoListModel> ItemList = new List<InfoListModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReportMaterialForm()
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
        private void InfoListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.Init();

            //戻り値を初期化
            this.ReturnList = new List<InfoListModel>();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //期間を表示
            if (this.FirstDay != null)
            {
                FirstDayNullableDateTimePicker.Text = ((DateTime)this.FirstDay).ToString("yyyy/MM/dd");
            }
            if (this.LastDay != null)
            {
                LastDayNullableDateTimePicker.Text = ((DateTime)this.LastDay).ToString("yyyy/MM/dd");
            }

            //イベント設定及び入力チェック用にtagを設定。
            this.LastDayNullableDateTimePicker.Validated += LastDayNullableDateTimePicker_Validated;
            this.FirstDayNullableDateTimePicker.Validated += FirstDayNullableDateTimePicker_Validated;
            this.LastDayNullableDateTimePicker.CloseUp += LastDayNullableDateTimePicker_CloseUp;
            this.FirstDayNullableDateTimePicker.CloseUp += FirstDayNullableDateTimePicker_CloseUp;
            this.LastDayNullableDateTimePicker.Tag = "ItemName(終了日)";
            this.FirstDayNullableDateTimePicker.Tag = "ItemName(開始日)";

            //選択IDの保存
            this.SelectSectionID = SessionDto.SectionID;
            this.SelectSectionGroupID = SessionDto.SectionGroupID;

            //ログインユーザの担当（進捗履歴）を表示
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;

            //検索実行
            FormControlUtil.FormWait(this, () => SerchItems());
        }
        #endregion

        #region 選択中ラジオボタン取得
        /// <summary>
        /// 選択中ラジオボタン取得
        /// </summary>
        private int GetSelectRadioButton()
        {
            int status = 0;

            foreach (var r in this.StatusPanel.Controls)
            {
                //選択しているラジオボタンの値(Tag)を取得
                if (r is RadioButton)
                {
                    var radio = r as RadioButton;
                    if (radio.Checked == true)
                    {
                        status = Convert.ToInt32(radio.Tag.ToString());
                        break;
                    }
                }
            }

            return status;
        }
        #endregion

        #region 画面表示項目検索
        /// <summary>
        /// 画面表示項目検索
        /// </summary>
        private void SerchItems()
        {
            //検索開始日を検索条件に設定する型に編集
            DateTime? firstDay = DateTimeUtil.ConvertDateStringToDateTime(FirstDayNullableDateTimePicker.Text);

            //検索終了日を検索条件に設定する型に編集
            DateTime? lastDay = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);

            //日付が設定されていない場合終了
            if ((firstDay == null) || (lastDay == null))
            {
                return;
            }
            
            //選択しているラジオボタンを取得
            int status = this.GetSelectRadioButton();

            //検索パラメータ作成
            InfoListSearchModel itemCond = null;
            if (status == 1 || status == 4)
            {
                //進捗履歴、担当週報

                //パラメータ設定
                if ((this.SelectSectionGroupID != null))
                {
                    itemCond = new InfoListSearchModel
                    {
                        //検索区分
                        CLASS_DATA = status,

                        //検索開始日
                        FIRST_DAY = firstDay,

                        //検索終了日
                        LAST_DAY = lastDay,

                        //作成単位
                        作成単位 = "担当",

                        //部ID
                        DEPARTMENT_ID = SessionDto.DepartmentID,

                        //課ID
                        SECTION_ID = this.SelectSectionID,

                        //担当ID
                        SECTION_GROUP_ID = this.SelectSectionGroupID,
                    };
                }
            }
            else if (status == 2)
            {
                //部週報

                //パラメータ設定
                itemCond = new InfoListSearchModel
                {
                    //検索区分
                    CLASS_DATA = status,

                    //検索開始日
                    FIRST_DAY = firstDay,

                    //検索終了日
                    LAST_DAY = lastDay,

                    //作成単位
                    作成単位 = "部",

                    //部ID
                    DEPARTMENT_ID = SessionDto.DepartmentID,

                    //課ID
                    SECTION_ID = null,

                    //担当ID
                    SECTION_GROUP_ID = null,
                };
            }
            else if (status == 3)
            {
                //課週報

                if (this.SelectSectionID != null)
                {
                    //パラメータ設定
                    itemCond = new InfoListSearchModel
                    {
                        //検索区分
                        CLASS_DATA = status,

                        //検索開始日
                        FIRST_DAY = firstDay,

                        //検索終了日
                        LAST_DAY = lastDay,

                        //作成単位
                        作成単位 = "課",

                        //部ID
                        DEPARTMENT_ID = SessionDto.DepartmentID,

                        //課ID
                        SECTION_ID = this.SelectSectionID,

                        //担当ID
                        SECTION_GROUP_ID = null,
                    };
                }
            }

            //リスト初期化
            InfoListDataGridView.Rows.Clear();
            this.ItemList.Clear();
            
            string[] colNameList;
            if (status == 1)
            {
                #region 進捗履歴の列順
                colNameList = new string[] {
                        this.CheckBox.Name,
                        this.GeneralCode.Name,
                        this.Category.Name,
                        this.Date.Name,
                        this.CurrentSituation.Name,
                        this.FutureSchedule.Name,
                        this.OpenClose.Name,
                        this.SelectKeyword.Name,
                        this.PersonelName.Name,
                        this.InputDatetime.Name
                    };
                #endregion

                InfoListDataGridView.Columns[this.Date.Name].HeaderText = "日付";
                InfoListDataGridView.Columns[this.Category.Name].HeaderText = "項目名";
                InfoListDataGridView.Columns[this.FutureSchedule.Name].HeaderText = "今後の予定";
                InfoListDataGridView.Columns[this.OpenClose.Name].HeaderText = "Open" + Environment.NewLine + "Close";
            }
            else
            {
                #region 上記以外の列順
                colNameList = new string[] {
                        this.CheckBox.Name,
                        this.Date.Name,
                        this.SectionGroupCode.Name,
                        this.GeneralCode.Name,
                        this.Category.Name,
                        this.CurrentSituation.Name,
                        this.FutureSchedule.Name
                    };
                #endregion
                    
                InfoListDataGridView.Columns[this.Date.Name].HeaderText = "発行日";
                InfoListDataGridView.Columns[this.Category.Name].HeaderText = "項目";
                InfoListDataGridView.Columns[this.FutureSchedule.Name].HeaderText = "今後の予定・他";
            }

            foreach (DataGridViewColumn col in InfoListDataGridView.Columns)
            {
                col.Visible = false;
            }
            for (var i = 0; i < colNameList.Count(); i++)
            {
                InfoListDataGridView.Columns[colNameList[i]].DisplayIndex = i;
                InfoListDataGridView.Columns[colNameList[i]].Visible = true;
            }
            
            //検索パラメータが作成されない場合は終了
            if (itemCond == null)
            {
                return;
            }

            //Get実行
            var res = HttpUtil.GetResponse<InfoListSearchModel, InfoListModel>(ControllerType.ReportMaterial, itemCond);
            if (res == null || res.Status != Const.StatusSuccess)
            {
                MessageLabel.Text = Resources.KKM00005;
                return;
            }

            //取得した情報を画面表示
            this.ItemList = (res.Results).ToList();
            if (this.ItemList.Count == 0)
            {
                //検索結果が０件の場合メッセージ表示
                MessageLabel.Text = Resources.KKM00005;
            }
            else
            {
                MessageLabel.Text = "";

                //検索結果を画面表示
                foreach (InfoListModel item in this.ItemList)
                {
                    InfoListDataGridView.Rows.Add();
                    int idx = InfoListDataGridView.Rows.Count - 1;
                    InfoListDataGridView.Rows[idx].Cells["Date"].Value = item.LISTED_DATE;
                    InfoListDataGridView.Rows[idx].Cells["SectionGroupCode"].Value = item.SECTION_GROUP_CODE;
                    InfoListDataGridView.Rows[idx].Cells["GeneralCode"].Value = item.GENERAL_CODE;
                    InfoListDataGridView.Rows[idx].Cells["Category"].Value = item.CATEGORY;
                    InfoListDataGridView.Rows[idx].Cells["CurrentSituation"].Value = item.CURRENT_SITUATION;
                    InfoListDataGridView.Rows[idx].Cells["FutureSchedule"].Value = item.FUTURE_SCHEDULE;

                    InfoListDataGridView.Rows[idx].Cells["OpenClose"].Value = item.OPEN_CLOSE;
                    InfoListDataGridView.Rows[idx].Cells["PersonelName"].Value = item.PERSONEL_NAME;
                    InfoListDataGridView.Rows[idx].Cells["InputDatetime"].Value = item.INPUT_DATETIME;
                    InfoListDataGridView.Rows[idx].Cells["SelectKeyword"].Value = item.SELECT_KEYWORD;
                }

                InfoListDataGridView.CurrentCell = null;

                //チェックボックスクリア
                SetCheckBox(false);
            }

            //フォーカス
            this.ActiveControl = InfoListDataGridView;
        }
        #endregion

        #region チェックボックスステータス設定
        /// <summary>
        /// チェックボックスステータス設定
        /// </summary>
        /// <param name="flag"></param>
        private void SetCheckBox(bool flag)
        {
            //全行にステータスを設定
            foreach (DataGridViewRow row in InfoListDataGridView.Rows)
            {
                row.Cells[0].Value = flag;
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
            //検索実行
            FormControlUtil.FormWait(this, () => SerchItems());
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
            //選択されている項目を親画面参照リストに追加する。
            for (int i = 0; i< InfoListDataGridView.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(InfoListDataGridView.Rows[i].Cells[0].Value);
                if (isChecked)
                {
                    // フォローリスト（進捗履歴）の時だけログイン者の担当を返す
                    if (this.GetSelectRadioButton() == 1)
                    {
                        ItemList[i].SECTION_GROUP_CODE = SessionDto.SectionGroupCode;
                    }
                    this.ReturnList.Add(ItemList[i]);
                }
            }

            if (this.ReturnList.Count == 0)
            {
                //選択項目がない場合メッセージ表示
                Messenger.Info(Resources.KKM00009);
            }
            else
            {
                //項目が選択されている場合画面を閉じる
                this.Close();
            }
        }
        #endregion
        
        #region 閉じるボタンクリック
        /// <summary>
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            //画面を閉じる
            this.Close();
        }
        #endregion

        #region 進捗履歴ラジオボタン選択
        /// <summary>
        /// 進捗履歴ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FollowListRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //初期表示では実行されない

            //非選択の場合、以下の処理を行わない
            if (FollowListRadioButton.Checked == false)
            { return; }

            //リスト作成
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;
            SelectSectionID = SessionDto.SectionID;
            SelectSectionGroupID = SessionDto.SectionGroupID;
            FormControlUtil.FormWait(this, () => SerchItems());
        }
        #endregion

        #region 部履歴ラジオボタン選択
        /// <summary>
        /// 部週報ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //初期表示では実行されない

            //非選択の場合、以下の処理を行わない
            if (DepartmentRadioButton.Checked == false)
            { return; }

            //リスト作成
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode);
            AffiliationComboBox.SelectedIndex = 0;
            SelectSectionID = null;
            SelectSectionGroupID = null;
            FormControlUtil.FormWait(this, () => SerchItems());
        }
        #endregion

        #region 課履歴ラジオボタン選択
        /// <summary>
        /// 課週報ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //初期表示では実行されない

            //非選択の場合、以下の処理を行わない
            if (SectionRadioButton.Checked == false)
            { return; }

            //リスト作成
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.SectionCode);
            AffiliationComboBox.SelectedIndex = 0;
            SelectSectionID = SessionDto.SectionID;
            SelectSectionGroupID = null;
            FormControlUtil.FormWait(this, () => SerchItems());
        }
        #endregion

        #region 担当週報ラジオボタン選択
        /// <summary>
        /// 担当週報ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //初期表示では実行されない

            //非選択の場合、以下の処理を行わない
            if (SectionGroupRadioButton.Checked == false)
            { return; }

            //リスト作成
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;
            SelectSectionID = SessionDto.SectionID;
            SelectSectionGroupID = SessionDto.SectionGroupID;
            FormControlUtil.FormWait(this, () => SerchItems());
        }
        #endregion

        #region 全選択チェックボックス描画
        /// <summary>
        /// 全選択チェックボックス描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoListDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.ColumnIndex == 0) && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - AllCheckBox.Width) / 2, (bmp.Height - AllCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds, e.PaintParts);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region 全選択チェックボックス選択
        /// <summary>
        /// 全選択チェックボックス選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //チェックボックスの表示を更新する
            if ((e.ColumnIndex == 0) && (e.RowIndex == -1))
            {
                AllCheckBox.Checked = !AllCheckBox.Checked;
            }
        }
        #endregion

        #region 全選択チェックボックス操作
        /// <summary>
        /// 全選択チェックボックス操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //表示されている項目のチェックボックスすべて表示を更新する
            DataGridViewCell dg = InfoListDataGridView.CurrentCell;
            InfoListDataGridView.CurrentCell = null;

            foreach (DataGridViewRow row in InfoListDataGridView.Rows)
            {
                row.Cells[0].Value = AllCheckBox.Checked;
            }
            InfoListDataGridView.CurrentCell = dg;
        }
        #endregion

        #region 画面表示完了イベント
        /// <summary>
        /// 画面表示完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoListForm_Shown(object sender, EventArgs e)
        {
            InfoListDataGridView.CurrentCell = null;
        }
        #endregion

        #region 部署プルダウンクリック
        /// <summary>
        /// 部署プルダウンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_Click(object sender, EventArgs e)
        {
            if (this.DepartmentRadioButton.Checked)
            {
                // 部

                //コンボボックスの表示を更新
                AffiliationComboBox.Items.Clear();
                AffiliationComboBox.Items.Add(SessionDto.DepartmentCode);
                AffiliationComboBox.SelectedIndex = 0;
                this.SelectSectionID = null;
                this.SelectSectionGroupID = null;

                //検索実行
                FormControlUtil.FormWait(this, () => SerchItems());
            }
            else if (this.SectionRadioButton.Checked)
            {
                // 課
                using (var form = new SectionListForm { DEPARTMENT_ID = SessionDto.DepartmentID, DEPARTMENT_COMBOBOX = false })
                {
                    // 課検索
                    if (form.ShowDialog().Equals(DialogResult.OK))
                    {
                        //コンボボックスの表示を更新
                        AffiliationComboBox.Items.Clear();
                        AffiliationComboBox.Items.Add(form.SECTION_CODE);
                        AffiliationComboBox.SelectedIndex = 0;
                        this.SelectSectionID = form.SECTION_ID;
                        this.SelectSectionGroupID = null;

                        //検索実行
                        FormControlUtil.FormWait(this, () => SerchItems());
                    }

                }

            }
            else if (SectionGroupRadioButton.Checked || FollowListRadioButton.Checked)
            {
                //担当,進捗履歴
                using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID, DEPARTMENT_COMBOBOX = false })
                {
                    // 担当検索
                    if (form.ShowDialog().Equals(DialogResult.OK))
                    {
                        //コンボボックスの表示を更新
                        AffiliationComboBox.Items.Clear();
                        AffiliationComboBox.Items.Add(form.SECTION_GROUP_CODE);
                        AffiliationComboBox.SelectedIndex = 0;
                        this.SelectSectionID = form.SECTION_ID;
                        this.SelectSectionGroupID = form.SECTION_GROUP_ID;

                        //検索実行
                        FormControlUtil.FormWait(this, () => SerchItems());
                    }
                }
            }

        }
        #endregion
        
        /// <summary>
        /// 期間開始日日付ドロップダウンクローズ時イベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            CheckDateTime(FirstDayNullableDateTimePicker);
        }

        /// <summary>
        /// 期間終了日日付ドロップダウンクローズ時イベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            CheckDateTime(LastDayNullableDateTimePicker);
        }

        /// <summary>
        /// 期間開始日検証終了時イベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            CheckDateTime(FirstDayNullableDateTimePicker);
        }

        /// <summary>
        /// 期間終了日検証終了時イベント。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            CheckDateTime(LastDayNullableDateTimePicker);
        }

        /// <summary>
        /// 期間開始終了日検証処理。
        /// </summary>
        /// <param name="focusSetPickerControl"></param>
        private void CheckDateTime(UC.NullableDateTimePicker focusSetPickerControl)
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();
            map[focusSetPickerControl] = (c, name) =>
            {
                var errMsg = "";

                if (this.FirstDayNullableDateTimePicker.Value != null && this.LastDayNullableDateTimePicker.Value != null)
                {
                    if (this.FirstDayNullableDateTimePicker.SelectedDate.Value > this.LastDayNullableDateTimePicker.SelectedDate.Value)
                    {
                        errMsg = Resources.KKM00018;
                        this.FirstDayNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                        this.LastDayNullableDateTimePicker.BackColor = Const.ErrorBackColor;
                    }
                }
                return errMsg;
            };
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                Messenger.Warn(msg);
            }
            
            DateTime? firstDay = DateTimeUtil.ConvertDateStringToDateTime(FirstDayNullableDateTimePicker.Text);
            DateTime? lastDay = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);

            if (lastDay < firstDay)
            {
                if (focusSetPickerControl == LastDayNullableDateTimePicker)
                {
                    LastDayNullableDateTimePicker.Text = FirstDayNullableDateTimePicker.Text;
                }
                else
                {
                    FirstDayNullableDateTimePicker.Text = LastDayNullableDateTimePicker.Text;
                }
            }
        }
    }
}