using System;
using System.Collections.Generic;
using System.Text;

namespace BlockChain.Framework.Extensions
{
    public static class GlobalExtensions
    {
        public static string RemoveSpaces(this string input)
        {
            return input.Replace(" ", "");
        }
    }
}
