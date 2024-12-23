using DevPlan.Presentation.Base;
using DevPlan.Presentation.Common;
using DevPlan.Presentation.UC.MultiRow;
using DevPlan.UICommon;
using DevPlan.UICommon.Config;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    /// <summary>
    /// 設計チェック
    /// </summary>
    public partial class DesignCheckDetailListForm : BaseForm
    {
        #region メンバ変数

        private const string progressHelpMsg = "処置記号を\r\n入力してください。";

        //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
        private const string NoneText = "－";
        //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

        /// <summary>
        /// クラス生成クラス
        /// </summary>
        private DetailFactory Factory;

        /// <summary>バインド中可否</summary>
        private bool IsBind { get; set; }

        //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
        /// <summary>編集フラグ</summary>
        private bool IsEdit { get; set; } = false;
        //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

        /// <summary>再読込フラグ</summary>
        private bool IsRefresh { get; set; } = false;

        /// <summary>検索条件</summary>
        private DesignCheckPointGetInModel ListSearchCond { get; set; }

        /// <summary>
        /// ADユーザー情報
        /// </summary>
        private Dictionary<string, ADUserInfo> ADUserDictionary;

        #endregion

        #region プロパティ
        //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
        /// <summary>
        /// 指摘追加により追加された場合の仮の指摘ID
        /// </summary>
        public const int TEMP_POINT_ID = 0;
        //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

        /// <summary>画面名</summary>
        public override string FormTitle { get { return "設計チェック"; } }

        /// <summary>設計チェック</summary>
        public DesignCheckGetOutModel DesignCheck { get; set; } = null;

        /// <summary>
        /// 親画面の再描画
        /// </summary>
        public System.Action ParentFormRefresh { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DesignCheckDetailListForm()
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
        private void DesignCheckDetailListForm_Load(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 初期化
                Factory = new DetailFactory(
                    this.PointMultiRow,
                    RowCountLabel,
                    DesignCheck,
                    base.GetFunction(FunctionID.DesignCheck),
                    () => { Factory.MyColleague.Edited(); PointMultiRow.Refresh(); });

                // AD情報の取得
                this.ADUserDictionary = ADUserInfoData.Dictionary;

                //画面初期化
                this.InitForm();

                //設計チェック詳細一覧設定
                this.SetDesignCheckDetailList();

                // 担当者セルクリックイベント登録
                Factory.BtnUser.SingleAction = () =>
                {
                    FormControlUtil.FormWait(this, () =>
                    {
                        this.PointMultiRow.CurrentCell.ReadOnly = true;
                        var detail = Factory.MultiRowContoller.GetModel(this.PointMultiRow.CurrentCell.RowIndex);

                        //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                        if (detail.試作管理NO == null)
                        {
                            //Append End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                            using (var form = new UserListForm { UserAuthority = Factory.Authority.UserAuthorityModel, SectionCode = detail.担当課名, StatusCode = "a", IsSearchLimit = false })
                            {
                                if (form.ShowDialog(this) == DialogResult.OK)
                                {
                                    detail.担当課名 = form.User.SECTION_CODE;
                                    detail.担当者名 = form.User.NAME;
                                    detail.担当者_ID = form.User.PERSONEL_ID;

                                    // 担当者TEL
                                    this.SetADUserInfo(this.PointMultiRow.CurrentRow);

                                    //編集フラグを有効化
                                    this.Factory.MyColleague.Edited();

                                    //グリッドリフレッシュ
                                    this.PointMultiRow.Refresh();
                                }
                            }
                            //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                        }
                        //Append End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                    });
                };

                // 担当者セルダブルクリックイベント登録
                Factory.BtnUser.DoubleAction = () =>
                {
                    //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                    var detail = Factory.MultiRowContoller.GetModel(this.PointMultiRow.CurrentCell.RowIndex);
                    if (detail.試作管理NO == null)
                    {
                        //Append End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                        this.PointMultiRow.CurrentCell.ReadOnly = false;
                        this.PointMultiRow.BeginEdit(true);
                        //Append Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                    }
                    //Append End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                };
            });
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                // 開催日
                this.OpenDayLabel.Text = DateTimeUtil.ConvertDateString(this.DesignCheck?.開催日);

                // 設計チェック名
                this.DesignCheckNameLabel.Text = this.DesignCheck?.名称;
                this.DesignCheckNameLabel.Text += this.DesignCheck?.回 > 0 ? " " + this.DesignCheck?.回 + "回目" : string.Empty;

                // ボタン表示制御
                this.RowAddButton.Visible = Factory.Authority.IsVisible(RowAddButton);
                this.RowDeleteButton.Visible = Factory.Authority.IsVisible(RowDeleteButton);
                this.EntryButton.Visible = Factory.Authority.IsVisible(EntryButton);
                this.DownloadButton.Visible = Factory.Authority.IsVisible(DownloadButton);
                this.Copybutton.Visible = Factory.Authority.IsVisible(Copybutton);
                this.DesignCheckUserListButton.Visible = Factory.Authority.IsVisible(DesignCheckUserListButton);

                // 注意文言描画
                this.SetNotice();

                // 吹き出し描画
                this.SetCallout();
            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;
            }
        }

        /// <summary>
        /// 注意文言描画
        /// </summary>
        /// <returns></returns>
        private void SetNotice()
        {
            Bitmap bmp = new Bitmap(PictNotice.Width, PictNotice.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            using (Graphics grph = Graphics.FromImage(bmp))
            {
                // 塗りつぶす
                grph.Clear(ContentsPanel.BackColor);

                // DPIに合わせてワールド変換を設定
                float scale = grph.DpiX / 96f;
                System.Drawing.Drawing2D.Matrix morg = grph.Transform;
                grph.ScaleTransform(scale, scale);

                // 四角い枠を描く
                using (Pen pen = new Pen(Color.Red, 2 / scale))
                {
                    grph.DrawRectangle(pen, 10, 5, 220, 25);
                }

                // 枠の中に文字を書く
                using (Font font = new Font("MS UI Gothic", 11 / scale, FontStyle.Bold))
                {
                    grph.DrawString("必ず参加者登録をしてください。", font, Brushes.Red, 20, 10);
                }

                // ワールド変換を元に戻しておく
                grph.Transform = morg;
            }

            PictNotice.Image = bmp;
        }

        /// <summary>
        /// 吹き出し描画
        /// </summary>
        /// <returns></returns>
        private void SetCallout()
        {
            // 吹き出し画像設定
            MsgPicture.Image = Properties.Resources.DchkMsg;
            MsgPicture.GetType().InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               MsgPicture,
               new object[] { true });

            Factory.MultiRowContoller.MsgPicture = MsgPicture;
        }

        /// <summary>
        /// 吹き出し表示位置初期化
        /// </summary>
        private void InitCalloutLocation()
        {
            var grid = this.PointMultiRow;

            grid.FirstDisplayedLocation = new Point(0, grid.FirstDisplayedLocation.Y);

            var scl = new DeviceUtil().GetScalingFactor();
            var cell = grid.ColumnHeaders[0].Cells["ch_TreatmentWhen"];
            var x = cell.Location.X + cell.Size.Width - grid.Location.X - Convert.ToInt32(28 * scl);

            this.MsgPicture.Location = new Point(x, this.MsgPicture.Location.Y);
        }

        /// <summary>
        /// AD情報のセット
        /// </summary>
        /// <param name="row"></param>
        private void SetADUserInfo(Row row)
        {
            if (ADUserDictionary == null)
            {
                return;
            }

            if (row.Cells["UserIDTextBoxColumn"].Value == null)
            {
                return;
            }

            var personelId = Convert.ToString(row.Cells["UserIDTextBoxColumn"].Value);
            var searchPersonelId = personelId.PadLeft(5, '0').Substring(0, 5);

            var val = new ADUserInfo();
            var key = string.Format("{0}_{1}", searchPersonelId, Convert.ToString(row.Cells["UserTextBoxColumn"].Value)).Replace(" ", "").Replace("　", "");

            ADUserDictionary.TryGetValue(key, out val);

            row["UserTelTextBoxColumn"].Value = string.Empty;

            // 内線番号あり
            if (val != null || val?.Tel != null)
            {
                row["UserTelTextBoxColumn"].Value = val.Tel;
            }
        }
        #endregion

        #region フォーム表示前
        /// <summary>
        /// フォーム表示前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckDetailListForm_Shown(object sender, EventArgs e)
        {
            // 参加者一覧画面開く
            Factory.UserListFormContoroller.OpenCenter();
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
            FormControlUtil.FormWait(this, () =>
            {
                //画面を変更していて登録するかどうか
                if (this.IsEditDesignCheckDetailEntry() == true)
                {
                    // AD情報の取得
                    ADUserDictionary = ADUserInfoData.Dictionary;

                    //設計チェック詳細一覧設定
                    this.SetDesignCheckDetailList();
                }
            });
        }
        #endregion

        #region 指摘追加ボタンクリック
        /// <summary>
        /// 指摘追加ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowAddButton_Click(object sender, EventArgs e)
        {
            //Delete Start 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
            //指摘がある場合→対象車両状況をチェックする
            //if (this.PointMultiRow.RowCount > 0)
            //{
            //    //対象車両の状況チェック
            //    var check = SituationCheck();

            //    if (!check)
            //    {
            //        //状況記号が設定されてないと、指摘登録ができない
            //        Messenger.Warn(Resources.KKM03049);
            //        return;
            //    }
            //}
            //Delete End 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)

            //Update Start 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
            // 画面を変更していて登録するかどうか
            // [false：編集されていても登録しない場合] → 編集したデータを廃棄し、指摘を追加する
            // [true：編集されていない場合、又は編集されていて登録する場合] → (編集したデータを登録し、)指摘を追加する
            if (Factory.EditMediator.AddPointJudge() == true)
            {
                // 編集された場合だけ編集したデータを登録する
                if (IsEdit == true)
                {
                    this.EntryDesignCheckDetail();
                }
            }

            // 新規行に対象車を追加
            if (this.Factory.PointHandleController.IsAddTargetCar(Factory.Authority.UserAuthorityModel))
            {
                FormControlUtil.FormWait(this, () =>
                {
                    // 再描画
                    this.SetDesignCheckDetailList();

                    // 追加行を選択状態に設定
                    var newRowIndex = Factory.MultiRowContoller.GetLastPointRowIndex();
                    this.Factory.MultiRowContoller.SelectRow(newRowIndex);
                    this.Factory.MultiRowContoller.ScrollRow(newRowIndex);

                    // コピーの確認
                    if (Messenger.Confirm(Resources.KKM01017) == DialogResult.Yes)
                    {
                        // 選択チェック初期化
                        this.Factory.MultiRowContoller.SetTargetAllOff();

                        // 最終行をチェック
                        this.Factory.MultiRowContoller.SetTargetEndRow();

                        // コピー貼り付け
                        this.Factory.PointHandleController.Copy();

                        // 選択チェック初期化
                        this.Factory.MultiRowContoller.SetTargetAllOff();
                    }
                });

                // 再読込フラグ
                this.IsRefresh = true;
            }
            //Update End 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
        }
        #endregion

        #region 指摘コピーボタン押下
        /// <summary>
        /// 指摘コピーボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Copybutton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                Factory.PointHandleController.Copy();
            });
        }
        #endregion

        #region 指摘削除ボタンクリック
        /// <summary>
        /// 指摘削除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDeleteButton_Click(object sender, EventArgs e)
        {
            FormControlUtil.FormWait(this, () =>
            {
                // 削除実行
                if (Factory.PointHandleController.Del())
                {
                    // DB削除により行がなくなった場合
                    if (this.PointMultiRow.RowCount <= 0)
                    {
                        //編集フラグ初期化
                        this.Factory.EditMediator.Reset();

                        //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                        //編集フラグ初期化
                        this.IsEdit = false;
                        //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                    }

                    // 再読込フラグ
                    this.IsRefresh = true;
                }
            });
        }
        #endregion

        #region 設計チェック詳細一覧イベント

        #region マウスイン
        /// <summary>
        /// マウスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseEnter(object sender, CellEventArgs e)
        {
            //行ヘッダー以外は終了
            if (e.RowIndex != -1)
            {
                return;
            }

            // 試験車列
            if (Factory.MultiRowContoller.CarModelList?.Count() > 0)
            {
                if (Factory.MultiRowContoller.CarModelList.FirstOrDefault(x => string.Format("ch_Car{0}", x.ID.ToString()) == e.CellName) != null)
                {
                    //ツールチップを表示
                    this.SituationToolTip.Active = false;
                    this.SituationToolTip.Active = true;
                    this.SituationToolTip.Show(progressHelpMsg, this.PointMultiRow);
                }

                return;
            }
        }
        #endregion

        #region マウスアウト
        /// <summary>
        /// マウスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseLeave(object sender, CellEventArgs e)
        {
            //ツールチップを非表示
            this.SituationToolTip.Hide(this.PointMultiRow);
        }
        #endregion

        #region マウスキーダウンイベント
        /// <summary>
        /// マウスキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            // 通常の左クリック以外受け付けない
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            // 編集モード中なら終了
            if (this.PointMultiRow.IsCurrentCellInEditMode)
            {
                return;
            }

            // 各ボタン処理
            FormControlUtil.FormWait(this, () => Factory.GetBtnInstance(e.CellName)?.Action(Factory.MultiRowContoller.GetModel(e.RowIndex)));
        }
        #endregion

        #region 値変更後
        /// <summary>
        /// 値変更後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellValueChanged(object sender, CellEventArgs e)
        {
            // バインド中のイベントは処理しない
            if (this.IsBind == true)
            {
                return;
            }

            // 無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var row = this.PointMultiRow.Rows[e.RowIndex];

            // バインドフラグOn
            this.IsBind = true;

            try
            {
                //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                //ステータス
                if (e.CellName == "StatusComboBoxColumn")
                {
                    Factory.MultiRowContoller.ChangeStatusEnable(row);
                }

                // 編集フラグを有効化
                if (e.CellName != "TargetCheckBoxColumn" && e.CellName != "HistoryLinkColumn")
                {
                    Factory.MyColleague.Edited();
                    this.IsEdit = true;
                }
                //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                // 担当者
                if (e.CellName == "UserTextBoxColumn")
                {
                    Factory.StaffController.Notice(row);
                }
            }
            finally
            {
                // バインドフラグOff
                this.IsBind = false;
            }
        }
        #endregion

        #region データエラーイベント
        /// <summary>
        /// データエラーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_DataError(object sender, DataErrorEventArgs e)
        {
            if (Factory.MultiRowContoller.CarModelList.Where(x => string.Format("Car_{0}", x.ID.ToString()) == e.CellName).Any() ||
                e.CellName == "TreatmentCheckBoxColumn" || e.CellName == "TreatmentOKCheckBoxColumn" ||
                e.CellName == "RepairCheckBoxColumn" || e.CellName == "ApprovalOKCheckBoxColumn")
            {
                //例外無視
                e.Cancel = false;
            }
        }
        #endregion

        #region チェックボックスセル変更時
        /// <summary>
        /// チェックボックスセル変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            // チェックボックスセルを変更すると内部バリデーションで値エラーが起こるのでエラーを無効化して値を強制設定する
            if (e.CellName == "TreatmentCheckBoxColumn" || e.CellName == "TreatmentOKCheckBoxColumn" ||
                e.CellName == "RepairCheckBoxColumn" || e.CellName == "ApprovalOKCheckBoxColumn")
            {
                if (Convert.ToBoolean(this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].EditedFormattedValue) == true)
                {
                    this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = (int?)1;
                }
                else
                {
                    this.PointMultiRow.Rows[e.RowIndex].Cells[e.CellIndex].Value = null;
                }
            }
        }
        #endregion

        #region MultiRow編集開始イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集開始イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.VerticalResize(100);
            }
        }
        #endregion

        #region MultiRow編集終了イベント（共通化検討中）
        /// <summary>
        /// MultiRow編集終了イベント（共通化検討中）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PointMultiRow_CellEndEdit(object sender, CellEndEditEventArgs e)
        {
            //無効な行か列の場合は終了
            if (e.RowIndex < 0 || e.CellIndex < 0)
            {
                return;
            }

            var cell = ((GcMultiRow)sender).CurrentCell;

            if (cell is TextBoxCell && ((TextBoxCell)cell).Style.Multiline != MultiRowTriState.False)
            {
                cell.PerformVerticalAutoFit();
            }
        }
        #endregion

        #region Windowリサイズイベント
        /// <summary>
        /// Windowリサイズイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckDetailListForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.InitCalloutLocation();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                var location = this.PointMultiRow.FirstDisplayedLocation;

                if (location.X > 0)
                {
                    this.PointMultiRow.FirstDisplayedLocation = new Point(0, location.Y);
                }
            }
        }
        #endregion

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
                // 登録実行
                if (this.EntryDesignCheckDetail() == true)
                {
                    //設計チェック詳細一覧設定
                    this.SetDesignCheckDetailList();
                }
            });
        }
        #endregion

        #region 参加者登録ボタンクリック
        /// <summary>
        /// 参加者登録ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckUserListButton_Click(object sender, EventArgs e)
        {
            Factory.UserListFormContoroller.OpenCenter();
        }
        #endregion

        #region Excel出力ボタンクリック
        /// <summary>
        /// Excel出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            var util = new MultiRowUtil(this.PointMultiRow);
            util.Excel.GetCols = () => this.PointMultiRow.Columns.Cast<Column>().Where(x => x.Name != "TargetCheckBoxColumn" && x.Name != "RowHeader");
            util.Excel.TreatmentRows = (x) => x.Select((y) => { y.InsertRange(0, new string[] { OpenDayLabel.Text, DesignCheckNameLabel.Text }); return y; }).ToList();
            util.Excel.TreatmentHeaders = (x) => { x.InsertRange(0, new string[] { "開催日", "設計チェック名" }); return x; };
            util.Excel.Out(args: new object[] { this.FormTitle, DateTime.Now });
        }
        #endregion

        //Append Start 2021/08/20 杉浦 設計チェック請負
        private void AutoFitLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 行高さ調整
            this.Factory.MultiRowContoller.AdjustmentRowsVertical();
        }
        //Append End 2021/08/20 杉浦 設計チェック請負

        #region フォームクローズ前
        /// <summary>
        /// フォームクローズ前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckDetailListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            FormControlUtil.FormWait(this, () =>
            {
                //設計チェック詳細の編集は終了
                this.PointMultiRow.EndEdit();

                //画面を変更していて登録するかどうか
                if (this.IsEditDesignCheckDetailEntry() == false)
                {
                    //登録に失敗した場合は閉じさせない
                    e.Cancel = true;
                    return;
                }
            });
        }
        #endregion

        #region フォームクローズ後
        /// <summary>
        /// フォームクローズ後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DesignCheckDetailListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 参加者一覧画面も閉じる
            Factory.UserListFormContoroller.Close();

            // 更新の場合のみ
            if (this.IsRefresh)
            {
                // 親画面の再描画
                ParentFormRefresh();
            }
        }
        #endregion

        #region 設計チェック詳細一覧設定
        /// <summary>
        /// 設計チェック詳細一覧設定
        /// </summary>
        private void SetDesignCheckDetailList()
        {
            //バインド中ON
            this.IsBind = true;

            try
            {
                // 設計チェック詳細取得
                this.Factory.MultiRowContoller.PointModelList = this.GetDesignCheckDetailList();

                // 設計チェック詳細一覧設定
                this.Factory.MultiRowContoller.Bind();

                // レイアウトの設定
                this.AdjustDesignCheckDetailList();

                //注意文言表示位置初期化
                this.InitCalloutLocation();

                // リストラベル
                this.ListDataLabel.Text = string.Empty;

                // 指摘がない場合
                if (!this.Factory.MultiRowContoller.PointModelList.Any())
                {
                    // 指摘追加
                    this.Factory.PointHandleController.Add(false);

                    // リストラベル
                    this.ListDataLabel.Text = Resources.KKM00005;
                }

                //一覧を未選択状態に設定
                this.PointMultiRow.CurrentCell = null;

                //編集フラグ初期化
                this.Factory.EditMediator.Reset();
            }
            finally
            {
                //バインド中OFF
                this.IsBind = false;

                //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                //編集フラグ初期化
                this.IsEdit = false;
                //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            }
        }

        /// <summary>
        /// 設計チェック詳細一覧調整
        /// </summary>
        private void AdjustDesignCheckDetailList()
        {
            foreach (Row row in this.PointMultiRow.Rows)
            {
                // 担当者TELが登録されていない場合
                if (string.IsNullOrWhiteSpace(Convert.ToString(row.Cells["UserTelTextBoxColumn"].Value)))
                {
                    // 担当者TEL
                    SetADUserInfo(row);
                }

                int pointId = Convert.ToInt32(row.Cells["IDTextBoxColumn"].Value);

                // 試験車
                foreach (var car in Factory.MultiRowContoller.CarModelList)
                {
                    row.Cells[string.Format("Car_{0}", car.ID.ToString())].Value = Factory.MultiRowContoller.ProgressController.GetProgSymbol(pointId, car.ID);
                }

                // 編集許可制御を設定
                Factory.MultiRowContoller.SetRowEnable(row);

                //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                // 履歴が無い場合は履歴はリンクを解除
                if (Convert.ToInt32(row.Cells["HistoryCountColumn"].Value) <= 0)
                {
                    row.Cells["HistoryLinkColumn"].Value = NoneText;

                    var linkCell = row.Cells["HistoryLinkColumn"] as LinkLabelCell;
                    linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                    linkCell.LinkColor = Color.Black;
                }

                // 背景色設定
                Factory.MultiRowContoller.SetRowBackColor(row, Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value));
                //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

            }
        }
        #endregion

        #region 変更設計チェック詳細登録可否
        /// <summary>
        /// 変更設計チェック詳細登録可否
        /// </summary>
        /// <returns></returns>

        private bool IsEditDesignCheckDetailEntry()
        {
            // 編集の判定
            if (Factory.EditMediator.Judge() == false)
            {
                return true;
            }

            //設計チェック詳細の登録
            return this.EntryDesignCheckDetail();
        }

        //Delete Start 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
        ///// <summary>
        ///// 追加する指摘の登録処理
        ///// 単発処理用
        ///// </summary>
        ///// <returns></returns>
        //private void Entry()
        //{
        //    FormControlUtil.FormWait(this, () =>
        //    {
        //        // 登録
        //        if (this.AddPoint() == true)
        //        {
        //            // 再描画
        //            this.SetDesignCheckDetailList();
        //        }
        //    });
        //}
        //Delete End 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
        #endregion

        //Delete Start 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region 新しい指摘を追加する
        ///// <summary>
        ///// 新しい指摘を追加する
        ///// </summary>
        ///// <returns></returns>
        //private bool AddPoint()
        //{
        //    //設計チェック詳細のチェック
        //    if (this.IsEntryDesignCheckDetail() == false)
        //    {
        //        return false;
        //    }

        //    // 登録対象の取得
        //    var list = this.GetNewPoint();

        //    //登録対象があるかどうか
        //    if (list == null || list.Any() == false)
        //    {
        //        return false;
        //    }

        //    //設計チェック詳細登録
        //    var res = HttpUtil.PostResponse<DesignCheckPointPostInModel, DesignCheckPointPostOutModel>(ControllerType.DesignCheckPoint, list);

        //    //レスポンスが取得できたかどうか
        //    if (res == null || res.Status != Const.StatusSuccess)
        //    {
        //        return false;
        //    }

        //    //再読込フラグ
        //    this.IsRefresh = true;

        //    return true;
        //}
        #endregion
        //Delete End 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)

        #region データの取得
        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheckList()
        {
            var cond = new DesignCheckGetInModel
            {
                // 開催日ID
                ID = this.DesignCheck.ID
            };

            return this.GetDesignCheckList(cond);
        }

        /// <summary>
        /// 設計チェックの取得
        /// </summary>
        /// <param name="cond">検索条件</param>
        /// <returns></returns>
        private DesignCheckGetOutModel GetDesignCheckList(DesignCheckGetInModel cond)
        {
            var list = new List<DesignCheckGetOutModel>();

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckGetInModel, DesignCheckGetOutModel>(ControllerType.DesignCheck, cond);

            //レスポンスが取得できたかどうか
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);
            }

            return list?.FirstOrDefault();
        }

        /// <summary>
        /// 設計チェック詳細の取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointGetOutModel> GetDesignCheckDetailList()
        {
            //パラメータ設定
            var cond = new DesignCheckPointGetInModel
            {
                // 開催日_ID
                開催日_ID = (int)this.DesignCheck.ID,

                //Delete Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                // ステータス
                //OPEN_FLG = true,
                //Delete End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                // 最新フラグ
                NEW_FLG = true
            };

            //設計チェック詳細検索条件
            this.ListSearchCond = cond;

            //APIで取得
            var res = HttpUtil.GetResponse<DesignCheckPointGetInModel, DesignCheckPointGetOutModel>(ControllerType.DesignCheckPoint, cond);

            //レスポンスが取得できたかどうか
            var list = new List<DesignCheckPointGetOutModel>();
            if (res != null && res.Status == Const.StatusSuccess)
            {
                list.AddRange(res.Results);

            }

            return list;

        }

        #endregion

        //Append Start 2021/07/07 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region 指摘NOの値をstring型に変換する
        /// <summary>
        /// nullならnullを返す
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ValueOf(object obj)
        {
            return (obj == null) ? null : obj.ToString();
        }
        #endregion
        //Append End 2021/07/07 張晋華 開発計画表設計チェック機能改修(保守対応)

        //Append Start 2021/07/08 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region 対象車両の状況チェック
        /// <summary>
        /// 対象車両の状況チェック
        /// </summary>
        public bool SituationCheck()
        {
            var RowList = this.PointMultiRow.Rows.Where(x => !string.IsNullOrEmpty(ValueOf(x.Cells["NoTextBoxColumn"].Value)));

            foreach (var row in RowList)
            {
                var check = false;

                foreach (var car in Factory.MultiRowContoller.CarModelList)
                {
                    //状況記号
                    var symbol = Convert.ToString(row.Cells[string.Format("Car_{0}", car.ID.ToString())].Value);

                    if (!string.IsNullOrEmpty(symbol))
                    {
                        check = true;
                    }
                }

                if (!check)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        //Append End 2021/07/08 張晋華 開発計画表設計チェック機能改修(保守対応)

        #region 設計チェック詳細の登録
        /// <summary>
        /// 設計チェック詳細の登録
        /// </summary>
        /// <returns></returns>
        private bool EntryDesignCheckDetail()
        {
            //登録対象が無ければ終了
            if (this.PointMultiRow.RowCount <= 0)
            {
                Messenger.Warn(Resources.KKM03045);
                return false;
            }
            //Append Start 2021/07/08 張晋華 開発計画表設計チェック機能改修(保守対応)
            else
            {
                //対象車両の状況チェック
                var check = SituationCheck();

                if (!check)
                {
                    //状況記号が設定されてないと、指摘登録ができない
                    Messenger.Warn(Resources.KKM03049);
                    return false;
                }
            }
            //Append End 2021/07/08 張晋華 開発計画表設計チェック機能改修(保守対応)

            //設計チェック詳細のチェック
            if (this.IsEntryDesignCheckDetail() == false)
            {
                return false;
            }

            // 登録対象の取得
            var list = this.GetEntryDesignCheckDetail();

            //登録対象があるかどうか
            if (list == null || list.Any() == false)
            {
                return false;
            }

            //設計チェック詳細登録
            var res = HttpUtil.PostResponse<DesignCheckPointPostInModel, DesignCheckPointPostOutModel>(ControllerType.DesignCheckPoint, list);

            //レスポンスが取得できたかどうか
            if (res == null || res.Status != Const.StatusSuccess)
            {
                return false;
            }

            //登録後メッセージ
            Messenger.Info(Resources.KKM00002);

            // 再読込フラグ
            this.IsRefresh = true;

            return true;
        }
        #endregion

        //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region 新しい行の開催日_IDと編集者_IDの取得(IDは仮発行)
        /// <summary>
        /// 新しい行の開催日_IDと編集者_IDの取得(IDは仮発行)
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointPostInModel> GetNewPoint()
        {
            var pointList = new List<DesignCheckPointPostInModel>();

            var data = new DesignCheckPointPostInModel();


            // 指摘を追加
            data = new DesignCheckPointPostInModel()
            {
                ID = DesignCheckDetailListForm.TEMP_POINT_ID,
                // 開催日ID
                開催日_ID = (int)this.DesignCheck.ID,
                // 編集者_ID
                編集者_ID = SessionDto.UserId,
            };

            // 登録データの追加
            pointList.Add(data);

            return pointList;
        }
        #endregion
        //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

        #region 登録する設計チェック詳細を取得
        /// <summary>
        /// 登録する設計チェック詳細を取得
        /// </summary>
        /// <returns></returns>
        private List<DesignCheckPointPostInModel> GetEntryDesignCheckDetail()
        {
            var pointList = new List<DesignCheckPointPostInModel>();

            var data = new DesignCheckPointPostInModel();

            foreach (Row row in this.PointMultiRow.Rows)
            {
                // 指摘を追加
                data = new DesignCheckPointPostInModel()
                {
                    // 指摘ID
                    ID = (int?)row.Cells["IDTextBoxColumn"].Value,
                    // 開催日ID
                    開催日_ID = (int)this.DesignCheck.ID,
                    // 指摘NO
                    指摘NO = (int?)row.Cells["NoTextBoxColumn"]?.Value > 0 ? (int?)row.Cells["NoTextBoxColumn"]?.Value : (int?)null,

                    //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                    // FLAG_CLOSE
                    FLAG_CLOSE = (int?)row.Cells["StatusComboBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                    //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                    // 指摘部品
                    部品 = (string)row.Cells["PartsTextBoxColumn"]?.Value,
                    // 状況
                    状況 = (string)row.Cells["SituationTextBoxColumn"]?.Value,
                    // FLAG_処置不要
                    FLAG_処置不要 = (int?)row.Cells["TreatmentCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                    // 処置課
                    処置課 = (string)row.Cells["TreatmentSectionTextBoxColumn"]?.Value,
                    // 処置対象
                    処置対象 = (string)row.Cells["TreatmentTargetTextBoxColumn"]?.Value,
                    // 処置方法
                    処置方法 = (string)row.Cells["TreatmentHowTextBoxColumn"]?.Value,
                    // FLAG_調整済
                    FLAG_調整済 = (int?)row.Cells["TreatmentOKCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                    // 処置調整
                    処置調整 = (string)row.Cells["TreatmentWhoTextBoxColumn"]?.Value,
                    // 織込日程
                    織込日程 = (DateTime?)row.Cells["TreatmentWhenCalendarColumn"]?.Value,
                    // FLAG_試作改修
                    FLAG_試作改修 = (int?)row.Cells["RepairCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                    // 部品納入日
                    部品納入日 = (DateTime?)row.Cells["PartsGetDayCalendarColumn"]?.Value,
                    // FLAG_上司承認（非表示）
                    FLAG_上司承認 = (int?)row.Cells["ApprovalOKCheckBoxColumn"]?.Value == 1 ? 1 : (int?)null,
                    // 担当課名
                    担当課名 = (string)row.Cells["SectionTextBoxColumn"]?.Value,
                    // 担当課_ID
                    担当課_ID = null,
                    // 担当者_ID
                    担当者_ID = Factory.StaffController.GetId(row),
                    // 担当者_TEL
                    担当者_TEL = (string)row.Cells["UserTelTextBoxColumn"]?.Value,
                    // 編集者_ID
                    編集者_ID = SessionDto.UserId,
                };

                // 指摘ID
                var pointId = Convert.ToInt32(row.Cells["IDTextBoxColumn"].Value);

                foreach (var car in Factory.MultiRowContoller.CarModelList)
                {
                    // 状況記号
                    var symbol = Convert.ToString(row.Cells[string.Format("Car_{0}", car.ID.ToString())].Value);

                    // 状況記号が設定されている、または状況記号が設定されていなくても状況が既に登録済の場合対象とする
                    if (string.IsNullOrEmpty(symbol) == false || Factory.MultiRowContoller.IsRegisteredProgress(pointId, car.ID))
                    {
                        // 登録対象を追加
                        data.PROGRESS_LIST.Add(new DesignCheckProgressPostInModel()
                        {
                            // 開催日ID
                            開催日_ID = (int)this.DesignCheck.ID,
                            // 対象車両ID
                            対象車両_ID = car.ID,
                            // 指摘ID
                            指摘_ID = pointId,
                            // 状況
                            状況 = symbol,
                            // 完了確認日（非表示）
                            完了確認日 = Factory.MultiRowContoller.PointModelList.FirstOrDefault((x) => x.ID == pointId && x.対象車両_ID == car.ID)?.完了確認日,
                        });
                    }
                }

                // 登録データの追加
                pointList.Add(data);
            }

            return pointList;
        }
        #endregion

        #region 設計チェック詳細のチェック
        /// <summary>
        /// 設計チェック詳細のチェック
        /// </summary>
        /// <returns></returns>
        private bool IsEntryDesignCheckDetail()
        {
            //入力がOKかどうか
            var msg = Validator.GetFormInputErrorMessage(this);
            if (msg != "")
            {
                Messenger.Warn(msg);
                return false;
            }

            //設計チェックが存在しているかどうか
            if (this.GetDesignCheckList(new DesignCheckGetInModel { ID = this.ListSearchCond.開催日_ID }) == null)
            {
                //存在していない場合はエラー
                Messenger.Warn(Resources.KKM00021);
                return false;
            }

            return true;

        }
        #endregion

        #region MultiRowを制御するクラス
        /// <summary>
        /// MultiRowを制御するクラス
        /// </summary>
        private class MultiRowContoller : BaseMultiRowContoller
        {
            #region 内部変数

            private GcMultiRow _mlr;
            private List<DesignCheckPointGetOutModel> _PointModelList;
            private List<DesignCheckProgressSymbolGetOutModel> _ProgressSymbolList;
            private Authority _Auth;
            private int _holdDayId;

            /// <summary>
            /// イベントの処理が繰り返さないようにコードによるスクロールかどうかをフラグで判別する
            /// </summary>
            private bool _scrollByEvent = false;

            /// <summary>
            /// 吹き出しメッセージのX座標
            /// </summary>
            private int _beforeMsgPictureX;

            /// <summary>
            /// セル幅のオフセット
            /// </summary>
            private int _resizeOffset;

            /// <summary>
            /// カスタムテンプレート
            /// </summary>
            private CustomTemplate CustomTemplate = new CustomTemplate();

            //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
            /// <summary>
            /// カスタムテンプレートセルスタイル
            /// </summary>
            private static CustomMultiRowCellStyle customMultiRowCellStyle = new CustomMultiRowCellStyle();
            //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

            #endregion

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region 列情報
            /// <summary>
            /// 列情報
            /// </summary>
            private List<ColInfo> ColInfos = new List<ColInfo>()
            {
                // 選択チェック
                new ColInfo() { Index = 0, RowCellName = "TargetCheckBoxColumn", Visible = true, HeaderName = "ch_Target" },

                // 指摘No
                new ColInfo() { Index = 1, RowCellName = "NoTextBoxColumn", Visible = true, HeaderName = "ch_No" },

                // ステータス
                new ColInfo() { Index = 2, RowCellName = "StatusComboBoxColumn", Visible = true, HeaderName = "ch_Status" },

                // 試作ステータス
                new ColInfo() { Index = 3, RowCellName = "MgrStatusComboBoxColumn", Visible = true, HeaderName = "ch_MgrStatus" },

                // 履歴
                new ColInfo() { Index = 4, RowCellName = "HistoryLinkColumn", Visible = true, HeaderName = "ch_History" },

                // 指摘部品
                new ColInfo() { Index = 5, RowCellName = "PartsTextBoxColumn", Visible = true, HeaderName = "ch_Parts" },

                // 状況
                new ColInfo() { Index = 6, RowCellName = "SituationTextBoxColumn", Visible = true, HeaderName = "ch_Situation" },

                // 処置しないチェック
                new ColInfo() { Index = 7, RowCellName = "TreatmentCheckBoxColumn", Visible = true, HeaderName = "ch_Treatment" },

                // [処置内容]どこの部署が?
                new ColInfo() { Index = 8, RowCellName = "TreatmentSectionTextBoxColumn", Visible = true, HeaderName = "ch_TreatmentSection" },

                // [処置内容]何を
                new ColInfo() { Index = 9, RowCellName = "TreatmentTargetTextBoxColumn", Visible = true, HeaderName = "ch_TreatmentTarget" },

                // [処置内容]どのように
                new ColInfo() { Index = 10, RowCellName = "TreatmentHowTextBoxColumn", Visible = true, HeaderName = "ch_TreatmentHow" },

                // [処置内容]調整:済
                new ColInfo() { Index = 11, RowCellName = "TreatmentOKCheckBoxColumn", Visible = true, HeaderName = "ch_TreatmentOK" },

                // [処置内容]誰と
                new ColInfo() { Index = 12, RowCellName = "TreatmentWhoTextBoxColumn", Visible = true, HeaderName = "ch_TreatmentWho" },

                // [処置内容]いつまでに
                new ColInfo() { Index = 13, RowCellName = "TreatmentWhenCalendarColumn", Visible = true, HeaderName = "ch_TreatmentWhen" },

                // 要試作改修
                new ColInfo() { Index = 14, RowCellName = "RepairCheckBoxColumn", Visible = true, HeaderName = "ch_Repair" },

                // 部品納入日
                new ColInfo() { Index = 15, RowCellName = "PartsGetDayCalendarColumn", Visible = true, HeaderName = "ch_PartsGetDay" },

                // 担当課
                new ColInfo() { Index = 16, RowCellName = "SectionTextBoxColumn", Visible = true, HeaderName = "ch_Section" },

                // 担当者
                new ColInfo() { Index = 17, RowCellName = "UserTextBoxColumn", Visible = true, HeaderName = "ch_User" },

                // 担当者TEL
                new ColInfo() { Index = 18, RowCellName = "UserTelTextBoxColumn", Visible = true, HeaderName = "ch_UserTel" },

                // 以下非表示列

                // 開催日_ID
                new ColInfo() { Index = 19, RowCellName = "OpenDayIDColumn", Visible = false ,HeaderName = "columnHeaderCell17" },

                // 担当者_ID
                new ColInfo() { Index = 20, RowCellName = "UserIDTextBoxColumn", Visible = false ,HeaderName = "columnHeaderCell18" },

                // 履歴数
                new ColInfo() { Index = 21, RowCellName = "HistoryCountColumn", Visible = false ,HeaderName = "columnHeaderCell19" },

                // 指摘ID
                new ColInfo() { Index = 22, RowCellName = "IDTextBoxColumn", Visible = false ,HeaderName = "columnHeaderCell20" },

                // 担当課長承認
                new ColInfo() { Index = 23, RowCellName = "ApprovalOKCheckBoxColumn", Visible = false, HeaderName = "columnHeaderCell21" },
            };
            #endregion
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            #region 公開プロパティ

            /// <summary>
            /// データソースの元になる内部保持の設計チェック指摘モデルリスト
            /// </summary>
            public List<DesignCheckPointGetOutModel> PointModelList
            {
                get
                {
                    return this._PointModelList;
                }

                set
                {
                    this._PointModelList = value;
                }
            }

            /// <summary>
            /// 対象車モデルリスト
            /// </summary>
            public List<DesignCheckCarGetOutModel> CarModelList { get; private set; }

            /// <summary>
            /// 状況管理クラス
            /// </summary>
            public ProgressController ProgressController { get; set; }

            /// <summary>
            /// オープン値
            /// </summary>
            public const int StatusOpen = 0;

            /// <summary>
            /// クローズ値
            /// </summary>
            public const int StatusClose = 1;

            //Append Start 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)
            /// <summary>
            /// ステータス値と表示文言の組み合わせ
            /// </summary>
            public readonly Dictionary<int?, string> StatusMap = new Dictionary<int?, string>
            {
                { StatusOpen, "OPEN" },
                { StatusClose, "CLOSE" }
            };
            /// <summary>
            /// ステータス値と表示文言の組み合わせ
            /// </summary>
            public readonly Dictionary<int?, string> StatusMap2 = new Dictionary<int?, string>
            {
                { StatusOpen, "未" },
                { StatusClose, "済" }
            };
            //Append End 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)

            /// <summary>
            /// 吹き出しメッセージ
            /// </summary>
            public PictureBox MsgPicture { get; set; }

            #endregion

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mlr"></param>
            /// <param name="rowCountLabel">行カウント表示ラベル</param>
            /// <param name="auth"></param>
            /// <param name="holdDayId">開催日ID</param>
            public MultiRowContoller(GcMultiRow mlr, Label rowCountLabel, Authority auth, int holdDayId) : base(mlr)
            {
                this._mlr = mlr;
                this._PointModelList = new List<DesignCheckPointGetOutModel>();
                _Auth = auth;
                _holdDayId = holdDayId;

                // 対象車の取得
                CarModelList = GetDesignCheckCarList();

                // 状況クラス生成
                ProgressController = new ProgressController(_mlr, CarModelList);

                // 状況記号の取得
                _ProgressSymbolList = (HttpUtil.GetResponse<DesignCheckProgressSymbolGetInModel, DesignCheckProgressSymbolGetOutModel>
                    (ControllerType.DesignCheckProgressSymbol, new DesignCheckProgressSymbolGetInModel())?.Results).ToList();
                _ProgressSymbolList.FindAll((x) => x.ID != 0).ForEach((x) => x.説明 = string.Format("   {0}        ： {1}", x.記号, x.説明));

                // カスタムテンプレート設定
                this.CustomTemplate.ColumnHeaderHeight = 70;
                this.CustomTemplate.RowCountLabel = rowCountLabel;
                this.CustomTemplate.MultiRow = this._mlr;

                // スクロール設定
                this._mlr.MouseWheelCount = new GridAppConfigAccessor().GetGridMouseWheelCount();
                this._mlr.VerticalScrollCount = this._mlr.MouseWheelCount;

                // ダブルバッファリング有効
                this._mlr.GetType().InvokeMember(
                   "DoubleBuffered",
                   System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                   null,
                   this._mlr,
                   new object[] { true });

                // イベント登録
                _mlr.Scroll += _mlr_Scroll;
                _mlr.CellResizeCompleting += _mlr_CellResizeCompleting;
                _mlr.CellResizeCompleted += _mlr_CellResizeCompleted;
            }
            #endregion

            #region 未選択状態にします。
            /// <summary>
            /// 未選択状態にします。
            /// </summary>
            public void Unselected()
            {
                this._mlr.CurrentCell = null;
            }
            #endregion

            #region 選択チェックボックスをONにします。
            /// <summary>
            /// 選択チェックボックスをONにします。
            /// </summary>
            /// <param name="index"></param>
            public void SetTargetOn(int index)
            {
                this._mlr.Rows[index].Cells["TargetCheckBoxColumn"].Value = true;
            }
            #endregion

            #region 選択チェックボックスをOFFにします。
            /// <summary>
            /// 選択チェックボックスをOFFにします。
            /// </summary>
            /// <param name="index"></param>
            public void SetTargetOff(int index)
            {
                this._mlr.Rows[index].Cells["TargetCheckBoxColumn"].Value = false;
            }
            #endregion

            #region 選択チェックボックスを全行OFFにします。
            /// <summary>
            /// 選択チェックボックスを全行OFFにします。
            /// </summary>
            public void SetTargetAllOff()
            {
                for (var i = 0; i < _mlr.RowCount; i++)
                {
                    SetTargetOff(i);
                }
            }
            #endregion

            #region 行選択状態にします。
            /// <summary>
            /// 行選択状態にします。
            /// </summary>
            /// <param name="index"></param>
            public void SelectRow(int index)
            {
                this._mlr.Rows[index].Selected = true;
            }
            #endregion

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region 最終指摘行Indexを取得します。
            /// <summary>
            /// 最終指摘行Indexを取得します。
            /// </summary>
            public int GetLastPointRowIndex()
            {
                for (var i = 1; i < _mlr.RowCount; i++)
                {
                    if (IsFirstPointRow(_mlr.RowCount - i))
                    {
                        return _mlr.RowCount - i;
                    }
                }

                return 0;
            }
            #endregion
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region 該当行がその指摘Noの最初の行か？
            /// <summary>
            /// 該当行がその指摘Noの最初の行か？
            /// </summary>
            /// <param name="rowIndex"></param>
            /// <returns></returns>
            public bool IsFirstPointRow(int rowIndex)
            {
                if (rowIndex == 0)
                {
                    return true;
                }

                var now = this._mlr.Rows[rowIndex].Cells["IDTextBoxColumn"].Value;
                var bef = this._mlr.Rows[rowIndex - 1].Cells["IDTextBoxColumn"].Value;

                return Convert.ToString(now) != Convert.ToString(bef);
            }
            #endregion
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region 対象行までスクロールします。
            /// <summary>
            /// 対象行までスクロールします。
            /// </summary>
            public void ScrollRow(int index)
            {
                if (this._mlr.RowCount > 0)
                {
                    CellPosition newPosition = new CellPosition(index, this._mlr.FirstDisplayedCellPosition.CellIndex);

                    this._mlr.FirstDisplayedCellPosition = newPosition;
                }
            }
            #endregion
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            #region 最終行を行選択状態にします。
            /// <summary>
            /// 最終行を行選択状態にします。
            /// </summary>
            public void SelectEndRow()
            {
                if (this._mlr.RowCount > 0)
                {
                    this.SelectRow(this._mlr.RowCount - 1);
                }
            }
            #endregion

            #region 最終行の選択チェックボックスをONにします。
            /// <summary>
            /// 最終行の選択チェックボックスをONにします。
            /// </summary>
            public void SetTargetEndRow()
            {
                if (this._mlr.RowCount > 0)
                {
                    SetTargetOn(_mlr.RowCount - 1);
                }
            }
            #endregion

            #region 最終行までスクロールします。
            /// <summary>
            /// 最終行までスクロールします。
            /// </summary>
            public void ScrollEndRow()
            {
                if (this._mlr.RowCount > 0)
                {
                    CellPosition newPosition = new CellPosition(this._mlr.RowCount - 1, this._mlr.FirstDisplayedCellPosition.CellIndex);

                    this._mlr.FirstDisplayedCellPosition = newPosition;
                }
            }
            #endregion

            #region データソースにバインドします。
            /// <summary>
            /// データソースにバインドします。
            /// </summary>
            public void Bind()
            {
                // 表示位置取得
                SaveScrollPoint();

                // 指摘モデルデータソース取得
                var target = new List<DesignCheckPointGetOutModel>();
                var sitekiid = 0;
                foreach (var data in _PointModelList)
                {
                    // 削除行はバインド対象外
                    if (data.DELETE_FLG == true) continue;

                    // 同じ指摘IDは跳ばす
                    if (sitekiid == data.ID) continue;

                    sitekiid = data.ID.Value;

                    //Append Start 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)
                    data.FLAG_CLOSE = data.FLAG_CLOSE == 1 ? 1 : 0;
                    //Append End 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)

                    target.Add(data);
                }

                // 画面から状況を取得
                ProgressController.Set(_PointModelList);

                // テンプレート設定
                SetTemplate();

                // データバインド
                this.CustomTemplate.SetDataSource(target);

                // 行の高さ調整
                this._mlr.Rows.ToList().ForEach((x) => x.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit());

                //一覧を未選択状態に設定
                _mlr.CurrentCell = null;

                // 表示件数ラベル更新
                this.CustomTemplate.SetCountLabel();

                // ソート反映
                this.CustomTemplate.Sort = this.CustomTemplate.Sort;

                //グリッドリフレッシュ
                _mlr.Refresh();

                //Append Start 2021/07/29 杉浦 設計チェックインポート
                //グリッドの表示周りの編集
                AdjustDesignCheckDetailList();
                //Append End 2021/07/29 杉浦 設計チェックインポート

                // 元の表示位置までスクロール                
                LoadScrollPoint();
            }
            #endregion

            #region チェックボックスで選択している行に対応する設計チェック指摘モデルリストを取得します。
            /// <summary>
            /// チェックボックスで選択している行に対応する設計チェック指摘モデルリストを取得します。
            /// </summary>
            /// <returns></returns>
            public List<DesignCheckPointGetOutModel> GetSelectedModels()
            {
                return GetSelectedRows().Select((x) => GetModel(x)).ToList();
            }
            #endregion

            #region チェックボックスで選択している行リストを取得します。
            /// <summary>
            /// チェックボックスで選択している行リストを取得します。
            /// </summary>
            /// <returns></returns>
            public List<Row> GetSelectedRows()
            {
                return
                    this._mlr.Rows
                    .Cast<Row>()
                    .Where((x) => Convert.ToBoolean(x.Cells["TargetCheckBoxColumn"].Value) == true)
                    .ToList();
            }
            #endregion

            #region 行に対応する設計チェック指摘モデルを取得します。
            /// <summary>
            /// 行に対応する設計チェック指摘モデルを取得します。
            /// </summary>
            /// <param name="rowIndex">行番号</param>
            /// <returns></returns>
            public DesignCheckPointGetOutModel GetModel(int rowIndex)
            {
                return this.GetModel(this._mlr.Rows[rowIndex]);

            }

            /// <summary>
            /// 行に対応する設計チェック指摘モデルを取得します。
            /// </summary>
            /// <param name="row">行</param>
            /// <returns></returns>
            public DesignCheckPointGetOutModel GetModel(Row row)
            {
                var id = Convert.ToInt32(row.Cells["IDTextBoxColumn"].Value);

                return this._PointModelList.First(x => x.ID == id);

            }
            #endregion

            #region 行の編集許可制御を設定します。
            /// <summary>
            /// その行の編集許可制御を設定します。
            /// </summary>
            public void SetRowEnable(Row row)
            {
                //Append Start 2021/07/27 張晋華 開発計画表設計チェック機能改修(保守対応)
                //管理者でない場合→ステータス欄が編集不可になる
                foreach (var col in this.ColInfos.Where(x => x.RowCellName == "StatusComboBoxColumn"))
                {
                    if (!_Auth.IsAdmin())
                    {
                        row.Cells[col.RowCellName].ReadOnly = true;
                    }
                }
                //Append End 2021/07/27 張晋華 開発計画表設計チェック機能改修(保守対応)

                //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                foreach (var col in this.ColInfos.Where(x => x.RowCellName != "StatusComboBoxColumn"))
                {
                    row.Cells[col.RowCellName].ReadOnly =
                        IsReadOnly(
                            Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value),
                            col.RowCellName,
                            //Update Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                            //Convert.ToString(row.Cells["SectionTextBoxColumn"].Value));
                            Convert.ToString(row.Cells["SectionTextBoxColumn"].Value),
                            Convert.ToString(row.Cells["MgrNoTextBoxColumn"].Value));
                    //Update End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                }
                //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                var point = GetModel(row);

                //Delete Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                //foreach (var col in _mlr.Columns)
                //{
                //row.Cells[col.Name].ReadOnly = _Auth.IsCan(col.Name, point.担当課名) == false;
                //}
                //Delete End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                //Update Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                // 試験車列
                foreach (var car in CarModelList)
                {
                    //row.Cells[string.Format("Car_{0}", car.ID.ToString())].ReadOnly = _Auth.IsCan("TestCarColumns", point.担当課名) == false;
                    row.Cells[string.Format("Car_{0}", car.ID.ToString())].ReadOnly =
                        IsReadOnly(
                            Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value),
                            string.Format("Car_{0}", car.ID.ToString()),
                            //Update Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                            //Convert.ToString(row.Cells["SectionTextBoxColumn"].Value));
                            Convert.ToString(row.Cells["SectionTextBoxColumn"].Value),
                            Convert.ToString(row.Cells["MgrNoTextBoxColumn"].Value));
                    //Update End 2021/07/29 杉浦 開発計画表設計チェック機能改修
                }
                //Update End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                //Append Start 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)
                row.Cells["MgrStatusComboBoxColumn"].ReadOnly = true;
                //Append End 2021/06/30 張晋華 開発計画表設計チェック機能改修(保守対応)

                //Update Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
                row.Cells["TargetCheckBoxColumn"].ReadOnly = _Auth.IsCan("TargetCheckBoxColumn", Convert.ToString(row.Cells["TargetCheckBoxColumn"].Value)) == false;
                //Update End 2021/07/29 杉浦 開発計画表設計チェック機能改修
            }

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            /// <summary>
            /// ステータス変更による同一指摘行の編集許可制御を設定します。
            /// </summary>
            public void ChangeStatusEnable(Row row)
            {
                var statusValue = Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value);

                foreach (var rw in this.GetSamePointRows(row))
                {
                    // 背景色
                    SetRowBackColor(rw, statusValue);

                    foreach (var col in this.ColInfos.Where(x => x.RowCellName != "StatusComboBoxColumn"))
                    {
                        //Update Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
                        //rw.Cells[col.RowCellName].ReadOnly = IsReadOnly(statusValue, col.RowCellName, Convert.ToString(rw.Cells["SectionTextBoxColumn"].Value));
                        rw.Cells[col.RowCellName].ReadOnly = IsReadOnly(statusValue, col.RowCellName, Convert.ToString(rw.Cells["SectionTextBoxColumn"].Value), Convert.ToString(rw.Cells["MgrNoTextBoxColumn"].Value));
                        //Update End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                    }
                }
            }
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            #endregion

            #region 保存してあるスクロール位置を設定します。
            /// <summary>
            /// 保存してあるスクロール位置を設定します。
            /// </summary>
            public override void LoadScrollPoint()
            {
                _scrollByEvent = true;

                base.LoadScrollPoint();

                _scrollByEvent = false;
            }
            #endregion

            #region 状況が登録されているか？
            /// <summary>
            /// 状況が登録されているか？
            /// </summary>
            /// <param name="pointId"></param>
            /// <param name="carId"></param>
            /// <returns></returns>
            public bool IsRegisteredProgress(int pointId, int carId)
            {
                // 仮指摘IDの場合は未登録
                if (pointId < 0) return false;

                return _PointModelList.FirstOrDefault((x) => x.ID == pointId && x.対象車両_ID == carId) != null;
            }
            #endregion

            //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region ステータスによる背景色を行に設定します。
            /// <summary>
            /// ステータスによる背景色を行に設定します。
            /// </summary>
            /// <param name="row"></param>
            /// <param name="statusValue"></param>
            public void SetRowBackColor(Row row, int statusValue)
            {
                row.Cells
                    .Where((x) => x.Name != "RowHeader").ToList()
                    .ForEach((x) =>
                    {
                        x.Style.BackColor = statusValue == StatusClose ? Color.LightGray : customMultiRowCellStyle.DataCellStyle.BackColor;
                    });
            }
            #endregion
            //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

            //Append Start 2021/07/29 杉浦 設計チェックインポート
            #region 設計チェック指摘一覧調整
            /// <summary>
            /// 設計チェック指摘一覧調整
            /// </summary>
            private void AdjustDesignCheckDetailList()
            {
                foreach (Row row in this._mlr.Rows)
                {
                    int pointId = Convert.ToInt32(row.Cells["IDTextBoxColumn"].Value);

                    // 試験車
                    foreach (var car in CarModelList)
                    {
                        row.Cells[string.Format("Car_{0}", car.ID.ToString())].Value = ProgressController.GetProgSymbol(pointId, car.ID);
                    }

                    // 編集許可制御を設定
                    SetRowEnable(row);

                    //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                    // 履歴が無い場合は履歴はリンクを解除
                    if (Convert.ToInt32(row.Cells["HistoryCountColumn"].Value) <= 0)
                    {
                        row.Cells["HistoryLinkColumn"].Value = NoneText;

                        var linkCell = row.Cells["HistoryLinkColumn"] as LinkLabelCell;
                        linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                        linkCell.LinkColor = Color.Black;
                    }

                    // 背景色設定
                    SetRowBackColor(row, Convert.ToInt32(row.Cells["StatusComboBoxColumn"].Value));
                    //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                }
            }
            #endregion
            //Append End 2021/07/29 杉浦 設計チェックインポート

            //Append Start 2021/08/20 杉浦 設計チェック請負
            #region 設計チェック指摘一覧高さ調整
            /// <summary>
            /// 設計チェック指摘一覧調整
            /// </summary>
            public void AdjustmentRowsVertical()
            {
                foreach (var row in this._mlr.Rows)
                {
                    var lengthList = new List<DesignCheckLengthCompareModel>();
                    lengthList.Add(new DesignCheckLengthCompareModel { name = "TreatmentTargetTextBoxColumn", size = row.Cells["TreatmentTargetTextBoxColumn"].Value.ToString().Length });
                    lengthList.Add(new DesignCheckLengthCompareModel { name = "TreatmentHowTextBoxColumn", size = row.Cells["TreatmentHowTextBoxColumn"].Value.ToString().Length });

                    var maxIdx = lengthList.Select((val, idx) => new { V = val.size, I = idx }).Aggregate((max, working) => (max.V > working.V) ? max : working).I;

                    row.Cells[lengthList[maxIdx].name].PerformVerticalAutoFit();
                }
            }
            #endregion
            //Append End 2021/08/20 杉浦 設計チェック請負

            #region 内部メソッド
            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region ステータスと権限から読み取り専用の判定
            //Update Start 2021/07/29 杉浦 開発計画表設計チェック機能改修
            /// <summary>
            /// ステータスと権限から読み取り専用か判定します。
            /// </summary>
            /// <param name="statusValue"></param>
            /// <param name="cellName"></param>
            /// <param name="sectionName"></param>
            /// <param name="MgrNo"></param>
            /// <returns></returns>
            //private bool IsReadOnly(int statusValue, string cellName, string sectionName)
            //{
            //    // ステータスがクローズならステータス以外の列は編集不可、オープンなら権限に従う
            //    return statusValue == StatusClose ? true : _Auth.IsCan(cellName, sectionName) == false;
            //}
            private bool IsReadOnly(int statusValue, string cellName, string sectionName, string MgrNo)
            {
                // ステータスがクローズならステータス以外の列は編集不可、オープンなら権限に従う
                return !string.IsNullOrEmpty(MgrNo) ? true : statusValue == StatusClose ? true : _Auth.IsCan(cellName, sectionName) == false;
            }
            //Update End 2021/07/29 杉浦 開発計画表設計チェック機能改修

            /// <summary>
            /// 対象行と同じ指摘IDの行を取得します。
            /// </summary>
            /// <param name="row"></param>
            /// <returns></returns>
            private IEnumerable<Row> GetSamePointRows(Row row)
            {
                return this._mlr.Rows.Where((x) => x.Cells["IDTextBoxColumn"].Value.ToString() == row.Cells["IDTextBoxColumn"].Value.ToString());
            }
            #endregion
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            #region 対象車の取得
            /// <summary>
            /// 対象車の取得
            /// </summary>
            /// <returns></returns>
            private List<DesignCheckCarGetOutModel> GetDesignCheckCarList()
            {
                var list = new List<DesignCheckCarGetOutModel>();

                var cond = new DesignCheckCarGetInModel
                {
                    // 開催日ID
                    開催日_ID = _holdDayId
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
            #endregion

            #region テンプレートに車列を追加して設定します。
            /// <summary>
            /// テンプレートに車列を追加して設定します。
            /// </summary>
            private void SetTemplate()
            {
                var scl = new DeviceUtil().GetScalingFactor();
                var tmp = new DesignCheckDetailListMultiRowTemplate();
                var cellSize = new Size(42, 20);
                var border = new CustomMultiRowCellStyle().DataCellStyle.Border;

                var beforeCell = tmp.Row.Cells["UserTelTextBoxColumn"];
                var beforeCol = tmp.ColumnHeaders[0].Cells["ch_UserTel"];
                foreach (var car in CarModelList)
                {
                    // 行セルの追加（Nameは対象車両IDにする）
                    ComboBoxCell newCell = new ComboBoxCell();
                    newCell.Name = string.Format("Car_{0}", car.ID.ToString());
                    newCell.Location = new Point(beforeCell.Right, newCell.Top);
                    newCell.MinimumSize = cellSize;
                    newCell.MaximumSize = new Size(52, 0);
                    newCell.Size = cellSize;
                    newCell.Style.Border = border;
                    newCell.Style.TextAlign = MultiRowContentAlignment.MiddleLeft;
                    newCell.Style.WordWrap = MultiRowTriState.False;
                    newCell.Style.Multiline = MultiRowTriState.False;
                    newCell.ValueMember = "記号";
                    newCell.DisplayMember = "説明";
                    newCell.DropDownWidth = Convert.ToInt32(330 * scl);
                    newCell.DataSource = _ProgressSymbolList;
                    newCell.ShowDropDownButton = CellButtonVisibility.ShowForCurrentCell;
                    newCell.TabIndex = beforeCell.TabIndex + 1;
                    tmp.Row.Cells.Add(newCell);

                    // テンプレートの幅の変更
                    tmp.Width += newCell.Width;

                    // 列ヘッダの追加
                    ColumnHeaderCell newHeaderCell = new ColumnHeaderCell();
                    newHeaderCell.Name = string.Format("ch_Car{0}", car.ID.ToString());
                    newHeaderCell.Location = new Point(beforeCol.Right, newHeaderCell.Top);
                    newHeaderCell.MinimumSize = newCell.MinimumSize;
                    newHeaderCell.MaximumSize = newCell.MaximumSize;
                    newHeaderCell.Size = cellSize;
                    newHeaderCell.Value = car.試験車名.Replace(" ", Const.CrLf);

                    tmp.ColumnHeaders[0].Cells.Add(newHeaderCell);

                    // 退避
                    beforeCell = newCell;
                    beforeCol = newHeaderCell;
                }

                //Append Start 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)
                // ステータス
                var status = tmp.Row.Cells["StatusComboBoxColumn"] as ComboBoxCell;
                status.ValueMember = "Key";
                status.DisplayMember = "Value";
                status.DataSource = this.StatusMap.ToArray();

                // 試作部ステータス
                var Mgrstatus = tmp.Row.Cells["MgrStatusComboBoxColumn"] as ComboBoxCell;
                Mgrstatus.ValueMember = "Key";
                Mgrstatus.DisplayMember = "Value";
                Mgrstatus.DataSource = this.StatusMap2.ToArray();
                //Append End 2021/07/01 張晋華 開発計画表設計チェック機能改修(保守対応)

                // カスタム設定
                var templete = this.CustomTemplate.SetContextMenuTemplate(tmp);

                Color correspondenceColor = Color.Khaki;
                Color otherColor = Color.LightGray;
                Color foreColor = Color.Black;

                // 試験車列
                templete.ColumnHeaders[0].Cells.Where((x) => x.Name.Contains("ch_Car") && x is ColumnHeaderCell).ToList().ForEach((x) =>
                {
                    x.Style.BackColor = correspondenceColor;
                    x.Style.ForeColor = foreColor;
                    x.Style.Font = new Font("MS UI Gothic", 7, FontStyle.Regular);
                });

                // 処置内容列
                templete.ColumnHeaders[0].Cells.Where((x) => x.Name.Contains("ch_Treatment") && x.Name != "ch_Treatment" && x is ColumnHeaderCell).ToList().ForEach((x) =>
                {
                    x.Style.BackColor = otherColor;
                    x.Style.ForeColor = foreColor;
                });

                // 指摘NO
                templete.ColumnHeaders[0].Cells["ch_No"].Style.BackColor = Color.Black;

                // 選択列のフィルターアイテム設定
                var cellCheck = templete.ColumnHeaders[0].Cells["ch_Target"] as ColumnHeaderCell;
                cellCheck.DropDownContextMenuStrip.Items.RemoveAt(cellCheck.DropDownContextMenuStrip.Items.Count - 1);
                cellCheck.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("有", "無") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：処置しない
                var cellTreatment = templete.ColumnHeaders[0].Cells["ch_Treatment"] as ColumnHeaderCell;
                cellTreatment.DropDownContextMenuStrip.Items.RemoveAt(cellTreatment.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatment.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("処置しない", "処置する") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：調整：済
                var cellTreatmentOK = templete.ColumnHeaders[0].Cells["ch_TreatmentOK"] as ColumnHeaderCell;
                cellTreatmentOK.DropDownContextMenuStrip.Items.RemoveAt(cellTreatmentOK.DropDownContextMenuStrip.Items.Count - 1);
                cellTreatmentOK.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("済", "未") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // フィルターアイテム設定：要試作改修
                var cellRepair = templete.ColumnHeaders[0].Cells["ch_Repair"] as ColumnHeaderCell;
                cellRepair.DropDownContextMenuStrip.Items.RemoveAt(cellRepair.DropDownContextMenuStrip.Items.Count - 1);
                cellRepair.DropDownContextMenuStrip.Items.Add(new CheckBoxFilterItem("要", "否") { MaxCount = CustomTemplate.FilterItemMaxCount });

                // テンプレート設定
                this._mlr.Template = templete;
            }
            #endregion

            #region スクロールイベント
            /// <summary>
            /// スクロールイベント
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _mlr_Scroll(object sender, ScrollEventArgs e)
            {
                if (_scrollByEvent == false)
                {
                    if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    {
                        var tmpX = e.NewValue - e.OldValue;
                        MsgPicture.Location = new Point(MsgPicture.Location.X - tmpX, MsgPicture.Location.Y);
                    }
                }
            }
            #endregion

            #region セルリサイズ後イベント
            /// <summary>
            /// セルリサイズ後イベント
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _mlr_CellResizeCompleted(object sender, CellEventArgs e)
            {
                if (e.RowIndex == -1 && (e.CellIndex < 11 || e.CellIndex == 41))
                {
                    var tmpX = _mlr.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Location.X - _beforeMsgPictureX + _resizeOffset;
                    MsgPicture.Location = new Point(MsgPicture.Location.X + tmpX, MsgPicture.Location.Y);
                }
            }
            #endregion

            #region セルリサイズ前イベント
            /// <summary>
            /// セルリサイズ前イベント
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void _mlr_CellResizeCompleting(object sender, CellResizeCompletingEventArgs e)
            {
                if (e.RowIndex == -1)
                {
                    _beforeMsgPictureX = _mlr.ColumnHeaders[0].Cells["ch_TreatmentWhen"].Location.X;
                    _resizeOffset = e.CellName == "ch_TreatmentWhen" ? e.ResizeOffset : 0;
                }
            }
            #endregion
            #endregion
        }
        #endregion

        //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
        #region 列情報クラス
        /// <summary>
        /// 列情報クラス
        /// </summary>
        private class ColInfo
        {
            public int Index { get; set; }

            /// <summary>
            /// その列の行に表示するセル名
            /// </summary>
            public string RowCellName { get; set; }

            /// <summary>
            /// その列を表示するか？
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// 列ヘッダー名
            /// </summary>
            public string HeaderName { get; set; }

            /// <summary>
            /// 列ヘッダーフィルターを表示しないか？
            /// </summary>
            public bool IsNonFilter { get; set; }
        }
        #endregion
        //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

        #region 指摘の追加/削除/コピーを制御するクラス
        /// <summary>
        /// 指摘の追加/削除/コピーを制御するクラス
        /// </summary>
        private class PointHandleController : Colleague
        {
            private MultiRowContoller _gridCon;
            private int _holdDayId;

            //Delete Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            /// <summary>
            /// DBに登録するまでの仮の指摘ID
            /// </summary>
            //private int _pointId = -1;
            //Delete End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            #region コンストラクタ
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="gridCon">データグリッドビュー制御クラス</param>
            /// <param name="holdDayId">開催日ID</param>
            /// <param name="med">編集管理メディエータ</param>
            public PointHandleController(MultiRowContoller gridCon, int holdDayId, EditMediator med) : base(med)
            {
                this._gridCon = gridCon;
                this._holdDayId = holdDayId;
            }
            #endregion

            //Append Start 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)
            #region 新規行に対象車追加します。
            /// <summary>
            /// 新規行に対象車追加します。
            /// </summary>
            /// <param name="userAuthority"></param>
            /// <returns></returns>
            public bool IsAddTargetCar(UserAuthorityOutModel userAuthority)
            {
                var target = new DesignCheckPointGetOutModel()
                {
                    // 指摘ID
                    ID = DesignCheckDetailListForm.TEMP_POINT_ID,

                    // 開催日_ID
                    開催日_ID = this._holdDayId,
                };

                using (var form = new PointingTargetCarAddForm() { DesignCheckPoint = target, UserAuthority = userAuthority, ParentFormType = true })
                {
                    return form.ShowDialog() == DialogResult.OK;
                }
            }
            #endregion
            //Append End 2021/07/28 張晋華 開発計画表設計チェック機能改修(保守対応)

            #region 指摘を追加します。
            /// <summary>
            /// 指摘を追加します。
            /// </summary>
            public void Add(bool isEdit = true)
            {
                var target = new[] { new DesignCheckPointGetOutModel()
                {
                    //Delete Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                    //// 指摘ID
                    //ID = this._pointId--,
                    //Delete End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                    //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                    // 指摘ID
                    ID = DesignCheckDetailListForm.TEMP_POINT_ID,
                    //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                    // 開催日_ID
                    開催日_ID = this._holdDayId,

                    // ステータス
                    FLAG_CLOSE = MultiRowContoller.StatusOpen,

                    // 履歴件数
                    HISTORY_COUNT = 0
                }};

                // 行追加して再バインド
                this._gridCon.PointModelList.AddRange(target);
                this._gridCon.Bind();

                //Delete Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
                //// 追加行を選択状態に設定
                //this._gridCon.Unselected();
                //this._gridCon.SelectEndRow();

                //// 最終行を表示
                //this._gridCon.ScrollEndRow();
                //Delete End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

                if (isEdit)
                {
                    Edited();
                }
            }
            #endregion

            #region 指摘を削除します。
            /// <summary>
            /// 指摘を削除します。
            /// </summary>
            /// <returns>bool</returns>
            public bool Del()
            {
                // 削除対象ID取得
                var targetPointIds = this._gridCon.GetSelectedModels().Where((x) => x?.ID != null).Select((x) => x.ID.Value).ToList();

                // 削除対象があるかどうか
                if (targetPointIds.Any() == false)
                {
                    //選択項目なしメッセージ
                    Messenger.Warn(Resources.KKM00009);
                    return false;
                }

                // 削除可否を問い合わせ
                if (Messenger.Confirm(Resources.KKM00008) != DialogResult.Yes)
                {
                    return false;
                }

                // DBから削除実行（正の数がDB登録しているIDのため）
                var dbDelList = targetPointIds.Where((x) => x > 0).Select((x) => new DesignCheckPointDeleteInModel() { ID = x }).ToList();
                if (Delete(dbDelList))
                {
                    // DBから削除はしているが、登録はDBにしていないのでDBから再取得はできないためモデルリストも削除処理する（論理削除なのは他画面から持ってきた名残らしい）
                    targetPointIds.ForEach(x => this._gridCon.PointModelList.Where(y => y.ID == x).ToList().ForEach(z => z.DELETE_FLG = true));
                    this._gridCon.Bind();
                    

                    // 削除完了メッセージ
                    Messenger.Info(Resources.KKM00003);

                    return true;
                }

                return false;
            }
            #endregion

            #region 指摘をコピーして選択行に貼り付けます。
            /// <summary>
            /// 指摘をコピーして選択行に貼り付けます。
            /// </summary>
            public void Copy()
            {
                // 選択行のチェック
                var rows = _gridCon.GetSelectedRows();
                if (rows.Any() == false)
                {
                    Messenger.Warn(Resources.KKM00009);
                    return;
                }

                //Append Start 2021/08/19 杉浦 設計チェック請負
                if(rows.Any(x => x.Cells["MgrNoTextBoxColumn"].Value != null))
                {
                    Messenger.Warn(Resources.KKM01024);
                    return;
                }
                //Append End 2021/08/19 杉浦 設計チェック請負

                // コピー画面表示
                using (var form = new DesignCheckCopyForm() { Original = rows.FirstOrDefault() })
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    var copys = form.Copys;
                    var staff = new StaffController();

                    foreach (var row in rows)
                    {
                        foreach (var copy in copys)
                        {
                            row.Cells["TargetCheckBoxColumn"].Value = null;
                            row.Cells[copy.Key].Value = copy.Value;
                        }

                        staff.CopyProcess(row);

                        // 高さ自動調整
                        row.Cells["SituationTextBoxColumn"].PerformVerticalAutoFit();
                    }

                    Edited();
                }
            }
            #endregion

            #region 内部メソッド

            /// <summary>
            /// DBから指摘の削除
            /// </summary>
            /// <returns></returns>
            private bool Delete(List<DesignCheckPointDeleteInModel> list)
            {
                if (list == null || list.Any() == false)
                {
                    return true;
                }

                //設計チェック詳細登録
                var res = HttpUtil.DeleteResponse(ControllerType.DesignCheckPoint, list);

                //レスポンスが取得できたかどうか
                if (res == null || res.Status != Const.StatusSuccess)
                {
                    return false;
                }

                return true;
            }

            #endregion
        }

        #endregion

        #region 編集されたかどうかを管理するクラス
        /// <summary>
        /// 編集されたかどうかを管理するクラス
        /// </summary>
        private class EditMediator
        {
            /// <summary>
            /// 監視対象リスト
            /// </summary>
            private List<Colleague> _Colleagues = new List<Colleague>();

            /// <summary>
            /// 編集されたかどうか？
            /// </summary>
            private bool _IsEdit = false;

            /// <summary>
            /// 監視対象オブジェクトを追加します。
            /// </summary>
            /// <param name="colleague"></param>
            public void addColleague(Colleague colleague)
            {
                _Colleagues.Add(colleague);
            }

            /// <summary>
            /// 通知
            /// </summary>
            public void Notify()
            {
                _IsEdit = true;
            }

            /// <summary>
            /// 編集の判定を行い、編集されている場合は確認メッセージを表示します。
            /// [false：編集されていない場合、または編集されていても登録しない場合]
            /// [true：編集されていて登録する場合]
            /// </summary>
            /// <returns></returns>
            public bool Judge()
            {
                if (_IsEdit == true)
                {
                    return Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes;
                }

                return false;
            }

            //Append Start 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)
            /// <summary>
            /// 編集の判定を行い、編集されている場合は確認メッセージを表示します。
            /// [false：編集されていても登録しない場合]
            /// [true：編集されていない場合、又は編集されていて登録する場合]
            /// </summary>
            /// <returns></returns>
            public bool AddPointJudge()
            {
                if (_IsEdit == true)
                {
                    return Messenger.Confirm(Resources.KKM00006) == DialogResult.Yes;
                }

                return true;
            }
            //Append End 2021/07/02 張晋華 開発計画表設計チェック機能改修(保守対応)

            /// <summary>
            /// 編集情報の初期化
            /// </summary>
            public void Reset()
            {
                _IsEdit = false;
            }
        }
        #endregion

        #region 監視オブジェクトの基底クラス
        /// <summary>
        /// 監視オブジェクトの基底クラス
        /// </summary>
        private class Colleague
        {
            /// <summary>
            /// 管理オブジェクト
            /// </summary>
            protected EditMediator mediator;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="mediator"></param>
            public Colleague(EditMediator mediator)
            {
                this.mediator = mediator;
            }

            /// <summary>
            /// 編集されたことを管理オブジェクトに通知します。
            /// </summary>
            public void Edited()
            {
                this.mediator.Notify();
            }
        }
        #endregion

        #region クラス生成管理クラス
        /// <summary>
        /// クラス生成管理クラス
        /// </summary>
        private class DetailFactory : Factory
        {
            /// <summary>
            /// 編集されたかどうかを管理するクラス
            /// </summary>
            public EditMediator EditMediator { get; private set; }

            /// <summary>
            /// MultiRowを制御するクラス
            /// </summary>
            public MultiRowContoller MultiRowContoller { get; private set; }

            /// <summary>
            /// 指摘の追加/削除/コピーを制御するクラス
            /// </summary>
            public PointHandleController PointHandleController { get; private set; }

            /// <summary>
            /// 本画面の編集を監視するオブジェクト
            /// </summary>
            public Colleague MyColleague { get; private set; }

            /// <summary>
            /// クラス生成管理クラスのコンストラクタ
            /// </summary>
            /// <param name="mlr"></param>
            /// <param name="rowCountLabel"></param>
            /// <param name="designCheck"></param>
            /// <param name="userAuthorityModel"></param>
            /// <param name="successProc"></param>
            public DetailFactory(GcMultiRow mlr, Label rowCountLabel, DesignCheckGetOutModel designCheck, UserAuthorityOutModel userAuthorityModel, System.Action successProc = null)
                : base(designCheck, userAuthorityModel, successProc)
            {
                EditMediator = new EditMediator();
                MultiRowContoller = new MultiRowContoller(mlr, rowCountLabel, Authority, designCheck.ID.Value);
                PointHandleController = new PointHandleController(MultiRowContoller, designCheck.ID.Value, EditMediator);
                MyColleague = new Colleague(EditMediator);
            }
        }
        #endregion

        #region 状況管理クラス
        /// <summary>
        /// 状況管理クラス
        /// </summary>
        private class ProgressController
        {
            private GcMultiRow _mlr;
            private List<DesignCheckCarGetOutModel> _CarModelList;
            private List<DesignCheckProgressGetOutModel> _ProgressModelList;

            /// <summary>
            /// 状況管理クラスのコンストラクタ
            /// </summary>
            /// <param name="mlr"></param>
            /// <param name="carModelList"></param>
            public ProgressController(GcMultiRow mlr, List<DesignCheckCarGetOutModel> carModelList)
            {
                _mlr = mlr;
                _CarModelList = carModelList;
                _ProgressModelList = new List<DesignCheckProgressGetOutModel>();
            }

            /// <summary>
            /// 状況を取得します。
            /// </summary>
            /// <param name="pointId">指摘ID</param>
            /// <param name="carId">対象車両ID</param>
            /// <returns></returns>
            public string GetProgSymbol(int pointId, int carId)
            {
                return GetModel(pointId, carId)?.状況;
            }

            /// <summary>
            /// 状況モデルを設定します。
            /// </summary>
            /// <param name="pointModelList">指摘モデルリスト</param>
            public void Set(List<DesignCheckPointGetOutModel> pointModelList)
            {
                // 初期化
                _ProgressModelList = new List<DesignCheckProgressGetOutModel>();

                // 指摘IDリスト
                var pintIds = pointModelList.Where((x) => x.DELETE_FLG == false).Select((x) => x.ID).Distinct();

                // 画面に指摘追加した仮行が存在するか？
                var isAddNewPoint = pointModelList.Any((x) => x.ID < 0);

                foreach (var Id in pintIds)
                {
                    if (isAddNewPoint == false)
                    {
                        // 指摘追加した仮行が存在しない場合は指摘モデルリストから取得

                        // 状況モデル取得
                        _ProgressModelList.AddRange(
                            pointModelList
                                .Where((x) => x.ID == Id && x.対象車両_ID != null)
                                .Select((x) => new DesignCheckProgressGetOutModel
                                {
                                    指摘_ID = x.ID.Value,
                                    対象車両_ID = x.対象車両_ID.Value,
                                    状況 = x.状況記号
                                }));
                    }
                    else
                    {
                        // 指摘追加した仮行が存在する場合は画面から取得

                        // 対象行
                        var row = _mlr.Rows.SingleOrDefault((x) => Convert.ToInt32(x.Cells["IDTextBoxColumn"].Value) == Id);

                        // 画面にバインドされているデータを対象とする
                        if (row != null)
                        {
                            // 対象車両セルリスト
                            var cels = row.Cells.Where((x) => _CarModelList.Any((y) => x.Name == string.Format("Car_{0}", y.ID)));

                            // 登録済対象車両_ID
                            var carIds = pointModelList.Where((y) => y.ID == Id).Select((x) => x.対象車両_ID).Distinct();

                            // 状況登録済のセル
                            var cels1 = cels.Where((x) => carIds.Contains(Convert.ToInt32(x.Name.Replace("Car_", ""))));

                            // 状況未登録のセル
                            var cels2 = cels.Where((x) => carIds.Contains(Convert.ToInt32(x.Name.Replace("Car_", ""))) == false && x.Value != null);

                            // 状況モデル取得
                            _ProgressModelList.AddRange(
                                    cels1.Union(cels2).Select((x) => new DesignCheckProgressGetOutModel()
                                    {
                                        指摘_ID = Id.Value,
                                        対象車両_ID = Convert.ToInt32(x.Name.Replace("Car_", "")),
                                        状況 = Convert.ToString(x.Value),
                                    }));
                        }
                    }
                }
            }

            #region 内部メソッド

            /// <summary>
            /// 状況モデル取得
            /// </summary>
            /// <param name="pontId"></param>
            /// <param name="carId"></param>
            /// <returns></returns>
            private DesignCheckProgressGetOutModel GetModel(int pontId, int carId)
            {
                return _ProgressModelList.SingleOrDefault((x) => x.指摘_ID == pontId && x.対象車両_ID == carId);
            }

            #endregion
        }
        #endregion

    }
}