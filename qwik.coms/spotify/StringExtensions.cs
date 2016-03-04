using System;
using System.Runtime.InteropServices;
using System.Text;

namespace qwik.coms.spotify
{
    public static class StringExtensions
    {
        public static IntPtr ToPtr(this string str)
        {
            if (str == null) return IntPtr.Zero;
            var buffer = Encoding.UTF8.GetBytes(str);
            var hStr = Marshal.AllocHGlobal(buffer.Length + 1);
            Marshal.Copy(buffer, 0, hStr, buffer.Length);
            Marshal.WriteByte(hStr, buffer.Length, 0);
            return hStr;
        }
    }
}