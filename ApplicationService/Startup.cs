using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application.Data;
using Application.Data.Repositories;
using Application.Data.RepositoryInterfaces;
using Application.Integration.ScoringService;
using Application.Service.Mapping;
using Application.Service.Models;
using Application.Service.RabbitMQ;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Application.Service
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
            var mapperConfig = new MapperConfiguration((v) =>
            {
               v.AddProfile(new MappingProfile());
            });

           IMapper mapper = mapperConfig.CreateMapper();
           services.AddSingleton(mapper);

            services.AddControllers();

            services.AddSingleton<IApplicationRepository, ApplicationRepository>();
            services.AddSingleton<IScoringService, ScoringService>();
            services.AddSingleton<IPublisher, Publisher>();
            services.AddSingleton<IConsumer, Consumer>();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApplicationService", Version = "v1" });
            });

            services.AddFluentValidation(fv => 
                fv.RegisterValidatorsFromAssemblyContaining<ApplicationQuery>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplicationService v1");
                });

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
