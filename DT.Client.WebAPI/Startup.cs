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
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DT.Client.WebAPI
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
            services.AddScoped<IDataService, BaseDataService>();
            services.AddScoped<IEntityRepository, BaseRepository>(service => new BaseRepository(service.GetRequiredService<WorkoutContext>));
            services.AddSingleton<IMapper>(service => new Mapper(new AutoMapperConfiguration().SetUpAutoMapper()));
            services.AddScoped<IWorkoutItemDataService, WorkoutItemDataService>();
            services.AddScoped<IWorkoutItemRepository, WorkoutItemRepository>(service => new WorkoutItemRepository(service.GetRequiredService<WorkoutContext>));
            services.AddDbContext<WorkoutContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Workout;Integrated Security=True;"));

            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new QueryStringApiVersionReader("v");
            });
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

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorkoutContext>
    {
        public WorkoutContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WorkoutContext>();
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Workout;Integrated Security=True;");
            return new WorkoutContext(builder.Options);
        }
    }
}
