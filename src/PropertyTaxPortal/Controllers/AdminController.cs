using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PropertyTaxPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            int i = 0;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}