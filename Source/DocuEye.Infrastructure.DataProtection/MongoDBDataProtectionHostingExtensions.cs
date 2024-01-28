using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DocuEye.Infrastructure.DataProtection
{
    /// <summary>
    /// Mongo DB Data Protection Hosting Extensions
    /// </summary>
    public static class MongoDBDataProtectionHostingExtensions
    {
        /// <summary>
        /// Configures the data protection system to persist keys to Mongo Database
        /// </summary>
        /// <param name="builder">IDataProtectionBuilder instance to modify.</param>
        /// <returns>The value <paramref name="builder"/>.</returns>
        public static IDataProtectionBuilder PersistKeysToMongoDB(this IDataProtectionBuilder builder)
        {
            builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(services =>
            {
                return new ConfigureOptions<KeyManagementOptions>(options =>
                {
                    options.XmlRepository = new MongoDbXmlRepository(services);
                });
            });

            return builder;
        }
    }
}
