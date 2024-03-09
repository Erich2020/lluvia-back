using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TempporalWS.Repositories;

namespace TempporalWS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string mySqlConnect = Encrypt.AppSettingsReader("ConnectionStrings", "ConnectionMySql");
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                               options.UseMySQL(mySqlConnect));
            IMapper mapper = MappingConfiguration.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<ITicketRepositorio, TicketRepositorio>();
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            services.AddScoped<IProductoTicketRepositorio,ProductoTicketRepositorio>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LluviaTemporadas", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "corsConfiguration", builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LluviaTemporadas v1"));
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/api?v=ok", "LluviaTemporadas v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors("corsConfiguration");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
