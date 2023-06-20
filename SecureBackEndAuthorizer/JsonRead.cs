using System;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace SecureBackEndAuthorizer
{


    public static class JsonRead
    {
        public static IConfiguration AppSetting { get; }

        static JsonRead()
        {
            try
            {
                AppSetting = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();

            }
            catch
            {
                AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            }
        }
    }
    
}
