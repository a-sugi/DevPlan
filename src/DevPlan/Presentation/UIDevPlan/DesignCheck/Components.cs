using DevPlan.Presentation.Common;
using DevPlan.Presentation.UIDevPlan.CapAndProduct;
using DevPlan.UICommon;
using DevPlan.UICommon.Dto;
using DevPlan.UICommon.Enum;
using DevPlan.UICommon.Properties;
using DevPlan.UICommon.Utils;
using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.Presentation.UIDevPlan.DesignCheck
{
    // 設計チェック共通部品群

    #region 権限管理クラス
    /// <summary>
    /// 権限管理クラス
    /// </summary>
    public class Authority
    {
        protected UserAuthorityOutModel _auth;

        #region 公開プロパティ

        public UserAuthorityOutModel UserAuthorityModel { get { return _auth; } }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        public Authority(UserAuthorityOutModel model)
        {
            this._auth = model;
        }
        #endregion

        #region その列が使用できるか？
        /// <summary>
        /// その列が使用できるか？
        /// </summary>
        /// <param name="ColName">対象列名</param>
        /// <param name="sectionCode">課コード</param>
        /// <returns></returns>
        public bool IsCan(string ColName, string sectionCode)
        {
            switch (ColName)
            {
                case "StatusComboBoxColumn":

                    return IsAdmin() || IsSks();

                case "TargetCheckBoxColumn":
                case "HistoryLinkColumn":
                case "PartsTextBoxColumn":
                case "SituationTextBoxColumn":
                case "TreatmentCheckBoxColumn":
                case "TreatmentSectionTextBoxColumn":
                case "TreatmentTargetTextBoxColumn":
                case "TreatmentHowTextBoxColumn":
                case "TreatmentOKCheckBoxColumn":
                case "TreatmentWhoTextBoxColumn":
                case "TreatmentWhenCalendarColumn":
                case "RepairCheckBoxColumn":
                case "PartsGetDayCalendarColumn":
                case "EndDayCalendarColumn":
                case "ApprovalOKCheckBoxColumn":
                case "ProgressComboBoxColumn":
                case "SectionTextBoxColumn":
                case "UserTextBoxColumn":
                case "UserTelTextBoxColumn":
                case "TestCarColumns":

                    return IsAdmin() || IsSks() || _auth.UPDATE_FLG == '1';

                case "RowHeader":
                case "NoTextBoxColumn":
                case "TestCarNameTextBoxColumn":
                case "EditDateTextColumn":
                case "EditUserNameTextBoxColumn":

                    // 常に読み取り専用
                    return false;

                default:
                    // それ以外はすべて使用許可
                    return true;
            }
        }
        #endregion

        #region そのボタンが表示できるか？ 
        /// <summary>
        /// そのボタンが表示できるか？
        /// </summary>
        /// <param name="btn">対象ボタン</param>
        /// <returns></returns>
        public bool IsVisible(Button btn)
        {
            switch (btn.Text)
            {
                case "設計ﾁｪｯｸ削除":
                case "新規作成":
                case "基本情報変更":

                    return IsAdmin();

                case "指摘エクスポート":
                case "指摘削除":

                    return IsAdmin() || IsSks();

                case "Excel出力":

                    return IsAdmin() || _auth.EXPORT_FLG == '1';

                case "登録":
                case "参加者追加":
                case "未登録参加者追加":
                case "参加者削除":
                case "指摘コピー":
                case "対象車追加":
                case "指摘追加":

                    return IsVisibleBtnUpdate();

                default:

                    // その他は常に表示
                    return true;
            }
        }
        #endregion

        #region 管理者か？
        /// <summary>
        /// 管理者か？
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            return this._auth.MANAGEMENT_FLG == '1';
        }
        #endregion

        #region SKS権限ありか？
        /// <summary>
        /// SKS権限ありか？
        /// </summary>
        /// <returns></returns>
        public bool IsSks()
        {
            return this._auth.SKS_FLG == '1';
        }
        #endregion

        #region 仮想メソッド

        /// <summary>
        /// 更新系ボタンの表示許可を判定します。
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsVisibleBtnUpdate()
        {
            return IsAdmin() || _auth.UPDATE_FLG == '1';
        }

        #endregion
    }
    #endregion

    #region クラス生成クラス
    /// <summary>
    /// クラス生成クラス
    /// </summary>
    public class Factory
    {
        private List<BtnCell> _btns;

        #region 公開プロパティ

        /// <summary>
        /// 担当者ボタンセルクラス
        /// </summary>
        public BtnUser BtnUser { get { return _btns.Single((x) => x.colName == "UserTextBoxColumn") as BtnUser; } }

        /// <summary>
        /// 権限管理クラス
        /// </summary>
        public Authority Authority { get; protected set; }

        /// <summary>
        /// 参加者一覧画面を制御するクラス
        /// </summary>
        public UserListFormContoroller UserListFormContoroller { get; private set; }

        /// <summary>
        /// 担当者セルを制御するクラス
        /// </summary>
        public StaffController StaffController { get; private set; }

        #endregion

        /// <summary>
        /// クラス生成クラスのコンストラクタ
        /// </summary>
        /// <param name="designCheck"></param>
        /// <param name="userAuthorityModel"></param>
        /// <param name="successProc">（ボタンセルクラス用）処理が成功した時の処理</param>
        public Factory(DesignCheckGetOutModel designCheck, UserAuthorityOutModel userAuthorityModel, System.Action successProc = null)
        {
            Authority = new Authority(userAuthorityModel);
            _btns = new List<BtnCell>()
            {
                new BtnResponsible(designCheck, Authority, successProc),
                new BtnHis(designCheck, Authority, successProc),
                new BtnTreatment(designCheck, Authority, successProc),
                new BtnUser(designCheck, Authority, successProc),
            };

            UserListFormContoroller = new UserListFormContoroller(designCheck, userAuthorityModel);
            StaffController = new StaffController();
        }

        /// <summary>
        /// ボタンセルクラスを取得します。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BtnCell GetBtnInstance(string name)
        {
            return _btns.SingleOrDefault((x) => x.colName == name);
        }
    }
    #endregion

    #region ボタンセル基底クラス
    /// <summary>
    /// ボタンセル基底クラス
    /// </summary>
    public abstract class BtnCell
    {
        protected Authority _auth;
        protected DesignCheckGetOutModel _DesignCheck;

        /// <summary>
        /// 処理が成功した時の処理デリゲート
        /// </summary>
        protected System.Action _SuccessProc;

        /// <summary>
        /// ボタンセル基底クラスのコンストラクタ
        /// </summary>
        /// <param name="designCheck"></param>
        /// <param name="auth">権限管理クラス</param>
        /// <param name="successProc">処理が成功した時の処理デリゲート</param>
        public BtnCell(DesignCheckGetOutModel designCheck, Authority auth, System.Action successProc)
        {
            _auth = auth;
            _DesignCheck = designCheck;
            _SuccessProc = successProc;
        }

        /// <summary>
        /// ボタンセルの列名
        /// </summary>
        public abstract string colName { get; }

        /// <summary>
        /// ボタンセルが使用できるか？
        /// </summary>
        /// <param name="model">行に対応する設計チェック指摘モデル</param>
        /// <returns></returns>
        protected virtual bool IsCan(DesignCheckPointGetOutModel model)
        {
            // 権限があるか？
            if (this._auth.IsCan(colName, model.担当課名) == false)
            {
                return false;
            }

            // ステータスがOPENの場合、使用許可
            return Convert.ToInt32(model.FLAG_CLOSE) == 0;
        }

        /// <summary>
        /// ボタンセル押下時の実装
        /// </summary>
        /// <param name="model"></param>
        protected abstract void Process(DesignCheckPointGetOutModel model);

        /// <summary>
        /// ボタンセル押下時の処理
        /// </summary>
        /// <param name="model"></param>
        public void Action(DesignCheckPointGetOutModel model)
        {
            // ボタンが使用できな場合は何もしない
            if (this.IsCan(model) == false)
            {
                return;
            }

            Process(model);
        }
    }
    #endregion

    #region 処置課ボタンクラス
    /// <summary>
    /// 処置課ボタンクラス
    /// </summary>
    public class BtnTreatment : BtnCell
    {
        public BtnTreatment(DesignCheckGetOutModel designCheck, Authority auth, System.Action successProc) : base(designCheck, auth, successProc)
        {
        }

        public override string colName
        {
            get
            {
                return "TreatmentSectionTextBoxColumn";
            }
        }

        protected override void Process(DesignCheckPointGetOutModel model)
        {
            //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
            if (model.試作管理NO == null)
            {
                //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                using (var form = new SectionListForm() { IS_ALL = true })
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        model.処置課 = form.SECTION_CODE;

                        _SuccessProc.Invoke();
                    }
                }
                //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
            }
            //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
        }
    }
    #endregion

    #region 担当課ボタンクラス
    /// <summary>
    /// 担当課ボタンクラス
    /// </summary>
    public class BtnResponsible : BtnCell
    {
        /// <summary>
        /// 担当課ボタンクラスのコンストラクタ
        /// </summary>
        /// <param name="designCheck"></param>
        /// <param name="auth"></param>
        /// <param name="successProc"></param>
        public BtnResponsible(DesignCheckGetOutModel designCheck, Authority auth, System.Action successProc) : base(designCheck, auth, successProc)
        {
        }

        public override string colName
        {
            get
            {
                return "SectionTextBoxColumn";
            }
        }

        protected override void Process(DesignCheckPointGetOutModel model)
        {
            //Append Start 2021/07/20 杉浦 開発計画表設計チェック機能改修
            if (model.試作管理NO == null)
            {
                //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
                using (var form = new SectionListForm() { DEPARTMENT_ID = string.Empty })
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        model.担当課名 = form.SECTION_CODE;
                        model.担当者名 = string.Empty;
                        model.担当者_ID = string.Empty;

                        _SuccessProc.Invoke();
                    }
                }
                //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
            }
            //Append End 2021/07/20 杉浦 開発計画表設計チェック機能改修
        }
    }
    #endregion

    #region 履歴ボタンセルクラス
    /// <summary>
    /// 履歴ボタンセルクラス
    /// </summary>
    public class BtnHis : BtnCell
    {
        public BtnHis(DesignCheckGetOutModel designCheck, Authority auth, System.Action successProc) : base(designCheck, auth, successProc)
        {
        }

        public override string colName
        {
            get
            {
                return "HistoryLinkColumn";
            }
        }

        protected override void Process(DesignCheckPointGetOutModel model)
        {
            // 履歴が無い場合は終了
            if (model.HISTORY_COUNT <= 0) return;

            using (var form = new DesignCheckPointHistoryForm { DesignCheck = _DesignCheck, DesignCheckPoint = model })
            {
                // 設計チェック指摘履歴画面表示
                form.ShowDialog();
            }
        }

        protected override bool IsCan(DesignCheckPointGetOutModel model)
        {
            // 権限があるか？
            return this._auth.IsCan(colName, model.担当課名);
        }
    }
    #endregion

    #region 担当者ボタンセルクラス
    /// <summary>
    /// 担当者ボタンセルクラス
    /// </summary>
    public class BtnUser : BtnCell
    {
        #region 内部変数

        /// <summary>
        /// ダブルクリック判定用タイマー
        /// </summary>
        private Timer doubleClickTimer = new Timer();

        private bool isFirstClick = true;
        private bool isDoubleClick = false;

        /// <summary>
        /// １回目クリックから２回目クリックまでの時間
        /// </summary>
        private int milliseconds = 0;

        #endregion

        #region 公開プロパティ

        /// <summary>
        /// シングルクリックの場合の処理
        /// </summary>
        public System.Action SingleAction { get; set; }

        /// <summary>
        /// ダブルクリックの場合の処理
        /// </summary>
        public System.Action DoubleAction { get; set; }

        /// <summary>
        /// セル名
        /// </summary>
        public override string colName
        {
            get
            {
                return "UserTextBoxColumn";
            }
        }

        #endregion

        /// <summary>
        /// 担当者ボタンセルクラスのコンストラクタ
        /// </summary>
        /// <param name="designCheck"></param>
        /// <param name="auth"></param>
        /// <param name="successProc"></param>
        public BtnUser(DesignCheckGetOutModel designCheck, Authority auth, System.Action successProc) : base(designCheck, auth, successProc)
        {
            doubleClickTimer.Interval = 40;
            doubleClickTimer.Tick += new EventHandler(doubleClickTimer_Tick);
        }

        protected override void Process(DesignCheckPointGetOutModel model)
        {
            // ダブルクリック判定
            Jude();
        }

        #region 内部メソッド

        /// <summary>
        /// ダブルクリック判定（マウスダウン時）
        /// </summary>
        private void Jude()
        {
            // １回目クリック時
            if (isFirstClick)
            {
                isFirstClick = false;

                // 計測開始
                doubleClickTimer.Start();
            }

            // ２回目クリック時
            else
            {
                // １回目クリックから２回目クリックまでの時間がダブルクリック設定時間内の場合
                if (milliseconds < SystemInformation.DoubleClickTime)
                {
                    isDoubleClick = true;
                }
            }
        }

        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleClickTimer_Tick(object sender, EventArgs e)
        {
            milliseconds += 100;

            // 計測時間がダブルクリック設定時間を超えている場合判定する
            if (milliseconds >= SystemInformation.DoubleClickTime)
            {
                doubleClickTimer.Stop();

                if (isDoubleClick)
                {
                    this.DoubleAction.Invoke();
                }
                else
                {
                    this.SingleAction.Invoke();
                }

                // 初期化
                isFirstClick = true;
                isDoubleClick = false;
                milliseconds = 0;
            }
        }

        #endregion
    }
    #endregion

    #region MultiRowを制御する基底クラス
    /// <summary>
    /// MultiRowを制御する基底クラス
    /// </summary>
    public class BaseMultiRowContoller
    {
        #region 内部変数

        private GcMultiRow _mlr;

        /// <summary>
        /// スクロール位置
        /// </summary>
        private Point? _ScrollLocation;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// MultiRowを制御するクラスのコンストラクタ
        /// </summary>
        /// <param name="mlr"></param>
        public BaseMultiRowContoller(GcMultiRow mlr)
        {
            _mlr = mlr;
        }
        #endregion

        #region スクロール制御
        /// <summary>
        /// スクロール位置を保存します。
        /// </summary>
        public void SaveScrollPoint()
        {
            _ScrollLocation = _mlr.FirstDisplayedLocation;
        }

        /// <summary>
        /// 保存してあるスクロール位置を設定します。
        /// </summary>
        public virtual void LoadScrollPoint()
        {
            if (_ScrollLocation != null)
            {
                _mlr.FirstDisplayedLocation = _ScrollLocation.Value;
            }
        }

        /// <summary>
        /// 保存してあるスクロール位置を初期化します。
        /// </summary>
        public void ClearScrollPoint()
        {
            _ScrollLocation = null;
        }
        #endregion
    }
    #endregion

    #region 参加者一覧画面を制御するクラス
    /// <summary>
    /// 参加者一覧画面を制御するクラス
    /// </summary>
    public class UserListFormContoroller
    {
        private DesignCheckGetOutModel _designCheck;
        private UserAuthorityOutModel _userAuthority;

        /// <summary>
        /// 参加者一覧画面
        /// </summary>
        private DesignCheckUserListForm _UserListForm;

        /// <summary>
        /// 参加者一覧画面を制御するクラスのコンストラクタ
        /// </summary>
        /// <param name="designCheck"></param>
        /// <param name="userAuthority"></param>
        public UserListFormContoroller(DesignCheckGetOutModel designCheck, UserAuthorityOutModel userAuthority)
        {
            _designCheck = designCheck;
            _userAuthority = userAuthority;
        }

        /// <summary>
        /// 参加者一覧画面をディスプレイ中央に開きます。（アクティブにします）
        /// </summary>
        public void OpenCenter()
        {
            // 閉じている場合は開く
            if (_UserListForm == null || _UserListForm.IsDisposed)
            {
                _UserListForm = new DesignCheckUserListForm() { DesignCheck = _designCheck };
                _UserListForm.Show();
                return;
            }

            // それ以外はアクティブにする
            _UserListForm.Activate();
        }

        /// <summary>
        /// 参加者一覧画面を読み取り専用でディスプレイ中央に開きます。（アクティブにします）
        /// </summary>
        public void OpenReadOnly()
        {
            // 閉じている場合は開く
            if (_UserListForm == null || _UserListForm.IsDisposed)
            {
                _UserListForm = new DesignCheckUserListForm() { DesignCheck = _designCheck, IsReadonly = true };
                _UserListForm.Show();
                return;
            }

            // それ以外はアクティブにする
            _UserListForm.Activate();
        }

        /// <summary>
        /// 参加者一覧画面を閉じます。
        /// </summary>
        public void Close()
        {
            if (_UserListForm != null && _UserListForm.IsDisposed == false)
            {
                _UserListForm.Close();
            }
        }
    }
    #endregion

    #region 担当者セルを制御するクラス
    /// <summary>
    /// 担当者セルを制御するクラス
    /// </summary>
    public class StaffController
    {
        /// <summary>
        /// 担当者セルを制御するクラスのコンストラクタ
        /// </summary>
        public StaffController()
        {

        }

        /// <summary>
        /// 手入力した担当者を取得します。手入力していない場合はNull。
        /// </summary>
        /// <param name="row">行</param>
        /// <returns></returns>
        public string GetManualInputValue(Row row)
        {
            // 担当者IDがブランク以外の場合は手入力ではないためNullを返却
            if (!string.IsNullOrWhiteSpace(row.Cells["UserIDTextBoxColumn"].Value?.ToString()))
            {
                return null;
            }

            return Convert.ToString(row.Cells["UserTextBoxColumn"].Value);
        }

        /// <summary>
        /// 担当者IDを取得します。手入力した担当者がある場合は担当者が返却されます。
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetId(Row row)
        {
            return row.Cells["UserIDTextBoxColumn"].Value == null ? GetManualInputValue(row) : Convert.ToString(row.Cells["UserIDTextBoxColumn"].Value);
        }

        /// <summary>
        /// コピー時の処理
        /// </summary>
        /// <param name="row"></param>
        public void CopyProcess(Row row)
        {
            // 担当者と担当者_IDが同じ値の場合は手入力とする（手入力の場合は担当者_IDに担当者名が入っているため）
            if (Convert.ToString(row.Cells["UserIDTextBoxColumn"].Value) == Convert.ToString(row.Cells["UserTextBoxColumn"].Value))
            {
                Notice(row);
            }
        }

        /// <summary>
        /// 手入力で担当者を設定した場合に通知します。
        /// </summary>
        /// <param name="row"></param>
        public void Notice(Row row)
        {
            // ※セルのチェンジイベントが発生するので注意すること。
            // → コードレビュー：バインドフラグなどを用いて不要なイベント処理は制御してください。

            // 手入力した担当者名の場合は担当者IDはNullとする
            row.Cells["UserIDTextBoxColumn"].Value = null;
        }
    }
    #endregion

    #region 期間コントロール管理クラス
    /// <summary>
    /// 期間コントロール管理クラス
    /// </summary>
    public class BetweenController
    {
        private UC.NullableDateTimePicker _start;
        private UC.NullableDateTimePicker _end;

        /// <summary>
        /// 期間コントロール管理クラスのコンストラクタ
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public BetweenController(UC.NullableDateTimePicker start, UC.NullableDateTimePicker end)
        {
            _start = start;
            _start.Tag = "ItemName(開催日From)";
            _end = end;
            _end.Tag = "ItemName(開催日To)";

            _start.LostFocus += LostFocus;
            _end.LostFocus += LostFocus;
        }

        /// <summary>
        /// ロストフォーカスイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostFocus(object sender, EventArgs e)
        {
            if (_start.SelectedDate != null && _end.SelectedDate == null)
            {
                _end.Value = _start.SelectedDate.Value.AddYears(1).AddDays(-1);
            }

            if (_end.SelectedDate != null && _start.SelectedDate == null)
            {
                _start.Value = _end.SelectedDate.Value.AddYears(-1).AddDays(1);
            }
        }

        /// <summary>
        /// 開始日の設定（終了日は１年後を自動設定）
        /// </summary>
        /// <param name="date"></param>
        public void SetSart(DateTime date)
        {
            _start.Value = date;
            _end.Value = date.AddYears(1).AddDays(-1);
        }

        /// <summary>
        /// 終了日の設定（開始日は１年前を自動設定）
        /// </summary>
        /// <param name="date"></param>
        public void SetEnd(DateTime date)
        {
            _end.Value = date;
            _start.Value = date.AddYears(-1).AddDays(1);
        }

        /// <summary>
        /// バリデーションルールを取得します。
        /// </summary>
        /// <returns></returns>
        public Dictionary<Control, Func<Control, string, string>> GetRule()
        {
            var map = new Dictionary<Control, Func<Control, string, string>>();

            var startDate = _start.SelectedDate;
            var endDate = _end.SelectedDate;

            map[_start] = (c, name) =>
            {
                var errMsg = "";

                if (startDate == null && endDate == null)
                {
                    // 必須チェック

                    errMsg = string.Format(Resources.KKM00001, "開始日または終了日のどちらか");

                    _start.BackColor = Const.ErrorBackColor;
                    _end.BackColor = Const.ErrorBackColor;
                }
                else if (startDate != null && endDate != null && startDate > endDate)
                {
                    // 相関チェック：期間Fromと期間Toがすべて入力で開始日が終了日より大きい場合はエラー

                    errMsg = Resources.KKM00018;

                    _start.BackColor = Const.ErrorBackColor;
                    _end.BackColor = Const.ErrorBackColor;
                }
                else if (startDate != null && endDate != null && DateTime.Compare(startDate.Value.AddYears(1).AddDays(-1), endDate.Value) < 0)
                {
                    // 一年以内チェック

                    errMsg = Resources.KKM02003;

                    _start.BackColor = Const.ErrorBackColor;
                    _end.BackColor = Const.ErrorBackColor;
                }

                return errMsg;
            };

            return map;
        }
    }
    #endregion
}
