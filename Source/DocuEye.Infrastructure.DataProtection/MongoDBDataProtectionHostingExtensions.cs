using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace DocuEye.Infrastructure.DataProtection
{
    public static class MongoDBDataProtectionHostingExtensions
    {

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

        /*
        public static IDataProtectionBuilder PersistKeysToMongoDB(this IDataProtectionBuilder builder, IDataProtectionDBContext dBContext)
        {
            builder.Services.Configure<KeyManagementOptions>(o =>
            {
                o.XmlRepository = new MongoDbXmlRepository(dBContext);
            });

            return builder;
        }*/
    }
}
