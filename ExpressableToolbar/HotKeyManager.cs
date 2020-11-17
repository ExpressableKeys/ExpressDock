﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ExpressableToolbar
{
    class HotKeyManager
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;
        private const uint VK_A = 0x41;

        static private IntPtr Hwnd;
        static private HwndSource Source;

        static public void SetupHotKeys(IntPtr hwnd)
        {
            Hwnd = hwnd;

            Source = HwndSource.FromHwnd(Hwnd);
            Source.AddHook(HwndHook);

            RegisterHotKey(Hwnd, HOTKEY_ID, MOD_CONTROL, VK_A); //CTRL + CAPS_LOCK
        }
        
        static public void ClearHotKeys()
        {
            Source.RemoveHook(HwndHook);
            UnregisterHotKey(Hwnd, HOTKEY_ID);
        }

        static private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_A)
                            {
                                Trace.WriteLine("Sup");
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }
}
