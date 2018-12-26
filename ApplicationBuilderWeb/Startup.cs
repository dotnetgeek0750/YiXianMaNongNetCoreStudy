using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApplicationBuilderWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
            //Run(app);
            //Use(app);
            //Map(app);
            //MapWhen(app);
            UseWhen(app);
        }



        //在管道的尾端增加一个Middleware；它是执行的最后一个Middleware。
        //即它执行完就不再执行下一个Middleware了
        public void Run(IApplicationBuilder app)
        {
            //第一个Run会执行
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            //第二个Run不会执行
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        public void Use(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 1");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }


        public void UseWhen(IApplicationBuilder app)
        {
            //主干Middleware，执行
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 1");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });
            //next Invoke后会执行此Middle
            app.UseWhen(context => context.Request.Path.Value.StartsWith("/Text"), (builder) =>
            {
                builder.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync(" usewhen!");
                    await next.Invoke();//继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
                });
            });
            // //next Invoke后会执行此Middle
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }


        public void Map(IApplicationBuilder app)
        {
            //主干上的Middleware，会执行
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 1");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });

            //Map，新劈开的Middleware，如果匹配到URL含有Text，则会执行这个分支
            app.Map("/Text", (builder) =>
            {
                builder.Run(async (context) =>
               {
                   await context.Response.WriteAsync("hello Map");
               });
            });

            //执行完Map之后，程序会走了Map的那个分支，不会执行此Middleware
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 2");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });
        }


        public void MapWhen(IApplicationBuilder app)
        {
            //主干上的Middleware，可以执行
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 1");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });
            app.MapWhen(context => context.Request.Path.Value.StartsWith("/Text"), (builder) =>
            {
                builder.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync(" MapWhen!! ");
                });
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("hello world!,Use 2");
                await next.Invoke(); //继续执行下一个Middleware，如果不Invoke，则会中断，下面的app.Run则不会执行
            });
        }
    }
}
