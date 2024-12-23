using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
//Append Start 2021/10/12 矢作
using DevPlan.Presentation.Common;
//Append End 2021/10/12 矢作

namespace DevPlan.Presentation.UITestCar.Othe.ControlSheet
{
    /// <summary>
    /// 管理票検索
    /// </summary>
    public partial class ControlSheetSearchForm : BaseSubForm
    {
        #region メンバ変数
        private BindingSource bindingSource = new BindingSource();

        private List<ControlSheetSearchConditionModel> controlSheetSearchConditionList = null;

        private Dictionary<string, List<CommonMasterModel>> masterMap = new Dictionary<string, List<CommonMasterModel>>();

        private bool isBind = false;

        private readonly string[] Range = { "のみ", "～", "～ ∞", "まで" };

        private readonly string[] From = { "", "(", "((", "(((", "((((" };
        private readonly string[] To = { "", ")", "))", ")))", "))))" };

        private readonly string[] Conjunction = { "", "かつ", "または" };

        private const string DefaultName = "**********";

        private const string IsOnly = "のみ";
        private const string IsRange = "～";

        private const string NumberRegex = "^[0-9]{1,8}$";

        private const string JyuryouBusyo = "受領部署";
        private const string KanriSekininBusyo = "管理責任部署";

        private const int BusyoStart = 8;

        private const string Start = "(";
        private const string End = ")";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "管理票検索"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>カスタムテンプレート</summary>
        public CustomTemplate CustomTemplate { get; set; }

        /// <summary>選択検索条件</summary>
        public ControlSheetSearchConditionModel SelectedUserSearchCondition { get; set; }

        /// <summary>検索条件</summary>
        public ControlSheetModel SearchCondition { get; set; }

        /// <summary>他ユーザー検索条件表示可否</summary>
        public bool IsAllUser
        {
            get { return this.AllUserCheckBox.Checked; }
            set { this.AllUserCheckBox.Checked = value; }

        }

        /// <summary>処理結果無可否</summary>
        public bool IsNoneResult { get; set; }

        //Append Start 2021/10/12 矢作
        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
        //Append End 2021/10/12 矢作
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControlSheetSearchForm()
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
        private void ControlSheetSearchForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, (Action)(() =>
            {
                //バインド可否
                this.isBind = true;
                
                //Append Start 2021/10/12 矢作
                // 権限
                this.UserAuthority = base.GetFunction(FunctionID.TestCarManagement);
                //Append End 2021/10/12 矢作

                //画面初期化
                this.InitForm();

                //グリッドビュー初期化
                this.InitGridView();

                //検索条件設定
                this.SetSearchConditionForm();

                //バインド可否
                this.isBind = false;
                
            }));

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //検索条件
            this.ActiveControl = this.SearchTabControl;

            //検索条件の設定
            this.SetUserSearchCondition();

            //検索条件が選択されているかどうか
            if (this.SelectedUserSearchCondition != null)
            {
                foreach (ControlSheetSearchConditionModel cond in this.SearchConditionComboBox.Items)
                {
                    //選択状態に設定
                    if (cond.ユーザーID == this.SelectedUserSearchCondition.ユーザーID && cond.条件名 == this.SelectedUserSearchCondition.条件名)
                    {
                        this.SearchConditionComboBox.SelectedItem = cond;
                        break;

                    }

                }

            }

            //検索条件
            FormControlUtil.SetComboBoxItem(this.ConditionComboBox, HttpUtil.GetResponse<UserSearchItemModel>(ControllerType.ControlSheetSearchItem).Results);

            //検索条件の範囲
            this.DateRangeComboBox.DataSource = Range;
            this.NumberRangeComboBox.DataSource = Range;

