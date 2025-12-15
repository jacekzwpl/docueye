using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrHealthCheck
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Interval { get; set; } = "60s";
        public string Timeout { get; set; } = "0ms";

        public StructurizrHealthCheck(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }
}
