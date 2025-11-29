using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitHealthCheckBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineHealthCheckBlockThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            healthCheck ""MyHealthCheck"" https://docueye.com
            ");
            var context = parser.healthCheckBlock();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrHealthCheck)visitor.VisitHealthCheckBlock(context);
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyHealthCheck"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.Interval, Is.EqualTo("60s"));
            Assert.That(result.Timeout, Is.EqualTo("0ms"));
        }

        [Test]
        public void WhenDefineHealthCheckBlockWithAllPropsThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            healthCheck ""MyHealthCheck"" https://docueye.com 120s 10ms
            ");
            var context = parser.healthCheckBlock();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrHealthCheck)visitor.VisitHealthCheckBlock(context);
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyHealthCheck"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.Interval, Is.EqualTo("120s"));
            Assert.That(result.Timeout, Is.EqualTo("10ms"));
        }
    }
}
