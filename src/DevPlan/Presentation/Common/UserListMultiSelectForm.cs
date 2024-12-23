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

using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using DevPlan.Presentation.UC.MultiRow;
using GrapeCity.Win.MultiRow;
using DevPlan.UICommon.Config;

namespace DevPlan.Presentation.Common
{
    /// <summary>
    /// ユーザー一覧検索 複数選択用
    /// </summary>
    public partial class UserListMultiSelectForm : BaseSubForm
    {
        #region インスタンス
        
        /// <summary>カスタムテンプレート</summary>
        private CustomTemplate CustomTemplate;

        #endregion

        #region 公開プロパティ

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "ユーザー一覧"; } }

        /// <summary>横幅</summary>
        public override int FormWidth { get { return this.Width; } }

        /// <summary>縦幅</summary>
        public override int FormHeight { get { return this.Height; } }

        /// <summary>フォームサイズ固定可否</summary>
        public override bool IsFormSizeFixed { get { return true; } }

        /// <summary>権限</summary>
        public UserAuthorityOutModel UserAuthority { get; set; }

        /// <summary>選択ユーザーリスト</summary>
        public List<UserSearchOutModel> Users { get; set; }

        /// <summary>自部課制限フラグ</summary>
        public bool IsAffiliationLimit { get; set; } = false;

        /// <summary>ユーザーステータスコード</summary>
        public string StatusCode { get; set; }

        #endregion

        #region 内部変数

        /// <summary>ユーザーリスト</summary>
        private List<UserSearchOutModel> userList;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UserListMultiSelectForm()
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
        private void UserListForm_Load(object sender, EventArgs e)
        {
            //初期表示フォーカス
            this.ActiveControl = UserListMultiRow;

            // 画面の初期化
            this.InitForm();

            //初期表示時に、検索結果を一覧表示
            this.SetGridData();
        }
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            this.CustomTemplate = new CustomTemplate() { MultiRow = UserListMultiRow, RowCountLabel = this.RowCountLabel };

            // テンプレート
            this.UserListMultiRow.Template = this.CustomTemplate.SetContextMenuTemplate(new UserListMultiSelectMultiRowTemplate());

            this.UserListMultiRow.CellClick += this.UserListMultiRow_CellClick;

            // 選択列のフィルターアイテム設定
            var cellCheck = this.UserListMultiRow.Template.ColumnHeaders[0].Cells["CheckHeaderCell"] as ColumnHeaderCell;
            cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
            cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // 自部課制限有・管理権限なし・更新権限ありの場合は自部課のみ
            if (IsAffiliationLimit && !isManagement && isUpdate)
            {

            }
        }
        #endregion

        #region 検索ボタンのクリック
        /// <summary>
        /// 検索ボタンのクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListSearchButton_Click(object sender, EventArgs e)
        {
            //検索可能かどうか
            if (this.IsSearch() == true)
            {
                this.SetGridData();

            }

        }

        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsSearch()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;

            }

            return true;

        }

        /// <summary>
        /// グリッドデータのセット
        /// </summary>
        private void SetGridData()
        {
            // グリッドのデータソースをAPIから受け取るデータに設定
            this.userList = GetGridDataList();

            // データバインド
            this.CustomTemplate.SetDataSource(this.userList);

            //一覧が取得できたかどうか
            this.ListDataLabel.Text = this.userList.Any() == false ? Resources.KKM00005 : "";

            //一覧を未選択状態に設定
            this.UserListMultiRow.CurrentCell = null;

        }
        #endregion

        #region グリッドデータの取得
        /// <summary>
        /// グリッドデータの取得
        /// </summary>
        private List<UserSearchOutModel> GetGridDataList()
        {
            var isManagement = this.UserAuthority.MANAGEMENT_FLG == '1';
            var isUpdate = this.UserAuthority.UPDATE_FLG == '1';

            // パラメータ設定
            var itemCond = new UserSearchModel
            {
                // ユーザステータスコード
                STATUS_CODE = this.StatusCode
            };

            // Get実行
            var res = HttpUtil.GetResponse<UserSearchModel, UserSearchOutModel>(ControllerType.User, itemCond);

            return res.Results.ToList();
        }
        #endregion

        #region MultiRowセルクリックイベント
        /// <summary>
        /// MultiRowセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListMultiRow_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            // 全選択チェックボックス選択
            if ((e.CellIndex == 0) && (e.RowIndex == -1))
            {
                AllCheckBox.Checked = !AllCheckBox.Checked;
            }

            // 無効セルは終了
            if (e.CellIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            // チェックボックスをOn/Off
            var checkCell = ((CheckBoxCell)((GcMultiRow)sender).Rows[e.RowIndex].Cells["CheckBoxColumn"]);
            checkCell.Value = !Convert.ToBoolean(checkCell.Value);
        }
        #endregion

        #region 全選択チェックボックス描画
        /// <summary>
        /// 全選択チェックボックス描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListMultiRow_CellPainting(object sender, CellPaintingEventArgs e)
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
                    Point point = new Point((bmp.Width - AllCheckBox.Width) / 2, (bmp.Height - AllCheckBox.Height) / 2);
                    if (point.X < 0)
                    {
                        point.X = 0;
                    }
                    if (point.Y < 0)
                    {
                        point.Y = 0;
                    }

                    // Bitmapに描画
                    AllCheckBox.DrawToBitmap(bmp, new Rectangle(point.X, point.Y, bmp.Width, bmp.Height));

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

        #region 全選択チェックボックス操作
        /// <summary>
        /// 全選択チェックボックス操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
             {
                 // 表示されている項目のチェックボックスすべて表示を更新する

                 var dg = UserListMultiRow.CurrentCell;
                 UserListMultiRow.CurrentCell = null;

                 foreach (var row in UserListMultiRow.Rows)
                 {
                     //Append Start 2020/01/27 杉浦
                     // 非表示行は未処理
                     if (!row.Visible) continue;
                     //Append End 2020/01/27 杉浦
                     
                     row.Cells[0].Value = AllCheckBox.Checked;
                 }

                 UserListMultiRow.CurrentCell = dg;
             });            
        }
        #endregion

        #region 登録ボタン押下
        /// <summary>
        /// 登録ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                if (UserListMultiRow.Rows.All((x) => Convert.ToBoolean(x.Cells[0].Value) == false))
                {
                    base.FormCancelClose();
                    return;
                }

                var targets = UserListMultiRow.Rows
                    .Where((x) => Convert.ToBoolean(x.Cells["CheckBoxColumn"].Value) == true)
                    .Select((x) => x.Cells["PersonelIDColumn"].Value.ToString())
                    .ToList();
                this.Users = this.userList.Where((x) => targets.Contains(x.PERSONEL_ID)).ToList();
                base.FormOkClose();
            });
        }
        #endregion
    }
}
