using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TestEkkleia.Helpers
{
    public static class TestHelper
    {
        public static string GekoDriverPath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

    }
}
