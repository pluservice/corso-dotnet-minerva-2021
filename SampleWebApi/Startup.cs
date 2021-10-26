using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using SampleWebApi.Authentication;
using SampleWebApi.BusinessLayer.MapperProfiles;
using SampleWebApi.BusinessLayer.Services;
using SampleWebApi.BusinessLayer.Settings;
using SampleWebApi.BusinessLayer.Validations;
using SampleWebApi.DataAccessLayer;
using System;
using System.Security.Claims;
using System.Text;

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

            Configure<ApplicationOptions>(nameof(ApplicationOptions));
            var jwtSettings = Configure<JwtSettings>(nameof(JwtSettings));

            services.AddControllers()
                .AddFluentValidation(options
                => options.RegisterValidatorsFromAssemblyContaining<SavePersonRequestValidator>())
                ; //.AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleWebApi", Version = "v1" });

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insert JWT token with the \"Bearer \" prefix",
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            //services.AddSingleton<IPeopleService, PeopleService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMinutes(2),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
                };
            });

            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddScoped<IAuthorizationHandler, UserActiveHandler>();

            services.AddAuthorization(options =>
            {
                var policyBuilder = new AuthorizationPolicyBuilder();
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.Requirements.Add(new UserActiveRequirement());

                //.RequireClaim(ClaimTypes.Country, "US");
                //.RequireClaim("ip_address", "192.168.1.149");

                options.FallbackPolicy = options.DefaultPolicy = policyBuilder.Build();

                //options.AddPolicy("OnlyUSContry", policyBuilder.Build());
                options.AddPolicy("OnlyUSContry", policy =>
                {
                    policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Country, "US");
                });

                options.AddPolicy("AtLeast18", policy =>
                {
                    policy.Requirements.Add(new MinimumAgeRequirement(18));
                });

                options.AddPolicy("AtLeast21", policy =>
                {
                    policy.Requirements.Add(new MinimumAgeRequirement(21));
                });

                options.AddPolicy("RequireActiveUser", policy =>
                {
                    policy.Requirements.Add(new UserActiveRequirement());
                });
            });

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

            T Configure<T>(string sectionName) where T : class
            {
                var section = Configuration.GetSection(sectionName);
                var settings = section.Get<T>();
                services.Configure<T>(section);

                return settings;
            }
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
