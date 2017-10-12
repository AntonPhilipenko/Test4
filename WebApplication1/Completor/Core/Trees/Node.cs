using System;
using System.Collections.Generic;
using System.Text;

namespace Completor.Core.Trees
{
    public class Node
    {
        public char Symbol { get; set; }
        public string SubStr { get; set; }
        public int Level { get; set; }
        public int Frequency { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public List<Node> SortedByFrequencyChildren { get; set; } = new List<Node>();
    }
}
