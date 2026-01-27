using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MonoGame.Framework.Utilities
{
    internal static partial class ReflectionHelpers
    {
        // This helper caches the Marshal.SizeOf result
        // as it generates an allocation on each call. 
        private static class SizeOf<T>
        {
            private static readonly int _sizeOf;

            static SizeOf()
            {
                _sizeOf = Marshal.SizeOf<T>();
            }

            public static int Get()
            {
                return _sizeOf;
            }
        }
    }
}
