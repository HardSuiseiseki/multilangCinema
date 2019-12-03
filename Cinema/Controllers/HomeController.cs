using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Cinema.Agent;
using Cinema.Models;
using Cinema.Reports;

namespace Cinema.Controllers
{
    //MVC
    //Model - Data
    //View - View
    //Controller - View + Data
    public class HomeController : Controller
    {
        private IMapper Mapper { get; set; }
        public HomeController(IMapper mapper)
        {
            Mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var cacheWarmingUpAgent = new CacheWarmingUpAgent();
            cacheWarmingUpAgent.Run();

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


    }
}