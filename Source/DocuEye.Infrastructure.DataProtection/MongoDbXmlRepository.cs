using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DocuEye.Infrastructure.DataProtection
{
    /// <summary>
    /// An IXmlRepository backed by an Mongo database.
    /// </summary>
    public class MongoDbXmlRepository : IXmlRepository
    {
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MongoDbXmlRepository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
        {

            var dbContext = this.serviceProvider.GetRequiredService<IDataProtectionDBContext>();
            var keys = dbContext.DataProtectionKeys.Find(o => true)
                .GetAwaiter().GetResult();
            return keys.Select(element => XElement.Parse(element.Key))
                .ToList().AsReadOnly();
        }

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var dbContext = this.serviceProvider.GetRequiredService<IDataProtectionDBContext>();
            var keyElement = new DataProtectionKey
            {
                Id = Guid.NewGuid().ToString(),
                Key = element.ToString(SaveOptions.DisableFormatting),
                FrendlyName = friendlyName
            };
            dbContext.DataProtectionKeys.InsertOneAsync(keyElement)
                .GetAwaiter().GetResult();
        }
    }
}
