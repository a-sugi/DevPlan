using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using DevPlan.Presentation.UIDevPlan.WorkProgress;

namespace DevPlan.Presentation.UIDevPlan.WorkProgress
{
    /// <summary>
    /// 車種別進捗状況一覧
    /// </summary>
    public partial class WorkProgressForm : BaseForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "車種別進捗状況一覧"; } }

        /// <summary>車系</summary>
        public string CAR_GROUP { get { return CarGroup; } set { CarGroup = value; } }

        /// <summary>開発符号</summary>
        public string GENERAL_CODE { get { return GeneralCode; } set { GeneralCode = value; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region 内部変数
        /// <summary>
        /// 選択部ID
        /// </summary>
        private string selectDepartmentID = null;

        /// <summary>
        /// 選択課ID
        /// </summary>
        private string selectSectionID = null;

        /// <summary>
        /// 選択担当ID
        /// </summary>
        private string selectSectionGroupID = null;

        /// <summary>車系</summary>
        private string CarGroup = null;

        /// <summary>開発符号</summary>
        private string GeneralCode = null;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkProgressForm()
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
        private void WorkProgressForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //権限
                this.UserAuthority = base.GetFunction(FunctionID.Plan);

                //画面初期化
                this.Init();                
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //カラムヘッダの背景色を変更
            FollowListDataGridView.EnableHeadersVisualStyles = false;
            FollowListDataGridView.Columns["キーワード"].HeaderCell.Style.BackColor = Color.LightBlue;

            //開発符号
            if(GeneralCode != null && CarGroup != null)
            {
                this.GeneralCodeComboBox.DataSource = new List<ComboBoxDto>{ new ComboBoxDto()
                    {
                        CODE = CarGroup + "  " + GeneralCode,
                        ID = GeneralCode
                    }};
                this.GeneralCodeComboBox.SelectedIndex = 0;
            }

            //出力権限のないログインユーザの場合、ダウンロードボタンは表示しない。
            if (this.UserAuthority.EXPORT_FLG != '1')
            {
                DownloadButton.Visible = false;
            }

            //更新権限のないログインユーザの場合、登録ボタンは表示しない。
            if (this.UserAuthority.UPDATE_FLG != '1')
            {
                EntryButton.Visible = false;
            }

            //所属にログインユーザの課名を表示
            AffiliationComboBox.Items.Clear();
            SetSelectID(null, null, SessionDto.SectionGroupID);
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode + " " + SessionDto.SectionCode + " " + SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;

            //表示項目検索
            SearchItems();            
        }
        #endregion

        #region 選択ID保存
        /// <summary>
        /// 選択ID保存
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="SectionID"></param>
        /// <param name="SectionGroupID"></param>
        private void SetSelectID(string DepartmentID, string SectionID, string SectionGroupID)
        {
            //選択ID保存
            this.selectDepartmentID = DepartmentID;
            this.selectSectionID = SectionID;
            this.selectSectionGroupID = SectionGroupID;
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
                SearchItems();
            });
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
            //登録処理
            EntryItems();
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

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        private void EntryItems(bool closing = false)
        {
            List<WorkProgressItemModel> entryList = new List<WorkProgressItemModel>();

            for (int i = 0; i < FollowListDataGridView.Rows.Count; i++)
            {
                if(FollowListDataGridView["キーワード", i].Value == FollowListDataGridView["キーワード比較", i].Value)
                {
                    continue;
                }

                //パラメータ設定
                var itemCond = new WorkProgressItemModel
                {
                    //ID
                    ID = Convert.ToInt64(FollowListDataGridView["ID", i].Value.ToString()),

                    //キーワード
                    SELECT_KEYWORD = FollowListDataGridView["キーワード", i].Value == null ? null : 
                                     FollowListDataGridView["キーワード", i].Value.ToString(),
                };

                //リストに追加
                entryList.Add(itemCond);
            }

            if(0 < entryList.Count)
            {
                //更新の確認
                if(closing == true)
                {
                    //変更があれば確認ダイアログ表示
                    if (Messenger.Confirm(Resources.KKM00006) == DialogResult.No)
                    {
                        //確認NOの場合終了
                        return;
                    }
                }

                //登録
                var res = HttpUtil.PutResponse<WeeklyReportModel>(ControllerType.WorkProgress, entryList);
                if ((res != null || res.Status == Const.StatusSuccess) && (closing == false))
                {
                    //正常完了していれば表示更新
                    SearchItems();

                    //登録完了メッセージ表示
                    SetMessageLabel(Resources.KKM00002, Color.Black);
                }
            }
        }
        #endregion

        #region 画面表示項目検索
        /// <summary>
        /// 画面表示項目検索
        /// </summary>
        private void SearchItems()
        {
            //ステータス
            string status = null;
            if(OpenCheckBox.Checked != CloseCheckBox.Checked)
            {
                if(OpenCheckBox.Checked == true)
                {
                    status = "open";
                }
                else
                {
                    status = "close";
                }
            }

            //開発符号
            string generalCode = null;
            if (this.GeneralCodeComboBox.SelectedValue != null)
            {
                generalCode = this.GeneralCodeComboBox.SelectedValue.ToString();
            }

            //パラメータ設定
            var itemCond = new WorkProgressItemSearchModel
            {
                //ID
                ID = null,

                //開発符号
                GENERAL_CODE = generalCode,

                //部ID
                DEPARTMENT_ID = this.selectDepartmentID,

                //課ID
                SECTION_ID = this.selectSectionID,

                //担当ID
                SECTION_GROUP_ID = this.selectSectionGroupID,

                //ステータス
                OPEN_CLOSE = status,
            };

            //Get実行
            var res = HttpUtil.GetResponse<WorkProgressItemSearchModel, WorkProgressItemModel>(ControllerType.WorkProgress, itemCond);
            FollowListDataGridView.DataSource = null;
            if (res == null || res.Status != Const.StatusSuccess)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
                return;
            }

            //取得した情報を画面表示
            FollowListDataGridView.AutoGenerateColumns = false;
            FollowListDataGridView.DataSource = (res.Results).ToList();

            FollowListDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            FollowListDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            FollowListDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            for (int i = 0; i <  FollowListDataGridView.Rows.Count; i++)
            {
                //取得した情報によりステータスの文字色を変更する
                if (FollowListDataGridView["ステータス", i].Value.ToString() == "close")
                {
                    FollowListDataGridView["ステータス", i].Style.ForeColor = Color.Blue;
                    FollowListDataGridView["ステータス", i].Value = "Close";
                }
                else
                {
                    FollowListDataGridView["ステータス", i].Style.ForeColor = Color.Red;
                    FollowListDataGridView["ステータス", i].Value = "Open";
                }

                //取得した情報により日付の背景色を変更する
                DateTime nowDate = DateTime.Now.Date;

                DateTime convertDate = Convert.ToDateTime(DateTimeUtil.ConvertDateStringToDateTime(FollowListDataGridView["日付", i].Value?.ToString()??""));

                TimeSpan span = nowDate - convertDate;
                if(span.Days <= 7)
                {
                    FollowListDataGridView["日付", i].Style.BackColor = Color.Yellow;
                }

                convertDate = Convert.ToDateTime(DateTimeUtil.ConvertDateStringToDateTime(FollowListDataGridView["編集日時", i].Value?.ToString()??""));
                span = nowDate - convertDate;
                if (90 <= span.Days)
                {
                    FollowListDataGridView["日付", i].Style.BackColor = Color.Gray;
                }

                //変更比較用を設定
                FollowListDataGridView["キーワード比較", i].Value = FollowListDataGridView["キーワード", i].Value;
            }

            FollowListDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            

            //メッセージラベルを初期化
            SetMessageLabel("", Color.Black);

            //グリッドを非選択
            FollowListDataGridView.CurrentCell = null;
        }
        #endregion

        #region 課検索
        /// <summary>
        /// 課検索
        /// </summary>
        /// <param name="sectionid"></param>
        private List<SectionModel> SearchSection(string sectionid)
        {
            //パラメータ設定
            var itemCond = new SectionSearchModel
            {
                //ID
                SECTION_ID = sectionid,
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 担当検索
        /// <summary>
        /// 担当検索
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="sectionid"></param>
        /// <param name="sectiongroupid"></param>
        private List<SectionGroupModel> SearchSectionGroup(string departmentid, string sectionid, string sectiongroupid)
        {
            //パラメータ設定
            var itemCond = new SectionGroupSearchModel
            {
                //ID
                DEPARTMENT_ID = departmentid,
                //ID
                SECTION_ID = sectionid,
                //ID
                SECTION_GROUP_ID = sectiongroupid,
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>(ControllerType.SectionGroup, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 部署コンボボックスクリック
        /// <summary>
        /// 部署コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_Click(object sender, EventArgs e)
        {
            //変更登録チェック
            //SaveItems();

            if (this.DepartmentRadioButton.Checked)
            {
                // 部

                return;
            }
            else if (this.SectionRadioButton.Checked)
            {
                // 課
                var form = new Common.SectionListForm();

                //コンボボックスを操作禁止
                form.DEPARTMENT_COMBOBOX = false;

                //IDセット
                form.DEPARTMENT_ID = SessionDto.DepartmentID;

                // 課検索
                form.ShowDialog();

                //選択された課IDからリストを取得
                List<SectionModel> list = SearchSection(form.SECTION_ID);
                form.Dispose();

                if ((0 < list.Count) && (list[0].DEPARTMENT_CODE != null) && (list[0].SECTION_CODE != null))
                {
                    //コンボボックスの表示を更新
                    AffiliationComboBox.Items.Clear();
                    SetSelectID(null, list[0].SECTION_ID, null);
                    AffiliationComboBox.Items.Add(list[0].DEPARTMENT_CODE + " " + list[0].SECTION_CODE);
                    AffiliationComboBox.SelectedIndex = 0;
                }
            }
            else if (this.SectionGroupRadioButton.Checked)
            {
                //担当
                using (var form = new SectionGroupListForm { DEPARTMENT_ID = SessionDto.DepartmentID, SECTION_ID = SessionDto.SectionID, DEPARTMENT_COMBOBOX = false })
                {
                    // 担当検索
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        //選択された課IDからリストを取得
                        var list = SearchSectionGroup(form.DEPARTMENT_ID, form.SECTION_ID, form.SECTION_GROUP_ID);


                        if ((0 < list.Count) && (list[0].DEPARTMENT_CODE != null) &&
                            (list[0].SECTION_CODE != null) && (list[0].SECTION_GROUP_CODE != null))
                        {
                            //コンボボックスの表示を更新
                            AffiliationComboBox.Items.Clear();
                            SetSelectID(null, null, list[0].SECTION_GROUP_ID);
                            AffiliationComboBox.Items.Add(list[0].DEPARTMENT_CODE + " " + list[0].SECTION_CODE + " " + list[0].SECTION_GROUP_CODE);
                            AffiliationComboBox.SelectedIndex = 0;
                        }

                    }

                }

            }

        }
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //登録処理
            EntryItems(true);
        }
        #endregion

        #region 開発符号コンボボックスクリック
        /// <summary>
        /// 開発符号コンボボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralCodeComboBox_Click(object sender, EventArgs e)
        {
            this.label3.Focus();

            var form = new GeneralCodeListForm();

            // パラメータ設定
            form.CAR_GROUP = this.CarGroup;

            // 開発符号検索
            if (form.ShowDialog().Equals(DialogResult.OK) && form.GENERAL_CODE != null)
            {
                // パラメータ取得
                var code = form.CAR_GROUP + "　" + form.GENERAL_CODE;
                var id = form.GENERAL_CODE;

                // コンボボックス設定
                this.GeneralCodeComboBox.DataSource =
                    new List<ComboBoxDto> { new ComboBoxDto() { CODE = code, ID = id } };
                this.GeneralCodeComboBox.SelectedValue = id;

                // 車系の退避
                this.CarGroup = form.CAR_GROUP;

                // スケジュール設定（項目含む）
                //this.SetScheduleAll();

            }

            form.Dispose();
        }
        #endregion

        #region 部ラジオボタン選択
        /// <summary>
        /// 部ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (DepartmentRadioButton.Checked == false)
            {
                return;
            }

            //所属にログインユーザの部名を表示
            AffiliationComboBox.Items.Clear();
            SetSelectID(SessionDto.DepartmentID, null, null);
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode);
            AffiliationComboBox.SelectedItem = SessionDto.DepartmentCode;
        }
        #endregion

        #region 課ラジオボタン選択
        /// <summary>
        /// 課ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SectionRadioButton.Checked == false)
            {
                return;
            }

            //所属にログインユーザの課名を表示
            AffiliationComboBox.Items.Clear();
            SetSelectID(null, SessionDto.SectionID, null);
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode + " " + SessionDto.SectionCode);
            AffiliationComboBox.SelectedIndex = 0;
        }
        #endregion

        #region 担当ラジオボタン選択
        /// <summary>
        /// 担当ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionGroupRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (SectionGroupRadioButton.Checked == false)
            {
                return;
            }

            //所属にログインユーザの担当名を表示
            AffiliationComboBox.Items.Clear();
            SetSelectID(null, null, SessionDto.SectionGroupID);
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode + " " + SessionDto.SectionCode + " " + SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;
        }
        #endregion

        #region 全部署ラジオボタン選択
        /// <summary>
        /// 全部署ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (AllRadioButton.Checked == false)
            {
                //所属コンボボックスを表示
                AffiliationComboBox.Visible = true;
                return;
            }

            //所属コンボボックスを非表示
            AffiliationComboBox.Visible = false;

            //所属をクリア
            AffiliationComboBox.Items.Clear();
            SetSelectID(null, null, null);
        }
        #endregion

        #region ダウンロードボタンクリック
        /// <summary>
        /// ダウンロードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            // 2017/3時点ではボタン表示のみ
        }
        #endregion

        #region グリッドダウブルクリック
        /// <summary>
        /// グリッドダウブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FollowListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView g = sender as DataGridView;

            if (g != null)
            {
                int row = e.RowIndex;
                int column = e.ColumnIndex;

                if (0 <= row && row < FollowListDataGridView.RowCount &&
                    FollowListDataGridView.Columns[column].ReadOnly == true)
                {
                    //進捗履歴画面表示
                    using (var form = new WorkProgressHistroryForm { ScheduleItem = ((List<WorkProgressItemModel>)(g.DataSource))[e.RowIndex], UserAuthority = this.UserAuthority })
                    {
                        form.ShowDialog(this);
                    }
                }
            }
        }
        #endregion

    }
}