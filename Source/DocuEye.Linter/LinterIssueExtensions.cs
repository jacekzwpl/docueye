using DocuEye.Linter.Model;
using Microsoft.Extensions.Logging;

namespace DocuEye.Linter
{
    public static class LinterIssueExtensions
    {
        static readonly string LogMessageWithElement = "Linter rule '{RuleName}' issue on element '{ElementName}'. {Description} More info: { HelpLink}";
        static readonly string LogMessageWithRelationship = "Linter rule '{RuleName}' issue on relationship '{Relationship}'. {Description} More info: { HelpLink}";
        static readonly string LogMessageWithMessage = "Linter rule '{RuleName}' issue with {Message}. {Description} More info: { HelpLink}";


        public static void LogIssue(this LinterIssue issue, ILogger logger)
        {
            switch(issue.Rule.Severity)
            {
               
                case LinterRuleSeverity.Error:
                    if(issue.Element != null)
                    {
                        logger.LogError(LogMessageWithElement, issue.Rule.Name, issue.Element.Name, issue.Rule.Description?? "", issue.Rule.HelpLink?? "");
                        break;
                    }else if(issue.Relationship != null)
                    {
                        logger.LogError(LogMessageWithRelationship, issue.Rule.Name, issue.Relationship.Source.Name + " -> " + issue.Relationship.Destination.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (string.IsNullOrEmpty(issue.Message))
                    {
                        logger.LogError(LogMessageWithMessage, issue.Rule.Name, issue.Message, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    break;
                case LinterRuleSeverity.Warning:
                    if (issue.Element != null)
                    {
                        logger.LogWarning(LogMessageWithElement, issue.Rule.Name, issue.Element.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (issue.Relationship != null)
                    {
                        logger.LogWarning(LogMessageWithRelationship, issue.Rule.Name, issue.Relationship.Source.Name + " -> " + issue.Relationship.Destination.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }else if(string.IsNullOrEmpty(issue.Message))
                    {
                        logger.LogWarning(LogMessageWithMessage, issue.Rule.Name, issue.Message, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    break;
                case LinterRuleSeverity.Information:
                    if (issue.Element != null)
                    {
                        logger.LogInformation(LogMessageWithElement, issue.Rule.Name, issue.Element.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (issue.Relationship != null)
                    {
                        logger.LogInformation(LogMessageWithRelationship, issue.Rule.Name, issue.Relationship.Source.Name + " -> " + issue.Relationship.Destination.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (string.IsNullOrEmpty(issue.Message))
                    {
                        logger.LogInformation(LogMessageWithMessage, issue.Rule.Name, issue.Message, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    break;
                default:
                    if (issue.Element != null)
                    {
                        logger.LogInformation(LogMessageWithElement, issue.Rule.Name, issue.Element.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (issue.Relationship != null)
                    {
                        logger.LogInformation(LogMessageWithRelationship, issue.Rule.Name, issue.Relationship.Source.Name + " -> " + issue.Relationship.Destination.Name, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    else if (string.IsNullOrEmpty(issue.Message))
                    {
                        logger.LogInformation(LogMessageWithMessage, issue.Rule.Name, issue.Message, issue.Rule.Description ?? "", issue.Rule.HelpLink ?? "");
                        break;
                    }
                    break;
            }
        }
    }
}
