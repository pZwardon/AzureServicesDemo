﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AzureServicesDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Scaling()
        {
            return View();
        }

        public ActionResult AwesomeNewFeature()
        {
            return View();
        }
    }
}