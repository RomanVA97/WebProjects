using System;
using TemplateEngine.Docx;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication1.Models;
using System.IO;


namespace WebApplication1.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль задан."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Создание и отправка маркера
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Ваш код безопасности: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Отправка SMS через поставщик SMS для проверки номера телефона
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Это сообщение означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Не удалось проверить телефон");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult ChangeResume()
        {
            ResumeContext RC = new ResumeContext();
            ViewBag.Industry = new SelectList(RC.Industry, "Id", "Name");

            ViewBag.Level = new SelectList(RC.Level, "Id", "Name");

            ViewBag.Profile = new SelectList(RC.Profile, "Id", "Name");

            ViewBag.FormOfTraining = new SelectList(RC.FormOfTraining, "Id", "Name");

            ViewBag.TheTypeOfTraining = new SelectList(RC.TheTypeOfTraining, "Id", "Name");

            ViewBag.SourceOfInformation = new SelectList(RC.SourceOfInformation, "Id", "Name");

            ViewBag.Language = new SelectList(RC.Language, "Id", "Name");
            // = new SelectList(RC.Language, "Id", "Name");

            ViewBag.TheLevelOfLanguageLearning = new SelectList(RC.TheLevelOfLanguageLearning, "Id", "Name");
            //
            ViewBag.Skill = new SelectList(RC.Skill, "Id", "Name");

            ViewBag.SkillLevel = new SelectList(RC.SkillLevel, "Id", "Name");
            //
            //
            ViewBag.ThePost = new SelectList(RC.ThePost, "Id", "Name");
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            Resume resume;
            if (user.ResumeID != null)
            {
                int? id = user.ResumeID;
                resume = RC.Resume.Find(id);
            }
            else
            {
                resume = new Resume();
                resume.Surname = "";
                resume.Name = "";
                resume.MiddleName = "";
                resume.DateOfBirth = DateTime.Now;
                resume.Adress = "";
                resume.HomeNumber = "";
                resume.WorkNumber = "";
                resume.Gender = "";
                resume.Citizenship = "";
                resume.MaritalStatus = "";
                resume.TheCompositionOfTheFamily = "";
                resume.DriversLicense = "";
                resume.CarBrand = "";
                resume.TheBusinessAndPsychologicalQualities = "";
                resume.ProfessionalSkills = "";
                resume.Hobbies = "";
                resume.WorkingConditions = "";
                resume.ProfessionalTasks = "";
                resume.ForMoreInformation = "";
                resume.Salary = "";
                resume.MinSalary = "";
                resume.NormSalary = "";
                resume.Comment = "";
                resume.Income = "";
                resume.TheProspectOfJobGrowth = "";
                resume.ToGetTheNecessaryExperience = "";
                resume.ToImproveTheProfessionalLevel = "";
                resume.ToDemonstrateTheirAbilities = "";
                resume.AHighLevelOfAutonomy = "";
                resume.TheStabilityOfTheCompany = "";
                resume.ActivitiesOfTheCompany = "";
                resume.WorkingConditionsInTheWorkplace = "";
                resume.RelationsWithTheLeadership = "";
                resume.SomethingElse = "";



                RC.Resume.Add(resume);
                RC.SaveChanges();
                user.ResumeID = resume.Id;
                userManager.Update(user);
            }
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

            

            ViewBag.ID = resume.Id;
            //RC.Dispose();
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            foreach (string file in Request.Files)
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

                ResumeContext RC = new ResumeContext();
                Resume resume = RC.Resume.Find(user.ResumeID);
                
                var upload = Request.Files[file];
                if (upload != null)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(upload.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(upload.ContentLength);
                    }
                    resume.ImageName = fileName;
                    resume.ImageByte = imageData;
                    
                }
                RC.SaveChanges();
            }

            
            return Json("файл загружен");
        }

        [HttpPost]
        public void CR1(int? id, string Surname, string Name, string MiddleName, DateTime DateOfBirth, string Adress,
            string MobileNumber, string HomeNumber, string Gender, string Citizenship, string MaritalStatus, string TheCompositionOfTheFamily,
            string DriversLicense, string CarBrand)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            ResumeContext RC = new ResumeContext();
            Resume resume = RC.Resume.Find(user.ResumeID);

            resume.Surname = Surname;
            resume.Name = Name;
            resume.MiddleName = MiddleName;
            resume.DateOfBirth = DateOfBirth;
            resume.Adress = Adress;
            resume.WorkNumber = MobileNumber;
            resume.HomeNumber = HomeNumber;
            resume.Gender = Gender;
            resume.Citizenship = Citizenship;
            resume.MaritalStatus = MaritalStatus;
            resume.TheCompositionOfTheFamily = TheCompositionOfTheFamily;
            resume.DriversLicense = DriversLicense;
            resume.CarBrand = CarBrand;
            RC.SaveChanges();
            //RC.Dispose();
        }

        [HttpPost]
        public void CR2(int? id, string TheBusinessAndPsychologicalQualities, string ProfessionalSkills, string Hobbies)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            ResumeContext RC = new ResumeContext();
            Resume resume = RC.Resume.Find(user.ResumeID);

            resume.TheBusinessAndPsychologicalQualities = TheBusinessAndPsychologicalQualities;
            resume.ProfessionalSkills = ProfessionalSkills;
            resume.Hobbies = Hobbies;
            RC.SaveChanges();
            //RC.Dispose();
        }

        [HttpPost]
        public void CR3(int? id, string WorkingConditions, string ProfessionalTasks, string ForMoreInformation, string Salary,
            string MinSalary, string NormSalary, bool SalaryChek, bool ThePercentage, bool SalaryBonus, bool SalaryPercentage,
            string Comment)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            ResumeContext RC = new ResumeContext();
            Resume resume = RC.Resume.Find(user.ResumeID);
            resume.WorkingConditions = WorkingConditions;
            resume.ProfessionalTasks = ProfessionalTasks;
            resume.ForMoreInformation = ForMoreInformation;
            resume.Salary = Salary;
            resume.MinSalary = MinSalary;
            resume.NormSalary = NormSalary;
            resume.SalaryChek = SalaryChek;
            resume.ThePercentage = ThePercentage;
            resume.SalaryBonus = SalaryBonus;
            resume.SalaryPercentage = SalaryPercentage;
            resume.Comment = Comment;


            RC.SaveChanges();
            //RC.Dispose();
        }



        [HttpPost]
        public void ChangeResume(int? id, string Income, string TheProspectOfJobGrowth, string ToGetTheNecessaryExperience, string ToImproveTheProfessionalLevel,
            string ToDemonstrateTheirAbilities, string AHighLevelOfAutonomy, string TheStabilityOfTheCompany, string ActivitiesOfTheCompany,
            string WorkingConditionsInTheWorkplace, string RelationsWithTheLeadership, string SomethingElse, int SourceOfInformation)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            
            ResumeContext RC = new ResumeContext();
            Resume resume = RC.Resume.Find(user.ResumeID);
            
            resume.Income = Income;
            resume.TheProspectOfJobGrowth = TheProspectOfJobGrowth;
            resume.ToGetTheNecessaryExperience = ToGetTheNecessaryExperience;
            resume.ToImproveTheProfessionalLevel = ToImproveTheProfessionalLevel;
            resume.ToDemonstrateTheirAbilities = ToDemonstrateTheirAbilities;
            resume.AHighLevelOfAutonomy = AHighLevelOfAutonomy;
            resume.TheStabilityOfTheCompany = TheStabilityOfTheCompany;
            resume.ActivitiesOfTheCompany = ActivitiesOfTheCompany;
            resume.WorkingConditionsInTheWorkplace = WorkingConditionsInTheWorkplace;
            resume.RelationsWithTheLeadership = RelationsWithTheLeadership;
            resume.SomethingElse = SomethingElse;
            resume.SourceOfInformationId = SourceOfInformation;
            
            RC.SaveChanges();
            //RC.Dispose();
        }


        [HttpPost]
        public ActionResult AddCareerHistory(int? id, string Organization, int? Industry, DateTime Since, DateTime For, string ThePost, 
            string JobResponsibilities, string Achievements, string Wages, string ReasonForLeaving)
        {
            CareerHistory ch = new CareerHistory();
            ch.ResumeId = id;
            ch.Organization = Organization;
            ch.IndustryId = Industry;
            ch.Since = Since;
            ch.For = For;
            ch.ThePost = ThePost;
            ch.JobResponsibilities = JobResponsibilities;
            ch.Achievements = Achievements;
            ch.Wages = Wages;
            ch.ReasonForLeaving = ReasonForLeaving;
            ResumeContext RC = new ResumeContext();
            RC.CareerHistory.Add(ch);
            RC.SaveChanges();

            ViewBag.CareerHistory = CareerHistory.GetItems(id);

            RC.Dispose();
            return PartialView();
        }

        

        [HttpPost]
        public ActionResult DeleteCareerHistory(int? id, int? id2)
        {
            ResumeContext RC = new ResumeContext();
            CareerHistory ch = RC.CareerHistory.Find(id);


            RC.CareerHistory.Remove(ch);
            RC.SaveChanges();

            ViewBag.CareerHistory = CareerHistory.GetItems(id2);
            RC.Dispose();
            return PartialView("AddCareerHistory");
        }

        [HttpGet]
        public ActionResult ChangeCareerHistory(int? id)
        {
            ResumeContext RC = new ResumeContext();
            CareerHistory ch = RC.CareerHistory.Find(id);
            ViewBag.ID = id;
            ViewBag.Organization = ch.Organization;
            ViewBag.Industry = new SelectList(RC.Industry, "Id", "Name");
            ViewBag.Since = ch.Since;
            ViewBag.For = ch.For;
            ViewBag.ThePost = ch.ThePost;
            ViewBag.JobResponsibilities = ch.JobResponsibilities;
            ViewBag.Achievements = ch.Achievements;
            ViewBag.Wages = ch.Wages;
            ViewBag.ReasonForLeaving = ch.ReasonForLeaving;

            //RC.Dispose();
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeCareerHistory(int? id, string Organization, int? Industry, DateTime Since, DateTime For, string ThePost,
            string JobResponsibilities, string Achievements, string Wages, string ReasonForLeaving)
        {
            ResumeContext RC = new ResumeContext();
            CareerHistory ch = RC.CareerHistory.Find(id);
            ch.Organization = Organization;
            ch.IndustryId = Industry;
            ch.Since = Since;
            ch.For = For;
            ch.ThePost = ThePost;
            ch.JobResponsibilities = JobResponsibilities;
            ch.Achievements = Achievements;
            ch.Wages = Wages;
            ch.ReasonForLeaving = ReasonForLeaving;

            RC.Entry(ch).State = System.Data.Entity.EntityState.Modified;
            RC.SaveChanges();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            ViewBag.CareerHistory = CareerHistory.GetItems(user.ResumeID);
            RC.Dispose();
            return PartialView("AddCareerHistory");
        }


        [HttpPost]
        public ActionResult AddEducation(int? id, string EducationName, int? Level, int? Profile, DateTime EducationSince,
            DateTime EducationFor, string EducationFaculty, string EducationSpecialty, string EducationDiplomaQualification,
            int? FormOfTraining)
        {
            ResumeContext RC = new ResumeContext();
            Education education = new Education();
            education.ResumeId = id;
            education.Name = EducationName;
            education.LevelId = Level;
            education.ProfileId = Profile;
            education.Since = EducationSince;
            education.For = EducationFor;
            education.Faculty = EducationFaculty;
            education.Specialty = EducationSpecialty;
            education.DiplomaQualification = EducationDiplomaQualification;
            education.FormOfTrainingId = FormOfTraining;
            RC.Education.Add(education);
            RC.SaveChanges();
            ViewBag.Education = Education.GetItems(id);
            RC.Dispose();
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteEducation(int? id, int? id2)
        {
            ResumeContext RC = new ResumeContext();
            Education e = RC.Education.Find(id);


            RC.Education.Remove(e);
            RC.SaveChanges();

            ViewBag.Education = Education.GetItems(id2);
            RC.Dispose();
            return PartialView("AddEducation");
        }


        [HttpGet]
        public ActionResult ChangeEducation(int? id)
        {
            ResumeContext RC = new ResumeContext();
            Education e = RC.Education.Find(id);
            ViewBag.ID = id;
            ViewBag.EducationName = e.Name;
            ViewBag.Level = new SelectList(RC.Level, "Id", "Name");
            ViewBag.Profile = new SelectList(RC.Profile, "Id", "Name");
            ViewBag.EducationFaculty = e.Faculty;
            ViewBag.EducationSpecialty = e.Specialty;
            ViewBag.EducationDiplomaQualification = e.DiplomaQualification;
            ViewBag.FormOfTraining = new SelectList(RC.FormOfTraining, "Id", "Name");
            
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeEducation(int? id, string EducationName, int? Level, int? Profile, DateTime EducationSince,
            DateTime EducationFor, string EducationFaculty, string EducationSpecialty, string EducationDiplomaQualification,
            int? FormOfTraining)
        {
            ResumeContext RC = new ResumeContext();
            Education education = RC.Education.Find(id);
            education.Name = EducationName;
            education.LevelId = Level;
            education.ProfileId = Profile;
            education.Since = EducationSince;
            education.For = EducationFor;
            education.Faculty = EducationFaculty;
            education.Specialty = EducationSpecialty;
            education.DiplomaQualification = EducationDiplomaQualification;
            education.FormOfTrainingId = FormOfTraining;
            RC.Entry(education).State = System.Data.Entity.EntityState.Modified;
            RC.SaveChanges();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            ViewBag.Education = Education.GetItems(user.ResumeID);
            RC.Dispose();
            return PartialView("AddEducation");
        }





        [HttpPost]
        public ActionResult AddAdditionalEducation(int? id, string AdditionalEducationName, int? TheTypeOfTraining, string AdditionalEducationYearOfCommencementOfStudy,
            string AdditionalEducationTheDurationOfTraining, string AdditionalEducationNameOfSchool, string AdditionalEducationTeacher)
        {
            ResumeContext RC = new ResumeContext();
            AdditionalEducation AE = new AdditionalEducation();
            AE.ResumeId = id;
            AE.Name = AdditionalEducationName;
            AE.TheTypeOfTrainingId = TheTypeOfTraining;
            AE.YearOfCommencementOfStudy = AdditionalEducationYearOfCommencementOfStudy;
            AE.TheDurationOfTraining = AdditionalEducationTheDurationOfTraining;
            AE.NameOfSchool = AdditionalEducationNameOfSchool;
            AE.Teacher = AdditionalEducationTeacher;
            RC.AdditionalEducation.Add(AE);
            RC.SaveChanges();
            ViewBag.AdditionalEducation = AdditionalEducation.GetItems(id);
            RC.Dispose();
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteAdditionalEducation(int? id, int? id2)
        {
            ResumeContext RC = new ResumeContext();
            AdditionalEducation ae = RC.AdditionalEducation.Find(id);


            RC.AdditionalEducation.Remove(ae);
            RC.SaveChanges();

            ViewBag.AdditionalEducation = AdditionalEducation.GetItems(id2);
            RC.Dispose();
            return PartialView("AddAdditionalEducation");
        }

        [HttpGet]
        public ActionResult ChangeAdditionalEducation(int? id)
        {
            ResumeContext RC = new ResumeContext();
            AdditionalEducation ae = RC.AdditionalEducation.Find(id);
            ViewBag.ID = id;
            ViewBag.AdditionalEducationName = ae.Name;
            ViewBag.TheTypeOfTraining = new SelectList(RC.TheTypeOfTraining, "Id", "Name");
            ViewBag.AdditionalEducationYearOfCommencementOfStudy = ae.YearOfCommencementOfStudy;
            ViewBag.AdditionalEducationTheDurationOfTraining = ae.TheDurationOfTraining;
            ViewBag.AdditionalEducationNameOfSchool = ae.NameOfSchool;
            ViewBag.AdditionalEducationTeacher = ae.Teacher;
            
            return PartialView();
        }

        [HttpPost]
        public ActionResult ChangeAdditionalEducation(int? id, string AdditionalEducationName, int? TheTypeOfTraining, string AdditionalEducationYearOfCommencementOfStudy,
            string AdditionalEducationTheDurationOfTraining, string AdditionalEducationNameOfSchool, string AdditionalEducationTeacher)
        {
            ResumeContext RC = new ResumeContext();
            AdditionalEducation AE = RC.AdditionalEducation.Find(id);
            AE.Name = AdditionalEducationName;
            AE.TheTypeOfTrainingId = TheTypeOfTraining;
            AE.YearOfCommencementOfStudy = AdditionalEducationYearOfCommencementOfStudy;
            AE.TheDurationOfTraining = AdditionalEducationTheDurationOfTraining;
            AE.NameOfSchool = AdditionalEducationNameOfSchool;
            AE.Teacher = AdditionalEducationTeacher;

            RC.Entry(AE).State = System.Data.Entity.EntityState.Modified;
            RC.SaveChanges();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            ViewBag.AdditionalEducation = AdditionalEducation.GetItems(user.ResumeID);
            RC.Dispose();
            return PartialView("AddAdditionalEducation");
        }




        [HttpPost]
        public ActionResult AddLanguage(int? id, int? Language, int? TheLevelOfLanguageLearning)
        {
            ResumeContext RC = new ResumeContext();
            KnowledgeOfForeignLanguages L = new KnowledgeOfForeignLanguages();
            L.LanguageId = Language;
            L.TheLevelOfLanguageLearningId = TheLevelOfLanguageLearning;
            L.ResumeId = id;
            RC.KnowledgeOfForeignLanguages.Add(L);
            RC.SaveChanges();
            ViewBag.Language = KnowledgeOfForeignLanguages.GetItems(id);

            RC.Dispose();
            return PartialView();
        }

        public ActionResult DeleteLanguage(int? id, int? id2)
        {
            ResumeContext RC = new ResumeContext();
            KnowledgeOfForeignLanguages kn = RC.KnowledgeOfForeignLanguages.Find(id);


            RC.KnowledgeOfForeignLanguages.Remove(kn);
            RC.SaveChanges();

            ViewBag.Language = KnowledgeOfForeignLanguages.GetItems(id2);
            RC.Dispose();
            return PartialView("AddLanguage");
        }

        [HttpPost]
        public ActionResult AddComputer(int? id, int? Skill, int? SkillLevel)
        {
            ResumeContext RC = new ResumeContext();
            Ability A = new Ability();
            A.SkillId = Skill;
            A.SkillLevelId = SkillLevel;
            A.ResumeId = id;
            RC.Ability.Add(A);
            RC.SaveChanges();
            ViewBag.Ability = Ability.GetItems(id);
            RC.Dispose();
            return PartialView();
        }

        public ActionResult DeleteComputer(int? id, int? id2)
        {
            ResumeContext RC = new ResumeContext();
            Ability ae = RC.Ability.Find(id);


            RC.Ability.Remove(ae);
            RC.SaveChanges();

            ViewBag.Ability = Ability.GetItems(id2);
            RC.Dispose();
            return PartialView("AddComputer");
        }


        [HttpPost]
        public ActionResult AddPosition(int? id, int? ThePost, string WorkingConditions, string ProfessionalTasks,
            string ForMoreInformation)
        {
            ResumeContext RC = new ResumeContext();
            DesiredPosition DP = new DesiredPosition();
            DP.ResumeId = id;
            DP.ThePostId = ThePost;
            RC.DesiredPosition.Add(DP);
            RC.SaveChanges();
            ViewBag.DesiredPosition = DesiredPosition.GetItems(id);
            ViewBag.WorkingConditions = WorkingConditions;
            ViewBag.ProfessionalTasks = ProfessionalTasks;
            ViewBag.ForMoreInformation = ForMoreInformation;
            //RC.Dispose();
            return PartialView();
        }

        public ActionResult DeletePosition(int? id, int? id2, string WorkingConditions, string ProfessionalTasks,
            string ForMoreInformation)
        {
            ResumeContext RC = new ResumeContext();
            DesiredPosition dp = RC.DesiredPosition.Find(id);


            RC.DesiredPosition.Remove(dp);
            RC.SaveChanges();
            ViewBag.DesiredPosition = DesiredPosition.GetItems(id2);
            ViewBag.WorkingConditions = WorkingConditions;
            ViewBag.ProfessionalTasks = ProfessionalTasks;
            ViewBag.ForMoreInformation = ForMoreInformation;
            RC.Dispose();
            return PartialView("AddPosition");
        }



        public ActionResult DocxCreate()
        {

            
            try
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

                ResumeContext RC = new ResumeContext();
                Resume resume = RC.Resume.Find(user.ResumeID);

                TableContent HistoryTC = new TableContent("CareerHistory");
                foreach (CareerHistoryItem item in CareerHistory.GetItems(resume.Id))
                {
                    HistoryTC.AddRow(new FieldContent("Organization", item.Organization),
                    new FieldContent("Industry", item.Industry),
                    new FieldContent("Time", item.Since.Day + "." + item.Since.Month + "." + item.Since.Year + "/"
                    + item.For.Day + "." + item.For.Month + "." + @item.For.Year),
                    new FieldContent("ThePost", item.ThePost), new FieldContent("JobResponsibilities", item.JobResponsibilities),
                    new FieldContent("Achievements", item.Achievements), new FieldContent("Wages", item.Wages),
                    new FieldContent("ReasonForLeaving", item.ReasonForLeaving));
                }
                HistoryTC.AddRow(new FieldContent("Organization", ""), new FieldContent("Industry", ""),
                    new FieldContent("Time", ""), new FieldContent("ThePost", ""), new FieldContent("JobResponsibilities", ""),
                    new FieldContent("Achievements", ""), new FieldContent("Wages", ""), new FieldContent("ReasonForLeaving", ""));


                TableContent EducationTC = new TableContent("Education");
                foreach (EducationItem item in Education.GetItems(resume.Id))
                {
                    EducationTC.AddRow(new FieldContent("Name", item.Name), new FieldContent("LevelProfile", item.Level + "/" + item.Profile),
                    new FieldContent("Time2", item.Since.Day + "." + item.Since.Month + "." + item.Since.Year + "/"
                    + item.For.Day + "." + item.For.Month + "." + @item.For.Year),
                    new FieldContent("Faculty", item.Faculty), new FieldContent("Specialty", item.Specialty),
                    new FieldContent("DiplomaQualification", item.DiplomaQualification),
                    new FieldContent("FormOfTraining", item.FormOfTraining));
                }
                EducationTC.AddRow(new FieldContent("Name", ""), new FieldContent("LevelProfile", ""),
                    new FieldContent("Time2", ""), new FieldContent("Faculty", ""), new FieldContent("Specialty", ""),
                    new FieldContent("DiplomaQualification", ""), new FieldContent("FormOfTraining", ""));


                TableContent AdditionalEducationTC = new TableContent("AdditionalEducation");
                foreach (AdditionalEducationItem item in AdditionalEducation.GetItems(resume.Id))
                {
                    AdditionalEducationTC.AddRow(new FieldContent("Name", item.Name), new FieldContent("TheTypeOfTraining", item.TheTypeOfTraining),
                    new FieldContent("YearOfCommencementOfStudy", item.YearOfCommencementOfStudy), new FieldContent("TheDurationOfTraining", item.TheDurationOfTraining),
                    new FieldContent("NameOfSchool", item.NameOfSchool), new FieldContent("Teacher", item.Teacher));
                }
                AdditionalEducationTC.AddRow(new FieldContent("Name", ""), new FieldContent("TheTypeOfTraining", ""),
                    new FieldContent("YearOfCommencementOfStudy", ""), new FieldContent("TheDurationOfTraining", ""),
                    new FieldContent("NameOfSchool", ""), new FieldContent("Teacher", ""));

                TableContent KnowledgeOfForeignLanguagesTC = new TableContent("KnowledgeOfForeignLanguages");
                foreach (KnowledgeOfForeignLanguagesItem item in KnowledgeOfForeignLanguages.GetItems(resume.Id))
                {
                    KnowledgeOfForeignLanguagesTC.AddRow(new FieldContent("Language", item.Language),
                        new FieldContent("TheLevelOfLanguageLearning", item.TheLevelOfLanguageLearning));
                }
                KnowledgeOfForeignLanguagesTC.AddRow(new FieldContent("Language", ""),
                    new FieldContent("TheLevelOfLanguageLearning", ""));


                TableContent AbilityTC = new TableContent("Ability");
                foreach (AbilityItem item in Ability.GetItems(resume.Id))
                {
                    AbilityTC.AddRow(new FieldContent("Skill", item.Skill), new FieldContent("SkillLevel", item.SkillLevel));
                }
                AbilityTC.AddRow(new FieldContent("Skill", ""), new FieldContent("SkillLevel", ""));

                ListContent list = new ListContent("List");
                foreach (DesiredPositionItem item in DesiredPosition.GetItems(resume.Id))
                {
                    list.AddItem(new FieldContent("ThePost", item.ThePost));
                }
                list.AddItem(new FieldContent("ThePost", ""));

                ImageContent image = new ImageContent("Image", resume.ImageByte);
                FieldContent SC;
                FieldContent TP;
                FieldContent SB;
                FieldContent SP;
                if (resume.SalaryChek)
                {
                    SC = new FieldContent("SalaryChek", "+");
                }
                else
                {
                    SC = new FieldContent("SalaryChek", "");
                }
                if (resume.ThePercentage)
                {
                    TP = new FieldContent("ThePercentage", "+");
                }
                else
                {
                    TP = new FieldContent("ThePercentage", "");
                }
                if (resume.SalaryBonus)
                {
                    SB = new FieldContent("SalaryBonus", "+");
                }
                else
                {
                    SB = new FieldContent("SalaryBonus", "");
                }
                if (resume.SalaryPercentage)
                {
                    SP = new FieldContent("SalaryPercentage", "+");
                }
                else
                {
                    SP = new FieldContent("SalaryPercentage", "");
                }

                var valuesToFill = new Content(
                    list,
                    image,
                    new FieldContent("FIO", resume.Surname + " " + resume.Name + " " + resume.MiddleName),
                    new FieldContent("DateOfBirth", resume.DateOfBirth.Day + "." + resume.DateOfBirth.Month + "." + resume.DateOfBirth.Year),
                    new FieldContent("MobilePhone", resume.WorkNumber),
                    new FieldContent("HomePhone", resume.HomeNumber),
                    new FieldContent("Gender", resume.Gender),
                    new FieldContent("Citizenship", resume.Citizenship),
                    new FieldContent("MaritalStatus", resume.MaritalStatus),
                    new FieldContent("TheCompositionOfTheFamily", resume.TheCompositionOfTheFamily),
                    new FieldContent("DriversLicense", resume.DriversLicense),
                    new FieldContent("CarBrand", resume.CarBrand),
                    new FieldContent("Adress", resume.Adress),
                    new FieldContent("Email", user.Email),
                    HistoryTC,
                    EducationTC,
                    AdditionalEducationTC,
                    KnowledgeOfForeignLanguagesTC,
                    AbilityTC,
                    new FieldContent("TheBusinessAndPsychologicalQualities", resume.TheBusinessAndPsychologicalQualities),
                    new FieldContent("ProfessionalSkills", resume.ProfessionalSkills),
                    new FieldContent("Hobbies", resume.Hobbies),
                    new FieldContent("WorkingConditions", resume.WorkingConditions),
                    new FieldContent("ProfessionalTasks", resume.ProfessionalTasks),
                    new FieldContent("ForMoreInformation", resume.ForMoreInformation),
                    new FieldContent("Salary", resume.Salary),
                    new FieldContent("MinSalary", resume.MinSalary),
                    new FieldContent("NormSalary", resume.NormSalary),
                    //
                    SC,//new FieldContent("SalaryChek", ""),
                    TP,//new FieldContent("ThePercentage", ""),
                    SB,//new FieldContent("SalaryBonus", ""),
                    SP,//new FieldContent("SalaryPercentage", ""),
                       //
                    new FieldContent("Comment", resume.Comment),


                    new FieldContent("Income", resume.Income),
                    new FieldContent("TheProspectOfJobGrowth", resume.TheProspectOfJobGrowth),
                    new FieldContent("ToGetTheNecessaryExperience", resume.ToGetTheNecessaryExperience),
                    new FieldContent("ToImproveTheProfessionalLevel", resume.ToImproveTheProfessionalLevel),
                    new FieldContent("ToDemonstrateTheirAbilities", resume.ToDemonstrateTheirAbilities),
                    new FieldContent("AHighLevelOfAutonomy", resume.AHighLevelOfAutonomy),
                    new FieldContent("TheStabilityOfTheCompany", resume.TheStabilityOfTheCompany),
                    new FieldContent("ActivitiesOfTheCompany", resume.ActivitiesOfTheCompany),
                    new FieldContent("WorkingConditionsInTheWorkplace", resume.WorkingConditionsInTheWorkplace),
                    new FieldContent("RelationsWithTheLeadership", resume.RelationsWithTheLeadership),
                    new FieldContent("SomethingElse", resume.SomethingElse),
                    new FieldContent("Date", DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year)
                    );

                //
                System.IO.File.Delete(Server.MapPath("~/Files/Resume.docx"));
                System.IO.File.Copy(Server.MapPath("~/Files/Template.docx"), Server.MapPath("~/Files/Resume.docx"));
                //File.Copy("ResumeTemplate.docx", "~/Files/Resume.docx");
                using (var outputDocument = new TemplateProcessor(Server.MapPath("~/Files/Resume.docx")).SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(valuesToFill);
                    outputDocument.SaveChanges();
                    outputDocument.Dispose();
                    //var doc = System.IO.File.ReadAllBytes("~/Files/Resume.docx");
                }
                RC.Dispose();
                return File(System.IO.File.ReadAllBytes(Server.MapPath("~/Files/Resume.docx")), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "MyResume.docx");
            }//"D:/3 курс 2 семестр/РПИ/WebApplication1/WebApplication1/App_Data/Resume.docx"
            catch
            {
                
                return View("Eror");
            }
            
        }


        



        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}