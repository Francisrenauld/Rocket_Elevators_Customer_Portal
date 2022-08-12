using Microsoft.AspNetCore.Mvc;


namespace Rocket_Elevator_Customer_Portal.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
