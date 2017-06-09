using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Img = Resume.GetItemsForIndexPage();
            ResumeContext RC = new ResumeContext();
            ViewBag.ThePost = new SelectList(RC.ThePost, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Filter(int? id)
        {
            List<ResumeItem> iip = Resume.GetItemsForIndexPage();
            ResumeContext RC = new ResumeContext();
            ViewBag.ThePost = new SelectList(RC.ThePost, "Id", "Name");
            List<ResumeItem> list = new List<ResumeItem>();
            
            ThePost tp = RC.ThePost.Find(id);
            foreach(ResumeItem item in iip)
            {
                foreach(string str in item.DesiredPosition)
                {
                    if (str == tp.Name) list.Add(item);
                }
            }
            ViewBag.Img = list;
            return PartialView();
        }


        [HttpPost]
        public ActionResult MoreInformation(int? id)
        {
            ResumeContext RC = new ResumeContext();
            
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            Resume resume = RC.Resume.Find(id); ;
            
            ViewBag.LastName = resume.Surname;
            ViewBag.Name = resume.Name;
            ViewBag.MiddleName = resume.MiddleName;
            ViewBag.DateOfBirth = resume.DateOfBirth;
            ViewBag.Adress = resume.Adress;

            ViewBag.MobileNumber = resume.WorkNumber;
            ViewBag.HomeNumber = resume.HomeNumber;
            //gender
            ViewBag.Citizenship = resume.Citizenship;
            //SP

            ViewBag.TheCompositionOfTheFamily = resume.TheCompositionOfTheFamily;
            ViewBag.DriversLicense = resume.DriversLicense;
            ViewBag.CarBrand = resume.CarBrand;
            //fotka
            //1_block



            ViewBag.CareerHistory = CareerHistory.GetItems(resume.Id);
            ViewBag.Education = Education.GetItems(resume.Id);
            ViewBag.AdditionalEducation = AdditionalEducation.GetItems(resume.Id);
            ViewBag.Lang = KnowledgeOfForeignLanguages.GetItems(resume.Id);
            ViewBag.Ability = Ability.GetItems(resume.Id);

            ViewBag.TheBusinessAndPsychologicalQualities = resume.TheBusinessAndPsychologicalQualities;
            ViewBag.ProfessionalSkills = resume.ProfessionalSkills;
            ViewBag.Hobbies = resume.Hobbies;
            //

            ViewBag.DesiredPosition = DesiredPosition.GetItems(resume.Id);

            ViewBag.WorkingConditions = resume.WorkingConditions;
            ViewBag.ProfessionalTasks = resume.ProfessionalTasks;
            ViewBag.ForMoreInformation = resume.ForMoreInformation;

            ViewBag.Salary = resume.Salary;
            ViewBag.MinSalary = resume.MinSalary;
            ViewBag.NormSalary = resume.NormSalary;
            ViewBag.SalaryChek = resume.SalaryChek;
            ViewBag.ThePercentage = resume.ThePercentage;
            ViewBag.SalaryBonus = resume.SalaryBonus;
            ViewBag.SalaryPercentage = resume.SalaryPercentage;
            ViewBag.Comment = resume.Comment;
            //

            ViewBag.Income = resume.Income;
            ViewBag.TheProspectOfJobGrowth = resume.TheProspectOfJobGrowth;
            ViewBag.ToGetTheNecessaryExperience = resume.ToGetTheNecessaryExperience;
            ViewBag.ToImproveTheProfessionalLevel = resume.ToImproveTheProfessionalLevel;
            ViewBag.ToDemonstrateTheirAbilities = resume.ToDemonstrateTheirAbilities;
            ViewBag.AHighLevelOfAutonomy = resume.AHighLevelOfAutonomy;
            ViewBag.TheStabilityOfTheCompany = resume.TheStabilityOfTheCompany;
            ViewBag.ActivitiesOfTheCompany = resume.ActivitiesOfTheCompany;
            ViewBag.WorkingConditionsInTheWorkplace = resume.WorkingConditionsInTheWorkplace;
            ViewBag.RelationsWithTheLeadership = resume.RelationsWithTheLeadership;
            ViewBag.SomethingElse = resume.SomethingElse;


            return PartialView();
        }

    }
}