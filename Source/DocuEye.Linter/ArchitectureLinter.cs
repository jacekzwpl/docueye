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
        private List<object?> evaluationContext = new List<object?>();
        public List<LinterIssue> Issues { get; private set; } = new List<LinterIssue>();

        public ArchitectureLinter(LinterModel model, ILogger<ArchitectureLinter> logger)
        {
            this.model = model;
            this.httpClient = new HttpClient();
            this.logger = logger;
        }

        public ArchitectureLinter(LinterModel model, ILogger<ArchitectureLinter> logger, HttpClient httpClient)
        {
            this.model = model;
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task LoadConfigurationFromFile(string filePath)
        {
            Configuration = await ReadConfigurationFromFile(filePath);
        }

        public async Task LoadConfiguration(string json)
        {
            Configuration = await ReadConfiguration(json);
        }

        private async Task<LinterConfiguration> ReadConfigurationFromFile(string filePath)
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
            return await ReadConfiguration(jsonText);
        }

        private async Task<LinterConfiguration> ReadConfiguration(string json)
        {
            var configuration = JsonSerializer.Deserialize<LinterConfiguration>(json) ?? new LinterConfiguration();
            
            if (!string.IsNullOrWhiteSpace(configuration.Extends))
            {
                var extendedConfiguration = await ReadConfigurationFromFile(configuration.Extends);
                configuration = extendedConfiguration.Merge(configuration);
            }

            configuration.Variables = ParseConfigurationVariables(configuration.Variables);

            foreach (var rule in configuration.Rules)
            {
                if(string.IsNullOrWhiteSpace(rule.Id))
                {
                    throw new Exception("Rule id cannot be null or empty.");
                }
                if (string.IsNullOrWhiteSpace(rule.Name))
                {
                    throw new Exception($"Rule name cannot be null or empty for rule with id: '{rule.Id}'");
                }
                if (string.IsNullOrWhiteSpace(rule.Expression))
                {
                    throw new Exception($"Rule expression cannot be null or empty for rule with id: '{rule.Id}'");
                }
                if (!LinterRuleType.AllTypes.Contains(rule.Type))
                {
                    throw new Exception($"Unsupported rule type: '{rule.Type}' for rule with id: '{rule.Id}'");
                }
                if (!new string[] { LinterRuleSeverity.Error, LinterRuleSeverity.Warning }.Contains(rule.Severity))
                {
                    throw new Exception($"Unsupported rule severity: '{rule.Severity}' for rule with id: '{rule.Id}'");
                }
            }
            return configuration;
        }

        private Dictionary<string, object?> ParseConfigurationVariables(Dictionary<string, object?> variables)
        {
            var parsedVariables = new Dictionary<string, object?>();
            foreach (var kvp in variables)
            {
                parsedVariables[kvp.Key] = GetVariableValue(kvp.Value);
            }
            return parsedVariables;
        }

        private object? GetVariableValue(object? value)
        {
            return GetVariableValue(value, out _);
        }

        private object? GetVariableValue(object? value, out Type? type)
        {
            if (value == null)
            {
                type = null;
                return null;
            }
                
            if (value is JsonElement jsonElement)
            {
                switch (jsonElement.ValueKind)
                {
                    case JsonValueKind.String:
                        type = typeof(string);
                        return jsonElement.GetString() ?? string.Empty;
                    case JsonValueKind.Number:
                        if (jsonElement.TryGetInt32(out int intValue))
                        {
                            type = typeof(int);
                            return intValue;
                        }
                        else if (jsonElement.TryGetDouble(out double doubleValue))
                        {
                            type = typeof(double);
                            return doubleValue;
                        }
                        else
                        {
                            type = typeof(int);
                            return 0;
                        }
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        type = typeof(bool);
                        return jsonElement.GetBoolean();
                    case JsonValueKind.Object:
                        var dict = new Dictionary<string, object?>();
                        Type? dictItemType = null;
                        foreach (var prop in jsonElement.EnumerateObject())
                        {
                            dict[prop.Name] = GetVariableValue(prop.Value, out var elementType);
                            if(dictItemType == null && elementType != null)
                            {
                                dictItemType = elementType;
                            }
                        }
                        type = typeof(Dictionary<,>).MakeGenericType(typeof(string), dictItemType ?? typeof(object));
                        var destDict = (System.Collections.IDictionary)Activator.CreateInstance(type);
                        foreach(var key in dict.Keys)
                        {
                            destDict[key] = dict[key];
                        }

                        return destDict;
                    case JsonValueKind.Array:
                        var list = new List<object?>();
                        Type? itemType = null;
                        
                        foreach (var item in jsonElement.EnumerateArray())
                        {
                            list.Add(GetVariableValue(item, out var elementType));
                            if(itemType == null && elementType != null)
                            {
                                itemType = elementType;
                            }
                        }
                        
                        // if itemType is still null, it means all items were null
                        if (itemType == null || list.Count == 0)
                        {
                            type = typeof(List<object?>);
                            return list;
                        }
                        
                        // Create a typed List<T>
                        var genericListType = typeof(List<>).MakeGenericType(itemType);
                        var typedList = (System.Collections.IList)Activator.CreateInstance(genericListType);
                        
                        // Copy elements to the typed list
                        foreach (var item in list)
                        {
                            typedList.Add(item);
                        }
                        
                        type = genericListType;
                        return typedList;
                    default:
                        type = null;
                        return null;
                }
            }
            type = value.GetType();
            return value;
        }

        

        private void CreateContext()
        {
            this.evaluationContext = new List<object?>();
            this.variablesMap = new Dictionary<string, string>();
            // Add model relationships and elements to the context with reserved keys
            this.variablesMap.Add("@ModelRelationships", "@0");
            this.evaluationContext.Add(this.model.Relationships);
            this.variablesMap.Add("@ModelElements", "@1");
            this.evaluationContext.Add(this.model.Elements);


            int iter = 2;
            foreach (var variable in Configuration.Variables)
            {
                string varKey = "@" + iter;
                this.variablesMap.Add("@" + variable.Key, varKey);
                this.evaluationContext.Add(variable.Value);
                iter++;
            }
        }

        public bool Analyze()
        {
            CreateContext();
            this.Issues = new List<LinterIssue>();
            foreach (var rule in Configuration.Rules.Where(rule => rule.Enabled))
            {
                this.logger.LogInformation("Evaluating rule: {RuleId}", rule.Id);
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
