using lab2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2
{
    public class Parser
    {
        public string mainString = "";
        public List<Node> tree = new List<Node>();
        long checkNum;
        Dictionary<string, string> valueDictionary = new Dictionary<string, string>();

        public string CheckType(string type, string variable, int line)
        {
            string error = "";

            if (type == "int")
            {
                if (variable.Contains('.') || !(long.TryParse(variable, out checkNum)) || variable.Contains('\"'))
                {
                    error = $"\nОшибка недопустимого типа в строке: {line}";
                }
                else if (long.Parse(variable) > 2147483647 || long.Parse(variable) < -2147483647)
                {
                    error = $"\nОшибка переполнения int в строке: {line}";
                }
            }
            else if (type == "string")
            {
                if (long.TryParse(variable, out checkNum) || variable[0] != '\"' || variable[variable.Count() - 1] != '\"')
                {
                    error = $"\nОшибка недопустимого типа в строке: {line}";
                }
            }
            else if (type == "bool")
            {
                if (variable != "true" && variable != "false")
                {
                    error = $"\nОшибка недопустимого типа в строке: {line}";
                }
            }
            else if (type == "float")
            {
                if (variable.Contains('\"'))
                {
                    error = $"\nОшибка недопустимого типа в строке: {line}";
                }

                if (float.Parse(variable) > Math.Pow(3.4, 38) || float.Parse(variable) < Math.Pow(3.4, 38) * (-1))
                {
                    error = $"\nОшибка переполнения float в строке: {line}";
                }
            }
            else if (type == "double")
            {
                if (variable.Contains('\"'))
                {
                    error = $"\nОшибка недопустимого типа в строке: {line}";
                }

                if (float.Parse(variable) > Math.Pow(1.7, 308) || float.Parse(variable) < Math.Pow(1.7, 308) * (-1))
                {
                    error = $"\nОшибка переполнения double в строке: {line}";
                }
            }
        

            return error;
      }

        public void BuildTree(string path)
        {
            if (!File.Exists(path))
            {
                System.Console.WriteLine("\nПо указанному пути файл не был найден");
                return;
            }
            if (File.ReadAllText(path).Count() == 0)
            {
                System.Console.WriteLine("\nФайл по указанному пути оказался пуст");
                return;
            }

            string stringMain = File.ReadAllText(path);
            string[] lexemArray = stringMain.Split('\n');
            string lastType = "";
            string lastName = "";
            Node node = new Node();
            string error = "";
            int codeLine = 1;
            Stack <int> forFlag = new Stack<int>();
            bool do_flag = false;
            bool void_flag = false;
            string branch = "";

            using (StreamWriter sw = new StreamWriter("C:\\Users\\Alexandr\\Desktop\\_\\5 курс УлГТУ ИВТ\\Вычислительная математика\\lab5\\lab2\\lab2\\Examples\\Tree.txt"))
            {
                for (int i = 0; i < lexemArray.Length; i++)
                {
                    if (lexemArray[i] != "")
                    {
                        string[] tmp_lexems = lexemArray[i].Split(';');
                        string[] tmp_lexems2 = new string[5];

                        if (tmp_lexems[0] == "Variable")
                        {
                            if (!valueDictionary.ContainsKey(lexemArray[3]))
                            {
                                valueDictionary[tmp_lexems[3]] = tmp_lexems[2];
                            }
                            node = new Node(tmp_lexems[0], tmp_lexems[1], tmp_lexems[2], tmp_lexems[3]);
                            lastType = tmp_lexems[2];
                            lastName = tmp_lexems[3];
                        }
                        else if (tmp_lexems[0] == "Constant")
                        {
                            error += CheckType(lastType, tmp_lexems[2], codeLine);
                            node = new Node(tmp_lexems[0], tmp_lexems[2]);
                        }
                        else if (tmp_lexems[0] == "Operation" && (tmp_lexems[2] == "increment_operation" || tmp_lexems[2] == "decrement_operation"))
                        {
                            if (lastType != "int")
                            {
                                error += $"\nОшибка недопустимого типа в строке: {codeLine}";
                            }
                            node = new Node(tmp_lexems[0], null, tmp_lexems[2]);
                        }
                        else if (tmp_lexems[0] == "Variable")
                        {
                            lastName = tmp_lexems[3];
                            lastType = tmp_lexems[2];
                        }
                        else if (tmp_lexems[0] == "Identifier")
                        {
                            node = new Node(tmp_lexems[0], tmp_lexems[2]);
                        }
                        else
                        {
                            node = new Node(tmp_lexems[0], tmp_lexems[1], tmp_lexems[2]);
                        }

                        if (i < lexemArray.Length - 1)
                        {
                            tmp_lexems2 = lexemArray[i + 1].Split(';');
                        }

                        if (tmp_lexems[0] == "Identifier" && tmp_lexems[2] == "if")
                        {
                            forFlag.Push(0);
                            if(forFlag.Count > 1)
                            {
                                sw.Write(branch + "|\n" + branch + "----------------\n");
                                branch += "               ";
                                sw.Write(branch + "|\n");
                            }
                            
                        }
                        if (tmp_lexems[0] == "Delimeter" && (tmp_lexems[2] == "}") && forFlag.Count!=0)
                        {
                            forFlag.Pop();
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -type: {node.Type}\n");
                            if (forFlag.Count != 0)
                            {
                                sw.Write(branch + "|\n");
                                branch = branch.Remove(branch.Length - 15, 15);
                                sw.Write(branch + "----------------\n" + branch + "|\n" );
                            } else
                            {
                                sw.Write(branch + "|\n" + branch + "----------------\n");
                                branch += "               ";
                                sw.Write(branch + "|\n");
                            }

                            tree.Add(node);
                            continue;

                        }

                        if (tmp_lexems[0] == "Variable" && tmp_lexems[2] == "class" && tmp_lexems2[0] == "Delimeter")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -type: {node.Type}\n");
                            sw.Write(branch + $"|\n" + branch + "----------------\n");
                            branch += "               ";
                            sw.Write(branch + "|\n");
                            tree.Add(node);
                            continue;
                        }

                        if (tmp_lexems[0] == "Variable" && tmp_lexems[2] == "void")
                        {
                            void_flag = true;
                        }

                        if (tmp_lexems[0] == "Delimeter" && tmp_lexems[2] == "{")
                        {
                            codeLine++;
                        }

                        if (tmp_lexems[0] == "Delimeter" && tmp_lexems[2] == "{" && void_flag)
                        {
                            void_flag = false;
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -type: {node.Type}\n");
                            sw.Write(branch + "|\n" + branch + "----------------\n");
                            branch += "               ";
                            sw.Write(branch + "|\n");
                            tree.Add(node);
                            continue;
                        }

                        if (tmp_lexems[0] == "Constant")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -value: {node.Value}\n");
                        }
                        else if (tmp_lexems[0] == "DataType")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -{node.Type}\n");
                        }
                        else if (tmp_lexems[0] == "Operation")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -{node.Type}\n");
                        }
                        else if (tmp_lexems[0] == "Delimeter")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -type: {node.Type}\n");
                        }
                        else if (tmp_lexems[0] == "Identifier")
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -type: {node.Value}\n");
                        }
                        else
                        {
                            sw.Write(branch + $"{node.NodeName} Node: \n{branch} -Type Variable: {node.Type} \n{branch} -{node.VName}\n");
                        }

                        if (tmp_lexems[2] == "semicolon" && i != lexemArray.Length - 2 && forFlag.Count==0)
                        {
                            codeLine++;
                            sw.Write(branch + "|\n" + branch +  "----------------\n");
                            branch += "               ";
                            sw.Write(branch + "|\n");
                        }

                        tree.Add(node);
                    }

                    if (error != "")
                    {
                        Console.WriteLine(error);
                    }
                }

            }
        }
    }
}
