using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Models
{
    public class Constant
    {
        public string Value { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }

        public Constant(string value, int id, string description)
        {
            Value = value;
            Id = id;
            Description = description;
        }
    }
}
