﻿using System.Web.Mvc;

namespace ExpensesTracker.Web.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}