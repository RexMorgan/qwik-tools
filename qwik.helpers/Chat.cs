using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace qwik.helpers
{
    public static class Chat
    {
        private const string AolListbox = "_AOL_Listbox";
        private const string AolRichControl = "RICHCNTL";
        private const string AolRichReadOnly = "RICHCNTLREADONLY";

        public static string GetAllChat()
        {
            return Externals.GetText(ChatBox());
        }

        public static void Send(string toSend)
        {
            var editBox = ChatEditBox();
            var text = Externals.GetText(editBox);
            if (text.StartsWith("Chat Input")) text = text.Substring(10);
            var reset = string.Empty;
            Externals.SendMessageByString(editBox, Externals.WM_SETTEXT, 0, ref reset);
            Thread.Sleep(10);
            Externals.SendMessageByString(editBox, Externals.WM_SETTEXT, 0, ref toSend);
            Externals.SendMessage(editBox, Externals.WM_CHAR, 13, 0);
            Thread.Sleep(10);
            Externals.SendMessageByString(editBox, Externals.WM_SETTEXT, 0, ref text);
        }

        public static void Clear()
        {
            Externals.SendMessage(ChatBox(), 0x000c, 0, 0);
        }

        public static void Close()
        {
            while(Windows.Chat() != IntPtr.Zero)
            {
                Externals.PostMessage(Windows.Chat(), 0x0010, 0, 0);
                Thread.Sleep(10);
            }
        }

        public static int GetLineCount()
        {
            return Externals.SendMessage(ChatBox(), Externals.EM_GETLINECOUNT, 0, 0).ToInt32();
        }

        public static string GetLastLine()
        {
            return Windows.GetTextByLine(ChatBox(), GetLineCount());
        }

        public static IntPtr ChatBox()
        {
            var room = Windows.Chat();
            return Externals.FindWindowEx(room, IntPtr.Zero, AolRichReadOnly, null);
        }

        public static IntPtr ChatEditBox()
        {
            var room = Windows.Chat();
            return Externals.FindWindowEx(room, IntPtr.Zero, AolRichControl, null);
        }

        public static IEnumerable<string> WhoIsChatting()
        {
            var listbox = Externals.FindWindowEx(Windows.Chat(), IntPtr.Zero, AolListbox, null);
            var processId = 0;
            Externals.GetWindowThreadProcessId(listbox, ref processId);
            var processHandle = Externals.OpenProcess(0xf0010, 0, processId);

            var count = Externals.SendMessageLong(listbox, Externals.LB_GETCOUNT, 0, 0);
            var chatters = new List<string>();
            for (var i = 0; i <= count - 1; ++i)
            {
                // What the actual fuck... not sure with LB_GETTEXT doesn't work.
                //  Thanks Majek
                var num7 = 0;
                var bytesWritten = 0;
                var buffer = new string('\0', 4);
                var baseAddress = Externals.SendMessage(listbox, Externals.LB_GETITEMDATA, i, 0).ToInt32();
                baseAddress += 28;
                Externals.ReadProcessMemory(processHandle, baseAddress, ref buffer, 4, ref bytesWritten);
                var bytes = Encoding.Default.GetBytes(buffer);
                var handle = GCHandle.Alloc(num7, GCHandleType.Pinned);
                var destination = handle.AddrOfPinnedObject();
                Marshal.Copy(bytes, 0, destination, 4);
                num7 = Marshal.ReadInt32(destination) + 6;
                buffer = new string('\0', 0x10);
                Externals.ReadProcessMemory(processHandle, num7, ref buffer, buffer.Length, ref bytesWritten);
                if (buffer.Contains("\0")) buffer = buffer.Substring(0, buffer.IndexOf('\0'));
                handle.Free();
                chatters.Add(buffer);
            }
            return chatters;
        }
    }
}