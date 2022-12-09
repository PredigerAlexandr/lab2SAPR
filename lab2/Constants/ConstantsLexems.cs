using lab2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Constants
{
    public class ConstantsLexems
    {
       
        public static Dictionary<string, DataType> DataTypes = new Dictionary<string, DataType>()
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
            {"=", new Operator("=", 0, "assign_operation") },
            {"+", new Operator("+", 1, "sum_operation") },
            {"-", new Operator("-", 2, "sub_operatoration") },
            {"*", new Operator("*", 3, "mul_operation") },
            {"/", new Operator("/", 4, "div_operation") },
            {"+=", new Operator("+=", 5, "add_amount_operation") },
            {"-=", new Operator("-=", 6, "subtract_amount_operation") },
            {"==", new Operator("==", 7, "equal_operation") },
            {">", new Operator(">", 8, "more_operation") },
            {"<", new Operator("<", 9, "less_operation") },
            {"++", new Operator("++", 10, "increment_operation") },
            {"--", new Operator("--", 11, "decrement_operation") },
            {"%", new Operator("%", 12, "modul_operation") },
            {"%=", new Operator("%=", 13, "modul_amount_operation") },
            {"*=", new Operator("*=", 13, "mul_amount_operation") },
            {"/=", new Operator("/=", 13, "div_amount_operation") },
        };

        public static Dictionary<string, KeyWord> KeyWords = new Dictionary<string, KeyWord>()
        {
            {"class", new KeyWord("class", 0, "class") },
            {"public", new KeyWord("public", 1, "public") },
            {"private", new KeyWord("private", 2, "private") },
            {"do", new KeyWord("do", 3, "do") },
            {"return", new KeyWord("return", 4, "return") },
            {"if", new KeyWord("if", 5, "if") },
            {"else", new KeyWord("else", 6, "else") },
            {"while", new KeyWord("while", 7, "while") },
            {"false", new KeyWord("false", 8, "false") },
            {"true", new KeyWord("true", 9, "true") },
            {"static", new KeyWord("static", 10, "static") },
            {"void", new KeyWord("void", 11, "void") },
        };

        public static Dictionary<string, KeySymbol> KeySymbols = new Dictionary<string, KeySymbol>()
        {
            {".", new KeySymbol(".", 0, ".") },
            {";", new KeySymbol(";", 1, "semicolon") },
            {",", new KeySymbol(",", 2, ",") },
            {"(", new KeySymbol("(", 3, "(") },
            {")", new KeySymbol(")", 4, ")") },
            {"[", new KeySymbol("[", 5, "[") },
            {"]", new KeySymbol("]", 6, "]") },
            {"{", new KeySymbol("{", 7, "{") },
            {"}", new KeySymbol("}", 8, "}") },
        };
    }
}
