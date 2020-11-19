using Microsoft.AspNetCore.Http;
using SeminarManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.MVC.Misc
{
    public class CustomAuthorizationMiddleware
    {
        private RequestDelegate next;
        private IPersonRepository repository;

        public CustomAuthorizationMiddleware(RequestDelegate next, IPersonRepository repository)
        {
            this.next = next;
            this.repository = repository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Authentication"))
            {
                if (context.Session.GetInt32("user_id").HasValue)
                {
                    var user_id = context.Session.GetInt32("user_id").Value;
                    var user = repository.ById(user_id);
                    context.Items["user"] = user;
                } 
                else
                {
                    context.Response.Redirect("/Authentication/Login");
                }
            }

            await next(context);
        }
    }
}