            //Append Start 2021/10/26 矢作
            var isManagementDepartment = SessionDto.ManagementDepartmentType.Any(x => x == Const.Kenjitu || x == Const.Kanri);
            this.ParkingSelectButton.Visible = this.UserAuthority.UPDATE_FLG == '1';
            //this.ParkingSelectButton.Visible = true;
            // 試験車管理部署権限なし
            if (!isManagementDepartment)
            {
                // 編集トグルボタン
                this.ParkingSelectButton.Visible = isManagementDepartment;
            }
            //Append End 2021/10/26 矢作

        }

        /// <summary>
        /// グリッド初期化
        /// </summary>
        private void InitGridView()
        {
            //データグリッドビュー初期化
            this.ConditionListDataGridView.AutoGenerateColumns = false;
            this.ConditionListDataGridView.DataSource = bindingSource;

            //ドロップダウンの設定
            this.FromColumn.DataSource = From;
            this.ConjunctionColumn.DataSource = Conjunction;
            this.ToColumn.DataSource = To;

        }

        /// <summary>
        /// 検索条件設定
        /// </summary>
        private void SetSearchConditionForm()
        {
            var cond = this.SearchCondition;

            //検索条件が無いかユーザー検索条件があるかどうか
            if (cond == null || cond.ControlSheetSearchConditionList == null || cond.ControlSheetSearchConditionList.Any() == false)
            {
                //簡易検索選択
                this.SearchTabControl.SelectedTab = this.SimpleTabPage;

                //固定資産NO
                this.FixedAssetTextBox.Text = cond?.ControlSheetSearch?.固定資産NO;

                //管理票NO
                this.ManagementNoTextBox.Text = cond?.ControlSheetSearch?.管理票NO;

                //車体番号
                this.CarBodyNoTextBox.Text = cond?.ControlSheetSearch?.車体番号;

                //号車
                this.CarTextBox.Text = cond?.ControlSheetSearch?.号車;

                //登録ナンバー
                this.EntryNoTextBox.Text = cond?.ControlSheetSearch?.登録ナンバー;

                //駐車場番号
                this.ParkingNoTextBox.Text = cond?.ControlSheetSearch?.駐車場番号;

                //Append Start 2021/07/21 矢作
                //改修前車両の表示・非表示
                if (cond != null)
                {
                    this.HistoryCheckBox.Checked = cond.ControlSheetSearch.改修前車両検索対象;
                }
                //Append End 2021/07/21 矢作

            }
            else
            {
                //詳細検索選択
                this.SearchTabControl.SelectedTab = this.DetailTabPage;

                //ユーザー検索条件
                this.bindingSource.DataSource = cond.ControlSheetSearchConditionList.ToList();

            }

        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSheetSearchForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ConditionListDataGridView.CurrentCell = null;

            //処理結果無可否
            if (this.IsNoneResult == true)
            {
                Messenger.Warn(Resources.KKM00005);

            }

        }
        #endregion

        #region 検索条件選択
        /// <summary>
        /// 検索条件選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchConditionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            //検索条件が先頭行かどうか
            if (this.SearchConditionComboBox.SelectedIndex == 0)
            {
                this.bindingSource.DataSource = null;
                return;

            }

            var cond = this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel;

            //設定の保存
            var flg = cond.ユーザーID == SessionDto.UserId;
            this.EntryButton.Enabled = flg;
            this.DeleteButton.Enabled = flg;

            //検索条件
            this.SetConditionList(this.controlSheetSearchConditionList.Where(x => x.ユーザーID == cond.ユーザーID && x.条件名 == cond.条件名 && x.行番号 >= 0));

        }
        #endregion

        #region 他のユーザーの条件も表示選択
        /// <summary>
        /// 他のユーザーの条件も表示選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllUserCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //バインド可否
                this.isBind = true;

                //検索条件の設定
                this.SetUserSearchCondition();

                //他のユーザーの条件を表示しない場合は設定の保存を押下可に設定
                if (this.AllUserCheckBox.Checked == false)
                {
                    //条件の保存
                    this.EntryButton.Enabled = true;

                }

                //バインド可否
                this.isBind = false;

            });

        }
        #endregion

        #region 条件の保存ボタンクリック
        /// <summary>
        /// 条件の保存ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            //検索条件がOKかどうか
            if (this.IsSearch() == false)
            {
                return;

            }

            //検索条件があれば設定情報を追加
            var list = this.GetConditionList();
            if (this.controlSheetSearchConditionList != null && controlSheetSearchConditionList.Any() == true)
            {
                var cond = this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel;
                list.AddRange(this.controlSheetSearchConditionList.Where(x => x.ユーザーID == cond.ユーザーID && x.条件名 == cond.条件名 && x.行番号 < 0));

            }

            using (var form = new ControlSheetSaveForm { CustumTemplate = this.CustomTemplate, SearchConditionList = list })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        //バインド可否
                        this.isBind = true;

                        //選択検索条件
                        this.SelectedUserSearchCondition = form.SelectedUserSearchCondition;

                        //画面初期化
                        this.InitForm();

                        //バインド可否
                        this.isBind = false;

                    });

                }

            }

        }
        #endregion

        #region 削除ボタンクリック
        /// <summary>
        /// 削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //検索条件を選択しているかどうか
            if (this.SearchConditionComboBox.SelectedIndex == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            FormControlUtil.FormWait(this, () =>
            {
                //削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    var cond = this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel;

                    //ユーザー検索条件が削除できたかどうか
                    if (this.DeleteUserSearchCondition(cond) == true)
                    {
                        //選択済の条件を削除の場合は初期化
                        if (this.SelectedUserSearchCondition?.ユーザーID == cond.ユーザーID && this.SelectedUserSearchCondition?.条件名 == cond.条件名)
                        {
                            this.SelectedUserSearchCondition = null;

                        }

                        //ユーザー検索条件の設定
                        this.SetUserSearchCondition();

                        Messenger.Info(Resources.KKM00003);

                    }

                }

            });

        }
        #endregion

        #region 条件選択
        /// <summary>
        /// 条件選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //検索条件初期化
            this.InitCondition();

        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void InitCondition()
        {
            Action<ComboBox> init = cmb => cmb.SelectedIndex = cmb.Items.Count == 0 ? -1 : 0;

            var today = DateTime.Today;

            //検索条件は非表示
            this.StringPanel.Visible = false;
            this.DatePanel.Visible = false;
            this.NumberPanel.Visible = false;
            this.MasterPanel.Visible = false;

            //値の初期化(文字列)
            this.StringTextBox.Text = "";
            this.StringTextBox.BackColor = Const.DefaultBackColor;

            //値の初期化(日付)
            this.FromDateTimePicker.Value = today;
            this.FromDateTimePicker.CustomFormat = Const.FormatDate;
            this.FromDateTimePicker.BackColor = Const.DefaultBackColor;
            init(this.DateRangeComboBox);
            this.ToDateTimePicker.Value = today;
            this.ToDateTimePicker.CustomFormat = Const.FormatDate;
            this.ToDateTimePicker.Visible = false;
            this.ToDateTimePicker.BackColor = Const.DefaultBackColor;

            //値の初期化(数値)
            this.NumberFromTextBox.Text = "";
            this.NumberFromTextBox.BackColor = Const.DefaultBackColor;
            init(this.NumberRangeComboBox);
            this.NumberToTextBox.Text = "";
            this.NumberToTextBox.Visible = false;
            this.NumberToTextBox.BackColor = Const.DefaultBackColor;

            //値の初期化(マスター)
            init(this.MasterComboBox);
            FormControlUtil.SetComboBoxBackColor(this.MasterComboBox, Const.DefaultBackColor);

            //存在しない
            this.NoneCheckBox.Checked = false;

            //否定
            this.NotCheckBox.Checked = false;

            //適用
            this.SetButton.Enabled = this.ConditionComboBox.SelectedIndex > 0;

            //条件未選択なら終了
            if (this.ConditionComboBox.SelectedIndex == 0)
            {
                return;

            }

            var item = this.ConditionComboBox.SelectedItem as UserSearchItemModel;

            //検索種別ごとの分岐
            switch (item.SearchDataType)
            {
                //文字列
                case SearchDataType.String:
                    this.StringPanel.Visible = true;
                    break;

                //日付
                case SearchDataType.Date:
                    this.DatePanel.Visible = true;

                    //年月の場合は書式変更
                    if (item.DateType == DateType.Month)
                    {
                        this.FromDateTimePicker.CustomFormat = Const.FormatMonth;
                        this.ToDateTimePicker.CustomFormat = Const.FormatMonth;

                    }
                    break;

                //数値
                case SearchDataType.Number:
                    this.NumberPanel.Visible = true;
                    break;

                //マスター
                case SearchDataType.Master:
                    this.MasterPanel.Visible = true;

                    //マスターの初期化
                    this.InitMaster(item);
                    break;

            }

        }

        /// <summary>
        /// マスター初期化
        /// </summary>
        /// <param name="item">ユーザー検索項目</param>
        private void InitMaster(UserSearchItemModel item)
        {
            //取得済みのマスターかどうか
            if (this.masterMap.ContainsKey(item.ColumnName) == false)
            {
                this.masterMap[item.ColumnName] = this.GetUserSearchMaster(item);

            }

            //マスター
            FormControlUtil.SetComboBoxItem(this.MasterComboBox, this.masterMap[item.ColumnName], false);

        }
        #endregion

        #region 範囲選択
        /// <summary>
        /// 範囲選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RangeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var cmb = sender as ComboBox;

            var flg = cmb.Text == IsRange;

            //日付の範囲のコンボボックスかどうか
            if (cmb == this.DateRangeComboBox)
            {
                this.ToDateTimePicker.Visible = flg;

            }
            //数値の範囲のコンボボックスかどうか
            else if (cmb == this.NumberRangeComboBox)
            {
                this.NumberToTextBox.Visible = flg;

            }

        }
        #endregion

        #region 存在しない選択
        /// <summary>
        /// 存在しない選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var flg = !this.NoneCheckBox.Checked;

            //検索条件の項目の有効可否設定
            this.StringTextBox.Enabled = flg;
            this.FromDateTimePicker.Enabled = flg;
            this.DateRangeComboBox.Enabled = flg;
            this.ToDateTimePicker.Enabled = flg;
            this.NumberFromTextBox.Enabled = flg;
            this.NumberRangeComboBox.Enabled = flg;
            this.NumberToTextBox.Enabled = flg;
            this.MasterComboBox.Enabled = flg;

        }
        #endregion

        #region 適用ボタンクリック
        /// <summary>
        /// 適用ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetButton_Click(object sender, EventArgs e)
        {
            var item = this.ConditionComboBox.SelectedItem as UserSearchItemModel;

            //検索条件の設定可否
            if (this.IsSetCondition(item) == true)
            {
                //検索条件の設定
                this.SetCondition(item);

            }

        }

        /// <summary>
        /// 検索条件の設定可否
        /// </summary>
        /// <param name="item">検索項目</param>
        /// <returns></returns>
        private bool IsSetCondition(UserSearchItemModel item)
        {
            var list = new List<string>();

            Func<Control, bool> required = c =>
            {
                var flg = !string.IsNullOrWhiteSpace(c.Text);

                c.BackColor = Const.DefaultBackColor;

                if (flg == false)
                {
                    var msg = string.Format(Resources.KKM00001, "条件");

                    if (list.Contains(msg) == false)
                    {
                        list.Add(msg);

                    }

                    c.BackColor = Const.ErrorBackColor;

                }

                return flg;

            };

            Func<Control, bool> number = c =>
            {
                var flg = required(c);

                if (flg == true)
                {
                    flg = Regex.IsMatch(c.Text, NumberRegex);

                    if (flg == false)
                    {
                        var msg = string.Format(Resources.KKM00025, "条件");

                        if (list.Contains(msg) == false)
                        {
                            list.Add(msg);

                        }

                        c.BackColor = Const.ErrorBackColor;

                    }

                }

                return flg;

            };

            //存在しないが選択されているかどうか
            if (this.NoneCheckBox.Checked == false)
            {
                //検索種別ごとの分岐
                switch (item.SearchDataType)
                {
                    //文字列
                    case SearchDataType.String:
                        required(this.StringTextBox);
                        break;

                    //日付
                    case SearchDataType.Date:
                        required(this.FromDateTimePicker);

                        //範囲指定かどうか
                        if (this.DateRangeComboBox.Text == IsRange)
                        {
                            required(this.ToDateTimePicker);

                        }
                        break;

                    //数値
                    case SearchDataType.Number:
                        number(this.NumberFromTextBox);

                        //範囲指定かどうか
                        if (this.NumberRangeComboBox.Text == IsRange)
                        {
                            number(this.NumberToTextBox);

                        }
                        break;

                    //マスター
                    case SearchDataType.Master:
                        required(this.MasterComboBox);
                        break;

                }

            }

            //エラーがあるかどうか
            var err = list.Any();
            if (err == true)
            {
                Messenger.Warn(string.Join(Const.CrLf, list.ToArray()));

            }

            return !err;

        }

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        /// <param name="item">検索項目</param>
        private void SetCondition(UserSearchItemModel item)
        {
            var list = this.GetConditionList();

            ControlSheetSearchConditionModel cond = null;

            Func<CheckBox, string> chk = c => c.Checked.ToString();

            Func<DateTime?, DateTime?> month = d => d == null ? null : (DateTime?)d.Value.AddDays((d.Value.Day - 1) * -1);

            Action init = () =>
            {
                cond.STR = null;
                cond.FROMNUM = null;
                cond.NUMMODE = null;
                cond.TONUM = null;
                cond.FROMDATE = null;
                cond.DATEMODE = null;
                cond.TODATE = null;
                cond.INDEX_NO = null;

            };

            //バインド可否
            this.isBind = true;

            //データが無いか選択している行がないかどうか
            if (this.ConditionListDataGridView.RowCount == 0 || this.ConditionListDataGridView.SelectedCells.Count == 0)
            {
                cond = new ControlSheetSearchConditionModel
                {
                    //ユーザーID
                    ユーザーID = SessionDto.UserId,

                    //条件名
                    条件名 = this.SearchConditionComboBox.Text,

                    //行番号
                    行番号 = (short)(this.ConditionListDataGridView.RowCount == 0 ? 0 : list.Count),


                };

                list.Add(cond);

            }
            else
            {
                cond = list.ElementAt(this.ConditionListDataGridView.SelectedCells[0].RowIndex);

            }

            //初期化
            init();

            var sb = new StringBuilder();
            sb.AppendFormat("{0} が ", item.ColumnName);

            //存在しないが選択されているかどうか
            if (this.NoneCheckBox.Checked == true)
            {
                sb.AppendFormat("存在{0}", this.NotCheckBox.Checked == true ? "する" : "しない");

            }
            else
            {
                //検索種別ごとの分岐
                switch (item.SearchDataType)
                {
                    //文字列
                    case SearchDataType.String:
                        //文字列の条件
                        cond.STR = this.StringTextBox.Text.Trim();

                        //文言
                        sb.AppendFormat("{0} ", cond.STR);
                        break;

                    //日付
                    case SearchDataType.Date:
                        //日付FROM
                        cond.FROMDATE = this.FromDateTimePicker.SelectedDate;

                        //日付範囲条件
                        cond.DATEMODE = (short)this.DateRangeComboBox.SelectedIndex;

                        //文言
                        sb.AppendFormat("{0} ", this.FromDateTimePicker.Text);

                        //のみを選択しているかどうか
                        if (this.DateRangeComboBox.Text != IsOnly)
                        {
                            sb.AppendFormat("{0} ", this.DateRangeComboBox.Text);

                        }

                        //範囲指定を選択しているかどうか
                        if (this.DateRangeComboBox.Text == IsRange)
                        {
                            //日付TO
                            cond.TODATE = this.ToDateTimePicker.SelectedDate;

                            //文言
                            sb.AppendFormat("{0} ", this.ToDateTimePicker.Text);

                        }

                        //日付種別が年月かどうか
                        if (item.DateType == DateType.Month)
                        {
                            cond.FROMDATE = month(cond.FROMDATE);
                            cond.TODATE = month(cond.TODATE);

                        }
                        break;

                    //数値
                    case SearchDataType.Number:
                        //数値FROM
                        cond.FROMNUM = int.Parse(this.NumberFromTextBox.Text);

                        //数値範囲条件
                        cond.NUMMODE = (short)this.NumberRangeComboBox.SelectedIndex;

                        //文言
                        sb.AppendFormat("{0} ", cond.FROMNUM);

                        //のみを選択しているかどうか
                        if (this.NumberRangeComboBox.Text != IsOnly)
                        {
                            sb.AppendFormat("{0} ", this.NumberRangeComboBox.Text);

                        }

                        //範囲指定を選択しているかどうか
                        if (this.NumberRangeComboBox.Text == IsRange)
                        {
                            //数値TO
                            cond.TONUM = int.Parse(this.NumberToTextBox.Text);

                            //文言
                            sb.AppendFormat("{0} ", cond.TONUM);

                        }
                        break;

                    //マスター
                    case SearchDataType.Master:
                        var value = this.MasterComboBox.Text;

                        //受領部署か管理責任部署かどうか
                        if (item.ColumnName == JyuryouBusyo || item.ColumnName == KanriSekininBusyo)
                        {
                            //選択しているかどうか
                            if (this.MasterComboBox.SelectedIndex != -1)
                            {
                                value = value.Substring(BusyoStart);

                            }

                        }

                        //文字列の条件
                        cond.STR = value.Trim();

                        //マスターインデックス
                        cond.INDEX_NO = this.MasterComboBox.SelectedIndex;

                        //文言
                        sb.AppendFormat("{0} ", cond.STR);
                        break;

                }

                sb.AppendFormat("で{0}", this.NotCheckBox.Checked == true ? "ない" : "ある");

            }

            //表示条件
            cond.TEXT = sb.ToString();

            //インデックス
            cond.ELEM = (short)(this.ConditionComboBox.SelectedIndex - 1);

            //否定可否
            cond.NOTFLAG = chk(this.NotCheckBox);

            //NULL可否
            cond.NULLFLAG = chk(this.NoneCheckBox);

            //検索条件一覧設定
            var index = this.GetScrollingRowIndex(cond.行番号);
            this.SetConditionList(list);

            //設定した行を選択状態に設定
            this.ConditionListDataGridView.FirstDisplayedScrollingRowIndex = index;
            this.ConditionListDataGridView.Rows[cond.行番号].Selected = true;

            //バインド可否
            this.isBind = false;

        }
        #endregion

        #region 検索条件一覧
        /// <summary>
        /// 検索条件一覧セルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //バインド中なら終了
            if (this.isBind == true)
            {
                return;

            }

            //無効な行か列の場合は終了
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;

            }

            //一覧が無い場合は終了
            var list = this.GetConditionList();
            if (list == null || list.Any() == false)
            {
                return;

            }

            var cond = list[e.RowIndex];

            //インデックスが無いなら終了
            if (cond.ELEM == null)
            {
                return;

            }

            //背景色の初期化(文字列)
            this.StringTextBox.BackColor = Const.DefaultBackColor;

            //背景色の初期化(日付)
            this.FromDateTimePicker.BackColor = Const.DefaultBackColor;
            this.ToDateTimePicker.BackColor = Const.DefaultBackColor;

            //背景色の初期化(数値)
            this.NumberFromTextBox.BackColor = Const.DefaultBackColor;
            this.NumberToTextBox.BackColor = Const.DefaultBackColor;

            //背景色の初期化(マスター)
            FormControlUtil.SetComboBoxBackColor(this.MasterComboBox, Const.DefaultBackColor);

            //条件
            this.ConditionComboBox.SelectedIndex = string.IsNullOrWhiteSpace(cond.TEXT) == true ? 0 : (cond.ELEM ?? 0) + 1;

            var item = this.ConditionComboBox.SelectedItem as UserSearchItemModel;

            Func<int?, string> number = value => value == null ? "" : value.ToString();
            Func<string, bool> chk = value => string.IsNullOrWhiteSpace(value) == true ? false : Convert.ToBoolean(value);

            //検索種別ごとの分岐
            switch (item.SearchDataType)
            {
                //文字列
                case SearchDataType.String:
                    //条件
                    this.StringTextBox.Text = cond.STR;
                    break;

                //日付
                case SearchDataType.Date:
                    //条件
                    this.FromDateTimePicker.Value = cond.FROMDATE;
                    this.DateRangeComboBox.SelectedIndex = cond.DATEMODE ?? 0;

                    //範囲指定を選択しているかどうか
                    if (this.DateRangeComboBox.Text == IsRange)
                    {
                        //文言
                        this.ToDateTimePicker.Value = cond.TODATE;

                    }
                    break;

                //数値
                case SearchDataType.Number:
                    //条件
                    this.NumberFromTextBox.Text = number(cond.FROMNUM);
                    this.NumberRangeComboBox.SelectedIndex = cond.NUMMODE ?? 0;

                    //範囲指定を選択しているかどうか
                    if (this.NumberRangeComboBox.Text == IsRange)
                    {
                        //数値TO
                        this.NumberToTextBox.Text = number(cond.TONUM);

                    }
                    break;

                //マスター
                case SearchDataType.Master:
                    //条件
                    this.MasterComboBox.SelectedIndex = -1;
                    this.MasterComboBox.Text = cond.STR;
                    break;

            }

            //否定
            this.NotCheckBox.Checked = chk(cond.NOTFLAG);

            //存在しない
            this.NoneCheckBox.Checked = chk(cond.NULLFLAG);

        }

        /// <summary>
        /// 検索条件一覧マウスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionListDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //右クリック以外は終了
            if (e.Button != MouseButtons.Right)
            {
                return;

            }

            //無効な列の場合は終了
            var point = new Point(e.X, e.Y);
            var hitTestInfo = this.ConditionListDataGridView.HitTest(point.X, point.Y);
            if (hitTestInfo.Type != DataGridViewHitTestType.None && hitTestInfo.ColumnIndex < 0)
            {
                return;

            }

            //有効な行を選択しているかどうか
            if (hitTestInfo.RowIndex >= 0)
            {
                //右クリックしたセルを選択
                this.ConditionListDataGridView.CurrentCell = this.ConditionListDataGridView[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];

            }

            //行削除
            this.RoeDeleteToolStripMenuItem.Visible = hitTestInfo.RowIndex >= 0;

            //コンテキストメニュー表示
            this.ConditionListContextMenuStrip.Tag = hitTestInfo.RowIndex;
            this.ConditionListContextMenuStrip.Show(this.ConditionListDataGridView, point);

        }
        #endregion

        #region 検索条件一覧コンテキストメニュー
        /// <summary>
        /// 行削除選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoeDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
            {
                return;

            }

            var rowIndex = (int)this.ConditionListContextMenuStrip.Tag;

            var list = this.GetConditionList();

            //バインド可否
            this.isBind = true;

            //選択行削除
            list.RemoveAt(rowIndex);

            //行番号再設定
            var i = (short)0;
            list.ForEach(x => x.行番号 = i++);

            //一覧セット
            var index = this.GetScrollingRowIndex(rowIndex);
            this.SetConditionList(list);

            //行があるかどうか
            if (this.ConditionListDataGridView.RowCount > 0)
            {
                //削除した行と同じ行を選択状態に設定
                this.ConditionListDataGridView.FirstDisplayedScrollingRowIndex = index;
                this.ConditionListDataGridView.Rows[rowIndex < i ? rowIndex : rowIndex - 1].Selected = true;

            }

            //バインド可否
            this.isBind = false;

        }

        /// <summary>
        /// 行追加選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var list = this.GetConditionList();

            var rowIndex = (int)this.ConditionListContextMenuStrip.Tag;

            var cond = new ControlSheetSearchConditionModel
            {
                //ユーザーID
                ユーザーID = SessionDto.UserId,

                //条件名
                条件名 = this.SearchConditionComboBox.Text,


            };

            //バインド可否
            this.isBind = true;

            //行追加
            rowIndex = (rowIndex < 0 ? 0 : rowIndex + 1);
            list.Insert(rowIndex, cond);

            //行番号再設定
            var i = (short)0;
            list.ForEach(x => x.行番号 = i++);

            //一覧セット
            var index = this.GetScrollingRowIndex(rowIndex);
            this.SetConditionList(list);

            //追加した行を選択状態に設定
            this.ConditionListDataGridView.FirstDisplayedScrollingRowIndex = index;
            this.ConditionListDataGridView.Rows[rowIndex].Selected = true;

            //バインド可否
            this.isBind = false;

        }

        /// <summary>
        /// クリア選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //削除可否を問い合わせ
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return;

            }

            //検索条件一覧クリア
            this.SetConditionList(null);

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
            //検索条件がOKかどうか
            if (this.IsSearch() == true)
            {
                //検索条件設定
                this.SetSearchCondition();

                //フォームクローズ
                base.FormOkClose();

            }

        }

        /// <summary>
        /// 検索条件設定
        /// </summary>
        private void SetSearchCondition()
        {
            var cond = new ControlSheetModel { ControlSheetSearch = new ControlSheetSearchModel() };

            //簡易検索のタブを選択しているかどうか
            if (this.SearchTabControl.SelectedTab == this.SimpleTabPage)
            {
                //選択検索条件
                this.SelectedUserSearchCondition = null;

                //固定資産NO
                cond.ControlSheetSearch.固定資産NO = this.FixedAssetTextBox.Text;

                //管理票NO
                cond.ControlSheetSearch.管理票NO = this.ManagementNoTextBox.Text;

                //車体番号
                cond.ControlSheetSearch.車体番号 = this.CarBodyNoTextBox.Text;

                //号車
                cond.ControlSheetSearch.号車 = this.CarTextBox.Text;

                //登録ナンバー
                cond.ControlSheetSearch.登録ナンバー = this.EntryNoTextBox.Text;

                //駐車場番号
                cond.ControlSheetSearch.駐車場番号 = this.ParkingNoTextBox.Text;

                //Append Start 2021/07/06 矢作
                //改修前車両の表示・非表示
                cond.ControlSheetSearch.改修前車両検索対象 = this.HistoryCheckBox.Checked;
                //Append End 2021/07/06 矢作


            }
            else
            {
                //選択検索条件
                this.SelectedUserSearchCondition = this.SearchConditionComboBox.SelectedItem as ControlSheetSearchConditionModel;

                var list = this.GetConditionList();

                //検索条件を選択しているかどうか
                if (this.SearchConditionComboBox.SelectedIndex > 0)
                {
                    //検索条件
                    list.AddRange(this.controlSheetSearchConditionList.Where(x => x.ユーザーID == this.SelectedUserSearchCondition.ユーザーID && x.条件名 == this.SelectedUserSearchCondition.条件名 && x.行番号 < 0));

                }

                //ユーザー検索条件
                cond.ControlSheetSearchConditionList = list;

            }

            //検索条件
            this.SearchCondition = cond;

        }
        #endregion

        #region ユーザー検索条件の設定
        /// <summary>
        /// ユーザー検索条件の設定
        /// </summary>
        private void SetUserSearchCondition()
        {
            //ユーザー検索条件リスト
            this.controlSheetSearchConditionList = this.GetUserSearchConditionList();

            //ユーザー検索条件
            var list = this.controlSheetSearchConditionList
                .Where(x => x.条件名 != DefaultName)
                .Select(x => new { ユーザーID = x.ユーザーID, 条件名 = x.条件名 })
                .Distinct()
                .Select(x => new ControlSheetSearchConditionModel { ユーザーID = x.ユーザーID, 条件名 = x.条件名 })
                .ToList();

            //保存済み検索条件
            FormControlUtil.SetComboBoxItem(this.SearchConditionComboBox, list);

        }
        #endregion

        #region 検索条件一覧のデータ
        /// <summary>
        /// 検索条件一覧設定
        /// </summary>
        /// <param name="list">検索条件</param>
        private void SetConditionList(IEnumerable<ControlSheetSearchConditionModel> list)
        {
            //検索条件設定
            this.bindingSource.DataSource = list == null || list.Any() == false ? null : list.OrderBy(x => x.行番号).ToList();
            this.bindingSource.ResetBindings(false);

            //一覧を未選択状態に設定
            this.ConditionListDataGridView.CurrentCell = null;

        }

        /// <summary>
        /// 検索条件一覧取得
        /// </summary>
        /// <returns></returns>
        private List<ControlSheetSearchConditionModel> GetConditionList()
        {
            var list = new List<ControlSheetSearchConditionModel>();

            //データソースがあれば取得
            if (this.bindingSource.DataSource != null)
            {
                list = this.bindingSource.DataSource as List<ControlSheetSearchConditionModel>;

            }

            return list.OrderBy(x => x.行番号).ToList();

        }
        #endregion

        #region 検索のチェック
        /// <summary>
        /// 検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearch()
        {
            //簡易検索のタブを選択しているかどうか
            if (this.SearchTabControl.SelectedTab == this.SimpleTabPage)
            {
                var map = new Dictionary<Control, Func<Control, string, string>>();

                var list = new List<TextBox> { this.FixedAssetTextBox, this.ManagementNoTextBox, this.CarBodyNoTextBox, this.CarTextBox, this.EntryNoTextBox, this.ParkingNoTextBox };

                map[this.FixedAssetTextBox] = (c, name) =>
                {
                    var errMsg = "";

                    //全て未入力かどうか 
                    if (list.All(x => string.IsNullOrWhiteSpace(x.Text) == true) == true)
                    {
                        //背景色変更
                        list.ForEach(x => x.BackColor = Const.ErrorBackColor);

                        errMsg = Resources.KKM00004;

                    }

                    return errMsg;

                };

                //入力がOKかどうか
                var msg = Validator.GetFormInputErrorMessage(this, map);
                if (msg != "")
                {
                    Messenger.Warn(msg);
                    return false;

                }

            }
            else
            {
                var list = this.GetConditionList();

                //検索条件が入力されているかどうか
                if (list.All(x => string.IsNullOrWhiteSpace(x.TEXT) == true) == true)
                {
                    Messenger.Warn(Resources.KKM00004);
                    return false;

                }

                var msgList = new List<string>();

                var soeji = 0;
                var jyouken = 0;

                var kakkoList = new List<bool>();

                var startList = new List<string>();
                var endList = new List<string>();

                var sb = new StringBuilder();

                foreach (var x in list)
                {
                    var isKakko = true;

                    //括弧の開始
                    if (string.IsNullOrWhiteSpace(x.BEGIN_KAKKO) == false)
                    {
                        var value = x.BEGIN_KAKKO.Trim();

                        startList.Add(value);

                        sb.Append(value);

                        //括弧がある行以降の行を検証
                        foreach (var y in list.Skip(x.行番号))
                        {
                            //括弧なしで条件設定かどうか
                            if (string.IsNullOrWhiteSpace(y.CONJUNCTION) == true && string.IsNullOrWhiteSpace(y.TEXT) == false)
                            {
                                isKakko = true;
                                break;

                            }
                            //条件があるかどうか
                            else if (string.IsNullOrWhiteSpace(y.CONJUNCTION) == false)
                            {
                                isKakko = false;
                                break;

                            }

                        }

                    }

                    //添字が設定されているかどうか
                    if (string.IsNullOrWhiteSpace(x.CONJUNCTION) == false)
                    {
                        soeji++;

                    }

                    //表示条件
                    if (string.IsNullOrWhiteSpace(x.TEXT) == false)
                    {
                        var value = x.TEXT.Trim();

                        sb.Append(value);

                        jyouken++;

                    }

                    //括弧の終了
                    if (string.IsNullOrWhiteSpace(x.END_KAKKO) == false)
                    {
                        var value = x.END_KAKKO.Trim();

                        endList.Add(value);

                        sb.Append(value);

                    }

                    kakkoList.Add(isKakko);

                }

                var conjunctionList = new List<bool>();

                //2行目以降で添え字の位置があっているかどうかを判定
                foreach (var x in list.Skip(1))
                {
                    var flg = false;

                    //添字があるかどうか
                    if (string.IsNullOrWhiteSpace(x.CONJUNCTION) == false)
                    {
                        //添え字がある行以降の行を検証
                        foreach (var y in list.Skip(x.行番号))
                        {
                            //条件があるかどうか
                            if (string.IsNullOrWhiteSpace(y.TEXT) == false)
                            {
                                flg = true;
                                break;

                            }

                        }

                        conjunctionList.Add(flg);

                    }

                }

                //条件が1個のみで添字があるかどうか
                if (jyouken == 1 && soeji != 0)
                {
                    msgList.Add(Resources.TCM03005);

                }
                //複数条件で先頭行に添え字があるか添字の数があっているかどうか
                else if (jyouken > 1 && string.IsNullOrWhiteSpace(list.First().CONJUNCTION) == false || (conjunctionList.Contains(false) ==  true || soeji != (jyouken - 1)) || kakkoList.Contains(false) == true)
                {
                    msgList.Add(Resources.TCM03006);

                }

                //開始と終了の括弧の数が不一致か括弧内に条件が無い場合はエラー
                if (startList.Sum(x => x.Length) != endList.Sum(x => x.Length) || sb.ToString().Contains(Start + End) == true)
                {
                    msgList.Add(Resources.TCM03007);

                }

                //エラーがあるかどうか
                if (msgList.Any() == true)
                {
                    Messenger.Warn(string.Join(Const.CrLf, msgList.Distinct().ToArray()));
                    return false;

                }

            }

            return true;

        }

        #endregion

        #region スクロールする行を取得
        /// <summary>
        /// スクロールする行を取得
        /// </summary>
        /// <param name="index">対象行</param>
        /// <returns></returns>
        private int GetScrollingRowIndex(int index)
        {
            var grid = this.ConditionListDataGridView;

            //行があるかどうか
            if (grid.RowCount == 0)
            {
                return 0;

            }

            var firstRow = grid.Rows.GetFirstRow(DataGridViewElementStates.Displayed);
            var lastRow = grid.Rows.GetLastRow(DataGridViewElementStates.Displayed);

            var scrollingRowIndex = grid.FirstDisplayedScrollingRowIndex;

            firstRow += (grid.GetRowDisplayRectangle(firstRow, false).Height == grid.GetRowDisplayRectangle(firstRow, true).Height ? 0 : 1);
            lastRow -= (grid.GetRowDisplayRectangle(lastRow, false).Height == grid.GetRowDisplayRectangle(lastRow, true).Height ? 0 : 1);

            //スクロール無しで表示できるなら終了
            if (firstRow <= index && index <= lastRow)
            {
                return scrollingRowIndex;

            }

            if (lastRow < index)
            {
                scrollingRowIndex = firstRow + index - lastRow;

            }
            else if (index < firstRow)
            {
                scrollingRowIndex = index;

            }

            return scrollingRowIndex;

        }
        #endregion

        #region API
        /// <summary>
        /// ユーザー検索条件取得
        /// </summary>
        /// <param name="name">条件名</param>
        /// <returns></returns>
        private List<ControlSheetSearchConditionModel> GetUserSearchConditionList(string name = null)
        {
            var cond = new ControlSheetSearchConditionSearchModel
            {
                //ユーザーID
                ユーザーID = this.AllUserCheckBox.Checked == true ? null : SessionDto.UserId,

                //条件名
                条件名 = name

            };

            //APIで取得
            var res = HttpUtil.GetResponse<ControlSheetSearchConditionSearchModel, ControlSheetSearchConditionModel>(ControllerType.ControlSheetSearchCondition, cond);

            //レスポンスが取得できたかどうか
            var list = new List<ControlSheetSearchConditionModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// ユーザー検索条件削除
        /// </summary>
        /// <param name="cond">ユーザー検索条件</param>
        /// <returns></returns>
        private bool DeleteUserSearchCondition(ControlSheetSearchConditionModel cond)
        {
            var list = new[] { cond };

            //削除
            var res = HttpUtil.DeleteResponse(ControllerType.ControlSheetSearchCondition, list);

            //削除できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);

        }

        /// <summary>
        /// ユーザー検索条件マスター取得
        /// </summary>
        /// <param name="item">ユーザー検索項目</param>
        /// <returns></returns>
        private List<CommonMasterModel> GetUserSearchMaster(UserSearchItemModel item)
        {
            var cond = new ControlSheetSearchMasterModel
            {
                //テーブル名
                Table = item.MasterTableName,

                //コード列名
                CodeColumn = item.MasterCodeColumnName,

                //表示列名
                NameColumn = item.MasterNameColumnName

            };

            //APIで取得
            var res = HttpUtil.GetResponse<ControlSheetSearchMasterModel, CommonMasterModel>(ControllerType.ControlSheetSearchMaster, cond);

            //レスポンスが取得できたかどうか
            var list = new List<CommonMasterModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion


        //Append Start 2021/10/12 矢作
        private void ParkingSelectButton_Click(object sender, EventArgs e)
        {
            using (var form = new ParkingSelectForm { UserAuthority = this.UserAuthority, FromTestCar = true })
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }
        //Append End 2021/10/12 矢作

    }
}
