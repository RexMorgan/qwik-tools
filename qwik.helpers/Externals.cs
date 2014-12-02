using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace qwik.helpers
{
    public static class Externals
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo
        public const int GWL_ID = -12;

        public const int EM_GETLINE = 0x00C4;
        public const int EM_GETLINECOUNT = 0x00BA;
        public const int EM_LINEINDEX = 0x00BB;
        public const int EM_LINELENGTH = 0x00C1;

        public const int LB_GETITEMDATA = 0x0199;
        public const int LB_GETCOUNT = 0x018B;
        public const int LB_GETTEXT = 0x0189;
        public const int LB_GETTEXTLEN = 0x018A;
        public const int LB_SETCURSEL = 0x0186;

        public const int WM_COMMAND = 0x0111;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_CHAR = 0x0102;

        public const uint MIIM_STRING = 0x0040;
        public const uint MFT_STRING = 0x000;
        // ReSharper restore IdentifierTypo
        // ReSharper restore InconsistentNaming

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, ref int lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr handle, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr handle, uint message, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr handle, uint message, int wParam, string lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr handle, int message, int wparam, int lparam);

        [DllImport("user32", EntryPoint = "PostMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int PostMessage(IntPtr handle, int wMsg, int wParam, int lParam);

        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessageByString(IntPtr handle, int message, int wParam, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lParam);

        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SendMessageLong(IntPtr handle, int message, int wParam, int lParam);

        [DllImport("user32")]
        public static extern IntPtr GetMenu(IntPtr aolWindow);

        [DllImport("user32")]
        public static extern int GetMenuItemCount(IntPtr menu);

        [DllImport("user32")]
        public static extern IntPtr GetSubMenu(IntPtr menu, int position);

        [DllImport("user32", EntryPoint = "GetMenuItemID")]
        public static extern IntPtr GetMenuItemId(IntPtr menu, int position);

        //[DllImport("user32", EntryPoint = "GetMenuStringA")]
        //public static extern int GetMenuString(IntPtr menu, int wIDItem, string lpString, int maxCount, int wFlag);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, int uItem, bool fByPosition, MENUITEMINFO lpmii);

        public static string GetMenuText(IntPtr menu, int item)
        {
            var mif = new MENUITEMINFO
            {
                fMask = MIIM_STRING,
                fType = MFT_STRING,
            };
            var res = GetMenuItemInfo(menu, item, true, mif); // load cch into memory
            if (!res) throw new Win32Exception();
            mif.cch++;
            mif.dwTypeData = Marshal.AllocHGlobal(new IntPtr(mif.cch*2));
            try
            {
                res = GetMenuItemInfo(menu, item, true, mif);
                if (!res) throw new Win32Exception();
                var menuTitle = Marshal.PtrToStringUni(mif.dwTypeData);
                if (menuTitle == null) return string.Empty;
                if (menuTitle.Contains("\t"))
                {
                    menuTitle = menuTitle.Substring(0, menuTitle.IndexOf("\t", StringComparison.Ordinal));
                }
                return menuTitle;
            }
            finally
            {
                Marshal.FreeHGlobal(mif.dwTypeData);
            }
        }

        public static string GetText(IntPtr handle)
        {
            var size = SendMessage(handle, WM_GETTEXTLENGTH, 0, 0).ToInt32();
            if (size <= 0) return string.Empty;

            var text = new StringBuilder(size + 1);
            SendMessage(handle, WM_GETTEXT, text.Capacity, text);
            return text.ToString();
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MENUITEMINFO
        {
            public int cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public IntPtr dwTypeData;
            public uint cch;
            public IntPtr hbmpItem;

            public MENUITEMINFO()
            {
                cbSize = Marshal.SizeOf(typeof (MENUITEMINFO));
            }
        }
    }
}