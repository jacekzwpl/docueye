using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;
using DocuEye.WorkspacesKeeper.Model;
using DocuEye.WorkspacesKeeper.Persistence;

namespace DocuEye.WorkspacesKeeper.Application.Tests
{
    public class FakeDbContext : IWorkspacesKeeperDBContext
    {
        private List<Workspace> workspaces { get; }
        private List<ViewConfiguration> viewConfigurations { get; }


        public IGenericCollection<Workspace> Workspaces
        {
            get
            {
                return new FakeGenericCollection<Workspace>(this.workspaces);
            }
        }

        public IGenericCollection<ViewConfiguration> ViewConfigurations
        {
            get
            {
                return new FakeGenericCollection<ViewConfiguration>(this.viewConfigurations);
            }
        }


        public FakeDbContext() { 
        
            this.workspaces = this.CreateWorkspacesData();
            this.viewConfigurations = this.CreateViewConfigurationsData();

        }



        private List<Workspace> CreateWorkspacesData()
        {
            return new List<Workspace>()
            {
                new Workspace()
                {
                    Id = "workspacetest1",
                    Name = "My Worspace Name"
                },
                new Workspace()
                {
                    Id = "workspacetest2",
                    Name = "Another Worspace"
                },
                new Workspace()
                {
                    Id = "workspacetest3",
                    Name = "Workspace number 3"
                }
            };
        }

        private List<ViewConfiguration> CreateViewConfigurationsData()
        {
            return new List<ViewConfiguration>()
            {
               new ViewConfiguration()
               {
                   Id = "workspacetest1"
               }
            };
        }

    }
}
