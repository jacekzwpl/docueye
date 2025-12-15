namespace DocuEye.Structurizr.DSL.Tests
{
    public class WorkspaceBodyTests : BaseTests
    {
        [Test]
        public void WhenSetBasicPropsThenPropertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            name ""Test Workspace""
            description ""Test Workspace Description""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Name, Is.EqualTo("Test Workspace"));
            Assert.That(visitor.Workspace.Description, Is.EqualTo("Test Workspace Description"));
        }

        [Test]
        public void WhenSetOnlyNameThenOnlyNameIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            name ""Test Workspace""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Name, Is.EqualTo("Test Workspace"));
            Assert.That(visitor.Workspace.Description, Is.Null);
        }

        [Test]
        public void WhenDuplicateNameIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            name ""Test Workspace""
            name ""Test Workspace""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenDuplicateDescriptionIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description ""Test Workspace""
            description ""Test Workspace""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenSetPropertiesThenPropertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Properties.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(visitor.Workspace.Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenDuplicatePropertiesSectionIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenSetIdentifiersThenIdentifiersHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers hierarchical
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Identifiers, Is.EqualTo("hierarchical"));
        }

        [Test]
        public void WhenSetInvalidIdentifiersThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers invalid
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test] 
        public void WhenDuplicateIdentifiersIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers flat
            !identifiers flat
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenSetDocThenDocsHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !docs path/to/docs
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Docs, Is.EqualTo("path/to/docs"));
        }

        [Test]
        public void WhenSetDocsAsTextThenDocsHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !docs ""path/to/docs""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Docs, Is.EqualTo("path/to/docs"));
        }

        [Test]
        public void WhenDuplicateDocsIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !docs path/to/docs
            !docs path/to/docs
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenSetAdrsThenAdrsHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !adrs path/to/adrs
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Adrs, Is.EqualTo("path/to/adrs"));
        }

        [Test]
        public void WhenSetAdrsAsTextThenAdrsHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !adrs ""path/to/adrs""
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Adrs, Is.EqualTo("path/to/adrs"));
        }

        [Test]
        public void WhenDuplicateAdrsIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !adrs path/to/adrs
            !adrs path/to/adrs
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }

        [Test]
        public void WhenSetModelThenModelIsNotNull()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            model {}
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act
            visitor.VisitWorkspaceBody(context);

            // Assert
            Assert.That(visitor.Workspace.Model, !Is.Null);
        }

        [Test]
        public void WhenDuplicatModelIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            model {}
            model {}
            ");
            var context = parser.workspaceBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitWorkspaceBody(context));
        }
    }
}
