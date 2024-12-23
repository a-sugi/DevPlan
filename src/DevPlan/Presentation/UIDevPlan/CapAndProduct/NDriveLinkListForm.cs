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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Utils;
using DevPlan.UICommon.Properties;

using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;

using GrapeCity.Win.MultiRow;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon.Config;

//Append Start 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加
namespace DevPlan.Presentation.UIDevPlan.CapAndProduct
{
    public partial class NDriveLinkListForm : BaseForm
    {
        #region インスタンス
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        public override string FormTitle { get { return "写真・動画一覧"; } }

        //写真・動画一覧権限
        public UserAuthorityOutModel UserAuthority { get; set; }

        private List<NDriveLinkOutModel> NDriveLinkList;

        /// <summary>お知らせCapID</summary>
        public int NDriveLinkCapID { get; set; }

        public NDriveLinkListForm()
        {
            InitializeComponent();
        }

        private void NDriveLinkListForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        /// <summary>
        /// 画面上のコントロール初期化
        /// </summary>
        private void InitForm()
        {
            //写真・動画一覧権限取得
            this.UserAuthority = base.GetFunction(FunctionID.CAP);

            // 更新権限を持っていなければ追加ボタンを非表示にする
            base.FunctionAuthList = base.GetFunctionList();
            if (!IsUpdatable(FunctionID.CAP))
            {
                AddItemButton.Visible = false;
            }

            this.CustomTemplate = new CustomTemplate() { MultiRow = this.NDriveLinkListMultiRow, RowCountLabel = this.RowCountLabel };

            // テンプレートの読み込み
            this.NDriveLinkListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new NDriveLinkListMultiRowTemplate());

            // グリッドのセット
            SetGridData();
        }

        /// <summary>
        /// 機能権限チェック
        /// </summary>
        /// <param name="function">機能ID</param>
        /// <returns></returns>
        public bool IsUpdatable(FunctionID function)
        {
            var id = (int)function;

            return base.FunctionAuthList.Any(x => x.FUNCTION_ID == id && x.UPDATE_FLG == '1');

        }

        /// <summary>
        /// グリッドデータの設定
        /// </summary>
        private void SetGridData()
        {
            // 写真・動画一覧読込
            this.NDriveLinkList = GetNDriveLinkList();

            // データバインド
            this.CustomTemplate.SetDataSource(NDriveLinkList);

            //検索結果の文言
            this.ListDataLabel.Text = NDriveLinkList.Any() == true ? "" : Resources.KKM00005;

            // 未選択状態
            this.NDriveLinkListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// 写真・動画検索APIの呼び出し
        /// </summary>
        /// <returns></returns>
        private List<NDriveLinkOutModel> GetNDriveLinkList()
        {
            var list = new List<NDriveLinkOutModel>();
            var cond = new NDriveLinkInModel { CAP_ID = this.NDriveLinkCapID };
            var res = HttpUtil.GetResponse<NDriveLinkInModel, NDriveLinkOutModel>(ControllerType.NDriveLink, cond);

            if (res != null && res.Status == Const.StatusSuccess && res.Results != null && res.Results.Any() == true)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        /// <summary>
        /// リンククリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NDriveLinkListMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.CellIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    var id = Convert.ToInt64(NDriveLinkListMultiRow.Rows[e.RowIndex].Cells["GridId"].Value);
                    var val = ((List<NDriveLinkOutModel>)(NDriveLinkListMultiRow.DataSource)).Find(x => x.ID == id);

                    Process.Start(val.URL);
                }
                catch
                {
                    // リンククリック時にエラーが発生した場合は何もしない
                }

            }
        }

        /// <summary>
        /// 追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            using (var frm = new NDriveLinkDetailForm() { UserAuthority = this.UserAuthority, NDriveLinkCapID = this.NDriveLinkCapID })
            {
                frm.NDriveLinkID = null;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // 現在のセル位置を退避
                    var position = NDriveLinkListMultiRow.FirstDisplayedCellPosition;

                    SetGridData();

                    // 行がある場合はスクロール調整
                    if (position.RowIndex > 0)
                    {
                        // MultiRowバグ対策
                        var maxCellIndex = NDriveLinkListMultiRow.Template.Row.Cells["GridUpdater"].CellIndex;

                        // 退避したセル位置に移動
                        NDriveLinkListMultiRow.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
                    }
                }
            }
        }

        /// <summary>
        /// MultiRowセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NDriveLinkListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //変数を引き渡す
            using (var frm = new NDriveLinkDetailForm() { UserAuthority = this.UserAuthority })
            {
                frm.NDriveLinkID = Convert.ToInt64(NDriveLinkListMultiRow.Rows[e.RowIndex].Cells["GridId"].Value);
                frm.NDriveLinkCapID = this.NDriveLinkCapID;
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // 現在のセル位置を退避
                    var position = NDriveLinkListMultiRow.FirstDisplayedCellPosition;

                    SetGridData();

                    // 行がある場合はスクロール調整
                    if (position.RowIndex > 0)
                    {
                        // MultiRowバグ対策
                        var maxCellIndex = NDriveLinkListMultiRow.Template.Row.Cells["GridUpdater"].CellIndex;

                        // 退避したセル位置に移動
                        NDriveLinkListMultiRow.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
                    }
                }
            }
        }
    }
}

//Append End 2021/05/21 杉浦 CAP確認結果に「写真・動画」列を追加