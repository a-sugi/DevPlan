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
    /// <summary>
    /// 指摘対象車追加
    /// </summary>
    public partial class PointingTargetCarAddForm : BaseSubForm
    {
        #region 定数
        /// <summary>選択</summary>
        private readonly Color Selected = Color.LightGray;
        #endregion

        #region プロパティ
        /// <summary>画面名</summary>
        public override string FormTitle { get { return "指摘対象車追加"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>設計チェック指摘</summary>
        public DesignCheckPointGetOutModel DesignCheckPoint { get; set; }

        /// <summary>設計チェック対象車</summary>
        private List<DesignCheckCarGetOutModel> DesignCheckCarList { get; set; } = new List<DesignCheckCarGetOutModel>();

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        public bool ParentFormType { get; set; } = false;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PointingTargetCarAddForm()
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
                //画面初期化
                this.InitForm();

                //グリッドビューの初期化
                this.InitDataGridView();

            });

        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //指摘No
            this.PointingNoLabel.Text = Convert.ToString(DesignCheckPoint.指摘NO);

            //指摘部品（部位）
            this.PointingPartsLabel.Text = Convert.ToString(DesignCheckPoint.部品);

            // 全選択チェックボックス調整
            AdjustAllSelectCheckBox();
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

        /// <summary>
        /// グリッドビューの初期化
        /// </summary>
        private void InitDataGridView()
        {
            //列の自動生成不可
            this.ListDataGridView.AutoGenerateColumns = false;

            //設計チェック対象車の取得
            this.DesignCheckCarList = this.GetDesignCheckCarList();

            //データソース
            this.ListDataGridView.DataSource = this.DesignCheckCarList;

            //一覧が取得できたかどうか
            if (this.DesignCheckCarList.Any() == false)
            {
                this.ListDataLabel.Text = Resources.KKM00005;

            }
            else
            {
                //設計チェック状況が取得できたかどうか
                var list = this.GetDesignCheckProgressList();
                if (list != null && list.Any() == true)
                {
                    foreach (DataGridViewRow row in this.ListDataGridView.Rows)
                    {
                        var id = Convert.ToInt32(row.Cells[this.IdColumn.Name].Value);

                        //存在する対象車両IDかどうか
                        if (list.Any(x => x.対象車両_ID == id) == true)
                        {
                            //行の設定
                            row.DefaultCellStyle.BackColor = Selected;

                            //選択
                            row.Cells[this.SelectedColumn.Name].Value = true;

                            //読み取り専用
                            row.ReadOnly = true;

                        }

                    }

                }

            }

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
            //一覧を未選択状態に設定
            this.ListDataGridView.CurrentCell = null;

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
            var ccel = this.ListDataGridView.CurrentCell;
            this.ListDataGridView.CurrentCell = null;

            var flg = this.AllSelectCheckBox.Checked;

            //全ての行の選択を設定
            foreach (DataGridViewRow row in this.ListDataGridView.Rows)
            {
                //読取専用かどうか
                if (row.ReadOnly == false)
                {
                    row.Cells[this.SelectedColumn.Name].Value = flg;
                }
            }

            this.ListDataGridView.CurrentCell = ccel;
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
                //設計チェック指摘のチェック
                if (this.EntryDesignCheckPoint() == true)
                {
                    //フォームクローズ
                    base.FormOkClose();
                }
            });
        }

        /// <summary>
        /// 設計チェック指摘の設定
        /// </summary>
        private bool EntryDesignCheckPoint()
        {
            var list = new List<DesignCheckProgressPostInModel>();

            foreach (DataGridViewRow row in this.ListDataGridView.Rows)
            {
                //選択しているかどうか
                if (Convert.ToBoolean(row.Cells[this.SelectedColumn.Name].Value) == true)
                {
                    //追加済みの場合はスキップ
                    if (row.DefaultCellStyle.BackColor == Selected) continue;

                    var id = Convert.ToInt32(row.Cells[IdColumn.Name].Value);

                    var car = this.DesignCheckCarList.First(x => x.ID == id);

                    list.Add(new DesignCheckProgressPostInModel
                    {
                        // 開催日ID
                        開催日_ID = this.DesignCheckPoint.開催日_ID,

                        // 対象車両ID
                        対象車両_ID = car.ID,

                        // 指摘ID
                        指摘_ID = this.DesignCheckPoint.ID.Value,

                        // 設計チェック指摘と状況の同時登録用
                        編集者_ID = SessionDto.UserId,
                    });
                }
            }

            //選択された車両があるかどうか
            if (list.Any() == false)
            {
                Messenger.Warn(Resources.KKM00009);
                return false;
            }

            //Delete Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            //// 指摘追加か対象車追加か？
            //if (DesignCheckPoint.ID == DesignCheckPointListForm.TEMP_POINT_ID)
            //{
            //    // 指摘と状況の登録
            //    var res = HttpUtil.PostResponse(ControllerType.DesignCheckPointProg, list);
            //    if (res == null || res.Status != Const.StatusSuccess)
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    // 状況のみ登録
            //    var res = HttpUtil.PostResponse(ControllerType.DesignCheckProgress, list);
            //    if (res == null || res.Status != Const.StatusSuccess)
            //    {
            //        return false;
            //    }
            //}

            //登録後メッセージ
            //Messenger.Info(Resources.KKM00002);

            //return true;
            //Delete End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)

            //Append Start 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            if (ParentFormType)
            {
                var pointID = this.DesignCheckPoint.ID;
                var kaisaibiID = this.DesignCheckPoint.開催日_ID;

                using (var form = new PointingTargetSituationAddForm() { pointID = pointID, kaisaibiID = kaisaibiID, CarList = list })
                {
                    return form.ShowDialog() == DialogResult.OK;
                }
            }else
            {
                //Append Start 2021/08/02 杉浦
                // 指摘追加か対象車追加か？
                if (DesignCheckPoint.ID == DesignCheckPointListForm.TEMP_POINT_ID)
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

                return true;
                //Append End 2021/08/02 杉浦
            }
            //Append End 2021/07/30 張晋華 開発計画表設計チェック機能改修(保守対応)
        }
        #endregion

        #region データの取得
        /// <summary>
        /// 設計チェック対象車の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckCarGetOutModel> GetDesignCheckCarList()
        {
            var list = new List<DesignCheckCarGetOutModel>();

            var cond = new DesignCheckCarGetInModel
            {
                //開催日ID
                開催日_ID = this.DesignCheckPoint.開催日_ID
            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckCarGetInModel, DesignCheckCarGetOutModel>(ControllerType.DesignCheckCar, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// 設計チェック状況の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckProgressGetOutModel> GetDesignCheckProgressList()
        {
            var list = new List<DesignCheckProgressGetOutModel>();

            // 指摘追加の場合
            if (this.DesignCheckPoint.ID == DesignCheckPointListForm.TEMP_POINT_ID)
            {
                return list;
            }

            var cond = new DesignCheckProgressGetInModel
            {
                //指摘ID
                指摘_ID = this.DesignCheckPoint.ID,

                //開催日ID
                開催日_ID = this.DesignCheckPoint.開催日_ID

            };

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckProgressGetInModel, DesignCheckProgressGetOutModel>(ControllerType.DesignCheckProgress, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

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

            // チェックボックスをOn/Off
            var checkCell = (DataGridViewCell)(row.Cells[this.SelectedColumn.Name]);

            if (!checkCell.ReadOnly && col.Name != this.SelectedColumn.Name)
            {
                checkCell.Value = !Convert.ToBoolean(checkCell.Value);
            }
        }
        #endregion
    }
}
