using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartsTest.Controllers
{
    public class ComponentController : Controller
    {
      
            public ActionResult Index()
            {
                 return View();
            }

            public ActionResult AddComponent()
            {
                return PartialView("AddComponent");
            }
            public ActionResult ShowComponents()
            {
                return PartialView("ShowComponents");
            }

            public ActionResult EditComponent()
            {
                return PartialView("EditComponent");
            }

            public ActionResult DeleteComponent()
            {
                return PartialView("DeleteComponent");
            }
        }
    
}