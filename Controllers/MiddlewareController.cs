using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAproject.Controllers
{
    public class MiddlewareController : Controller
    {
        // GET: Middleware
        public ActionResult Index()
        {
            return RedirectToAction("LogIn", "Auther", new { area = "" });
        }
    }
}