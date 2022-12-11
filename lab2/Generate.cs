using lab2.Constants;
using lab2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace lab2
{
    public class Generate
    {
        public List<Node> tree;

        public Generate(List<Node> tree)
        {
            this.tree = tree;
        }

        public void GenetateJavaCode()
        {
            string tab = "";
            bool skip_tabe = false;
            Node nextNode = new Node();

            using (StreamWriter sw = new StreamWriter("C:\\Users\\Alexandr\\Desktop\\_\\5 курс УлГТУ ИВТ\\Вычислительная математика\\lab5\\lab2\\lab2\\Examples\\java_code.txt"))
            {
                for (int i = 0; i < tree.Count; i++)
                {
                    string space = " ";

                    if (i < tree.Count - 1)
                    {
                        nextNode = tree[i + 1];
                        if (nextNode.Type == "semicolon" || nextNode.Type == ")")
                        {
                            space = "";
                        }
                    }

                    switch (tree[i].NodeName)
                    {
                        case "DataType":
                            if (tree[i].Type == "boolean")
                            {
                                sw.Write("boolean" + space);
                            }
                            else if (tree[i].Type == "string of char")
                            {
                                sw.Write("String" + space);
                            }
                            else
                            {
                                var list2 = ConstantsLexems.DataTypes.Where(dt => dt.Value.Description == tree[i].Type);
                                if(list2.Count() > 0)
                                {
                                    sw.Write(list2.First().Key + space);
                                }
                            }
                            break;

                        case "Variable":
                            if (nextNode.Type == "increment_operation" || nextNode.Type == "decrement_operation")
                            {
                                sw.Write(tree[i].VName);
                            }
                            else if (tree[i].Type == "void" && tree[i].VName == "Main")
                            {
                                sw.Write("static void main");
                            }
                            else
                            {
                                sw.Write(tree[i].VName + space);
                            }
                            break;

                        case "Operation":
                            var list = ConstantsLexems.Operators.Where(n => n.Value.Description == tree[i].Type).ToList();
                            if (list.Count != 0)
                            {
                                sw.Write(list.First().Value.Value + space);
                            }
                            break;

                        case "Constant":
                            sw.Write(tree[i].Value + space);
                            break;

                        case "Delimeter":
                            if (tree[i].Type == "semicolon")
                            {
                                if (nextNode.Type == "}")
                                {
                                    tab = tab.Remove(tab.Length - 1, 1);
                                    skip_tabe = true;
                                }
                                sw.Write(";\n" + tab);
                            }
                            else if (tree[i].Type == "{")
                            {
                                tab += '\t';
                                sw.Write(tree[i].Type + '\n' + tab);
                            }
                            else if (tree[i].Type == "}")
                            {
                                if (nextNode.Type == "}")
                                {
                                    if(tab.Length > 0)
                                    {
                                        tab = tab.Remove(tab.Length - 1, 1);
                                    }
                                }
                                sw.Write(tree[i].Type + '\n' + tab);
                            }
                            else if (tree[i].Type == "(")
                            {
                                sw.Write(tree[i].Type);
                            }
                            else
                            {
                                sw.Write(tree[i].Type + space);
                            }
                            break;

                        case "Identifier":
                            if (tree[i].Value == "static" && nextNode.Value=="void" && tree[i+2].VName == "Main" || tree[i].Value == "void" && nextNode.VName == "main")
                            {
                                continue;
                            }
                            sw.Write(tree[i].Value + space);   
                            break;
                    }

                }
            }
        }
    }
}
