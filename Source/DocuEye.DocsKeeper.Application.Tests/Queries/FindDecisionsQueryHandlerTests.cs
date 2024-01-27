﻿using DocuEye.DocsKeeper.Application.Queries.FindDecisions;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class FindDecisionsQueryHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHanldeFindDecisionsQueryForWorkspaceAccurateNumberOfDecisionsAreReturned()
        {
            // Arrange
            var query = new FindDecisionsQuery("workspacetest1");

            // Act
            var handler = new FindDecisionsQueryHandler(this.dbContext, this.mapper);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return exact 2 items.");
        }

        [Test]
        public async Task WhenHanldeFindDecisionsQueryForElementAccurateNumberOfDecisionsAreReturned()
        {
            // Arrange
            var query = new FindDecisionsQuery("workspacetest1", "elementId1");

            // Act
            var handler = new FindDecisionsQueryHandler(this.dbContext, this.mapper);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return exact 2 items.");
        }
    }
}
