using lab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Constants
{
    public class Constants
    {
        public static Dictionary<string, DataType> Datatypes = new Dictionary<string, DataType>()
        {
            {"int", new DataType("int", 0, "32-bit integer")},
            {"bool", new DataType("bool", 1, "boolean") },
            {"long", new DataType("long", 2, "64-bit integer")},
            {"string", new DataType("string", 3, "string of char") },
            {"float", new DataType("float", 4, "float") },
            {"double", new DataType("double", 5, "double") }
        };

        public static Dictionary<string, Operator> Operators = new Dictionary<string, Operator>()
        {
            {"=", new Operator("=", 0, "assign_operator") }
        };
    }
}
