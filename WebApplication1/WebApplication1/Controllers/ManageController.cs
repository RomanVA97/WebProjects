using System;
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
                resume.DateOfBirth = DateTime.Now;
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