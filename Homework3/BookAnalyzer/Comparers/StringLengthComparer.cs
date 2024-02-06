using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalyzer.Comparers
{
    internal class StringLengthComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            int lengthCompare = x.Length.CompareTo(y.Length);
            return lengthCompare == 0 ? x.CompareTo(y) : lengthCompare;
        }
    }
}
