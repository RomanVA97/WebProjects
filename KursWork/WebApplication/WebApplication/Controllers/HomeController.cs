using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;



namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            IList<string> roles = new List<string>();
            int c = roles.Count();
            
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
            {
                roles = userManager.GetRoles(user.Id);
                if(roles.Count == 1)
                {
                    return RedirectPermanent("~/User/Index");
                }
                if(roles.IndexOf("admin") != -1)
                {
                    return RedirectPermanent("~/Admin/Index");
                }
                if (roles.IndexOf("doctor") != -1)
                {
                    return RedirectPermanent("~/Doctor/Index");
                }
                
            }
            return View();
        }

        
        public ActionResult Create()
        {
            MedicalContext obj = new MedicalContext();

            

            //Добавляем игрока в таблицу
            HealthOrganization user = new HealthOrganization();
            user.Id = 0;
            user.NameOfHealthOrganization = "1123";
            user.Address = "asd";
            obj.HO.Add(user);
            obj.SaveChanges();
            obj.Dispose();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
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