using lab2.Constants;
using lab2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2
{
    public class LexemProcessor
    {
        string mainString = File.ReadAllText("C:\\Users\\Alexandr\\Desktop\\_\\5 курс УлГТУ ИВТ\\Вычислительная математика\\lab5\\lab2\\lab2\\Examples\\Example3.txt");
        List<Lexem> listLexems = new List<Lexem>();
        List<Variable> listVariables = new List<Variable>();
        bool flagInc = false;
        string buffer = "";
        string last_variable;
        string prev_oper;
        int checkNumeric;
        int variableCount;
        public static List<BaseElement> resultList = new List<BaseElement>();

        public void process_file()
        {
            for (int i = 0; i < mainString.Length; i++)
            {
                //if (flagInc)
                //{
                //    flagInc = false;
                //    continue;
                //}

                if (mainString[i] == '\n' || mainString[i] == ' ' || mainString[i] == ';'
                    || mainString[i] == '{' || mainString[i] == '}' || mainString[i] == '('
                    || mainString[i] == ')' || mainString[i] == '+' || mainString[i] == '-'
                    || mainString[i] == '*' || mainString[i] == '/' || mainString[i] == '\r')
                {
                    if (ConstantsLexems.DataTypes.ContainsKey(buffer))
                    {
                        AddLexem(buffer);
                    }
                    else if (mainString[i] == '+' && mainString[i + 1] == '+' || mainString[i] == '-' && mainString[i + 1] == '-')
                    {
                        AddLexem(buffer);
                        AddVariable(buffer);
                        buffer = "" + mainString[i] + mainString[i++];
                        continue;
                    }
                    else if (mainString[i] == '*' && mainString[i + 1] == '=' || mainString[i] == '/' && mainString[i] == '='
                        || mainString[i] == '%' && mainString[i + 1] == '=' || mainString[i] == '+' && mainString[i + 1] == '='
                        || mainString[i] == '-' && mainString[i + 1] == '=')
                    {
                        AddLexem(buffer);
                        buffer = "" + mainString[i] + mainString[i++];
                    }
                    else if (ConstantsLexems.Operators.ContainsKey(buffer))
                    {
                        AddLexem(buffer);
                    }
                    else if (ConstantsLexems.KeyWords.ContainsKey(buffer))
                    {
                        AddLexem(buffer);
                    }
                    else if (ConstantsLexems.KeySymbols.ContainsKey(buffer))
                    {
                        AddLexem(buffer);
                    }
                    else if (int.TryParse(buffer, out checkNumeric))
                    {
                        prev_oper = "";
                        AddLexem(buffer);
                    }
                    else if (prev_oper == "assign_operation")
                    {
                        prev_oper = "";
                        listLexems.Add(new Lexem("Constant", 0, buffer));
                        resultList.Add(new Lexem("Constant", 0, buffer));
                    }
                    else
                    {
                        AddVariable(buffer);
                    }

                    if (mainString[i] == '\n' || mainString[i] == ' ' || mainString[i] == '\r') 
                    {
                        buffer = "";
                    }
                    else if(mainString[i] == '(' || mainString[i] == '{')
                    {
                        buffer = mainString[i].ToString();
                        AddLexem(buffer);
                        buffer = "";
                    }
                    else
                    {
                        buffer = mainString[i].ToString();
                    }
                }
                else
                {
                    buffer += mainString[i];
                }
            }
            AddLexem(buffer);
        }

        public void AddLexem(string unknown)
        {
            if (ConstantsLexems.DataTypes.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("DataType", ConstantsLexems.DataTypes[unknown].Id, ConstantsLexems.DataTypes[unknown].Description));
                resultList.Add(new Lexem("DataType", ConstantsLexems.DataTypes[unknown].Id, ConstantsLexems.DataTypes[unknown].Description));
                last_variable = unknown;
            }
            else if (ConstantsLexems.Operators.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Operation", ConstantsLexems.Operators[unknown].Id, ConstantsLexems.Operators[unknown].Description));
                resultList.Add(new Lexem("Operation", ConstantsLexems.Operators[unknown].Id, ConstantsLexems.Operators[unknown].Description));
                if (ConstantsLexems.Operators[unknown].Value == "=") prev_oper = "assign_operation";
            }
            else if (ConstantsLexems.KeyWords.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Identifier", ConstantsLexems.KeyWords[unknown].Id, ConstantsLexems.KeyWords[unknown].Description));
                resultList.Add(new Lexem("Identifier", ConstantsLexems.KeyWords[unknown].Id, ConstantsLexems.KeyWords[unknown].Description));
                last_variable = unknown;
            }
            else if (ConstantsLexems.KeySymbols.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Delimeter", ConstantsLexems.KeySymbols[unknown].Id, ConstantsLexems.KeySymbols[unknown].Description));
                resultList.Add(new Lexem("Delimeter", ConstantsLexems.KeySymbols[unknown].Id, ConstantsLexems.KeySymbols[unknown].Description));
            }
            else if (int.TryParse(unknown, out checkNumeric))
            {
                listLexems.Add(new Lexem("Constant", 0, unknown));
                resultList.Add(new Lexem("Constant", 0, unknown));
            }
        }

        public void AddVariable(string variable)
        {
            if (variable != "")
            {
                var elem = listVariables.Where(v => v.Name == variable).ToList();
                if (elem.Count != 0)
                {
                    listVariables.Add(new Variable(variable, variableCount++, elem.First().DataType));
                    resultList.Add(new Variable(variable, variableCount++, elem.First().DataType));
                }
                else
                {
                    listVariables.Add(new Variable(variable, variableCount++, last_variable));
                    resultList.Add(new Variable(variable, variableCount++, last_variable));
                }
            }
        }
    }
}
