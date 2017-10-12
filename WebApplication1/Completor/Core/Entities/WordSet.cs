using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completor.Core.Entities
{
    /// <summary>
    /// Initial data
    /// </summary>
    public class WordSet
    {
        /// <summary>
        /// Used
        /// </summary>
        public Word[] WI { get; private set; }
        /// <summary>
        /// Not used
        /// The purpose ot this block is unknown
        /// Can be easily included to use if necessary
        /// </summary>
        public Word[] UJ { get; private set; }

        public WordSet(Word[] wi, Word[] uj)
        {
            WI = wi;
            UJ = uj;
        }
    }
}
