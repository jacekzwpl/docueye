using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectDecisionChnagesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public void WhenDetectDecisionChnagesThenDecisionsAreReturned()
        {
            // Arrange
            var explodedDecisions = new List<ExplodedDecision>() { 
                new ExplodedDecision() {
                    Id = "1",
                    Content = "Content"
                },
                new ExplodedDecision() {
                    Id = "2",
                    Content = "Content"
                }
            };
            


            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectDecisionChnages("wId", "documentationId", null, explodedDecisions, this.existingDecisions);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "There should be 2 decisions returned.");


        }
    }
}
