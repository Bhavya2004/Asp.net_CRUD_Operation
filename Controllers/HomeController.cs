using CRUD_with_sql.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CRUD_with_sql.Controllers
{
	public class HomeController : Controller
	{
        private readonly _21010101010Context context;
        private readonly IConfiguration _configuration;

        public HomeController(_21010101010Context context, IConfiguration configuration)
        {
            this.context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
		{
            var model = new Count(_configuration);
            ViewBag.EntityCounts = model.GetEntityCounts();
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

		[HttpPost]
        public IActionResult Login(UserTable user)
        {
            var myUser = context.UserTables.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession",myUser.Email);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Login Failed..!";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}