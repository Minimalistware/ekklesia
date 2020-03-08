using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ekklesia;
using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.ReportModel;
using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ekklesiapi
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //DB
            services.AddDbContextPool<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EkklesiaDBConnection"));
            });

            //Injection
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            //Mvc routing
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            //Auth
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "^([A-Za-z\u00C0-\u00D6\u00D8-\u00f6\u00f8-\u00ff]*)$ ";
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationContext>();

            
            //JWT
            var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromHours(1),
                    ValidIssuer = configuration["Issuer"],
                    ValidAudience = configuration["ValidAt"]
                };
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
