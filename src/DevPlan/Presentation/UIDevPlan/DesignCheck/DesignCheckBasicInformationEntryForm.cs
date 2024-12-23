using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 設計チェック基本情報登録
    /// </summary>
    public partial class DesignCheckBasicInformationEntryForm : BaseSubForm
    {
        #region メンバ変数
        private const int CarNameLength = 18;
        // 号車
        private string CarNo;
        // 完了予定日
        private string CompleteDate;

        //Update Start 2021/06/30 張晋華 開発計画表設計チェック機能改修
        // FLAG_試作CLOSE(試作部ステータス)
        private int? status;
        //Update End 2021/06/30 張晋華 開発計画表設計チェック機能改修

        // FLAG_処置不要
        private int? NoProcedure;
        // 織込日程
        private DateTime? OrigomeDate;
        // 試験車名
        private string CarName;
        // 編集者日
        private DateTime? EditDate;

        private BindingSource bindingSource = new BindingSource();

        private Func<object, string> cnvTrimingString = obj =>
        {
            return obj == null ? "" : Convert.ToString(obj).Replace(" ", "");
        };

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; } = false;

        /// <summary>再読込フラグ</summary>
        public bool IsRefresh { get; set; } = false;

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修
        /// <summary>Excelフィルター</summary>
        private const string ExcelFilter = "Excel ブック(*.xlsx;*.xls)|*.xlsx;*.xls";

        /// <summary>ディスクトップパス取得</summary>
        private string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        /// <summary>インポートリスト</summary>
        private List<DesignCheckImportModel> ImportList { get; set; } = new List<DesignCheckImportModel>();
        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "設計チェック基本情報登録"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; }

        /// <summary>設計チェック対象車</summary>
        private List<DesignCheckCarGetOutModel> DesignCheckCarList { get; set; } = new List<DesignCheckCarGetOutModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckBasicInformationEntryForm()
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
        private void DesignCheckBasicInformationEntryForm_Load(object sender, EventArgs e)
        {
            //Append Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
            this.ImportList = null;
            //Append End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)

            FormControlUtil.FormWait(this, () =>
            {
                // 更新
                if (this.DesignCheck.ID != null)
                {
                    //設計チェックの取得
                    this.DesignCheck = this.GetDesignCheck();

                    //設計チェック対象車の取得
                    this.DesignCheckCarList = this.GetDesignCheckCarList();
                }

                // バインドフラグOn
                this.IsBind = true;

                try
                {
                    // 画面初期化
                    this.InitForm();

                    // グリッド初期化
                    this.InitDataGridView();

                    // 全選択チェックボックス調整
                    this.AdjustAllSelectCheckBox();
                }
                finally
                {
                    // バインドフラグOff
                    this.IsBind = false;
                }

            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //開催日
            this.EventDateTimePicker.Value = this.DesignCheck.開催日;

            //開催回
            this.HoldingTimesTextBox.Text = this.DesignCheck.回 == null ? "" : this.DesignCheck.回.ToString();

            //設計チェック名
            this.DesignCheckNameTextBox.Text = this.DesignCheck.名称;
        }

        /// <summary>
        /// グリッド初期化
        /// </summary>
        private void InitDataGridView()
        {
            //列の自動生成不可
            this.ListDataGridView.AutoGenerateColumns = false;

            //データソース
            this.bindingSource.DataSource = this.DesignCheckCarList;
            this.ListDataGridView.DataSource = this.bindingSource;

            //一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;
        }

        /// <summary>
        /// 全選択チェックボックス調整
        /// </summary>
        private void AdjustAllSelectCheckBox()
        {
            var grid = this.ListDataGridView;
            var corner = grid.TopLeftHeaderCell;
            var cbox = this.AllSelectCheckBox;

            var x = (corner.Size.Width - cbox.Width) / 2 + grid.Location.X - 3;
            var y = (corner.Size.Height - cbox.Height) / 2 + grid.Location.Y + 2;

            cbox.Location = new Point(x, y);
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckBasicInformationEntryForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = this.EventDateTimePicker;
            this.EventDateTimePicker.Focus();
        }
        #endregion

        #region Sys登録済車両ボタンクリック
        /// <summary>
        /// Sys登録済車両ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemRegisteredCarAddButton_Click(object sender, EventArgs e)
        {
            using (var form = new DesignCheckSystemTestCarListForm())
            {
                //試験車一覧(試験車管理)画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //設計チェック対象車追加
                    this.AddDesignCheckCar(form.SelectedTestCarList, (x => new DesignCheckCarGetOutModel
                    {
                        試験車_ID = x.ID,
                        管理票NO = x.管理票NO,
                        試験車名 = x.試験車名,
                        グレード = x.グレード,
                        試験目的 = x.試験目的,
                        開発符号 = x.開発符号,
                        試作時期 = x.試作時期,
                        号車 = x.号車,
                        排気量 = x.排気量,
                        E_G型式 = x.E_G型式,
                        駆動方式 = x.駆動方式,
                        トランスミッション = x.トランスミッション
                    }));
                }
            }
        }
        #endregion

        #region 登録済車両ボタンクリック
        /// <summary>
        /// 登録済車両ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisteredCarAddButton_Click(object sender, EventArgs e)
        {
            using (var form = new DesignCheckTestCarListForm())
            {
                //試験車一覧(設計チェック)画面表示
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    //設計チェック対象車追加
                    this.AddDesignCheckCar(form.SelectedTestCarList, (x => new DesignCheckCarGetOutModel
                    {
                        試験車_ID = x.ID,
                        管理票NO = x.管理票NO,
                        試験車名 = x.試験車名,
                        グレード = x.グレード,
                        試験目的 = x.試験目的,
                        開発符号 = x.開発符号,
                        試作時期 = x.試作時期,
                        号車 = x.号車,
                        排気量 = x.排気量,
                        E_G型式 = x.E_G型式,
                        駆動方式 = x.駆動方式,
                        トランスミッション = x.トランスミッション
                    }));
                }
            }
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
            // 設計チェック対象車の追加
            this.AddEmptyDesignCheckCar();
        }

        /// <summary>
        /// 設計チェック対象車の追加（空行・インクリメント行）
        /// </summary>
        private void AddEmptyDesignCheckCar()
        {
            var grid = this.ListDataGridView;

            // 追加データ設定
            var data = new DesignCheckCarGetOutModel();
            if (this.IncrementCheckBox.Checked && grid.RowCount > 0)
            {
                var row = grid.Rows[grid.RowCount - 1];
                var vehicle = Convert.ToString(row.Cells[this.VehicleColumn.Name].Value).Trim();
                double d = 0;

                if (double.TryParse(vehicle, out d))
                {
                    // 号車が2桁、または3桁の場合
                    if (vehicle.Length == 2 || vehicle.Length == 3)
                    {
                        vehicle = Convert.ToString(d + 1);
                    }
                }

                data.開発符号 = Convert.ToString(row.Cells[this.GeneralCodeColumn.Name].Value);
                data.試作時期 = Convert.ToString(row.Cells[this.PrototypeTimingColumn.Name].Value);
                data.号車 = vehicle;
            }

            //一覧に反映
            this.DesignCheckCarList.Add(data);
            this.bindingSource.ResetBindings(false);

            // フォーカス調整
            grid.CurrentCell = grid[this.VehicleColumn.Index, grid.Rows.Count - 1];
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
            FormControlUtil.FormWait(this, () =>
            {
                // 削除対象
                var cars = GetSelectCars();

                // 削除チェック
                if (IsDeleteDesignCheckCar(cars))
                {
                    // 設計チェック対象車の削除
                    this.DeleteDesignCheckCar(cars);
                }
            });
        }

        /// <summary>
        /// 車両削除を削除してよいか？
        /// </summary>
        /// <param name="cars"></param>
        /// <returns></returns>
        private bool IsDeleteDesignCheckCar(List<DesignCheckCarGetOutModel> cars)
        {
            // 選択チェック
            if (cars.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            // 新規作成チェック
            if (DesignCheck.ID == null)
            {
                return true;
            }

            //パラメータ設定
            var cond = new DesignCheckPointGetInModel
            {
                // 開催日_ID
                開催日_ID = this.DesignCheck.ID,

                // 最新フラグ
                NEW_FLG = true
            };

            // 指摘で使用されている車を取得
            var res = HttpUtil.GetResponse<DesignCheckPointGetInModel, DesignCheckPointGetOutModel>(ControllerType.DesignCheckPoint, cond);

            // レスポンスが取得できたかどうか
            if (res == null)
            {
                return false;
            }

            // 指摘存在チェック
            if (cars.Any((x) => res.Results.Any((y) => x.ID == y.対象車両_ID)))
            {
                // 削除確認（指摘あり）
                if (Messenger.Confirm(Resources.KKM02008) != DialogResult.Yes)
                {
                    return false;
                }
            }
            else
            {
                // 削除確認
                if (Messenger.Confirm(Resources.KKM00007) != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 選択されている車を取得します。
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckCarGetOutModel> GetSelectCars()
        {
            var list = new List<DesignCheckCarGetOutModel>();

            for (var i = 0; i < this.ListDataGridView.RowCount; i++)
            {
                var row = this.ListDataGridView.Rows[i];

                //選択しているかどうか
                if (Convert.ToBoolean(row.Cells[this.SelectedColumn.Name].Value) == true)
                {
                    //削除対象を追加
                    list.Add(this.DesignCheckCarList[i]);
                }
            }

            return list;
        }

        /// <summary>
        /// 設計チェック対象車の削除
        /// </summary>
        private void DeleteDesignCheckCar(List<DesignCheckCarGetOutModel> list)
        {
            //レスポンスが取得できたかどうか
            var res = HttpUtil.DeleteResponse(ControllerType.DesignCheckCar, list);

            // レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return;
            }

            // バインドフラグOn
            this.IsBind = true;

            try
            {
                // 一覧に反映
                this.DesignCheckCarList.RemoveAll(x => list.Contains(x) == true);
                this.bindingSource.ResetBindings(false);

                // 全選択チェック初期化
                this.AllSelectCheckBox.Checked = false;

                // 一覧を未選択
                this.ListDataGridView.CurrentCell = null;
            }
            finally
            {
                // バインドフラグOff
                this.IsBind = false;
            }

            //削除後メッセージ
            Messenger.Info(Resources.KKM00003);
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
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // 一覧に反映
                this.DesignCheckCarList.ForEach((x) => x.Selected = this.AllSelectCheckBox.Checked);
                this.bindingSource.ResetBindings(false);

                // 一覧を未選択
                this.ListDataGridView.CurrentCell = null;
            });
        }
        #endregion

        //Append Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckBasicInformationEntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (this.ImportList != null)
                {
                    //Update Start 2021/07/28 杉浦
                    //取り込んだすべてのデータの登録を中止します。
                    //登録していないデータがありますがよろしいですか？
                    //if (!(Messenger.Confirm(Resources.KKM00052) == DialogResult.Yes))
                    if (!(Messenger.Confirm(Resources.KKM00053) == DialogResult.Yes))
                    //Update End 2021/07/28 杉浦
                    {
                        //「いいえ」を選択した場合→閉じさせない
                        e.Cancel = true;
                        return;
                    }
                }
            });
        }
        #endregion
        //Append End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)

        #region 登録ボタン
        /// <summary>
        /// 登録ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //設計チェックのチェック
                if (this.IsEntryDesignCheck() == true)
                {
                    //設計チェックの登録
                    if (this.EntryDesignCheck() == true)
                    {
                        //フォームクローズ
                        base.FormOkClose();
                    }
                }
            });
        }

        /// <summary>
        /// 設計チェックのチェック
        /// </summary>
        /// <returns></returns>
        private bool IsEntryDesignCheck()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var errMsg = new StringBuilder();
            var errIdx = -1;

            map[this.ListDataGridView] = (c, name) =>
            {
                foreach (DataGridViewRow row in this.ListDataGridView.Rows)
                {
                    var number = row.Cells[this.ManagementNoColumn.Name];
                    var textList = (new[] { row.Cells[this.GeneralCodeColumn.Name], row.Cells[this.PrototypeTimingColumn.Name], row.Cells[this.VehicleColumn.Name] }).ToList();

                    // 試験車名
                    var text = string.Concat(textList.Select(x => cnvTrimingString(x.Value)));

                    // 必須チェック
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        // 一行のみ表示
                        if (errMsg.ToString().IndexOf(Resources.KKM00022) < 0)
                        {
                            //エラーメッセージ
                            errMsg.AppendLine(string.Concat("開発符号・試作時期・号車は", Resources.KKM00022));

                            //エラーインデックス
                            errIdx = errIdx < 0 ? row.Index : errIdx;
                        }

                        // 背景色を変更
                        textList.ForEach(x => x.Style.BackColor = Const.ErrorBackColor);
                    }

                    // バイト数チェック
                    if (StringUtil.SjisByteLength(text) > CarNameLength)
                    {
                        // 一行のみ表示
                        if (errMsg.ToString().IndexOf(Resources.KKM03028) < 0)
                        {
                            //エラーメッセージ
                            errMsg.AppendLine(Resources.KKM03028);

                            //エラーインデックス
                            errIdx = errIdx < 0 ? row.Index : errIdx;
                        }

                        // 背景色を変更
                        textList.ForEach(x => x.Style.BackColor = Const.ErrorBackColor);
                    }

                    // 重複チェック
                    if (!string.IsNullOrWhiteSpace(text) &&
                        this.DesignCheckCarList.Count(y =>
                            cnvTrimingString(y.管理票NO) == cnvTrimingString(number.Value) &&
                            cnvTrimingString(y.開発符号) == cnvTrimingString(textList[0].Value) &&
                            cnvTrimingString(y.試作時期) == cnvTrimingString(textList[1].Value) &&
                            cnvTrimingString(y.号車) == cnvTrimingString(textList[2].Value)) > 1)
                    {
                        // 一行のみ表示
                        if (errMsg.ToString().IndexOf(Resources.KKM03044) < 0)
                        {
                            //エラーメッセージ
                            errMsg.AppendLine(Resources.KKM03044);

                            //エラーインデックス
                            errIdx = errIdx < 0 ? row.Index : errIdx;
                        }

                        // 背景色を変更
                        number.Style.BackColor = Const.ErrorBackColor;
                        textList.ForEach(x => x.Style.BackColor = Const.ErrorBackColor);
                    }
                }

                return errMsg.ToString();
            };

            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this, map);
            if (msg != "")
            {
                // スクロール調整
                //Append Start 2021/08/18 杉浦 設計チェック修正
                if (errIdx >= 0)
                {
                    //Append End 2021/08/18 杉浦 設計チェック修正
                    this.ListDataGridView.FirstDisplayedScrollingRowIndex = errIdx;
                    // 一覧を未選択
                    this.ListDataGridView.CurrentCell = null;
                    //Append Start 2021/08/18 杉浦 設計チェック修正

                }
                //Append End 2021/08/18 杉浦 設計チェック修正
                
                Messenger.Warn(msg);
                return false;
            }

            //Append Start 2021/07/07 張晋華 開発計画表設計チェック機能改修(保守対応)
            // 対象車両登録チェック
            if (this.ListDataGridView.Rows.Count == 0)
            {
                //エラーメッセージ
                errMsg.AppendLine(string.Concat("対象車両は", Resources.KKM00022));
                Messenger.Warn(errMsg.ToString());
                return false;
            }
            //Append End 2021/07/07 張晋華 開発計画表設計チェック機能改修(保守対応)

            return true;
        }

        /// <summary>
        /// 設計チェックの登録
        /// </summary>
        /// <returns></returns>
        private bool EntryDesignCheck()
        {
            ResponseDto<DesignCheckGetOutModel> designCheckRes = null;

            var kaisaibiId = this.DesignCheck.ID;

            //登録設計チェック設定
            this.SetEntryDesignCheck();

            //開催日IDがあるかどうか
            if (kaisaibiId == null)
            {
                //追加
                designCheckRes = HttpUtil.PostResponse<DesignCheckGetOutModel>(ControllerType.DesignCheck, this.DesignCheck);
            }
            else
            {
                //更新
                designCheckRes = HttpUtil.PutResponse<DesignCheckGetOutModel>(ControllerType.DesignCheck, this.DesignCheck);
            }

            //レスポンスが取得できたかどうか
            if (designCheckRes == null || designCheckRes.Status != Const.StatusSuccess)
            {
                return false;
            }

            //試験車があるかどうか
            if (this.DesignCheckCarList != null && this.DesignCheckCarList.Any() == true)
            {
                //開催日IDがあるかどうか
                if (kaisaibiId == null)
                {
                    kaisaibiId = designCheckRes.Results.First().ID;
                }

                //開催日ID
                this.DesignCheckCarList.ForEach(x => x.開催日_ID = kaisaibiId.Value);

                //更新
                var carRes = HttpUtil.PostResponse(ControllerType.DesignCheckCar, this.DesignCheckCarList);

                //レスポンスが取得できたかどうか
                if (carRes == null || carRes.Status != Const.StatusSuccess)
                {
                    return false;
                }
            }

            //Update Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
            if (this.ImportList != null)
            {
                //開催日ID
                this.ImportList.ForEach(x => x.開催日_ID = kaisaibiId);

                //登録実行
                if (this.PostImportList() != true)
                {
                    //重複メッセージ
                    Messenger.Warn(Resources.KKM00049);

                    return false;
                }

                //EXCEL_INPUTに開催日_ID、登録日、登録者_IDを登録する
                var list = new List<DesignCheckExcelInputModel>();

                list.Add(new DesignCheckExcelInputModel
                {
                    //開催日ID
                    開催日_ID = kaisaibiId,
                    //登録者_ID
                    登録者_ID = SessionDto.UserId,
                });

                HttpUtil.PostResponse<DesignCheckExcelInputModel, DesignCheckExcelInputModel>(ControllerType.DesignCheckExcelInput, list);
            }
            //Update End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            //Append Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
            this.ImportList = null;
            //Append End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)

            return true;
        }

        /// <summary>
        /// 登録設計チェック設定
        /// </summary>
        private void SetEntryDesignCheck()
        {
            //開催日
            this.DesignCheck.開催日 = this.EventDateTimePicker.SelectedDate;

            //開催回
            this.DesignCheck.回 = this.HoldingTimesTextBox.Text.Trim() == "" ? null : (int?)int.Parse(this.HoldingTimesTextBox.Text.Trim());

            //設計チェック名称
            this.DesignCheck.名称 = this.DesignCheckNameTextBox.Text;
        }
        #endregion

        #region 設計チェック対象車追加
        /// <summary>
        /// 設計チェック対象車追加
        /// </summary>
        /// <param name="list">リスト</param>
        /// <param name="convert">変換</param>
        private void AddDesignCheckCar<T>(List<T> list, Func<T, DesignCheckCarGetOutModel> convert)
        {
            // 重複行削除
            list?.RemoveAll(x => this.DesignCheckCarList.Any(y =>
                            cnvTrimingString(y.管理票NO) == cnvTrimingString(convert(x).管理票NO) &&
                            cnvTrimingString(y.開発符号) == cnvTrimingString(convert(x).開発符号) &&
                            cnvTrimingString(y.試作時期) == cnvTrimingString(convert(x).試作時期) &&
                            cnvTrimingString(y.号車) == cnvTrimingString(convert(x).号車)));

            // すべて登録済の車両の場合
            if (list == null || list?.Count == 0)
            {
                Messenger.Warn(Resources.KKM03032);
                return;
            }

            //追加対象があるかどうか
            if (list != null && list.Any() == true)
            {
                var count = this.ListDataGridView.Rows.Count;

                //一覧に反映
                this.DesignCheckCarList.AddRange(list.Select(x => convert(x)));
                this.bindingSource.ResetBindings(false);

                // スクロール調整
                this.ListDataGridView.FirstDisplayedScrollingRowIndex = count;

                // 一覧を未選択
                this.ListDataGridView.CurrentCell = null;
            }
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheck()
        {
            var cond = new DesignCheckGetInModel
            {
                //開催日ID
                ID = this.DesignCheck.ID
            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckGetInModel, DesignCheckGetOutModel>(ControllerType.DesignCheck, cond);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return new DesignCheckGetOutModel();
            }

            return res.Results.FirstOrDefault();
        }

        /// <summary>
        /// 設計チェック対象車の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckCarGetOutModel> GetDesignCheckCarList()
        {
            var cond = new DesignCheckCarGetInModel
            {
                //開催日ID
                開催日_ID = this.DesignCheck.ID
            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckCarGetInModel, DesignCheckCarGetOutModel>(ControllerType.DesignCheckCar, cond);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return new List<DesignCheckCarGetOutModel>();
            }

            return res.Results.ToList();
        }
        #endregion

        #region コントローラ値変更
        /// <summary>
        /// コントローラ値変更
        /// </summary>
        private void Controller_Changed(object sender, EventArgs e)
        {
            // バインド中は終了
            if (this.IsBind)
            {
                return;
            }

            this.IsRefresh = true;
        }
        #endregion

        #region 試作車不具合情報取込みボタンクリック

        //Append Start 2021/06/14 張晋華 開発計画表設計チェック機能改修

        private void ImprotButton_Click(object sender, EventArgs e)
        {
            // 選択項目設定
            this.SetPrint();
        }

        //Append End 2021/06/14 張晋華 開発計画表設計チェック機能改修

        #endregion

        #region Excel取込処理

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// Excel取込処理
        /// </summary>
        private void SetPrint()
        {
            //Append Start 2021/08/20 杉浦 設計チェック請負
            // 指摘で使用されている車を取得
            var res = HttpUtil.GetResponse<DesignCheckImportPathModel, DesignCheckImportPathModel>(ControllerType.DesignCheckImportGetPath, null);

            // レスポンスが取得できたかどうか
            var pathList = new List<DesignCheckImportPathModel>();
            pathList.AddRange(res.Results);
            //Append End 2021/08/20 杉浦 設計チェック請負

            //Update Start 2021/08/20 杉浦 設計チェック請負
            //var frm = new OpenFileDialog() { Filter = ExcelFilter, FilterIndex = 2, InitialDirectory = DesktopPath };
            var frm = new OpenFileDialog() { Filter = ExcelFilter, FilterIndex = 2, InitialDirectory = pathList[0].PATH };
            //Update End 2021/08/20 杉浦 設計チェック請負

            if (frm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                // Excel ファイル判定
                if (!FileUtil.IsFileExcel(frm.FileName))
                {
                    Messenger.Warn(Resources.TCM03018); // ファイルアクセス失敗

                    return;
                }

                //ファイルの読み込み
                if (this.ImportFile(frm.FileName))
                {
                    Messenger.Info(Resources.KKM00048); // データ読み込み完了のメッセージ
                }
            });
        }

        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

        #endregion

        #region ファイルの読み込み

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// ファイルの読み込み
        /// </summary>
        /// <param name="path"></param>
        private bool ImportFile(string path)
        {
            Func<string, DateTime?> getDateTime = dt => string.IsNullOrWhiteSpace(dt) ? null : (DateTime?)Convert.ToDateTime(dt);
            //エラーメッセージ
            Func<int, string, string> cnvLineFormat = (no, msg) => string.Concat(String.Format("{0, 3}", no), "行目：", msg);

            var list = new List<DesignCheckImportModel>();
            var exculude = new StringBuilder();
            var error = new StringBuilder();
            var rowno = 7;

            using (var xls = new XlsUtil(path))
            {
                //Append Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
                foreach (var cell in xls.GetRowsList(0, 6, 6, 11, 33).Where(x => x.Count > 1))
                {
                    if (cell[0] != "№"
                      || cell[1] != "日付"
                      || cell[2] != "記入者"
                      || cell[3] != "係"
                      || cell[4] != "大分類"
                      || cell[5] != "分類"
                      || cell[6] != "ブロック"
                      || cell[7] != "不具合(改修)・内容(現象)"
                      || cell[8] != "発生原因"
                      || cell[9] != "処理方法"
                      || cell[10] != "部署"
                      || cell[11] != "担当者"
                      || cell[12] != "内線"
                      || cell[13] != "確認\n工数"
                      || cell[14] != "対応工数\n(台)"
                      || cell[15] != "停滞時間\n(H)"
                      || cell[16] != "対応\n台数"
                      || cell[17] != "備考"
                      || cell[18] != "備考(艤装)"
                      || cell[19] != "資料"
                      || cell[20] != "クローズ担当"
                      || cell[21] != "対応状況")
                    {
                        Messenger.Warn(Resources.KKM00051.ToString());

                        return false;
                    }
                }
                //Append End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)

                foreach (var cell in xls.GetRowsList(0, 2, 2, 11, 12).Where(x => x.Count > 1))
                {
                    // 号車取得
                    CarNo = cell[0];

                    //Append Start 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
                    if (CarNo.StartsWith("#"))
                    {
                        // 号車の先頭の#を削除する
                        CarNo = CarNo.TrimStart('#');
                    }
                    //Append End 2021/07/16 張晋華 開発計画表設計チェック機能改修(保守対応)
                }

                //Append Start 2021/06/16 張晋華 開発計画表設計チェック機能改修
                // 画面で同じ号車は存在するか
                var isSame = false;

                foreach (DataGridViewRow row in this.ListDataGridView.Rows)
                {
                    // 管理票NO
                    var number = row.Cells[this.ManagementNoColumn.Name];
                    // 開発符号 + 試作時期 + 号車
                    var textList = (new[] { row.Cells[this.GeneralCodeColumn.Name], row.Cells[this.PrototypeTimingColumn.Name], row.Cells[this.VehicleColumn.Name] }).ToList();
                    // 号車
                    var text = cnvTrimingString(textList[2].Value);

                    CarNo = Strings.StrConv(CarNo, VbStrConv.Narrow);// 半角に変換する
                    text = Strings.StrConv(text, VbStrConv.Narrow);// 半角に変換する

                    // 取り込んだデータの号車は数値であるか
                    var ret = IsNumberic(CarNo);
                    // 画面の号車は数値であるか
                    var ret2 = IsNumberic(text);

                    // 両方数値の場合
                    if (ret && ret2)
                    {
                        var k1 = Convert.ToInt32(CarNo);
                        var k2 = Convert.ToInt32(text);

                        if (k1 == k2)
                        {
                            isSame = true;

                            // 画面で同じ号車が存在する場合、その試験車名(開発符号＋試作時期＋号車)を取得する
                            CarName = cnvTrimingString(textList[0].Value) + " " + cnvTrimingString(textList[1].Value) + " " + cnvTrimingString(textList[2].Value);
                            break;
                        }
                    }
                    // それ以外の場合
                    else
                    {
                        var k1 = CarNo.ToUpper();// 大文字に変換する
                        var k2 = text.ToUpper();// 大文字に変換する

                        if (k1 == k2)
                        {
                            isSame = true;

                            // 画面で同じ号車が存在する場合、その試験車名(開発符号＋試作時期＋号車)を取得する
                            CarName = cnvTrimingString(textList[0].Value) + " " + cnvTrimingString(textList[1].Value) + " " + cnvTrimingString(textList[2].Value);
                            break;
                        }
                    }
                }

                if (!isSame)
                {
                    error.AppendLine(Resources.KKM00050);
                }
                //Append End 2021/06/16 張晋華 開発計画表設計チェック機能改修

                foreach (var cell in xls.GetRowsList(0, 1, 1, 16, 17).Where(x => x.Count > 1))
                {
                    //完了予定日取得
                    CompleteDate = cell[0];
                }

                foreach (var cell in xls.GetRowsList(0, rowno, null, 11, 33).Where(x => x.Count > 22))
                {
                    //FLAG_試作CLOSE(試作部ステータス)の初期値は0である→OPEN
                    status = 0;
                    NoProcedure = null;
                    OrigomeDate = null;
                    EditDate = null;

                    rowno++;

                    // 空行は行カウントのみでスキップ
                    if (!cell.Any(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        continue;
                    }

                    //入力チェック
                    var tuple = TupleImportDataCheck(cell);

                    //重複チェック
                    if (cell[0] != "")
                    {
                        if (list.Any(x => x.指摘NO == Convert.ToInt32(cell[0])))
                        {
                            tuple.Add("指摘NOの" + Resources.KKM03044);
                        }
                    }

                    //エラー
                    if (tuple.Any() == true)
                    {
                        tuple.ForEach(x => error.AppendLine(cnvLineFormat(rowno, x)));

                        continue;
                    }


                    //対応状況で判断するカラム
                    if (cell[21] == "●" || cell[21] == "◆")
                    {
                        //Update Start 2021/06/30 張晋華 開発計画表設計チェック機能改修
                        //FLAG_試作CLOSE(試作部ステータス)をCLOSEにする
                        status = 1;
                        //Update End 2021/06/30 張晋華 開発計画表設計チェック機能改修

                        //織込日程を完了予定日にする
                        OrigomeDate = DateTime.Parse(CompleteDate);
                    }
                    if (cell[21] == "×")
                    {
                        //FLAG_処置不要をチェックにする
                        NoProcedure = 1;
                    }

                    //Append Start 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)
                    if (!string.IsNullOrWhiteSpace(cell[1]))
                    {
                        EditDate = DateTime.Parse(cell[1]);
                    }
                    //Append End 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)

                    list.Add(new DesignCheckImportModel
                    {
                        試作管理NO = Convert.ToInt32(cell[0]),
                        試験車名 = CarName,// 試験車名
                        部品 = cell[6],
                        状況 = cell[7],
                        処置課 = cell[3],
                        処置対象 = cell[8],
                        処置方法 = cell[9],
                        処置調整 = cell[10] + "\n" + cell[11] + "\n" + cell[12],
                        担当課名 = cell[3],
                        担当者_ID = cell[2],
                        //Update Start 2021/07/15 張晋華 開発計画表設計チェック機能改修
                        編集者日 = EditDate,
                        //Update End 2021/07/15 張晋華 開発計画表設計チェック機能改修
                        編集者_ID = cell[2],
                        //Update Start 2021/06/30 張晋華 開発計画表設計チェック機能改修
                        FLAG_試作CLOSE = status,
                        //Update End 2021/06/30 張晋華 開発計画表設計チェック機能改修
                        FLAG_処置不要 = NoProcedure,
                        織込日程 = OrigomeDate,
                    });
                }
            }

            // エラー
            if (error.Length > 0)
            {
                Messenger.Warn(error.ToString());

                return false;
            }

            if (list.Any() == false)
            {
                Messenger.Warn(string.Concat(Resources.TCM03008, exculude.Length > 0 ? Const.CrLf + Const.CrLf + exculude.ToString() : ""));

                return false;
            }

            // インポートリストのセット
            this.ImportList = list;

            return true;
        }

        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

        #endregion

        #region 数値のチェック
        private bool IsNumberic(string text)
        {
            try
            {
                int var = Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region インポートデータのチェック

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// インポートデータのチェック
        /// </summary>
        private List<string> TupleImportDataCheck(List<string> cell)
        {
            var result = new List<string>();

            #region チェックデリゲード

            // 必須チェック
            Func<string, string, bool> isRequired = (name, value) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result.Add(string.Format(Resources.KKM00001, name));

                    return false;
                }

                return true;
            };

            // 桁数チェック（Byte）
            Func<string, string, int, bool> isWide = (name, value, length) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (StringUtil.SjisByteLength(value) > length)
                    {
                        result.Add(string.Format(Resources.KKM00027, name));

                        return false;
                    }
                }

                return true;
            };

            // 型式チェック（Date）
            Func<string, string, bool> isDate = (name, value) =>
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (!Regex.IsMatch((string)value, @"\b(?<year>\d{4})/(?<month>\d{1,2})/(?<day>\d{1,2})\b"))
                    {
                        result.Add(string.Format(Resources.KKM00026, name));

                        return false;
                    }
                }

                return true;
            };

            #endregion

            // 数値・桁数・形式チェック
            isRequired("No.", cell[0]);
            //Delete Start 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)
            //isRequired("係", cell[3]);
            //isRequired("記入者", cell[2]);
            //isRequired("日付", cell[1]);
            //Delete End 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)
            isWide("ブロック", cell[6], 100);
            isWide("不具合(改修)・内容(現象)", cell[7], 800);
            isWide("係", cell[3], 10);
            //Update Start 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)
            isWide("発生原因", cell[8], 800);
            //Update End 2021/07/15 張晋華 開発計画表設計チェック機能改修(保守対応)
            isWide("処理方法", cell[9], 800);
            isWide("部署・担当者・内線の合計", cell[10] + "\n" + cell[11] + "\n" + cell[12], 200);
            isWide("記入者", cell[2], 10);
            isDate("日付", cell[1]);

            return result;
        }

        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

        #endregion

        #region データの操作

        //Append Start 2021/05/17 張晋華 開発計画表設計チェック機能改修

        /// <summary>
        /// インポートデータの登録（更新）
        /// </summary>
        private bool PostImportList()
        {
            var res = HttpUtil.PostResponse<DesignCheckImportModel, DesignCheckImportModel>(ControllerType.DesignCheckImport, this.ImportList);

            this.ImportList = res?.Results?.ToList();

            return res?.Results.Count() > 0;
        }
        //Append End 2021/05/17 張晋華 開発計画表設計チェック機能改修

        #endregion
    }
}
