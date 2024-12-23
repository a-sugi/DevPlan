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
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;
using GrapeCity.Win.MultiRow;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 試験車一覧(試験車管理)
    /// </summary>
    public partial class DesignCheckSystemTestCarListForm : BaseSubDialogForm
    {
        #region メンバ変数
        /// <summary>
        /// カスタムテンプレート
        /// </summary>
        private CustomTemplate customTemplate = new CustomTemplate();
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "試験車一覧(試験車管理)"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>横幅(最小)</summary>
        public override int FormMinWidth { get { return this.MinimumSize.Width; } }

        /// <summary>縦幅(最小)</summary>
        public override int FormMinHeight { get { return this.MinimumSize.Height; } }

        /// <summary>試験車リスト</summary>
        private List<DesignCheckTestCarGetOutModel> TestCarList { get; set; }

        /// <summary>選択した試験車</summary>
        public List<DesignCheckTestCarGetOutModel> SelectedTestCarList { get; private set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckSystemTestCarListForm()
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
        private void DesignCheckTestCarListForm_Load(object sender, EventArgs e)
        {
            //画面初期化
            this.InitForm();

            SearchButton_Click(null, null);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void InitForm()
        {
            // テンプレート設定
            this.customTemplate.RowCountLabel = RowCountLabel;
            this.customTemplate.MultiRow = this.ListMultiRow;
            this.ListMultiRow.Template = this.customTemplate.SetContextMenuTemplate(new DesignCheckTestCarListMultiRowTemplate());

            // 選択列のフィルターアイテム設定
            var headerCell = this.ListMultiRow.Template.ColumnHeaders[0].Cells["columnHeaderCell1"] as ColumnHeaderCell;
            headerCell.DropDownContextMenuStrip.Items.RemoveAt(headerCell.DropDownContextMenuStrip.Items.Count - 1);
            headerCell.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });
        }
        #endregion

        #region 画面初期表示時
        /// <summary>
        /// 画面初期表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckTestCarListForm_Shown(object sender, EventArgs e)
        {
            //一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;
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
            //試験車一覧設定
            FormControlUtil.FormWait(this, () => this.SetTestCarList());
        }

        /// <summary>
        /// 試験車一覧設定
        /// </summary>
        private void SetTestCarList()
        {
            //描画停止
            this.ListMultiRow.SuspendLayout();

            //試験車が取得できたかどうか
            var list = this.TestCarList = this.GetTestCarList();

            //試験車一覧に設定
            this.customTemplate.SetDataSource(list);

            //試験車一覧のデータが取得できたかどうか
            this.ListDataLabel.Text = list.Any() == true ? "" : Resources.KKM00005;

            //一覧を未選択状態に設定
            this.ListMultiRow.CurrentCell = null;

            //描画再開
            this.ListMultiRow.ResumeLayout();
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
            //試験車が設定できたかどうか
            if (this.SetSelectedTestCarList() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return;

            }

            //フォームクローズ
            base.FormOkClose();

        }

        /// <summary>
        /// 選択試験車設定
        /// </summary>
        private bool SetSelectedTestCarList()
        {
            var list = new List<DesignCheckTestCarGetOutModel>();

            Func<object, string> cnvStr = obj =>
            {
                return obj == null ? null : Convert.ToString(obj);
            };

            FormControlUtil.FormWait(this, () =>
            {
                foreach (var row in this.ListMultiRow.Rows)
                {
                    //選択しているかどうか
                    if (Convert.ToBoolean(row.Cells["SelectedColumn"].Value) == true)
                    {
                        list.Add(this.TestCarList.First(x =>
                                cnvStr(x.管理票NO) == cnvStr(row.Cells["ManagementNoColumn"].Value) &&
                                cnvStr(x.開発符号) == cnvStr(row.Cells["GeneralCodeColumn"].Value) &&
                                cnvStr(x.試作時期) == cnvStr(row.Cells["PrototypeTimingColumn"].Value) &&
                                cnvStr(x.号車) == cnvStr(row.Cells["VehicleColumn"].Value)
                            ));
                    }
                }

                //選択した試験車セット
                this.SelectedTestCarList = list;
            });

            //試験車があるかどうか
            return this.SelectedTestCarList.Any();
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 試験車一覧取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckTestCarGetOutModel> GetTestCarList()
        {
            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckTestCarGetInModel, DesignCheckTestCarGetOutModel>(ControllerType.DesignCheckSystemTestCar, new DesignCheckTestCarGetInModel());

            //レスポンスが取得できたかどうか
            var list = new List<DesignCheckTestCarGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }
        #endregion

        #region 全選択チェックボックス描画処理
        /// <summary>
        /// 全選択チェックボックス描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellPainting(object sender, GrapeCity.Win.MultiRow.CellPaintingEventArgs e)
        {
            // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
            {
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    // チェックボックスの描画領域を確保
                    using (Graphics graphics = Graphics.FromImage(bmp))
                    {
                        graphics.Clear(Color.Transparent);
                    }

                    // 描画領域の中央に配置
                    Point point = new Point((bmp.Width - AllSelectCheckBox.Width) / 2, (bmp.Height - AllSelectCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllSelectCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

                    // DataGridViewの現在描画中のセルの中央に描画
                    int x = (e.CellBounds.Width - bmp.Width) / 2;
                    int y = (e.CellBounds.Height - bmp.Height) / 2;

                    point = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                    e.Paint(e.ClipBounds);
                    e.Graphics.DrawImage(bmp, point);
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            //チェックボックスの表示を更新する
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
            {
                AllSelectCheckBox.Checked = !AllSelectCheckBox.Checked;
            }

            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            // チェックボックスをOn/Off
            var checkCell = ((CheckBoxCell)((GcMultiRow)sender).Rows[e.RowIndex].Cells["SelectedColumn"]);
            checkCell.Value = !Convert.ToBoolean(checkCell.Value);
        }
        #endregion

        #region 全選択チェックボックス本体変更時
        /// <summary>
        /// 全選択チェックボックス本体変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
             {
                 var flg = this.AllSelectCheckBox.Checked;

                 var dg = ListMultiRow.CurrentCell;
                 ListMultiRow.CurrentCell = null;

                 //全ての行の選択を設定
                 foreach (var row in this.ListMultiRow.Rows)
                 {
                     //Append Start 2020/01/27 杉浦
                     // 非表示行は未処理
                     if (!row.Visible) continue;
                     //Append End 2020/01/27 杉浦
                     row.Cells["SelectedColumn"].Value = flg;
                 }

                 ListMultiRow.CurrentCell = dg;
             });            
        }
        #endregion
    }
}
