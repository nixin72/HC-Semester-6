using System.Web.Mvc;
using CSAdminMVC.App_Code;

namespace CSAdminMVC.Controllers
{
	[CSAdminAuthorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}