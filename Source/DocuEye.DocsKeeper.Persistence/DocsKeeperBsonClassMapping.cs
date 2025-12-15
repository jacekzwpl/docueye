using DocuEye.DocsKeeper.Model;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Persistence
{
    /// <summary>
    /// Entities mapping for WorkspacesKeeper module
    /// </summary>
    public class DocsKeeperBsonClassMapping
    {
        /// <summary>
        /// Registers mappings
        /// </summary>
        public static void Register()
        {
            BsonClassMap.RegisterClassMap<Decision>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
