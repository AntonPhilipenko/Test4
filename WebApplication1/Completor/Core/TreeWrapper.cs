using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Completor.Core.Trees;
using Completor.Core.Entities;

namespace Completor.Core
{
    public class TreeWrapper
    {
        private Tree WordTree { get; set; }

        public TreeWrapper(WordSet wordSet)
        {
            WordTree = new Tree(wordSet.WI);
        }

        public async Task<string[]> GetCompletionsAsync(string line)
        {
            return await WordTree.GetCompletionsAsync(line);
        }
    }
}
