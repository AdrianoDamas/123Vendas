using _123Vendas.Application.Business;
using _123Vendas.Core.Business;
using _123Vendas.Core.Repositories._123Vendas;
using _123Vendas.Infrastructure.MessageBus;
using _123Vendas.Infrastructure.Persistence.Repositories._123Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace _123Vendas.API.Configurations
{
    public class DependencyInjectionConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Services

            //DbContexts
            services.AddDbContext<Infrastructure.Persistence.Context._123VendasDbContext > (options =>
                options.UseSqlServer(configuration.GetConnectionString("123Vendas")));

            // Authorization
            services.AddAuthorization();

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            });

            services.AddControllers();

            //Api Endpoints Configuration
            services.AddEndpointsApiExplorer();

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "123Vendas API",
                    Description = "Developed by Adriano Damas - Owner @ Adriano Damas",
                    Contact = new OpenApiContact { Name = "Adriano Damas", Email = "contato@adrianodamas.com.br" },
                    License = new OpenApiLicense { Name = "Adriano Damas", Url = new Uri("https://adrianodamas.com.br/") }
                });
            });

            //Services
            //Repositories - 123Vendas (SQLSERVER)
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Business - 123Vendas
            services.AddScoped<IVendaBusiness, VendaBusiness>();

            //MessageBus
            services.AddScoped<IMessageBus, MessageBus>();

            //Configs
            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
            #endregion
        }
    }
}
