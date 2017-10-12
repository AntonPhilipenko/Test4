using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Completor.Core.Entities
{
    public class WordSet
    {
        public Word[] WI { get; private set; }
        public Word[] UJ { get; private set; }

        public WordSet(Word[] wi, Word[] uj)
        {
            WI = wi;
            UJ = uj;
        }
    }
}
