using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Completor.Core.Entities;

namespace Completor.Core.Trees
{
    public class Tree
    {
        public Node Root { get; set; }

        public Tree(Word[] words)
        {
            Root = new Node { Level = 0, Frequency = 0, SubStr = string.Empty, Symbol = (char)0 };

            BuildNode(Root, words);
        }

        private void BuildNode(Node node, Word[] words)
        {
            int level = node.Level;
            List<Word> current = words.Where(w => w.Value.Length > level).ToList();
            for (char c = 'a'; c <= 'z'; ++c)
            {
                List<Word> startsWithSymbol = current.Where(w => w.Value[level] == c).ToList();
                int frequency = startsWithSymbol.Count == 1
                    ? startsWithSymbol[0].Frequency
                    : 0;
                Node child;
                node.Children.Add(child = new Node { Level = level + 1, Frequency = frequency, SubStr = node.SubStr + c, Symbol = c });

                if (startsWithSymbol.Any())
                {
                    BuildNode(child, startsWithSymbol.ToArray());
                }
            }

            if (level > 0)
            {
                List<Word> currentRoot = words.Where(w => w.Value.Length > level - 1).ToList();
                List<Word> startsWithSymbolRoot = currentRoot.Where(w => w.Value[level - 1] == node.Symbol).ToList();
                int toTake = Math.Min(startsWithSymbolRoot.Count, 10);
                startsWithSymbolRoot.Sort((x, y) => -x.Frequency.CompareTo(y.Frequency));
                var res = startsWithSymbolRoot.OrderBy(x => -x.Frequency).ThenBy(x => x.Value).ToList();

                node.SortedByFrequencyChildren.AddRange(res.Take(toTake).Select(s => new Node { Level = level, Frequency = s.Frequency, SubStr = s.Value, Symbol = (char)0 }));
            }
        }

        private string[] GetCompletions(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string[0];
            }

            Node currentNode = Root;
            for (int i = 0, n = text.Length; i < n; ++i)
            {
                currentNode = currentNode.Children.FirstOrDefault(c => c.Symbol == text[i]);
                if (currentNode == null)
                {
                    return new string[0];
                }
            }

            List<Node> nodes = currentNode.SortedByFrequencyChildren;

            List<string> result = nodes.Select(n => n.SubStr).ToList();
            int toTake = Math.Min(10, result.Count);

            return result.Take(toTake).ToArray();
        }

        public async Task<string[]> GetCompletionsAsync(string text)
        {
            return await Task.Run(() => GetCompletions(text));
        }
    }
}
