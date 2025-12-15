using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DocuEye.CLI.ApiClient.Model
{
    public class ProblemDetailsResponse
    {
        public string? Type { get; set; }

        public string? Title { get; set; }

        public int? Status { get; set; }

        public string? Detail { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
