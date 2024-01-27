using Microsoft.AspNetCore.DataProtection.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DocuEye.Infrastructure.DataProtection
{
    public class MongoDbXmlRepository : IXmlRepository
    {
        private readonly IDataProtectionDBContext dbContext; 

        public MongoDbXmlRepository(IDataProtectionDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var keys = this.dbContext.DataProtectionKeys.Find(o => true).GetAwaiter().GetResult();
            return keys.Select(element => XElement.Parse(element.Key)).ToList().AsReadOnly();
           
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var keyElement = new DataProtectionKey
            {
                Id = Guid.NewGuid().ToString(),
                Key = element.ToString(SaveOptions.DisableFormatting)
            };
            this.dbContext.DataProtectionKeys.InsertOneAsync(keyElement).GetAwaiter().GetResult();
        }
    }
}
