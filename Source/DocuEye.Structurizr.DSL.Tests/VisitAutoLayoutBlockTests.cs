using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitAutoLayoutBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineAutoLayoutWithDefaultsBlockThenPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout
            ");
            var context = parser.autoLayoutBlock();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrAutoLayout)visitor.VisitAutoLayoutBlock(context);
            // Assert
            Assert.That(result.RankDirection, Is.EqualTo("tb"));
            Assert.That(result.RankSeparation, Is.EqualTo(300));
            Assert.That(result.NodeSeparation, Is.EqualTo(300));

        }

        [Test]
        public void WhenDefineAutoLayoutBlockThenPropertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout lr 100 100
            ");
            var context = parser.autoLayoutBlock();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrAutoLayout)visitor.VisitAutoLayoutBlock(context);
            // Assert
            Assert.That(result.RankDirection, Is.EqualTo("lr"));
            Assert.That(result.RankSeparation, Is.EqualTo(100));
            Assert.That(result.NodeSeparation, Is.EqualTo(100));
            
        }

        [Test]
        public void WhenDefineAutoLayoutBlockWithInvalidRankDirectionThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout ww 100 100
            ");
            var context = parser.autoLayoutBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitAutoLayoutBlock(context));
        }

        [Test]
        public void WhenDefineAutoLayoutBlockWithInvalidRankSeparationThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout lr aaaa 100
            ");
            var context = parser.autoLayoutBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitAutoLayoutBlock(context));
        }

        [Test]
        public void WhenDefineAutoLayoutBlockWithInvalidNodeSeparationThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout lr 100 aaa
            ");
            var context = parser.autoLayoutBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitAutoLayoutBlock(context));
        }
    }
}
