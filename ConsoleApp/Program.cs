using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Program>()
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var mySettingsConfig = new MySettingsConfig();
            configuration.GetSection("MySettings").Bind(mySettingsConfig);

            Console.WriteLine("Setting from appsettings.json: " + mySettingsConfig.AccountName);
            Console.WriteLine("Setting from secrets.json: " + mySettingsConfig.ApiSecret);
            Console.WriteLine("Connection string: " + configuration.GetConnectionString("DefaultConnection"));

            Console.ReadKey();
        }
    }
}
