using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Node
    {
        public string NodeName { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string VName { get; set; }

        public Node(string nodeName = null, string value=null, string type = null, string vName = null)
        {
            NodeName = nodeName;
            Value = value;
            Type = type;
            VName = vName;
        }
    }
}
