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

namespace DevPlan.Presentation.UIDevPlan.Announce
{
    public partial class AnnounceListForm : BaseForm
    {
        #region インスタンス
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;
        #endregion

        public override string FormTitle { get { return "お知らせ一覧"; } }

        //お知らせ一覧権限
        public UserAuthorityOutModel UserAuthority { get; set; }

        private List<InformationOutModel> AnnounceList;

        public AnnounceListForm()
        {
            InitializeComponent();
        }

        private void AnnounceListForm_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        /// <summary>
        /// 画面上のコントロール初期化
        /// </summary>
        private void InitForm()
        {
            //お知らせ一覧権限取得
            this.UserAuthority = base.GetFunction(FunctionID.Notice);

            // 更新権限を持っていなければ追加ボタンを非表示にする
            base.FunctionAuthList = base.GetFunctionList();
            if (!IsUpdatable(FunctionID.Notice))
            {
                AddItemButton.Visible = false;
            }

            BeforeInfoCheckBox.Checked = true;
            ReleacedCheckBox.Checked = true;
            ClosedInfoCheckBox.Checked = false;

            this.CustomTemplate = new CustomTemplate() { MultiRow = this.AnnounceListMultiRow, RowCountLabel = this.RowCountLabel };

            // テンプレートの読み込み
            this.AnnounceListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new AnnounceListMultiRowTemplate());

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
        /// 検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            //検索可能かどうか
            if (this.IsSearch() == true)
            {
                SetGridData();
            }
        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearch()
        {
            var list = (new[] { this.BeforeInfoCheckBox, this.ReleacedCheckBox, this.ClosedInfoCheckBox }).ToList();

            //すべて未入力ならエラー
            if (list.All(x => x.Checked == false) == true)
            {
                list.ForEach(x => x.BackColor = Const.ErrorBackColor);
                Messenger.Warn(Resources.KKM00004);
                return false;
            }

            list.ForEach(x => x.BackColor = this.BeforeInfoCheckBox.Parent.BackColor);

            return true;

        }

        /// <summary>
        /// グリッドデータの設定
        /// </summary>
        private void SetGridData()
        {
            // お知らせ一覧読込
            this.AnnounceList = GetAnnounceList();

            // データバインド
            this.CustomTemplate.SetDataSource(AnnounceList);

            //検索結果の文言
            this.ListDataLabel.Text = AnnounceList.Any() == true ? "" : Resources.KKM00005;

            // 未選択状態
            this.AnnounceListMultiRow.CurrentCell = null;
        }

        /// <summary>
        /// お知らせ検索APIの呼び出し
        /// </summary>
        /// <returns></returns>
        private List<InformationOutModel> GetAnnounceList()
        {
            int Status = 0;

            // 公開中のみ
            if (!BeforeInfoCheckBox.Checked && ReleacedCheckBox.Checked && !ClosedInfoCheckBox.Checked)
                Status = 1;

            // 全て
            if (BeforeInfoCheckBox.Checked && ReleacedCheckBox.Checked && ClosedInfoCheckBox.Checked)
                Status = 2;

            // 公開前のみ
            if (BeforeInfoCheckBox.Checked && !ReleacedCheckBox.Checked && !ClosedInfoCheckBox.Checked)
                Status = 3;

            // 公開終了のみ
            if (!BeforeInfoCheckBox.Checked && !ReleacedCheckBox.Checked && ClosedInfoCheckBox.Checked)
                Status = 4;

            // 公開前と公開中
            if (BeforeInfoCheckBox.Checked && ReleacedCheckBox.Checked && !ClosedInfoCheckBox.Checked)
                Status = 5;

            // 公開前と公開終了
            if (BeforeInfoCheckBox.Checked && !ReleacedCheckBox.Checked && ClosedInfoCheckBox.Checked)
                Status = 6;

            // 公開中と公開終了
            if (!BeforeInfoCheckBox.Checked && ReleacedCheckBox.Checked && ClosedInfoCheckBox.Checked)
                Status = 7;

            var cond = new InformationInModel
            {
                STATUS = Status
            };

            return base.GetAnnounceList(cond);

        }

        /// <summary>
        /// リンククリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceListMultiRow_CellContentClick(object sender, CellEventArgs e)
        {
            if (e.CellIndex == 2 && e.RowIndex >= 0)
            {
                try
                {
                    var id = Convert.ToInt64(AnnounceListMultiRow.Rows[e.RowIndex].Cells["GridId"].Value);
                    var val = ((List<InformationOutModel>)(AnnounceListMultiRow.DataSource)).Find(x => x.ID == id);

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
            using (var frm = new AnnounceDetailForm() { UserAuthority = this.UserAuthority })
            {
                frm.AnnounceID = null;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // 現在のセル位置を退避
                    var position = AnnounceListMultiRow.FirstDisplayedCellPosition;

                    SetGridData();

                    // 行がある場合はスクロール調整
                    if (position.RowIndex > 0)
                    {
                        // MultiRowバグ対策
                        var maxCellIndex = AnnounceListMultiRow.Template.Row.Cells["GridUpdater"].CellIndex;

                        // 退避したセル位置に移動
                        AnnounceListMultiRow.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
                    }
                }
            }
        }

        /// <summary>
        /// MultiRowセルダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnnounceListMultiRow_CellDoubleClick(object sender, CellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            //変数を引き渡す
            using (var frm = new AnnounceDetailForm() { UserAuthority = this.UserAuthority})
            {
                frm.AnnounceID = Convert.ToInt64(AnnounceListMultiRow.Rows[e.RowIndex].Cells["GridId"].Value);
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    // 現在のセル位置を退避
                    var position = AnnounceListMultiRow.FirstDisplayedCellPosition;

                    SetGridData();

                    // 行がある場合はスクロール調整
                    if (position.RowIndex > 0)
                    {
                        // MultiRowバグ対策
                        var maxCellIndex = AnnounceListMultiRow.Template.Row.Cells["GridUpdater"].CellIndex;

                        // 退避したセル位置に移動
                        AnnounceListMultiRow.FirstDisplayedCellPosition = new CellPosition(position.RowIndex, maxCellIndex);
                    }
                }
            }
        }
    }
}
