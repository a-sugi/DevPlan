using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevPlan.UICommon.Util
{
    /// <summary>
    /// フォーム制御クラス
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class FormUtil : IDisposable
    {
        private Form _frm;
        private Cursor _cur;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormUtil()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form">対象フォーム</param>
        public FormUtil(Form form)
        {
            _frm = form;
        }

        /// <summary>
        /// 対象フォームの設定
        /// </summary>
        public void SetForm(Form form)
        {
            _frm = form;
        }

        /// <summary>
        /// 対象フォームの取得
        /// </summary>
        public Form GetForm()
        {
            return _frm;
        }

        /// <summary>
        /// シングルフォームの表示（アプリ一意）
        /// </summary>
        /// <remarks>アプリ内で一意の非モーダル</remarks>
        /// <remarks>同一フォームが開いている場合は閉じる</remarks>
        /// <param name="owner"></param>
        /// <param name="isSingle"></param>
        public void SingleFormShow(Form owner, bool isSingle = true)
        {
            var forms = Application.OpenForms;

            // 親画面がモーダルの場合など
            if (isSingle　== false)
            {
                _frm.Show(owner);

                return;
            }

            for (var i = 0; i < forms.Count; i++)
            {
                var oth = forms[i];

                if (oth.GetType() == _frm.GetType())
                {
                    if (oth != null && !oth.IsDisposed)
                    {
                        oth.Close();
                    }
                }
            }

            _frm.Show(owner);
        }

        /// <summary>
        /// 待機カーソル設定
        /// </summary>
        public void CursorWait()
        {
            _cur = _frm.Cursor;
            _frm.Cursor = Cursors.WaitCursor;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            if (_cur != null)
            {
                _frm.Cursor = _cur;
            }
        }
    }
}
