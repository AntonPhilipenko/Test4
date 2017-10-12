using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Completor.Core.Tools
{
    /// <summary>
    /// Read file contents
    /// </summary>
    public class FileProcessor
    {
        public string[] ReadAllLines(string path)
        {
            try
            {
                return File.Exists(path)
                    ? File.ReadAllLines(path)
                    : new string[0];
            }
            catch (IOException)
            {
                // TODO:
                return new string[0];
            }
        }
    }
}
