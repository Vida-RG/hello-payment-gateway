using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaymentGateway.BankAccess.Models;
using PaymentGateway.BankAccess.Services;
using PaymentGateway.DataStorage.Models;
using PaymentGateway.DataStorage.Services;
using PaymentGateway.Domain.Infrastructure;
using PaymentGateway.Domain.Services;

namespace PaymentGateway
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<CosmosDBOptions>(Configuration);
            services.AddSingleton<CosmosClient>(serviceProvider =>
            {
                CosmosDBOptions options = serviceProvider.GetRequiredService<IOptionsMonitor<CosmosDBOptions>>().CurrentValue;

                return new CosmosClient(
                    options.CosmosEndpoint,
                    options.CosmosAccessKey);
            });
            services.AddTransient<ITransactionResultRepository, TransactionResultCosmosRepository>();

            services.Configure<BankClientOptions>(Configuration);
            services.AddHttpClient<BankClient>();
            services.AddTransient<IBankClientService, BankClientService>();

            services.AddTransient<IPaymentProvider, PaymentProvider>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Hello Payment Gateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello Payment Gateway");
            });

            app.UseMvc();
        }
    }
}
