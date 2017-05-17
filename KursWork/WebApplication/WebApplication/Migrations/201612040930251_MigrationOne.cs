namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MapOfDentalHealthStudents", "HealthOrganizationId", "dbo.HealthOrganizations");
            DropForeignKey("dbo.MapOfDentalHealthStudents", "InstitutionEducationId", "dbo.InstitutionEducations");
            DropForeignKey("dbo.MapOfDentalHealthStudents", "UserId", "dbo.Users");
            DropForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.DentalStatusMODHS", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "AListOfDoctorsAppointmentId", "dbo.AListOfDoctorsAppointments");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ElementLeafDailyAccountingWorkOfADentists", "LeafDailyAccountingWorkOfADentistId", "dbo.LeafDailyAccountingWorkOfADentists");
            DropForeignKey("dbo.LeafDailyAccountingWorkOfADentists", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", "PassportDentalHealthOfPupilsOfEducationalInstitutionsId", "dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropForeignKey("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId", "dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropForeignKey("dbo.TheOverallTreatmentPlanStudents", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", new[] { "MedicalDoctorId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "UserId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "HealthOrganizationId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "InstitutionEducationId" });
            DropIndex("dbo.DentalStatusMODHS", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "MedicalDoctorId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "AListOfDoctorsAppointmentId" });
            DropIndex("dbo.ElementLeafDailyAccountingWorkOfADentists", new[] { "LeafDailyAccountingWorkOfADentistId" });
            DropIndex("dbo.LeafDailyAccountingWorkOfADentists", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", new[] { "PassportDentalHealthOfPupilsOfEducationalInstitutionsId" });
            DropIndex("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", new[] { "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId" });
            DropIndex("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", new[] { "MedicalDoctorId" });
            DropIndex("dbo.TheOverallTreatmentPlanStudents", new[] { "MapOfDentalHealthStudentId" });
            DropTable("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations");
            DropTable("dbo.MapOfDentalHealthStudents");
            DropTable("dbo.DentalStatusMODHS");
            DropTable("dbo.DiaryVisitsMODHCs");
            DropTable("dbo.ElementLeafDailyAccountingWorkOfADentists");
            DropTable("dbo.LeafDailyAccountingWorkOfADentists");
            DropTable("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropTable("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropTable("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropTable("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropTable("dbo.TheOverallTreatmentPlanStudents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TheOverallTreatmentPlanStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapOfDentalHealthStudentId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        EmergencyCare = c.String(),
                        PreventiveMeasures = c.String(),
                        TherapeuticTreatment = c.String(),
                        SurgicalTreatment = c.String(),
                        OrthopedicTreatment = c.String(),
                        More = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId = c.Int(),
                        AdoptedChildrenUnderTheAgeOf6YearsOnly = c.Int(nullable: false),
                        TheNumberOfHealthyPatientsWithIntactTeeth = c.Int(nullable: false),
                        KPUTheAmount1 = c.Int(nullable: false),
                        K1 = c.Int(nullable: false),
                        P1 = c.Int(nullable: false),
                        U1 = c.Int(nullable: false),
                        TheOralHygienePLITheAmount = c.Int(nullable: false),
                        AdoptedChildrenUnderTheAgeOf12YearsOnly = c.Int(nullable: false),
                        TheNumberOfHealthyPatientsWithIntactTeeth2 = c.Int(nullable: false),
                        KPUTheAmount2 = c.Int(nullable: false),
                        K2 = c.Int(nullable: false),
                        P2 = c.Int(nullable: false),
                        U2 = c.Int(nullable: false),
                        TheOralHygieneOHI_STheAmount = c.Int(nullable: false),
                        ThePeriodontalStatusOfTheKPIAmount = c.Int(nullable: false),
                        AdoptedChildrenUnderTheAgeOf15YearsOnly = c.Int(nullable: false),
                        TheNumberOfHealthyPatientsWithIntactTeeth3 = c.Int(nullable: false),
                        KPUTheAmount3 = c.Int(nullable: false),
                        K3 = c.Int(nullable: false),
                        P3 = c.Int(nullable: false),
                        U3 = c.Int(nullable: false),
                        TheOralHygieneOHI_S2TheAmount = c.Int(nullable: false),
                        ThePeriodontalStatusOfTheKPIAmount2 = c.Int(nullable: false),
                        AdoptedChildrenUnderTheAgeOf18YearsOnly = c.Int(nullable: false),
                        TheNumberOfHealthyPatientsWithIntactTeeth4 = c.Int(nullable: false),
                        KPUTheAmount4 = c.Int(nullable: false),
                        K4 = c.Int(nullable: false),
                        P4 = c.Int(nullable: false),
                        U4 = c.Int(nullable: false),
                        TheOralHygieneOHI_S3TheAmount = c.Int(nullable: false),
                        ThePeriodontalStatusOfTheKPIAmount3 = c.Int(nullable: false),
                        AdoptedAdultsOfAge35_44Total = c.Int(nullable: false),
                        KPUTheAmount5 = c.Int(nullable: false),
                        K5 = c.Int(nullable: false),
                        P5 = c.Int(nullable: false),
                        U5 = c.Int(nullable: false),
                        AdoptedAdultsAge65AndOverTotal = c.Int(nullable: false),
                        KPUTheAmount6 = c.Int(nullable: false),
                        K6 = c.Int(nullable: false),
                        P6 = c.Int(nullable: false),
                        U6 = c.Int(nullable: false),
                        TheNumberOfPersonsWhoCompletelyLostTheirTeeth = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Since = c.DateTime(nullable: false),
                        For = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PassportDentalHealthOfPupilsOfEducationalInstitutionsId = c.Int(),
                        NumberOfStudentsTotalPersons = c.Int(nullable: false),
                        ViewedByTotalPersons = c.Int(nullable: false),
                        Healthy = c.Int(nullable: false),
                        PreviouslySanitized = c.Int(nullable: false),
                        NeededRehabilitation = c.Int(nullable: false),
                        Improved = c.Int(nullable: false),
                        AHealthyPercentageOfTheNumberExamined = c.Single(nullable: false),
                        TheIntensityOfСaries = c.Int(nullable: false),
                        TheActivityOfCariesLow = c.Int(nullable: false),
                        TheActivityOfCariesMedium = c.Int(nullable: false),
                        TheActivityOfCariesHigh = c.Int(nullable: false),
                        TheActivityOfCariesVeryHigh = c.Int(nullable: false),
                        IndexOfHygieneOHI_S = c.Int(nullable: false),
                        ComprehensivePeriodontalIndex = c.Int(nullable: false),
                        TakenPreventiveMeasuresHealthEducation = c.Int(nullable: false),
                        TakenPreventiveMeasuresFissureSealing = c.Int(nullable: false),
                        CariesPermanentTeeth = c.Int(nullable: false),
                        CariesTemporaryTeeth = c.Int(nullable: false),
                        SealedPermanentTeeth = c.Int(nullable: false),
                        SealedTemporaryTeeth = c.Int(nullable: false),
                        ComplicatedCariesPermanentTeeth = c.Int(nullable: false),
                        ComplicatedCariesTemporaryTeeth = c.Int(nullable: false),
                        RefusedTreatmentInspection = c.Int(nullable: false),
                        DirectedToTheRemovalOfTeeth = c.Int(nullable: false),
                        NeedOrthodonticTreatment = c.Int(nullable: false),
                        ConductedHealthLessons = c.Int(nullable: false),
                        TrainingTeacher = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeafDailyAccountingWorkOfADentists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ElementLeafDailyAccountingWorkOfADentists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeafDailyAccountingWorkOfADentistId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Surname = c.String(),
                        Name = c.String(),
                        MiddleName = c.String(),
                        TheNumberOfFullYears = c.Int(nullable: false),
                        Adress = c.String(),
                        KodeMKB_10 = c.String(),
                        Description = c.String(),
                        Treatment = c.String(),
                        ViewVisit = c.String(),
                        CodeKeyGroup = c.String(),
                        IntactDentition = c.String(),
                        K = c.Int(nullable: false),
                        P = c.Int(nullable: false),
                        U = c.Int(nullable: false),
                        Just = c.Int(nullable: false),
                        OHI_S_PL1 = c.String(),
                        KPI = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiaryVisitsMODHCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapOfDentalHealthStudentId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                        Entry = c.String(),
                        AListOfDoctorsAppointmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DentalStatusMODHS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapOfDentalHealthStudentId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Bite = c.String(),
                        ToothNumber55 = c.String(),
                        ToothNumber54 = c.String(),
                        ToothNumber53 = c.String(),
                        ToothNumber52 = c.String(),
                        ToothNumber51 = c.String(),
                        ToothNumber61 = c.String(),
                        ToothNumber62 = c.String(),
                        ToothNumber63 = c.String(),
                        ToothNumber64 = c.String(),
                        ToothNumber65 = c.String(),
                        ToothNumber18 = c.String(),
                        ToothNumber17 = c.String(),
                        ToothNumber16 = c.String(),
                        ToothNumber15 = c.String(),
                        ToothNumber14 = c.String(),
                        ToothNumber13 = c.String(),
                        ToothNumber12 = c.String(),
                        ToothNumber11 = c.String(),
                        ToothNumber21 = c.String(),
                        ToothNumber22 = c.String(),
                        ToothNumber23 = c.String(),
                        ToothNumber24 = c.String(),
                        ToothNumber25 = c.String(),
                        ToothNumber26 = c.String(),
                        ToothNumber27 = c.String(),
                        ToothNumber28 = c.String(),
                        ToothNumber48 = c.String(),
                        ToothNumber47 = c.String(),
                        ToothNumber46 = c.String(),
                        ToothNumber45 = c.String(),
                        ToothNumber44 = c.String(),
                        ToothNumber43 = c.String(),
                        ToothNumber42 = c.String(),
                        ToothNumber41 = c.String(),
                        ToothNumber31 = c.String(),
                        ToothNumber32 = c.String(),
                        ToothNumber33 = c.String(),
                        ToothNumber34 = c.String(),
                        ToothNumber35 = c.String(),
                        ToothNumber36 = c.String(),
                        ToothNumber37 = c.String(),
                        ToothNumber38 = c.String(),
                        ToothNumber85 = c.String(),
                        ToothNumber84 = c.String(),
                        ToothNumber83 = c.String(),
                        ToothNumber82 = c.String(),
                        ToothNumber81 = c.String(),
                        ToothNumber71 = c.String(),
                        ToothNumber72 = c.String(),
                        ToothNumber73 = c.String(),
                        ToothNumber74 = c.String(),
                        ToothNumber75 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MapOfDentalHealthStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        HealthOrganizationId = c.Int(),
                        Data = c.DateTime(nullable: false),
                        InstitutionEducationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MapOfDentalHealthStudentId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                        SchoolClass = c.Int(nullable: false),
                        OHI_S1 = c.Int(nullable: false),
                        OHI_S2 = c.Int(nullable: false),
                        OHI_S3 = c.Int(nullable: false),
                        RatingIndexOHI_S = c.Int(nullable: false),
                        KPI1 = c.Int(nullable: false),
                        KPI2 = c.Int(nullable: false),
                        KPI3 = c.Int(nullable: false),
                        RatingIndexKPI = c.Int(nullable: false),
                        K = c.Int(nullable: false),
                        P = c.Int(nullable: false),
                        U = c.Int(nullable: false),
                        KP = c.Int(nullable: false),
                        RatingIndexKPU = c.Int(nullable: false),
                        UIK = c.Int(nullable: false),
                        RatingIndexUIK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TheOverallTreatmentPlanStudents", "MapOfDentalHealthStudentId");
            CreateIndex("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "MedicalDoctorId");
            CreateIndex("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId");
            CreateIndex("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", "MedicalDoctorId");
            CreateIndex("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", "PassportDentalHealthOfPupilsOfEducationalInstitutionsId");
            CreateIndex("dbo.LeafDailyAccountingWorkOfADentists", "MedicalDoctorId");
            CreateIndex("dbo.ElementLeafDailyAccountingWorkOfADentists", "LeafDailyAccountingWorkOfADentistId");
            CreateIndex("dbo.DiaryVisitsMODHCs", "AListOfDoctorsAppointmentId");
            CreateIndex("dbo.DiaryVisitsMODHCs", "MedicalDoctorId");
            CreateIndex("dbo.DiaryVisitsMODHCs", "MapOfDentalHealthStudentId");
            CreateIndex("dbo.DentalStatusMODHS", "MapOfDentalHealthStudentId");
            CreateIndex("dbo.MapOfDentalHealthStudents", "InstitutionEducationId");
            CreateIndex("dbo.MapOfDentalHealthStudents", "HealthOrganizationId");
            CreateIndex("dbo.MapOfDentalHealthStudents", "UserId");
            CreateIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MedicalDoctorId");
            CreateIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MapOfDentalHealthStudentId");
            AddForeignKey("dbo.TheOverallTreatmentPlanStudents", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents", "Id");
            AddForeignKey("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId", "dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "Id");
            AddForeignKey("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "MedicalDoctorId", "dbo.MedicalDoctors", "Id");
            AddForeignKey("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", "MedicalDoctorId", "dbo.MedicalDoctors", "Id");
            AddForeignKey("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", "PassportDentalHealthOfPupilsOfEducationalInstitutionsId", "dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", "Id");
            AddForeignKey("dbo.LeafDailyAccountingWorkOfADentists", "MedicalDoctorId", "dbo.MedicalDoctors", "Id");
            AddForeignKey("dbo.ElementLeafDailyAccountingWorkOfADentists", "LeafDailyAccountingWorkOfADentistId", "dbo.LeafDailyAccountingWorkOfADentists", "Id");
            AddForeignKey("dbo.DiaryVisitsMODHCs", "MedicalDoctorId", "dbo.MedicalDoctors", "Id");
            AddForeignKey("dbo.DiaryVisitsMODHCs", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents", "Id");
            AddForeignKey("dbo.DiaryVisitsMODHCs", "AListOfDoctorsAppointmentId", "dbo.AListOfDoctorsAppointments", "Id");
            AddForeignKey("dbo.DentalStatusMODHS", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents", "Id");
            AddForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MedicalDoctorId", "dbo.MedicalDoctors", "Id");
            AddForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents", "Id");
            AddForeignKey("dbo.MapOfDentalHealthStudents", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.MapOfDentalHealthStudents", "InstitutionEducationId", "dbo.InstitutionEducations", "Id");
            AddForeignKey("dbo.MapOfDentalHealthStudents", "HealthOrganizationId", "dbo.HealthOrganizations", "Id");
        }
    }
}
