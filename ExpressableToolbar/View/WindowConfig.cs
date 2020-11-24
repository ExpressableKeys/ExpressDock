
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace ExpressableToolbar
{
    class WindowConfig
    {
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public static void SetWindowNoActivate(Window win)
        {
            var helper = new WindowInteropHelper(win);

            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        public static void SetWindowTransparent(Window win)
        {
            var helper = new WindowInteropHelper(win);

            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT);
        }

		public static void SetWindowHidden(Window win)
        {
            var helper = new WindowInteropHelper(win);

            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }
    }
}
