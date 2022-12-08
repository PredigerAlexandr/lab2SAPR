using lab2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab2
{
    public class LexemProcessor
    {
        string mainString = File.ReadAllText("C:\\Users\\User\\OneDrive\\Desktop\\УлГТУ\\САПР\\lab2SAPR\\lab2\\Examples\\Example1.txt");
        List<Lexem> listLexems = new List<Lexem>();
        List<Variable> listVariables = new List<Variable>();
        bool flagInc = false;
        
        List<Lexem> process_file()
        {
            for(int i=0; i < mainString.Length; i++)
            {
                if (flagInc)
                {
                    flagInc = false;
                    continue;
                }
            }




            return listLexems;
        }
    }
}
