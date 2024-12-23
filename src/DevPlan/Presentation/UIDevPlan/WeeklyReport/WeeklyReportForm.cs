using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UIDevPlan.ReportMaterial;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

namespace DevPlan.Presentation.UIDevPlan.WeeklyReport
{
    public partial class WeeklyReportForm : BaseForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "週報"; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }
        #endregion

        #region 内部変数
        /// <summary>
        /// 承認ボタン表示文字列
        /// </summary>
        private const string APPROVE_BUTTON = "承認";

        /// <summary>
        /// 承認解除ボタン表示文字列
        /// </summary>
        private const string APPROVE_CLEAR_BUTTON = "承認解除";

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
        /// 部リスト
        /// </summary>
        List<DepartmentModel> DepartmentList = new List<DepartmentModel>();

        /// <summary>
        /// グリッド選択行
        /// </summary>
        private int selectIndex = 0;

        /// <summary>
        /// グリッド挿入行
        /// </summary>
        private int insertIndex = 0;

        /// <summary>
        /// 週報タイトル
        /// </summary>
        private static string TitileName = "";
        
        /// <summary>
        /// 期間
        /// </summary>
        private static string TitleDate = "";

        /// <summary>
        /// 選択した種別
        /// </summary>
        private string SelectedStatus = "";

        /// <summary>
        /// 選択した日付
        /// </summary>
        private DateTime? SelectedDate = new DateTime();

        /// <summary>
        /// 日付印画像
        /// </summary>
        private const string SEAL_FILE_NAME = "\\SealPanel.png";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WeeklyReportForm()
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
        private void WeeklyReportForm_Load(object sender, EventArgs e)
        {
            //権限
            this.UserAuthority = base.GetFunction(FunctionID.WeeklyReport);

            //画面初期化
            this.Init();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void Init()
        {
            //部署プルダウンにログインユーザの課を表示
            AffiliationComboBox.Items.Add(SessionDto.SectionCode);
            AffiliationComboBox.SelectedIndex = 0;

            //初期表示の部、課を設定
            SetSelectID(SessionDto.DepartmentID, SessionDto.SectionID, null);

            //初期表示項目検索
            SearchItems(true);

            //日付印
            SetSeal();

            //出力権限のないログインユーザの場合、ダウンロードボタンは表示しない。
            if (this.UserAuthority.EXPORT_FLG != '1')
            {
                DownloadButton.Visible = false;
            }

            //管理権限のないログインユーザの場合、承認/承認解除ボタンは表示しない。
            if (this.UserAuthority.MANAGEMENT_FLG != '1')
            {
                ApproveButton.Visible = false;
            }

            //更新権限のないログインユーザの場合、登録ボタンは表示しない。
            if (this.UserAuthority.UPDATE_FLG != '1')
            {
                EntryButton.Visible = false;
            }


            //帳票タイトルを変更
            WeeklyReportLabel.Text = SessionDto.DepartmentName + "  " + SessionDto.SectionName + "  週報";

            //開始日に終了日の６日前を設定
            SetFirstDay();

            //選択した種別
            SelectedStatus = SectionRadioButton.Tag.ToString();
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
            if (LastDayNullableDateTimePicker.Value == null)
            {
                Messenger.Warn(string.Format(Resources.KKM00001, "期間"));
                return;
            }

            //情報元一覧画面を表示
            ReportMaterialForm infoListForm = new ReportMaterialForm();
            infoListForm.FirstDay = (DateTime)DateTimeUtil.ConvertDateStringToDateTime(FirstDayTextBox.Text);
            infoListForm.LastDay = (DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);
            infoListForm.ShowDialog();

            //情報元一覧画面で選択した項目を最終行に追加
            List<InfoListModel> returnList = infoListForm.ReturnList;

            foreach (InfoListModel info in infoListForm.ReturnList)
            {
                WeeklyReportDataGridView.Rows.Add();
                int idx = WeeklyReportDataGridView.Rows.Count - 1;
                WeeklyReportDataGridView.Rows[idx].Cells["SectionGroupCode"].Value = info.SECTION_GROUP_CODE;
                WeeklyReportDataGridView.Rows[idx].Cells["GeneralCode"].Value = info.GENERAL_CODE;
                WeeklyReportDataGridView.Rows[idx].Cells["Category"].Value = info.CATEGORY;
                WeeklyReportDataGridView.Rows[idx].Cells["CurrentSituation"].Value = info.CURRENT_SITUATION;
                WeeklyReportDataGridView.Rows[idx].Cells["FutureSchedule"].Value = info.FUTURE_SCHEDULE;
            }

            infoListForm.Dispose();

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
            if (WeeklyReportDataGridView.Rows.Count == 0)
            {
                return;
            }

            //削除確認メッセージ表示
            DialogResult result = Messenger.Confirm(Resources.KKM00008);
            if (result == DialogResult.Yes)
            {
                if (WeeklyReportDataGridView.SelectedRows.Count == 0)
                {
                    //選択行がない場合、１行目を削除
                    WeeklyReportDataGridView.Rows.RemoveAt(0);
                }
                else
                {
                    //選択されている行を削除
                    foreach (DataGridViewRow item in WeeklyReportDataGridView.SelectedRows)
                    {
                        if (!item.IsNewRow)
                        {
                            WeeklyReportDataGridView.Rows.Remove(item);
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
            FormControlUtil.FormWait(this, () =>
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
            });
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

        #region ソート番号設定
        /// <summary>
        /// ソート番号設定
        /// </summary>
        private void SetSortNo()
        {
            //ソート番号を振り直す
            for (int i = 0; i < this.WeeklyReportDataGridView.Rows.Count; i++)
            {
                WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value = (float)i;
            }
        }
        #endregion

        /// <summary>
        /// 変更登録チェック
        /// </summary>
        private void SaveItems()
        {
            //登録ボタンが非表示の場合、権限がないので以下のチェック、登録は行わない。
            if(EntryButton.Visible == false)
            {
                return;
            }

            //変更があるかチェック
            bool flag = false;
            if (this.CompareDataGridView.Rows.Count != this.WeeklyReportDataGridView.Rows.Count)
            {
                //件数が増減しているので変更有
                flag = true;
            }

            if (flag == false)
            {
                for (int i = 0; i < this.CompareDataGridView.Rows.Count; i++)
                {
                    //比較用と画面表示をチェック
                    if (WeeklyReportDataGridView.Rows[i].Cells["SectionGroupCode"].Value !=
                            CompareDataGridView.Rows[i].Cells["SectionGroupCodeCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (WeeklyReportDataGridView.Rows[i].Cells["GeneralCode"].Value !=
                            CompareDataGridView.Rows[i].Cells["GeneralCodeCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (WeeklyReportDataGridView.Rows[i].Cells["Category"].Value !=
                            CompareDataGridView.Rows[i].Cells["CategoryCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value !=
                            CompareDataGridView.Rows[i].Cells["CurrentSituationCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value !=
                            CompareDataGridView.Rows[i].Cells["FutureScheduleCompare"].Value)
                    {
                        flag = true;
                        break;
                    }
                    if (Convert.ToInt64(WeeklyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()) !=
                            Convert.ToInt64(CompareDataGridView.Rows[i].Cells["IDCompare"].Value.ToString()))
                    {
                        flag = true;
                        break;
                    }
                    if (Convert.ToSingle(WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()) !=
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
            if(result == DialogResult.No)
            {
                return;
            }

            //登録処理
            bool enter = EnterItems();

            if (enter)
            {
                //表示項目検索
                //SearchItems();

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

            List<WeeklyReportModel> deleteList = new List<WeeklyReportModel>();

            //削除されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.CompareDataGridView.Rows.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < this.WeeklyReportDataGridView.Rows.Count; j++)
                {
                    if (CompareDataGridView.Rows[i].Cells["IDCompare"].Value.Equals(WeeklyReportDataGridView.Rows[j].Cells["ID"].Value))
                    {
                        flag = true;
                        break;
                    }
                }

                //削除されている場合は週報IDをリストに追加
                if (flag == false)
                {
                    //パラメータ設定
                    var itemCond = new WeeklyReportModel
                    {
                        //ID
                        ID = Convert.ToInt32(CompareDataGridView.Rows[i].Cells["IDCompare"].Value.ToString()),
                    };

                    //リストに追加
                    deleteList.Add(itemCond);
                }
            }

            List<WeeklyReportModel> postList = new List<WeeklyReportModel>();
            List<WeeklyReportModel> putList = new List<WeeklyReportModel>();

            //追加、変更されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.WeeklyReportDataGridView.Rows.Count; i++)
            {
                if (WeeklyReportDataGridView.Rows[i].Cells["ID"].Value == null)
                {
                    //追加

                    //パラメータ設定
                    var itemCond = new WeeklyReportModel
                    {
                        //期間
                        WEEKEND_DATE = SelectedDate,

                        //作成単位
                        作成単位 = SelectedStatus,

                        //部ID
                        DEPARTMENT_ID = selectDepartmentID,

                        //課ID
                        SECTION_ID = selectSectionID,

                        //担当ID
                        SECTION_GROUP_ID = selectSectionGroupID,

                        //担当
                        SECTION_GROUP_CODE_情報元 = WeeklyReportDataGridView.Rows[i].Cells["SectionGroupCode"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["SectionGroupCode"].Value.ToString(),

                        //開発符号
                        GENERAL_CODE = WeeklyReportDataGridView.Rows[i].Cells["GeneralCode"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["GeneralCode"].Value.ToString(),

                        //項目
                        CATEGORY = WeeklyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                        //現状
                        CURRENT_SITUATION = WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                        //今後の予定
                        FUTURE_SCHEDULE = WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                        //ソート番号
                        SORT_NO = WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),
                    };

                    //リストに追加
                    postList.Add(itemCond);
                }
                else
                {
                    for (int j = 0; j < this.CompareDataGridView.Rows.Count; j++)
                    {
                        if ((Convert.ToInt64(WeeklyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()) ==
                                Convert.ToInt64(CompareDataGridView.Rows[j].Cells["IDCompare"].Value.ToString())) &&
                            ((WeeklyReportDataGridView.Rows[i].Cells["SectionGroupCode"].Value != CompareDataGridView.Rows[j].Cells["SectionGroupCodeCompare"].Value) ||
                            (WeeklyReportDataGridView.Rows[i].Cells["GeneralCode"].Value != CompareDataGridView.Rows[j].Cells["GeneralCodeCompare"].Value) ||
                            (WeeklyReportDataGridView.Rows[i].Cells["Category"].Value != CompareDataGridView.Rows[j].Cells["CategoryCompare"].Value) ||
                            (WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value != CompareDataGridView.Rows[j].Cells["CurrentSituationCompare"].Value) ||
                            (WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value != CompareDataGridView.Rows[j].Cells["FutureScheduleCompare"].Value) ||
                            ((Convert.ToInt64(WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value) !=
                                Convert.ToInt64(CompareDataGridView.Rows[j].Cells["SortNoCompare"].Value)))))
                        {
                            //変更

                            //パラメータ設定
                            var itemCond = new WeeklyReportModel
                            {
                                //ID
                                ID = Convert.ToInt64(WeeklyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()),

                                //項目
                                CATEGORY = WeeklyReportDataGridView.Rows[i].Cells["Category"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["Category"].Value.ToString(),

                                //現状
                                CURRENT_SITUATION = WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["CurrentSituation"].Value.ToString(),

                                //今後の予定
                                FUTURE_SCHEDULE = WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value == null ? null : WeeklyReportDataGridView.Rows[i].Cells["FutureSchedule"].Value.ToString(),

                                //ソート番号
                                SORT_NO = WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value == null ? 0 : Convert.ToSingle(WeeklyReportDataGridView.Rows[i].Cells["SortNo"].Value.ToString()),
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
                var res = HttpUtil.DeleteResponse<WeeklyReportModel>(ControllerType.WeeklyReport, deleteList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //PUT実行
            if (0 < putList.Count)
            {
                var res = HttpUtil.PutResponse<WeeklyReportModel>(ControllerType.WeeklyReport, putList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //POST実行
            if (0 < postList.Count)
            {
                var res = HttpUtil.PostResponse<WeeklyReportModel>(ControllerType.WeeklyReport, postList);
                if (res == null || res.Status != Const.StatusSuccess)
                { /*失敗しても以下を継続 */ }
            }

            //比較用リストの更新
            CompareDataGridView.Rows.Clear();
            foreach (DataGridViewRow item in WeeklyReportDataGridView.Rows)
            {
                //比較用
                CompareDataGridView.Rows.Add();
                int idx = CompareDataGridView.Rows.Count - 1;
                CompareDataGridView.Rows[idx].Cells["SectionGroupCodeCompare"].Value = item.Cells["SectionGroupCode"].Value;
                CompareDataGridView.Rows[idx].Cells["GeneralCodeCompare"].Value = item.Cells["GeneralCode"].Value;
                CompareDataGridView.Rows[idx].Cells["CategoryCompare"].Value = item.Cells["Category"].Value;
                CompareDataGridView.Rows[idx].Cells["CurrentSituationCompare"].Value = item.Cells["CurrentSituation"].Value;
                CompareDataGridView.Rows[idx].Cells["FutureScheduleCompare"].Value = item.Cells["FutureSchedule"].Value;
                CompareDataGridView.Rows[idx].Cells["IDCompare"].Value = item.Cells["ID"].Value;
                CompareDataGridView.Rows[idx].Cells["SortNoCompare"].Value = item.Cells["SortNo"].Value;
                CompareDataGridView.Rows[idx].Cells["IssuedPersonelIdCompare"].Value = item.Cells["IssuedPersonelId"].Value;
                CompareDataGridView.Rows[idx].Cells["IssuedDatetimeCompare"].Value = item.Cells["IssuedDatetime"].Value;
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
            var status = "";

            DateTime? date = null;
            //日付表示
            if (dateFlag)
            {
                //終了日
                int Week = (int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Thursday;
                if (Week < 0)
                {
                    LastDayNullableDateTimePicker.Text =
                        (DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 1 -
                                                (((int)DayOfWeek.Saturday) - (int)DayOfWeek.Thursday))).ToString("yyyy/MM/dd");

                    //選択した日付を保存
                    SelectedDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 1 -
                                                (((int)DayOfWeek.Saturday) - (int)DayOfWeek.Thursday));
                }
                else
                {
                    LastDayNullableDateTimePicker.Text = (DateTime.Now.AddDays(-Week)).ToString("yyyy/MM/dd");

                    //選択した日付を保存
                    SelectedDate = DateTime.Now.AddDays(-Week);
                }
            }
            else
            {
                date = GetLastDay();
            }

            //日付印初期化
            ClearSeal();

            //開始日表示
            SetFirstDay();

            foreach (var r in this.StatusPanel.Controls)
            {
                //選択しているラジオボタンの値を取得
                if (r is RadioButton)
                {
                    var radio = r as RadioButton;
                    if (radio.Checked == true)
                    {
                        status = radio.Tag.ToString();
                        break;
                    }
                }
            }

            //パラメータ設定
            var itemCond = new WeeklyReportSearchModel
            {
                //期間
                WEEKEND_DATE = date,

                //作成単位
                作成単位 = status,

                //部ID
                DEPARTMENT_ID = this.selectDepartmentID,

                //課ID
                SECTION_ID = this.selectSectionID,

                //担当ID
                SECTION_GROUP_ID = this.selectSectionGroupID,
            };

            //承認履歴ボタン
            ApproveHistoryButton.Enabled = true;

            //Get実行
            var res = HttpUtil.GetResponse<WeeklyReportSearchModel, WeeklyReportModel>(ControllerType.WeeklyReport, itemCond);
            WeeklyReportDataGridView.Rows.Clear();
            CompareDataGridView.Rows.Clear();
            if (res == null || res.Status != Const.StatusSuccess)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
                return;
            }

            //取得した情報を画面表示
            DateTime? dt = null;
            List<WeeklyReportModel> list = (res.Results).ToList();
            foreach (WeeklyReportModel item in list)
            {
                //表示編集用
                WeeklyReportDataGridView.Rows.Add();
                int idx = WeeklyReportDataGridView.Rows.Count - 1;
                WeeklyReportDataGridView.Rows[idx].Cells["SectionGroupCode"].Value = item.SECTION_GROUP_CODE_情報元;
                WeeklyReportDataGridView.Rows[idx].Cells["GeneralCode"].Value = item.GENERAL_CODE;
                WeeklyReportDataGridView.Rows[idx].Cells["Category"].Value = item.CATEGORY;
                WeeklyReportDataGridView.Rows[idx].Cells["CurrentSituation"].Value = item.CURRENT_SITUATION;
                WeeklyReportDataGridView.Rows[idx].Cells["FutureSchedule"].Value = item.FUTURE_SCHEDULE;
                WeeklyReportDataGridView.Rows[idx].Cells["ID"].Value = item.ID;
                WeeklyReportDataGridView.Rows[idx].Cells["SortNo"].Value = item.SORT_NO;
                WeeklyReportDataGridView.Rows[idx].Cells["IssuedPersonelId"].Value = item.ISSUED_PERSONEL_ID;
                WeeklyReportDataGridView.Rows[idx].Cells["IssuedDatetime"].Value =
                    item.ISSUED_DATETIME == null ? null : ((DateTime)item.ISSUED_DATETIME).ToString("yy/MM/dd");
                dt = item.WEEKEND_DATE;

                //比較用
                CompareDataGridView.Rows.Add();
                CompareDataGridView.Rows[idx].Cells["SectionGroupCodeCompare"].Value = item.SECTION_GROUP_CODE_情報元;
                CompareDataGridView.Rows[idx].Cells["GeneralCodeCompare"].Value = item.GENERAL_CODE;
                CompareDataGridView.Rows[idx].Cells["CategoryCompare"].Value = item.CATEGORY;
                CompareDataGridView.Rows[idx].Cells["CurrentSituationCompare"].Value = item.CURRENT_SITUATION;
                CompareDataGridView.Rows[idx].Cells["FutureScheduleCompare"].Value = item.FUTURE_SCHEDULE;
                CompareDataGridView.Rows[idx].Cells["IDCompare"].Value = item.ID;
                CompareDataGridView.Rows[idx].Cells["SortNoCompare"].Value = item.SORT_NO;
                CompareDataGridView.Rows[idx].Cells["IssuedPersonelIdCompare"].Value = item.ISSUED_PERSONEL_ID;
                CompareDataGridView.Rows[idx].Cells["IssuedDatetimeCompare"].Value = 
                    item.ISSUED_DATETIME == null ? null : ((DateTime)item.ISSUED_DATETIME).ToString("yy/MM/dd");
            }

            //検索結果が１件もない場合、メッセージを表示
            if (this.WeeklyReportDataGridView.Rows.Count == 0)
            {
                SetMessageLabel(Resources.KKM00005, Color.Red);
            }
            else
            {
                //メッセージラベルを初期化
                SetMessageLabel("", Color.Black);

                //取得した日付を表示に反映
                if (dt != null)
                {
                    LastDayNullableDateTimePicker.Text = ((DateTime)dt).ToString("yyyy/MM/dd");

                    //取得した日付を保存
                    SelectedDate = dt;
                }

                //日付印
                SetSeal();

                //行選択を行わない
                WeeklyReportDataGridView.CurrentCell = null;
            }

            //フォーカス
            this.ActiveControl = WeeklyReportDataGridView;
        }
        #endregion

        #region カレンダー部表示
        /// <summary>
        /// カレンダー部表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_DropDown(object sender, EventArgs e)
        {
            //変更前を保存
            SelectedDate = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Value.ToString());
        }
        #endregion

        #region カレンダー部非表示
        /// <summary>
        /// カレンダー部非表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            //日付の変更があった場合表示更新
            if (SelectedDate != DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text))
            {
                //変更登録チェック
                SaveItems();
               
                //表示項目検索
                SearchItems();
            }

            //変更値を保存
            SelectedDate = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Value.ToString());
        }
        #endregion

        #region カレンダー部キーボード編集完了
        /// <summary>
        /// カレンダー部キーボード編集完了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastDayNullableDateTimePicker_Validated(object sender, EventArgs e)
        {
            //日付の変更があった場合表示更新
            if (SelectedDate != DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text))
            {
                //変更登録チェック
                SaveItems();

                //表示項目検索
                SearchItems();
            }

            //変更値を保存
            SelectedDate = DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Value.ToString());
        }
        #endregion

        #region 開始日表示設定
        /// <summary>
        /// 開始日表示設定
        /// </summary>
        private void SetFirstDay()
        {
            if (0 < LastDayNullableDateTimePicker.Text.Length)
            {
                DateTime date = (DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);

                //開始日に終了日の６日前を設定
                this.FirstDayTextBox.Text = date.AddDays(-6).ToString("yyyy/MM/dd");

                //木曜日チェック
                if (date.DayOfWeek != DayOfWeek.Thursday)
                {
                    Messenger.Info(Resources.KKM02002);
                }
            }
            else
            {
                this.FirstDayTextBox.Text = "";
            }

            WeeklyReportDateLabel.Text = "(" + FirstDayTextBox.Text + "～" + LastDayNullableDateTimePicker.Text + ")";
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

        #region 部ラジオボタン選択
        /// <summary>
        /// 部ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(DepartmentRadioButton.Checked == false)
            {
                SelectedStatus = DepartmentRadioButton.Tag.ToString();
                return;
            }

            //変更登録チェック
            SaveItems();

            //自部署の部名を追加表示
            AffiliationComboBox.Items.Clear();
            AffiliationComboBox.Items.Add(SessionDto.DepartmentCode);
            AffiliationComboBox.SelectedItem = SessionDto.DepartmentCode;

            SetSelectID(SessionDto.DepartmentID, null, null);

            //初期表示項目検索
            SearchItems();

            //帳票タイトルを変更
            WeeklyReportLabel.Text = SessionDto.DepartmentName + "  週報";

            //選択した種別
            SelectedStatus = DepartmentRadioButton.Tag.ToString();
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
                SelectedStatus = SectionRadioButton.Tag.ToString();
                return;
            }

            //変更登録チェック
            SaveItems();

            //部署コンボボックス初期化
            AffiliationComboBox.Items.Clear();

            this.WeeklyReportDataGridView.Rows.Clear();
            CompareDataGridView.Rows.Clear();

            //部署プルダウンにログインユーザの課を表示
            AffiliationComboBox.Items.Add(SessionDto.SectionCode);
            AffiliationComboBox.SelectedIndex = 0;

            SetSelectID(SessionDto.DepartmentID, SessionDto.SectionID, null);

            //初期表示項目検索
            SearchItems();

            //帳票タイトルを変更
            WeeklyReportLabel.Text = SessionDto.DepartmentName + "  " + SessionDto.SectionName + "  週報";

            //選択した種別
            SelectedStatus = SectionRadioButton.Tag.ToString();
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
                SelectedStatus = SectionGroupRadioButton.Tag.ToString();
                return;
            }

            //変更登録チェック
            SaveItems();

            //部署コンボボックス初期化
            AffiliationComboBox.Items.Clear();

            this.WeeklyReportDataGridView.Rows.Clear();
            CompareDataGridView.Rows.Clear();

            //部署プルダウンにログインユーザの課を表示
            AffiliationComboBox.Items.Add(SessionDto.SectionGroupCode);
            AffiliationComboBox.SelectedIndex = 0;

            SetSelectID(SessionDto.DepartmentID, SessionDto.SectionID, SessionDto.SectionGroupID);

            //初期表示項目検索
            SearchItems();

            //帳票タイトルを変更
            WeeklyReportLabel.Text = SessionDto.DepartmentName + "  " + SessionDto.SectionName +
                                    "  " + SessionDto.SectionGroupName + "  週報";

            //帳票タイトルを変更
            //WeeklyReportLabel.Text = "週報";

            //選択した種別
            SelectedStatus = SectionGroupRadioButton.Tag.ToString();
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
            SaveItems();

            if (this.DepartmentRadioButton.Checked)
            {
                // 部

                //帳票タイトルを変更
                WeeklyReportLabel.Text = SessionDto.DepartmentName + "  週報";

                return;
            }
            else if (this.SectionRadioButton.Checked)
            {
                // 課
                using (var form = new SectionListForm { DEPARTMENT_ID = SessionDto.DepartmentID, DEPARTMENT_COMBOBOX = false })
                {
                    // 課検索
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        //選択された課IDからリストを取得
                        var list = SearchSection(form.SECTION_ID);


                        if ((0 < list.Count) && (list[0].SECTION_CODE != null))
                        {
                            //コンボボックスの表示を更新
                            AffiliationComboBox.Items.Add(list[0].SECTION_CODE);
                            AffiliationComboBox.SelectedItem = list[0].SECTION_CODE;

                            //選択されたIDによりボタンの状態を更新
                            SetSelectID(list[0].DEPARTMENT_ID, list[0].SECTION_ID, null);

                            //帳票タイトルを変更
                            WeeklyReportLabel.Text = list[0].DEPARTMENT_NAME + "  " + list[0].SECTION_NAME + "  週報";

                            //表示項目検索
                            SearchItems();
                        }

                    }

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

                        if ((0 < list.Count) && (list[0].SECTION_GROUP_CODE != null))
                        {
                            //コンボボックスの表示を更新
                            AffiliationComboBox.Items.Add(list[0].SECTION_GROUP_CODE);
                            AffiliationComboBox.SelectedItem = list[0].SECTION_GROUP_CODE;

                            //選択されたIDによりボタンの状態を更新
                            SetSelectID(list[0].DEPARTMENT_ID, list[0].SECTION_ID, list[0].SECTION_GROUP_ID);

                            //帳票タイトルを変更
                            WeeklyReportLabel.Text = list[0].DEPARTMENT_NAME + "  " + list[0].SECTION_NAME + "  " + list[0].SECTION_GROUP_NAME + "  週報";

                            //表示項目検索
                            SearchItems();
                        }

                    }

                }

            }

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
        private DateTime? GetLastDay()
        {
            if (0 < LastDayNullableDateTimePicker.Text.Length)
            {
                return (DateTime)DateTimeUtil.ConvertDateStringToDateTime(LastDayNullableDateTimePicker.Text);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 承認ボタンクリック
        /// <summary>
        /// 承認ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApproveButton_Click(object sender, EventArgs e)
        {
            //承認(承認解除)確認メッセージ表示
            short? flag = 0; //0：承認解除、1:承認
            DialogResult result;
            if (this.ApproveButton.Text == APPROVE_BUTTON)
            {
                result = Messenger.Confirm(Resources.KKM00010);
                flag = 1;
            }
            else
            {
                result = Messenger.Confirm(Resources.KKM00012);
                flag = 0;
            }

            if (result == DialogResult.No)
            {
                //キャンセルの場合終了
                return;
            }

            //登録処理
            EnterItems();

            //表示項目検索
            SearchItems();

            List<WeeklyReportApproveModel> postList = new List<WeeklyReportApproveModel>();

            //追加、変更されている項目を検索(週報IDのリストを作成）
            for (int i = 0; i < this.WeeklyReportDataGridView.Rows.Count; i++)
            {
                var status = "";

                foreach (var r in this.StatusPanel.Controls)
                {
                    //選択しているラジオボタンの値を取得
                    if (r is RadioButton)
                    {
                        var radio = r as RadioButton;
                        if (radio.Checked == true)
                        {
                            status = radio.Tag.ToString();
                            break;
                        }
                    }
                }

                //パラメータ設定
                var itemCond = new WeeklyReportApproveModel
                {
                    //承認FLAG
                    FLAG_承認 = flag,

                    //承認者
                    承認者_PERSONEL_ID = SessionDto.UserId,

                    //週報ID
                    ID = Convert.ToInt32(WeeklyReportDataGridView.Rows[i].Cells["ID"].Value.ToString()),

                    //期間
                    WEEKEND_DATE = GetLastDay(),

                    //作成単位
                    作成単位 = status,

                    //部ID
                    DEPARTMENT_ID = selectDepartmentID,

                    //課ID
                    SECTION_ID = selectSectionID,

                    //担当ID
                    SECTION_GROUP_ID = selectSectionGroupID,
                };

                //リストに追加
                postList.Add(itemCond);
            }

            if (0 < postList.Count)
            {
                //承認登録実行
                var res = HttpUtil.PostResponse<WeeklyReportApproveModel>(ControllerType.WeeklyReportApproval, postList);

                if (res != null && res.Status == Const.StatusSuccess)
                {
                    //承認登録が正常の場合、処理結果メッセージ表示

                    //表示項目検索
                    SearchItems();

                    if (this.ApproveButton.Text == APPROVE_BUTTON)
                    {
                    }
                    else
                    {
                        SetMessageLabel(Resources.KKM00011, Color.Black);
                    }
                }
            }
        }
        #endregion

        #region 承認履歴ボタンクリック
        /// <summary>
        /// 承認履歴ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApproveHistoryButton_Click(object sender, EventArgs e)
        {
            //承認履歴ダイアログ表示
            WeeklyReportApprovalForm ApproveForm = new WeeklyReportApprovalForm();
            ApproveForm.WEEKEND_DATE = (DateTime)GetLastDay();

            if (this.DepartmentRadioButton.Checked)
            {
                // 部
                ApproveForm.DEPARTMENT_ID = this.selectDepartmentID;
                ApproveForm.SECTION_ID = null;
                ApproveForm.SECTION_GROUP_ID = null;
            }
            else if (this.SectionRadioButton.Checked)
            {
                // 課
                ApproveForm.DEPARTMENT_ID = null;
                ApproveForm.SECTION_ID = this.selectSectionID;
                ApproveForm.SECTION_GROUP_ID = null;
            }
            else if (this.SectionGroupRadioButton.Checked)
            {
                //担当
                ApproveForm.DEPARTMENT_ID = null;
                ApproveForm.SECTION_ID = null;
                ApproveForm.SECTION_GROUP_ID = this.selectSectionGroupID;
            }

            //表示
            ApproveForm.ShowDialog();
            ApproveForm.Dispose();
        }
        #endregion

        #region 日付印表示
        /// <summary>
        /// 日付印表示
        /// </summary>
        /// <param name="flag"></param>
        private void SetSeal()
        {
            //表示なし
            if(WeeklyReportDataGridView.Rows.Count == 0)
            {
                //日付印非表示
                ClearSeal();

                return;
            }

            if (WeeklyReportDataGridView.Rows[0].Cells["IssuedDatetime"].Value != null)
            {
                //日付印表示
                SealPictureBox.Image = Resources.DateStamp;

                DateTextBox.Text = WeeklyReportDataGridView.Rows[0].Cells["IssuedDatetime"].Value.ToString();
                //DateTextBox.Location = new Point(
                //    SealPictureBox.Location.X + (SealPictureBox.Size.Width / 2) - (DateTextBox.Size.Width / 2),
                //    SealPictureBox.Location.Y + (SealPictureBox.Size.Height / 2) - (DateTextBox.Size.Height / 2));
                DateTextBox.Visible = true;

                //承認者IDからユーザー情報を取得
                var itemCond = new UserSearchModel
                {
                    // 部コード
                    PERSONEL_ID = WeeklyReportDataGridView.Rows[0].Cells["IssuedPersonelId"].Value.ToString(),
                };

                // Get実行
                var res = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>(ControllerType.User, itemCond);
                List<UserSearchOutModel> userList = res.Results.ToList();

                if (userList != null && 0 < userList.Count)
                {
                    //所属
                    if (this.DepartmentRadioButton.Checked)
                    {
                        // 部
                        SectionTextBox.Text = userList[0].DEPARTMENT_CODE;
                    }
                    else if (this.SectionRadioButton.Checked)
                    {
                        // 課
                        SectionTextBox.Text = userList[0].SECTION_CODE;
                    }
                    else if (this.SectionGroupRadioButton.Checked)
                    {
                        //担当
                        SectionTextBox.Text = userList[0].SECTION_GROUP_CODE;
                    }

                    //SectionTextBox.Location = new Point(
                    //SealPictureBox.Location.X + (SealPictureBox.Size.Width / 2) - (SectionTextBox.Size.Width / 2),
                    //SealPictureBox.Location.Y + (SealPictureBox.Size.Height / 4) - (SectionTextBox.Size.Height / 2));
                    SectionTextBox.Visible = true;



                    //承認者苗字（全角スペースで区切られている前提）
                    string[] names = userList[0].NAME.Split('　');

                    if(0 < names[0].Length)
                    {
                        if(4 < names[0].Length)
                        {
                            NameTextBox.Text = names[0].Substring(0, 4);
                        }
                        else
                        {
                            NameTextBox.Text = names[0];
                        }
                    }

                    //NameTextBox.Location = new Point(
                    //SealPictureBox.Location.X + (SealPictureBox.Size.Width / 2) - (SectionTextBox.Size.Width / 2),
                    //SealPictureBox.Location.Y + (SealPictureBox.Size.Height / 0.5) - (SectionTextBox.Size.Height / 2));
                    NameTextBox.Visible = true;

                }



                ApproveButton.Text = APPROVE_CLEAR_BUTTON;
                SearchButton.Enabled = false;
                DeleteButton.Enabled = false;
                //WeeklyReportDataGridView.Enabled = false;
                WeeklyReportDataGridView.Columns["Category"].ReadOnly = true;
                WeeklyReportDataGridView.Columns["CurrentSituation"].ReadOnly = true;
                WeeklyReportDataGridView.Columns["FutureSchedule"].ReadOnly = true;
                EntryButton.Enabled = false;
            }
            else
            {
                //日付印非表示
                ClearSeal();
            }
        }
        #endregion

        #region 日付印初期化
        /// <summary>
        /// 日付印初期化
        /// </summary>
        private void ClearSeal()
        {
            this.SealPictureBox.Image = null;
            DateTextBox.Visible = false;
            SectionTextBox.Visible = false;
            NameTextBox.Visible = false;
            ApproveButton.Text = APPROVE_BUTTON;
            SearchButton.Enabled = true;
            DeleteButton.Enabled = true;
            //WeeklyReportDataGridView.Enabled = true;
            WeeklyReportDataGridView.Columns["Category"].ReadOnly = false;
            WeeklyReportDataGridView.Columns["CurrentSituation"].ReadOnly = false;
            WeeklyReportDataGridView.Columns["FutureSchedule"].ReadOnly = false;
            EntryButton.Enabled = true;
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
            DataGridView.HitTestInfo hit = WeeklyReportDataGridView.HitTest(e.X, e.Y);
            if (hit.Type != DataGridViewHitTestType.Cell)
            {
                //セル以外は終了
                return;
            }

            if (e.Clicks == 2)
            {
                //承認中の場合、編集は不可
                if (e.Button == MouseButtons.Left && this.ApproveButton.Text == APPROVE_BUTTON)
                {
                    //ダブルクリックの場合はセルを編集状態とする
                    WeeklyReportDataGridView.BeginEdit(true);
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
                DestinationRow.CreateCells(WeeklyReportDataGridView);  // 複写先DataGridView指定

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
            Point clientPoint = WeeklyReportDataGridView.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit =
                WeeklyReportDataGridView.HitTest(clientPoint.X, clientPoint.Y);

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
                    WeeklyReportDataGridView.Rows.Insert(hit.RowIndex, Row_Work);

                    // 追加した行を選択状態にする。
                    WeeklyReportDataGridView.Rows[hit.RowIndex].Selected = true;

                    if (selectIndex < hit.RowIndex)
                    {
                        WeeklyReportDataGridView.Rows.RemoveAt(selectIndex);
                        insertIndex = hit.RowIndex - 1;
                    }
                    else
                    {
                        WeeklyReportDataGridView.Rows.RemoveAt(selectIndex + 1);
                        insertIndex = hit.RowIndex;
                    }
                }
                // ドロップ先としての指定位置が、有効でない場合（x,y座標値の取得に失敗した場合）
                else
                {
                    // 行データをdataGridView2の末尾に追加
                    WeeklyReportDataGridView.Rows.Add(Row_Work);

                    // 追加した行を選択状態にする。
                    WeeklyReportDataGridView.Rows.RemoveAt(selectIndex);

                    insertIndex = WeeklyReportDataGridView.Rows.Count - 1;
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
            SetGrid(WeeklyReportDataGridView);
        }
        #endregion

        #region 行選択指定
        /// <summary>
        /// 行選択指定
        /// </summary>
        /// <param name="text"></param>
        private void SetGrid(DataGridView text)
        {
            if (this.WeeklyReportDataGridView.InvokeRequired)
            {
                SetGridCallback callBack = new SetGridCallback(SetGrid);
                this.Invoke(callBack, new object[] { text });
            }
            else
            {
                //行選択
                WeeklyReportDataGridView.Rows[insertIndex].Selected = true;
                WeeklyReportDataGridView.Refresh();
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

            if (hit.Type == DataGridViewHitTestType.Cell && (WeeklyReportDataGridView.NewRowIndex == -1
                    || WeeklyReportDataGridView.NewRowIndex != hit.RowIndex))
            {
                // ドラッグ元としての指定位置が、有効なセル上を選択している場合
                // 複写元となる行データー（選択されている行データー）
                SourceRow = WeeklyReportDataGridView.Rows[hit.RowIndex];
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
        private void WeeklyReportForm_Shown(object sender, EventArgs e)
        {
            WeeklyReportDataGridView.CurrentCell = null;
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
            //タイトルを取得
            TitileName = WeeklyReportLabel.Text.Trim();
            TitleDate = WeeklyReportDateLabel.Text.Trim();

            //ファイル保存ダイアログ表示
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = (WeeklyReportLabel.Text.Replace("  ", "_")).Replace("/", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";
            sfd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.Filter = "PDF(*.pdf)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //日付印を画像ファイルに保存
                CaptureControl();

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
                //週報タイトル
                pdfContentByte.ShowTextAligned(1, TitileName, (PageSize.A4.Width / 2), PageSize.A4.Height - 30, 0);
                //期間
                pdfContentByte.ShowTextAligned(1, TitleDate, (PageSize.A4.Width / 2), PageSize.A4.Height - 50, 0);
                //ページ番号
                pdfContentByte.ShowTextAligned(1, writer.PageNumber.ToString(), (PageSize.A4.Width / 2), PageSize.A4.Bottom + (doc.BottomMargin / 2), 0);

                //出力終了
                pdfContentByte.EndText();
            }
        }
        #endregion

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest,
             int nXDest, int nYDest, int nWidth, int nHeight,
             IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        private const int SRCCOPY = 0xCC0020;

        /// <summary>
        /// コントロールのイメージを取得する
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public void CaptureControl()
        {
            Graphics g = SealPanel.CreateGraphics();
            Bitmap img = new Bitmap(SealPanel.ClientRectangle.Width,
                SealPanel.ClientRectangle.Height, g);
            Graphics memg = Graphics.FromImage(img);
            IntPtr dc1 = g.GetHdc();
            IntPtr dc2 = memg.GetHdc();
            BitBlt(dc2, 0, 0, img.Width, img.Height, dc1, 0, 0, SRCCOPY);
            g.ReleaseHdc(dc1);
            memg.ReleaseHdc(dc2);
            memg.Dispose();
            g.Dispose();
            img.Save(System.Environment.GetFolderPath(Environment.SpecialFolder.Templates) + SEAL_FILE_NAME);
            img.Dispose();
        }

        #region 表出力
        /// <summary>
        /// 表出力
        /// </summary>
        /// <param name="pdfPath"></param>
        public void OutputPDF(string pdfPath)
        {
            //ファイル形式設定
            Document document = new Document(PageSize.A4);
            document.SetMargins(document.LeftMargin, document.RightMargin, 80, document.BottomMargin);

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
                float[] headerwodth = new float[] { 0.10f, 0.10f, 0.20f, 0.30f, 0.30f };
                PdfPTable tbl = new PdfPTable(headerwodth);

                //ヘッダ行数を設定(1行目は日付印、2行目は列タイトル）
                tbl.HeaderRows = 2;

                //日付印の画像をセット
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(System.Environment.GetFolderPath(Environment.SpecialFolder.Templates) + SEAL_FILE_NAME);
                image.ScaleToFit(50, 50);
                image.SetAbsolutePosition(document.Right - (image.ScaledWidth), document.Top);

                // 1行目に日付印をセット
                PdfPCell celImage = new PdfPCell(image);
                celImage.Colspan = 5;
                celImage.HorizontalAlignment = Element.ALIGN_RIGHT;
                celImage.VerticalAlignment = Element.ALIGN_BOTTOM;
                celImage.Border = 0;
                tbl.AddCell(celImage);

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
                foreach (DataGridViewRow row in WeeklyReportDataGridView.Rows)
                {
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["SectionGroupCode"].Value == null ? " " : row.Cells["SectionGroupCode"].Value), fnt));
                    tbl.AddCell(cell);
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["GeneralCode"].Value == null ? " " : row.Cells["GeneralCode"].Value), fnt));
                    tbl.AddCell(cell);
                    cell = new PdfPCell(new Phrase(GetCellValue(row.Cells["Category"].Value == null ? " " : row.Cells["Category"].Value), fnt));
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

            PdfPCell cell = new PdfPCell(new Phrase(WeeklyReportDataGridView.Columns["SectionGroupCode"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(WeeklyReportDataGridView.Columns["GeneralCode"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(WeeklyReportDataGridView.Columns["Category"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(WeeklyReportDataGridView.Columns["CurrentSituation"].HeaderText, fnt));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.WHITE;
            columnName.Add(cell);

            cell = new PdfPCell(new Phrase(WeeklyReportDataGridView.Columns["FutureSchedule"].HeaderText, fnt));
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
            if(obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //変更登録チェック
            SaveItems();
        }
        #endregion
    }
}