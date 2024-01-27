using DocuEye.ChangeTracker.Model;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.MongoDB;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Model;
using DocuEye.WorkspacesKeeper.Persistence;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DocuEye.Persistence
{
    /// <summary>
    /// MongoDB Context
    /// </summary>
    public class MongoDBContext : 
        IChangeTrackerDBContext, 
        IViewsKeeperDBContext, 
        IDocsKeeperDBContext, 
        IModelKeeperDBContext,
        IWorkspacesKeeperDBContext,
        IWorkspaceImporterDBContext
    {
        private MongoClient client;
        private IMongoDatabase database;

        /// <summary>
        /// Name of collection containing workspaces
        /// </summary>
        private const string workspacesCollectionName = "Workspaces";
        /// <summary>
        /// Name of collection containing elements
        /// </summary>
        private const string elementsCollectionName = "Elements";
        /// <summary>
        /// Name of collection containing relationships
        /// </summary>
        private const string relationshipsCollectionName = "Relationships";
        /// <summary>
        /// Name of collection containing views
        /// </summary>
        private const string viewsCollectionName = "Views";
        /// <summary>
        /// Name of collection containing view configuration for workspaces
        /// </summary>
        private const string viewConfigurationsCollectionName = "ViewConfigurations";
        /// <summary>
        /// Name of collection containing images
        /// </summary>
        private const string imagesCollectionName = "Images";
        /// <summary>
        /// Name of collection containing decisions
        /// </summary>
        private const string decisionsCollectionName = "Decisions";
        /// <summary>
        /// Name of collection containing documentation content
        /// </summary>
        private const string documentationsCollectionName = "Documentations";
        /// <summary>
        /// Name of collection containing model changes history
        /// </summary>
        private const string modelChangesCollectionName = "ModelChanges";
        /// <summary>
        /// Name of collection containing workspaces imports
        /// </summary>
        private const string workspaceImportsCollectionName = "WorkspaceImports";
        
        /// <summary>
        /// Creates db context instance
        /// </summary>
        /// <param name="connectionString">MongoDb connection string</param>
        /// <param name="databaseName">Database name</param>
        public MongoDBContext(string connectionString, string databaseName) { 
            this.client = new MongoClient(connectionString);
            this.database = this.client.GetDatabase(databaseName);
        }

        /// <summary>
        /// Collection of Workspaces
        /// </summary>
        public IGenericCollection<Workspace> Workspaces { 
            get
            {
                return new GenericCollection<Workspace>(this.database.GetCollection<Workspace>(workspacesCollectionName));
            }
        }
        /// <summary>
        /// Collection of Elements
        /// </summary>
        public IGenericCollection<Element> Elements
        {
            get
            {
                return new GenericCollection<Element>(this.database.GetCollection<Element>(elementsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Relationships
        /// </summary>
        public IGenericCollection<Relationship> Relationships
        {
            get
            {
                return new GenericCollection<Relationship>(this.database.GetCollection<Relationship>(relationshipsCollectionName));
            }
        }
        /// <summary>
        /// Collection of System Landscape Views
        /// </summary>
        public IGenericCollection<SystemLandscapeView> SystemLandscapeViews
        {
            get
            {
                return new GenericCollection<SystemLandscapeView>(this.database.GetCollection<SystemLandscapeView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of System Context Views
        /// </summary>
        public IGenericCollection<SystemContextView> SystemContextViews
        {
            get
            {
                return new GenericCollection<SystemContextView>(this.database.GetCollection<SystemContextView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Container Views
        /// </summary>
        public IGenericCollection<ContainerView> ContainerViews
        {
            get
            {
                return new GenericCollection<ContainerView>(this.database.GetCollection<ContainerView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Component Views
        /// </summary>
        public IGenericCollection<ComponentView> ComponentViews
        {
            get
            {
                return new GenericCollection<ComponentView>(this.database.GetCollection<ComponentView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Dynamic Views
        /// </summary>
        public IGenericCollection<DynamicView> DynamicViews
        {
            get
            {
                return new GenericCollection<DynamicView>(this.database.GetCollection<DynamicView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Deployment Views
        /// </summary>
        public IGenericCollection<DeploymentView> DeploymentViews
        {
            get
            {
                return new GenericCollection<DeploymentView>(this.database.GetCollection<DeploymentView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Filtered Views
        /// </summary>
        public IGenericCollection<FilteredView> FilteredViews
        {
            get
            {
                return new GenericCollection<FilteredView>(this.database.GetCollection<FilteredView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Image Views
        /// </summary>
        public IGenericCollection<ImageView> ImageViews
        {
            get
            {
                return new GenericCollection<ImageView>(this.database.GetCollection<ImageView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Views with common attributes
        /// </summary>
        public IGenericCollection<BaseView> AllViews
        {
            get
            {
                return new GenericCollection<BaseView>(this.database.GetCollection<BaseView>(viewsCollectionName));
            }
        }
        /// <summary>
        /// Collection of View configurations for workspace
        /// </summary>
        public IGenericCollection<ViewConfiguration> ViewConfigurations
        {
            get
            {
                return new GenericCollection<ViewConfiguration>(this.database.GetCollection<ViewConfiguration>(viewConfigurationsCollectionName));
            }
        }
        /// <summary>
        /// Collection of Images
        /// </summary>
        public IGenericCollection<Image> Images
        {
            get
            {
                return new GenericCollection<Image>(this.database.GetCollection<Image>(imagesCollectionName));
            }
        }
        /// <summary>
        /// Collection of documentations
        /// </summary>
        public IGenericCollection<Documentation> Documentations
        {
            get
            {
                return new GenericCollection<Documentation>(this.database.GetCollection<Documentation>(documentationsCollectionName));
            }
        }
        /// <summary>
        /// Collection of decisions
        /// </summary>
        public IGenericCollection<Decision> Decisions
        {
            get
            {
                return new GenericCollection<Decision>(this.database.GetCollection<Decision>(decisionsCollectionName));
            }
        }
        /// <summary>
        /// Collection of model changes
        /// </summary>
        public IGenericCollection<ModelChange> ModelChanges
        {
            get
            {
                return new GenericCollection<ModelChange>(this.database.GetCollection<ModelChange>(modelChangesCollectionName));
            }
        }
        /// <summary>
        /// Collection of workspace imports 
        /// </summary>
        public IGenericCollection<WorkspaceImport> WorkspaceImports
        {
            get
            {
                return new GenericCollection<WorkspaceImport>(this.database.GetCollection<WorkspaceImport>(workspaceImportsCollectionName));
            }
        }
        /// <summary>
        /// Creates DB indexes
        /// </summary>
        /// <returns></returns>
        public async Task CreateIndexes()
        {
            //Create Indexes 
            await this.CreateElementsCollectionIndexes();
            await this.CreateRelationshipsCollectionIndexes();
            await this.CreateViewsCollectionIndexes();
            await this.CreateImagesCollectionIndexes();
            await this.CreateDocumentationsCollectionIndexes();
            await this.CreateDecisionsCollectionIndexes();
            await this.CreateModelChangesCollectionIndexes();
        }
        /// <summary>
        /// Creates indexes for Elements collection
        /// </summary>
        private async Task CreateElementsCollectionIndexes()
        {
            var collection = this.database.GetCollection<Element>(elementsCollectionName);
            var logBuilder = Builders<Element>.IndexKeys;
            var indexModel = new CreateIndexModel<Element>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(indexModel);
        }
        /// <summary>
        /// Creates indexes for Relationships collection
        /// </summary>
        private async Task CreateRelationshipsCollectionIndexes()
        {
            var collection = this.database.GetCollection<Relationship>(relationshipsCollectionName);
            var logBuilder = Builders<Relationship>.IndexKeys;
            var indexModel = new CreateIndexModel<Relationship>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(indexModel);
        }
        /// <summary>
        /// Creates indexes for Views collection
        /// </summary>
        private async Task CreateViewsCollectionIndexes()
        {
            var collection = this.database.GetCollection<BaseView>(viewsCollectionName);
            var logBuilder = Builders<BaseView>.IndexKeys;
            var indexModel = new CreateIndexModel<BaseView>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(indexModel);
        }
        /// <summary>
        /// Creates indexes for Images collection
        /// </summary>
        private async Task CreateImagesCollectionIndexes()
        {
            var collection = this.database.GetCollection<Image>(imagesCollectionName);
            var logBuilder = Builders<Image>.IndexKeys;
            var workspaceIndexModel = new CreateIndexModel<Image>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(workspaceIndexModel);
            var documentationIndexModel = new CreateIndexModel<Image>(logBuilder.Ascending(x => x.DocumentationId));
            await collection.Indexes.CreateOneAsync(documentationIndexModel);
            var nameIndexModel = new CreateIndexModel<Image>(logBuilder.Ascending(x => x.Name));
            await collection.Indexes.CreateOneAsync(nameIndexModel);
        }
        /// <summary>
        /// Creates indexes for Documentations collection
        /// </summary>
        private async Task CreateDocumentationsCollectionIndexes()
        {
            var collection = this.database.GetCollection<Documentation>(documentationsCollectionName);
            var logBuilder = Builders<Documentation>.IndexKeys;
            var workspaceIndexModel = new CreateIndexModel<Documentation>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(workspaceIndexModel);
            var elementIndexModel = new CreateIndexModel<Documentation>(logBuilder.Ascending(x => x.ElementId));
            await collection.Indexes.CreateOneAsync(elementIndexModel);
        }
        /// <summary>
        /// Creates indexes for Decisions collection
        /// </summary>
        private async Task CreateDecisionsCollectionIndexes()
        {
            var collection = this.database.GetCollection<Decision>(decisionsCollectionName);
            var logBuilder = Builders<Decision>.IndexKeys;
            var workspaceIndexModel = new CreateIndexModel<Decision>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(workspaceIndexModel);
            var elementIndexModel = new CreateIndexModel<Decision>(logBuilder.Ascending(x => x.ElementId));
            await collection.Indexes.CreateOneAsync(elementIndexModel);
        }
        /// <summary>
        /// Creates indexes for ModelChanges collection
        /// </summary>
        private async Task CreateModelChangesCollectionIndexes()
        {
            var collection = this.database.GetCollection<ModelChange>(modelChangesCollectionName);
            var logBuilder = Builders<ModelChange>.IndexKeys;
            var workspaceIndexModel = new CreateIndexModel<ModelChange>(logBuilder.Ascending(x => x.WorkspaceId));
            await collection.Indexes.CreateOneAsync(workspaceIndexModel);
            var elementIndexModel = new CreateIndexModel<ModelChange>(logBuilder.Ascending(x => x.ElementId));
            await collection.Indexes.CreateOneAsync(elementIndexModel);
            var importIndexModel = new CreateIndexModel<ModelChange>(logBuilder.Ascending(x => x.ImportId));
            await collection.Indexes.CreateOneAsync(importIndexModel);
        }
    }
}
