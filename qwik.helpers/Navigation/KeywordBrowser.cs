using System;
using System.Threading;

namespace qwik.helpers.Navigation
{
    public class KeywordBrowser : IKeywordBrowser
    {
        public void Browse(string keyword)
        {
            Menu.RunMenuByName("Go to Keyword");
            var stopWaiting = DateTime.Now.Add(TimeSpan.FromSeconds(5));
            while (Windows.AolKeyword() == IntPtr.Zero && DateTime.Now <= stopWaiting)
            {
                Thread.Sleep(10);
            }

            if (Windows.AolKeyword() == IntPtr.Zero) return;
            IntPtr editBox;
            do
            {
                editBox = Externals.FindWindowEx(Windows.AolKeyword(), IntPtr.Zero, "_AOL_Edit", null);
            } while (editBox == IntPtr.Zero);
            Externals.SendMessageByString(editBox, Externals.WM_SETTEXT, 0, ref keyword);
            Thread.Sleep(100);
            Externals.SendMessage(editBox, Externals.WM_CHAR, 13, 0);
            Thread.Sleep(200);
        }
    }
}