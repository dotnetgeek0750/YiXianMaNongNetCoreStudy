using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationPipeline
{
    public class MyStaticFileMiddleware
    {
        //声明下一个中间件
        private readonly RequestDelegate _next;

        public MyStaticFileMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// Invoke 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            //在执行下一个中间件之前
            //可以实现一些自定义逻辑
            //比如判断URL含有jpg或者png，则返回一张指定图片
            var path = context.Request.Path.Value;
            if (path.Contains("jpg") || path.Contains("png"))
            {
                return context.Response.SendFileAsync("1.png");
            }
            //执行下一个中间件并返回
            var task = this._next(context);
            return task;
        }
    }

}
