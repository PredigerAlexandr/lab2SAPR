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
        string mainString = File.ReadAllText("C:\\Users\\User\\OneDrive\\Desktop\\УлГТУ\\САПР\\lab2SAPR\\lab2\\Examples\\Example1.txt");
        List<Lexem> listLexems = new List<Lexem>();
        List<Variable> listVariables = new List<Variable>();
        bool flagInc = false;
        string buffer = "";
        string last_variable;
        string prev_oper;
        int checkNumeric;
        int variableCount;

        List<Lexem> process_file()
        {
            for (int i = 0; i < mainString.Length; i++)
            {
                //if (flagInc)
                //{
                //    flagInc = false;
                //    continue;
                //}

                if (mainString[i] == '\n' || mainString[i] == ' ' || mainString[i] ==';'
                    || mainString[i] == '{' || mainString[i] == '}' || mainString[i] == '(' 
                    || mainString[i] == ')' || mainString[i] == '+' || mainString[i] == '-'
                    || mainString[i] == '*' || mainString[i] == '/')
                {
                    if (Constants.Constants.DataTypes.ContainsKey(buffer))
                    {
                        AddLexem(buffer);
                    }
                    else if (mainString[i]=='+' && mainString[i+1] == '+'|| mainString[i] == '-' && mainString[i+1] == '-')
                    {
                        AddLexem(buffer);
                        AddVariable(buffer);
                        buffer = "" + mainString[i] + mainString[i++];
                        continue;
                    }
                    else if()
                } 

            }




            return listLexems;
        }

        public void AddLexem(string unknown)
        {
            if (Constants.Constants.DataTypes.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("DataType", Constants.Constants.DataTypes[unknown].Id, Constants.Constants.DataTypes[unknown].Description));
                last_variable = unknown;
            } else if (Constants.Constants.Operators.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Operation", Constants.Constants.Operators[unknown].Id, Constants.Constants.Operators[unknown].Description));
                if (Constants.Constants.Operators[unknown].Value == "=") prev_oper = "assign_operation";
            } else if (Constants.Constants.KeyWords.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Identifier", Constants.Constants.KeyWords[unknown].Id, Constants.Constants.KeyWords[unknown].Description));
                last_variable = unknown;
            } else if (Constants.Constants.KeySymbols.ContainsKey(unknown))
            {
                listLexems.Add(new Lexem("Delimeter", Constants.Constants.KeySymbols[unknown].Id, Constants.Constants.KeySymbols[unknown].Value));
            } else if (int.TryParse(unknown, out checkNumeric)){
                listLexems.Add(new Lexem("Constant", 0, unknown));
            }
        }

        public void AddVariable(string variable)
        {
            if(variable != "")
            {
                var elem = listVariables.Where(v => v.Name == variable).ToList();
                if (elem.Count != 0)
                {
                    listVariables.Add(new Variable(variable, variableCount++, elem.First().DataType));
                }
                else
                {
                    listVariables.Add(new Variable(variable, variableCount++, last_variable));
                }
            }
        }
    }
}
