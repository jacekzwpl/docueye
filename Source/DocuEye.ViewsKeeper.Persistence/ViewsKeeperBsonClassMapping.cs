using DocuEye.ViewsKeeper.Model;
using MongoDB.Bson.Serialization;

namespace DocuEye.ViewsKeeper.Persistence
{
    /// <summary>
    /// Entities mapping for ViewsKeeper module
    /// </summary>
    public static class ViewsKeeperBsonClassMapping
    {
        /// <summary>
        /// Registers mappings
        /// </summary>
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<BaseView>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
