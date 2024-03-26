using Microsoft.AspNetCore.Mvc;

namespace WebbApp.Controllers;

public class CoursesController : Controller
{
	[Route("/Courses")]
	public IActionResult Index()
	{
		return View();
	}
}
