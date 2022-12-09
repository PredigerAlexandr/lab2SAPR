using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Variable:BaseElement
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string DataType { get; set; }

        public Variable(string name, int id, string dataType)
        {
            Name = name;
            Id = id;
            DataType = dataType;
        }
    }
}
