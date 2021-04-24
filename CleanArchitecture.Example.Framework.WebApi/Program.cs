using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Example.Framework.WebApi
{

    public class Program
    {

        #region - - - - - - Methods - - - - - -

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        #endregion Methods

    }


}
