using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Completor.Core;
using Completor.Core.Tools;
using Completor.Core.Entities;
using Completor.Core.Trees;

namespace Completor
{
    public class RequestProcessor
    {
        private readonly FileProcessor fileProcessor = new FileProcessor();
        private readonly Parser parser = new Parser();
        private readonly string filePath;
        private readonly bool initSucceeded;

        private TreeWrapper wrapper;

        public RequestProcessor(string path)
        {
            filePath = path;

            try
            {
                Init();
                initSucceeded = true;
            }
            catch (Exception)
            {
                initSucceeded = false;
                // TODO:
            }
        }

        private void Init()
        {
            string[] content = fileProcessor.ReadAllLines(filePath);
            WordSet wordSet = parser.Parse(content);
            wrapper = new TreeWrapper(wordSet);
        }

        public async Task<string[]> Response(string request)
        {
            return initSucceeded
                ? await wrapper.GetCompletionsAsync(request)
                : new string[] { "Error: Init failed." };
        }
    }
}
