using DocuEye.WorkspacesKeeper.Model;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspacesKeeper.Persistence
{

    /// <summary>
    /// Entities mapping for WorkspacesKeeper module
    /// </summary>
    public static class WorkspacesKeeperBsonClassMapping
    {
        /// <summary>
        /// Registers mappings
        /// </summary>
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<Workspace>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
