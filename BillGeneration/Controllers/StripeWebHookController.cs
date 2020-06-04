using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillGeneration.Controllers
{
    public class StripeWebHookController : Controller
    {
        // GET: StripeWebHook
        public ActionResult Index()
        {
            return View();
        }
    }
}