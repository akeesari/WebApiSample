﻿using System.Web;
using System.Web.Mvc;

namespace AK.Net.Todo.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}