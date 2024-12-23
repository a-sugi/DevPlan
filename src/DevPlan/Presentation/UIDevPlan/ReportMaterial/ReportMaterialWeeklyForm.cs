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
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.ReportMaterial
{
    public partial class ReportMaterialWeeklyForm : BaseForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "情報元一覧（週報）"; } }

        /// <summary>
        /// 部ID
        /// </summary>
        public string DEPARTMENT_ID { get { return DepartmentID; } set { DepartmentID = value; } }

        /// <summary>
        /// 課ID
        /// </summary>
        public string SECTION_ID { get { return SectionID; } set { SectionID = value; } }

        /// <summary>
        /// 期間（開始日）
        /// </summary>
        public DateTime FIRSTDAY { get { return FirstDay; } set { FirstDay = value; } }

        /// <summary>
        /// 期間（終了日）
        /// </summary>
        public DateTime LASTDAY { get { return LastDay; } set { LastDay = value; } }

        /// <summary>
        /// 戻り値
        /// </summary>
        public List<InfoListModel> RETURN_LIST { get { return ReturnList; } }
        #endregion

        #region 内部変数

        /// <summary>
        /// 部ID
        /// </summary>
        public string DepartmentID;

        /// <summary>
        /// 課ID
        /// </summary>
        public string SectionID;

        /// <summary>
        /// 期間（開始日）
        /// </summary>
        public DateTime FirstDay;

        /// <summary>
        /// 期間（終了日）
        /// </summary>
        public DateTime LastDay;

        /// <summary>
        /// 戻り値
        /// </summary>
        public List<InfoListModel> ReturnList = new List<InfoListModel>();

        /// <summary>
        /// 検索結果リスト
        /// </summary>
        private List<InfoListModel> ItemList = new List<InfoListModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReportMaterialWeeklyForm()
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
        private void InfoListWeeklyForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.Init();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //期間を表示
            FirstDayNullableDateTimePicker.Text = this.FirstDay.ToString("yyyy/MM/dd");
            LastDayNullableDateTimePicker.Text = this.LastDay.ToString("yyyy/MM/dd");

            //イベント設定（_ValueChangedイベントから移動）及び入力チェック用にtagを設定。
            this.LastDayNullableDateTimePicker.Validated += LastDayNullableDateTimePicker_Validated;
            this.FirstDayNullableDateTimePicker.Validated += FirstDayNullableDateTimePicker_Validated;
            this.LastDayNullableDateTimePicker.Tag = "ItemName(終了日)";
            this.FirstDayNullableDateTimePicker.Tag = "ItemName(開始日)";
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
            if((firstDay == null) || (lastDay == null))
            {
                return;
            }

            //パラメータ設定
            var itemCond = new InfoListSearchModel
            {
                //検索区分(担当)
                CLASS_DATA = 6,

                //検索開始日
                FIRST_DAY = firstDay,

                //検索終了日
                LAST_DAY = lastDay,

                //作成単位
                作成単位 = "担当",

                //部ID
                DEPARTMENT_ID = this.DepartmentID,

                //課ID
                SECTION_ID = this.SectionID,

                //担当ID
                SECTION_GROUP_ID = null,
            };

            //リスト初期化
            InfoListDataGridView.Rows.Clear();
            this.ItemList.Clear();

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
                }

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
            SerchItems();
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
                if(Convert.ToBoolean(InfoListDataGridView.Rows[i].Cells[0].Value.ToString()))
                {
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
            if ((e.ColumnIndex == 0) && (e.RowIndex == -1))
            {
                //全チェックボックスの表示を更新
                AllCheckBox.Checked = !AllCheckBox.Checked;
            }
            else if (e.ColumnIndex == 0)
            {
                //選択されたチェックボックスの状態を更新
                bool value = !((bool)InfoListDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                InfoListDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;

                //全チェックボックスの状態が同じか判定
                bool flg = true;
                foreach (DataGridViewRow row in InfoListDataGridView.Rows)
                {
                    if ((bool)row.Cells[0].Value != value)
                    {
                        flg = false;
                        break;
                    }
                }

                if(flg)
                {
                    //全チェックボックスの状態が同じ場合、全選択用チェックボックスの状態も更新
                    AllCheckBox.Checked = value;
                    InfoListDataGridView.Refresh();
                }
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

        #region 期間開始変更(カレンダー入力)
        /// <summary>
        /// 期間開始変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            //期間範囲チェック
            CheckDateRange(FirstDayNullableDateTimePicker);
        }
        #endregion

        #region 期間終了変更(カレンダー入力)
        /// <summary>
        /// 期間終了変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            //期間範囲チェック
            CheckDateRange(LastDayNullableDateTimePicker);
        }
        #endregion

        #region 期間開始変更(キーボード入力)
        /// <summary>
        /// 期間開始変更(キーボード入力)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDayNullableDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // ここに範囲チェックがあるとCloseUpとあわせて２回処理が行われることになるのでコメントアウト。Validatedへ移動。
            ////期間範囲チェック
            //CheckDateRange();
        }

        /// <summary>
        /// 期間開始変更(キーボード入力)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            CheckDateRange(FirstDayNullableDateTimePicker);
        }
        #endregion

        #region 期間終了変更(キーボード入力)
        /// <summary>
        /// 期間終了変更(キーボード入力)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // ここに範囲チェックがあるとCloseUpとあわせて２回処理が行われることになるのでコメントアウト。Validatedへ移動。
            ////期間範囲チェック
            //CheckDateRange(true);
        }

        /// <summary>
        /// 期間終了変更(キーボード入力)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            CheckDateRange(LastDayNullableDateTimePicker);
        }
        #endregion

        #region 期間範囲チェック
        /// <summary>
        /// 期間範囲チェック
        /// </summary>
        /// <param name="focusSetPickerControl">チェック後のフォーカスセット対象DateTimePickerコントロール</param>
        private void CheckDateRange(UC.NullableDateTimePicker focusSetPickerControl)
        {
            //期間大小チェック
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

            //検索開始日
            DateTime? firstDay = DateTimeUtil.ConvertDateStringToDateTime(FirstDayNullableDateTimePicker.Text);

            //検索終了日
            DateTime? lastDay = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);

            if(lastDay < firstDay)
            {
                if(focusSetPickerControl == LastDayNullableDateTimePicker)
                {
                    //検索開始日に合わせる
                    LastDayNullableDateTimePicker.Text = FirstDayNullableDateTimePicker.Text;
                }
                else
                {
                    //検索終了日に合わせる
                    FirstDayNullableDateTimePicker.Text = LastDayNullableDateTimePicker.Text;
                }
            }
        }
        #endregion

        #region 画面表示完了イベント
        /// <summary>
        /// 画面表示完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoListWeeklyForm_Shown(object sender, EventArgs e)
        {
            InfoListDataGridView.CurrentCell = null;
        }
        #endregion
    }
}