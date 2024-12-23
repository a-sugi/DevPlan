using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using DevPlan.UICommon;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using System.ComponentModel;

namespace DevPlan.Presentation.UIDevPlan.FunctionAuthority
{
    /// <summary>
    /// ロール管理
    /// </summary>
    public partial class FunctionAuthorityRollSettingForm : BaseForm
    {
        #region メンバ変数
        private BindingSource RollSettingDataSource = new BindingSource();
        private BindingSource RollNameDataSource = new BindingSource();
        private ToolTip ToolTip = new ToolTip();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "ロール設定"; } }

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }
        /// <summary>編集フラグ</summary>
        private bool IsEdit { get; set; } = false;

        /// <summary>コミット中可否</summary>
        private bool IsCommit { get; set; } = false;

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        private RollModel EditRoll { get; set; } = new RollModel();

        /// <summary>ロール設定リスト</summary>
        private BindingList<RollModel> RollNameList { get; set; } = new BindingList<RollModel>();

        /// <summary>ロール設定リスト</summary>
        private List<RollModel> RollSettingDataList { get; set; } = new List<RollModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FunctionAuthorityRollSettingForm()
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
        private void FunctionAuthorityRollSettingForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // ユーザー権限
                this.UserAuthority = base.GetFunction(FunctionID.FunctionAuthority);

                // 画面の初期化
                this.InitForm();

                // グリッドの初期化
                this.InitDataGridView();

                // プロセスの初期化
                this.InitProcess();
            });
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';

            // バインド中ON
            this.IsBind = true;

            try
            {
                //　ツールチップの初期化
                this.ToolTip = new ToolTip()
                {
                    InitialDelay = 0,
                    ReshowDelay = 0,
                    AutoPopDelay = int.MaxValue,
                    ShowAlways = true,
                };

                // ロール新規登録ボタン
                this.RollAddButton.Visible = isUpdate || isManagement;

                // クリアボタン
                this.ClearButton.Visible = isUpdate || isManagement;

                // 登録ボタン
                this.EntryButton.Visible = isUpdate || isManagement;

                // 削除ボタン
                this.DeleteButton.Visible = isUpdate || isManagement;
            }
            finally
            {
                // バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// データグリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            // バインド中ON
            this.IsBind = true;

            try
            {
                // ヘッダの調整
                foreach (DataGridViewColumn col in this.RollSettingDataGridView.Columns)
                {
                    // ヘッダーの改行コードを置き換え
                    var headerText = col.HeaderText;
                    col.HeaderText = headerText.Replace(@"\n", Const.CrLf);

                    // 改行がある場合は折り返し
                    if (col.HeaderText != headerText)
                    {
                        col.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
                    }
                }

                // 列の自動生成可否
                this.RollSettingDataGridView.AutoGenerateColumns = false;

                // データーソース
                this.RollSettingDataGridView.DataSource = this.RollSettingDataSource;
            }
            finally
            {
                // バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// プロセスの初期化
        /// </summary>
        private void InitProcess()
        {
            // バインド中ON
            this.IsBind = true;

            try
            {
                // コンボボックス初期化
                this.InitComboBox();

                // 編集ロール設定
                this.SetEditRoll();

                // 一覧設定
                this.SetDataList();
            }
            finally
            {
                // 編集中OFF
                this.IsEdit = false;

                // バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// コンボボックスの初期化
        /// </summary>
        private void InitComboBox()
        {
            // ロールリストの取得
            this.RollNameList = new BindingList<RollModel>(GetRollNameList());

            // ロール選択リスト
            FormControlUtil.SetComboBoxItem(this.RollComboBox, "ROLL_ID", "ROLL_NAME", this.RollNameList, this.RollNameDataSource, true);
            if (this.EditRoll.ROLL_ID > 0) this.RollComboBox.SelectedValue = this.EditRoll.ROLL_ID;
        }
        #endregion

        #region 編集ロール設定
        /// <summary>
        /// 編集ロール設定
        /// </summary>
        private void SetEditRoll()
        {
            this.EditRoll = new RollModel()
            {
                // ロールID
                ROLL_ID = (long?)this.RollComboBox.SelectedValue > 0 ? (long?)this.RollComboBox.SelectedValue : null,
                // ロール名
                ROLL_NAME = this.RollComboBox.Text
            };
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// ロール設定一覧の設定
        /// </summary>
        private void SetDataList()
        {
            // ロール設定リスト取得
            this.RollSettingDataList = this.GetRollSettingList();

            this.RollSettingDataGridView.SuspendLayout();

            // 元の設定値を取得
            var autoSizeColumnsMode = this.RollSettingDataGridView.AutoSizeColumnsMode;
            var autoSizeRowsMode = this.RollSettingDataGridView.AutoSizeRowsMode;
            var columnHeadersHeightSizeMode = this.RollSettingDataGridView.ColumnHeadersHeightSizeMode;

            // 行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない
            this.RollSettingDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.RollSettingDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.RollSettingDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // データのバインド
            this.RollSettingDataSource.DataSource = this.RollSettingDataList.OrderBy(x => x.FUNCTION_ID);

            // 一覧の調整
            this.AdjustDataList();

            // 元の設定値を復元
            this.RollSettingDataGridView.AutoSizeColumnsMode = autoSizeColumnsMode;
            this.RollSettingDataGridView.AutoSizeRowsMode = autoSizeRowsMode;
            this.RollSettingDataGridView.ColumnHeadersHeightSizeMode = columnHeadersHeightSizeMode;

            this.RollSettingDataGridView.ResumeLayout();

            // 一覧を未選択状態に設定
            this.RollSettingDataGridView.CurrentCell = null;

            // 編集フラグ初期化
            this.IsEdit = false;
        }

        /// <summary>
        /// ロール設定一覧の調整
        /// </summary>
        private void AdjustDataList()
        {
            var isPrtScrAll = false;

            foreach (DataGridViewRow row in this.RollSettingDataGridView.Rows)
            {
                var isUpdate = (string)row.Cells[this.UpdateFlgColumn.Name].Value == "1";
                var isExport = (string)row.Cells[this.ExportFlgColumn.Name].Value == "1";
                var isManagement = (string)row.Cells[this.ManagementFlgColumn.Name].Value == "1";
                var isPrtScr = (string)row.Cells[this.PrintScreenFlgColumn.Name].Value == "1";

                if (isPrtScr && !isPrtScrAll) isPrtScrAll = true;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    var col = this.RollSettingDataGridView.Columns[cell.ColumnIndex];

                    // チェックボックス
                    if (cell.GetType() == typeof(DataGridViewCheckBoxCell))
                    {
                        // 参照
                        if (col.Name == this.ReadFlgColumn.Name)
                        {
                            // 更新・出力・管理がチェックされている場合
                            if (isUpdate || isExport || isManagement)
                            {
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // 更新
                        else if (col.Name == this.UpdateFlgColumn.Name)
                        {
                            // 管理がチェックされている場合
                            if (isManagement)
                            {
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // 出力
                        else if (col.Name == this.ExportFlgColumn.Name)
                        {
                            // 管理がチェックされている場合
                            if (isManagement)
                            {
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }

                        // ログイン制御
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) == (int)FunctionID.Login)
                        {
                            // 参照以外
                            if (col.Name != this.ReadFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // 試験車日程、CAP・商品力以外
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.TestCar &&
                            Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.CAP)
                        {
                            // 全閲覧
                            if (col.Name == this.AllGeneralCodeFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // カーシェア日程、試験車管理以外
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.CarShare &&
                            Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.TestCarManagement)
                        {
                            // カーシェア事務所
                            if (col.Name == this.CarshareOfficeFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // カーシェア日程以外
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.CarShare)
                        {
                            // 自部管理
                            if (col.Name == this.OwnPartManagementFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // CAP・商品力以外
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.CAP)
                        {
                            // 自部出力
                            if (col.Name == this.OwnPartExportFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }
                        // 設計チェック以外
                        if (Convert.ToInt16(row.Cells[this.FunctionIdColumn.Name].Value) != (int)FunctionID.DesignCheck)
                        {
                            // SKS
                            if (col.Name == this.SksFlgColumn.Name)
                            {
                                cell.Value = "0";
                                cell.ReadOnly = true;
                                cell.Style.BackColor = Color.LightGray;
                                continue;
                            }
                        }

                        // チェック済み
                        if (cell.Value?.ToString() == "1")
                        {
                            cell.Style.BackColor = Color.LightYellow;
                        }
                    }
                }
            }

            foreach (DataGridViewRow psrow in this.RollSettingDataGridView.Rows)
            {
                var pscell = psrow.Cells[this.PrintScreenFlgColumn.Name];

                // ログイン制御
                if (Convert.ToInt16(psrow.Cells[this.FunctionIdColumn.Name].Value) == (int)FunctionID.Login) continue;

                // PrtScr
                pscell.Value = isPrtScrAll ? "1" : "0";
                pscell.Style.BackColor = isPrtScrAll ? Color.LightYellow : Color.White;
            }
        }
        #endregion

        #region データの操作

        #region 登録データのチェック
        /// <summary>
        /// 登録データのチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsEntryRoll(bool isNew = false)
        {
            //編集していなければ終了
            if (this.IsEdit == false)
            {
                return false;
            }

            //登録対象が無ければ終了
            if (this.RollSettingDataGridView.RowCount <= 0)
            {
                return false;
            }

            // 更新
            if (!isNew)
            {
                //対象が選択されているかどうか
                if (this.EditRoll.ROLL_ID == null)
                {
                    //選択されていない場合はエラー
                    FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.ErrorBackColor);
                    Messenger.Warn(Resources.KKM00009);
                    return false;
                }

                //対象が存在しているかどうか
                if (this.GetRollNameList(new RollGetInModel { ROLL_ID = (long)this.EditRoll.ROLL_ID }) == null)
                {
                    //存在していない場合はエラー
                    FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.ErrorBackColor);
                    Messenger.Warn(Resources.KKM00021);
                    return false;
                }
            }

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            return true;
        }
        #endregion

        #region 削除データのチェック
        /// <summary>
        /// 削除データのチェック
        /// </summary>
        /// <returns>判定結果</returns>
        private bool IsDeleteRoll()
        {
            // 対象が選択されているかどうか
            if (this.EditRoll.ROLL_ID == null)
            {
                // 選択されていない場合はエラー
                FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.ErrorBackColor);
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            // すでに割り当てが行われているかどうか
            if (this.GetRolllAuthorityList().Any())
            {
                // すでに割り当てられている場合はエラー
                FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.ErrorBackColor);
                Messenger.Warn(Resources.KKM03033);
                return false;
            }

            return true;
        }
        #endregion

        #region 登録データの取得
        /// <summary>
        /// 登録データの取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<RollEntryInModel> GetEntryRoll(bool isNew = false)
        {
            Func<object, string> getchk = val => val?.ToString() != "1" ? "0" : "1";
            var list = new List<RollEntryInModel>();

            foreach (DataGridViewRow row in this.RollSettingDataGridView.Rows)
            {
                list.Add(new RollEntryInModel()
                {
                    // ロールID
                    ROLL_ID = !isNew ? this.EditRoll.ROLL_ID : null,
                    // ロール名
                    ROLL_NAME = string.IsNullOrWhiteSpace(this.EditRoll.ROLL_NAME) ? (string)this.RollComboBox.Text : this.EditRoll.ROLL_NAME,
                    // 機能ID
                    FUNCTION_ID = (long)row.Cells[this.FunctionIdColumn.Name].Value,
                    // 参照フラグ(0:権限なし 1:権限あり)
                    READ_FLG = getchk(row.Cells[this.ReadFlgColumn.Name].Value),
                    // 更新フラグ(0:権限なし 1:権限あり)
                    UPDATE_FLG = getchk(row.Cells[this.UpdateFlgColumn.Name].Value),
                    // 出力フラグ(0:権限なし 1:権限あり)
                    EXPORT_FLG = getchk(row.Cells[this.ExportFlgColumn.Name].Value),
                    // 管理フラグ(0:権限なし 1:権限あり)
                    MANAGEMENT_FLG = getchk(row.Cells[this.ManagementFlgColumn.Name].Value),
                    // プリントスクリーンフラグ(0:権限なし 1:権限あり)
                    PRINTSCREEN_FLG = getchk(row.Cells[this.PrintScreenFlgColumn.Name].Value),
                    // カーシェア事務所フラグ(0:権限なし 1:権限あり)
                    CARSHARE_OFFICE_FLG = getchk(row.Cells[this.CarshareOfficeFlgColumn.Name].Value),
                    // 全閲覧権限フラグ(0:権限なし 1:権限あり)
                    ALL_GENERAL_CODE_FLG = getchk(row.Cells[this.AllGeneralCodeFlgColumn.Name].Value),
                    // SKSフラグ(0:権限なし 1:権限あり)
                    SKS_FLG = getchk(row.Cells[this.SksFlgColumn.Name].Value),
                    // 自部更新フラグ(0:権限なし 1:権限あり)
                    JIBU_UPDATE_FLG = getchk(row.Cells[this.OwnPartUpdateFlgColumn.Name].Value),
                    // 自部出力フラグ(0:権限なし 1:権限あり)
                    JIBU_EXPORT_FLG = getchk(row.Cells[this.OwnPartExportFlgColumn.Name].Value),
                    // 自部管理フラグ(0:権限なし 1:権限あり)
                    JIBU_MANAGEMENT_FLG = getchk(row.Cells[this.OwnPartManagementFlgColumn.Name].Value),
                    // ユーザーID(登録者)
                    INPUT_PERSONEL_ID = SessionDto.UserId,
                    // ユーザーID(更新者)
                    CHANGE_PERSONEL_ID = SessionDto.UserId
                });
            }

            return list;
        }
        #endregion

        #region ロールの登録
        /// <summary>
        /// ロールの登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool InsertRoll()
        {
            // 入力チェック
            if (this.IsEntryRoll(true) == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryRoll(true);

            // 登録対象があるかどうか
            if (list == null || list.Any() == false)
            {
                return false;
            }

            // Post実行
            var res = HttpUtil.PostResponse(ControllerType.Roll, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            if (res.Results?.Count() > 0)
            {
                this.EditRoll.ROLL_ID = res.Results.FirstOrDefault().ROLL_ID;
            }

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region ロールの更新
        /// <summary>
        /// ロールの更新
        /// </summary>
        /// <returns>実行結果</returns>
        private bool UpdateRoll()
        {
            // 入力チェック
            if (this.IsEntryRoll() == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryRoll();

            // 登録対象があるかどうか
            if (list == null || list.Any() == false)
            {
                return false;
            }

            // Post実行
            var res = HttpUtil.PutResponse(ControllerType.Roll, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }
            
            if (this.EditRoll.ROLL_ID > 0)
            {
                var editRoll = this.RollNameList.FirstOrDefault(x => x.ROLL_ID == this.EditRoll.ROLL_ID);

                if (editRoll != null && editRoll.ROLL_NAME != this.EditRoll.ROLL_NAME)
                {
                    editRoll.ROLL_NAME = this.EditRoll.ROLL_NAME;
                    RollNameDataSource.ResetBindings(false);
                }

            }

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region ロールの削除
        /// <summary>
        /// ロールの削除
        /// </summary>
        /// <returns>実行結果</returns>
        private bool DeleteRoll()
        {
            // 入力チェック
            if (this.IsDeleteRoll() == false)
            {
                return false;
            }

            // 削除確認
            if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
            {
                return false;
            }

            // パラメータ設定
            var prm = new RollDeleteInModel() { ROLL_ID = (long)this.EditRoll.ROLL_ID };

            // 削除実行
            var res = HttpUtil.DeleteResponse<RollDeleteInModel>(ControllerType.Roll, prm);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 削除完了
            Messenger.Info(Resources.KKM00003);

            return true;
        }
        #endregion

        #region ロールの登録（変更フラグチェック）
        /// <summary>
        /// ロールの登録（変更フラグチェック）
        /// </summary>
        /// <returns></returns>
        private bool IsEditRollUpdate()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 更新・管理権限なしは確認や更新処理を行わない。
            if (!isUpdate && !isManagement)
            {
                return true;
            }

            // 画面を変更していないか登録するかどうか
            if (this.IsEdit == false || (this.IsEdit == true && Messenger.Confirm(Resources.KKM00006) != DialogResult.Yes))
            {
                return true;
            }

            // 更新
            return this.UpdateRoll();
        }
        #endregion

        #endregion

        #region データの取得

        #region ロール名の取得
        /// <summary>
        /// ロール名の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollModel> GetRollNameList(RollGetInModel cond = null)
        {
            return HttpUtil.GetResponse<RollGetInModel, RollModel>(ControllerType.RollName, cond)?.Results?.ToList();
        }
        #endregion

        #region ロールの取得
        /// <summary>
        /// ロールの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollModel> GetRollSettingList()
        {
            // Get実行
            var list = HttpUtil.GetResponse<RollGetInModel, RollModel>(ControllerType.Roll, new RollGetInModel() { ROLL_ID = (long?)this.RollComboBox.SelectedValue ?? 0 })?.Results?.ToList();

            // 取得リストの調整
            foreach (var val in this.GetFunctionList())
            {
                if (!list.Any(x => x.FUNCTION_ID == val.FUNCTION_ID))
                {
                    list.Add(new RollModel()
                    {
                        FUNCTION_ID = val.FUNCTION_ID,
                        FUNCTION_NAME = val.FUNCTION_NAME
                    });
                }
            }

            return list;
        }
        #endregion

        #region 機能マスタの取得
        /// <summary>
        /// 機能マスタの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<FunctionOutModel> GetFunctionList()
        {
            //Get実行
            var res = HttpUtil.GetResponse<FunctionInModel, FunctionOutModel>(ControllerType.Function, null);

            return (res.Results).ToList();
        }
        #endregion

        #region ロール権限の取得
        /// <summary>
        /// ロール権限の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollAuthorityGetOutModel> GetRolllAuthorityList()
        {
            // 条件設定
            var cond = new RollAuthorityGetInModel()
            {
                ROLL_ID = this.EditRoll.ROLL_ID
            };

            //APIで取得
            var res = HttpUtil.GetResponse<RollAuthorityGetInModel, RollAuthorityGetOutModel>(ControllerType.RollAuthority, cond);

            return res.Results?.ToList();
        }
        #endregion

        #endregion

        #region イベント

        #region ロール設定一覧
        /// <summary>
        /// セルマウスポインターエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollSettingDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.SituationToolTip.Active)
            {
                this.SituationToolTip.Active = false;
            }

            //行ヘッダー以外は終了
            if (e.RowIndex != -1)
            {
                return;
            }

            var grid = this.RollSettingDataGridView;
            var tips = string.Empty;

            // プリントスクリーン
            if (e.ColumnIndex == this.PrintScreenFlgColumn.Index)
            {
                tips = "参照権限のある画面のプリントスクリーンを許可します。";
            }
            // カーシェア事務所
            else if (e.ColumnIndex == this.CarshareOfficeFlgColumn.Index)
            {
                tips = @"カーシェア日程にて以下の権限を付与します。

　・SKC管理課(SKCKA)項目のスケジュールの作成,編集,削除 ···他人作成スケジュールの編集,削除も○
　・総括部署(SJSB)項目のスケジュールの作成,編集,削除 ···他人作成スケジュールの編集,削除は×
　・SKC管理課(SKCKA)項目の作業履歴の作成,編集,削除 ···他部署(課)作成作業履歴の編集,削除も○　
　・総括部署(SJSB)項目の作業履歴の作成,編集,削除 ···他部署(課)作成作業履歴の編集,削除は×

※SKC管理課,総括部署はロールにて割り当てられます。";
            }
            // 全閲覧権限
            else if (e.ColumnIndex == this.AllGeneralCodeFlgColumn.Index)
            {
                tips = "試験車日程,CAP・商品力にて表示されたすべての開発符号の利用を許可します。";
            }
            // SKS
            else if (e.ColumnIndex == this.SksFlgColumn.Index)
            {
                tips = "設計チェックにて基本情報の作成,編集,削除以外の管理権限を付与します。";
            }
            // 自部出力
            else if (e.ColumnIndex == this.OwnPartExportFlgColumn.Index)
            {
                tips = "CAP・商品力にてExcel出力（自部）を許可します。";
            }
            // 自部管理
            else if (e.ColumnIndex == this.OwnPartManagementFlgColumn.Index)
            {
                tips = @"カーシェア日程にて以下の権限を付与します。

　・SKC管理課(SKCKA)項目の作成,編集,削除,移譲 ···総括部署(SJSB)項目の編集,削除,移譲は×
　・SKC管理課(SKCKA)項目のスケジュールの作成,編集,削除 ···他人作成スケジュールの編集,削除も○
　・総括部署(SJSB)項目のスケジュールの作成,編集,削除 ···他人作成スケジュールの編集,削除は×
　・SKC管理課(SKCKA)項目の作業履歴の作成,編集,削除 ···他部署(課)作成作業履歴の編集,削除も○　
　・総括部署(SJSB)項目の作業履歴の作成,編集,削除 ···他部署(課)作成作業履歴の編集,削除は×

※SKC管理課,総括部署はロールにて割り当てられます。";
            }

            // 表示メッセージがある場合
            if (!string.IsNullOrWhiteSpace(tips))
            {
                //マウスの位置より20px下にずらして表示
                var point = grid.PointToClient(Control.MousePosition);
                point.Y += 20;

                //ツールチップを表示
                this.SituationToolTip.Active = true;
                this.SituationToolTip.Show(tips, grid, point);
            }
        }

        /// <summary>
        /// セルデータ値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollSettingDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            var col = this.RollSettingDataGridView.Columns[e.ColumnIndex];
            var name = col.Name;

            var row = this.RollSettingDataGridView.Rows[e.RowIndex];

            var cell = row.Cells[name];

            // 読み取り専用の場合は終了
            if (cell.ReadOnly)
            {
                return;
            }

            // チェックボックス
            if (cell.GetType() == typeof(DataGridViewCheckBoxCell))
            {
                var isCheck = (bool)cell.EditedFormattedValue;

                var cellRead = row.Cells[this.ReadFlgColumn.Name];
                var cellUpdate = row.Cells[this.UpdateFlgColumn.Name];
                var cellExport = row.Cells[this.ExportFlgColumn.Name];
                var cellPrtScr = row.Cells[this.PrintScreenFlgColumn.Name];

                Func<object, bool> isConvCheck = (obj) => obj == null ? false : (bool)obj;

                //バインド中ON
                this.IsBind = true;

                try
                {
                    // 更新
                    if (col.Name == this.UpdateFlgColumn.Name)
                    {
                        // 出力がチェックされていない場合
                        if (!isConvCheck(cellExport.EditedFormattedValue))
                        {
                            // 参照
                            cellRead.Value = isCheck ? "1" : "0";
                            cellRead.ReadOnly = isCheck;
                            cellRead.Style.BackColor = isCheck ? Color.LightGray : Color.White;
                        }
                    }
                    // 出力
                    else if (col.Name == this.ExportFlgColumn.Name)
                    {
                        // 更新がチェックされていない場合
                        if (!isConvCheck(cellUpdate.EditedFormattedValue))
                        {
                            // 参照
                            cellRead.Value = isCheck ? "1" : "0";
                            cellRead.ReadOnly = isCheck;
                            cellRead.Style.BackColor = isCheck ? Color.LightGray : Color.White;
                        }
                    }
                    // 管理
                    else if (col.Name == this.ManagementFlgColumn.Name)
                    {
                        // 参照・更新・出力
                        cellRead.Value = cellUpdate.Value = cellExport.Value = isCheck ? "1" : "0";
                        cellRead.ReadOnly = cellUpdate.ReadOnly = cellExport.ReadOnly = isCheck;
                        cellRead.Style.BackColor = cellUpdate.Style.BackColor = cellExport.Style.BackColor = isCheck ? Color.LightGray : Color.White;
                    }
                    // PrtScr
                    else if (col.Name == this.PrintScreenFlgColumn.Name)
                    {
                        foreach (DataGridViewRow psrow in this.RollSettingDataGridView.Rows)
                        {
                            // 同一列
                            if (psrow.Index == e.RowIndex) continue;

                            var pscell = psrow.Cells[this.PrintScreenFlgColumn.Name];

                            // 読取専用
                            if (pscell.ReadOnly) continue;

                            // PrtScr 列
                            pscell.Value = isCheck ? "1" : "0";
                            pscell.Style.BackColor = isCheck ? Color.LightYellow : Color.White;
                        }
                    }

                    cell.Value = isCheck ? "1" : "0";
                    cell.Style.BackColor = isCheck ? Color.LightYellow : Color.White;
                }
                finally
                {
                    //バインド中OFF
                    this.IsBind = false;

                    //編集フラグON
                    this.IsEdit = true;
                }
            }
        }

        /// <summary>
        /// カレントセル変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollSettingDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            try
            {
                // コミット
                this.RollSettingDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            finally
            {
                //コミットON
                this.IsCommit = true;
            }
        }
        #endregion

        #region 画面クローズ
        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionAuthorityRollSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // グリッドの編集は終了
                this.RollSettingDataGridView.EndEdit();

                // 画面を変更していて登録するかどうか
                if (!IsEditRollUpdate())
                {
                    // 登録に失敗した場合は閉じさせない
                    e.Cancel = true;
                }
            });
        }
        #endregion

        #region ロールコンボボックス選択変更
        /// <summary>
        /// ロールコンボボックス選択変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 変更反映確認
                if (this.IsEditRollUpdate())
                {
                    // コントローラの初期化
                    FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.DefaultBackColor);

                    // 編集ロール設定
                    this.SetEditRoll();

                    // 一覧設定
                    this.SetDataList();
                }
            });
        }
        #endregion

        #region ロールコンボボックステキスト変更
        /// <summary>
        /// ロールコンボボックステキスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollComboBox_TextChanged(object sender, EventArgs e)
        {
            //バインド中の場合は終了
            if (this.IsBind)
            {
                return;
            }

            // 選択以外でテキスト変更があった場合
            if ((long?)this.RollComboBox.SelectedValue == null)
            {
                // 編集ロール情報の退避
                this.EditRoll.ROLL_NAME = this.RollComboBox.Text;

                // 編集フラグON
                this.IsEdit = true;
            }
        }
        #endregion

        #region ロール新規登録ボタンクリック
        /// <summary>
        /// ロール新規登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollAddButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (this.InsertRoll())
                {
                    // 初期化
                    this.InitProcess();
                }
            });
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

            FormControlUtil.FormWait(this, () =>
            {
                // 変更反映確認
                if (this.IsEditRollUpdate())
                {
                    this.IsBind = true;

                    try
                    {
                        // 空選択
                        this.RollComboBox.SelectedIndex = 0;

                        // コントローラの初期化
                        FormControlUtil.SetComboBoxBackColor(this.RollComboBox, Const.DefaultBackColor);

                        // 編集ロール設定
                        this.SetEditRoll();

                        // 一覧設定
                        this.SetDataList();
                    }
                    finally
                    {
                        this.IsBind = false;
                    }
                }
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
            FormControlUtil.FormWait(this, () =>
            {
                if (this.UpdateRoll())
                {
                    // 初期化
                    this.InitProcess();
                }
            });
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
            FormControlUtil.FormWait(this, () =>
            {
                if (this.DeleteRoll())
                {
                    // 初期化
                    this.InitProcess();
                }
            });
        }
        #endregion

        #endregion
    }
}