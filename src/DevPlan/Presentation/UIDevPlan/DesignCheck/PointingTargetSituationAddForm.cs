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


namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    //Append Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
    public partial class PointingTargetSituationAddForm : BaseSubForm
    {
        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指摘対象車状況選択"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>開催日ID</summary>
        public int kaisaibiID { get; set; }

        /// <summary>指摘ID</summary>
        public int? pointID { get; set; }

        /// <summary>編集されたかどうか？</summary>
        private bool IsEdit = false;

        /// <summary>状況記号</summary>
        private List<DesignCheckProgressSymbolGetOutModel> _ProgressSymbolList;

        /// <summary>選択した対象車リスト</summary>
        public List<DesignCheckProgressPostInModel> CarList { get; set; }

        /// <summary>設計チェック対象車</summary>
        public List<DesignCheckCarGetOutModel> DesignCheckCarList { get; set; } = new List<DesignCheckCarGetOutModel>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PointingTargetSituationAddForm()
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
            //画面初期化
            FormControlUtil.FormWait(this, () =>
            {
                //グリッドビューの初期化
                this.InitDataGridView();
            });

        }

        /// <summary>
        /// グリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            //列の自動生成不可
            this.ListDataGridView.AutoGenerateColumns = false;

            //設計チェック対象車の取得
            this.DesignCheckCarList = this.GetDesignCheckCarList();

            // 状況記号の取得
            _ProgressSymbolList = (HttpUtil.GetResponse<DesignCheckProgressSymbolGetInModel, DesignCheckProgressSymbolGetOutModel>
                (ControllerType.DesignCheckProgressSymbol, new DesignCheckProgressSymbolGetInModel())?.Results).ToList();
            _ProgressSymbolList.FindAll((x) => x.ID != 0).ForEach((x) => x.説明 = string.Format(" {0} ： {1}", x.記号, x.説明));

            //データソース(開発符号 試作時期 号車)
            this.ListDataGridView.DataSource = this.DesignCheckCarList;
            //データソース(状況)
            this.Column2.DataSource = this._ProgressSymbolList;
            //Append Start 2024/09/20 早見 プロパティ編集時にグリットビューを再設定をする
            DataGridViewComboBoxColumn comboBoxColumn = (DataGridViewComboBoxColumn)this.ListDataGridView.Columns["Column2"];
            comboBoxColumn.DisplayMember = "説明";  // 表示用フィールド
            comboBoxColumn.ValueMember = "ID";     // 値フィールド
                                                   // 行テンプレートの高さを設定
            this.ListDataGridView.RowTemplate.Height = 25;
            //Append Start 2024/09/20 早見 プロパティ編集時にグリットビューを再設定をする
        }
        #endregion

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointingTargetSituationAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                //編集を終了する
                this.ListDataGridView.EndEdit();

                if (this.CheckEdit())
                {
                    //設計チェック指摘の登録
                    this.EntryDesignCheckPoint();

                    //フォームクローズ
                    base.FormOkClose();
                }
            });
        }
        private bool CheckEdit()
        {
            if (IsEdit == true)
            {
                return Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes;
            }

            return false;
        }
        #endregion

        #region 値変更後
        /// <summary>
        /// 値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_CellValueChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ListDataGridView.Rows)
            {
                var situation = Convert.ToString(row.Cells[Column2.Name].Value);

                // 状況を選択したかどうか
                if (!string.IsNullOrEmpty(situation))
                {
                    IsEdit = true;
                }
            }
        }
        #endregion

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
                //設計チェック指摘の登録
                if (this.EntryDesignCheckPoint() == true)
                {
                    //フォームクローズ
                    base.FormOkClose();
                }
            });
        }

        /// <summary>
        /// 設計チェック指摘の登録
        /// </summary>
        private bool EntryDesignCheckPoint()
        {
            var list = new List<DesignCheckProgressPostInModel>();

            foreach (DataGridViewRow row in this.ListDataGridView.Rows)
            {
                var id = Convert.ToInt32(row.Cells[IdColumn.Name].Value);

                var car = this.DesignCheckCarList.First(x => x.ID == id);

                var situation = Convert.ToString(row.Cells[Column2.Name].Value);

                // いずれかの状況を選ばないと、指摘登録ができない
                if (string.IsNullOrEmpty(situation))
                {
                    Messenger.Warn(Resources.KKM03049);
                    return false;
                }

                list.Add(new DesignCheckProgressPostInModel
                {
                    // 開催日ID
                    開催日_ID = kaisaibiID,

                    // 対象車両ID
                    対象車両_ID = car.ID,

                    // 指摘ID
                    指摘_ID = pointID.Value,

                    // 設計チェック指摘と状況の同時登録用
                    編集者_ID = SessionDto.UserId,

                    // 状況
                    状況 = situation,
                });
            }

            // 指摘追加か対象車追加か？
            if (pointID == DesignCheckPointListForm.TEMP_POINT_ID)
            {
                // 指摘と状況の登録
                var res = HttpUtil.PostResponse(ControllerType.DesignCheckPointProg, list);
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    return false;
                }
            }
            else
            {
                // 状況のみ登録
                var res = HttpUtil.PostResponse(ControllerType.DesignCheckProgress, list);
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    return false;
                }
            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            IsEdit = false;

            return true;
        }
        #endregion

        #region 選択した対象車の取得
        /// <summary>
        /// 選択した対象車の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckCarGetOutModel> GetDesignCheckCarList()
        {
            var list = new List<DesignCheckCarGetOutModel>();

            var cond = new DesignCheckCarGetInModel
            {
                //開催日ID
                開催日_ID = kaisaibiID
            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckCarGetInModel, DesignCheckCarGetOutModel>(ControllerType.DesignCheckCar, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            //車両選択画面で選択した対象車のリスト
            var newList = new List<DesignCheckCarGetOutModel>();

            foreach (var k in this.CarList)
            {
                newList.AddRange(list.Where(x => x.ID == k.対象車両_ID).ToList());
            }

            return newList;
        }
        #endregion

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        private void ListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 無効セルは終了
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            var grid = (DataGridView)sender;
            var row = grid.Rows[e.RowIndex];
            var col = grid.Columns[e.ColumnIndex];
        }
        #endregion
    }
    //Append End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
}
