using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qwik.helpers
{
    public static class Windows
    {
        private const string AolFrame = "aol frame25";
        private const string MdiClient = "mdiclient";
        private const string AolChild = "AOL Child";

        private const string RichControl = "RICHCNTL";
        private const string AolIcon = "_AOL_Icon";
        private const string AolListbox = "_AOL_Listbox";
        private const string AolStatic = "_AOL_Static";

        private const string AolMainToolbar = "aol toolbar";
        private const string AolToolbar = "_AOL_toolbar";
        private const string AolAddressBar = "_AOL_edit";

        public static IntPtr AolWindow()
        {
            return Externals.FindWindow(AolFrame, null);
        }

        public static IntPtr AolMdiClient()
        {
            return Externals.FindWindowEx(AolWindow(), IntPtr.Zero, MdiClient, null);
        }

        public static IntPtr AolKeyword()
        {
            //return FindChildByTitle("Keyword");
            return ChildWindows().FirstOrDefault(x => GetWindowTitle(x) == "Keyword");
        }

        public static IntPtr FindChildByTitle(string title)
        {
            return FindChildByTitle(AolWindow(), AolChild, title);
        }

        public static IntPtr FindChildByTitle(IntPtr parent, string className, string title)
        {
            return Externals.FindWindowEx(parent, IntPtr.Zero, className, title);
        }

        public static string GetWindowTitle(IntPtr handle)
        {
            var length = Externals.GetWindowTextLength(handle);
            var sb = new StringBuilder(length + 1);
            Externals.GetWindowText(handle, sb, sb.Capacity);
            return sb.ToString();
        }

        public static string GetTextByLine(IntPtr handle, int line)
        {
            var lineIndex = Externals.SendMessage(handle, Externals.EM_LINEINDEX, line, 0).ToInt32();
            var lineLength = Externals.SendMessage(handle, Externals.EM_LINELENGTH, lineIndex, 0).ToInt32();

            var builder = new string(' ', lineLength);
            Externals.SendMessageByString(handle, Externals.EM_GETLINE, line, ref builder);
            return builder;
        }

        public static IEnumerable<IntPtr> ChildWindows()
        {
            return ChildWindows(AolMdiClient(), AolChild);
        }

        public static IEnumerable<IntPtr> ChildWindows(IntPtr handle, string className)
        {
            var child = IntPtr.Zero;
            do
            {
                child = Externals.FindWindowEx(handle, child, className, null);
                if (child != IntPtr.Zero) yield return child;
                else yield break;
            } while (child != IntPtr.Zero);
        }

        public static IntPtr Chat()
        {
            foreach (var child in ChildWindows())
            {
                var rich = Externals.FindWindowEx(child, IntPtr.Zero, RichControl, null);
                var aolList = Externals.FindWindowEx(child, IntPtr.Zero, AolListbox, null);
                var aolIcon = Externals.FindWindowEx(child, IntPtr.Zero, AolIcon, null);
                var aolStatic = Externals.FindWindowEx(child, IntPtr.Zero, AolStatic, null);
                if (rich != IntPtr.Zero && aolList != IntPtr.Zero && aolIcon != IntPtr.Zero && aolStatic != IntPtr.Zero)
                {
                    return child;
                }
            }
            return IntPtr.Zero;
        }
    }
}