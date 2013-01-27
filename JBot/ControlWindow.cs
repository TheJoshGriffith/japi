﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace JBot
{
    class ControlWindow
    {
        public const Int32 WM_CHAR = 0x0102;
        public const Int32 WM_KEYDOWN = 0x0100;
        public const Int32 WM_KEYUP = 0x0101;
        public const Int32 VK_RETURN = 0x0D;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        // Key code preparation.
        [Flags]
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }
        enum KeyboardControls : uint
        {
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            left = 0x25,
            up = 0x26,
            right = 0x27,
            down = 0x28
        }


        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        // Mouse Tricks
        public bool LeftClick(int dx, int dy)
        {
            SetCursorPos(dx, dy);
            mouse_event(MouseEventFlag.LeftDown, dx, dy, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, dx, dy, 0, UIntPtr.Zero);
            return true;
        }
        public bool RightClick(int dx, int dy)
        {
            SetCursorPos(dx, dy);
            mouse_event(MouseEventFlag.RightDown, dx, dy, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, dx, dy, 0, UIntPtr.Zero);
            return true;
        }

        // Get key codes ready for sending
        public UInt32 getKeyCode(string Key)
        {
            KeyboardControls keynum;
            if (Enum.TryParse<KeyboardControls>(Key, true, out keynum))
            {
                return (UInt32)(keynum);
            }
            else
            {
                return new UInt32();
            }
        }

        // Send key codes
        public void SendKeys(string message)
        {
            foreach (char c in message)
            {
                int charValue = c;
                IntPtr val = new IntPtr((Int32)c);
                SendMessage(Util.Handle, WM_CHAR, val, new IntPtr(0));
            }
        }

        public void SendButton(string Key)
        {
            IntPtr wParam = new IntPtr(getKeyCode(Key));
            SendMessage(Util.Handle, WM_KEYDOWN, wParam, new IntPtr(0));
            SendMessage(Util.Handle, WM_KEYUP, wParam, new IntPtr(0));
        }
    }
}
