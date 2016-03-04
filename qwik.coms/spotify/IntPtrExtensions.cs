using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace qwik.coms.spotify
{
    public static class IntPtrExtensions
    {
        public static T ToStructure<T>(this IntPtr ptr)
            where T : struct
        {
            return (T)Marshal.PtrToStructure(ptr, typeof(T));
        }

        public static string ToStr(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero) return string.Empty;

            var l = new List<byte>();
            byte read;
            do
            {
                read = Marshal.ReadByte(ptr, l.Count);
                l.Add(read);
            } while (read != 0);

            return l.Any()
                ? Encoding.UTF8.GetString(l.ToArray(), 0, l.Count - 1)
                : string.Empty;
        }
    }
}