using System;
using System.IO;

namespace commonUtilitiesLib;

public static class StringUtil
{

    public static string byteSizeToString(long byteSize)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;
        while (byteSize >= 1024 && order < sizes.Length - 1) {
            order++;
            byteSize = byteSize/1024;
        }

        // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
        // show a single decimal place, and no space.
        string result = String.Format("{0:0.##} {1}", byteSize, sizes[order]);
        return result;
    }
}