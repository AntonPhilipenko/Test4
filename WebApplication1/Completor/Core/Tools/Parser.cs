using System;
using System.Collections.Generic;
using System.Text;
using Completor.Core.Entities;

namespace Completor.Core.Tools
{
    /// <summary>
    /// Parse file contents
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Returns instance or WordSet - initial data for complete
        /// </summary>
        /// <param name="text">File contents.</param>
        /// <returns></returns>
        public WordSet Parse(string[] text)
        {
            if (text.Length == 0)
            {
                throw new Exception("Set is empty.");
            }

            int wiCount = 0;
            if (!int.TryParse(text[0], out wiCount))
            {
                throw new Exception("Invalid lines count.");
            }

            if (text.Length < wiCount + 1)
            {
                throw new Exception("Insufficient lines.");
            }

            List<Word> wi = new List<Word>();
            for (int i = 0; i < wiCount; ++i)
            {
                string[] parts = text[i + 1].Split(' ');
                if (parts.Length != 2)
                {
                    throw new Exception(string.Format("Invalid line {0}.", i));
                }

                int frequency = 0;
                if (!int.TryParse(parts[1], out frequency))
                {
                    throw new Exception(string.Format("Invalid frequency in line {0}.", i));
                }

                wi.Add(new Word { Value = parts[0], Frequency = frequency });
            }

            if (text.Length < wiCount + 2)
            {
                throw new Exception("Phrase count is not defined.");
            }

            int ujCount = 0;
            if (!int.TryParse(text[wiCount + 1], out ujCount))
            {
                throw new Exception("Invalid lines count.");
            }

            if (text.Length < wiCount + ujCount + 2)
            {
                throw new Exception("Insufficient lines.");
            }

            List<Word> uj = new List<Word>();
            for (int j = 0; j < ujCount; ++j)
            {
                string[] parts = text[ujCount + 2].Split(' ');
                if (parts.Length != 2)
                {
                    throw new Exception(string.Format("Invalid line {0}.", j + ujCount + 1));
                }

                int frequency = 0;
                if (!int.TryParse(parts[1], out frequency))
                {
                    throw new Exception(string.Format("Invalid frequency in line {0}.", j + ujCount + 1));
                }

                uj.Add(new Word { Value = parts[0], Frequency = frequency });
            }

            wi.Sort((x, y) => x.Value.CompareTo(y.Value));
            uj.Sort((x, y) => x.Value.CompareTo(y.Value));

            return new WordSet(wi.ToArray(), uj.ToArray());
        }
    }
}
