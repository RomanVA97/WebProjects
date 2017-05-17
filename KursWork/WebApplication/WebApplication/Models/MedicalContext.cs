using System.Data.Entity;

namespace WebApplication.Models
{
    public class MedicalContext : DbContext
    {
        
        public DbSet<AListOfDoctorsAppointment> ALODA { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<DentalPatientCard> DPC { get; set; }
        public DbSet<DentalStatusDPC> DSDPC { get; set; }
        public DbSet<DiaryVisitsDPC> DVDPC { get; set; }
        public DbSet<DoctorQualification> DQ { get; set; }
        public DbSet<ExaminationOfThePatientDuringInitialTreatment> EOTPDIT { get; set; }
        public DbSet<GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment> GTPATTROEOTPDIT { get; set; }
        public DbSet<HealthOrganization> HO {get;set;}
        public DbSet<InstitutionEducation> IE {get;set;}
        public DbSet<MedicalDirection> MD {get;set;}
        public DbSet<MedicalDoctor> MDoc {get;set;}
        public DbSet<MedicalProfile> MP {get;set;}
        public DbSet<MedicalQualification> MQ {get;set;}
        public DbSet<RecordOnReceptionToTheDoctor> RORTTD { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<StructuralDivision> SD {get;set;}
        public DbSet<TheListOfAppointmentsAndAccountingLoadsOfRadiographic> TLOAAALOR { get; set; }
        public DbSet<User> U { get; set; }
        
    }
}