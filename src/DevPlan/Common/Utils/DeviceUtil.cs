using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using DevPlan.UICommon.Enum;
using System.Diagnostics;

namespace DevPlan.UICommon.Utils
{
    /// <summary>
    /// Deviceユーティリティークラス
    /// </summary>
    public class DeviceUtil
    {
        #region Win32 API
        [DllImport("user32.dll")]
        private static extern IntPtr SetProcessDPIAware();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion

        #region デリゲート
        private delegate IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr MouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        #region プロパティ
        /// <summary>
        /// プリントスクリーン許可フラグ
        /// </summary>
        public bool IsPrintScreen { get; set; }

        /// <summary>
        /// ログインセッション監視デリゲート
        /// </summary>
        public Action LoginSessionMonitor { get; set; } = () => { };
        #endregion

        #region メンバ
        private KeyboardProc keyProc;
        private IntPtr keyHookId = IntPtr.Zero;
        private MouseProc mouseProc;
        private IntPtr mouseHookId = IntPtr.Zero;
        #endregion

        #region Win32 Constants
        protected const int WH_KEYBOARD_LL = 0x000D;
        protected const int WH_MOUSE_LL = 0x000E;
        #endregion

        #region Win32API Structures
        /// <summary>
        /// キーボードの状態の構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class StateKeyboard
        {
            public uint Code;
            public uint ScanCode;
            public KeyboardEventFlags Flags;
            public uint Time;
            public UIntPtr ExtraInfo;
        }

        /// <summary>
        /// キーボードイベントフラグ
        /// </summary>
        [Flags]
        public enum KeyboardEventFlags : uint
        {
            KEYEVENTF_EXTENDEDKEY = 0x0001,
            KEYEVENTF_KEYUP = 0x0002,
            KEYEVENTF_SCANCODE = 0x0008,
            KEYEVENTF_UNICODE = 0x0004,
        }

        /// <summary>
        /// マウスの状態の構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct StateMouse
        {
            public int X;
            public int Y;
            public uint Data;
            public uint Flags;
            public uint Time;
            public UIntPtr ExtraInfo;
        }

        /// <summary>
        /// 挙動コード
        /// </summary>
        public enum StrokeCode
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MOUSEWHEE = 0x020A,
            WM_XBUTTONDOWN = 0x20B,
            WM_XBUTTONUP = 0x20C,
        }
        #endregion

        /// <summary>
        /// 高DPI対応設定
        /// </summary>
        public void SetProcessHighDPI()
        {
            SetProcessDPIAware();
        }

        /// <summary>
        /// Windows スケールを取得する
        /// </summary>
        /// <returns>Windows スケール</returns>
        public float GetScalingFactor()
        {
            return (float)GetDeviceCaps(GetWindowDC(IntPtr.Zero), (int)DeviceCap.LOGPIXELSX) / 96;
        }

        /// <summary>
        /// グローバルフックを開始する
        /// </summary>
        /// <param name="isKeyHook"></param>
        /// <param name="isMouseHook"></param>
        public void SetGlobalHook(bool isKeyHook = true, bool isMouseHook = true)
        {
            // キーボードフック
            if (keyHookId != IntPtr.Zero && isKeyHook == true)
            {
                return;
            }

            // マウスフック
            if (mouseHookId != IntPtr.Zero && isMouseHook == true)
            {
                return;
            }

            using (var curProcess = Process.GetCurrentProcess())
            {
                using (var curModule = curProcess.MainModule)
                {
                    // キーボードフック
                    if (isKeyHook == true)
                    {
                        keyProc = KeyboardHookProcedure;
                        keyHookId = SetWindowsHookEx(WH_KEYBOARD_LL, keyProc, GetModuleHandle(curModule.ModuleName), 0);
                    }

                    // マウスフック
                    if (isMouseHook == true)
                    {
                        mouseProc = MouseHookProcedure;
                        mouseHookId = SetWindowsHookEx(WH_MOUSE_LL, mouseProc, GetModuleHandle(curModule.ModuleName), 0);
                    }
                }
            }
        }

        /// <summary>
        /// グローバルフックを終了する
        /// </summary>
        /// <param name="isKeyHook"></param>
        /// <param name="isMouseHook"></param>
        public void UnGlobalHook(bool isKeyHook = true, bool isMouseHook = true)
        {
            // キーボードフック
            if (keyHookId != IntPtr.Zero && isKeyHook == true)
            {
                UnhookWindowsHookEx(keyHookId);
                keyHookId = IntPtr.Zero;
            }

            // マウスフック
            if (mouseHookId != IntPtr.Zero && isMouseHook == true)
            {
                UnhookWindowsHookEx(mouseHookId);
                mouseHookId = IntPtr.Zero;
            }
        }

        /// <summary>
        /// キーボードフックプロシージャ
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        private IntPtr KeyboardHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // アプリが有効な場合
                if (Form.ActiveForm != null)
                {
                    // キーダウンの場合
                    if (wParam == (IntPtr)StrokeCode.WM_KEYDOWN)
                    {
                        // ログインセッション監視
                        LoginSessionMonitor();
                    }
                }

                var kb = (StateKeyboard)Marshal.PtrToStructure(lParam, typeof(StateKeyboard));

                // プリントスクリーンの場合
                if (kb.Code == (uint)Keys.PrintScreen)
                {
                    if (!IsPrintScreen)
                    {
                        // 無効化
                        return (IntPtr)1;
                    }
                }
            }

            return CallNextHookEx(keyHookId, nCode, wParam, lParam);
        }

        /// <summary>
        /// マウスフックプロシージャ
        /// </summary>
        private IntPtr MouseHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // アプリが有効な場合
                if (Form.ActiveForm != null)
                {
                    // 左右ボタンの場合
                    if (wParam == (IntPtr)StrokeCode.WM_LBUTTONDOWN ||
                        wParam == (IntPtr)StrokeCode.WM_RBUTTONDOWN)
                    {
                        // ログインセッション監視
                        LoginSessionMonitor();
                    }
                }
            }

            return CallNextHookEx(mouseHookId, nCode, wParam, lParam);
        }
    }
}
