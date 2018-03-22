using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Parte02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parte02
{
    public class SesionTerminadaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            if (context.Session == null || String.IsNullOrWhiteSpace(context.Session.GetString("Usuario")))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                            { "Controller", "Home" },
                            { "Action", "Login" }
                });
            }
            else
            {
                Usuario en = JsonConvert.DeserializeObject<Usuario>(context.Session.GetString("Usuario"));
                Controller controller = filterContext.Controller as Controller;
                controller.ViewData["rol"] = en.Rol;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
