using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            ViewBag.Message = "";
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if(user.UserId == null)
            {
                ViewBag.Message = "У вас не заполнены данные профиля, пожалуйста заполните их! Для этого перейдите в редактирование учётных данных";
            }
            return View();
        }

        //редактирование данных о пользователе

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult TheEditCredentials()
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalContext MC = new MedicalContext();
            ViewBag.Country = MC.Country.ToList();
            ViewBag.Region = MC.Region.ToList();
            if (user.UserId == null)
            {
                User userProfile = new User();
                userProfile.SurName = "";
                userProfile.Name = "";
                userProfile.MiddleName = "";
                userProfile.NumberAndSeriesOfPassport = "";
                userProfile.DateOfBirth = "";
                userProfile.Address = "";
                userProfile.TheContactPhoneNumber = "";
                userProfile.SocialStatus = "";
                userProfile.PlaceOfWork = "";
                ViewBag.User = userProfile;
            }
            else
            {

                User userProfile = MC.U.Find(user.UserId);
                ViewBag.User = userProfile;
            }
            int selectedIndex = 1;
            SelectList states = new SelectList(MC.Country, "Id", "Name");
            ViewBag.States = states;
            SelectList regions = new SelectList(MC.Region.Where(c => c.CountryId == selectedIndex), "Id", "Name");
            ViewBag.Regions = regions;
            SelectList cities = new SelectList(MC.City.Where(c => c.RegionId == selectedIndex), "Id", "Name");
            ViewBag.Cities = cities;
            //MC.Dispose();
            return View();
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult TheEditCredentials(string SurName, string Name, string MiddleName, string NumberAndSeriesOfPassport, string DateOfBirth, 
            string Gender, string Address, string TheContactPhoneNumber, string SocialStatus, string PlaceOfWork, int City)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalContext MC = new MedicalContext();
            if (user.UserId == null)
            {
                User userProfile = new User();
                userProfile.SurName = SurName;
                userProfile.Name = Name;
                userProfile.MiddleName = MiddleName;
                userProfile.NumberAndSeriesOfPassport = NumberAndSeriesOfPassport;
                userProfile.DateOfBirth = DateOfBirth;
                userProfile.Gender = Gender;
                userProfile.CityId = City;
                userProfile.Address = Address;
                userProfile.TheContactPhoneNumber = TheContactPhoneNumber;
                userProfile.SocialStatus = SocialStatus;
                userProfile.PlaceOfWork = PlaceOfWork;
                MC.U.Add(userProfile);
                MC.Entry(userProfile).State = EntityState.Added;
                MC.SaveChanges();
                /*userProfile = MC.U.Find(userProfile.SurName, userProfile.Name, userProfile.MiddleName,
                userProfile.DateOfBirth, userProfile.Gender, userProfile.Address,
                userProfile.TheContactPhoneNumber, userProfile.SocialStatus, userProfile.PlaceOfWork);*/
                user.UserId = userProfile.Id;
                userManager.Update(user);
                userManager.UpdateAsync(user);
            }
            else
            {
                
                User userProfile = MC.U.Find(user.UserId);
                userProfile.SurName = SurName;
                userProfile.Name = Name;
                userProfile.MiddleName = MiddleName;
                userProfile.NumberAndSeriesOfPassport = NumberAndSeriesOfPassport;
                userProfile.DateOfBirth = DateOfBirth;
                userProfile.Gender = Gender;
                userProfile.CityId = City;
                userProfile.Address = Address;
                userProfile.TheContactPhoneNumber = TheContactPhoneNumber;
                userProfile.SocialStatus = SocialStatus;
                userProfile.PlaceOfWork = PlaceOfWork;
                MC.Entry(userProfile).State = EntityState.Modified;
                MC.SaveChanges();
            }
            MC.Dispose();
            return View("Index");
        }


        [Authorize(Roles ="user")]
        [HttpGet]
        public ActionResult GetItemsRegions(int? id)
        {
            MedicalContext MC = new MedicalContext();
            SelectList regions = new SelectList(MC.Region.Where(c => c.CountryId == id), "Id", "Name");
            ViewBag.Regions = regions;
            return PartialView();
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult GetItemsCities(int? id)
        {
            MedicalContext MC = new MedicalContext();
            SelectList cities = new SelectList(MC.City.Where(c => c.RegionId == id), "Id", "Name");
            ViewBag.Cities = cities;
            return PartialView();
        }


        //запись на приём к врачу
        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult ToMakeAnAppointmentToSeeTheDoctor()
        {
            MedicalContext MC = new MedicalContext();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            int? id;
            if (user.UserId != null)
            {
                id = MC.U.Find(user.UserId).CityId;
            }
            else
            {
                return HttpNotFound();
            }
            SelectList g = new SelectList(MC.SD.Where(c => c.HealthOrganization.CityId == id), "Id", "NameOfStructuralDivision");
            int? idSD = Convert.ToInt32(g.ElementAt(0).Value);
            var p = MC.MDoc.Where(c => c.StructuralDivisionId == idSD).ToList();
            //var p = MC.MDoc.ToList();
            ICollection<SelectItemMD> list = new List<SelectItemMD>();

            foreach(MedicalDoctor item in p)
            {
                SelectItemMD item2 = new SelectItemMD();
                User us = MC.U.Find(item.UserId);
                item2.Id = item.Id;
                item2.FIO = us.SurName + " " + us.Name + " " + us.MiddleName;
                list.Add(item2);
            }
            ViewBag.Doctors = list;
            ViewBag.SD = g;



            return View();
        }
        //ne rab kusok


        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult GetItemsRoomPass(DateTime date, int? id)
        {
            const int theNumberOfDailyReceptions = 12;
            MedicalContext MC = new MedicalContext();
            List<RecordOnReceptionToTheDoctor> list = MC.RORTTD.Where(c => c.DateOfAdmission == date).ToList();
            MedicalDoctor MD = MC.MDoc.Find(id);
            float a = date.Day;
            DateTime DT; 
            if (a%2 == 0)
            {
                DT = MD.HoursOnEvenNumbers;
            }else
            {
                DT = MD.HoursOnOddNumbers;
            }
            List<SelectItemTimeRORTTD> listItems = new List<SelectItemTimeRORTTD>();
            int hour = DT.Hour, minute = DT.Minute;
            for (int i=0;i<theNumberOfDailyReceptions;i++)
            {
                bool chek = true;
                foreach(RecordOnReceptionToTheDoctor item in list)
                {
                    if (item.RoomPass == i) chek = false;
                }
                
                if(chek)
                {
                    SelectItemTimeRORTTD obj = new SelectItemTimeRORTTD();
                    obj.Id = i;
                    obj.Time = hour + " : ";
                    if (minute < 10) obj.Time += "0";
                    obj.Time += minute;

                    listItems.Add(obj);
                }
                minute += 30;
                if (minute > 59)
                {
                    minute -= 60;
                    hour += 1;
                }
            }
            
            ViewBag.Items = listItems;
            return PartialView();
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult GetItemsMDSelect(int id)
        {
            MedicalContext MC = new MedicalContext();
            var p = MC.MDoc.Where(c => c.StructuralDivisionId == id).ToList();
            
            ICollection<SelectItemMD> list = new List<SelectItemMD>();

            foreach (MedicalDoctor item in p)
            {
                SelectItemMD item2 = new SelectItemMD();
                User us = MC.U.Find(item.UserId);
                item2.Id = item.UserId;
                item2.FIO = us.SurName + " " + us.Name + " " + us.MiddleName;
                list.Add(item2);
            }
            ViewBag.Doctors = list;
            return PartialView();
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult ToMakeAnAppointmentToSeeTheDoctor(DateTime Date, int? Doctor, int TimeId)
        {
            MedicalContext MC = new MedicalContext();
            RecordOnReceptionToTheDoctor obj = new RecordOnReceptionToTheDoctor();
            obj.DateOfAdmission = Date;
            obj.MedicalDoctorId = Doctor;
            obj.RoomPass = TimeId;
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            obj.UserId = user.UserId;
            MC.RORTTD.Add(obj);
            MC.SaveChangesAsync();

            return View("Index");
        }

        
        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult MyNotesToTheDoctors()
        {
            MedicalContext MC = new MedicalContext();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);


            List<MyNotesToTheDoctorsItem> list = new List<MyNotesToTheDoctorsItem>();
            foreach (RecordOnReceptionToTheDoctor item in MC.RORTTD.Where(c => c.UserId == user.UserId).ToList())
            {
                MyNotesToTheDoctorsItem item2 = new MyNotesToTheDoctorsItem();

                MedicalDoctor MD = MC.MDoc.Where(c => c.Id == item.MedicalDoctorId).FirstOrDefault();
                DateTime DT;
                if (item.DateOfAdmission.Day % 2 == 0)
                {
                    DT = MD.HoursOnEvenNumbers;
                }
                else
                {
                    DT = MD.HoursOnOddNumbers;
                }
                User us = MC.U.Find(MD.UserId);
                item2.Id = item.Id;
                item2.FIO = us.SurName + " " + us.Name + " " + us.MiddleName;
                item2.Office = MD.Office.ToString();
                int roomPass = (int)item.RoomPass;
                int a = (DT.Minute + (roomPass * 60)) / 60;
                int b = (DT.Minute + (roomPass * 60)) % 60;
                item2.Date = new DateTime(item.DateOfAdmission.Year, item.DateOfAdmission.Month, item.DateOfAdmission.Day, a+DT.Hour, b, 0);
                list.Add(item2);
            }
            list.Reverse();
            ViewBag.Items = list;
            ViewBag.Title = "Мои записи к врачам";
            return View();
        }

        
        [Authorize(Roles = "user")]
        [HttpGet]
        public void DeletingRecordsToTheDoctor(int? id)
        {
            MedicalContext MC = new MedicalContext();
            MC.RORTTD.Remove(MC.RORTTD.Find(id));
            MC.SaveChanges();
        }


        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult DoctorsPrescription()
        {
            MedicalContext MC = new MedicalContext();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);


            List<DoctorsPrescriptionItem> list = new List<DoctorsPrescriptionItem>();
            foreach (AListOfDoctorsAppointment item in MC.ALODA.Where(c => c.UserId == user.UserId).ToList())
            {
                DoctorsPrescriptionItem item2 = new DoctorsPrescriptionItem();
                MedicalDoctor MD = MC.MDoc.Where(c => c.Id == item.MedicalDoctorId).FirstOrDefault();
                User us = MC.U.Find(MD.UserId);
                item2.Id = item.Id;
                item2.FIO = us.SurName + " " + us.Name + " " + us.MiddleName;
                item2.Appointments = item.Appointments;
                list.Add(item2);
            }
            list.Reverse();
            ViewBag.Items = list;
            ViewBag.Title = "Назначения врачей";


            return View();
        }

    }
}
