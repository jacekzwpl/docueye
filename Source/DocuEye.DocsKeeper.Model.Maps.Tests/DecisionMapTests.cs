using DocuEye.Infrastructure.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Application.Model;

namespace DocuEye.DocsKeeper.Model.Maps.Tests
{
    public class DecisionMapTests
    {
        [Test]
        public void Mapping_Decision_To_DecisionsListItem_Works()
        {
            var source = new Decision
            {
                Id = "dec-123",
                DslId = "dsl-456",
                WorkspaceId = "ws-789",
                DocumentationId = "doc-101",
                ElementId = "elem-112"
            };
            var result = source.MapToDecisionsListItem();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>());
                
        }

        [Test]
        public void Mapping_Decision_To_FoundedDecision_Works()
        {
            var source = new Decision
            {
                Id = "dec-123",
                DslId = "dsl-456",
                WorkspaceId = "ws-789",
                DocumentationId = "doc-101",
                ElementId = "elem-112",
                Date = DateTime.UtcNow,
                Status = "Approved",
                Title = "Decision Title",
                Links = new List<DecisionLink>
                {
                    new DecisionLink { Description = "Example Link" }
                }
            };
            var result = source.MapToFoundedDecision();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(FoundedDecision.Links)
                }); 
            Assert.That(result.Links.Count(), Is.EqualTo(source.Links.Count()));
        }
    }
}
