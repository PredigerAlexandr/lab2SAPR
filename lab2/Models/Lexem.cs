using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Lexem:BaseElement
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }

        public Lexem(string type, int id, string value)
        {
            Type = type;
            Id = id;
            Value = value;
        }

        public override string ToString()
        {
            return $"Lexem Type: {Type};\t lexem id: {Id};\t value:{Value}";
        }
    }
}
