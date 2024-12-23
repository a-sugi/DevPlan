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
    /// 情報元一覧(進捗状況/週報/月報)
    /// </summary>
    public partial class ReportMaterialListForm : BaseSubForm
    {
        #region メンバ変数
        private const　string SakuseiTani = "課";
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "情報元一覧(進捗状況/週報/月報)"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>検討会資料詳細リスト</summary>
        public List<MeetingDocumentDetailModel> MeetingDocumentDetailList { get; private set; } = new List<MeetingDocumentDetailModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReportMaterialListForm()
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
        private void ReportMaterialListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //情報元一覧の設定
            this.ReportMaterialDataGridView.AutoGenerateColumns = false;

            //期間(開始)
            this.PeriodDateTimePicker.Value = DateTime.Today.AddMonths(-1).AddDays(1);

        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportMaterialListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ReportMaterialDataGridView.CurrentCell = null;

        }
        #endregion

        #region 検索ボタン
        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            //情報元一覧設定
            this.SetReportMaterialList();

        }
        #endregion

        #region 全選択変更
        /// <summary>
        /// 全選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var flg = this.AllSelectCheckBox.Checked;

            //全ての行の選択を設定
            foreach (DataGridViewRow row in this.ReportMaterialDataGridView.Rows)
            {
                row.Cells[this.SelectedColumn.Name].Value = flg;

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
            //検討会資料詳細が設定できたかどうか
            if (this.SetMeetingDocumentDetailList() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //フォームクローズ
            base.FormOkClose();

        }

        /// <summary>
        /// 検討会資料詳細設定
        /// </summary>
        /// <returns></returns>
        private bool SetMeetingDocumentDetailList()
        {
            var list = new List<MeetingDocumentDetailModel>();

            Func<DataGridViewRow, string, string> getValue = (row, name) =>
            {
                var cell = row.Cells[name];

                var value = cell.Value;

                if (string.IsNullOrEmpty(this.ReportMaterialDataGridView.Columns[name].DefaultCellStyle.Format) == false)
                {
                    value = cell.FormattedValue;

                }

                return value == null ? "" : value.ToString();

            };

            foreach (DataGridViewRow row in this.ReportMaterialDataGridView.Rows)
            {
                //選択しているかどうか
                if (Convert.ToBoolean(row.Cells[this.SelectedColumn.Name].Value) == true)
                {
                    list.Add(new MeetingDocumentDetailModel
                    {
                        //開発符号
                        GENERAL_CODE = getValue(row, this.GeneralCodeColumn.Name),

                        //課
                        試験部署_CODE = getValue(row, this.SectionColumn.Name),

                        //項目
                        CATEGORY = getValue(row, this.CategoryColumn.Name),

                        //課題
                        CURRENT_SITUATION = getValue(row, this.CurrentColumn.Name),

                        //対応状況
                        FUTURE_SCHEDULE = getValue(row, this.FutureColumn.Name)

                    });

                }

            }

            //検討会資料詳細セット
            this.MeetingDocumentDetailList = list;

            //検討会資料詳細があるかどうか
            return this.MeetingDocumentDetailList.Any();

        }
        #endregion

        #region 情報元一覧設定
        /// <summary>
        /// 情報元一覧設定
        /// </summary>
        private void SetReportMaterialList()
        {
            FormControlUtil.FormWait(this, () =>
            {
                //情報元一覧初期化
                this.ReportMaterialDataGridView.DataSource = null;

                //情報元一覧が取得できたかどうか
                var list = this.GetInfoList();
                if (list.Any() == true)
                {
                    this.ReportMaterialDataGridView.DataSource = list;

                }

                //情報元一覧のデータが取得できたかどうか
                this.ListDataLabel.Text = list.Any() == true ? "" : Resources.KKM00005;

                //一覧を未選択状態に設定
                this.ReportMaterialDataGridView.CurrentCell = null;

            });

        }
        #endregion

        #region データの取得
        /// <summary>
        /// 情報元一覧の取得
        /// </summary>
        /// <returns></returns>
        private List<InfoListModel> GetInfoList()
        {
            //パラメータ設定
            var cond = new InfoListSearchModel
            {
                //検索区分
                CLASS_DATA = int.Parse(FormControlUtil.GetRadioButtonValue(this.TypePanel)),

                //検索開始日
                FIRST_DAY = this.PeriodDateTimePicker.Value.Date,

                //検索終了日
                LAST_DAY = this.PeriodDateTimePicker.MaxDate.Date,

                //作成単位
                作成単位 = SakuseiTani,

                //部ID
                DEPARTMENT_ID = SessionDto.DepartmentID,

                //課ID
                SECTION_ID = SessionDto.SectionID,

                //担当ID
                SECTION_GROUP_ID = SessionDto.SectionGroupID,

            };

            //APIで取得
            var res = HttpUtil.GetResponse<InfoListSearchModel, InfoListModel>(ControllerType.ReportMaterial, cond);

            //レスポンスが取得できたかどうか
            var list = new List<InfoListModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

    }
}
