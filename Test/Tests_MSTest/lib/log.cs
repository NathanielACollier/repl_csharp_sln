using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests_MSTest.lib;

public static class log
{

    public static void raw(string text)
    {
        System.Diagnostics.Debug.WriteLine(text);
    }
}
