using System;
using System.Collections.Generic;
using System.Text;

namespace Completor.Core.Entities
{
    /// <summary>
    /// Word to complete
    /// </summary>
    public class Word
    {
        public string Value { get; set; }
        public int Frequency { get; set; }
    }
}
