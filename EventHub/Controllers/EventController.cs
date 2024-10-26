using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers;

public class EventController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}