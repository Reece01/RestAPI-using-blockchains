using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebLoginApi.BlockChain;

namespace WebLoginApi
{
    public class Program
    {
        public static Blockchain Chain { get; } = new Blockchain();

        public static void Main(string[] args)
        {
            new ReadChainFile();
            Chain.AddBlock(new Block(DateTime.Now, null, "Dummy", "utHd7plwKUB#WMA2cdReyaGNFAoNxlfvADZ6WsvzIG&7bK^MeL", null, 0));
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
