using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitViewsTests : BaseTests
    {
        [Test]
        public void WhenDefineViewsThenPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            views {
                systemLandscape viewId ""view description"" {
                    title ""Great system title""
                    include mydidentifier1 mydidentifier2
                    exclude mydidentifier3 mydidentifier4
                    properties {
                        ""key1"" ""value1""
                        ""key2"" ""value2""
                    }
                    animation {
                        mydidentifier1 mydidentifier2
                        mydidentifier3 mydidentifier4
                    }   
                }

                styles {
                    element tag1 {
                    }
                }

                terminology {
                    person ""Person""
                }   

                branding {
                    logo ""logo.png""
                }
                
                
            }
            
            ");
            var context = parser.views();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitViews(context);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SystemLandscapeViews.Count, Is.EqualTo(1));
            Assert.That(result.Styles?.ElementStyles.Count, Is.EqualTo(1));
            Assert.That(result.Terminology?.Person, Is.EqualTo("Person"));
            Assert.That(result.Branding?.Logo, Is.EqualTo("logo.png"));
        }
    }
}
