using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SampleWebApi.BusinessLayer.MapperProfiles;
using SampleWebApi.BusinessLayer.Services;
using SampleWebApi.DataAccessLayer;
using SampleWebApi.Settings;

namespace SampleWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SqlConnection");
            //var setting1 = Configuration.GetValue<string>("ApplicationOptions:Setting1");
            //var setting2 = Configuration.GetValue<int>("ApplicationOptions:Setting2");

            var section = Configuration.GetSection(nameof(ApplicationOptions));
            var settings = section.Get<ApplicationOptions>();
            services.Configure<ApplicationOptions>(section);

            services.AddControllers(); //.AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleWebApi", Version = "v1" });
            });

            //services.AddSingleton<IPeopleService, PeopleService>();
            services.AddScoped<IPeopleService, PeopleService>();

            //if (Environment.IsDevelopment())
            //{
            //    services.AddScoped<IPeopleService, StubPeopleService>();
            //}
            //else
            //{
            //    services.AddScoped<IPeopleService, PeopleService>();
            //}

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddAutoMapper(typeof(PersonMapperProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SampleWebApi v1"));

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
