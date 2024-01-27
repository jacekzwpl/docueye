using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

namespace DocuEye.ViewsKeeper.Application.Tests
{
    public class FakeDbContext : IViewsKeeperDBContext
    {
        private List<BaseView> allViews { get; }


        public FakeDbContext() { 
        
            var systemLandscapeViews = this.CreateSystemLandscapeViewsData();
            var systemContextViews = this.CreateSystemContextViewsData();
            var containerViews = this.CreateContainerViewsData();
            var componentViews = this.CreateComponentViewsData();
            var dynamicViews = this.CreateDynamicViewsData();
            var deploymentViews = this.CreateDeploymentViewsData();
            var filteredViews = this.CreateFilteredViewsData();
            var imageViews = this.CreateImageViewsData();
            this.allViews = [
                .. systemLandscapeViews, 
                .. systemContextViews,
                .. containerViews,
                .. componentViews,
                .. dynamicViews,
                .. deploymentViews,
                .. filteredViews,
                .. imageViews
            ];

        }

        public IGenericCollection<SystemLandscapeView> SystemLandscapeViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<SystemLandscapeView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<SystemContextView> SystemContextViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<SystemContextView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<ContainerView> ContainerViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<ContainerView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<ComponentView> ComponentViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<ComponentView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<DynamicView> DynamicViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<DynamicView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<DeploymentView> DeploymentViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<DeploymentView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<FilteredView> FilteredViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<FilteredView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<ImageView> ImageViews
        {
            get
            {
                return new FakeGenericCollectionWithBaseType<ImageView, BaseView>(this.allViews);
            }
        }

        public IGenericCollection<BaseView> AllViews
        {
            get
            {
                return new FakeGenericCollection<BaseView>(this.allViews);
            }
        }

        private List<SystemLandscapeView> CreateSystemLandscapeViewsData()
        {
            return new List<SystemLandscapeView>()
            {
                new SystemLandscapeView()
                {
                    Id = "SystemLandscapeViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.SystemLandscapeView
                }
            };
        }

        private List<SystemContextView> CreateSystemContextViewsData()
        {
            return new List<SystemContextView>()
            {
                new SystemContextView()
                {
                    Id = "SystemContextViewId1",
                    WorkspaceId = "workspacetest1",
                    SoftwareSystemId = "elementId1",
                    ViewType = ViewType.SystemContextView
                }
            };
        }

        private List<ContainerView> CreateContainerViewsData()
        {
            return new List<ContainerView>()
            {
                new ContainerView()
                {
                    Id = "ContainerViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.ContainerView
                }
            };
        }

        private List<ComponentView> CreateComponentViewsData()
        {
            return new List<ComponentView>()
            {
                new ComponentView()
                {
                    Id = "ComponentViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.ComponentView
                }
            };
        }

        private List<DynamicView> CreateDynamicViewsData()
        {
            return new List<DynamicView>()
            {
                new DynamicView()
                {
                    Id = "DynamicViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.DynamicView
                }
            };
        }

        private List<DeploymentView> CreateDeploymentViewsData()
        {
            return new List<DeploymentView>()
            {
                new DeploymentView()
                {
                    Id = "DeploymentViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.DeploymentView
                }
            };
        }

        private List<FilteredView> CreateFilteredViewsData()
        {
            return new List<FilteredView>()
            {
                new FilteredView()
                {
                    Id = "FilteredViewId1",
                    ViewType = ViewType.FilteredView,
                    WorkspaceId = "workspacetest1"
                    //WorkspaceId = Guid.NewGuid().ToString(),
                }
            };
        }

        private List<ImageView> CreateImageViewsData()
        {
            return new List<ImageView>()
            {
                new ImageView()
                {
                    Id = "ImageViewId1",
                    WorkspaceId = "workspacetest1",
                    ViewType = ViewType.ImageView
                }
            };
        }
    }
}
