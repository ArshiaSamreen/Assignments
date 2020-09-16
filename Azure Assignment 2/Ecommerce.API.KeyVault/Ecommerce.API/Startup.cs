using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.Data;
using Microsoft.Azure.KeyVault.Models;

namespace Ecommerce.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddDbContext<EcommerceDbContext>(async options =>
            //{
            //    var keyVaultUri = new Uri("https://keyvaultdbdemo.vault.azure.net/");
            //    var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            //    SecretBundle connectionStringSecret = await keyVaultClient.GetSecretAsync(keyVaultUri + "secrets/EcommerceConnection");
            //    options.UseSqlServer(connectionStringSecret.Value);
            //});

            services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EcommerceDBConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
