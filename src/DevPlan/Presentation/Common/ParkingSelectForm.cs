using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Microsoft.Win32;

using DevPlan.Presentation.Base;

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;
using System.IO;
using AxAcroPDFLib;
using DevPlan.Presentation.UITestCar.ControlSheet;
using DevPlan.UICommon.Util;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// 駐車場検索
    /// </summary>
    public partial class ParkingSelectForm : BaseSubForm
    {
        /// <summary>画面タイトル</summary>
        public override string FormTitle { get { return "駐車場検索"; } }
        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        private const string MODE_EDIT = "編集モードへ";
        private const string MODE_SELECT = "選択モードへ";
        private const string STATUS_USE = "使用中";
        private const string STATUS_FREE = "空き";
        private const string STATUS_ALL = "全て";
        //Append Start 2021/11/09 杉浦 エリアラジオボタン追加
        private const string AREA = "エリアのみ";
        //Append End 2021/11/09 杉浦 エリアラジオボタン追加

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>項目テーブル一覧</summary>
        private Dictionary<string, short> StatusItemTable = new Dictionary<string, short>()
        {
            //空き
            { STATUS_FREE, 0 },
            //使用中
            { STATUS_USE, 1 },
            //Append Start 2021/11/09 杉浦 エリアラジオボタン追加
            //エリア
            { AREA, 2 },
            //Append End 2021/11/09 杉浦 エリアラジオボタン追加
        };

        /// <summary>区画情報グリッド</summary>
        private DataGridViewUtil<ParkingModel> gridUtil = null;

        /// <summary>所在地リスト</summary>
        private List<ParkingModel> locationList;

        /// <summary>エリアリスト</summary>
        private List<ParkingModel> areaList;

        //新規表示するPDFを作成
        private AxAcroPDF pdf = null;

        // DCのレジストリ設定
        string rKeyName = @"Software\Adobe\Acrobat Reader\DC\AVGeneral";
        // ツールパネルの状態
        string rGetValueToolPanelState = "bExpandRHPInViewer";
        // ツールパネルの状態を記憶
        string rGetValueToolPanelStateMemory = "bRHPSticky";


        //Append Start 2021/10/07 矢作

        /// <summary>選択中駐車場の行</summary>
        private DataGridViewRow selectedRow;

        /// <summary>選択中の行のエリアNo</summary>
        private string selectedAreaNo = "";

        //Append End 2021/10/07 矢作

        #region <<< 外部フォームとやり取りするためのプロパティ >>>
        /// <summary>
        /// 選択データ
        /// </summary>
        public ParkingModel Data { get; set; }

        /// <summary>
        /// 駐車場選択モードフラグ
        /// </summary>
        public bool IsSelectMode { get; set; } = false;

        //Append Start 2021/10/07 矢作

        /// <summary>
        /// 試験車検索から開いたかどうかのフラグ
        /// </summary>
        public bool FromTestCar { get; set; } = false;

        //Append End 2021/10/07 矢作

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParkingSelectForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemCopyMoveForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            //区画リスト表示初期化
            this.InitGrid();

            // 初期表示フォーカス
            this.ActiveControl = this.LocationComboBox;
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //管理権限のないログインユーザの場合、選択モードの場合、モードボタン、状態更新ボタンは表示しない。
            if (this.UserAuthority.MANAGEMENT_FLG != '1' || this.IsSelectMode)
            {
                this.ModeButton.Visible = false;
                this.UpdateButton.Visible = false;
            }
            
            //所在地コンボボックスを作成
            this.CreateLocationComboBox();

            //エリアコンボボックスを作成
            this.CreateAreaComboBox();

            //PDF表示用レジストリの設定
            // DC-環境設定-分類-文書-開き方の設定-「ツールパネルの現在の状態を記憶」にチェック
            bool setReg1 = this.SetRegistry(this.rGetValueToolPanelStateMemory, 1);
            // DC-ツールパネルを非展開
            bool setReg2 = this.SetRegistry(this.rGetValueToolPanelState, 0);
            if(setReg1 == true && setReg2 == true)
            {
                try
                {
                    //表示用PDFをパネルに追加
                    this.pdf = new AxAcroPDF();
                    this.ListFormMainPanel.Controls.Add(pdf);

                    //表示位置
                    var scale = new DeviceUtil().GetScalingFactor();
                    this.pdf.Location = new Point(Convert.ToInt32(70 * scale), Convert.ToInt32(70 * scale));
                    var xSize = Convert.ToInt32(Math.Ceiling(540 * Math.Sqrt(2)) * scale);
                    var ySize = Convert.ToInt32(540 * scale);
                    this.pdf.Size = new Size(xSize, ySize);
                }
                catch (Exception ex)
                {
                    //インストール確認メッセージ
                    Messenger.Error(Resources.TCM03025, ex);
                    logger.Error(ex.Message, ex);
                    this.pdf = null;
                }
            }

            //所在地PDFを表示
            this.ViewPDF((this.locationList.First(x => x.LOCATION_NO == Convert.ToInt32(this.LocationComboBox.SelectedValue))).MAP_PDF);

            //Delete Start 2022/08/15 杉浦 グリッド表示不具合対応
            ////Append Start 2021/11/09 杉浦 画面遷移時に全エリア表示に変更
            //this.LocationComboBox.SelectedIndex = 0;
            //this.AreaComboBox.SelectedIndex = 0;
            ////Append End 2021/11/09 杉浦 画面遷移時に全エリア表示に変更
            //Delete End 2022/08/15 杉浦 グリッド表示不具合対応
        }

        #region 区画リスト表示初期化
        /// <summary>
        /// 区画リスト表示初期化
        /// </summary>
        private void InitGrid()
        {
            //グリッド初期化
            this.gridUtil = new DataGridViewUtil<ParkingModel>(this.SectionDataGridView, false);

            //区画グリッド設定
            this.SetGrid();
        }
        #endregion

        #region 所在地コンボボックス作成
        /// <summary>
        /// 所在地コンボボックス作成
        /// </summary>
        private void CreateLocationComboBox()
        {
            //コンボボックスのイベントを使用不可
            this.LocationComboBox.SelectedIndexChanged -= new EventHandler(this.LocationComboBox_SelectedIndexChanged);

            //コンボボックスをクリア
            FormControlUtil.ClearComboBoxDataSource(this.LocationComboBox);

            //コンボボックスに表示する情報を取得
            var resLocationModel = HttpUtil.GetResponse<ParkingModel>(ControllerType.ParkingLocation);
            if (resLocationModel != null && resLocationModel.Status == Const.StatusSuccess)
            {
                FormControlUtil.SetComboBoxItem(this.LocationComboBox, resLocationModel.Results, false);

                //内部に保存
                this.locationList = resLocationModel.Results.ToList();

                //所在地
                if (this.locationList != null && 0 < this.locationList.Count)
                {
                    this.LocationComboBox.SelectedItem = this.locationList.FirstOrDefault(x => x.ESTABLISHMENT == SessionDto.Affiliation) ?? this.locationList[0];
                }
            }

            //コンボボックスのイベントを使用可
            this.LocationComboBox.SelectedIndexChanged += new EventHandler(this.LocationComboBox_SelectedIndexChanged);
        }
        #endregion

        #region エリアコンボボックス作成
        /// <summary>
        /// エリアコンボボックス作成
        /// </summary>
        private void CreateAreaComboBox()
        {
            //コンボボックスのイベントを使用不可
            this.AreaComboBox.SelectedIndexChanged -= new EventHandler(this.AreaComboBox_SelectedIndexChanged);

            //コンボボックスをクリア
            FormControlUtil.ClearComboBoxDataSource(this.AreaComboBox);

            //コンボボックスに表示する情報を取得
            var cond = new ParkingSearchModel
            {
                LOCATION_NO = (this.locationList.First(x => x.LOCATION_NO == Convert.ToInt32(this.LocationComboBox.SelectedValue))).LOCATION_NO,
            };

            var resAreaModel = HttpUtil.GetResponse<ParkingSearchModel, ParkingModel>(ControllerType.ParkingArea, cond);
            if (resAreaModel != null && resAreaModel.Status == Const.StatusSuccess)
            {
                //Update Start 2021/10/28 矢作
                //FormControlUtil.SetComboBoxItem(this.AreaComboBox, "AREA_NO", "NAME", resAreaModel.Results, true);

                var dataSource = (resAreaModel.Results ?? Enumerable.Empty<ParkingModel>()).ToList();

                //バインドの設定
                this.AreaComboBox.ValueMember = "AREA_NO";
                this.AreaComboBox.DisplayMember = "NAME";

                //空白
                //dataSource.Insert(0, new ParkingModel());

                ////全エリア追加
                dataSource.Insert(0, new ParkingModel() { AREA_NO = 0 , NAME = "全エリア"});

                //バインド
                this.AreaComboBox.DataSource = dataSource;

                //幅の設定
                FormControlUtil.SetDropDownWidth(this.AreaComboBox);

                //Update End 2021/10/28 矢作
            }


            //内部に保存
            this.areaList = resAreaModel.Results.ToList();

            //コンボボックスのイベントを使用可
            this.AreaComboBox.SelectedIndexChanged += new EventHandler(this.AreaComboBox_SelectedIndexChanged);
        }
        #endregion

        #region 所在地リスト変更
        /// <summary>
        /// 所在地リスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //エリアコンボボックスを作成
            this.CreateAreaComboBox();

            //所在地PDFを表示
            if (this.locationList != null && 0 < this.locationList.Count)
            {
                this.ViewPDF((this.locationList.First(x => x.LOCATION_NO == Convert.ToInt32(this.LocationComboBox.SelectedValue))).MAP_PDF);
            }

            //全てを選択
            this.AllRadioButton.Checked = true;

            //Append Start 2021/10/28 矢作
            this.selectedAreaNo = "";
            //Append End 2021/10/28 矢作

            //グリッド更新
            this.SetGrid();
        }
        #endregion

        #region エリアリスト変更
        /// <summary>
        /// エリアリスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AreaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //エリアPDF表示
            if (this.areaList != null && 0 < this.areaList.Count && this.AreaComboBox.SelectedValue != null)
            {
                if(!this.AreaComboBox.SelectedValue.Equals(0))
                {
                    this.ViewPDF((this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue))).MAP_PDF);
                }
                //Append Start 2021/11/09 杉浦 PDF表示対象追加
                else
                {
                    this.ViewPDF((this.locationList.First(x => x.LOCATION_NO == Convert.ToInt32(this.LocationComboBox.SelectedValue))).MAP_PDF);
                }
                //Append End 2021/11/09 杉浦 PDF表示対象追加
            }
            else
            {
                //所在地を選択
                this.LocationComboBox_SelectedIndexChanged(null, null);
            }

            // エリア駐車カウントラベルの初期化
            this.AreaCountLabel.Text = string.Empty;

            // エリア名（駐車場番号）がある場合
            //Append Start 2021/11/09 杉浦 全エリア表示の場合は除外
            //if (!string.IsNullOrWhiteSpace(this.AreaComboBox.Text))
            if (!string.IsNullOrWhiteSpace(this.AreaComboBox.Text) && !this.AreaComboBox.SelectedValue.Equals(0))
            //Append End 2021/11/09 杉浦 全エリア表示の場合は除外
            {
                var res = HttpUtil.GetResponse<ParkingCountSearchModel, ParkingCountModel>(ControllerType.ParkingCount, new ParkingCountSearchModel { 駐車場番号 = this.AreaComboBox.Text });
                if (res != null && res.Status == Const.StatusSuccess && res.Results.FirstOrDefault().COUNT > 0)
                {
                    // 使用中台数の表示
                    this.AreaCountLabel.Text = string.Format("{0} ({1:#,0}台)", STATUS_USE, Convert.ToString(res.Results.FirstOrDefault().COUNT));
                }
            }

            //全てを選択
            this.AllRadioButton.Checked = true;

            //グリッド更新
            this.SetGrid();
        }
        #endregion

        #region PDF表示
        /// <summary>
        /// PDF表示
        /// </summary>
        /// <param name="path">PDFファイルパス</param>
        private bool ViewPDF(string path)
        {
            var ret = true;

            if (this.pdf == null)
            {
                //PDF表示ができない場合、以下は行わない。
                return false;
            }

            //パラメータ設定
            var cond = new FileSearchModel()
            {
                FILE_PATH = path,
                SAVE_FILENAME = "Parking.pdf",
            };

            try
            {
                var res = HttpUtil.PostResponse<FileSearchModel>(ControllerType.File, cond);
                if (res != null && res.Status == Const.StatusSuccess && res.FileMap.First().Key.Length > 0)
                {
                    //PDFの書き込み（上書き）
                    var pdfPath = FileUtil.GetPathCombine(Path.GetTempPath(), cond.SAVE_FILENAME);
                    res.FileMap.First().Key.WriteStream(pdfPath);

                    //PDFの読み込み
                    if (this.pdf.LoadFile(pdfPath))
                    {
                        this.pdf.setShowToolbar(true);
                        this.pdf.setLayoutMode("SinglePage");
                        this.pdf.setPageMode("none");
                        this.pdf.setShowScrollbars(false);
                        this.pdf.setView("Fit");
                        this.pdf.TabIndex = 0;
                        this.pdf.TabStop = false;

                        //フォーカスを設定する（初期表示時に表示されないため）
                        this.ActiveControl = this.pdf;
                    }
                    else
                    {
                        logger.Error(String.Format("PDFの読み込みに失敗しました。[{0}][{1}]", cond.FILE_PATH, cond.SAVE_FILENAME));
                        ret = false;
                    }
                }
                else
                {
                    logger.Error(String.Format("PDFの読み込みに失敗しました。[{0}][{1}]", cond.FILE_PATH, cond.SAVE_FILENAME));
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                ret = false;
            }

            this.pdf.Visible = ret;
            return ret;
        }
        #endregion

        /// <summary>
        /// レジストリの設定
        /// </summary>
        /// <param name="name">キー</param>
        /// <param name="value">値</param>
        private bool SetRegistry(string name, int value)
        {
            bool ret = true;
            try
            {
                RegistryKey rKey = Registry.CurrentUser.CreateSubKey(rKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                rKey.SetValue(name, value);
                rKey.Close();
            }
            catch (Exception ex)
            {
                // レジストリ・キーが存在しない
                logger.Error(ex.Message, ex);
                ret = false;
            }

            return ret;
        }

        #region 全てラジオボタン選択
        /// <summary>
        /// 全てラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //区画グリッド設定
            if (this.AllRadioButton.Checked == true)
            {
                this.SetGrid();
            }
        }
        #endregion

        #region 空きラジオボタン選択
        /// <summary>
        /// 空きラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FreeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //区画グリッド設定
            if (this.FreeRadioButton.Checked == true)
            {
                this.SetGrid();
            }
        }
        #endregion

        #region 使用中ラジオボタン選択
        /// <summary>
        /// 使用中ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //区画グリッド設定
            if (this.UseRadioButton.Checked == true)
            {
                this.SetGrid();
            }
        }
        #endregion

        #region エリアラジオボタン選択
        /// <summary>
        /// 使用中ラジオボタン選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AreaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //区画グリッド設定
            if (this.AreaRadioButton.Checked == true)
            {
                this.SetGrid();
            }
        }
        #endregion

        #region 区画グリッド設定
        /// <summary>
        /// 区画グリッド設定
        /// </summary>
        private void SetGrid()
        {
            //Update Start 2021/11/09 杉浦 初期値変更
            //if (this.areaList == null || this.areaList.Count == 0 || this.AreaComboBox.SelectedIndex == 0)
            if (this.areaList == null || this.areaList.Count == 0)
            //Update End 2021/11/09 杉浦 初期値変更
            {
                //エリア情報がない場合、「エリアを選択」「駐車場NOを選択」ボタンを使用不可とする。
                this.AreaButton.Enabled = false;
                this.SectionButton.Enabled = false;
                this.ModeButton.Enabled = false;
                this.RadioButtonPanel.Enabled = false;
                this.gridUtil.DataSource = null;

                // 区画ラジオボタンテキスト設定（初期化）
                this.SetParkingRadioButtonText();

                return;
            }

            //グリッド情報取得
            this.GetGridInfo();

            this.AreaButton.Enabled = true;

            this.RadioButtonPanel.Enabled = true;
            this.SectionButton.Enabled = false;
            if (this.gridUtil.DataSource == null || this.gridUtil.DataSource.Any() == false)
            {
                this.ModeButton.Enabled = false;
            }
            else
            {
                this.ModeButton.Enabled = true;
            }
        }
        #endregion

        #region グリッド情報取得
        /// <summary>
        /// グリッド情報取得
        /// </summary>
        private void GetGridInfo(int? rowindex = null)
        {
            //区画グリッド値変更イベントを使用不可
            this.SectionDataGridView.CellValueChanged -= new DataGridViewCellEventHandler(this.SectionDataGridView_CellValueChanged);

            //グリッドクリア
            this.gridUtil.DataSource = null;

            if (this.AreaComboBox.SelectedValue != null)
            {
                //Update Start 2021/10/07 矢作

                //パラメータ設定
                //var cond = new ParkingSearchModel()
                //{
                //    LOCATION_NO = (this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue))).LOCATION_NO,
                //    AREA_NO = (this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue))).AREA_NO,
                //};

                //パラメータ設定
                var cond = new ParkingSearchModel();

                //全エリアが対象の場合は所在地NOもパラメータに追加
                //対象でない場合はエリアNOのみパラメータに追加
                if (this.AreaComboBox.SelectedValue.Equals(0))
                {
                    cond.LOCATION_NO = (this.locationList.First(x => x.LOCATION_NO == Convert.ToInt32(this.LocationComboBox.SelectedValue))).LOCATION_NO;
                }
                else
                {
                    cond.LOCATION_NO = (this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue))).LOCATION_NO;
                    cond.AREA_NO = (this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue))).AREA_NO;
                }

                //Update End 2021/10/07 矢作

                if (this.FreeRadioButton.Checked == true)
                {
                    //空き
                    cond.STATUS = StatusItemTable[STATUS_FREE];
                }
                else if (this.UseRadioButton.Checked == true)
                {
                    //使用中
                    cond.STATUS = StatusItemTable[STATUS_USE];
                }
                //Append Start 2021/11/09 杉浦 エリアラジオボタン追加
                else if (this.AreaRadioButton.Checked == true)
                {
                    cond.STATUS = StatusItemTable[AREA];
                }
                //Append End 2021/11/09 杉浦 エリアラジオボタン追加

                //表示データ取得
                var resSectionModel = HttpUtil.GetResponse<ParkingSearchModel, ParkingModel>(ControllerType.ParkingSection, cond);
                if (resSectionModel != null && resSectionModel.Status == Const.StatusSuccess)
                {
                    this.gridUtil.DataSource = resSectionModel.Results.ToArray();
                }

                foreach(DataGridViewRow row in this.gridUtil.Rows)
                {
                    //Update Start 2021/11/11 杉浦 区画ラジオボタン内容変更
                    //if (row.Cells["状態"].Value.ToString() != STATUS_USE)
                    if (row.Cells["状態"].Value != null && row.Cells["状態"].Value.ToString() != STATUS_USE)
                    //Update Start 2021/11/11 杉浦 区画ラジオボタン内容変更
                    {
                        row.Cells["状態"].ReadOnly = true;
                    }
                }
            }

            if (this.AllRadioButton.Checked)
            {
                // 区画ラジオボタンテキスト設定（件数表示）
                this.SetParkingRadioButtonText();
            }

            // 行が指定されている場合
            if (rowindex != null)
            {
                this.SectionDataGridView.FirstDisplayedScrollingRowIndex = (int)rowindex;
                this.SectionDataGridView.CurrentCell = this.SectionDataGridView["状態", (int)rowindex];
            }

            //区画グリッド値変更イベントを使用可
            this.SectionDataGridView.CellValueChanged += new DataGridViewCellEventHandler(this.SectionDataGridView_CellValueChanged);
        }
        #endregion

        #region 区画グリッドクリック
        /// <summary>
        /// 区画グリッドクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            //Append Start 2021/10/07 矢作
            this.selectedRow = this.SectionDataGridView.Rows[e.RowIndex];
            //Append End 2021/10/07 矢作

            var row = this.SectionDataGridView.Rows[e.RowIndex];
            var name = this.SectionDataGridView.Columns[e.ColumnIndex].Name;

            // 選択モード
            if (this.ModeButton.Text == MODE_EDIT)
            {
                //Append Start 2021/11/09 杉浦 クリックアクション追加
                if (row.Cells["状態"].Value != DBNull.Value && row.Cells["状態"].Value != null)
                {
                    //Append End 2021/11/09 杉浦 クリックアクション追加
                    if (row.Cells["状態"].Value.ToString() == STATUS_USE)
                    {
                        //使用中の行を選択した場合は「駐車場NOを選択」ボタンを使用不可
                        this.SectionButton.Enabled = false;
                    }
                    else
                    {
                        //使用中の行を選択した場合は「駐車場NOを選択」ボタンを使用可
                        this.SectionButton.Enabled = true;
                    }
                    //Append Start 2021/11/09 杉浦 クリックアクション追加
                }else
                {
                    this.SectionButton.Enabled = false;
                }
                //Append End 2021/11/09 杉浦 クリックアクション追加
            }

            //Append Start 2021/10/28 矢作
            var currentNo = row.Cells["AREA_NO"].Value;

            //所在地PDFを表示
            if (this.AreaComboBox.SelectedValue.Equals(0) && this.selectedAreaNo != row.Cells["AREA_NO"].Value.ToString())
            {
                this.ViewPDF((this.areaList.First(x => x.AREA_NO == Convert.ToInt32(row.Cells["AREA_NO"].Value))).MAP_PDF);
                this.selectedAreaNo = row.Cells["AREA_NO"].Value.ToString();
            }
            //Append End 2021/10/28 矢作

            //使用中台数表示(全エリア表示時のみ)
            if (this.AreaComboBox.SelectedValue.Equals(0))
            {
                if(row.Cells["SECTION_NO"].Value == null)
                {
                    var res = HttpUtil.GetResponse<ParkingCountSearchModel, ParkingCountModel>(ControllerType.ParkingCount, new ParkingCountSearchModel { 駐車場番号 = row.Cells["エリア"].Value.ToString() });
                    if (res != null && res.Status == Const.StatusSuccess && res.Results.FirstOrDefault().COUNT > 0)
                    {
                        // 使用中台数の表示
                        this.AreaCountLabel.Text = string.Format("{0} ({1:#,0}台)", STATUS_USE, Convert.ToString(res.Results.FirstOrDefault().COUNT));
                    }
                }else
                {
                    this.AreaCountLabel.Text = "";
                }

            }
        }
        #endregion

        #region 区画グリッドダブルクリック
        /// <summary>
        /// 区画グリッドダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 無効な行か列の場合は終了
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var row = this.SectionDataGridView.Rows[e.RowIndex];
            var name = this.SectionDataGridView.Columns[e.ColumnIndex].Name;

            // 駐車場番号
            //Update Start 2021/11/09 杉浦 クリックアクション追加
            //if (name == "駐車場NO")
            if (name == "駐車場NO" || name == "エリア")
            //Update End 2021/11/09 杉浦 クリックアクション追加
            {
                var dataID = Convert.ToInt32(row.Cells["データID"].Value);

                if (dataID > 0)
                {
                    //Update Start 2023/07/19 空き⇒使用中の車両のステータス変更
                    //// 試験車情報画面の表示
                    //new FormUtil(new ControlSheetIssueForm { TestCar = new TestCarCommonModel { データID = dataID }, UserAuthority = this.UserAuthority, IsViewMode = true }).SingleFormShow(this, false);

                    var dummyCheck = row.Cells["管理票NO"].Value;
                    
                    if(!dummyCheck.Equals("Dummy"))
                    {
                        // 試験車情報画面の表示
                        new FormUtil(new ControlSheetIssueForm { TestCar = new TestCarCommonModel { データID = dataID }, UserAuthority = this.UserAuthority, IsViewMode = true }).SingleFormShow(this, false);
                    }else
                    {
                        Messenger.Info("本来、「空き」となっている駐車場です。\n使用する場合は､状態を「空き」に変更して下さい。");
                    }
                    //Update Start 2023/07/19 空き⇒使用中の車両のステータス変更
                }
            }
        }
        #endregion

        #region モードボタンクリック
        /// <summary>
        /// モードボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModeButton_Click(object sender, EventArgs e)
        {
            //モード変更
            this.ChangeMode();
        }

        /// <summary>
        /// モード変更
        /// </summary>
        private void ChangeMode()
        {
            if (this.ModeButton.Text == MODE_EDIT)
            {
                //選択モード→編集モード
                this.ModeButton.Text = MODE_SELECT;
                this.UpdateButton.Enabled = true;
                this.RadioButtonPanel.Enabled = false;
                this.AreaButton.Enabled = false;
                this.SectionButton.Enabled = false;
                this.LocationComboBox.Enabled = false;
                this.AreaComboBox.Enabled = false;

                //編集可能なカラムヘッダの背景色を変更
                this.SectionDataGridView.Columns["状態"].ReadOnly = false;
                this.SectionDataGridView.Columns["状態"].HeaderCell.Style.BackColor = Color.LightBlue;

                //Delete Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                //foreach (DataGridViewRow row in this.gridUtil.Rows)
                //{
                //    //Update Start 2021/11/11 杉浦 区画ラジオボタン内容変更
                //    //if (row.Cells["状態"].Value.ToString() != STATUS_USE)
                //    if (row.Cells["状態"].Value != null && row.Cells["状態"].Value.ToString() != STATUS_USE)
                //    //Update Start 2021/11/11 杉浦 区画ラジオボタン内容変更
                //    {
                //        row.Cells["状態"].ReadOnly = true;
                //    }
                //}
                //Delete End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
            }
            else
            {
                //編集モード→選択モード

                this.CheckChangeItem();

                this.ModeButton.Text = MODE_EDIT;
                this.UpdateButton.Enabled = false;
                this.RadioButtonPanel.Enabled = true;
                this.AreaButton.Enabled = true;
                this.SectionButton.Enabled = false;
                this.LocationComboBox.Enabled = true;
                this.AreaComboBox.Enabled = true;

                //編集可能なカラムヘッダの背景色を変更
                this.SectionDataGridView.Columns["状態"].ReadOnly = true;
                this.SectionDataGridView.Columns["状態"].HeaderCell.Style.BackColor = SystemColors.Control;

                //グリッド情報取得
                this.GetGridInfo();
            }
        }
        #endregion

        #region エリアを選択ボタンクリック
        /// <summary>
        /// エリアを選択ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AreaButton_Click(object sender, EventArgs e)
        {
            //Update Start 2021/10/12 矢作

            ////戻り値設定
            //this.Data = this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue));

            ////画面を閉じる
            //base.FormOkClose();

            if (this.FromTestCar == false)
            {
                //Append Start 2021/12/13 杉浦 全エリア選択時の処理を追加
                if ((this.AreaComboBox.SelectedValue.Equals(0) && (this.selectedRow != null)) || !this.AreaComboBox.SelectedValue.Equals(0))
                {
                    //Append End 2021/12/13 杉浦 全エリア選択時の処理を追加
                    //戻り値設定
                    //Append Start 2022/08/15 杉浦 全エリア選択時のエリア選択処理を追加
                    if (!this.AreaComboBox.SelectedValue.Equals(0))
                    {
                        //Append End 2022/08/15 杉浦 全エリア選択時のエリア選択処理を追加
                        this.Data = this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.AreaComboBox.SelectedValue));
                        //Append Start 2022/08/15 杉浦 全エリア選択時のエリア選択処理を追加
                    }else
                    {
                        if(this.SectionDataGridView.SelectedCells.Count == 0)
                        {
                            Messenger.Warn(Resources.KKM00009);
                        }
                        var rowIndex = this.SectionDataGridView.SelectedCells[0].RowIndex;
                        this.Data = this.areaList.First(x => x.AREA_NO == Convert.ToInt32(this.SectionDataGridView["AREA_NO", rowIndex].Value));
                    }
                    //Append End 2022/08/15 杉浦 全エリア選択時のエリア選択処理を追加

                    //画面を閉じる
                    base.FormOkClose();
                    //Append Start 2021/12/13 杉浦 全エリア選択時の処理を追加
                }
                else
                {
                    Messenger.Warn(Resources.KKM00009); // 対象を選択してください
                }
                //Append End 2021/12/13 杉浦 全エリア選択時の処理を追加
            }
            else
            {
                if((this.AreaComboBox.SelectedValue.Equals(0) &&  (this.selectedRow != null)) || !this.AreaComboBox.SelectedValue.Equals(0))
                {
                    using (var form = new UIDevPlan.TestCarSchedule.TestCarListForm() { fromParking = true })
                    {
                        // OKの場合は項目名を設定
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            //試験車情報を取得
                            TestCarCommonModel TestCar = new TestCarCommonModel();
                            // Get実行
                            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.ControlSheetTestCar, new TestCarCommonSearchModel() { 管理票NO = form.SelectedTestCar.管理票NO });
                            TestCar = res.Results.ToList().FirstOrDefault();
                            
                            //PARKING_USEのデータ削除
                            DeleteParkingInfo(TestCar.データID);

                            //試験車基本情報を更新
                            if (this.AreaComboBox.SelectedValue.Equals(0))
                            {
                                TestCar.駐車場番号 = this.selectedRow.Cells["エリア"].Value.ToString();
                            }
                            else
                            {
                                TestCar.駐車場番号 = this.AreaComboBox.Text;
                            }

                            var res2 = HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCar, TestCar);

                            Messenger.Info(Resources.KKM00002); // 登録完了

                            //グリッド情報再取得
                            this.GetGridInfo();
                        }
                    }
                }else
                {
                    Messenger.Warn(Resources.KKM00009); // 対象を選択してください
                }

            }

            //Update End 2021/10/12 矢作
        }
        #endregion

        #region 駐車場NOを選択ボタンクリック
        /// <summary>
        /// 駐車場NOを選択ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionButton_Click(object sender, EventArgs e)
        {
            //Update Start 2021/10/12 矢作

            ////区画状態更新
            //if(this.ParkingSelect() == true)
            //{
            //    //画面を閉じる
            //    base.FormOkClose();
            //}

            if (this.FromTestCar == false)
            {
                //Append Start 2021/12/13 杉浦 全エリア選択時の処理を追加
                if ((this.AreaComboBox.SelectedValue.Equals(0) && (this.selectedRow != null)) || !this.AreaComboBox.SelectedValue.Equals(0))
                {
                    //Append End 2021/12/13 杉浦 全エリア選択時の処理を追加
                    //区画状態更新
                    if (this.ParkingSelect() == true)
                    {
                        //画面を閉じる
                        base.FormOkClose();
                    }
                    //Append Start 2021/12/13 杉浦 全エリア選択時の処理を追加
                }
                else
                {
                    Messenger.Warn(Resources.KKM00009); // 対象を選択してください
                }
                //Append End 2021/12/13 杉浦 全エリア選択時の処理を追加
            }
            else
            {
                if ((this.AreaComboBox.SelectedValue.Equals(0) && (this.selectedRow != null)) || !this.AreaComboBox.SelectedValue.Equals(0))
                {
                    using (var form = new UIDevPlan.TestCarSchedule.TestCarListForm() { fromParking = true })
                    {
                        // OKの場合は項目名を設定
                        if (form.ShowDialog(this) == DialogResult.OK)
                        {
                            //試験車情報を取得
                            TestCarCommonModel TestCar = new TestCarCommonModel();
                            // Get実行
                            var res = HttpUtil.GetResponse<TestCarCommonSearchModel, TestCarCommonModel>(ControllerType.ControlSheetTestCar, new TestCarCommonSearchModel() { 管理票NO = form.SelectedTestCar.管理票NO });
                            TestCar = res.Results.ToList().FirstOrDefault();

                            //試験車基本情報を更新
                            TestCar.駐車場番号 = this.selectedRow.Cells["駐車場NO"].Value.ToString();
                            var res2 = HttpUtil.PutResponse<TestCarCommonModel>(ControllerType.ControlSheetTestCar, TestCar);

                            //駐車場NOの状態を使用中に変更
                            var row = this.SectionDataGridView.Rows;
                            var parkingList = new List<ParkingModel>();
                            parkingList.Add(new ParkingModel
                            {
                                LOCATION_NO = (int)this.selectedRow.Cells["LOCATION_NO"].Value,
                                AREA_NO = (int)this.selectedRow.Cells["AREA_NO"].Value,
                                SECTION_NO = (int)this.selectedRow.Cells["SECTION_NO"].Value,
                                STATUS = 1,
                                INPUT_PERSONEL_ID = SessionDto.UserId,
                            });

                            var res3 = HttpUtil.PutResponse(ControllerType.ParkingSection, parkingList);

                            //PARKING_USEのデータ削除
                            DeleteParkingInfo(TestCar.データID);

                            //PARKING_USEにデータ追加
                            var addUse = new ParkingUseModel
                            {
                                データID = TestCar.データID,
                                LOCATION_NO = (int)this.selectedRow.Cells["LOCATION_NO"].Value,
                                AREA_NO = (int)this.selectedRow.Cells["AREA_NO"].Value,
                                SECTION_NO = (int)this.selectedRow.Cells["SECTION_NO"].Value,
                                INPUT_PERSONEL_ID = SessionDto.UserId,
                            };
                            AddParkingInfo(addUse);


                            Messenger.Info(Resources.KKM00002); // 登録完了

                            //グリッド情報再取得
                            this.GetGridInfo();
                        }
                    }
                }else
                {
                    Messenger.Warn(Resources.KKM00009); // 対象を選択してください
                }
            }

            //Update End 2021/10/12 矢作

        }
        #endregion

        #region 区画状態更新
        /// <summary>
        /// 区画状態更新
        /// </summary>
        /// <returns></returns>
        private bool ParkingSelect()
        {
            //グリッドの選択がない場合はメッセージを表示して終了
            if (this.SectionDataGridView.SelectedCells.Count == 0)
            {
                Messenger.Info(Resources.KKM00009);
                return false;
            }

            //更新パラメータ
            var rowIndex = this.SectionDataGridView.SelectedCells[0].RowIndex;
            var list = new List<ParkingModel>();
            var updateData = this.gridUtil.DataSource.First(
                x => x.LOCATION_NO == Convert.ToInt32(this.SectionDataGridView["LOCATION_NO", rowIndex].Value) &&
                x.AREA_NO == Convert.ToInt32(this.SectionDataGridView["AREA_NO", rowIndex].Value) &&
                x.SECTION_NO == Convert.ToInt32(this.SectionDataGridView["SECTION_NO", rowIndex].Value));

            updateData.STATUS = StatusItemTable[STATUS_USE];
            updateData.INPUT_PERSONEL_ID = SessionDto.UserId;
            list.Add(updateData);

            //更新
            var res = HttpUtil.PutResponse<ParkingModel>(ControllerType.ParkingSection, list);

            //戻り値設定
            if (res != null && res.Status == Const.StatusSuccess)
            {
                this.Data = updateData;
            }

            //レスポンスが取得できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);
        }
        #endregion

        #region 区画ラジオボタンテキスト設定
        /// <summary>
        /// 区画ラジオボタンテキスト設定
        /// </summary>
        /// <returns></returns>
        private void SetParkingRadioButtonText()
        {
            var all = this.gridUtil.DataSource?.Count();
            var format = "{0} ({1:#,0}台)";

            //Append Start 2021/11/11 杉浦 区画ラジオボタン内容変更
            var area = this.gridUtil.DataSource.Any(x => x.SECTION_NO == null);
            //Append End 2021/11/11 杉浦 区画ラジオボタン内容変更

            //Update Start 2021/11/11 杉浦 区画ラジオボタン内容変更
            //if (all > 0)
            if ((all > 0 && !area ) && !this.AreaComboBox.SelectedValue.Equals(0))
            {
                //Update End 2021/11/11 杉浦 区画ラジオボタン内容変更
                this.AllRadioButton.Text = string.Format(format, STATUS_ALL, all);
                this.FreeRadioButton.Text = string.Format(format, STATUS_FREE, this.gridUtil.DataSource.Count(x => x.STATUS != 1));
                this.UseRadioButton.Text = string.Format(format, STATUS_USE, this.gridUtil.DataSource.Count(x => x.STATUS == 1));
            }
            else
            {
                this.AllRadioButton.Text = STATUS_ALL;
                this.FreeRadioButton.Text = STATUS_FREE;
                this.UseRadioButton.Text = STATUS_USE;
            }
        }
        #endregion

        #region 状態更新ボタンクリック
        /// <summary>
        /// 状態更新ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            //変更された項目をリスト化
            var list = GetChangeList();

            if (list.Count > 0)
            {
                // 更新確認
                if (Messenger.Confirm(Resources.TCM00007) == DialogResult.Yes)
                {
                    //更新
                    var res = HttpUtil.PutResponse<ParkingModel>(ControllerType.ParkingSection, list);

                    //グリッド情報取得
                    this.GetGridInfo();

                    //正常終了メッセージ(登録しました)
                    Messenger.Info(Resources.TCM00008);

                    //モード変更
                    this.ChangeMode();
                }
            }
            else
            {
                //変更なしメッセージ
                Messenger.Warn(Resources.TCM03024);
            }
        }
        #endregion

        #region 区画グリッド値変更
        /// <summary>
        /// 区画グリッド値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SectionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            if (this.SectionDataGridView.Columns[e.ColumnIndex].Name == "状態")
            {
                //変更された値を保存
                if (this.SectionDataGridView[e.ColumnIndex, e.RowIndex].Value.ToString() == STATUS_USE)
                {
                    // 使用中
                    this.SectionDataGridView["CHANGE_STATUS", e.RowIndex].Value = StatusItemTable[STATUS_USE];
                    //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                    this.SectionDataGridView["管理票NO", e.RowIndex].Value = "Dummy";
                    //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                }
                else
                {
                    var dataID = Convert.ToInt32(this.SectionDataGridView["データID", e.RowIndex].Value);
                    var controlNo = Convert.ToString(this.SectionDataGridView["管理票NO", e.RowIndex].Value);

                    //Update Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                    //if (dataID > 0)
                    if (dataID > 0 && !controlNo.Equals("Dummy"))
                    //Update End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                    {
                        if (Messenger.Confirm(string.Format(Resources.KKM01022, controlNo)) == DialogResult.Yes)
                        {
                            // 試験車情報画面の表示
                            new FormUtil(new ControlSheetIssueForm
                            {
                                TestCar = new TestCarCommonModel { データID = dataID },
                                UserAuthority = this.UserAuthority,
                                IsMoveMode = true,
                                Reload = () => this.GetGridInfo(e.RowIndex)
                            })
                            .SingleFormShow(this, false);
                        }

                        // 使用中に戻す
                        this.SectionDataGridView[e.ColumnIndex, e.RowIndex].Value = STATUS_USE;

                        return;
                    }

                    // 空き
                    this.SectionDataGridView["CHANGE_STATUS", e.RowIndex].Value = StatusItemTable[STATUS_FREE];
                }
            }
        }
        #endregion

        #region 変更項目リスト化
        /// <summary>
        /// 変更項目リスト化
        /// </summary>
        /// <returns></returns>
        private List<ParkingModel> GetChangeList()
        {
            var list = new List<ParkingModel>();
            for (var i = 0; i < this.SectionDataGridView.Rows.Count; i++)
            {
                //変更有のものをリストに追加
                if (this.SectionDataGridView["CHANGE_STATUS", i].Value != null &&
                    this.SectionDataGridView["STATUS", i].Value != this.SectionDataGridView["CHANGE_STATUS", i].Value)
                {
                    ParkingModel updateData = this.gridUtil.DataSource.First(
                        x => x.LOCATION_NO == Convert.ToInt32(this.SectionDataGridView["LOCATION_NO", i].Value) &&
                        x.AREA_NO == Convert.ToInt32(this.SectionDataGridView["AREA_NO", i].Value) &&
                        x.SECTION_NO == Convert.ToInt32(this.SectionDataGridView["SECTION_NO", i].Value));

                    updateData.STATUS = Convert.ToInt16(this.SectionDataGridView["CHANGE_STATUS", i].Value);
                    updateData.INPUT_PERSONEL_ID = SessionDto.UserId;

                    //Append Start 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更
                    if (Convert.ToString(this.SectionDataGridView["CHANGE_STATUS", i].Value) == "Dummy")
                    {
                        updateData.管理票NO = "Dummy";
                    }
                    //Append End 2023/04/14 杉浦 空き⇒使用中の車両のステータス変更

                    list.Add(updateData);
                }
            }

            return list;
        }
        #endregion

        #region 画面終了時処理
        /// <summary>
        /// 画面終了時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParkingSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //変更項目の保存確認
            this.CheckChangeItem();
        }
        #endregion

        #region 変更項目の保存確認
        /// <summary>
        /// 変更項目の保存確認
        /// </summary>
        /// <returns></returns>
        private bool CheckChangeItem()
        {
            //変更された項目をリスト化
            var list = GetChangeList();

            if (0 == list.Count)
            {
                return false;
            }

            //登録確認
            var result = Messenger.Confirm(Resources.KKM00006);
            if (result != DialogResult.Yes)
            {
                return false;
            }

            //更新
            var res = HttpUtil.PutResponse<ParkingModel>(ControllerType.ParkingSection, list);

            //レスポンスが取得できたかどうか
            return (res != null && res.Status == Const.StatusSuccess);
        }
        #endregion

        private void SectionDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var sdgv = (DataGridView)sender;

            var row = this.SectionDataGridView.Rows[e.RowIndex];
            var name = this.SectionDataGridView.Columns[e.ColumnIndex].Name;

            if ((name == "駐車場NO" || name == "状態"))
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.CellStyle.BackColor = Color.LightGray;
                    //Delete Start 2021/12/06 杉浦 状態変更の不具合
                    //this.SectionDataGridView.Columns["状態"].ReadOnly = true;
                    //Delete End 2021/12/06 杉浦 状態変更の不具合
                }
            }
        }
        //Append End 2021/10/07 矢作

        #region 駐車場選択情報削除
        /// <summary>
        /// 駐車場選択情報削除
        /// </summary>
        /// <returns></returns>
        private bool DeleteParkingInfo(int dataId)
        {
            //駐車場選択情報を削除（PARKING_SECTIONのSTATUSを0,PARKING_USEから情報を削除）
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            var cond = new ParkingUseModel
            {
                データID = dataId,
                INPUT_PERSONEL_ID = SessionDto.UserId,
            };
            list.Add(cond);

            var res = HttpUtil.DeleteResponse<ParkingUseModel>(ControllerType.ParkingUse, list);

            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion

        #region 駐車場選択情報登録
        /// <summary>
        /// 駐車場選択情報登録
        /// </summary>
        /// <returns></returns>
        private bool AddParkingInfo(ParkingUseModel model)
        {
            //駐車場選択情報を登録
            List<ParkingUseModel> list = new List<ParkingUseModel>();
            list.Add(model);

            var res = HttpUtil.PostResponse<ParkingUseModel>(ControllerType.ParkingUse, list);
            return res.Status.Equals(Const.StatusSuccess);
        }
        #endregion


    }
}
