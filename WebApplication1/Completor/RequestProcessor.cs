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
    /// <summary>
    /// Superclass of the layer
    /// </summary>
    public class RequestProcessor
    {
        private readonly FileProcessor fileProcessor = new FileProcessor();
        private readonly Parser parser = new Parser();
        private readonly string filePath;
        private readonly bool initSucceeded;

        private TreeWrapper wrapper;

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="path">Init filename</param>
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

        /// <summary>
        /// Init from file and build tree
        /// </summary>
        private void Init()
        {
            // Read
            string[] content = fileProcessor.ReadAllLines(filePath);
            // Parse
            WordSet wordSet = parser.Parse(content);
            // Build tree
            wrapper = new TreeWrapper(wordSet);
        }

        /// <summary>
        /// Return completions if init succeeded
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string[]> Response(string request)
        {
            return initSucceeded
                ? await wrapper.GetCompletionsAsync(request)
                : new string[] { "Error: Init failed." };
        }
    }
}
