﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AMZ_Coupon
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
