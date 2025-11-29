using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public StructurizrHealthCheck? VisitHealthCheckBlocks(StructurizrDSLParser.HealthCheckBlockContext[]? contexts)
        {
            if (contexts == null)
            {
                return null;
            }
            if (contexts.Length > 1)
            {
                var duplicate = contexts[1];
                throw new Exception(
                    string.Format(
                        "Duplicate healthCheck block  at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (contexts.Length > 0)
            {
                return (StructurizrHealthCheck)this.VisitHealthCheckBlock(contexts[0]);
            }
            return null;
        }

        public override object VisitHealthCheckBlock([NotNull] StructurizrDSLParser.HealthCheckBlockContext context)
        {

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for healthCheck at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for healthCheck at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var urlContext = context.url();
            if (urlContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Url is required for healthCheck at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var url = urlContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception(
                    string.Format(
                        "Url is required for healthCheck at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var healthCheck = new StructurizrHealthCheck(name,url);

            var intervalContext = context.interval();
            if (intervalContext != null)
            {
                var interval = intervalContext.GetText().Trim('"');
                healthCheck.Interval = interval;
            }

            var timeoutContext = context.timeout();
            if (timeoutContext != null)
            {
                var timeout = timeoutContext.GetText().Trim('"');
                healthCheck.Timeout = timeout;
            }

            return healthCheck;
        }
    }
}
