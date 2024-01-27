using DocuEye.ChangeTracker.Model;
using MongoDB.Bson.Serialization;

namespace DocuEye.ChangeTracker.Persistence
{
    /// <summary>
    /// DB mappings
    /// </summary>
    public static class ChangeTrackerBsonClassMapping
    {
        /// <summary>
        /// Registers db mappings
        /// </summary>
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<ModelChange>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
