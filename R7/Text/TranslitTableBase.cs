using System;

namespace R7.Text
{
    [Obsolete]
    public abstract class TranslitTableBase
    {
        protected string [,] translitTable;

        public string [,] TranslitTable {
            get { return translitTable; }
        }

        protected TranslitTableBase (string [,] translitTable)
        {
            this.translitTable = translitTable;
        }
    }
}

