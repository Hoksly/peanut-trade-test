using CryptoAPI.Exchanges;
using CryptoAPI.Services;
using CryptoExchangeApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CryptoExchangeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IEstimateService, EstimateService>();
            services.AddScoped<IRatesService, RatesService>();
            
            var validCurrencies = new List<string> { "BTC", "ETH", "USDT" }; // Replace with your actual list of valid currencies
            services.AddSingleton<IRequestValidationService>(new RequestValidationService(validCurrencies));


            services.AddTransient<IExchange, BinanceExchange>();
            services.AddTransient<IExchange, KucoinExchange>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
