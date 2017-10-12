using System;
using System.Collections.Generic;
using System.Text;

namespace Completor.Core.Trees
{
    /// <summary>
    /// For quick search tree is used
    /// The node
    /// For each next symbol is a new branch
    /// </summary>
    public class Node
    {
        // Current next symbol
        public char Symbol { get; set; }
        // Subsequense of the symbols from the beginning of the word to current
        public string SubStr { get; set; }
        // Semidegree of ingoing of the node (Рус. - Полустепень захода)
        public int Level { get; set; }
        // Frequense of the word if is leaf (Substr == Word.Value in this case), else 0
        public int Frequency { get; set; }
        // Child nodes
        public List<Node> Children { get; set; } = new List<Node>();
        // The list of nodes to return for current, predetermined at the search moment, filled when the tree build
        public List<Node> SortedByFrequencyChildren { get; set; } = new List<Node>();
    }
}
