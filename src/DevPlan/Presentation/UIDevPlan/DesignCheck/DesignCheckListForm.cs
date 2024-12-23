using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Config;

using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 設計チェック一覧
    /// </summary>
    public partial class DesignCheckListForm : BaseForm
    {
        #region メンバ変数

        private BindingSource designCheckDataSource = new BindingSource();

        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();

        /// <summary>
        /// 権限管理クラス
        /// </summary>
        private Auth authority;

        /// <summary>
        /// バインド中可否
        /// </summary>
        private bool IsBind { get; set; } = false;

        //Update Start 2023/02/28 杉浦 ボタン押下時にPC端末を決定する
        public string PcAuthKind { get; set; }
        //Update End 2023/02/28 杉浦 ボタン押下時にPC端末を決定する

        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "設計チェック一覧"; } }

        /// <summary>検索条件</summary>
        private DesignCheckGetInModel ListSearchCond { get; set; }

        /// <summary>取得リスト</summary>
        private List<DesignCheckGetOutModel> DataList { get; set; } = new List<DesignCheckGetOutModel>();

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckListForm()
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
        private void DesignCheckListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 権限取得
                authority = new Auth(base.GetFunction(FunctionID.DesignCheck));

                //画面初期化
                this.InitForm();

                //グリッド初期化
                this.InitDataGridView();

                //詳細チェック一覧設定
                this.SetDesignCheckList();
            });
        }

        /// <summary>
        /// データグリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            var template = new DesignCheckMultiRowTemplate();

            // 権限制御
            if (!authority.IsAdmin())
            {
                var col = template.ColumnHeaders[0];
                var row = template.Row;
                var diff = col.Cells["columnHeaderCell7"].Width;

                // 設計チェック名
                col.Cells["columnHeaderCell2"].Size = new Size(col.Cells["columnHeaderCell2"].Width + diff, col.Cells["columnHeaderCell2"].Height);
                row.Cells["DesignCheckNameLinkColumn"].Size = new Size(row.Cells["DesignCheckNameLinkColumn"].Width + diff, row.Cells["DesignCheckNameLinkColumn"].Height);

                // オープン件数
                col.Cells["columnHeaderCell3"].Location = new Point(col.Cells["columnHeaderCell3"].Location.X + diff, col.Cells["columnHeaderCell3"].Location.Y);
                row.Cells["OpenCountColumn"].Location = new Point(row.Cells["OpenCountColumn"].Location.X + diff, row.Cells["OpenCountColumn"].Location.Y);

                // クローズ件数
                col.Cells["columnHeaderCell4"].Location = new Point(col.Cells["columnHeaderCell4"].Location.X + diff, col.Cells["columnHeaderCell4"].Location.Y);
                row.Cells["CloseCountColumn"].Location = new Point(row.Cells["CloseCountColumn"].Location.X + diff, row.Cells["CloseCountColumn"].Location.Y);

                // 基本情報
                col.Cells["columnHeaderCell7"].Visible = false;
                row.Cells["BaseInfoLinkColumn"].Visible = false;
            }

            // カスタムテンプレート適用
            customTemplate.RowCountLabel = this.RowCountLabel;
            customTemplate.MultiRow = this.DesignCheckMultiRow;
            this.DesignCheckMultiRow.Template = customTemplate.SetContextMenuTemplate(template);

            //データーソース
            this.DesignCheckMultiRow.DataSource = this.designCheckDataSource;
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
                // 開催日
                this.OpenStartDateTimePicker.Value = new DateTime(DateTime.Today.AddMonths(-3).Year, DateTime.Today.AddMonths(-3).Month, 1);
                this.OpenEndDateTimePicker.Value = null;

                // 設計チェック名
                this.DesignCheckNameTextBox.Text = null;

                // ステータス
                this.StatusOpenCheckBox.Checked = authority.IsAdmin() == false && authority.IsSks() == false;
                this.StatusCloseCheckBox.Checked = false;
                this.StatusCloseCheckBox.Enabled = authority.PCAuth.IsOk() == false;

                // ボタン表示制御
                this.AddButton.Visible = authority.IsVisible(this.AddButton);
                this.DeleteButton.Visible = authority.IsVisible(this.DeleteButton);
                this.DownloadButton.Visible = authority.IsVisible(this.DownloadButton);
                this.ExportButton.Visible = authority.IsVisible(this.ExportButton);

                // 説明文言
                this.ScreenFlowLabel.Text = string.Format(this.ScreenFlowLabel.Text, authority.PCAuth.IsOk() ? "設計チェック" : "指摘一覧");
            }
            finally
            {
                // バインドフラグ
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 必須条件の自動セット
        /// </summary>
        private void SetRequiredCondition()
        {
            // 開催日
            if (this.OpenStartDateTimePicker.Value == null)
            {
                this.OpenStartDateTimePicker.Value = this.ListSearchCond.OPEN_START_DATE;
            }
        }
        #endregion

        #region PC端末権限クラス
        /// <summary>
        /// PC端末権限クラス
        /// </summary>
        private class PCAuth
        {
            private bool? _IsOk;

            /// <summary>
            /// PC端末権限があるか？
            /// </summary>
            /// <returns></returns>
            public bool IsOk()
            {
                if (_IsOk == null)
                {
                    _IsOk = this.GetPCAuth()?.ID > 0;
                }

                return _IsOk.Value;
            }

            /// <summary>
            /// PC端末権限の取得
            /// </summary>
            /// <returns></returns>
            private PCAuthorityGetOutModel GetPCAuth()
            {
                var list = new List<PCAuthorityGetOutModel>();

                var cond = new PCAuthorityGetInModel
                {
                    // NetBIOS名
                    PC_NAME = Environment.MachineName,
                    // 機能ID
                    FUNCTION_ID = (int?)FunctionID.DesignCheck
                };

                //APIで取得
                var res = HttpUtil.GetResponse<PCAuthorityGetInModel, PCAuthorityGetOutModel>(ControllerType.PCAuthority, cond);

                //レスポンスが取得できたかどうか
                if (res != null && res.Status == Const.StatusSuccess)
                {
                    list.AddRange(res.Results);

                }

                return list.FirstOrDefault();
            }
        }
        #endregion

        #region 設計チェック検索のチェック
        /// <summary>
        /// 設計チェック検索のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearchDesignCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var start = this.OpenStartDateTimePicker.SelectedDate;
            var end = this.OpenEndDateTimePicker.SelectedDate;

            //空車期間の大小チェック
            map[this.OpenStartDateTimePicker] = (c, name) =>
            {
                var errMsg = "";

                //期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー
                if (start != null && end != null && start > end)
                {
                    //エラーメッセージ
                    errMsg = Resources.KKM00018;

                    //背景色を変更
                    this.OpenStartDateTimePicker.BackColor = Const.ErrorBackColor;
                    this.OpenEndDateTimePicker.BackColor = Const.ErrorBackColor;
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

            return true;
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.DesignCheckMultiRow.CurrentCell = null;

            this.ActiveControl = OpenStartDateTimePicker;
            OpenStartDateTimePicker.Focus();
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
            base.SearchConditionVisible(this.SearchConditionButton, this.SearchConditionTableLayoutPanel, this.MainPanel, new List<Control> { this.ScreenFlowLabel, this.RowCountLabel }, 130);

            //検索結果メッセージ
            this.ListDataLabel.Visible = flg;
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
            //設計チェック一覧設定
            FormControlUtil.FormWait(this, () => this.SetDesignCheckList());
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
            // 画面初期化
            this.InitForm();
        }
        #endregion

        #region 設計チェック一覧イベント
        /// <summary>
        /// 設計チェック一覧マウスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckMultiRow_CellMouseLeave(object sender, CellEventArgs e)
        {
            //ツールチップを非表示
            this.SituationToolTip.Hide(this.DesignCheckMultiRow);
        }

        /// <summary>
        /// 設計チェック一覧セルコンテンツクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // 表示リンク以外の列なら終了
            if (this.DesignCheckMultiRow.Columns[e.CellIndex].Name != "DesignCheckNameLinkColumn" &&
                this.DesignCheckMultiRow.Columns[e.CellIndex].Name != "BaseInfoLinkColumn")
            {
                return;
            }

            var row = this.DesignCheckMultiRow.Rows[e.RowIndex];

            // 設計チェック名リンク
            if (this.DesignCheckMultiRow.Columns[e.CellIndex].Name == "DesignCheckNameLinkColumn")
            {
                // 必須条件のセット
                this.SetRequiredCondition();

                //Update Start 2023/02/28 杉浦 ボタン押下時にPC端末を決定する
                // PC端末権限判定
                //if (authority.PCAuth.IsOk())
                if (PcAuthKind == "1")
                //Update End 2023/02/28 杉浦 ボタン押下時にPC端末を決定する
                {
                    // 設計チェック
                    new DesignCheckDetailListForm
                    {
                        DesignCheck = this.GetDesignCheckByDataGridView(e.RowIndex),
                        ParentFormRefresh = () => this.SetDesignCheckList(true)
                    }
                    .Show();
                }
                else
                {
                    // 指摘一覧
                    new DesignCheckPointListForm
                    {
                        DesignCheck = this.GetDesignCheckByDataGridView(e.RowIndex),
                        ParentFormRefresh = () => this.SetDesignCheckList(true)
                    }
                    .Show();
                }
            }
            // 基本情報リンク
            else if (this.DesignCheckMultiRow.Columns[e.CellIndex].Name == "BaseInfoLinkColumn")
            {
                // 必須条件のセット
                this.SetRequiredCondition();

                using (var form = new DesignCheckBasicInformationEntryForm { DesignCheck = this.GetDesignCheckByDataGridView(e.RowIndex) })
                {
                    //OKかどうか
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        if (form.IsRefresh)
                        {
                            this.SetDesignCheckList(true);
                        }
                    }
                }
            }
        }
        #endregion

        #region 新規作成ボタンクリック
        /// <summary>
        /// 新規作成ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            // 必須条件のセット
            this.SetRequiredCondition();

            using (var form = new DesignCheckBasicInformationEntryForm { DesignCheck = new DesignCheckGetOutModel { ID = null } })
            {
                //OKかどうか
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //設計チェック一覧設定
                    FormControlUtil.FormWait(this, () => this.SetDesignCheckList());
                }
            }
        }
        #endregion

        #region 設計チェック一覧設定
        /// <summary>
        /// 設計チェック一覧設定
        /// </summary>
        /// <param name="isKeepScroll"></param>
        private void SetDesignCheckList(bool isKeepScroll = false)
        {
            // 検索条件チェック
            if (!this.IsSearchDesignCheck()) return;

            // 設計チェック取得
            this.DataList = this.GetDesignCheckList();

            // データのバインド
            this.customTemplate.SetDataSource(this.DataList, this.designCheckDataSource);

            // レイアウトの設定
            this.SetLayout();

            // 設計チェックが取得できたかどうか
            this.ListDataLabel.Text = this.DataList.Any() == true ? "" : Resources.KKM00005;

            if (!isKeepScroll)
            {
                // スクロールの初期化
                this.DesignCheckMultiRow.FirstDisplayedLocation = new Point(0, 0);

                //一覧を未選択状態に設定
                this.DesignCheckMultiRow.CurrentCell = null;
            }
        }

        /// <summary>
        /// レイアウトの設定
        /// </summary>
        private void SetLayout()
        {
            foreach (var row in this.DesignCheckMultiRow.Rows)
            {
                // 設計チェック名の整形
                row.Cells["DesignCheckNameLinkColumn"].Value = Convert.ToInt32(row.Cells["TimesColumn"]?.Value) > 0
                    ? string.Format("{0} {1}回目", row.Cells["NameColumn"]?.Value?.ToString(), row.Cells["TimesColumn"]?.Value?.ToString())
                    : row.Cells["NameColumn"]?.Value.ToString();

                // OPEN件数の背景色変更
                row.Cells["OpenCountColumn"].Style.BackColor = Color.LightYellow;
            }
        }
        #endregion

        #region 設計チェックの取得
        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckGetOutModel> GetDesignCheckList()
        {
            //パラメータ設定
            var cond = new DesignCheckGetInModel
            {
                // 開催日（開始）
                OPEN_START_DATE = this.OpenStartDateTimePicker.SelectedDate,
                // 開催日（終了）
                OPEN_END_DATE = this.OpenEndDateTimePicker.SelectedDate,
                // 設計チェック名称
                名称 = this.DesignCheckNameTextBox.Text,
                // オープンフラグ
                OPEN_FLG = this.StatusOpenCheckBox.Checked,
                // クローズフラグ
                CLOSE_FLG = this.StatusCloseCheckBox.Checked,
            };

            //設計チェック検索条件
            this.ListSearchCond = cond;

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckGetInModel, DesignCheckGetOutModel>(ControllerType.DesignCheck, cond);

            //レスポンスが取得できたかどうか
            var list = new List<DesignCheckGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list;
        }
        #endregion

        #region グリッドデータを設計チェック一覧から取得
        /// <summary>
        /// グリッドデータを設計チェック一覧から取得
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheckByDataGridView(int index)
        {
            return this.GetDesignCheckByDataGridView(this.DesignCheckMultiRow.Rows[index]);
        }

        /// <summary>
        /// グリッドデータを設計チェック一覧から取得
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheckByDataGridView(GrapeCity.Win.MultiRow.Row row)
        {
            var id = Convert.ToInt64(row.Cells["IdColumn"].Value);

            return this.DataList.First(x => x.ID == id);
        }
        #endregion

        #region 設計チェック削除ボタンクリック  
        /// <summary>
        /// 設計チェック削除ボタンクリック
        /// </summary>
        /// <param name="sender">行</param>
        /// <param name="e">行</param>
        /// <returns></returns>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // グリッドの選択確認
            if (this.DesignCheckMultiRow.CurrentCell == null || this.DesignCheckMultiRow.SelectedCells.Count == 0)
            {
                Messenger.Warn(Resources.KKM00009);
                return;
            }

            // グリッドの情報を取得
            foreach (var cell in this.DesignCheckMultiRow.SelectedCells)
            {
                this.DesignCheck = this.GetDesignCheckByDataGridView(this.DesignCheckMultiRow.Rows[cell.RowIndex]);
            }


            FormControlUtil.FormWait(this, () =>
            {
                //削除可否を問い合わせ
                if (Messenger.Confirm(string.Format(Resources.KKM00042, "開催日", DateTimeUtil.FormatDateWithSlash(this.DesignCheck.開催日.ToString()), "設計チェック名", this.DesignCheck.名称)) == DialogResult.Yes)
                {
                    //設計チェックの削除
                    if (this.DeleteDesignCheck() == true)
                    {
                        //設計チェック一覧設定
                        FormControlUtil.FormWait(this, () => this.SetDesignCheckList());
                    }
                }
            });
        }

        /// <summary>
        /// 設計チェックの削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteDesignCheck()
        {
            //APIで取得
            var res = HttpUtil.DeleteResponse<DesignCheckGetOutModel>(ControllerType.DesignCheck, this.DesignCheck);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除後メッセージ
                Messenger.Info(Resources.KKM00003);

                return true;
            }

            return false;
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            var util = new MultiRowUtil(this.DesignCheckMultiRow);
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        #region エクスポートボタンクリック
        /// <summary>
        /// エクスポートボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            // 必須条件のセット
            this.SetRequiredCondition();

            var form = new DesignCheckExportForm();

            form.FromOpenDate = this.OpenStartDateTimePicker.SelectedDate;
            form.ToOpenDate= this.OpenEndDateTimePicker.SelectedDate;
            form.DesignCheckName = this.DesignCheckNameTextBox.Text;
            form.StatusOpen = this.StatusOpenCheckBox.Checked;
            form.StatusClose = this.StatusCloseCheckBox.Checked;
            form.Show();
        }
        #endregion

        #region 設計チェック一覧用権限管理クラス
        /// <summary>
        /// 設計チェック一覧用権限管理クラス
        /// </summary>
        private class Auth : Authority
        {
            /// <summary>
            /// PC端末権限クラス
            /// </summary>
            public PCAuth PCAuth { get; private set; }

            public Auth(UserAuthorityOutModel model) : base(model)
            {
                PCAuth = new PCAuth();
            }

            /// <summary>
            /// そのボタンが表示できるか？
            /// </summary>
            /// <param name="btn"></param>
            /// <returns></returns>
            public new bool IsVisible(Button btn)
            {
                // 権限がある場合でも､PC AUTHテーブルに登録時は非表示
                if (PCAuth.IsOk())
                {
                    return false;
                }

                return base.IsVisible(btn);
            }
        }
        #endregion
    }
}