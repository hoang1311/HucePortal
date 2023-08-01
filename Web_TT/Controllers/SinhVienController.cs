using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_TT.Models;
using Web_TT.Common;
using Web_TT.App_Start;

namespace Web_TT.Controllers
{
    public class SinhVienController : Controller
    {     
        [AdminAuthorize(ID ="4")]
        public ActionResult Index()
        {          
            return View();
        }
        [AdminAuthorize(ID ="1")]
        public ActionResult Create()
        {          
            return View();
        }
        [AdminAuthorize(ID ="2")]
        public ActionResult Edit()
        {          
            return View();
        }

    }
}