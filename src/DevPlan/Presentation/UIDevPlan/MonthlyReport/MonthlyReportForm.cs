using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.UIDevPlan.ReportMaterial;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.MonthlyReport
{
    public partial class MonthlyReportForm : BaseForm
    {
        private const string SORT_ON = "項目(ｿｰﾄ解除)";
        private const string SORT_OFF = "項目(ｿｰﾄ)";

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "月報"; } }

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }
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

        /// <summary>
        /// 課リスト
        /// </summary>
        private List<SectionModel> SectionList = new List<SectionModel>();

        /// <summary>
        /// カレンダー選択年
        /// </summary>
        //private int CalenderYear = 0;

        /// <summary>
        /// グリッド選択行
        /// </summary>
        private int selectIndex = 0;

        /// <summary>
        /// グリッド挿入行
        /// </summary>
        private int insertIndex = 0;

        /// <summary>
        /// 日付一時保存
        /// </summary>
        //string dateTimePickerText = "";

        /// <summary>
        /// 月報タイトル
        /// </summary>
        private static string TitileName = "";

        /// <summary>
        /// 担当/部長名
        /// </summary>
        private static string TitleManagerName = "";

        /// <summary>
        /// 選択した指定月
        /// </summary>
        private DateTime? selectedDate = new DateTime();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MonthlyReportForm()
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
        private void MonthlyReportForm_Load(object sender, EventArgs e)
        {
            //権限
            this.UserAuthority = base.GetFunction(FunctionID.MonthlyReport);

            //画面初期化
            this.Init();

        }

        /// <summary>
        /// 画面初期化　
        /// </summary>
        private void Init()
        {
            var defaultTime = DateTime.Now;
            LastDayNullableDateTimePicker.Text = defaultTime.ToString("yyyy/MM");
            this.selectedDate = new DateTime(defaultTime.Year, defaultTime.Month, 1);

            //選択された課IDからリストを取得
            this.SectionList = SearchSection(SessionDto.DepartmentID, null);

            //部署コンボリスト作成
            SetListItem();

            //初期表示の課を設定
            SetSelectID(null, SessionDto.SectionID, null);

            //初期表示項目検索
            SearchItems(true);

            //出力権限のないログインユーザの場合、ダウンロードボタンは表示しない。
            if (this.UserAuthority.EXPORT_FLG != '1')
            {
                DownloadButton.Visible = false;
            }

            //管理権限のないログインユーザの場合、「各課月報を一つにまとめる。」チェックボックスは表示しない。
            if (this.UserAuthority.MANAGEMENT_FLG != '1')
            {
                SumCheckBox.Visible = false;
            }

            //更新権限のないログインユーザの場合、登録ボタンは表示しない。
            if (this.UserAuthority.UPDATE_FLG != '1')
            {
                EntryButton.Visible = false;
            }

            LastDayNullableDateTimePicker.Tag = "Required;ItemName(指定月)";
        }
        #endregion

        #region 行読込ボタンクリック
        /// <summary>
        /// 行読込ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            List<InfoListModel> returnList;

            //帳票タイトル作成
            if (SumCheckBox.Checked)
            {
                // 部

                //情報元一覧(月報)画面を表示
                ReportMaterialMonthlyForm infoListForm = new ReportMaterialMonthlyForm();
                infoListForm.FirstDay = (DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01");
                infoListForm.LastDay = ((DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01")).AddMonths(1).AddDays(-1);
                infoListForm.DepartmentID = this.selectDepartmentID;
                infoListForm.ShowDialog();
                returnList = infoListForm.ReturnList;
                infoListForm.Dispose();
            }
            else
            {
                //課

                //情報元一覧(週報)画面を表示
                ReportMaterialWeeklyForm infoListForm = new ReportMaterialWeeklyForm();
                infoListForm.FirstDay = (DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01");
                infoListForm.LastDay = ((DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01")).AddMonths(1).AddDays(-1);
                infoListForm.DepartmentID = this.selectDepartmentID;
                infoListForm.SectionID = this.selectSectionID;
                infoListForm.ShowDialog();
                returnList = infoListForm.ReturnList;
                infoListForm.Dispose();
            }

            //情報元一覧画面で選択した項目を追加
            foreach (InfoListModel info in returnList)
            {
                MonthlyReportDataGridView.Rows.Add();
                int idx = MonthlyReportDataGridView.Rows.Count - 1;
                MonthlyReportDataGridView.Rows[idx].Cells["Category"].Value = info.CATEGORY;
                MonthlyReportDataGridView.Rows[idx].Cells["CurrentSituation"].Value = info.CURRENT_SITUATION;
                MonthlyReportDataGridView.Rows[idx].Cells["FutureSchedule"].Value = info.FUTURE_SCHEDULE;
                MonthlyReportDataGridView.Rows[idx].Cells["MonthFirstDay"].Value = info.LISTED_DATE;
            }

            //ソート番号を振り直す
            SetSortNo();
        }
        #endregion

        #region 行追加ボタンクリック
        /// <summary>
        /// 行追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (MonthlyReportDataGridView.Rows.Count == 0)
            {
                //行がない場合追加１行追加
                MonthlyReportDataGridView.Rows.Add();
            }
            else
            {
                if (MonthlyReportDataGridView.SelectedRows.Count == 0)
                {
                    //選択行がない場合、先頭行に追加
                    MonthlyReportDataGridView.Rows.Insert(0);
                }
                else
                {
                    //選択行がある場合、選択行に追加
                    MonthlyReportDataGridView.Rows.Insert(MonthlyReportDataGridView.SelectedRows[0].Index);
                }
            }

            //ソート番号を振り直す
            SetSortNo();
        }
        #endregion

        #region 行削除ボタンクリック
        /// <summary>
        /// 行削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //グリッドが０行の場合、処理終了
            if (MonthlyReportDataGridView.Rows.Count == 0)
            {
                return;
            }

            //削除確認メッセージ表示
            DialogResult result = Messenger.Confirm(Resources.KKM00008);
            if (result == DialogResult.Yes)
            {
                if (MonthlyReportDataGridView.SelectedRows.Count == 0)
                {
                    //選択行がない場合、１行目を削除
                    MonthlyReportDataGridView.Rows.RemoveAt(0);
                }
                else
                {
                    //選択されている行を削除
                    foreach (DataGridViewRow item in MonthlyReportDataGridView.SelectedRows)
                    {
                        if (!item.IsNewRow)
                        {
                            MonthlyReportDataGridView.Rows.Remove(item);
                        }
                    }
                }

                //ソート番号を振り直す
                SetSortNo();

                //削除メッセージ表示
                SetMessageLabel(Resources.KKM00003, Color.Black);
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
            //登録処理
            bool enter = EnterItems();

            if (enter)
            {
                //表示項目検索
                SearchItems();

                //完了メッセージ
                SetMessageLabel(Resources.KKM00002, Color.Black);
            }
        }
        #endregion

        #region ソート番号設定
        /// <summary>
        /// ソート番号設定
        /// </summary>
        private void SetSortNo()
        {
            //ソート番号を振り直す
            for (int i = 0; i < this.MonthlyReportDataGridView.Rows.Count; i++)
            {
                MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value = (float)i;
            }
        }
        #endregion

        /// <summary>
        /// 変更登録チェック
        /// </summary>
        private void SaveItems()
        {
            //登録ボタンが非表示の場合、権限がないので以下のチェック、登録は行わない。
            if (EntryButton.Visible == false)
            {
                return;
            }

            //変更があるかチェック
            bool flag = false;
            if (this.CompareDataGridView.Rows.Count != this.MonthlyReportDataGridView.Rows.Count)
            {
                //件数が増減しているので変更有
                flag = true;
            }

            if (flag == false)
            {
                for (int i = 0; i < this.CompareDataGridView.Rows.Count; i++)
                {
                    //比較用と画面表示をチェック
                    if (MonthlyReportDataGridView.Rows[i].Cells["Category"].Value !=
                            CompareDataGridView.Rows[i].Cells["CategoryCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value !=
                            CompareDataGridView.Rows[i].Cells["CurrentSituationCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value !=
                            CompareDataGridView.Rows[i].Cells["FutureScheduleCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (Convert.ToInt32(MonthlyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()) !=
                            Convert.ToInt64(CompareDataGridView.Rows[i].Cells["IDCompare"].Value.ToString()))
                    {
                        flag = true;
                        break;
                    }
                    if (Convert.ToSingle(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()) !=
                            Convert.ToSingle(CompareDataGridView.Rows[i].Cells["SortNoCompare"].Value.ToString()))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            //変更があれば確認ダイアログ表示
            DialogResult result = DialogResult.No;
            if (flag)
            {
                //確認ダイアログ
                result = Messenger.Confirm(Resources.KKM00006);
            }

            //変更なし、確認NOの場合終了
            if (result == DialogResult.No)
            {
                return;
            }

            //登録処理
            bool enter = EnterItems();

            if (enter)
            {
                //表示項目検索
                SearchItems();

                //完了メッセージ
                SetMessageLabel(Resources.KKM00002, Color.Black);
            }
        }

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        private bool EnterItems()
        {
            //ソート番号を振り直す
            SetSortNo();

            List<MonthlyReportModel> deleteList = new List<MonthlyReportModel>();

            //削除されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.CompareDataGridView.Rows.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < this.MonthlyReportDataGridView.Rows.Count; j++)
                {
                    if (CompareDataGridView.Rows[i].Cells["IDCompare"].Value.Equals(MonthlyReportDataGridView.Rows[j].Cells["ID"].Value))
                    {
                        flag = true;
                        break;
                    }
                }

                //削除されている場合は週報IDをリストに追加
                if (flag == false)
                {
                    //パラメータ設定
                    var itemCond = new MonthlyReportModel
                    {
                        //ID
                        ID = Convert.ToInt32(CompareDataGridView.Rows[i].Cells["IDCompare"].Value.ToString()),
                    };

                    //リストに追加
                    deleteList.Add(itemCond);
                }
            }

            List<MonthlyReportModel> postList = new List<MonthlyReportModel>();
            List<MonthlyReportModel> putList = new List<MonthlyReportModel>();

            //追加、変更されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.MonthlyReportDataGridView.Rows.Count; i++)
            {
                if (MonthlyReportDataGridView.Rows[i].Cells["ID"].Value == null)
                {
                    //追加

                    //パラメータ設定
                    var itemCond = new MonthlyReportModel
                    {
                        //部ID
                        DEPARTMENT_ID = this.selectDepartmentID,

                        //課ID
                        SECTION_ID = this.selectSectionID,

                        //項目
                        GENERAL_CODE = MonthlyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                        //現状
                        CURRENT_SITUATION = MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                        //今後の予定
                        FUTURE_SCHEDULE = MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                        //ソート番号
                        SORT_NO = MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),

                        //対象月
                        MONTH_FIRST_DAY = this.selectedDate,
                    };

                    //リストに追加
                    postList.Add(itemCond);
                }
                else
                {
                    for (int j = 0; j < this.CompareDataGridView.Rows.Count; j++)
                    {
                        if ((Convert.ToInt64(MonthlyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()) ==
                                Convert.ToInt64(CompareDataGridView.Rows[j].Cells["IDCompare"].Value.ToString())) &&
                            ((MonthlyReportDataGridView.Rows[i].Cells["Category"].Value != CompareDataGridView.Rows[j].Cells["CategoryCompare"].Value) ||
                            (MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value != CompareDataGridView.Rows[j].Cells["CurrentSituationCompare"].Value) ||
                            (MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value != CompareDataGridView.Rows[j].Cells["FutureScheduleCompare"].Value) ||
                            ((Convert.ToInt64(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value) !=
                                Convert.ToInt64(CompareDataGridView.Rows[j].Cells["SortNoCompare"].Value)))))
                        {
                            //変更

                            //パラメータ設定
                            var itemCond = new MonthlyReportModel
                            {
                                //ID
                                ID = Convert.ToInt32(MonthlyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()),

                                //項目
                                GENERAL_CODE = MonthlyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                                //現状
                                CURRENT_SITUATION = MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                                //今後の予定
                                FUTURE_SCHEDULE = MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                                //ソート番号
                                SORT_NO = MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),
                            };

                            //リストに追加
                            putList.Add(itemCond);

                            break;
                        }
                    }
                }
            }

            //更新がなければ終了
            if ((deleteList.Count == 0) && (putList.Count == 0) && (postList.Count == 0))
            {
                return false;
            }

            //DELETE実行
            if (0 < deleteList.Count)
            {
                var res = HttpUtil.DeleteResponse<MonthlyReportModel>(ControllerType.MonthlyReport, deleteList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //PUT実行
            if (0 < putList.Count)
            {
                var res = HttpUtil.PutResponse<MonthlyReportModel>(ControllerType.MonthlyReport, putList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //POST実行
            if (0 < postList.Count)
            {
                var res = HttpUtil.PostResponse<MonthlyReportModel>(ControllerType.MonthlyReport, postList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //比較用リストの更新
            CompareDataGridView.Rows.Clear();
            foreach (DataGridViewRow item in MonthlyReportDataGridView.Rows)
            {
                //比較用
                CompareDataGridView.Rows.Add();
                int idx = CompareDataGridView.Rows.Count - 1;
                CompareDataGridView.Rows[idx].Cells["CategoryCompare"].Value = item.Cells["Category"].Value;
                CompareDataGridView.Rows[idx].Cells["CurrentSituationCompare"].Value = item.Cells["CurrentSituation"].Value;
                CompareDataGridView.Rows[idx].Cells["FutureScheduleCompare"].Value = item.Cells["FutureSchedule"].Value;
                CompareDataGridView.Rows[idx].Cells["IDCompare"].Value = item.Cells["ID"].Value;
                CompareDataGridView.Rows[idx].Cells["SortNoCompare"].Value = item.Cells["SortNo"].Value;
            }

            return true;
        }
        #endregion

        #region 画面表示項目検索
        /// <summary>
        /// 画面表示項目検索
        /// </summary>
        /// <param name="dateFlag"></param>
        private void SearchItems(bool dateFlag = false)
        {
            DateTime? date = null;
            if (dateFlag == false)
            {
                date = GetMonthFirstDay();
            }

            //パラメータ設定
            MonthyReportSearchModel itemCond;
            itemCond = new MonthyReportSearchModel
            {
                //対象月
                MONTH_FIRST_DAY = date,

                //部ID
                DEPARTMENT_ID = this.selectDepartmentID,

                //課ID
                SECTION_ID = this.selectSectionID,
            };

            //Get実行
            var res = HttpUtil.GetResponse<MonthyReportSearchModel, MonthlyReportModel>(ControllerType.MonthlyReport, itemCond);
            MonthlyReportDataGridView.Rows.Clear();
            CompareDataGridView.Rows.Clear();
            if (res == null || res.Status != Const.StatusSuccess)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
                return;
            }

            //取得した情報を画面表示
            DateTime dt = new DateTime();
            List<MonthlyReportModel> list = (res.Results).ToList();
            foreach (MonthlyReportModel item in list)
            {
                //表示編集用
                MonthlyReportDataGridView.Rows.Add();
                int idx = MonthlyReportDataGridView.Rows.Count - 1;
                MonthlyReportDataGridView.Rows[idx].Cells["Category"].Value = item.GENERAL_CODE;
                MonthlyReportDataGridView.Rows[idx].Cells["CurrentSituation"].Value = item.CURRENT_SITUATION;
                MonthlyReportDataGridView.Rows[idx].Cells["FutureSchedule"].Value = item.FUTURE_SCHEDULE;
                MonthlyReportDataGridView.Rows[idx].Cells["ID"].Value = item.ID;
                MonthlyReportDataGridView.Rows[idx].Cells["SortNo"].Value = item.SORT_NO;
                MonthlyReportDataGridView.Rows[idx].Cells["MonthFirstDay"].Value = item.MONTH_FIRST_DAY;
                dt = (DateTime)item.MONTH_FIRST_DAY;

                //比較用
                CompareDataGridView.Rows.Add();
                CompareDataGridView.Rows[idx].Cells["CategoryCompare"].Value = item.GENERAL_CODE;
                CompareDataGridView.Rows[idx].Cells["CurrentSituationCompare"].Value = item.CURRENT_SITUATION;
                CompareDataGridView.Rows[idx].Cells["FutureScheduleCompare"].Value = item.FUTURE_SCHEDULE;
                CompareDataGridView.Rows[idx].Cells["IDCompare"].Value = item.ID;
                CompareDataGridView.Rows[idx].Cells["SortNoCompare"].Value = item.SORT_NO;
                CompareDataGridView.Rows[idx].Cells["MonthFirstDayCompare"].Value = item.MONTH_FIRST_DAY;
            }

            //検索結果が１件もない場合、メッセージを表示
            if (this.MonthlyReportDataGridView.Rows.Count == 0)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
            }
            else
            {
                SetMessageLabel("", Color.Black);

                //取得した日付を表示に反映
                //LastDayNullableDateTimePicker.Value = (DateTime)dt;
                if (dt != null)
                {
                    LastDayNullableDateTimePicker.Text = ((DateTime)dt).ToString("yyyy/MM");

                    //取得した日付を保存
                    this.selectedDate = dt;
                }

                //行選択を行わない
                MonthlyReportDataGridView.CurrentCell = null;

                //帳票タイトル作成
                SetReportTitle();
            }

            //フォーカス
            this.ActiveControl = MonthlyReportDataGridView;
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

        #region 課検索
        /// <summary>
        /// 課検索
        /// </summary>
        /// <param name="sectionid"></param>
        private List<SectionModel> SearchSection(string departmentid, string sectionid)
        {
            //パラメータ設定
            var itemCond = new SectionSearchModel
            {
                //部ID
                DEPARTMENT_ID = departmentid,

                //課ID
                SECTION_ID = sectionid,
            };

            //Get実行
            var res = HttpUtil.GetResponse<SectionSearchModel, SectionModel>(ControllerType.Section, itemCond);

            return (res.Results).ToList();
        }
        #endregion

        #region 部署コンボボックス変更
        /// <summary>
        /// 部署コンボボックス変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AffiliationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //変更登録チェック
            SaveItems();

            if (SumCheckBox.Checked)
            {
                //部
                SetSelectID(SessionDto.DepartmentID, null, null);
            }
            else
            {
                //課
                SetSelectID(null, SectionList[AffiliationComboBox.SelectedIndex].SECTION_ID, null);
            }

            //タイトル設定
            SetReportTitle();

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

        #region 期間取得
        /// <summary>
        /// 期間取得
        /// </summary>
        /// <returns></returns>
        private DateTime? GetMonthFirstDay()
        {
            if(LastDayNullableDateTimePicker.Text.Length == 0)
            {
                return null;
            }

            return (DateTime?)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01");
        }
        #endregion

        #region 各課月報を一つにまとめる。チェックボックス
        /// <summary>
        /// 各課月報を一つにまとめる。チェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SumCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //部署コンボリスト作成
            SetListItem();
        }
        #endregion

        #region 部署コンボボックスリスト作成
        /// <summary>
        /// 部署コンボボックスリスト作成
        /// </summary>
        private void SetListItem()
        {
            if (SumCheckBox.Checked)
            {
                //チェックされた場合
                AffiliationLabel.Text = "部名";

                //部署プルダウンにログインユーザの部を表示
                AffiliationComboBox.Items.Clear();
                AffiliationComboBox.Items.Add(SessionDto.DepartmentCode);
                AffiliationComboBox.SelectedItem = SessionDto.DepartmentCode;
            }
            else
            {
                //チェックが外れた場合
                AffiliationLabel.Text = "課名";

                //部署プルダウンにログインユーザが所属する部の課を表示
                AffiliationComboBox.Items.Clear();
                foreach (SectionModel section in this.SectionList)
                {
                    AffiliationComboBox.Items.Add(section.SECTION_CODE);
                }

                //部署プルダウンにログインユーザの課を表示
                AffiliationComboBox.SelectedItem = SessionDto.SectionCode;
            }
        }
        #endregion

        #region 項目ソートボタン表示
        /// <summary>
        /// 項目ソートボタン表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyReportDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.ColumnIndex == 0) && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(200, 25))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - SortButton.Width) / 2, (bmp.Height - SortButton.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    SortButton.DrawToBitmap(bmp, new System.Drawing.Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

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

        #region 項目ソートボタンクリック
        /// <summary>
        /// 項目ソートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyReportDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex != 0) || (e.RowIndex != -1))
            {
                return;
            }

            if (MonthlyReportDataGridView.Rows.Count == 0)
            {
                return;
            }

            if (SortButton.Text == SORT_OFF)
            {
                //戻すときのためにソート番号を退避
                foreach (DataGridViewRow row in MonthlyReportDataGridView.Rows)
                {
                    row.Cells["SortNoBefore"].Value = row.Cells["SortNo"].Value;
                }

                //ソート実行
                MonthlyReportDataGridView.Sort(MonthlyReportDataGridView.Columns["Category"], System.ComponentModel.ListSortDirection.Ascending);
                MonthlyReportDataGridView.CurrentCell = MonthlyReportDataGridView[0, 0];
                SortButton.Text = SORT_ON;
            }
            else if (SortButton.Text == SORT_ON)
            {
                //ソートを元に戻す
                MonthlyReportDataGridView.Sort(MonthlyReportDataGridView.Columns["SortNoBefore"], System.ComponentModel.ListSortDirection.Ascending);
                MonthlyReportDataGridView.CurrentCell = MonthlyReportDataGridView[0, 0];
                SortButton.Text = SORT_OFF;
            }
        }
        #endregion

        #region グリッドマウスダウン
        /// <summary>
        /// グリッドマウスダウン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            // MouseDownイベント発生時の (x,y)座標を取得
            DataGridView.HitTestInfo hit = MonthlyReportDataGridView.HitTest(e.X, e.Y);
            if (hit.Type != DataGridViewHitTestType.Cell)
            {
                //セル以外は終了
                return;
            }

            if (e.Clicks == 2)
            {
                if (e.Button == MouseButtons.Left)
                {
                    //ダブルクリックの場合はセルを編集状態とする
                    MonthlyReportDataGridView.BeginEdit(true);
                }
                return;
            }

            // マウスの左ボタンが押されている場合
            if (e.Button == MouseButtons.Left)
            {
                // 選択行を複写元行データーとして設定
                System.Windows.Forms.DataGridViewRow Source_Row = GetSelectedRow(hit);

                // 複写先となる行用オブジェクトを作成
                System.Windows.Forms.DataGridViewRow DestinationRow = new System.Windows.Forms.DataGridViewRow();
                DestinationRow.CreateCells(MonthlyReportDataGridView);  // 複写先DataGridView指定

                // 該当行のセルを複写するループ
                for (int i = 0; i < Source_Row.Cells.Count; i++)
                {
                    // セルを複写
                    DestinationRow.Cells[i].Value = Source_Row.Cells[i].Value;
                }

                // ドラッグ&ドロップを開始
                selectIndex = hit.RowIndex;
                DoDragDrop(DestinationRow, DragDropEffects.Move);
            }
        }
        #endregion

        #region グリッドドラッグオーバー
        /// <summary>
        /// グリッドドラッグオーバー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportDataGridView_DragOver(object sender, DragEventArgs e)
        {
            //移動を指定
            e.Effect = DragDropEffects.Move;
        }
        #endregion

        #region グリッドドラッグドロップ
        /// <summary>
        /// グリッドドラッグドロップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            // DragDropイベント発生時の (x,y)座標を取得
            Point clientPoint = MonthlyReportDataGridView.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit =
                MonthlyReportDataGridView.HitTest(clientPoint.X, clientPoint.Y);

            // ドラッグされているデーターが行データー（DataGridViewRow型）で、かつ、
            // ドラッグ ソースのデータは、ドロップ先に複写するよう指示さ
            // れている場合（すなわち、移動等の別の指示ではない場合）
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.DataGridViewRow))
                && (e.Effect == DragDropEffects.Move))
            {
                // ドラッグ ソースの行データー（DataGridViewRow型データー）を取得
                System.Windows.Forms.DataGridViewRow Row_Work = 
                    (System.Windows.Forms.DataGridViewRow)e.Data.GetData(typeof(System.Windows.Forms.DataGridViewRow));

                // ドロップ先としての指定位置が、有効な場合（x,y座標値の取得に成功している場合）
                if (hit.RowIndex != -1)
                {
                    // 行データを、dataGridView2の指定行の前に挿入
                    MonthlyReportDataGridView.Rows.Insert(hit.RowIndex, Row_Work);

                    // 追加した行を選択状態にする。
                    MonthlyReportDataGridView.Rows[hit.RowIndex].Selected = true;

                    if (selectIndex < hit.RowIndex)
                    {
                        MonthlyReportDataGridView.Rows.RemoveAt(selectIndex);
                        insertIndex = hit.RowIndex - 1;
                    }
                    else
                    {
                        MonthlyReportDataGridView.Rows.RemoveAt(selectIndex + 1);
                        insertIndex = hit.RowIndex;
                    }
                }
                // ドロップ先としての指定位置が、有効でない場合（x,y座標値の取得に失敗した場合）
                else
                {
                    // 行データをdataGridView2の末尾に追加
                    MonthlyReportDataGridView.Rows.Add(Row_Work);

                    // 追加した行を選択状態にする。
                    MonthlyReportDataGridView.Rows.RemoveAt(selectIndex);

                    insertIndex = MonthlyReportDataGridView.Rows.Count - 1;
                }
            }

            //ドラッグ＆ドロップ後の行選択指定
            this.GridBackgroundWorker.RunWorkerAsync();
        }
        #endregion

        delegate void SetGridCallback(DataGridView grid);

        #region 行選択指定ワーカー
        /// <summary>
        /// 行選択指定ワーカー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //行選択指定実行
            SetGrid(MonthlyReportDataGridView);
        }
        #endregion

        #region 行選択指定
        /// <summary>
        /// 行選択指定
        /// </summary>
        /// <param name="text"></param>
        private void SetGrid(DataGridView text)
        {
            if (this.MonthlyReportDataGridView.InvokeRequired)
            {
                SetGridCallback callBack = new SetGridCallback(SetGrid);
                this.Invoke(callBack, new object[] { text });
            }
            else
            {
                //行選択
                MonthlyReportDataGridView.Rows[insertIndex].Selected = true;
                MonthlyReportDataGridView.Refresh();
            }
        }
        #endregion

        #region 選択行取得
        /// <summary>
        /// 選択行取得
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private DataGridViewRow GetSelectedRow(DataGridView.HitTestInfo hit)
        {
            // 複写元となる行データ（選択されている行データ）
            DataGridViewRow SourceRow = null;

            if (hit.Type == DataGridViewHitTestType.Cell && (MonthlyReportDataGridView.NewRowIndex == -1
                    || MonthlyReportDataGridView.NewRowIndex != hit.RowIndex))
            {
                // ドラッグ元としての指定位置が、有効なセル上を選択している場合
                // 複写元となる行データー（選択されている行データー）
                SourceRow = MonthlyReportDataGridView.Rows[hit.RowIndex];
            }
            else
            {
                // ドラッグ元の指定位置が、有効なセル上を選択していない場合
                // 指定行は、ドラッグ&ドロップの対象ではないので、処理を終了
                return null;
            }

            return SourceRow;
        }
        #endregion

        #region フォーム表示処理
        /// <summary>
        /// フォーム表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyReportForm_Shown(object sender, EventArgs e)
        {
            MonthlyReportDataGridView.CurrentCell = null;
        }
        #endregion

        #region カレンダー部
        /// <summary>
        /// カレンダー部非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            ChangeLastDayDateTimePicker();
        }

        /// <summary>
        /// カレンダー部キーボード編集完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            ChangeLastDayDateTimePicker();
        }

        /// <summary>
        /// 指定月変更処理。
        /// </summary>
        private void ChangeLastDayDateTimePicker()
        {
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return;
            }

            //日付の変更があった場合表示更新
            if (this.selectedDate != DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01"))
            {
                //変更登録チェック
                SaveItems();

                //表示項目検索
                SearchItems();

                //タイトル設定
                SetReportTitle();
            }

            //変更値を保存
            this.selectedDate = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text + "/01");
        }
        #endregion

        #region タイトル設定
        /// <summary>
        /// タイトル設定
        /// </summary>
        private void SetReportTitle()
        {
            if (SumCheckBox.Checked)
            {
                //部
                MonthlyReportLabel.Text =
                    SessionDto.DepartmentName + "  " +
                    LastDayNullableDateTimePicker.Text + "月度月報";

                ManagerNameLabel.Text = "";
            }
            else
            {
                //課
                MonthlyReportLabel.Text =
                    SessionDto.DepartmentName + "  " +
                    SectionList[AffiliationComboBox.SelectedIndex].SECTION_NAME + "  " +
                    LastDayNullableDateTimePicker.Text + "月度月報";

                //自担当名を表示
                ManagerNameLabel.Text = SessionDto.SectionGroupName;
            }
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
            //部の場合、
            if (SumCheckBox.Checked)
            {
                //部長名設定ダイアログを表示
                //ManagerNameForm
                ManagerNameForm managerNameForm = new ManagerNameForm();
                managerNameForm.DepartmentID = this.selectDepartmentID;
                DialogResult result = managerNameForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ManagerNameLabel.Text = managerNameForm.ManagerName;
                    managerNameForm.Dispose();

                    //部長名登録
                    EnterManagerName();
                }
                else
                {
                    managerNameForm.Dispose();
                    return;
                }
            }

            //ファイル保存ダイアログ表示
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = (MonthlyReportLabel.Text.Replace("  ", "_")).Replace("/", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "PDF(*.pdf)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //タイトルを取得
                TitileName = MonthlyReportLabel.Text.Trim();
                TitleManagerName = ManagerNameLabel.Text.Trim();

                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName);

                //PDF出力
                OutputPDF(sfd.FileName);
            }

            sfd.Dispose();
        }
        #endregion

        #region PDF出力イベント処理
        /// <summary>
        /// PDF出力イベント処理
        /// </summary>
        public class pdfPage : iTextSharp.text.pdf.PdfPageEventHelper
        {
            public PdfPTable tbl;

            /// <summary>
            /// 改ページイベント
            /// </summary>
            /// <param name="writer"></param>
            /// <param name="doc"></param>
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                PdfContentByte pdfContentByte = writer.DirectContent;

                string fontFolder = Environment.SystemDirectory.Replace("system32", "fonts");
                string fontName = fontFolder + "\\msgothic.ttc,0";

                BaseFont baseFont = BaseFont.CreateFont(fontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                pdfContentByte.SetFontAndSize(baseFont, 10.0f);

                //ヘッダー出力
                pdfContentByte.BeginText();
                //月報タイトル
                pdfContentByte.ShowTextAligned(1, TitileName, (PageSize.A4.Width / 2), PageSize.A4.Height - 30, 0);
                //担当/部長名
                pdfContentByte.ShowTextAligned(2, TitleManagerName, doc.Right, PageSize.A4.Height - 30, 0);

                //ページ番号
                pdfContentByte.ShowTextAligned(1, writer.PageNumber.ToString(), (PageSize.A4.Width / 2), PageSize.A4.Bottom + (doc.BottomMargin / 2), 0);

                //出力終了
                pdfContentByte.EndText();
            }
        }
        #endregion

        #region 表出力
        /// <summary>
        /// 表出力
        /// </summary>
        /// <param name="pdfPath"></param>
        public void OutputPDF(string pdfPath)
        {
            //ファイル形式設定
            Document document = new Document(PageSize.A4);
            document.SetMargins(document.LeftMargin, document.RightMargin, 40, document.BottomMargin);

            try
            {
                //ファイル出力用のストリームを取得
                PdfWriter pw = PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));

                //イベント設定
                pdfPage page = new pdfPage();
                pw.PageEvent = page;

                //本文用のフォント
                FontFactory.RegisterDirectory(Environment.SystemDirectory.Replace("system32", "fonts"));
                iTextSharp.text.Font fnt =
                    FontFactory.GetFont("ＭＳ ゴシック",
                    BaseFont.IDENTITY_H,
                    BaseFont.NOT_EMBEDDED,
                    10.0f,
                    iTextSharp.text.Font.NORMAL,
                    BaseColor.BLACK);

                //文章の出力を開始
                document.Open();

                //カラムの割合を定義
                float[] headerwodth = new float[] { 0.20f, 0.40f, 0.40f };
                PdfPTable tbl = new PdfPTable(headerwodth);

                //ヘッダ行数を設定
                tbl.HeaderRows = 1;

                //イベント処理でも使用できるように設定
                page.tbl = tbl;

                //テーブル配置設定
                tbl.WidthPercentage = 100;
                tbl.DefaultCell.Padding = 4;

                //ヘッダ行
                List<PdfPCell> columnName = getHederColumnName();
                foreach (PdfPCell pdfCell in columnName)
                {
                    tbl.AddCell(pdfCell);
                }

                //データ行
                PdfPCell cell;
                foreach (DataGridViewRow row in MonthlyReportDataGridView.Rows)
                {
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["Category"].Value == null ?  " " : row.Cells["Category"].Value), fnt));
                    tbl.AddCell(cell);
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["CurrentSituation"].Value == null ? " " : row.Cells["CurrentSituation"].Value), fnt));
                    tbl.AddCell(cell);
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["FutureSchedule"].Value == null ? " " : row.Cells["FutureSchedule"].Value), fnt));
                    tbl.AddCell(cell);
                }

                //ファイルに出力
                document.Add(tbl);
            }
            catch { }
            finally
            {
                //ドキュメントを閉じる
                try
                {
                    document.Close();
                }
                catch { };
            }
        }
        #endregion

        #region ヘッダ情報
        /// <summary>
        /// ヘッダ情報
        /// </summary>
        /// <returns></returns>
        private List<PdfPCell> getHederColumnName()
        {
            //ヘッダ用のフォント
            iTextSharp.text.Font fnt =
                FontFactory.GetFont("ＭＳ ゴシック",
                BaseFont.IDENTITY_H,            //横書き
                BaseFont.NOT_EMBEDDED,          //フォントを組み込まない
                10.0f,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK);

            List<PdfPCell> columnName = new List<PdfPCell>();

            PdfPCell cell = new PdfPCell(new Phrase(MonthlyReportDataGridView.Columns["Category"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(MonthlyReportDataGridView.Columns["CurrentSituation"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(MonthlyReportDataGridView.Columns["FutureSchedule"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            return columnName;
        }
        #endregion

        #region セル値取得
        /// <summary>
        /// セル値取得
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string GetCellValue(object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 登録処理（部長名）
        /// <summary>
        /// 登録処理（部長名）
        /// </summary>
        private void EnterManagerName()
        {
            //ソート番号を振り直す
            SetSortNo();

            List<MonthlyReportModel> deleteList = new List<MonthlyReportModel>();

            //削除されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.CompareDataGridView.Rows.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < this.MonthlyReportDataGridView.Rows.Count; j++)
                {
                    if (CompareDataGridView.Rows[i].Cells["IDCompare"].Value.Equals(MonthlyReportDataGridView.Rows[j].Cells["ID"].Value))
                    {
                        flag = true;
                        break;
                    }
                }

                //削除されている場合は週報IDをリストに追加
                if (flag == false)
                {
                    //パラメータ設定
                    var itemCond = new MonthlyReportModel
                    {
                        //ID
                        ID = Convert.ToInt32(CompareDataGridView.Rows[i].Cells["IDCompare"].Value.ToString()),
                    };

                    //リストに追加
                    deleteList.Add(itemCond);
                }
            }

            List<MonthlyReportModel> postList = new List<MonthlyReportModel>();
            List<MonthlyReportModel> putList = new List<MonthlyReportModel>();

            //追加、変更されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.MonthlyReportDataGridView.Rows.Count; i++)
            {
                if (MonthlyReportDataGridView.Rows[i].Cells["ID"].Value == null)
                {
                    //追加

                    //パラメータ設定
                    var itemCond = new MonthlyReportModel
                    {
                        //部ID
                        DEPARTMENT_ID = this.selectDepartmentID,

                        //課ID
                        SECTION_ID = this.selectSectionID,

                        //項目
                        GENERAL_CODE = MonthlyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                        //現状
                        CURRENT_SITUATION = MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                        //今後の予定
                        FUTURE_SCHEDULE = MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                        //ソート番号
                        SORT_NO = MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),

                        //対象月
                        MONTH_FIRST_DAY = GetMonthFirstDay(),

                        //部長名
                        MANAGER_NAME = ManagerNameLabel.Text,
                    };

                    //リストに追加
                    postList.Add(itemCond);
                }
                else
                {
                    //変更

                    //パラメータ設定
                    var itemCond = new MonthlyReportModel
                    {
                        //ID
                        ID = Convert.ToInt32(MonthlyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()),

                        //項目
                        GENERAL_CODE = MonthlyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                        //現状
                        CURRENT_SITUATION = MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                        //今後の予定
                        FUTURE_SCHEDULE = MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : MonthlyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                        //ソート番号
                        SORT_NO = MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(MonthlyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),

                        //部長名
                        MANAGER_NAME = ManagerNameLabel.Text,
                    };

                    //リストに追加
                    putList.Add(itemCond);
                }
            }

            //更新がなければ終了
            if ((deleteList.Count == 0) && (putList.Count == 0) && (postList.Count == 0))
            {
                return;
            }

            //DELETE実行
            if (0 < deleteList.Count)
            {
                var res = HttpUtil.DeleteResponse<MonthlyReportModel>(ControllerType.MonthlyReport, deleteList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //PUT実行
            if (0 < putList.Count)
            {
                var res = HttpUtil.PutResponse<MonthlyReportModel>(ControllerType.MonthlyReport, putList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //POST実行
            if (0 < postList.Count)
            {
                var res = HttpUtil.PostResponse<MonthlyReportModel>(ControllerType.MonthlyReport, postList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //比較用リストの更新
            CompareDataGridView.Rows.Clear();
            foreach (DataGridViewRow item in MonthlyReportDataGridView.Rows)
            {
                //比較用
                CompareDataGridView.Rows.Add();
                int idx = CompareDataGridView.Rows.Count - 1;
                CompareDataGridView.Rows[idx].Cells["CategoryCompare"].Value = item.Cells["Category"].Value;
                CompareDataGridView.Rows[idx].Cells["CurrentSituationCompare"].Value = item.Cells["CurrentSituation"].Value;
                CompareDataGridView.Rows[idx].Cells["FutureScheduleCompare"].Value = item.Cells["FutureSchedule"].Value;
                CompareDataGridView.Rows[idx].Cells["IDCompare"].Value = item.Cells["ID"].Value;
                CompareDataGridView.Rows[idx].Cells["SortNoCompare"].Value = item.Cells["SortNo"].Value;
            }

            return;
        }
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //変更登録チェック
            SaveItems();
        }
        #endregion
    }
}