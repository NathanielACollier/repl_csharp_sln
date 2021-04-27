using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace curl_01_basicPInvokeTest
{
    /*
     Found code here:
     https://github.com/dotnet/runtime/issues/4461
     
     Trying to figure out how to load libcurl, and call it from dotnet
     */
    class Program
    {
        private const string libcurl = "libcurl";

        [DllImport(libcurl)]
        private static extern int curl_global_init(long flags);

        [DllImport(libcurl)]
        private static extern IntPtr curl_multi_init();

        [DllImport(libcurl)]
        private static extern IntPtr curl_easy_init();

        [DllImport(libcurl)]
        private static extern int curl_multi_add_handle(IntPtr multi_handle, IntPtr easy_handle);

        [DllImport(libcurl, CharSet = CharSet.Ansi)]
        private static extern int curl_easy_setopt(IntPtr curl, int option, string value);

        [DllImport(libcurl)]
        private static extern int curl_multi_perform(IntPtr multi, out int running_handles);

        private const int CURL_GLOBAL_ALL = 3;
        private const int CURLOPT_URL = 10002;

        private static int ThrowIfNotZero(int result, [CallerLineNumber] int line = 0)
        {
            if (result != 0) throw new Exception("Line " + line);
            return result;
        }

        private static IntPtr ThrowIfZero(IntPtr result, [CallerLineNumber] int line = 0)
        {
            if (result == IntPtr.Zero) throw new Exception("Line " + line);
            return result;
        }

        public static void Main()
        {
            ThrowIfNotZero(curl_global_init(CURL_GLOBAL_ALL));
            IntPtr multi = ThrowIfZero(curl_multi_init());

            const int Iters = 100;
            for (int i = 0; i < Iters; i++)
            {
                IntPtr easy = ThrowIfZero(curl_easy_init());
                ThrowIfNotZero(curl_easy_setopt(easy, CURLOPT_URL, "http://www.httpbin.org"));
                ThrowIfNotZero(curl_multi_add_handle(multi, easy));
            }

            Console.WriteLine("Before");
            int running_handles;
            ThrowIfNotZero(curl_multi_perform(multi, out running_handles));
            Console.WriteLine("After");
        }
    }
}
