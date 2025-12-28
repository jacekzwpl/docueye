using DocuEye.Linter.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DocuEye.Linter
{
    public class ArchitectureLinter
    {
        public LinterConfiguration Configuration { get; private set; } = new LinterConfiguration();

        private LinterModel model;
        private readonly ILogger<ArchitectureLinter> logger;
        private HttpClient httpClient;
        public List<LinterIssue> Issues { get; private set; } = new List<LinterIssue>();

        public ArchitectureLinter(LinterModel model, ILogger<ArchitectureLinter> logger)
        {
            this.model = model;
            this.httpClient = new HttpClient();
            this.logger = logger;
        }

        public async Task LoadConfigurationFromFile(string filePath)
        {
            string jsonText = String.Empty;
            Uri? validatedUri;
            var isValid = Uri.TryCreate(filePath, UriKind.Absolute, out validatedUri);
            if (isValid && validatedUri?.Scheme == Uri.UriSchemeHttp)
            {
                jsonText = await this.httpClient.GetStringAsync(filePath);
            }
            else
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Linter configuration file not found: {filePath}");
                }

                jsonText = File.ReadAllText(filePath);
            }
            LoadConfiguration(jsonText);
        }

        public void LoadConfiguration(string json)
        {
            Configuration = JsonSerializer.Deserialize<LinterConfiguration>(json) ?? new LinterConfiguration();
            foreach (var rule in Configuration.Rules) {
                if (!new string[] { LinterRuleType.ModelElement, LinterRuleType.ModelRelationship }.Contains(rule.Type)) { 
                    throw new System.Exception($"Unsupported rule type: '{rule.Type}' for rule with key: '{rule.Key}'");
                }
                if (!new string[] { LinterRuleSeverity.Error, LinterRuleSeverity.Warning }.Contains(rule.Severity))
                {
                    throw new System.Exception($"Unsupported rule severity: '{rule.Severity}' for rule with key: '{rule.Key}'");
                }
            }
        }

        public bool Analyze()
        {
            this.Issues = new List<LinterIssue>();
            foreach (var rule in Configuration.Rules.Where(rule => rule.Enabled))
            {
                var ruleIssues = rule.Evaluate(model);
                Issues.AddRange(ruleIssues);
            }

            foreach (var issue in Issues)
            {
                issue.LogIssue(logger);
            }

            return Issues
                .Where(o =>
                    o.SeverityValue > LinterRuleSeverity
                        .GetSeverityValue(this.Configuration.MaxAllowedSeverity)
                ).Count() > 0 ? false : true;
        }
    }
}
