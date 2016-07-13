using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TodoList.EF.Contract.DTO;
using TodoList.EF.Contract.DTO.Todo;
using TodoList.EF.Contract.Query.Todo;
using TodoList.EF.Handler;
using TodoList.EF.Handler.Todo;
using TodoList.EF.Repositories;
using Newtonsoft.Json;

namespace TodoList.EF.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddCors();

            services.AddSingleton<TodoRepository>();

            services.AddTransient<IQueryHandler<GetTodosQuery, TodoListDTO>, GetTodosQueryHandler>();
            services.AddTransient<IQueryHandler<GetTodoByIdQuery, SingleTodoDTO>, GetTodoByIdQueryHandler>();
            services.AddTransient<IQueryHandler<UpdateTodoQuery, SuccessDTO>, UpdateTodoQueryHandler>();
            services.AddTransient<IQueryHandler<CreateTodoQuery, AddDTO>, CreateTodoQueryHandler>();
            services.AddTransient<IQueryHandler<DeleteTodoQuery, SuccessDTO>, DeleteTodoQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => 
                builder.WithOrigins("http://localhost:50347").AllowAnyHeader()
            );

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
