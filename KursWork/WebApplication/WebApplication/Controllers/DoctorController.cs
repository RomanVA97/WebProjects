using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        [Authorize(Roles = "doctor")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult ViewingAppointments()
        {

            return View();
        }

        

        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult GetAppointments(DateTime date)
        {
            MedicalContext MC = new MedicalContext();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor MD = MC.MDoc.Single(c => c.UserId == user.UserId);
            List<RecordOnReceptionToTheDoctor> list = MC.RORTTD.Where(c => c.MedicalDoctor.Id == MD.Id && c.DateOfAdmission==date).ToList();
            DateTime DT;
            if (MD.HoursOnEvenNumbers == date)
            {
                DT = MD.HoursOnEvenNumbers;
            }
            else
            {
                DT = MD.HoursOnOddNumbers;
            }

            List<GetAppointmentsItem> listItems = new List<GetAppointmentsItem>();
            foreach (RecordOnReceptionToTheDoctor item in list)
            {
                int hour = DT.Hour, minute = DT.Minute;
                User u = MC.U.Find(item.UserId);
                GetAppointmentsItem gAI = new GetAppointmentsItem();
                gAI.FIO = u.SurName + " " + u.Name + " " + u.MiddleName;
                gAI.Id = u.Id;
                gAI.PasportNumber = u.NumberAndSeriesOfPassport;

                int roomPass = (int)item.RoomPass;
                int a = (DT.Minute + (roomPass * 60)) / 60;
                int b = (DT.Minute + (roomPass * 60)) % 60;
                gAI.Time = a + DT.Hour + " : ";
                if (b < 10) gAI.Time += "0";
                gAI.Time += b;
                listItems.Add(gAI);
            }

            ViewBag.Items = listItems;
            return View();
        }


        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult TakeAPatient(int? id)
        {
            MedicalContext MC = new MedicalContext();
            DentalPatientCard dpc;
            List<DentalStatusDPC> ds;
            ExaminationOfThePatientDuringInitialTreatment eotpdit;
            List<TheListOfAppointmentsAndAccountingLoadsOfRadiographic> tloaaalor;
            List<GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment> gtpattroeotpdit;
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor MD = MC.MDoc.Single(c => c.UserId == user.UserId);
            try
            {
                dpc = MC.DPC.Single(c => c.UserId == id);
            }
            catch
            {
                
                dpc = new DentalPatientCard();
                dpc.UserId = id;
                dpc.Data = DateTime.Now;
                dpc.HealthOrganizationId = MC.SD.Find(MD.StructuralDivisionId).HealthOrganizationId;
                    
                MC.DPC.Add(dpc);
                MC.SaveChanges();
            }
            ViewBag.Id = dpc.Id;

            tloaaalor = MC.TLOAAALOR.Where(c => c.DentalPatientCardId == dpc.Id).ToList();
            tloaaalor.Reverse();
            ViewBag.tloaaalor = tloaaalor;
            
            ds = MC.DSDPC.Where(c => c.DentalPatientCardId == dpc.Id).ToList();
            ds.Reverse();
            ViewBag.ds = ds;


            try
            {
                eotpdit = MC.EOTPDIT.Single(c => c.DentalPatientCardId == dpc.Id);
                
            }
            catch
            {
                eotpdit = new ExaminationOfThePatientDuringInitialTreatment();
                eotpdit.Date = DateTime.Now;
                eotpdit.MedicalDoctorId = MD.Id;
                eotpdit.DentalPatientCardId = dpc.Id;
                MC.EOTPDIT.Add(eotpdit);
                MC.SaveChanges();
            }
            ViewBag.eotpdit = eotpdit;

            gtpattroeotpdit = MC.GTPATTROEOTPDIT.Where(c => c.DentalPatientCardId == dpc.Id).ToList();
            gtpattroeotpdit.Reverse();
            ViewBag.eotpdit = eotpdit;


            gtpattroeotpdit = MC.GTPATTROEOTPDIT.Where(c => c.DentalPatientCardId == dpc.Id).ToList();
            gtpattroeotpdit.Reverse();
            ViewBag.gtpattroeotpdit = gtpattroeotpdit;


            return View();
        }
        


        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult AddTheListOfAppointmentsAndAccountingLoadsOfRadiographic(int? id, string assignedTypeOfResearch, string theOrganizationWhichConductedTheStudy, DateTime date, float equivalentDose)
        {
            MedicalContext MC = new MedicalContext();
            TheListOfAppointmentsAndAccountingLoadsOfRadiographic tloaaalor = new TheListOfAppointmentsAndAccountingLoadsOfRadiographic();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor MD = MC.MDoc.Single(c => c.UserId == user.UserId);
            tloaaalor.MedicalDoctorId = MD.Id;
            tloaaalor.TheOrganizationWhichConductedTheStudy = theOrganizationWhichConductedTheStudy;
            tloaaalor.EquivalentDose = equivalentDose;
            tloaaalor.AssignedTypeOfResearch = assignedTypeOfResearch;
            tloaaalor.Date = date;
            tloaaalor.DentalPatientCardId = id;

            MC.TLOAAALOR.Add(tloaaalor);
            MC.SaveChanges();
            List<TheListOfAppointmentsAndAccountingLoadsOfRadiographic> list = MC.TLOAAALOR.Where(c => c.DentalPatientCardId == id).ToList();
            list.Reverse();
            ViewBag.tloaaalor = list;
            return PartialView();
        }


        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult AddDentalStatusDPC(int? id, string OHI_S, string KPI, string Bite, string TheStatusOfHardTissueOfTeethPeriodontal, string TheConditionOfTheOralMucosa, string DataX_rayAndOtherStudies, string ThePreliminaryDiagnosis, string t18, string t17, string t16, string t15, string t14, string t13, string t12, string t11, string t21,  string t22, string t23, string t24, string t25, string t26, string t27, string t28, string t48, string t47, string t46, string t45, string t44, string t43, string t42, string t41, string t31, string t32, string t33, string t34, string t35, string t36, string t37, string t38)
        {
            DentalStatusDPC ds = new DentalStatusDPC();
            ds.DentalPatientCardId = id;
            ds.OHI_S = OHI_S;
            ds.KPI = KPI;
            ds.Bite = Bite;
            ds.TheStatusOfHardTissueOfTeethPeriodontal = TheStatusOfHardTissueOfTeethPeriodontal;
            ds.TheConditionOfTheOralMucosa = TheConditionOfTheOralMucosa;
            ds.DataX_rayAndOtherStudies = DataX_rayAndOtherStudies;
            ds.ThePreliminaryDiagnosis = ThePreliminaryDiagnosis;
            ds.Date = DateTime.Now;

            ds.ToothNumber18 = t18;
            ds.ToothNumber17 = t17;
            ds.ToothNumber16 = t16;
            ds.ToothNumber15 = t15;
            ds.ToothNumber14 = t14;
            ds.ToothNumber13 = t13;
            ds.ToothNumber12 = t12;
            ds.ToothNumber11 = t11;
            ds.ToothNumber21 = t21;
            ds.ToothNumber22 = t22;
            ds.ToothNumber23 = t23;
            ds.ToothNumber24 = t24;
            ds.ToothNumber25 = t25;
            ds.ToothNumber26 = t26;
            ds.ToothNumber27 = t27;
            ds.ToothNumber28 = t28;
            ds.ToothNumber48 = t48;
            ds.ToothNumber47 = t47;
            ds.ToothNumber46 = t46;
            ds.ToothNumber45 = t45;
            ds.ToothNumber44 = t44;
            ds.ToothNumber43 = t43;
            ds.ToothNumber42 = t42;
            ds.ToothNumber41 = t41;
            ds.ToothNumber31 = t31;
            ds.ToothNumber32 = t32;
            ds.ToothNumber33 = t33;
            ds.ToothNumber34 = t34;
            ds.ToothNumber35 = t35;
            ds.ToothNumber36 = t36;
            ds.ToothNumber37 = t37;
            ds.ToothNumber38 = t38;
            MedicalContext MC = new MedicalContext();

            MC.DSDPC.Add(ds);
            MC.SaveChanges();

            List<DentalStatusDPC> list = MC.DSDPC.Where(c => c.DentalPatientCardId == id).ToList();
            list.Reverse();
            ViewBag.ds = list;


            return PartialView();
        }


        [Authorize(Roles = "doctor")]
        [HttpGet]
        public void ChangeExaminationOfThePatientDuringInitialTreatment(int? id, string DiseaseOfTheCardiovascularSystem, string DiseaseOfTheNervousSystem, string DiseaseOfTheEndocrineSystem, string DiseaseOfTheDigestiveSystem, string DiseaseOfTheRespiratorySystem, string InfectiousDiseases, string AllergicReactions, string TheConstantUseOfDrugs, string HarmfulFactorsOfProductionEnvironment, string PregnancyPostpartumPeriod, string More, string ExternalExamination, string TheConditionOfTheSkin, string TheStatusOfTheRegionalLymphNodes, string TheConditionOfTheTemporomandibularJoint)
        {
            MedicalContext mc = new MedicalContext();
            ExaminationOfThePatientDuringInitialTreatment eotpdit = mc.EOTPDIT.Single(c=>c.DentalPatientCardId==id);
            eotpdit.DiseaseOfTheCardiovascularSystem = DiseaseOfTheCardiovascularSystem;
            eotpdit.DiseaseOfTheNervousSystem = DiseaseOfTheNervousSystem;
            eotpdit.DiseaseOfTheEndocrineSystem = DiseaseOfTheEndocrineSystem;
            eotpdit.DiseaseOfTheDigestiveSystem = DiseaseOfTheDigestiveSystem;
            eotpdit.DiseaseOfTheRespiratorySystem = DiseaseOfTheRespiratorySystem;
            eotpdit.InfectiousDiseases = InfectiousDiseases;
            eotpdit.AllergicReactions = AllergicReactions;
            eotpdit.TheConstantUseOfDrugs = TheConstantUseOfDrugs;
            eotpdit.HarmfulFactorsOfProductionEnvironment = HarmfulFactorsOfProductionEnvironment;
            eotpdit.PregnancyPostpartumPeriod = PregnancyPostpartumPeriod;
            eotpdit.More = More;
            eotpdit.ExternalExamination = ExternalExamination;
            eotpdit.TheConditionOfTheSkin = TheConditionOfTheSkin;
            eotpdit.TheStatusOfTheRegionalLymphNodes = TheStatusOfTheRegionalLymphNodes;
            eotpdit.TheConditionOfTheTemporomandibularJoint = TheConditionOfTheTemporomandibularJoint;
            mc.Entry(eotpdit).State = System.Data.Entity.EntityState.Modified;
            mc.SaveChanges();
            
        }



        
        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult AddGeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment(int? id, string emergencyCare, string preventiveMeasures, string hygieneEducation, string therapeuticTreatment, string surgicalTreatment, string orthopedicTreatment, string orthodonticTreatment, string additionalDiagnosticMeasures, string consultOtherSpecialists)
        {
            MedicalContext MC = new MedicalContext();
            TheListOfAppointmentsAndAccountingLoadsOfRadiographic tloaaalor = new TheListOfAppointmentsAndAccountingLoadsOfRadiographic();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor MD = MC.MDoc.Single(c => c.UserId == user.UserId);
            GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment gtpattroeotpdin = new GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment();
            gtpattroeotpdin.Date = DateTime.Now;
            gtpattroeotpdin.MedicalDoctorId = MD.Id;
            gtpattroeotpdin.DentalPatientCardId = id;
            gtpattroeotpdin.EmergencyCare = emergencyCare;
            gtpattroeotpdin.PreventiveMeasures = preventiveMeasures;
            gtpattroeotpdin.HygieneEducation = hygieneEducation;
            gtpattroeotpdin.TherapeuticTreatment = therapeuticTreatment;
            gtpattroeotpdin.SurgicalTreatment = surgicalTreatment;
            gtpattroeotpdin.OrthodonticTreatment = orthodonticTreatment;
            gtpattroeotpdin.OrthopedicTreatment = orthopedicTreatment;
            gtpattroeotpdin.AdditionalDiagnosticMeasures = additionalDiagnosticMeasures;
            gtpattroeotpdin.ConsultOtherSpecialists = consultOtherSpecialists;

            MC.GTPATTROEOTPDIT.Add(gtpattroeotpdin);
            MC.SaveChanges();

            List<GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment> list = MC.GTPATTROEOTPDIT.Where(c => c.DentalPatientCardId == id).ToList();
            list.Reverse();
            ViewBag.gtpattroeotpdit = list;

            return PartialView();
        }



        [Authorize(Roles = "doctor")]
        [HttpGet]
        public void AddDiaryVisitsDPC(int? id, string entry, string appointment)
        {
            MedicalContext MC = new MedicalContext();
            DiaryVisitsDPC dv = new DiaryVisitsDPC();
            dv.Date = DateTime.Now;
            dv.DentalPatientCardId = id;
            dv.Entry = entry;
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor MD = MC.MDoc.Single(c => c.UserId == user.UserId);
            dv.MedicalDoctorId = MD.Id;

            AListOfDoctorsAppointment aloda = new AListOfDoctorsAppointment();
            aloda.MedicalDoctorId = MD.Id;
            aloda.Appointments = appointment;
            aloda.UserId = MC.DPC.Find(id).UserId;

            MC.ALODA.Add(aloda);
            MC.SaveChanges();

            dv.AListOfDoctorsAppointmentId = aloda.Id;

            MC.DVDPC.Add(dv);
            MC.SaveChanges();
            
        }
        

        
        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult PatientAcceptance()
        {

            return View();
        }
        [Authorize(Roles = "doctor")]
        [HttpPost]
        public ActionResult PatientAcceptance(string numberPassport)
        {
            MedicalContext mc = new MedicalContext();
            User u;
            try
            {
                u = mc.U.Single(c => c.NumberAndSeriesOfPassport == numberPassport);
            }
            catch
            {
                return HttpNotFound();
            }




            return RedirectToAction("TakeAPatient", "Doctor", new { id = u.Id });
        }

        [Authorize(Roles = "doctor")]
        [HttpGet]
        public ActionResult DataDoctor()
        {
            MedicalContext mc = new MedicalContext();

            SelectList structuralDivision = new SelectList(mc.SD, "Id", "NameOfStructuralDivision");
            ViewBag.structuralDivision = structuralDivision;

            SelectList medicalDirection = new SelectList(mc.MD, "Id", "NameOfMedicalDirection");
            ViewBag.medicalDirection = medicalDirection;

            SelectList medicalProfile = new SelectList(mc.MP, "Id", "NameOfMedicalProfile");
            ViewBag.medicalProfile = medicalProfile;

            SelectList medicalQualification = new SelectList(mc.MQ, "Id", "NameOfMedicalQualification");
            ViewBag.medicalQualification = medicalQualification;

            SelectList doctorQualification = new SelectList(mc.DQ, "Id", "Qualification");
            ViewBag.doctorQualification = doctorQualification;


            return View();
        }
        [Authorize(Roles = "doctor")]
        [HttpPost]
        public ActionResult DataDoctor(int structuralDivision, int medicalDirection, int medicalProfile, int medicalQualification,
            int doctorQualification, int office, DateTime hoursOnEvenNumbers, DateTime hoursOnOddNumbers)
        {
            MedicalContext mc = new MedicalContext();
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            MedicalDoctor md;
            bool chek = true;
            try
            {
                md = mc.MDoc.Single(c => c.UserId == user.UserId);
            }
            catch
            {
                md = new MedicalDoctor();
                chek = false;
            }
            md.UserId = user.UserId;
            md.StructuralDivisionId = structuralDivision;
            md.MedicalDirectionId = medicalDirection;
            md.MedicalProfileId = medicalProfile;
            md.MedicalQualificationId = medicalQualification;
            md.DoctorQualificationId = doctorQualification;
            md.Office = office;
            md.HoursOnEvenNumbers = hoursOnEvenNumbers;
            md.HoursOnOddNumbers = hoursOnOddNumbers;
            if(chek)
            {
                mc.Entry(md).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                mc.MDoc.Add(md);
            }
            mc.SaveChanges();
            return RedirectPermanent("Index");
        }

    }
}