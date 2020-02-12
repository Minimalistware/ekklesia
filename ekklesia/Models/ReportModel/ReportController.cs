using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Models.ReportModel
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}