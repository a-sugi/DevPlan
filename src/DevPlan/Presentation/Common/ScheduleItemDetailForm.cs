using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UIDevPlan.TestCarSchedule;
using DevPlan.Presentation.UIDevPlan.CarShare;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// スケジュール項目詳細
    /// </summary>
    public partial class ScheduleItemDetailForm<item, schedule> : BaseSubForm
        where item : class, new()
        where schedule : class, new()
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return string.Format("項目詳細({0})", FormSubTitle); } }

        /// <summary>画面名(サブ)</summary>
        public string FormSubTitle { get; set; }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>機能ID</summary>
        public FunctionID FunctionId { get; set; }

        /// <summary>機能制限フラグ</summary>
        public bool IsRestriction { get; set; } = false;

        /// <summary>リロードフラグ</summary>
        public bool IsReload { get; set; } = false;

        /// <summary>スケジュール項目</summary>
        public ScheduleItemModel<item> Item { get; set; }

        /// <summary>スケジュール利用車</summary>
        private ScheduleCarDetailModel ScheduleCar { get; set; } = new ScheduleCarDetailModel();

        /// <summary>試験車</summary>
        private TestCarSearchOutModel TestCar { get; set; } = new TestCarSearchOutModel();

        /// <summary>カーシェア車</summary>
        private CarShareInnerSearchOutModel CarShare { get; set; } = new CarShareInnerSearchOutModel();

        /// <summary>権限</summary>
        private UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>車両種別(車両情報)</summary>
        private int? SinkCarClass { get; set; } = null;

        /// <summary>車両種別(試験車基本情報)</summary>
        private int? SourceCarClass { get; set; } = null;

        //Append Start 2022/01/31 杉浦 項目を変動できるようにする
        private bool modified { get; set; } = false;

        //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
        private string 管理票No { get; set; }

        private List<ScheduleItemDisplayConfigurationModel> ViewList { get; set; }

        private bool ChangeView { get; set; } = false;
        //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
        //Append End 2022/01/31 杉浦 項目を変動できるようにする
        #endregion

        #region メンバ
        private Func<int?, string> getCarClassString = c =>
        {
            if (c == 0) return "試験車";
            if (c == 1) return "内製車";
            if (c == 2) return "外製車";

            return string.Empty;
        };

        private Func<string, DateTime?> getParseLeaseEndDate = str =>
        {
            DateTime dateTime;

            if (DateTime.TryParse(str, out dateTime))
            {
                return dateTime.AddDays(-1);
            }

            return null;
        };

        private Func<string, DateTime?> getParseDisposalDate = str =>
        {
            DateTime dateTime;

            if (DateTime.TryParse(str, out dateTime))
            {
                return new DateTime(dateTime.AddMonths(-1).Year, dateTime.AddMonths(-1).Month, 20);
            }

            return null;
        };
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScheduleItemDetailForm()
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
        private void ScheduleItemDetailForm_Load(object sender, EventArgs e)
        {
            // 権限の取得
            this.UserAuthority = this.GetFunction((FunctionID)FunctionId);

            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            // 初期表示フォーカス
            //this.ActiveControl = this.SinkMemoTextBox;
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // 画面の初期化
            this.InitForm();
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 画面初期化
        /// </summary>
        public void InitForm()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isJibuManagement = this.UserAuthority.JIBU_MANAGEMENT_FLG == '1';

            // 試験車日程
            if (this.FunctionId == FunctionID.TestCar)
            {
                // 車両種別の初期化
                this.SinkCarClass = this.SourceCarClass = 0;

                //Append Start 2022/02/08 杉浦 項目を変動できるようにする
                var item = this.Item.ScheduleItem as TestCarScheduleItemModel;
                this.Item.GeneralCode = string.IsNullOrEmpty(this.Item.GeneralCode) ? item.GENERAL_CODE : this.Item.GeneralCode;
                //Append End 2022/02/08 杉浦 項目を変動できるようにする
                
                // 画面初期化(車輛情報)
                this.ScheduleCarInitForm();

                // 画面初期化(試験車)
                this.TestCarInitForm();
            }
            // カーシェア日程
            else if (this.FunctionId == FunctionID.CarShare)
            {
                // 車両種別の初期化
                this.SinkCarClass = this.SourceCarClass = 1;

                //Append Start 2022/02/08 杉浦 項目を変動できるようにする
                var item = this.Item.ScheduleItem as CarShareScheduleItemModel;
                this.Item.GeneralCode = string.IsNullOrEmpty(this.Item.GeneralCode) ? item.GENERAL_CODE : this.Item.GeneralCode;
                //Append End 2022/02/08 杉浦 項目を変動できるようにする

                // 画面初期化(車輛情報)
                this.ScheduleCarInitForm();

                // 画面初期化(カーシェア)
                this.CarShareInitForm();
            }
            // 外製車日程
            else if (this.FunctionId == FunctionID.OuterCar)
            {
                // 車両種別の初期化
                this.SinkCarClass = this.SourceCarClass = 2;

                //Append Start 2022/02/08 杉浦 項目を変動できるようにする
                var item = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;
                this.Item.GeneralCode = string.IsNullOrEmpty(this.Item.GeneralCode) ? item.GENERAL_CODE : this.Item.GeneralCode;
                //Append End 2022/02/08 杉浦 項目を変動できるようにする

                // 画面初期化(車輛情報)
                this.ScheduleCarInitForm();

                // 画面初期化(外製車)
                this.OuterCarInitForm();
            }

            //Append Start 2022/01/31 杉浦 項目を変動できるようにする
            this.InitGridView();
            //Append End 2022/01/31 杉浦 項目を変動できるようにする

            //Append Start 2022/03/16 杉浦
            this.ListConfigPictureBox.Visible = this.Item.ScheduleItemEdit != ScheduleItemEditType.Insert;
            //Append End 2022/03/16 杉浦

            // 登録(コピー)ボタン
            this.EntryCopyButton.Visible = this.Item.ScheduleItemEdit == ScheduleItemEditType.Insert;

            // 削除ボタン
            this.DeleteButton.Visible = this.Item.ScheduleItemEdit == ScheduleItemEditType.Update;

            // 管理移譲ボタン（カーシェアの更新のみ）
            this.TransferButton.Visible = this.FunctionId == FunctionID.CarShare && this.Item.ScheduleItemEdit == ScheduleItemEditType.Update;

            // 管理権限なし(自部管理なし)
            if (!isManagement && !isJibuManagement)
            {
                this.EntryButton.Visible = false;
                this.EntryCopyButton.Visible = false;
                this.DeleteButton.Visible = false;
                this.TransferButton.Visible = false;
            }
            // 管理権限なし(自部管理あり)
            else if (!isManagement && isJibuManagement)
            {
                // カーシェア日程(更新・削除)
                if (this.FunctionId == FunctionID.CarShare && this.Item.ScheduleItemEdit == ScheduleItemEditType.Update)
                {
                    // 管理者作成スケジュール
                    if (string.IsNullOrWhiteSpace((this.Item.ScheduleItem as CarShareScheduleItemModel).INPUT_SECTION_ID))
                    {
                        this.EntryButton.Visible = false;
                        this.EntryCopyButton.Visible = false;
                        this.DeleteButton.Visible = false;
                        this.TransferButton.Visible = false;
                    }
                }
            }

            // 機能制限
            if (IsRestriction)
            {
                this.DeleteButton.Visible = false;
                this.TransferButton.Visible = false;
            }

            /*********************************************************
             *  スケジュール利用車詳細情報なし暫定対応
             *  データ移行が問題なければ必要なし (2018/07/04 T.Ueda)
             *********************************************************/
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (!this.IsChildrenController<TextBox>(this.SinkDetailTableLayoutPanel))
            //{
            //    // 備考にタイトル（カテゴリ）をセット
            //    if (this.FunctionId == FunctionID.TestCar)
            //    {
            //        // 試験車日程
            //        this.SinkMemoTextBox.Text = (this.Item.ScheduleItem as TestCarScheduleItemModel).CATEGORY;
            //    }
            //    else if (this.FunctionId == FunctionID.CarShare)
            //    {
            //        // カーシェア日程
            //        this.SinkMemoTextBox.Text = (this.Item.ScheduleItem as CarShareScheduleItemModel).CATEGORY;
            //    }
            //    else if (this.FunctionId == FunctionID.OuterCar)
            //    {
            //        // 外製車日程
            //        this.SinkMemoTextBox.Text = (this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel).CATEGORY;
            //    }
            //}
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

        }

        //Append Start 2022/01/31 杉浦 項目を変動できるようにする
        private void InitGridView()
        {
            this.ScheduleItemDataGridView.Columns[0].ReadOnly = true;
            this.ScheduleItemDataGridView.Columns[1].ReadOnly = false;
            this.ScheduleItemDataGridView.Columns[2].Visible = false;
            this.ScheduleItemDataGridView.Columns[3].Visible = false;
            this.ScheduleItemDataGridView.Columns[0].Width = 150;
            this.ScheduleItemDataGridView.Columns[1].Width = 230;
            this.ScheduleItemDataGridView.Columns[0].DefaultCellStyle.BackColor = System.Drawing.Color.Aquamarine;
            this.ScheduleItemDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.ScheduleItemDataGridView.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.ScheduleItemDataGridView.ColumnHeadersVisible = false;
            this.ScheduleItemDataGridView.RowHeadersVisible = false;

            this.CarDataGridView.ReadOnly = true;
            this.CarDataGridView.Columns[2].Visible = false;
            this.CarDataGridView.Columns[3].Visible = false;
            this.CarDataGridView.Columns[0].Width = 150;
            this.CarDataGridView.Columns[1].Width = 230;
            this.CarDataGridView.ColumnHeadersVisible = false;
            this.CarDataGridView.RowHeadersVisible = false;
            this.CarDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.CarDataGridView.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.CarDataGridView.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;

        }

        private void SetGridView()
        {
            // 車両種別
            this.SinkCarClassLabel.Text = getCarClassString(this.SinkCarClass);
            var list = setList();
            if(list != null)
            {
                this.ScheduleItemDataGridView.DataSource = list;
                //Append Start 2023/10/15 杉浦 スケジュール詳細表示変更
                var num = list.FindIndex(x => x.category == "管理票番号");
                if (num >= 0)
                {
                    if (string.IsNullOrEmpty(list[num].item))
                    {
                        this.ScheduleItemDataGridView.Rows[num].Cells[0].Style.BackColor = System.Drawing.Color.Yellow;
                        this.ScheduleItemDataGridView.Rows[num].Cells[0].ToolTipText = "管理票Noが未記入のため、他機能が正常に動かない可能性があります。管理票Noの記入をお願いします。";
                    }
                }
                //Append End 2023/10/15 杉浦 スケジュール詳細表示変更
            }

        }
        //Append End 2022/01/31 杉浦 項目を変動できるようにする

        /// <summary>
        /// 画面初期化(車輛情報)
        /// </summary>
        private void ScheduleCarInitForm()
        {
            if (this.Item.ScheduleItemEdit == ScheduleItemEditType.Update)
            {
                // スケジュール利用車情報の取得
                this.ScheduleCar = this.GetScheduleCarDetail(this.Item.ID);

                // 車両種別の更新
                this.SinkCarClass = this.ScheduleCar?.CAR_CLASS ?? this.SinkCarClass;
            }

            // テーブル初期化
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SinkInitTable();
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // スケジュール利用車情報のセット
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SetScheduleCar2SinkCar();
            this.SetGridView();
            //Update End 2022/01/31 杉浦 項目を変動できるようにする
        }
        /// <summary>
        /// 画面初期化(試験車)
        /// </summary>
        private void TestCarInitForm()
        {
            var item = this.Item.ScheduleItem as TestCarScheduleItemModel;

            // 試験車リストボタン
            this.TestCarListButton.Visible = true;

            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            // 内製車リストボタン
            this.InsideCarListButton.Visible = true;

            // 外製車リストボタン
            this.OutsideCarListButton.Visible = true;

            // 最終予約可能日
            this.LastReservationDateLayoutPanel.Visible = false;
            //this.LastReservationDateTimePicker.Visible = true;
            //this.LastReservationDateTimePicker.Enabled = false;
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

            // 管理票番号がある場合
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (!string.IsNullOrWhiteSpace(this.SinkControlNoTextBox.Text))
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            if (num < 0)
            {
                if (!string.IsNullOrWhiteSpace(this.管理票No))
                {
                    //試験車
                    this.TestCar = this.GetCommonCar4TestCar();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
            }
            else
            {
                //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                if (!string.IsNullOrWhiteSpace(ScheduleItemDataGridView.Rows[num].Cells[1].Value == null ? null : ScheduleItemDataGridView.Rows[num].Cells[1].Value.ToString()))
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                {
                    //試験車
                    this.TestCar = this.GetCommonCar4TestCar();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
                //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            }
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            // テーブル初期化
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SourceInitTable();
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // 試験車情報のセット
            this.SetTestCar2SourceCar();
        }
        /// <summary>
        /// 画面初期化(カーシェア)
        /// </summary>
        private void CarShareInitForm()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            var item = this.Item.ScheduleItem as CarShareScheduleItemModel;

            // FLAG_要予約許可
            this.SjsbReservationCheckBox.Visible = isManagement;

            // 管理者以外作成
            if (!string.IsNullOrWhiteSpace(item.INPUT_SECTION_ID) && this.Item.ScheduleItemEdit == ScheduleItemEditType.Update)
            {
                this.SjsbReservationCheckBox.Visible = false;
            }

            // 最終予約可能日
            this.LastReservationDateLayoutPanel.Visible = true;
            this.LastReservationDateTimePicker.Visible = true;

            // FLAG_要予約許可
            this.SjsbReservationCheckBox.Checked = item.FLAG_要予約許可 == 1;

            // 最終予約可能日
            this.LastReservationDateTimePicker.Value = item.最終予約可能日;

            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            // 試験車リストボタン
            this.TestCarListButton.Visible = true;
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

            // 内製車リストボタン
            this.InsideCarListButton.Visible = true;

            // 外製車リストボタン
            this.OutsideCarListButton.Visible = true;

            // 管理票番号がある場合
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (!string.IsNullOrWhiteSpace(this.SinkControlNoTextBox.Text))
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            if (num < 0)
            {
                if (!string.IsNullOrWhiteSpace(this.管理票No))
                {
                    //試験車
                    this.TestCar = this.GetCommonCar4TestCar();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
            }
            else
            {
                //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                if (!string.IsNullOrWhiteSpace(ScheduleItemDataGridView.Rows[num].Cells[1].Value == null ? null : ScheduleItemDataGridView.Rows[num].Cells[1].Value.ToString()))
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                {
                    //カーシェア車
                    this.CarShare = this.GetCommonCar4CarShare();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
                //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            }
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正

            // テーブル初期化
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SourceInitTable();
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // 試験車情報のセット
            this.SetCarShare2SourceCar();
        }
        /// <summary>
        /// 画面初期化(外製車)
        /// </summary>
        private void OuterCarInitForm()
        {
            var item = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;

            // FLAG_要予約許可
            this.SjsbReservationCheckBox.Visible = true;

            // 最終予約可能日
            this.LastReservationDateLayoutPanel.Visible = true;
            this.LastReservationDateTimePicker.Visible = true;

            // FLAG_要予約許可（外製車日程のみ基本的に予約許可必須のため、nullの時もデフォルトでチェックを付ける）
            this.SjsbReservationCheckBox.Checked = item.FLAG_要予約許可 == 1;
            if(item.FLAG_要予約許可 == null) { this.SjsbReservationCheckBox.Checked = true; }

            // 最終予約可能日
            this.LastReservationDateTimePicker.Value = item.最終予約可能日;

            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
            // 試験車リストボタン
            this.TestCarListButton.Visible = true;
            //Append Start 2022/02/03 杉浦 試験車日程の車も登録する

            // 内製車リストボタン
            this.InsideCarListButton.Visible = true;

            // 外製車リストボタン
            this.OutsideCarListButton.Visible = true;

            // 管理票番号がある場合
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (!string.IsNullOrWhiteSpace(this.SinkControlNoTextBox.Text))
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            if (num < 0)
            {
                if (!string.IsNullOrWhiteSpace(this.管理票No))
                {
                    //試験車
                    this.TestCar = this.GetCommonCar4TestCar();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
            }
            else
            {
                //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                if (!string.IsNullOrWhiteSpace(ScheduleItemDataGridView.Rows[num].Cells[1].Value == null ? null : ScheduleItemDataGridView.Rows[num].Cells[1].Value.ToString()))
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                {
                    //カーシェア車
                    this.CarShare = this.GetCommonCar4CarShare();

                    // 車両種別のセット
                    this.SourceCarClass = this.SinkCarClass;
                }
                //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            }
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正

            // テーブル初期化
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SourceInitTable();
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // 試験車情報のセット
            this.SetCarShare2SourceCar();
        }
        /// <summary>
        /// 情報先(車輛情報)テーブル初期化
        /// </summary>
        private void SinkInitTable()
        {
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //// 車両区分(0:試験車, 1:内製車, 2:外製車)
            //if (this.SinkCarClass != 2)
            //{
            //    // 開発符号
            //    //this.SinkCommonLabel1.Text = "開発符号";
            //    //this.SinkCommonCell1TextBox.Tag = "ItemName(開発符号);Byte(20)";

            //    // 試作時期
            //    //this.SinkCommonLabel2.Text = "試作時期";
            //    //this.SinkCommonCell2TextBox.Tag = "ItemName(試作時期);Byte(20)";

            //    // 号車
            //    //this.SinkCommonLabel3.Text = "号車";
            //    //this.SinkCommonCell3TextBox.Visible = true;
            //}
            //else
            //{
            //    // メーカー名
            //    //this.SinkCommonLabel1.Text = "メーカー名";
            //    //this.SinkCommonCell1TextBox.Tag = "ItemName(メーカー名);Byte(50)";

            //    // 外製車名
            //    //this.SinkCommonLabel2.Text = "外製車名";
            //    //this.SinkCommonCell2TextBox.Tag = "ItemName(外製車名);Byte(50)";

            //    // 号車
            //    //this.SinkCommonLabel3.Text = string.Empty;
            //    //this.SinkCommonCell3TextBox.Visible = false;
            //}
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする
        }
        /// <summary>
        /// 情報元(試験車基本情報)テーブル初期化
        /// </summary>
        private void SourceInitTable()
        {
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //// 車両区分(0:試験車, 1:内製車, 2:外製車)
            //if (this.SourceCarClass != 2)
            //{
            //    // 開発符号
            //    this.SourceCommonLabel1.Text = "開発符号";
            //    this.SourceCommonCell1TextBox.Tag = "ItemName(開発符号);Byte(20)";

            //    // 試作時期
            //    this.SourceCommonLabel2.Text = "試作時期";
            //    this.SourceCommonCell2TextBox.Tag = "ItemName(試作時期);Byte(20)";

            //    // 号車
            //    this.SourceCommonLabel3.Text = "号車";
            //    this.SourceCommonCell3TextBox.Visible = true;
            //}
            //else
            //{
            //    // メーカー名
            //    this.SourceCommonLabel1.Text = "メーカー名";
            //    this.SourceCommonCell1TextBox.Tag = "ItemName(メーカー名);Byte(50)";

            //    // 外製車名
            //    this.SourceCommonLabel2.Text = "外製車名";
            //    this.SourceCommonCell2TextBox.Tag = "ItemName(外製車名);Byte(50)";

            //    // 号車
            //    this.SourceCommonLabel3.Text = string.Empty;
            //    this.SourceCommonCell3TextBox.Visible = false;
            //}
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする
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
            // 初期情報クリア
            this.ScheduleCar = null;

            // 選択情報クリア
            this.TestCar = null;
            this.CarShare = null;

            // 車輛情報クリア
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.ClearChildrenController<TextBox>(this.SinkDetailTableLayoutPanel);
            this.ClearScheduleItemDataGridView();
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            // 試験車情報クリア
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.ClearChildrenController<TextBox>(this.SourceDetailTableLayoutPanel);
            this.ClearCarDataGridView();
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            //Append Start 2022/01/31 杉浦 項目を変動できるようにする
            this.InitGridView();
            //Append End 2022/01/31 杉浦 項目を変動できるようにする

            // SJSB予約許可
            this.SjsbReservationCheckBox.Checked = false;

            // 最終予約可能日
            this.LastReservationDateTimePicker.Value = null;
        }
        #endregion

        #region 車リストボタンクリック
        /// <summary>
        /// 試験車リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestCarListButton_Click(object sender, EventArgs e)
        {
            //試験車一覧画面表示
            FormControlUtil.FormWait(this, this.ShowTestCarList);
        }
        /// <summary>
        /// 内製車リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsideCarListButton_Click(object sender, EventArgs e)
        {
            //カーシェア一覧画面表示
            FormControlUtil.FormWait(this, () => this.ShowCarShareList(true));
        }
        /// <summary>
        /// 外製車リストボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutsideCarListButton_Click(object sender, EventArgs e)
        {
            //カーシェア一覧画面表示
            FormControlUtil.FormWait(this, () => this.ShowCarShareList(false));
        }

        /// <summary>
        /// 試験車一覧画面表示
        /// </summary>
        private void ShowTestCarList()
        {
            using (var form = new TestCarListForm())
            {
                // OKの場合は項目名を設定
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var isCopy = true;

                    // 車輛情報の入力チェック
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (this.IsChildrenController<TextBox>(this.SinkDetailTableLayoutPanel, new List<Control>() { this.SinkMemoTextBox }))
                    var itemList = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                    //Update Start 2022/02/16 杉浦 項目が入力されていたらメッセージが出るようにする
                    //if (itemList.Where(x => x.category != "備考").Any(x => x.modified == true))
                    if (itemList.Where(x => x.category != "備考").Any(x => x.item != null))
                    //Update End 2022/01/31 杉浦 項目が入力されていたらメッセージが出るようにする
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        // 反映確認
                        isCopy = Messenger.Confirm(Resources.KKM03034) == DialogResult.Yes;
                    }

                    // 試験車
                    this.TestCar = form.SelectedTestCar;

                    // 車両区分
                    //Delete Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //this.SourceCarClass = 0;
                    //Delete End 2022/03/07 杉浦 スケジュール詳細不具合修正

                    // テーブル初期化
                    //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                    //this.SourceInitTable();
                    //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                    //試験車情報のセット
                    this.SetTestCar2SourceCar();

                    if (isCopy)
                    {
                        // 車両区分
                        this.SinkCarClass = this.SourceCarClass;

                        // テーブル初期化
                        //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                        //this.SinkInitTable();
                        //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                        // データのコピー
                        this.CopySourceCar2SinkCar();
                    }
                }
            }
        }
        /// <summary>
        /// カーシェア一覧画面表示
        /// </summary>
        /// <param name="isNaisei">内製車一覧可否</param>
        private void ShowCarShareList(bool isNaisei)
        {
            var cond = new CarShareInnerSearchInModel();
            var item = this.Item.ScheduleItem;

            // 内製車の場合は条件（車系）追加
            if (isNaisei)
            {
                // カーシェア日程
                if (this.FunctionId == FunctionID.CarShare)
                {
                    cond.車系 = (item as CarShareScheduleItemModel).CAR_GROUP;
                }
                // 外製車日程
                else if (this.FunctionId == FunctionID.OuterCar)
                {
                    cond.車系 = (item as OuterCarScheduleItemGetOutModel).CAR_GROUP;
                }
                //Append Start 2022/02/03 杉浦 試験車日程の車も登録する
                // 試験車日程
                else if (this.FunctionId == FunctionID.TestCar)
                {
                    cond.車系 = (item as TestCarScheduleItemModel).車系;
                    if (!cond.車系.Contains("系")) cond.車系 = cond.車系 + "系";
                }
                //Append End 2022/02/03 杉浦 試験車日程の車も登録する
            }
            // 外製車の場合は条件（カーシェア利用フラグ）追加
            else
            {
                // カーシェア日程
                if (this.FunctionId == FunctionID.CarShare)
                {
                    cond.CARSHARE_FLG = true;
                }
            }

            using (var form = new CarShareListForm() { IsNaisei = isNaisei, SearchCondition = cond })
            {
                // OKの場合は項目名を設定
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var isCopy = true;

                    // 車輛情報の入力チェック
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (this.IsChildrenController<TextBox>(this.SinkDetailTableLayoutPanel, new List<Control>() { this.SinkMemoTextBox }))
                    var itemList = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                    //Update Start 2022/02/16 杉浦 項目が入力されていたらメッセージが出るようにする
                    //if (itemList.Where(x => x.category != "備考").Any(x => x.modified == true))
                    if (itemList.Where(x => x.category != "備考").Any(x => x.item != null))
                    //Update End 2022/01/31 杉浦 項目が入力されていたらメッセージが出るようにする
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                            // 反映確認
                            isCopy = Messenger.Confirm(Resources.KKM03034) == DialogResult.Yes;
                    }
                    
                    // カーシェア車
                    this.CarShare = form.SelectedCarShare;

                    // 車両区分
                    //Delete Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //this.SourceCarClass = isNaisei ? 1 : 2;
                    //Delete End 2022/03/07 杉浦 スケジュール詳細不具合修正

                    // テーブル初期化
                    //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                    //this.SourceInitTable();
                    //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                    // 試験車情報のセット
                    this.SetCarShare2SourceCar();

                    if (isCopy)
                    {
                        // 車両区分
                        this.SinkCarClass = this.SourceCarClass;

                        // テーブル初期化
                        //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                        //this.SinkInitTable();
                        //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                        // データのコピー
                        this.CopySourceCar2SinkCar();
                    }
                    
                    if (this.FunctionId == FunctionID.OuterCar)
                    {
                        this.SjsbReservationCheckBox.Checked = true;
                    }

                    //コピー終了後、最終予約可能日の確認メッセージボックスを表示する（現行も確認したが、既に入力されていてもいなくてもメッセージを出していた）
                    //Update Start 2022/01/31 杉浦 試験車日程では最終予約可能日の設定はしない
                    //if (this.LastReservationDateTimePicker.SelectedDate == null)
                    if (this.FunctionId != FunctionID.TestCar && this.LastReservationDateTimePicker.SelectedDate == null)
                    //Update End 2022/01/31 杉浦 項目が入力されていたらメッセージが出るようにする
                        Messenger.Info(Resources.KKM01006);
                }
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
                var type = this.Item.ScheduleItemEdit;

                //スケジュール項目のチェック
                if (this.IsEntryScheduleItem(type) == true)
                {
                    //スケジュール項目の登録
                    this.EntryScheduleItem(type);
                }
            });
        }
        #endregion

        #region 登録(コピー)ボタンクリック
        /// <summary>
        /// 登録(コピー)ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryCopyButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                var type = this.Item.ScheduleItemEdit;

                //スケジュール項目のチェック
                if (this.IsEntryScheduleItem(type) == true)
                {
                    //スケジュール項目の登録
                    this.EntryScheduleItem(type, false);

                    // 試験車情報のクリア
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //this.ClearChildrenController<TextBox>(this.SourceDetailTableLayoutPanel);
                    this.ClearCarDataGridView();
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする

                    // 管理票番号のクリア
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //this.SinkControlNoTextBox.Text = string.Empty;
                    var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                    var num = list.FindIndex(x => x.category == "管理票番号");
                    //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    if (num >= 0)
                    {
                        this.ScheduleItemDataGridView.Rows[num].Cells[1].Value = string.Empty;
                    }
                    //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    this.管理票No = string.Empty;
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする

                    //Append Start 2022/01/31 杉浦 項目を変動できるようにする
                    this.InitGridView();
                    //Append End 2022/01/31 杉浦 項目を変動できるようにする
                }
            });
        }
        #endregion

        #region 管理移譲ボタンクリック
        /// <summary>
        /// 管理移譲ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>現在はSJSB(管理者)→SKCKA(自部管理者)、SKCKA(自部管理者)→SJSB(管理者)にのみ対応</remarks>
        private void TransferButton_Click(object sender, EventArgs e)
        {
            // SJSB(管理者)→SKCKA(自部管理者)
            var str = new string[] { "総括部署", "SKC管理課" }; 

            // 自部署作成スケジュール
            if (!string.IsNullOrWhiteSpace((this.Item.ScheduleItem as CarShareScheduleItemModel).INPUT_SECTION_ID))
            {
                // SKCKA(自部管理者)→SJSB(管理者)
                str = new string[] { "SKC管理課", "総括部署" };
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 移譲可否を問い合せ
                if (Messenger.Confirm(String.Format(Resources.KKM01021, str)) == DialogResult.Yes)
                {
                    var type = ScheduleItemEditType.Transfer;

                    //スケジュール項目のチェック
                    if (this.IsEntryScheduleItem(type) == true)
                    {
                        //スケジュール項目の登録
                        this.EntryScheduleItem(type);
                    }
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
                //削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00007) == DialogResult.Yes)
                {
                    var type = ScheduleItemEditType.Delete;

                    //スケジュール項目のチェック
                    if (this.IsEntryScheduleItem(type) == true)
                    {
                        //スケジュール項目の登録
                        this.EntryScheduleItem(type);
                    }
                }
            });
        }
        #endregion

        #region 一括反映リンククリック
        private void ReflectionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 試験車情報が入力されていない場合
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (!this.IsChildrenController<TextBox>(this.SourceDetailTableLayoutPanel))
            var souceDetail = (List<GridViewDisplayItemModel>)this.CarDataGridView.DataSource;
            if (!souceDetail.Any(x => x.item != null))
            //Update End 2022/01/31 杉浦 項目を変動できるようにする
            {
                return;
            }

            // 車輛情報が入力されている場合
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //if (this.IsChildrenController<TextBox>(this.SinkDetailTableLayoutPanel, new List<Control>() { this.SinkMemoTextBox }))
            var itemList = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            //Update Start 2022/02/16 杉浦 項目が入力されていたらメッセージが出るようにする
            //if (itemList.Where(x => x.category != "備考").Any(x => x.modified == true))
            if (itemList.Where(x => x.category != "備考").Any(x => x.item != null))
            //Update End 2022/01/31 杉浦 項目が入力されていたらメッセージが出るようにする
            //Update End 2022/01/31 杉浦 項目を変動できるようにする
            {
                // 反映確認
                if (Messenger.Confirm(Resources.KKM03034) != DialogResult.Yes)
                {
                    return;
                }
            }

            // 車両区分
            this.SinkCarClass = this.SourceCarClass;

            // テーブル初期化
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SinkInitTable();
            //Delete End 2022/01/31 杉浦 項目を変動できるようにする

            // データのコピー
            this.CopySourceCar2SinkCar();
        }
        #endregion

        #region スケジュール項目のチェック
        /// <summary>
        /// スケジュール項目のチェック
        /// </summary>
        /// <param name="type">スケジュール項目編集区分</param>
        /// <returns>チェック可否</returns>
        private bool IsEntryScheduleItem(ScheduleItemEditType type)
        {
            var flg = true;
            var map = new Dictionary<Control, Func<Control, string, string>>();
            
            switch (type)
            {
                case ScheduleItemEditType.Update:
                    //最終予約可能日が未来日の場合、同じ管理票番号の項目の最終予約可能日を確認する
                    string code = "";
                    if (this.FunctionId == FunctionID.CarShare)
                    {
                        var model = this.Item.ScheduleItem as CarShareScheduleItemModel;
                        if (this.LastReservationDateTimePicker.Value != null &&
                            this.LastReservationDateTimePicker.SelectedDate != model.最終予約可能日)
                        {
                            var manageNum = string.IsNullOrEmpty(this.Item.KanriNo) ? model.管理票番号 : this.Item.KanriNo;
                            if (!string.IsNullOrEmpty(manageNum))
                            {
                                var res = HttpUtil.GetResponse<CarShareScheduleSearchModel, CarShareScheduleItemModel>(ControllerType.CarShareScheduleItem, new CarShareScheduleSearchModel { 管理票番号 = manageNum });
                                if (res != null && res.Status == Const.StatusSuccess)
                                {
                                    var list = new List<CarShareScheduleItemModel>();
                                    list.AddRange(res.Results);
                                    foreach (var d in list.Where(x => x.CATEGORY_ID != this.Item.ID &&
                                        (x.最終予約可能日 >= DateTime.Now.Date || x.最終予約可能日 == null)).Select(x => x.GENERAL_CODE).Distinct())
                                    {
                                        code += d + " ";
                                    }
                                }
                            }
                        }
                    }
                    else if (this.FunctionId == FunctionID.OuterCar)
                    {
                        var model = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;
                        if (this.LastReservationDateTimePicker.Value != null &&
                            this.LastReservationDateTimePicker.SelectedDate != model.最終予約可能日)
                        {
                            var manageNum = string.IsNullOrEmpty(this.Item.KanriNo) ? model.管理票NO : this.Item.KanriNo;
                            if (!string.IsNullOrEmpty(manageNum))
                            {
                                var res = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, OuterCarScheduleItemGetOutModel>(ControllerType.OuterCarScheduleItem, new OuterCarScheduleItemGetInModel { 管理票NO = manageNum });
                                if (res != null && res.Status == Const.StatusSuccess)
                                {
                                    var list = new List<OuterCarScheduleItemGetOutModel>();
                                    list.AddRange(res.Results);
                                    foreach (var d in list.Where(x => x.CATEGORY_ID != this.Item.ID &&
                                        (x.最終予約可能日 >= DateTime.Now.Date || x.最終予約可能日 == null)).Select(x => x.GENERAL_CODE).Distinct())
                                    {
                                        code += d + " ";
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        Messenger.Warn(code + "に既に同車両(管理票No" + this.Item.KanriNo + ")の登録がある為、変更出来ません。");
                        return false;
                    }
                    break;
            }

            //スケジュール項目編集区分ごとの分岐
            switch (type)
            {
                //登録
                //更新
                case ScheduleItemEditType.Insert:
                case ScheduleItemEditType.Update:

                    // 最終予約可能日
                    map[this.LastReservationDateTimePicker] = (c, name) =>
                    {
                        //システム日付より過去は入力不可
                        if (this.LastReservationDateTimePicker.Value != null &&
                        this.LastReservationDateTimePicker.SelectedDate.Value.Date < DateTime.Now.Date)
                        {
                            return "最終予約可能日は本日より未来日を設定してください。";
                        }

                        return string.Empty;
                    };

                    //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                    // 管理票番号
                    //map[this.SinkControlNoTextBox] = (c, name) =>
                    //{
                    //    var carList = this.GetScheduleCarList(this.SinkControlNoTextBox.Text);
                    //    //重複チェック
                    //    if (!string.IsNullOrWhiteSpace(this.SinkControlNoTextBox.Text) && this.SinkControlNoTextBox.Text.Length <= 20 &&
                    //        carList.Any(x => x.CATEGORY_ID != this.Item.ID))
                    //    {
                    //        //return Resources.KKM03035;
                    //        string code = "";
                    //        foreach(var d in carList.Where(x => x.CATEGORY_ID != this.Item.ID).Select(x => x.GENERAL_CODE))
                    //        {
                    //            code += d + " ";
                    //        }
                    //        return (code + "に既に同車両(管理票No" + this.SinkControlNoTextBox.Text + ")の登録がある為、変更出来ません。");
                    //    }

                    //    return string.Empty;
                    //};

                    //// 備考(代表)
                    //map[this.SinkMemoTextBox] = (c, name) =>
                    //{
                    //    //車輛情報チェック（複合必須チェック）
                    //    if (!this.IsChildrenController<TextBox>(this.SinkDetailTableLayoutPanel))
                    //    {
                    //        this.SinkDetailTableLayoutPanel.BackColor = Const.ErrorBackColor;

                    //        return string.Format(Resources.KKM00001, "車輛情報のいずれか");
                    //    }

                    //    this.SinkDetailTableLayoutPanel.BackColor = Const.DefaultBackColor;

                    //    return string.Empty;
                    //};
                    //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                    //入力がOKかどうか
                    var msg = Validator.GetFormInputErrorMessage(this, map, false, false);

                    //Append Start 2022/01/31 杉浦 項目を変動できるようにする
                    for(int i = 0; i < this.ScheduleItemDataGridView.RowCount; i++)
                    {
                        if(this.ScheduleItemDataGridView.Rows[i].Cells[0].Value.ToString() != "備考")
                        {
                            int byteCount = (int)this.ScheduleItemDataGridView.Rows[i].Cells[2].Value;
                            string content = this.ScheduleItemDataGridView.Rows[i].Cells[1].Value?.ToString();
                            var byteFlg = content != null ? StringUtil.SjisByteLength(content) <= byteCount : true;
                            if (!byteFlg)
                            {
                                this.ScheduleItemDataGridView.Rows[i].Cells[1].Style.BackColor = Const.ErrorBackColor;
                                msg += string.Format(Resources.KKM00027, this.ScheduleItemDataGridView.Rows[i].Cells[0].Value.ToString());
                            }
                            else
                            {
                                this.ScheduleItemDataGridView.Rows[i].Cells[1].Style.BackColor = Const.DefaultBackColor;
                            }
                        }
                    }

                    for (int i = 0; i < this.CarDataGridView.RowCount; i++)
                    {
                        if (this.CarDataGridView.Rows[i].Cells[0].Value.ToString() != "備考")
                        {
                            int byteCount = (int)this.CarDataGridView.Rows[i].Cells[2].Value;
                            string content = this.CarDataGridView.Rows[i].Cells[2].Value?.ToString();
                            var byteFlg = StringUtil.SjisByteLength(content) <= byteCount;
                            if (!byteFlg)
                            {
                                this.CarDataGridView.Rows[i].Cells[1].Style.BackColor = Const.ErrorBackColor;
                                msg += string.Format(Resources.KKM00027, this.CarDataGridView.Rows[i].Cells[0].Value.ToString());
                            }else
                            {
                                this.CarDataGridView.Rows[i].Cells[1].Style.BackColor = System.Drawing.Color.LightGray;
                            }
                        }
                    }

                    // 管理票番号
                    var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                    var num = list.FindIndex(x => x.category == "管理票番号");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //var controlNo = this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
                    var controlNo = num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    var carList = this.GetScheduleCarList(controlNo);
                    //重複チェック
                    //Update Start 2022/02/03 杉浦 試験車日程の車も登録する
                    //if (!string.IsNullOrWhiteSpace(controlNo) && controlNo.Length <= 20 && carList.Any(x => x.CATEGORY_ID != this.Item.ID))
                    //{
                    //    string code = "";
                    //    foreach (var d in carList.Where(x => x.CATEGORY_ID != this.Item.ID).Select(x => x.GENERAL_CODE))
                    //    {
                    //        code += d + " ";
                    //    }
                    //    msg += (code + "に既に同車両(管理票No" + this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString() + ")の登録がある為、変更出来ません。");
                    //    this.ScheduleItemDataGridView.Rows[num].Cells[1].Style.BackColor = Const.ErrorBackColor;
                    //}
                    if (this.FunctionId == FunctionID.TestCar)
                    {
                        if (!string.IsNullOrWhiteSpace(controlNo) && controlNo.Length <= 20 && carList.Any(x => x.TESTCAR_ITEM_ID != null && x.TESTCAR_ITEM_ID != this.Item.ID))
                        {
                            string code = "";
                            foreach (var d in carList.Where(x => x.TESTCAR_ITEM_ID != this.Item.ID).Select(x => x.TESTCAR_ITEM_CODE))
                            {
                                code += d + " ";
                            }
                            msg += (code + "に既に同車両(管理票No" + controlNo + ")の登録がある為、変更出来ません。");
                            if (num >= 0) this.ScheduleItemDataGridView.Rows[num].Cells[1].Style.BackColor = Const.ErrorBackColor;
                        }
                    }
                    else if (this.FunctionId == FunctionID.OuterCar)
                    {
                        if (!string.IsNullOrWhiteSpace(controlNo) && controlNo.Length <= 20 && carList.Any(x => x.OUTERCAR_ITEM_ID != null && x.OUTERCAR_ITEM_ID != this.Item.ID))
                        {
                            string code = "";
                            foreach (var d in carList.Where(x => x.OUTERCAR_ITEM_ID != this.Item.ID).Select(x => x.OUTERCAR_ITEM_CODE))
                            {
                                code += d + " ";
                            }
                            msg += (code + "に既に同車両(管理票No" + controlNo + ")の登録がある為、変更出来ません。");
                            if (num >= 0) this.ScheduleItemDataGridView.Rows[num].Cells[1].Style.BackColor = Const.ErrorBackColor;
                        }
                    }
                    else if (this.FunctionId == FunctionID.CarShare)
                    {
                        if (!string.IsNullOrWhiteSpace(controlNo) && controlNo.Length <= 20 && carList.Any(x => x.CARSHARING_ITEM_ID != null && x.CARSHARING_ITEM_ID != this.Item.ID))
                        {
                            string code = "";
                            foreach (var d in carList.Where(x => x.CARSHARING_ITEM_ID != this.Item.ID).Select(x => x.CARSHARING_ITEM_CODE))
                            {
                                code += d + " ";
                            }
                            msg += (code + "に既に同車両(管理票No" + controlNo + ")の登録がある為、変更出来ません。");
                            if (num >= 0) this.ScheduleItemDataGridView.Rows[num].Cells[1].Style.BackColor = Const.ErrorBackColor;
                        }
                    }else
                    {
                        this.ScheduleItemDataGridView.Rows[num].Cells[1].Style.BackColor = Const.DefaultBackColor;
                    }
                    //Update Start 2022/02/03 杉浦 試験車日程の車も登録する
                    
                    // 備考(代表)
                    //車輛情報チェック（複合必須チェック）
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (!list.Any(x => x.modified == true))
                    if (!list.Any(x => x.item != null))
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    {
                        this.ScheduleItemDataGridView.Columns[1].DefaultCellStyle.BackColor = Const.ErrorBackColor;

                        msg += string.Format(Resources.KKM00001, "車輛情報のいずれか");
                    }else
                    {
                        this.ScheduleItemDataGridView.Columns[1].DefaultCellStyle.BackColor = Const.DefaultBackColor;

                    }
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする

                    if (msg != "")
                    {
                        Messenger.Warn(msg);

                        flg = false;
                        return flg;
                    }
                    break;
            }

            var item = this.Item.ScheduleItem;

            //スケジュール項目編集区分ごとの分岐
            switch (type)
            {
                //更新
                //削除
                //移譲
                case ScheduleItemEditType.Update:
                case ScheduleItemEditType.Delete:
                case ScheduleItemEditType.Transfer:
                    //スケジュール項目が存在しているかどうか
                    item = GetScheduleItem();
                    if (item == null)
                    {
                        //存在していない場合はエラー
                        Messenger.Warn(Resources.KKM00021);
                        flg = false;
                    }
                    else
                    {
                        //スケジュール再設定
                        this.Item.ScheduleItem = item;
                    }
                    break;
            }

            //スケジュール項目編集区分ごとの分岐
            switch (type)
            {
                //削除
                case ScheduleItemEditType.Delete:
                    //スケジュールがある場合はエラー
                    if (this.GetScheduleList()?.Any() == true)
                    {
                        Messenger.Warn(Resources.KKM00033);
                        return false;
                    }
                    break;
            }

            return flg;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// スケジュール項目の取得
        /// </summary>
        /// <returns></returns>
        private item GetScheduleItem()
        {
            var res = new ResponseDto<item>();

            //APIで取得
            if (this.FunctionId == FunctionID.TestCar)
            {
                // 試験車日程
                res = HttpUtil.GetResponse<TestCarScheduleSearchModel, item>(ControllerType.TestCarScheduleItem, new TestCarScheduleSearchModel { ID = this.Item.ID });
            }
            else if (this.FunctionId == FunctionID.CarShare)
            {
                // カーシェア日程
                res = HttpUtil.GetResponse<CarShareScheduleSearchModel, item>(ControllerType.CarShareScheduleItem, new CarShareScheduleSearchModel { ID = this.Item.ID });
            }
            else if (this.FunctionId == FunctionID.OuterCar)
            {
                // 外製車日程
                res = HttpUtil.GetResponse<OuterCarScheduleItemGetInModel, item>(ControllerType.OuterCarScheduleItem, new OuterCarScheduleItemGetInModel { ID = this.Item.ID });
            }

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results?.OfType<item>().FirstOrDefault();
        }

        /// <summary>
        /// スケジュールの取得
        /// </summary>
        /// <returns></returns>
        private IEnumerable<schedule> GetScheduleList()
        {
            var res = new ResponseDto<schedule>();

            //APIで取得
            if (this.FunctionId == FunctionID.TestCar)
            {
                // 試験車日程
                res = HttpUtil.GetResponse<TestCarScheduleSearchModel, schedule>(ControllerType.TestCarSchedule, new TestCarScheduleSearchModel { CATEGORY_ID = this.Item.ID });
            }
            else if (this.FunctionId == FunctionID.CarShare)
            {
                // カーシェア日程
                res = HttpUtil.GetResponse<CarShareScheduleSearchModel, schedule>(ControllerType.CarShareSchedule, new CarShareScheduleSearchModel { CATEGORY_ID = this.Item.ID });
            }
            else if (this.FunctionId == FunctionID.OuterCar)
            {
                // 外製車日程
                res = HttpUtil.GetResponse<OuterCarScheduleGetInModel, schedule>(ControllerType.OuterCarSchedule, new OuterCarScheduleGetInModel { CATEGORY_ID = this.Item.ID });
            }

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results?.OfType<schedule>().ToList();
        }

        /// <summary>
        /// 車情報の取得
        /// </summary>
        /// <params name="no">管理票NO</params>
        /// <returns>TestCarCommonModel</returns>
        private TestCarCommonModel GetCommonCarInfo(string no)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.ControlSheetTestCar, new TestCarCommonSearchModel { 管理票NO = no });

            // レスポンスのセット
            if (res == null && res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results?.FirstOrDefault();
        }


        /// <summary>
        /// スケジュール利用車情報の取得
        /// </summary>
        /// <params name="no">管理票NO</params>
        /// <returns>ScheduleCarModel</returns>
        private List<ScheduleCarModel> GetScheduleCarList(string no)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<ScheduleCarSearchModel, ScheduleCarModel>(ControllerType.ScheduleCar, new ScheduleCarSearchModel { 管理票番号 = no });

            // レスポンスのセット
            if (res == null && res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results.ToList();
        }

        /// <summary>
        /// スケジュール利用車詳細情報の取得
        /// </summary>
        /// <params name="no">管理票NO</params>
        /// <returns>ScheduleCarDetailModel</returns>
        private ScheduleCarDetailModel GetScheduleCarDetail(long id)
        {
            //APIで取得
            var res = HttpUtil.GetResponse<ScheduleCarDetailSearchModel, ScheduleCarDetailModel>(ControllerType.ScheduleCarDetail, new ScheduleCarDetailSearchModel { CATEGORY_ID = id });

            // レスポンスのセット
            if (res == null && res.Status != Const.StatusSuccess)
            {
                return null;
            }

            return res.Results.FirstOrDefault();
        }
        #endregion

        #region スケジュール項目の登録
        /// <summary>
        /// スケジュール項目の登録
        /// </summary>
        /// <param name="type">スケジュール項目編集区分</param>
        /// <param name="isClose">クローズフラグ</param>
        private void EntryScheduleItem(ScheduleItemEditType type, bool isClose = true)
        {
            var res = new ResponseDto<item>();

            var msg = Resources.KKM00002;

            long categoryid = 0;

            //スケジュール編集区分ごとの分岐
            if (type == ScheduleItemEditType.Insert)
            {
                // APIで登録
                if (this.FunctionId == FunctionID.TestCar)
                {
                    // 試験車日程
                    res = HttpUtil.PostResponse<item>(ControllerType.TestCarScheduleItem, new[] { this.GetEntryTestCarScheduleItem() });

                    // カテゴリーID
                    categoryid = res.Results.OfType<TestCarScheduleItemModel>().FirstOrDefault().ID;
                }
                else if (this.FunctionId == FunctionID.CarShare)
                {
                    // カーシェア日程
                    res = HttpUtil.PostResponse<item>(ControllerType.CarShareScheduleItem, new[] { this.GetEntryCarShareScheduleItem() });

                    // カテゴリーID
                    categoryid = res.Results.OfType<CarShareScheduleItemModel>().FirstOrDefault().ID;
                }
                else if (this.FunctionId == FunctionID.OuterCar)
                {
                    // 外製車日程
                    res = HttpUtil.PostResponse<item>(ControllerType.OuterCarScheduleItem, this.GetEntryOuterCarScheduleItem());

                    // カテゴリーID
                    categoryid = res.Results.OfType<OuterCarScheduleItemGetOutModel>().FirstOrDefault().CATEGORY_ID;
                }
            }
            else if (type == ScheduleItemEditType.Update)
            {
                // APIで更新
                if (this.FunctionId == FunctionID.TestCar)
                {
                    // 試験車日程
                    res = HttpUtil.PutResponse<item>(ControllerType.TestCarScheduleItem, new[] { this.GetEntryTestCarScheduleItem() });
                }
                else if (this.FunctionId == FunctionID.CarShare)
                {
                    // カーシェア日程
                    res = HttpUtil.PutResponse<item>(ControllerType.CarShareScheduleItem, new[] { this.GetEntryCarShareScheduleItem() });
                }
                else if (this.FunctionId == FunctionID.OuterCar)
                {
                    // 外製車日程
                    res = HttpUtil.PutResponse<item>(ControllerType.OuterCarScheduleItem, this.GetEditOuterCarScheduleItem());
                }

                // カテゴリーID
                categoryid = this.Item.ID;
            }
            else if (type == ScheduleItemEditType.Delete)
            {
                msg = Resources.KKM00003;

                // APIで削除
                if (this.FunctionId == FunctionID.TestCar)
                {
                    // 試験車日程
                    res = HttpUtil.DeleteResponse<item>(ControllerType.TestCarScheduleItem, new[] { this.GetEntryTestCarScheduleItem() });
                }
                else if (this.FunctionId == FunctionID.CarShare)
                {
                    // カーシェア日程
                    res = HttpUtil.DeleteResponse<item>(ControllerType.CarShareScheduleItem, new[] { this.GetEntryCarShareScheduleItem() });
                }
                else if (this.FunctionId == FunctionID.OuterCar)
                {
                    // 外製車日程
                    res = HttpUtil.DeleteResponse<item>(ControllerType.OuterCarScheduleItem, this.GetDeleteOuterCarScheduleItem());
                }
            }
            else if (type == ScheduleItemEditType.Transfer)
            {
                msg = Resources.KKM00045;

                // 戻り値の設定
                if (this.FunctionId == FunctionID.CarShare)
                {
                    // カーシェア日程
                    res.Status = Const.StatusSuccess;
                }

                // カテゴリーID
                categoryid = this.Item.ID;
            }

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                //削除以外共通
                if (type != ScheduleItemEditType.Delete)
                {
                    var resDetail = new ResponseDto<ScheduleCarDetailModel>();

                    if (type == ScheduleItemEditType.Transfer)
                    {
                        // スケジュール利用車詳細(移譲)
                        resDetail = HttpUtil.PutResponse<ScheduleCarDetailModel>(ControllerType.ScheduleCarDetail, this.GetTransferScheduleCarDetail(categoryid));
                    }
                    else
                    {
                        // スケジュール利用車詳細(登録・更新)
                        resDetail = HttpUtil.PostResponse<ScheduleCarDetailModel>(ControllerType.ScheduleCarDetail, this.GetEntryScheduleCarDetail(categoryid));
                    }

                    //レスポンスが取得できたかどうか
                    if (resDetail == null || resDetail.Status != Const.StatusSuccess)
                    {
                        // 処理中止
                        return;
                    }
                }
               
                //リロードフラグON
                this.IsReload = true;

                //処理後メッセージ
                Messenger.Info(msg);

                if (isClose)
                {
                    ChangeView = false;
                    //フォームクローズ
                    base.FormOkClose();
                }
            }
        }

        /// <summary>
        /// スケジュール利用車登録データの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ScheduleCarDetailModel GetEntryScheduleCarDetail(long id)
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';

            Func<string, short?> getFlag = str =>
            {
                if (str == "あり" || str == "有" || str == "1")
                {
                    return (short?)1;
                }

                if (str == "なし" || str == "無" || str == "0")
                {
                    return (short?)0;
                }

                return null;
            };

            //Append Start 2022/01/31 杉浦 項目を変動できるようにする
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;

            Func<string, string> getItem = str =>
            {
                //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                if (str == "管理票番号")
                {
                    if(!list.Any(x => x.category == str))
                    {
                        return this.管理票No;
                    }else
                    {
                        return list.Find(x => x.category == str).item;
                    }
                }
                else
                {
                    //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    if(list.Any(x => x.category == str))
                    {
                        return list.Find(x => x.category == str).item;
                    }else
                    {
                        if (str == "ナビ付" || str == "ETC付" || str == "T/M")
                        {
                            if (str == "ナビ付")
                            {
                                var propertysub = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ナビ付");
                                return propertysub.GetValue(this.ScheduleCar) == null ? null : propertysub.GetValue(this.ScheduleCar).ToString();
                            }
                            else if (str == "ETC付")
                            {
                                var propertysub = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ETC付");
                                return propertysub.GetValue(this.ScheduleCar) == null ? null : propertysub.GetValue(this.ScheduleCar).ToString();
                            }
                            else if (str == "T/M")
                            {
                                var propertysub = typeof(ScheduleCarDetailModel).GetProperty("トランスミッション");
                                return propertysub.GetValue(this.ScheduleCar) == null ? null : propertysub.GetValue(this.ScheduleCar).ToString();
                            }
                        }
                        var property = typeof(ScheduleCarDetailModel).GetProperty(str);
                        return property.GetValue(this.ScheduleCar) == null ? null : property.GetValue(this.ScheduleCar).ToString();
                    }

                    //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                }
                //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            };
            //Append End 2022/01/31 杉浦 項目を変動できるようにする
            
            return new ScheduleCarDetailModel()
            {
                CATEGORY_ID = id,
                CAR_CLASS = (short?)this.SinkCarClass,
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //管理票番号 = this.SinkControlNoTextBox.Text,
                //車型 = this.SinkCarTypeTextBox.Text,
                //型式符号 = string.Empty,
                //駐車場番号 = this.SinkParkingNoTextBox.Text,
                //リースNO = this.SinkLeaseNoTextBox.Text,
                //開発符号 = this.SinkCarClass != 2 ? this.SinkCommonCell1TextBox.Text : string.Empty,
                //試作時期 = this.SinkCarClass != 2 ? this.SinkCommonCell2TextBox.Text : string.Empty,
                //メーカー名 = this.SinkCarClass == 2 ? this.SinkCommonCell1TextBox.Text : string.Empty,
                //外製車名 = this.SinkCarClass == 2 ? this.SinkCommonCell2TextBox.Text : string.Empty,
                //号車 = this.SinkCommonCell3TextBox.Text,
                //仕向地 = this.SinkShimukechiTextBox.Text,
                //E_G型式 = this.SinkEGTypeTextBox.Text,
                //排気量 = this.SinkDisplacementTextBox.Text,
                //トランスミッション = this.SinkTMTextBox.Text,
                //駆動方式 = this.SinkDriveSystemTextBox.Text,
                //グレード = string.Empty,
                //車体色 = this.SinkCarColorTextBox.Text,
                //固定資産NO = this.SinkFixedAssetNoTextBox.Text,
                //リース満了日 = this.SinkLeaseEndDateTextBox.Text,
                //処分予定年月 = this.SinkDisposalDateTextBox.Text,
                //登録ナンバー = this.SinkLicensePlateNumberTextBox.Text,
                //FLAG_ナビ付 = getFlag(this.SinkNaviTextBox.Text),
                //FLAG_ETC付 = getFlag(this.SinkETCTextBox.Text),
                //備考 = this.SinkMemoTextBox.Text,
                管理票番号 = getItem("管理票番号"),
                車型 = getItem("車型"),
                型式符号 = string.Empty,
                駐車場番号 = getItem("駐車場番号"),
                リースNO = getItem("リースNO"),
                開発符号 = this.SinkCarClass != 2 ? getItem("開発符号") : string.Empty,
                試作時期 = this.SinkCarClass != 2 ? getItem("試作時期") : string.Empty,
                メーカー名 = this.SinkCarClass == 2 ? getItem("メーカー名") : string.Empty,
                外製車名 = this.SinkCarClass == 2 ? getItem("外製車名") : string.Empty,
                号車 = getItem("号車"),
                仕向地 = getItem("仕向地"),
                E_G型式 = getItem("E_G型式"),
                排気量 = getItem("排気量"),
                //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                //トランスミッション = getItem("トランスミッション"),
                トランスミッション = getItem("T/M"),
                //Update End 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                駆動方式 = getItem("駆動方式"),
                グレード = string.Empty,
                車体色 = getItem("車体色"),
                固定資産NO = getItem("固定資産NO"),
                リース満了日 = getItem("リース満了日"),
                処分予定年月 = getItem("処分予定年月"),
                登録ナンバー = getItem("登録ナンバー"),
                FLAG_ナビ付 = getFlag(getItem("ナビ付")),
                FLAG_ETC付 = getFlag(getItem("ETC付")),
                備考 = getItem("備考"),
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                INPUT_DEPARTMENT_ID = isManagement ? null : SessionDto.DepartmentID,
                INPUT_SECTION_ID = isManagement ? null : SessionDto.SectionID,
                INPUT_SECTION_GROUP_ID = isManagement ? null : SessionDto.SectionGroupID,
                PERSONEL_ID = SessionDto.UserId
            };
        }

        /// <summary>
        /// スケジュール利用車移譲データの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>現在はSJSB(管理者)→SKCKA(自部管理者)、SKCKA(自部管理者)→SJSB(管理者)にのみ対応</remarks>
        private ScheduleCarDetailModel GetTransferScheduleCarDetail(long id)
        {
            var isAllToJibu = string.IsNullOrWhiteSpace((this.Item.ScheduleItem as CarShareScheduleItemModel).INPUT_SECTION_ID);

            /******************************************************************
             *  自部管理暫定対応
             *  今後、画面選択などで部署選択すれば必要なし (2019/12/25 T.Ueda)
             ******************************************************************/
            string departmentID = null;
            string sectionID = null;
            string sectionGroupID = null;

            if (isAllToJibu)
            {
                // SKC管理課部ID
                departmentID = "4";

                // SKC管理課（SORT_NO 昇順先頭代表部署）の部署情報取得
                var val = HttpUtil.GetResponse<SectionGroupSearchModel, SectionGroupModel>
                    (ControllerType.SectionGroup, new SectionGroupSearchModel() { DEPARTMENT_ID = departmentID })?.Results?.FirstOrDefault();

                // SKC管理課代表課ID
                sectionID = val?.SECTION_ID;

                // SKC管理課代表担当ID
                sectionGroupID = val?.SECTION_GROUP_ID;
            }

            return new ScheduleCarDetailModel()
            {
                CATEGORY_ID = id,
                INPUT_DEPARTMENT_ID = departmentID,
                INPUT_SECTION_ID = sectionID,
                INPUT_SECTION_GROUP_ID = sectionGroupID,
                PERSONEL_ID = SessionDto.UserId
            };
        }
        /// <summary>
        /// 試験車日程項目登録データの取得
        /// </summary>
        /// <returns></returns>
        private TestCarScheduleItemModel GetEntryTestCarScheduleItem()
        {
            var item = this.Item.ScheduleItem as TestCarScheduleItemModel;

            //スケジュール項目
            item.CATEGORY = this.MakeCategoryName();

            //管理票番号
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //item.管理票番号 = this.SinkControlNoTextBox.Text;
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            if (num < 0)
            {
                //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
                item.管理票番号 = this.管理票No;
            }else
            {
                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                item.管理票番号 = this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
                //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            }
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            //ユーザー情報設定
            item.SetUserInfo();

            return item;
        }
        /// <summary>
        /// カーシェア日程項目登録データの取得
        /// </summary>
        /// <returns></returns>
        private CarShareScheduleItemModel GetEntryCarShareScheduleItem()
        {
            var item = this.Item.ScheduleItem as CarShareScheduleItemModel;

            //FLAG_要予約許可
            item.FLAG_要予約許可 = Convert.ToInt16(this.SjsbReservationCheckBox.Checked);

            //スケジュール項目
            item.CATEGORY = this.MakeCategoryName();

            //最終予約可能日
            item.最終予約可能日 = this.LastReservationDateTimePicker.SelectedDate;

            //開発符号
            item.GENERAL_CODE = item.GENERAL_CODE ?? item.CAR_GROUP;

            //Update Start 2022/01/17 杉浦 入れ替え中車両の処理
            //if (item.管理票番号 != this.SinkControlNoTextBox.Text)
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            //if (item.管理票番号 != this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString())
            if (item.管理票番号 != (num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString()))
            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
            {
                item.入れ替え中車両 = null;
            }
            //Update End 2022/01/17 杉浦 入れ替え中車両の処理

            //管理票番号
            //Update Start 2022/01/17 杉浦 入れ替え中車両の処理
            //item.管理票番号 = this.SinkControlNoTextBox.Text;
            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            //item.管理票番号 = this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            item.管理票番号 = num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Update End 2022/01/17 杉浦 入れ替え中車両の処理

            //ユーザー情報設定
            item.SetUserInfo();

            return item;
        }

        /// <summary>
        /// 外製車日程項目登録データの取得
        /// </summary>
        /// <returns></returns>
        private OuterCarScheduleItemPostInModel GetEntryOuterCarScheduleItem()
        {
            var item = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;
            //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            var 管理票番号 = num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Append End 2022/01/17 杉浦 入れ替え中車両の処理

            return new OuterCarScheduleItemPostInModel
            {
                // 開発符号
                GENERAL_CODE = item.GENERAL_CODE ?? item.CAR_GROUP,
                // 管理票番号
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //管理票番号 = this.SinkControlNoTextBox.Text,
                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                //管理票番号 = this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString(),
                管理票番号 = 管理票番号,
                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                // カテゴリー
                CATEGORY = this.MakeCategoryName(),
                // 並び順
                SORT_NO = item.SORT_NO ?? 0,
                // 行数
                PARALLEL_INDEX_GROUP = item.PARALLEL_INDEX_GROUP,
                // 所属グループID
                SECTION_GROUP_ID = SessionDto.SectionGroupID,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId,
                // 最終予約可能日
                最終予約可能日 = this.LastReservationDateTimePicker.SelectedDate,
                // FLAG_要予約許可
                FLAG_要予約許可 = Convert.ToInt16(this.SjsbReservationCheckBox.Checked)
            };
        }
        /// <summary>
        /// 外製車日程項目更新データの取得
        /// </summary>
        /// <returns></returns>
        private OuterCarScheduleItemPutInModel GetEditOuterCarScheduleItem()
        {
            var item = this.Item.ScheduleItem as OuterCarScheduleItemGetOutModel;
            //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            var 管理票番号 = num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Append End 2022/01/17 杉浦 入れ替え中車両の処理

            return new OuterCarScheduleItemPutInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.Item.ID,
                // 開発符号
                GENERAL_CODE = item.GENERAL_CODE ?? item.CAR_GROUP,
                // 管理票番号
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //管理票番号 = this.SinkControlNoTextBox.Text,
                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                //管理票番号 = this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString(),
                管理票番号 = 管理票番号,
                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                // カテゴリー
                CATEGORY = this.MakeCategoryName(),
                // 並び順
                SORT_NO = item.SORT_NO ?? 0,
                // 行数
                PARALLEL_INDEX_GROUP = item.PARALLEL_INDEX_GROUP,
                // パーソナルID
                PERSONEL_ID = SessionDto.UserId,
                // 最終予約可能日
                最終予約可能日 = this.LastReservationDateTimePicker.SelectedDate,
                // FLAG_要予約許可
                FLAG_要予約許可 = Convert.ToInt16(this.SjsbReservationCheckBox.Checked)
            };
        }
        /// <summary>
        /// 外製車日程項目削除データの取得
        /// </summary>
        /// <returns></returns>
        private OuterCarScheduleItemDeleteInModel GetDeleteOuterCarScheduleItem()
        {
            return new OuterCarScheduleItemDeleteInModel
            {
                // カテゴリーID
                CATEGORY_ID = this.Item.ID
            };
        }

        /// <summary>
        /// 管理票番号による試験車情報(試験車)の取得
        /// </summary>
        private TestCarSearchOutModel GetCommonCar4TestCar(string manageNo = null)
        {
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //var val = this.GetCommonCarInfo(this.SinkControlNoTextBox.Text);
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            //var val = this.GetCommonCarInfo(this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString());
            var 管理票番号 = manageNo != null ? manageNo : num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            var val = this.GetCommonCarInfo(管理票番号);
            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            if (val == null || string.IsNullOrWhiteSpace(val.管理票NO ))
            {
                return default(TestCarSearchOutModel);
            }

            return new TestCarSearchOutModel()
            {
                管理票NO = val.管理票NO,
                開発符号 = val.開発符号,
                試作時期 = val.試作時期,
                登録ナンバー = val.登録ナンバー,
                号車 = val.号車,
                駐車場番号 = val.駐車場番号,
                FLAG_ナビ付 = val.FLAG_ナビ付,
                FLAG_ETC付 = val.FLAG_ETC付,
                仕向地 = val.仕向地,
                排気量 = val.排気量,
                E_G型式 = val.E_G型式,
                駆動方式 = val.駆動方式,
                トランスミッション = val.トランスミッション,
                車型 = val.車型,
                車体色 = val.車体色,
                リースNO = val.リースNO,
                固定資産NO = val.固定資産NO,
                リース満了日 = val.リース満了日,
                処分予定年月 = val.処分予定年月,
            };
        }
        /// <summary>
        /// 管理票番号による試験車情報(カーシェア)の取得
        /// </summary>
        private CarShareInnerSearchOutModel GetCommonCar4CarShare(string manageNo = null)
        {
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //var val = this.GetCommonCarInfo(this.SinkControlNoTextBox.Text);
            var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var num = list.FindIndex(x => x.category == "管理票番号");
            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            //var val = this.GetCommonCarInfo(this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString());
            var 管理票番号 = manageNo != null ? manageNo : num < 0 ? this.管理票No : this.ScheduleItemDataGridView.Rows[num].Cells[1].Value?.ToString();
            var val = this.GetCommonCarInfo(管理票番号);
            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            if (val == null || string.IsNullOrWhiteSpace(val.管理票NO))
            {
                return default(CarShareInnerSearchOutModel);
            }

            return new CarShareInnerSearchOutModel()
            {
                管理票NO = val.管理票NO,
                開発符号 = val.開発符号,
                試作時期 = val.試作時期,
                メーカー名 = val.メーカー名,
                外製車名 = val.外製車名,
                登録ナンバー = val.登録ナンバー,
                号車 = val.号車,
                駐車場番号 = val.駐車場番号,
                FLAG_ナビ付 = val.FLAG_ナビ付,
                FLAG_ETC付 = val.FLAG_ETC付,
                仕向地 = val.仕向地,
                排気量 = val.排気量,
                E_G型式 = val.E_G型式,
                駆動方式 = val.駆動方式,
                トランスミッション = val.トランスミッション,
                車型 = val.車型,
                車体色 = val.車体色,
                リースNO = val.リースNO,
                固定資産NO = val.固定資産NO,
                リース満了日 = val.リース満了日,
                処分予定年月 = val.処分予定年月,
            };
        }
        #endregion

        #region 画面項目の操作
        //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
        ///// <summary>
        ///// 車両情報(試験車・内製車・外製車)のセット
        ///// </summary>
        //private void SetScheduleCar2SinkCar()
        //{
        //    // 車両種別
        //    this.SinkCarClassLabel.Text = getCarClassString(this.SinkCarClass);

        //    if (this.ScheduleCar == null || this.ScheduleCar.CATEGORY_ID <= 0)
        //    {
        //        ClearChildrenController<TextBox>(this.SinkDetailTableLayoutPanel);
        //        return;
        //    }

        //    this.SinkControlNoTextBox.Text = this.ScheduleCar.管理票番号;
        //    this.SinkCommonCell1TextBox.Text = this.SinkCarClass != 2 ? this.ScheduleCar.開発符号 : this.ScheduleCar.メーカー名;
        //    this.SinkCommonCell2TextBox.Text = this.SinkCarClass != 2 ? this.ScheduleCar.試作時期 : this.ScheduleCar.外製車名;
        //    this.SinkLicensePlateNumberTextBox.Text = this.ScheduleCar.登録ナンバー;
        //    this.SinkCommonCell3TextBox.Text = this.ScheduleCar.号車;
        //    this.SinkParkingNoTextBox.Text = this.ScheduleCar.駐車場番号;
        //    this.SinkNaviTextBox.Text = this.ScheduleCar.FLAG_ナビ付 == 1 ? "あり" : string.Empty;
        //    this.SinkNaviTextBox.Text = this.ScheduleCar.FLAG_ナビ付 == 0 ? "なし" : this.SinkNaviTextBox.Text;
        //    this.SinkETCTextBox.Text = this.ScheduleCar.FLAG_ETC付 == 1 ? "あり" : string.Empty;
        //    this.SinkETCTextBox.Text = this.ScheduleCar.FLAG_ETC付 == 0 ? "なし" : this.SinkETCTextBox.Text;
        //    this.SinkShimukechiTextBox.Text = this.ScheduleCar.仕向地;
        //    this.SinkDisplacementTextBox.Text = this.ScheduleCar.排気量;
        //    this.SinkEGTypeTextBox.Text = this.ScheduleCar.E_G型式;
        //    this.SinkDriveSystemTextBox.Text = this.ScheduleCar.駆動方式;
        //    this.SinkTMTextBox.Text = this.ScheduleCar.トランスミッション;
        //    this.SinkCarTypeTextBox.Text = this.ScheduleCar.車型;
        //    this.SinkCarColorTextBox.Text = this.ScheduleCar.車体色;
        //    this.SinkLeaseNoTextBox.Text = this.ScheduleCar.リースNO;
        //    this.SinkFixedAssetNoTextBox.Text = this.ScheduleCar.固定資産NO;
        //    this.SinkLeaseEndDateTextBox.Text = this.ScheduleCar.リース満了日;
        //    this.SinkDisposalDateTextBox.Text = this.ScheduleCar.処分予定年月;
        //    this.SinkMemoTextBox.Text = this.ScheduleCar.備考;
        //}
        //Delete End 2022/01/31 杉浦 項目を変動できるようにする

        /// <summary>
        /// 試験車情報(試験車)のセット
        /// </summary>
        private void SetTestCar2SourceCar()
        {
            // 車両種別
            this.SourceCarClassLabel.Text = getCarClassString(this.SourceCarClass);

            if (this.TestCar == null)
            {
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //ClearChildrenController<TextBox>(this.SourceDetailTableLayoutPanel);
                this.ClearCarDataGridView();
                this.CarDataGridView.DataSource = this.setTestCarList();
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                return;
            }

            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SourceControlNoTextBox.Text = this.TestCar.管理票NO;
            //this.SourceCommonCell1TextBox.Text = this.SourceCarClass != 2 ? this.TestCar.開発符号 : this.TestCar.メーカー名;
            //this.SourceCommonCell2TextBox.Text = this.SourceCarClass != 2 ? this.TestCar.試作時期 : this.TestCar.外製車名;
            //this.SourceLicensePlateNumberTextBox.Text = this.TestCar.登録ナンバー;
            //this.SourceCommonCell3TextBox.Text = this.TestCar.号車;
            //this.SourceParkingNoTextBox.Text = this.TestCar.駐車場番号;
            //if (!string.IsNullOrWhiteSpace(this.TestCar.登録ナンバー))
            //{
            //    this.SourceParkingNoTextBox.Text += Convert.ToInt32(this.TestCar.駐車場番号?.Substring(0, 1)[0]) >= 80 ? "(SKC)": "(G)";
            //}
            //this.SourceNaviTextBox.Text = this.TestCar.FLAG_ナビ付 == 1 ? "あり" : string.Empty;
            //this.SourceNaviTextBox.Text = this.TestCar.FLAG_ナビ付 == 0 ? "なし" : this.SourceNaviTextBox.Text;
            //this.SourceETCTextBox.Text = this.TestCar.FLAG_ETC付 == 1 ? "あり" : string.Empty;
            //this.SourceETCTextBox.Text = this.TestCar.FLAG_ETC付 == 0 ? "なし" : this.SourceETCTextBox.Text;
            //this.SourceShimukechiTextBox.Text = this.TestCar.仕向地;
            //this.SourceDisplacementTextBox.Text = this.TestCar.排気量;
            //this.SourceEGTypeTextBox.Text = this.TestCar.E_G型式;
            //this.SourceDriveSystemTextBox.Text = this.TestCar.駆動方式;
            //this.SourceTMTextBox.Text = this.TestCar.トランスミッション;
            //this.SourceCarTypeTextBox.Text = this.TestCar.車型;
            //this.SourceCarColorTextBox.Text = this.TestCar.車体色;
            //this.SourceLeaseNoTextBox.Text = this.TestCar.リースNO;
            //this.SourceFixedAssetNoTextBox.Text = this.TestCar.固定資産NO;
            //this.SourceLeaseEndDateTextBox.Text = this.TestCar.リース満了日 != null ? ((DateTime)this.TestCar.リース満了日).ToString("yyyy/MM/dd") : string.Empty;
            //this.SourceDisposalDateTextBox.Text = this.TestCar.処分予定年月 != null ? ((DateTime)this.TestCar.処分予定年月).ToString("yyyy/MM/dd") : string.Empty;

            this.CarDataGridView.DataSource = this.setTestCarList(true);
            //Update End 2022/01/31 杉浦 項目を変動できるようにする
        }
        /// <summary>
        /// 試験車情報(内製車・外製車)のセット
        /// </summary>
        private void SetCarShare2SourceCar()
        {
            // 車両種別
            this.SourceCarClassLabel.Text = getCarClassString(this.SourceCarClass);

            if (this.CarShare == null)
            {
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //ClearChildrenController<TextBox>(this.SourceDetailTableLayoutPanel);
                this.ClearCarDataGridView();
                this.CarDataGridView.DataSource = this.setCarShareList();
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                return;
            }

            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //this.SourceControlNoTextBox.Text = this.CarShare.管理票NO;
            //this.SourceCommonCell1TextBox.Text = this.SourceCarClass != 2 ? this.CarShare.開発符号 : this.CarShare.メーカー名;
            //this.SourceCommonCell2TextBox.Text = this.SourceCarClass != 2 ? this.CarShare.試作時期 : this.CarShare.外製車名;
            //this.SourceLicensePlateNumberTextBox.Text = this.CarShare.登録ナンバー;
            //this.SourceCommonCell3TextBox.Text = this.CarShare.号車;
            //this.SourceParkingNoTextBox.Text = this.CarShare.駐車場番号;
            //if (!string.IsNullOrWhiteSpace(this.CarShare.登録ナンバー) && this.SourceCarClass != 2)
            //{
            //    this.SourceParkingNoTextBox.Text += Convert.ToInt32(this.CarShare.駐車場番号?.Substring(0, 1)[0]) >= 80 ? "(SKC)" : "(G)";
            //}
            //this.SourceNaviTextBox.Text = this.CarShare.FLAG_ナビ付 == 1 ? "あり" : string.Empty;
            //this.SourceNaviTextBox.Text = this.CarShare.FLAG_ナビ付 == 0 ? "なし" : this.SourceNaviTextBox.Text;
            //this.SourceETCTextBox.Text = this.CarShare.FLAG_ETC付 == 1 ? "あり" : string.Empty;
            //this.SourceETCTextBox.Text = this.CarShare.FLAG_ETC付 == 0 ? "なし" : this.SourceETCTextBox.Text;
            //this.SourceShimukechiTextBox.Text = this.CarShare.仕向地;
            //this.SourceDisplacementTextBox.Text = this.CarShare.排気量;
            //this.SourceEGTypeTextBox.Text = this.CarShare.E_G型式;
            //this.SourceDriveSystemTextBox.Text = this.CarShare.駆動方式;
            //this.SourceTMTextBox.Text = this.CarShare.トランスミッション;
            //this.SourceCarTypeTextBox.Text = this.CarShare.車型;
            //this.SourceCarColorTextBox.Text = this.CarShare.車体色;
            //this.SourceLeaseNoTextBox.Text = this.CarShare.リースNO;
            //this.SourceFixedAssetNoTextBox.Text = this.CarShare.固定資産NO;
            //this.SourceLeaseEndDateTextBox.Text = this.CarShare.リース満了日 != null ? ((DateTime)this.CarShare.リース満了日).ToString("yyyy/MM/dd") : string.Empty;
            //this.SourceDisposalDateTextBox.Text = this.CarShare.処分予定年月 != null ? ((DateTime)this.CarShare.処分予定年月).ToString("yyyy/MM/dd") : string.Empty;
            this.CarDataGridView.DataSource = this.setCarShareList(true);
            //Update End 2022/01/31 杉浦 項目を変動できるようにする
        }
        /// <summary>
        /// 車輛情報項目へのデータコピー
        /// </summary>
        private void CopySourceCar2SinkCar()
        {
            var text = new List<string>();

            // 車両種別
            this.SinkCarClassLabel.Text = getCarClassString(this.SinkCarClass);

            // 車輛情報
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //var sinkCarControllers = this.GetChildrenController<TextBox>(this.SinkDetailTableLayoutPanel);
            var sinkCarControllers = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            // 試験車情報
            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //var sourceCarControllers = this.GetChildrenController<TextBox>(this.SourceDetailTableLayoutPanel);
            var sourceCarControllers = (List<GridViewDisplayItemModel>)this.CarDataGridView.DataSource;
            //Update End 2022/01/31 杉浦 項目を変動できるようにする

            //Update Start 2022/01/31 杉浦 項目を変動できるようにする
            //foreach (var ctl in sinkCarControllers)
            //{
            //    // 備考は除外
            //    if (ctl == this.SinkMemoTextBox) continue;

            //    // テキストのコピー
            //    ctl.Text = sourceCarControllers.FirstOrDefault(x => this.GetTagName(x) == this.GetTagName(ctl))?.Text;
            //}
            if (this.SinkCarClass != 2 && !sinkCarControllers.Any(x => x.category == "開発符号" || x.category == "試作時期"))
            {
                var num = sinkCarControllers.FindIndex(x => x.category == "メーカー名");
                sinkCarControllers[num].category = "開発符号";
                var num2 = sinkCarControllers.FindIndex(x => x.category == "外製車名");
                sinkCarControllers[num2].category = "試作時期";
            }
            else if (this.SinkCarClass == 2 && !sinkCarControllers.Any(x => x.category == "メーカー名" || x.category == "外製車名"))
            {
                var num = sinkCarControllers.FindIndex(x => x.category == "開発符号");
                sinkCarControllers[num].category = "メーカー名";
                var num2 = sinkCarControllers.FindIndex(x => x.category == "試作時期");
                sinkCarControllers[num2].category = "外製車名";
            }

            foreach (var row in sinkCarControllers)
            {
                // 備考は除外
                if (row.category == "備考") continue;

                // テキストのコピー
                row.item = sourceCarControllers.FirstOrDefault(x => x.category == row.category)?.item;
                row.modified = true;
            }
            this.ScheduleItemDataGridView.DataSource = null;
            this.ScheduleItemDataGridView.DataSource = sinkCarControllers;
            this.InitGridView();
            //Update End 2022/01/31 杉浦 項目を変動できるようにする


            // 試験車の場合は終了
            if (this.SinkCarClass == 0) return;

            // 最終予約可能日の自動セット
            if (!string.IsNullOrWhiteSpace(this.CarShare.リースNO) && this.CarShare.リース満了日 != null)
            {
                // リースでリース満了日が入力済みの場合
                this.LastReservationDateTimePicker.Value = getParseLeaseEndDate(this.CarShare.リース満了日?.ToString("yyyy/MM/dd"));
            }
            else if (!string.IsNullOrWhiteSpace(this.CarShare.固定資産NO) && this.CarShare.処分予定年月 != null)
            {
                // 固定資産で処分予定年月が入力済みの場合
                this.LastReservationDateTimePicker.Value = getParseDisposalDate(this.CarShare.処分予定年月?.ToString("yyyy/MM/dd"));
            }
            else
            {
                // その他
                this.LastReservationDateTimePicker.Value = null;
            }
        }
        /// <summary>
        /// コントロールリストのチェック
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="exclusion"></param>
        /// <returns>bool</returns>
        private bool IsChildrenController<T>(Control parent, List<Control> exclusion = null)
        {
            foreach (Control child in parent.Controls)
            {
                // タイプ相違
                if (!child.GetType().Equals(typeof(T))) continue;

                // 除外指定あり
                if (exclusion != null && exclusion.Any(x => x == child)) continue;

                if (!string.IsNullOrWhiteSpace(child.Text))
                {
                    // 入力あり
                    return true;
                }
            }

            // 入力なし
            return false;
        }
        /// <summary>
        /// コントロールリストの取得
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>IEnumerable</returns>
        private IEnumerable<Control> GetChildrenController<T>(Control parent)
        {
            var controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                if (!child.GetType().Equals(typeof(T))) continue;

                controls.Add(child);
            }

            return controls;
        }
        /// <summary>
        /// コントロールのクリア
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>IEnumerable</returns>
        private void ClearChildrenController<T>(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (!child.GetType().Equals(typeof(T))) continue;

                child.Text = string.Empty;
            }
        }
        /// <summary>
        /// タグ名の取得
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns>string</returns>
        private string GetTagName(Control ctl)
        {
            Func<string, string> getTagValue = s => Regex.Replace(s, @"^.+?\((.+)\)$", "$1");

            Func<object, string[]> split = o =>
            {
                var value = o == null ? string.Empty : o.ToString();

                var list = new List<string>();

                var tag = "";

                foreach (var s in value.Split(';'))
                {
                    tag += s;

                    if (Regex.IsMatch(s, @"([a-z]|\))$") == true)
                    {
                        list.Add(tag);

                        tag = "";
                    }
                    else
                    {
                        tag += ";";
                    }
                }

                return list.ToArray();
            };

            var tagList = split(ctl.Tag);

            var itemName = tagList.FirstOrDefault(x => x.Contains(Const.ItemName) == true);

            return itemName == null ? "" : getTagValue(itemName);
        }
        /// <summary>
        /// 項目名の作成
        /// </summary>
        /// <returns>string</returns>
        private string MakeCategoryName()
        {
            Func<string, string> getFlag = str =>
            {
                if (str == "あり" || str == "有" || str == "1")
                {
                    return "あり";
                }

                if (str == "なし" || str == "無" || str == "0")
                {
                    return "なし";
                }

                return "なし";
            };

            var list = new List<string>();

            // 車輛情報
            //var sinkCarControllers = this.GetChildrenController<TextBox>(this.SinkDetailTableLayoutPanel);
            var sinkCarControllers = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            var cntrolNo = string.Empty;
            var leaseText = string.Empty;
            var assetText = string.Empty;
            //Append Start 2022/01/17 杉浦 入れ替え中車両の処理
            int num;
            //Append End 2022/01/17 杉浦 入れ替え中車両の処理

            foreach (var ctl in sinkCarControllers)
            {
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //if (!string.IsNullOrWhiteSpace(ctl.Text))
                //{
                //    var text = ctl.Text;
                if (!string.IsNullOrWhiteSpace(ctl.item))
                {
                    var text = ctl.item;
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする

                    // 管理票番号
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkControlNoTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "管理票番号");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        cntrolNo = text;
                        //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                        //continue;
                        //Delete End 2022/01/31 杉浦 項目を変動できるようにする
                        //Append Start 2022/01/31 杉浦 項目を変動できるようにする
                        if (!string.IsNullOrWhiteSpace(cntrolNo))
                        {
                            text = string.Format("({0})", cntrolNo);
                            cntrolNo = string.Empty;
                        }
                        //Append End 2022/01/31 杉浦 項目を変動できるようにする
                    }

                    // 駐車場番号
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkParkingNoTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "駐車場番号");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        text = string.Format("駐No.{0}", text);
                    }
                    // ナビ
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkNaviTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "ナビ付");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        text = string.Format("ナビ{0}", text);
                    }

                    // ETC
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkETCTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "ETC付");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        text = string.Format("ETC{0}", text);
                    }

                    // リースNo.
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkLeaseNoTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "リースNO");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        leaseText = "リース";
                        text = string.Empty;
                    }

                    // 固定資産No.
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkFixedAssetNoTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "固定資産NO");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        assetText = "固定資産";
                        text = string.Empty;
                    }

                    // リース満了日
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkLeaseEndDateTextBox)
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    num = sinkCarControllers.FindIndex(x => x.category == "リース満了日");
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        var leaseenddate = getParseLeaseEndDate(text)?.ToString("yy/MM/dd");

                        if (!string.IsNullOrWhiteSpace(leaseText) && !string.IsNullOrWhiteSpace(leaseenddate))
                        {
                            leaseText = string.Format("{0} ～{1}", leaseText, leaseenddate);
                        }

                        text = string.Empty;
                    }

                    // 処分予定年月
                    //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkDisposalDateTextBox)
                    num = sinkCarControllers.FindIndex(x => x.category == "処分予定年月");
                    //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                    //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                    //Update End 2022/01/31 杉浦 項目を変動できるようにする
                    {
                        var disposaldate = getParseDisposalDate(text)?.ToString("yy/MM/dd");

                        if (!string.IsNullOrWhiteSpace(assetText) && !string.IsNullOrWhiteSpace(disposaldate))
                        {
                            assetText = string.Format("{0} ～{1}", assetText, disposaldate);
                        }

                        text = string.Empty;
                    }

                    // メモ
                    //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
                    //if (ctl == this.SinkMemoTextBox)
                    //{
                    //    if (!string.IsNullOrWhiteSpace(cntrolNo))
                    //    {
                    //        list.Add(string.Format("({0})", cntrolNo));
                    //        cntrolNo = string.Empty;
                    //    }
                    //}
                    //Delete End 2022/01/31 杉浦 項目を変動できるようにする

                    if (!string.IsNullOrWhiteSpace(text)) list.Add(text);
                }

                // 予約許可(ETCの後)
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //if (ctl == this.SinkETCTextBox)
                num = sinkCarControllers.FindIndex(x => x.category == "ETC付");
                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                {
                    if (this.SjsbReservationCheckBox.Visible && this.SjsbReservationCheckBox.Checked)
                    {
                        list.Add("(予約許可必要)");
                    }
                }

                // リース or 固定資産文言(処分予定年月の)
                //Update Start 2022/01/31 杉浦 項目を変動できるようにする
                //if (ctl == this.SinkDisposalDateTextBox)
                num = sinkCarControllers.FindIndex(x => x.category == "処分予定年月");
                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                //if (ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                if (num >= 0 && ctl.category == this.ScheduleItemDataGridView.Rows[num].Cells[0].Value?.ToString())
                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                //Update End 2022/01/31 杉浦 項目を変動できるようにする
                {
                    // リース
                    if (!string.IsNullOrWhiteSpace(leaseText))
                    {
                        list.Add(leaseText);
                    }
                    // 固定資産
                    else if (!string.IsNullOrWhiteSpace(assetText))
                    {
                        list.Add(assetText);
                    }
                }
            }

            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする
            //// 調整
            //if (!string.IsNullOrWhiteSpace(cntrolNo))
            //{
            //    list.Add(string.Format("({0})", cntrolNo));
            //}
            //Delete Start 2022/01/31 杉浦 項目を変動できるようにする

            return list.Count > 0 ? string.Join(" ", list) : "?";
        }
        #endregion

        //Append Start 2022/01/31 杉浦 項目を変動できるようにする
        #region 表示順変更
        private void ListConfigPictureBox_Click(object sender, EventArgs e)
        {
            //Append Start 2022/03/16 杉浦
            var nowList = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
            bool checkBefore = false;
            string manageNo = null;
            if(nowList.Any(x => x.modified == true))
            {
                if(Messenger.Confirm("編集が途中です。一時保存しますか？") == DialogResult.Yes)
                {
                    var changeList = nowList.Where(x => x.modified == true).ToList();
                    checkBefore = true;
                    foreach(var display in changeList)
                    {
                        if(display.category == "ナビ付" || display.category == "ETC付" || display.category == "T/M")
                        {

                            if (display.category == "ナビ付")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ナビ付");
                                var result = Convert.ToInt16(display.item != null && display.item == "あり" ? 1 : 0);
                                property.SetValue(this.ScheduleCar, result);
                            }
                            else if (display.category == "ETC付")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ETC付");
                                var result = Convert.ToInt16(display.item != null && display.item == "あり" ? 1 : 0);
                                property.SetValue(this.ScheduleCar, result);
                            }
                            else if (display.category == "T/M")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty("トランスミッション");
                                property.SetValue(this.ScheduleCar, display.item);
                            }
                        }else
                        {
                            var property = typeof(ScheduleCarDetailModel).GetProperty(display.category);
                            property.SetValue(this.ScheduleCar, display.item);
                        }
                    }
                }else
                {
                    manageNo = this.ScheduleCar.管理票番号;
                }
            }
            //Append End 2022/03/16 杉浦

            // 表示設定画面表示
            var list = this.SetItemForSetting();
            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            //using (var form = new DataGridViewDisplayForm { FormName = this.Item.GeneralCode, Display = list })
            //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
            //using (var form = new DataGridViewDisplayForm { FormName = this.Item.GeneralCode, Display = list, CarType = this.SinkCarClass })
            using (var form = new DataGridViewDisplayForm { FormName = this.Item.ID.ToString(), Display = list, CarType = this.SinkCarClass })
            //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.ClearCarDataGridView();
                    // 表示反映
                    if (this.FunctionId == FunctionID.TestCar)
                    {
                        if(!checkBefore) this.TestCar = this.GetCommonCar4TestCar(manageNo);
                        this.CarDataGridView.DataSource = this.setTestCarList();
                    }else
                    {
                        if (!checkBefore) this.CarShare = this.GetCommonCar4CarShare(manageNo);
                        this.CarDataGridView.DataSource = this.setCarShareList();
                    }

                    //Update Start 2022/03/16 杉浦
                    //var setList = this.ChangeDisplay(nowList);
                    var setList = this.setList(true);
                    //Update End 2022/03/16 杉浦
                    this.ScheduleItemDataGridView.DataSource = null;
                    this.ScheduleItemDataGridView.DataSource = setList;

                    this.ChangeView = true;
                    
                    this.InitGridView();

                    //Append Start 2023/10/15 杉浦 スケジュール詳細表示変更
                    var num = setList.FindIndex(x => x.category == "管理票番号");
                    if (num >= 0)
                    {
                        if (string.IsNullOrEmpty(setList[num].item))
                        {
                            this.ScheduleItemDataGridView.Rows[num].Cells[0].Style.BackColor = System.Drawing.Color.Yellow;
                            this.ScheduleItemDataGridView.Rows[num].Cells[0].ToolTipText = "管理票Noが未記入のため、他機能が正常に動かない可能性があります。管理票Noの記入をお願いします。";
                        }
                    }
                    //Append End 2023/10/15 杉浦 スケジュール詳細表示変更
                }
            }
        }
        #endregion

        #region DataGridViewのソース
        public List<GridViewDisplayItemModel> setList(bool changeFlg = false)
        {
            var list = new List<GridViewDisplayItemModel>();
            var cond = new ScheduleItemDisplayConfigurationSearchModel
            {
                // ユーザーID
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //開発符号 = this.Item.GeneralCode
                開発符号 = this.Item.ID.ToString()
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            };

            // APIで取得
            var res = HttpUtil.GetResponse<ScheduleItemDisplayConfigurationSearchModel, ScheduleItemDisplayConfigurationModel>(ControllerType.ScheduleItemDisplayConfiguration, cond);


            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //return list;
                var itemData = new ScheduleItemDisplayConfigurationModel();
                //Update Start 2023/10/15 杉浦 スケジュール詳細表示変更
                //itemData.表示列名 = "管理票番号,開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                itemData.表示列名 = "開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                //Update End 2023/10/15 杉浦 スケジュール詳細表示変更

                var itemDataList = new List<ScheduleItemDisplayConfigurationModel>();
                itemDataList.Add(itemData);

                res.Results = itemDataList;
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            }

            // 取得データの格納
            var data = res.Results.FirstOrDefault();
            //Append Start 2023/10/15 杉浦 スケジュール詳細表示変更
            data.表示列名 = data.表示列名 + ",管理票番号";
            //Append End 2023/10/15 杉浦 スケジュール詳細表示変更

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var scheduleItemCar = this.ScheduleCar;
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            this.管理票No = scheduleItemCar == null ? null : scheduleItemCar.管理票番号;
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                var item = new GridViewDisplayItemModel { category = displayName, modified = false };
                if (scheduleItemCar != null && (this.Item.ScheduleItemEdit == ScheduleItemEditType.Update || changeFlg))
                {
                    if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                    {
                        if (this.SinkCarClass != 2)
                        {
                            if (displayName == "開発符号")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                                item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            }
                            else if (displayName == "試作時期")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                                item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (displayName == "メーカー名")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                                item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            }
                            else if (displayName == "外製車名")
                            {
                                var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                                item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                    else if (displayName == "ナビ付" || displayName == "ETC付" || displayName == "T/M")
                    //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    {
                        //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                        //var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                        //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                        //if (displayName == "FLAG_ナビ付")
                        //{
                        //    item.category = "ナビ付";
                        //}
                        //else
                        //{
                        //    item.category = "ETC付";
                        //}
                        //item.item = result;
                        if (displayName == "ナビ付")
                        {
                            var property = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ナビ付");
                            var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                            item.item = result;
                        }
                        else if (displayName == "ETC付")
                        {
                            var property = typeof(ScheduleCarDetailModel).GetProperty("FLAG_ETC付");
                            var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                            item.item = result;
                        }
                        //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //Append Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        else if (displayName == "T/M")
                        {
                            var property = typeof(ScheduleCarDetailModel).GetProperty("トランスミッション");
                            var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                            //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                            //item.item = result;
                            item.item = getItem;
                            //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                        }
                        //Append End 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                    }
                    else if (displayName == "処分予定年月" || displayName == "リース満了日")
                    {
                        var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                        var dateItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                        //Update Start 2022/02/16 杉浦 日付表示変更
                        //if (dateItem != null)
                        //{
                        //    item.item = DateTime.Parse(dateItem).ToString("yyyy/MM/dd");
                        //}
                        DateTime day = new DateTime();
                        if (DateTime.TryParse(dateItem, out day))
                        {
                            item.item = day.ToString("yyyy/MM/dd");
                        }
                        //Update End 2022/02/16 杉浦 日付表示変更
                        else
                        {
                            item.item = null;
                        }
                    }
                    else
                    {
                        var property = typeof(ScheduleCarDetailModel).GetProperty(displayName);
                        item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                    }

                }else
                {
                    if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                    {
                        if (this.SinkCarClass != 2)
                        {
                            if (displayName == "メーカー名" || displayName == "外製車名")
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (displayName == "開発符号" || displayName == "試作時期")
                            {
                                continue;
                            }
                        }
                    }
                    //Delete Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                    //{
                    //    if (displayName == "FLAG_ナビ付")
                    //    {
                    //        item.category = "ナビ付";
                    //    }
                    //    else
                    //    {
                    //        item.category = "ETC付";
                    //    }
                    //}
                    //Delete End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                }
                switch (displayName)
                {
                    case "管理票番号":
                    case "登録ナンバー":
                    case "メーカー名":
                    case "外製車名":
                    case "号車":
                    case "駐車場番号":
                    case "仕向地":
                    case "排気量":
                    case "E_G型式":
                    case "駆動方式":
                    //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                    //case "トランスミッション":
                    case "T/M":
                    //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                    case "車体色":
                    case "リース満了日":
                    case "処分予定年月":
                        item.checkByte = 50;
                        break;

                    case "開発符号":
                    case "試作時期":
                    case "リースNO":
                        item.checkByte = 20;
                        break;


                    //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    //    case "FLAG_ナビ付":
                    //    case "FLAG_ETC付":
                    case "ナビ付":
                    case "ETC付":
                    //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    case "車型":
                    case "固定資産NO":
                        item.checkByte = 10;
                        break;

                    case "備考":
                        item.checkByte = 1000;
                        break;

                    default:
                        item.checkByte = null;
                        break;
                }
                list.Add(item);
            }

            return list;

        }
        public List<GridViewDisplayItemModel> setTestCarList(bool changeFlg = false)
        {
            var list = new List<GridViewDisplayItemModel>();
            var cond = new ScheduleItemDisplayConfigurationSearchModel
            {
                // ユーザーID
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //開発符号 = this.Item.GeneralCode
                開発符号 = this.Item.ID.ToString()
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            };

            // APIで取得
            var res = HttpUtil.GetResponse<ScheduleItemDisplayConfigurationSearchModel, ScheduleItemDisplayConfigurationModel>(ControllerType.ScheduleItemDisplayConfiguration, cond);


            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //return list;
                var itemData = new ScheduleItemDisplayConfigurationModel();
                //Update Start 2023/10/15 杉浦 スケジュール詳細表示変更
                //itemData.表示列名 = "管理票番号,開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                itemData.表示列名 = "開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                //Update End 2023/10/15 杉浦 スケジュール詳細表示変更

                var itemDataList = new List<ScheduleItemDisplayConfigurationModel>();
                itemDataList.Add(itemData);

                res.Results = itemDataList;
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            }

            // 取得データの格納
            var data = res.Results.FirstOrDefault();
            //Append Start 2023/10/15 杉浦 スケジュール詳細表示変更
            data.表示列名 = data.表示列名 + ",管理票番号";
            //Append End 2023/10/15 杉浦 スケジュール詳細表示変更

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var scheduleItemCar = this.TestCar;
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            this.管理票No = scheduleItemCar == null ? null : scheduleItemCar.管理票NO;
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                if(displayName != "備考")
                {
                    var item = new GridViewDisplayItemModel { category = displayName, modified = false };
                    if (scheduleItemCar != null)
                    {
                        if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                        {
                            if (this.SourceCarClass != 2)
                            {
                                if (displayName == "開発符号")
                                {
                                    var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else if (displayName == "試作時期")
                                {
                                    var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (displayName == "メーカー名")
                                {
                                    var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else if (displayName == "外製車名")
                                {
                                    var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                        else if (displayName == "ナビ付" || displayName == "ETC付" || displayName == "T/M")
                        //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        {
                            //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                            //var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                            //var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                            //if (displayName == "FLAG_ナビ付")
                            //{
                            //    item.category = "ナビ付";
                            //}
                            //else
                            //{
                            //    item.category = "ETC付";
                            //}
                            //item.item = result;
                            if (displayName == "ナビ付")
                            {
                                var property = typeof(TestCarSearchOutModel).GetProperty("FLAG_ナビ付");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                                item.item = result;
                            }
                            else if (displayName == "ETC付")
                            {
                                var property = typeof(TestCarSearchOutModel).GetProperty("FLAG_ETC付");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                                item.item = result;
                            }
                            //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                            //Append Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                            else if (displayName == "T/M")
                            {
                                var property = typeof(TestCarSearchOutModel).GetProperty("トランスミッション");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                                //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                                //item.item = result;
                                item.item = getItem;
                                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                            }
                            //Append End 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        }
                        else if (displayName == "駐車場番号")
                        {
                            if (!string.IsNullOrWhiteSpace(this.TestCar.登録ナンバー))
                            {
                                var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                                var parking = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                                //item.item = Convert.ToInt32(parking?.Substring(0, 1)[0]) >= 80 ? "(SKC)" : "(G)";
                                item.item = parking;
                                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                            }
                        }
                        else if (displayName == "管理票番号")
                        {
                            item.item = scheduleItemCar.管理票NO;
                        }
                        else if (displayName == "処分予定年月" || displayName == "リース満了日")
                        {
                            var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                            var dateItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //Update Start 2022/02/16 杉浦 日付表示変更
                            //if (dateItem != null)
                            //{
                            //    item.item = DateTime.Parse(dateItem).ToString("yyyy/MM/dd");
                            //}
                            DateTime day = new DateTime();
                            if (DateTime.TryParse(dateItem, out day))
                            {
                                item.item = day.ToString("yyyy/MM/dd");
                            }
                            //Update End 2022/02/16 杉浦 日付表示変更
                            else
                            {
                                item.item = null;
                            }
                        }
                        else
                        {
                            var property = typeof(TestCarSearchOutModel).GetProperty(displayName);
                            item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                        }
                    }
                    else
                    {
                        if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                        {
                            if (this.SourceCarClass != 2)
                            {
                                if (displayName == "メーカー名" || displayName == "外製車名")
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (displayName == "開発符号" || displayName == "試作時期")
                                {
                                    continue;
                                }
                            }
                        }
                        //Delete Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                        //{
                        //    if (displayName == "FLAG_ナビ付")
                        //    {
                        //        item.category = "ナビ付";
                        //    }
                        //    else
                        //    {
                        //        item.category = "ETC付";
                        //    }
                        //}
                        //Delete End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    }
                    switch (displayName)
                    {
                        case "管理票番号":
                        case "登録ナンバー":
                        case "メーカー名":
                        case "外製車名":
                        case "号車":
                        case "駐車場番号":
                        case "仕向地":
                        case "排気量":
                        case "E_G型式":
                        case "駆動方式":
                        //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        //case "トランスミッション":
                        case "T/M":
                        //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        case "車体色":
                        case "リース満了日":
                        case "処分予定年月":
                            item.checkByte = 50;
                            break;

                        case "開発符号":
                        case "試作時期":
                        case "リースNO":
                            item.checkByte = 20;
                            break;


                        //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //    case "FLAG_ナビ付":
                        //    case "FLAG_ETC付":
                        case "ナビ付":
                        case "ETC付":
                        //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        case "車型":
                        case "固定資産NO":
                            item.checkByte = 10;
                            break;

                        default:
                            item.checkByte = null;
                            break;
                    }
                    list.Add(item);
                }
            }

            return list;

        }

        public List<GridViewDisplayItemModel> setCarShareList(bool changeFlg = false)
        {
            var list = new List<GridViewDisplayItemModel>();
            var cond = new ScheduleItemDisplayConfigurationSearchModel
            {
                // ユーザーID
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //開発符号 = this.Item.GeneralCode
                開発符号 = this.Item.ID.ToString()
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            };

            // APIで取得
            var res = HttpUtil.GetResponse<ScheduleItemDisplayConfigurationSearchModel, ScheduleItemDisplayConfigurationModel>(ControllerType.ScheduleItemDisplayConfiguration, cond);


            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //return list;
                var itemData = new ScheduleItemDisplayConfigurationModel();
                //Update Start 2023/10/15 杉浦 スケジュール詳細表示変更
                //itemData.表示列名 = "管理票番号,開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                itemData.表示列名 = "開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                //Update End 2023/10/15 杉浦 スケジュール詳細表示変更

                var itemDataList = new List<ScheduleItemDisplayConfigurationModel>();
                itemDataList.Add(itemData);

                res.Results = itemDataList;
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            }

            // 取得データの格納
            var data = res.Results.FirstOrDefault();
            //Append Start 2023/10/15 杉浦 スケジュール詳細表示変更
            data.表示列名 = data.表示列名 + ",管理票番号";
            //Append End 2023/10/15 杉浦 スケジュール詳細表示変更

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var scheduleItemCar = this.CarShare;
            //Append Start 2022/03/07 杉浦 スケジュール詳細不具合修正
            this.管理票No = scheduleItemCar == null ? null : scheduleItemCar.管理票NO;
            //Append End 2022/03/07 杉浦 スケジュール詳細不具合修正
            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {
                if(displayName != "備考")
                {
                    var item = new GridViewDisplayItemModel { category = displayName, modified = false };
                    if (scheduleItemCar != null)
                    {
                        if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                        {
                            if (this.SourceCarClass != 2)
                            {
                                if (displayName == "開発符号")
                                {
                                    var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else if (displayName == "試作時期")
                                {
                                    var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (displayName == "メーカー名")
                                {
                                    var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else if (displayName == "外製車名")
                                {
                                    var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                                    item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                        else if (displayName == "ナビ付" || displayName == "ETC付" || displayName == "T/M")
                        //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        {
                            //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                            //var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                            //var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                            //if (displayName == "FLAG_ナビ付")
                            //{
                            //    item.category = "ナビ付";
                            //}
                            //else
                            //{
                            //    item.category = "ETC付";
                            //}
                            //item.item = result;
                            if (displayName == "ナビ付")
                            {
                                var property = typeof(CarShareInnerSearchOutModel).GetProperty("FLAG_ナビ付");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                                item.item = result;
                            }
                            else if (displayName == "ETC付")
                            {
                                var property = typeof(CarShareInnerSearchOutModel).GetProperty("FLAG_ETC付");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                var result = getItem != null && getItem == "1" ? "あり" : getItem != null && getItem == "0" ? "なし" : string.Empty;
                                item.item = result;
                            }
                            //Delete Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                            //else if (displayName == "T/M")
                            //{
                            //    var property = typeof(CarShareInnerSearchOutModel).GetProperty("トランスミッション");
                            //    var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //    var result = getItem != null && getItem == "1" ? "あり" : "なし";
                            //    item.item = result;
                            //}
                            //Delete Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                            //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                            //Append Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                            else if (displayName == "T/M")
                            {
                                var property = typeof(CarShareInnerSearchOutModel).GetProperty("トランスミッション");
                                var getItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                                //var result = getItem != null && getItem == "1" ? "あり" : "なし";
                                //item.item = result;
                                item.item = getItem;
                                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                            }
                            //Append End 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        }
                        else if (displayName == "駐車場番号")
                        {
                            if (!string.IsNullOrWhiteSpace(this.TestCar.登録ナンバー))
                            {
                                var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                                var parking = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                                //Update Start 2022/03/07 杉浦 スケジュール詳細不具合修正
                                //item.item = Convert.ToInt32(parking?.Substring(0, 1)[0]) >= 80 ? "(SKC)" : "(G)";
                                item.item = parking;
                                //Update End 2022/03/07 杉浦 スケジュール詳細不具合修正
                            }
                        }
                        else if (displayName == "管理票番号")
                        {
                            item.item = scheduleItemCar.管理票NO;
                        }
                        else if (displayName == "処分予定年月" || displayName == "リース満了日")
                        {
                            var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                            var dateItem = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                            //Update Start 2022/02/16 杉浦 日付表示変更
                            //if (dateItem != null)
                            //{
                            //    item.item = DateTime.Parse(dateItem).ToString("yyyy/MM/dd");
                            //}
                            DateTime day = new DateTime();
                            if (DateTime.TryParse(dateItem, out day))
                            {
                                item.item = day.ToString("yyyy/MM/dd");
                            }
                            //Update End 2022/02/16 杉浦 日付表示変更
                            else
                            {
                                item.item = null;
                            }
                        }
                        else
                        {
                            var property = typeof(CarShareInnerSearchOutModel).GetProperty(displayName);
                            item.item = property.GetValue(scheduleItemCar) == null ? null : property.GetValue(scheduleItemCar).ToString();
                        }
                    }
                    else
                    {
                        if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
                        {
                            if (this.SourceCarClass != 2)
                            {
                                if (displayName == "メーカー名" || displayName == "外製車名")
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (displayName == "開発符号" || displayName == "試作時期")
                                {
                                    continue;
                                }
                            }
                        }
                        //Delete Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //else if (displayName == "FLAG_ナビ付" || displayName == "FLAG_ETC付")
                        //{
                        //    if (displayName == "FLAG_ナビ付")
                        //    {
                        //        item.category = "ナビ付";
                        //    }
                        //    else
                        //    {
                        //        item.category = "ETC付";
                        //    }
                        //}
                        //Delete End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                    }
                    switch (displayName)
                    {
                        case "管理票番号":
                        case "登録ナンバー":
                        case "メーカー名":
                        case "外製車名":
                        case "号車":
                        case "駐車場番号":
                        case "仕向地":
                        case "排気量":
                        case "E_G型式":
                        case "駆動方式":
                        //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        //case "トランスミッション":
                        case "T/M":
                        //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
                        case "車体色":
                        case "リース満了日":
                        case "処分予定年月":
                            item.checkByte = 50;
                            break;

                        case "開発符号":
                        case "試作時期":
                        case "リースNO":
                            item.checkByte = 20;
                            break;


                        //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        //    case "FLAG_ナビ付":
                        //    case "FLAG_ETC付":
                        case "ナビ付":
                        case "ETC付":
                        //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
                        case "車型":
                        case "固定資産NO":
                            item.checkByte = 10;
                            break;

                        default:
                            item.checkByte = null;
                            break;
                    }
                    list.Add(item);
                }
            }

            return list;

        }
        #endregion

        #region クリア
        private void ClearScheduleItemDataGridView()
        {
            if(this.ScheduleItemDataGridView.DataSource != null)
            {
                var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                var list2 = list.Select(x => new GridViewDisplayItemModel { category = x.category, item = null, checkByte = x.checkByte }).ToList();

                this.ScheduleItemDataGridView.DataSource = null;
                this.ScheduleItemDataGridView.DataSource = list2;
            }
        }

        private void ClearCarDataGridView()
        {
            if (this.CarDataGridView.DataSource != null)
            {
                var list = (List<GridViewDisplayItemModel>)this.CarDataGridView.DataSource;
                var list2 = list.Select(x => new GridViewDisplayItemModel { category = x.category, item = null, checkByte = x.checkByte }).ToList();

                this.CarDataGridView.DataSource = null;
                this.CarDataGridView.DataSource = list2;
            }

        }
        #endregion

        #region 値変更
        private void ScheduleItemDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.ScheduleItemDataGridView.Rows[e.RowIndex].Cells[3].Value = true;
        }
        #endregion

        #region 値変更用プロパティセット
        private List<DataGridViewDisplayModel> SetItemForSetting()
        {
            var list = new List<DataGridViewDisplayModel>();
            var cond = new ScheduleItemDisplayConfigurationSearchModel
            {
                // ユーザーID
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //開発符号 = this.Item.GeneralCode
                開発符号 = this.Item.ID.ToString()
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            };

            // APIで取得
            var res = HttpUtil.GetResponse<ScheduleItemDisplayConfigurationSearchModel, ScheduleItemDisplayConfigurationModel>(ControllerType.ScheduleItemDisplayConfiguration, cond);


            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                //Update Start 2022/03/16 杉浦 スケジュール詳細不具合修正
                //return list;
                var itemData = new ScheduleItemDisplayConfigurationModel();
                //Update Start 2023/10/15 杉浦 スケジュール詳細表示変更
                //itemData.表示列名 = "管理票番号,開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                itemData.表示列名 = "開発符号,試作時期,メーカー名,外製車名,登録ナンバー,号車,駐車場番号,ナビ付,ETC付,仕向地,排気量,E_G型式,駆動方式,T/M,車型,車体色,リースNO,固定資産NO,リース満了日,処分予定年月,備考";
                //Update End 2023/10/15 杉浦 スケジュール詳細表示変更

                var itemDataList = new List<ScheduleItemDisplayConfigurationModel>();
                itemDataList.Add(itemData);

                res.Results = itemDataList;
                //Update End 2022/03/16 杉浦 スケジュール詳細不具合修正
            }

            if (this.ViewList == null) this.ViewList = res.Results.ToList();

            // 取得データの格納
            var data = res.Results.FirstOrDefault();

            // 取得できたかどうか
            if (data == null)
            {
                return list;
            }

            var index = 1;
            foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
            {

                var item = new DataGridViewDisplayModel()
                {
                    Name = displayName,
                    HeaderText = displayName,
                    DataPropertyName = displayName,
                    Visible = true,
                    DisplayIndex = index
                };
                list.Add(item);
                index++;
            }
            if (!string.IsNullOrEmpty(data.非表示列名))
            {
                foreach (var displayName in (data.非表示列名 ?? "").Split(',').Select(x => x.Trim()))
                {
                    var item = new DataGridViewDisplayModel()
                    {
                        Name = displayName,
                        HeaderText = displayName,
                        DataPropertyName = displayName,
                        Visible = false,
                        DisplayIndex = index
                    };
                    list.Add(item);
                    index++;
                }
            }

            return list;
        }
        

        private void ScheduleItemDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangeView && Messenger.Confirm("項目順序が変更されています。反映しますか?") != DialogResult.Yes)
            {
                this.ViewList[0].開発符号 = this.Item.ID.ToString();

                //登録
                var res = HttpUtil.PutResponse(ControllerType.ScheduleItemDisplayConfiguration, this.ViewList);

            }
            else
            {
                var list = (List<GridViewDisplayItemModel>)this.ScheduleItemDataGridView.DataSource;
                if (ChangeView)
                {
                    Messenger.Warn("登録ボタンを押下してください。");
                    e.Cancel = true;
                }

            }
        }
        #endregion

        #region 画面更新用
        //private List<GridViewDisplayItemModel> ChangeDisplay(List<GridViewDisplayItemModel> nowList)
        //{
        //    var list = new List<GridViewDisplayItemModel>();
        //    var cond = new ScheduleItemDisplayConfigurationSearchModel
        //    {
        //        // ユーザーID
        //        開発符号 = this.Item.GeneralCode
        //    };

        //    // APIで取得
        //    var res = HttpUtil.GetResponse<ScheduleItemDisplayConfigurationSearchModel, ScheduleItemDisplayConfigurationModel>(ControllerType.ScheduleItemDisplayConfiguration, cond);


        //    // レスポンスが取得できたかどうか
        //    if (res == null || res.Status != Const.StatusSuccess)
        //    {
        //        return list;
        //    }

        //    // 取得データの格納
        //    var data = res.Results.FirstOrDefault();

        //    // 取得できたかどうか
        //    if (data == null)
        //    {
        //        return list;
        //    }


        //    foreach (var displayName in (data.表示列名 ?? "").Split(',').Select(x => x.Trim()))
        //    {
        //        //Delete Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
        //        //if (displayName != "FLAG_ナビ付" && displayName != "FLAG_ETC付")
        //        //{
        //            //Delete End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
        //            if (displayName == "開発符号" || displayName == "メーカー名" || displayName == "試作時期" || displayName == "外製車名")
        //            {
        //                if (this.SinkCarClass != 2)
        //                {
        //                    if (displayName == "メーカー名" || displayName == "外製車名")
        //                    {
        //                        continue;
        //                    }
        //                }
        //                else
        //                {
        //                    if (displayName == "開発符号" || displayName == "試作時期")
        //                    {
        //                        continue;
        //                    }
        //                }
        //            }
        //            if(!nowList.Any(x => x.category == displayName))
        //            {
        //                var item = new GridViewDisplayItemModel() { category = displayName };
        //                switch (displayName)
        //                {
        //                    case "管理票番号":
        //                    case "登録ナンバー":
        //                    case "メーカー名":
        //                    case "外製車名":
        //                    case "号車":
        //                    case "駐車場番号":
        //                    case "仕向地":
        //                    case "排気量":
        //                    case "E_G型式":
        //                    case "駆動方式":
        //                    //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
        //                    //case "トランスミッション":
        //                    case "T/M":
        //                    //Update Start 2022/02/16 杉浦 「トランスミッション」⇒「T/M」
        //                    case "車体色":
        //                    case "リース満了日":
        //                    case "処分予定年月":
        //                        item.checkByte = 50;
        //                        break;

        //                    case "開発符号":
        //                    case "試作時期":
        //                    case "リースNO":
        //                        item.checkByte = 20;
        //                        break;

        //                //Update Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
        //                //    case "FLAG_ナビ付":
        //                //    case "FLAG_ETC付":
        //                    case "ナビ付":
        //                    case "ETC付":
        //                //Update End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
        //                    case "車型":
        //                    case "固定資産NO":
        //                        item.checkByte = 10;
        //                        break;

        //                    default:
        //                        item.checkByte = null;
        //                        break;
        //                }
        //                list.Add(item);
        //            }
        //            else
        //            {
        //                var row = nowList.Where(x => x.category == displayName).ToList();
        //                list.Add(row[0]);
        //            }
        //        //Delete Start 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす
        //        //} else if(displayName == "FLAG_ナビ付")
        //        //{
        //        //    var row = nowList.Where(x => x.category == "ナビ付").ToList();
        //        //    list.Add(row[0]);
        //        //}else
        //        //{
        //        //    var row = nowList.Where(x => x.category == "ETC付").ToList();
        //        //    list.Add(row[0]);
        //        //}
        //        //Delete End 2022/02/16 杉浦 ナビ・ETC付から「FLAG_」表記をなくす

        //    }

        //    return list;
        //}
        #endregion

        //Append End 2022/01/31 杉浦 項目を変動できるようにする
    }
}
