using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override StructurizrConfigurationUser VisitConfigurationUser([NotNull] StructurizrDSLParser.ConfigurationUserContext context)
        {
            var usernameContext = context.username();
            if(usernameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Username is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var roleContext = context.userrole();
            if (roleContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Role is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return new StructurizrConfigurationUser
            {
                Username = usernameContext.GetText().Trim('"'),
                Role = roleContext.GetText().Trim('"')
            };
        }

        public override IEnumerable<StructurizrConfigurationUser> VisitConfigurationUsers([NotNull] StructurizrDSLParser.ConfigurationUsersContext context)
        {
            List<StructurizrConfigurationUser> users = new List<StructurizrConfigurationUser>();
            var userContexts = context.configurationUser();
            if (userContexts != null)
            {
                foreach(var userContext in userContexts)
                {
                    var user = this.VisitConfigurationUser(userContext);
                    users.Add(user);
                }
            }
            return users;
        }

        public override object VisitConfigurationVisibilityBlock([NotNull] StructurizrDSLParser.ConfigurationVisibilityBlockContext context)
        {
            var allowedValues = new string[] {
                "private", "public"
            };

            var value = context.value().GetText().Trim('"');

            if (!allowedValues.Contains(value.ToLower()))
            {
                throw new Exception(
                    string.Format(
                        "Invalid visibility value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return value;
        }

        public override object VisitConfigurationScopeBlock([NotNull] StructurizrDSLParser.ConfigurationScopeBlockContext context)
        {
            var allowedValues = new string[] {
                "landscape", "softwaresystem", "none"
            };

            var value = context.value().GetText().Trim('"');

            if (!allowedValues.Contains(value.ToLower()))
            {
                throw new Exception(
                    string.Format(
                        "Invalid scope value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return value;
        }

        public override StructurizrConfiguration VisitConfiguration([NotNull] StructurizrDSLParser.ConfigurationContext context)
        {
            var configuration = new StructurizrConfiguration();

            var scopeContext = context.configurationScopeBlock();
            if (scopeContext != null && scopeContext.Length > 0)
            {
                configuration.Scope = (string)this.VisitConfigurationScopeBlock(scopeContext.First());
            }

            var visibilityContext = context.configurationVisibilityBlock();
            if (visibilityContext != null && visibilityContext.Length > 0)
            {
                configuration.Visibility = (string)this.VisitConfigurationVisibilityBlock(visibilityContext.First());
            }

            var usersContext = context.configurationUsers();
            if (usersContext != null && usersContext.Length > 0)
            {
                configuration.Users = this.VisitConfigurationUsers(usersContext.First());
            }

            return configuration;
        }

    }
}
