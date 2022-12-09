using lab2.Models;
using System;
using System.IO;

namespace lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LexemProcessor processor = new LexemProcessor();
            processor.process_file();
            Lexem checkLex = new Lexem("dd", 3, "1231");
            Variable checkVar = new Variable("fefe", 112, "ddeeewer");
            using(StreamWriter sw = new StreamWriter("C:\\Users\\User\\OneDrive\\Desktop\\УлГТУ\\САПР\\lab2SAPR\\lab2\\Examples\\Lexems.txt"))
            {
                foreach (var elem in LexemProcessor.resultList)
                {
                    if (elem.GetType().Name == "Lexem")
                    {
                        sw.Write($"{((Lexem)elem).Type};{((Lexem)elem).Id};{((Lexem)elem).Value}\n");
                        
                    }
                    if (elem.GetType().Name == "Variable")
                    {
                        sw.Write($"Variable;{((Variable)elem).Id};{((Variable)elem).DataType};{((Variable)elem).Name}\n");
                    }

                }
            }
            Parser parser = new Parser();
            parser.BuildTree("C:\\Users\\User\\OneDrive\\Desktop\\УлГТУ\\САПР\\lab2SAPR\\lab2\\Examples\\Lexems.txt");
        }
    }
}
