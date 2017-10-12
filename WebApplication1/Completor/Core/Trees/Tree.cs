using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Completor.Core.Entities;

namespace Completor.Core.Trees
{
    /// <summary>
    /// Search Tree
    /// The branch for each next symbol
    /// </summary>
    public class Tree
    {
        public Node Root { get; set; }

        public Tree(Word[] words)
        {
            Root = new Node { Level = 0, Frequency = 0, SubStr = string.Empty, Symbol = (char)0 };

            BuildNode(Root, words);
        }

        /// <summary>
        /// Recursively
        /// </summary>
        /// <param name="node">Node to build this and all children</param>
        /// <param name="words">Words available to build (not all to decrease the number and time)</param>
        private void BuildNode(Node node, Word[] words)
        {
            int level = node.Level;
            // Words that can be used by length
            List<Word> current = words.Where(w => w.Value.Length > level).ToList();

            // Branch for each symbol
            for (char c = 'a'; c <= 'z'; ++c)
            {
                // All completions possible for this tree branch
                List<Word> startsWithSymbol = current.Where(w => w.Value[level] == c).ToList();
                int frequency = startsWithSymbol.Count == 1
                    // If the leaf
                    ? startsWithSymbol[0].Frequency
                    // Else
                    : 0;

                // Add child node with current symbol
                Node child;
                node.Children.Add(child = new Node { Level = level + 1, Frequency = frequency, SubStr = node.SubStr + c, Symbol = c });

                // If are some words with the beginning corresponds to the branch
                // Build this branch
                if (startsWithSymbol.Any())
                {
                    BuildNode(child, startsWithSymbol.ToArray());
                }
            }

            // For empty string - Root is not needed
            if (level > 0)
            {
                List<Word> currentRoot = words.Where(w => w.Value.Length > level - 1).ToList();
                List<Word> startsWithSymbolRoot = currentRoot.Where(w => w.Value[level - 1] == node.Symbol).ToList();
                int toTake = Math.Min(startsWithSymbolRoot.Count, 10);
                startsWithSymbolRoot.Sort((x, y) => -x.Frequency.CompareTo(y.Frequency));
                var res = startsWithSymbolRoot.OrderBy(x => -x.Frequency).ThenBy(x => x.Value).ToList();

                // Adding of preselected outputs
                node.SortedByFrequencyChildren.AddRange(res.Take(toTake).Select(s => new Node { Level = level, Frequency = s.Frequency, SubStr = s.Value, Symbol = (char)0 }));
            }
        }

        /// <summary>
        /// Getting completions
        /// </summary>
        /// <param name="text">src</param>
        /// <returns></returns>
        private string[] GetCompletions(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string[0];
            }
            
            // Move through tree to the current phrase
            Node currentNode = Root;
            for (int i = 0, n = text.Length; i < n; ++i)
            {
                currentNode = currentNode.Children.FirstOrDefault(c => c.Symbol == text[i]);
                // If is absent
                if (currentNode == null)
                {
                    return new string[0];
                }
            }

            // Getting result
            List<Node> nodes = currentNode.SortedByFrequencyChildren;

            // Retrieving text
            List<string> result = nodes.Select(n => n.SubStr).ToList();
            int toTake = Math.Min(10, result.Count);

            return result.Take(toTake).ToArray();
        }

        /// <summary>
        /// async
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<string[]> GetCompletionsAsync(string text)
        {
            return await Task.Run(() => GetCompletions(text));
        }
    }
}
