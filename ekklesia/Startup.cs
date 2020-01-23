﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekklesia.Models.EventModel;
using ekklesia.Models.MemberModel;
using ekklesia.Models.TransactionModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ekklesia
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
            services.AddDbContextPool<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EkklesiaDBConnection"));
            });
            services.AddMvc();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default","{controller=member}/{action=list}/{id?}");
            });
        }
    }
}
