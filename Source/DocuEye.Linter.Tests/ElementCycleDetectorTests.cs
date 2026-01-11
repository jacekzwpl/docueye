using DocuEye.Linter.Model;

namespace DocuEye.Linter.Tests
{
    public class ElementCycleDetectorTests
    {
        [Test]
        public void WhenDirectCycleExistsThenIsFound()
        {
            var relationships = new List<LinterModelRelationship>()
            {
                new LinterModelRelationship()
                {
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "B" },
                    Technology = "Tech1"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "B" },
                    Destination = new LinterModelElement() { Identifier = "C" },
                    Technology = "Tech2"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "D" },
                    Technology = "Tech4"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "D" },
                    Destination = new LinterModelElement() { Identifier = "A" },
                    Technology = "Tech4"
                }
            };
            var result = ElementCycleDetector.CycleExists(relationships);
            Assert.That(result, Is.True);
            Assert.That(ElementCycleDetector.CurrentCycle, Is.Not.Null);
            Assert.That(ElementCycleDetector.CurrentCycle, Is.EquivalentTo(new string[] { "A", "D", "A" }));
        }

        [Test]
        public void WhenNoCycleExistsThenNotFound()
        {
            var relationships = new List<LinterModelRelationship>()
            {
                new LinterModelRelationship()
                {
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "B" },
                    Technology = "Tech1"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "B" },
                    Destination = new LinterModelElement() { Identifier = "C" },
                    Technology = "Tech2"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "D" },
                    Technology = "Tech4"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "D" },
                    Destination = new LinterModelElement() { Identifier = "C" },
                    Technology = "Tech4"
                }
            };
            var result = ElementCycleDetector.CycleExists(relationships);
            Assert.That(result, Is.False);
            Assert.That(ElementCycleDetector.CurrentCycle, Is.Null);
        }

        [Test]
        public void WhenIndirectCycleExistsThenIsFound()
        {
            var relationships = new List<LinterModelRelationship>()
            {
                new LinterModelRelationship()
                {
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "B" },
                    Technology = "Tech1"
                },
                new LinterModelRelationship()
                {
                    Source = new LinterModelElement() { Identifier = "A" },
                    Destination = new LinterModelElement() { Identifier = "D" },
                    Technology = "Tech1"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "B" },
                    Destination = new LinterModelElement() { Identifier = "C" },
                    Technology = "Tech2"
                },
                new LinterModelRelationship(){
                    Source = new LinterModelElement() { Identifier = "C" },
                    Destination = new LinterModelElement() { Identifier = "A" },
                    Technology = "Tech3"
                }
            };
            var result = ElementCycleDetector.CycleExists(relationships);
            Assert.That(result, Is.True);
            Assert.That(ElementCycleDetector.CurrentCycle, Is.Not.Null);
            Assert.That(ElementCycleDetector.CurrentCycle, Is.EquivalentTo(new string[] { "A", "B", "C", "A" }));
        }
    }
}
