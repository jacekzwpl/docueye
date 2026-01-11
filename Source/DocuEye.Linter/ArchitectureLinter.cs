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
    public class ArchitectureLinter : IDisposable
    {
        public LinterConfiguration Configuration { get; private set; } = new LinterConfiguration();

        private LinterModel model;
        private readonly ILogger<ArchitectureLinter> logger;
        private HttpClient httpClient;
        private Dictionary<string,string> variablesMap = new Dictionary<string, string>();
        private List<object> evaluationContext = new List<object>();
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
                    throw new Exception($"Unsupported rule type: '{rule.Type}' for rule with key: '{rule.Key}'");
                }
                if (!new string[] { LinterRuleSeverity.Error, LinterRuleSeverity.Warning }.Contains(rule.Severity))
                {
                    throw new Exception($"Unsupported rule severity: '{rule.Severity}' for rule with key: '{rule.Key}'");
                }
            }
        }

        private void CreateContext()
        {
            this.evaluationContext = new List<object>();
            this.variablesMap = new Dictionary<string, string>();
            this.variablesMap.Add("@ModelRelationships", "@0");
            this.evaluationContext.Add(this.model.Relationships);

            int iter = 1;
            foreach (var variable in Configuration.Variables)
            {
                string varKey = "@" + iter;
                this.variablesMap.Add("@" + variable.Key, varKey);
                if(variable.Value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
                {
                    var value = jsonElement.EnumerateArray()
                        .Select(e => e.GetString())
                        .ToArray();
                    this.evaluationContext.Add(value);
                }
                else
                {
                    this.evaluationContext.Add(variable.Value);
                }
                
                iter++;
            }
        }

        public bool Analyze()
        {
            CreateContext();
            this.Issues = new List<LinterIssue>();
            foreach (var rule in Configuration.Rules.Where(rule => rule.Enabled))
            {
                this.logger.LogInformation("Evaluating rule: {RuleKey}", rule.Key);
                var ruleIssues = rule.Evaluate(model, evaluationContext, variablesMap);
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

        public void Dispose()
        {
            this.httpClient.Dispose();
        }
    }
}
