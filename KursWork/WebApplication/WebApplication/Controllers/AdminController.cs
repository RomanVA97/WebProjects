using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddRole()
        {
            
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> AddRole(string email, string role)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(email);
            await userManager.AddToRoleAsync(user.Id, role);
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult RemoveRole()
        {
            
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> RemoveRole(string email, string role)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(email);
            await userManager.RemoveFromRoleAsync(user.Id, role);
            return View();
        }
    }
}