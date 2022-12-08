using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Constants
{
    public enum LexemTypes:int
    {
        ParsingError = -1,
        DataType = 0,
        Variable = 1,
        Delimeter = 2,
        Identifier = 3,
        Constant = 4,
        Operation = 5
    }
}
