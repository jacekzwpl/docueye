using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrKrokiConfig
    {
        public string Format { get; set; }
        public string Value { get; set; }

        public StructurizrKrokiConfig(string format, string value)
        {
            Format = format;
            Value = value;
        }
    }
}
