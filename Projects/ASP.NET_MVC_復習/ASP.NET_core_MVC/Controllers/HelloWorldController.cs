using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_core_MVC.Controllers
{
    public class HelloWorldController : Controller
    {
        // /HelloWorld にアクセスした時の処理
        public IActionResult Index()
        {
            return View();
        }

        // /HelloWorld/Welcome にアクセスした時の処理
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["message"] = $"こんにちは！{name}さん";
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}　　　


