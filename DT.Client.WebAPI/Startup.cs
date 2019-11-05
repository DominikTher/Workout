using AutoMapper;
using DT.Business.Configuration;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using DT.Business.Services;
using DT.DataRepository;
using DT.DataRepository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DT.Client.WebAPI
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
            Action<DbContextOptionsBuilder> sqlProvider = (options) => options.UseSqlServer(Configuration.GetConnectionString("SQL"));
            Action<DbContextOptionsBuilder> sqliteProvider = (options) => options.UseSqlite(Configuration.GetConnectionString("SQLite"));

            services.AddScoped<IDataService, BaseDataService>();
            services.AddScoped<IEntityRepository, BaseRepository>(service => new BaseRepository(service.GetRequiredService<WorkoutContext>));
            services.AddSingleton<IMapper>(service => new Mapper(new AutoMapperConfiguration().SetUpAutoMapper()));
            services.AddScoped<IWorkoutItemDataService, WorkoutItemDataService>();
            services.AddScoped<IWorkoutItemRepository, WorkoutItemRepository>(service => new WorkoutItemRepository(service.GetRequiredService<WorkoutContext>));
            services.AddScoped<ISeriesDataService, SeriesDataService>();
            services.AddScoped<ISeriesRepository, SeriesRepository>(service => new SeriesRepository(service.GetRequiredService<WorkoutContext>));
            services.AddDbContext<WorkoutContext>(sqliteProvider);

            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new QueryStringApiVersionReader("v");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Workout API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout API V1");
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
