﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using System.Drawing;

namespace DevPlan.Presentation.UIDevPlan.FunctionAuthority
{
    /// <summary>
    /// 機能権限詳細(人)画面
    /// </summary>
    public partial class FunctionAuthorityRollPersonelForm : BaseSubForm
    {
        #region メンバ変数
        private BindingSource RollSettingDataSource = new BindingSource();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "機能権限詳細(人)"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>検索条件</summary>
        public RollAuthorityModel ListSearchCond { get; set; }

        /// <summary>ロールリスト</summary>
        private List<RollModel> RollDataList { get; set; } = new List<RollModel>();

        /// <summary>ロール権限リスト(人)</summary>
        private List<RollAuthorityGetOutModel> RollPersonelDataList { get; set; }

        /// <summary>ロール権限リスト(部)</summary>
        private List<RollAuthorityGetOutModel> RollDepartmentDataList { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FunctionAuthorityRollPersonelForm()
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
        private void FunctionAuthorityRollPersonelForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //データグリッドビューの初期化
                this.InitDataGridView();

                //画面初期化
                this.InitForm();

                //一覧設定
                this.SetDataList();

                //画面調整
                this.AdjustForm();
            });
        }

        /// <summary>
        /// データグリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            //列の自動生成可否
            this.RollDataGridView.AutoGenerateColumns = false;

            //データーソース
            this.RollDataGridView.DataSource = this.RollSettingDataSource;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';
            var isExport = this.UserAuthority.EXPORT_FLG == '1';
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            // 職番
            this.PersonelIdLabel.Text = this.ListSearchCond.PERSONEL_ID;

            // 氏名
            this.PersonelNameLabel.Text = this.ListSearchCond.NAME;

            // 登録ボタン
            this.RegistButton.Visible = isUpdate || isManagement;
        }

        /// <summary>
        /// 画面調整
        /// </summary>
        private void AdjustForm()
        {
            var grid = this.RollDataGridView;
            var corner = grid.TopLeftHeaderCell;
            var cbox = this.AllSelectCheckBox;

            var x = (corner.Size.Width - cbox.Width) / 2 + grid.Location.X + 4;
            var y = (corner.Size.Height - cbox.Height) / 2 + grid.Location.Y + 2;

            cbox.Location = new Point(x, y);
        }
        #endregion

        #region 一覧設定
        /// <summary>
        /// ロール権限一覧の設定
        /// </summary>
        private void SetDataList()
        {
            // ロール権限リスト取得
            this.RollPersonelDataList = this.GetRollPersonelList();
            this.RollDepartmentDataList = this.GetRollDepartmentList();

            // 元の設定値を取得
            var autoSizeColumnsMode = this.RollDataGridView.AutoSizeColumnsMode;
            var autoSizeRowsMode = this.RollDataGridView.AutoSizeRowsMode;
            var columnHeadersHeightSizeMode = this.RollDataGridView.ColumnHeadersHeightSizeMode;

            // 行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない
            this.RollDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.RollDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            this.RollDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // データのバインド
            this.RollSettingDataSource.DataSource = this.GetRollNameList();

            // 一覧の調整
            this.AdjustDataList();

            // 元の設定値を復元
            this.RollDataGridView.AutoSizeColumnsMode = autoSizeColumnsMode;
            this.RollDataGridView.AutoSizeRowsMode = autoSizeRowsMode;
            this.RollDataGridView.ColumnHeadersHeightSizeMode = columnHeadersHeightSizeMode;

            // 一覧を未選択状態に設定
            this.RollDataGridView.CurrentCell = null;
        }

        /// <summary>
        /// ロール権限一覧の調整
        /// </summary>
        private void AdjustDataList()
        {
            foreach (DataGridViewRow row in this.RollDataGridView.Rows)
            {
                // チェックボックスチェック(人)
                row.Cells[this.DataCheckBoxColumn.Name].Value
                    = this.RollPersonelDataList.Any(x => x.ROLL_ID == Convert.ToInt64(row.Cells[this.RollIdColumn.Name].Value));

                // 人の割り当てがない場合のみ
                if (!Convert.ToBoolean(row.Cells[this.DataCheckBoxColumn.Name].Value))
                {
                    // チェックボックスチェック(部)：チェックの場合は読取専用
                    row.Cells[this.DataCheckBoxColumn.Name].Value = row.Cells[this.DataCheckBoxColumn.Name].ReadOnly
                        = this.RollDepartmentDataList.Any(x => x.ROLL_ID == Convert.ToInt64(row.Cells[this.RollIdColumn.Name].Value));

                    if (Convert.ToBoolean(row.Cells[this.DataCheckBoxColumn.Name].ReadOnly))
                    {
                        row.Cells[this.DataCheckBoxColumn.Name].ToolTipText = row.Cells[this.RollNameColumn.Name].ToolTipText = "部署・役職で割り当てられています。";
                        row.Cells[this.DataCheckBoxColumn.Name].Style.BackColor = row.Cells[this.RollNameColumn.Name].Style.BackColor = Color.LightGray;
                    }
                }
            }
        }
        #endregion

        #region データの操作

        #region 登録データ(人)の取得
        /// <summary>
        /// 登録データ(人)の取得
        /// </summary>
        /// <returns>取得データ</returns>
        private List<RollAuthorityPutInModel> GetEntryPersonelRollAuth()
        {
            var list = new List<RollAuthorityPutInModel>();

            foreach (DataGridViewRow row in this.RollDataGridView.Rows)
            {
                if (!Convert.ToBoolean(row.Cells[this.DataCheckBoxColumn.Name].Value)) continue;

                if (Convert.ToBoolean(row.Cells[this.DataCheckBoxColumn.Name].ReadOnly)) continue;

                list.Add(new RollAuthorityPutInModel()
                {
                    // ロールID
                    ROLL_ID = (long)row.Cells[this.RollIdColumn.Name].Value,
                    // ユーザーID
                    PERSONEL_ID = this.ListSearchCond.PERSONEL_ID,
                    // ユーザーID(登録者)
                    INPUT_PERSONEL_ID = SessionDto.UserId,
                    // 種別(0:人, 1:部署・役職)
                    TYPE = 0
                });
            }

            return list;
        }
        #endregion

        #region ロール権限(人)の登録
        /// <summary>
        /// ロール権限(人)の登録
        /// </summary>
        /// <returns>実行結果</returns>
        private bool EntryPersonelRollAuth()
        {
            // 登録対象の取得
            var list = this.GetEntryPersonelRollAuth();

            //対象が選択されているかどうか
            if (list == null || list.Count <= 0)
            {
                // 削除
                return this.DeletePersonelRollAuth();
            }

            // Post実行
            var res = HttpUtil.PutResponse(ControllerType.RollAuthority, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 登録完了
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #region ロール権限(人)の削除
        /// <summary>
        /// ロール権限(人)の削除
        /// </summary>
        /// <returns>実行結果</returns>
        private bool DeletePersonelRollAuth()
        {
            // 条件設定
            var cond = new RollAuthorityDeleteInModel()
            {
                // ユーザーID
                PERSONEL_ID = this.ListSearchCond.PERSONEL_ID,
                // 種別(0:人, 1:部署・役職)
                TYPE = 0
            }; 

            // Delete実行
            var res = HttpUtil.DeleteResponse<RollAuthorityDeleteInModel>(ControllerType.RollAuthority, cond);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            // 削除完了(メッセージは登録完了)
            Messenger.Info(Resources.KKM00002);

            return true;
        }
        #endregion

        #endregion

        #region データの取得

        #region ロールマスタの取得
        /// <summary>
        /// ロールマスタの取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollModel> GetRollNameList()
        {
            return HttpUtil.GetResponse<RollGetInModel, RollModel>(ControllerType.RollName, null)?.Results?.ToList();
        }
        #endregion

        #region 機能権限(ロール)の取得
        /// <summary>
        /// 機能権限(ロール:人)の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollAuthorityGetOutModel> GetRollPersonelList()
        {
            // 条件設定
            var cond = new RollAuthorityGetInModel()
            {
                PERSONEL_ID = this.ListSearchCond.PERSONEL_ID,
                TYPE = 0    // 人
            };

            //APIで取得
            var res = HttpUtil.GetResponse<RollAuthorityGetInModel, RollAuthorityGetOutModel>(ControllerType.RollAuthority, cond);

            return res.Results?.ToList();
        }
        /// <summary>
        /// 機能権限(ロール:部)の取得
        /// </summary>
        /// <returns>取得結果</returns>
        private List<RollAuthorityGetOutModel> GetRollDepartmentList()
        {
            // 条件設定
            var cond = new RollAuthorityGetInModel()
            {
                DEPARTMENT_ID = this.ListSearchCond.DEPARTMENT_ID,
                SECTION_ID = this.ListSearchCond.SECTION_ID,
                SECTION_GROUP_ID = this.ListSearchCond.SECTION_GROUP_ID,
                OFFICIAL_POSITION = this.ListSearchCond.OFFICIAL_POSITION,
                TYPE = 1    // 部
            };

            //APIで取得
            var res = HttpUtil.GetResponse<RollAuthorityGetInModel, RollAuthorityGetOutModel>(ControllerType.RollAuthority, cond);

            return res.Results?.ToList();
        }
        #endregion

        #endregion

        #region イベント

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
            foreach (DataGridViewRow row in this.RollDataGridView.Rows)
            {
                //Append Start 2020/01/27 杉浦
                // 非表示行は未処理
                if (!row.Visible) continue;
                //Append End 2020/01/27 杉浦

                if (Convert.ToBoolean(row.Cells[this.DataCheckBoxColumn.Name].ReadOnly)) continue;

                row.Cells[this.DataCheckBoxColumn.Name].Value = flg;
            }
        }
        #endregion

        #region 登録ボタンクリック
        /// <summary>
        /// 登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 登録実行
                if (this.EntryPersonelRollAuth())
                {
                    // フォームクローズ
                    base.FormOkClose();
                }
            });
        }
        #endregion

        #endregion
    }
}