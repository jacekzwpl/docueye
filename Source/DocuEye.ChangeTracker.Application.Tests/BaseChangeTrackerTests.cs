using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.Tests
{
    public abstract class BaseChangeTrackerTests
    {
        protected FakeDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
        }
    }
}
