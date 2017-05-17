namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AListOfDoctorsAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        MedicalDoctorId = c.Int(),
                        Appointments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MedicalDoctorId);
            
            CreateTable(
                "dbo.MedicalDoctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        StructuralDivisionId = c.Int(),
                        MedicalDirectionId = c.Int(),
                        MedicalProfileId = c.Int(),
                        MedicalQualificationId = c.Int(),
                        DoctorQualificationId = c.Int(),
                        Office = c.Int(nullable: false),
                        HoursOnEvenNumbers = c.DateTime(nullable: false),
                        HoursOnOddNumbers = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorQualifications", t => t.DoctorQualificationId)
                .ForeignKey("dbo.MedicalDirections", t => t.MedicalDirectionId)
                .ForeignKey("dbo.MedicalProfiles", t => t.MedicalProfileId)
                .ForeignKey("dbo.MedicalQualifications", t => t.MedicalQualificationId)
                .ForeignKey("dbo.StructuralDivisions", t => t.StructuralDivisionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.StructuralDivisionId)
                .Index(t => t.MedicalDirectionId)
                .Index(t => t.MedicalProfileId)
                .Index(t => t.MedicalQualificationId)
                .Index(t => t.DoctorQualificationId);
            
            CreateTable(
                "dbo.DoctorQualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qualification = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicalDirections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfMedicalDirection = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicalProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfMedicalProfile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicalQualifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfMedicalQualification = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StructuralDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HealthOrganizationId = c.Int(),
                        NameOfStructuralDivision = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthOrganizations", t => t.HealthOrganizationId)
                .Index(t => t.HealthOrganizationId);
            
            CreateTable(
                "dbo.HealthOrganizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfHealthOrganization = c.String(),
                        CityId = c.Int(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegionId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurName = c.String(),
                        Name = c.String(),
                        MiddleName = c.String(),
                        NumberAndSeriesOfPassport = c.String(),
                        DateOfBirth = c.String(),
                        Gender = c.String(),
                        CityId = c.Int(),
                        Address = c.String(),
                        TheContactPhoneNumber = c.String(),
                        SocialStatus = c.String(),
                        PlaceOfWork = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapOfDentalHealthStudents", t => t.MapOfDentalHealthStudentId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.MapOfDentalHealthStudentId)
                .Index(t => t.MedicalDoctorId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthOrganizations", t => t.HealthOrganizationId)
                .ForeignKey("dbo.InstitutionEducations", t => t.InstitutionEducationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HealthOrganizationId)
                .Index(t => t.InstitutionEducationId);
            
            CreateTable(
                "dbo.InstitutionEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfInstitutionEducation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DentalPatientCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HealthOrganizationId = c.Int(),
                        Data = c.DateTime(nullable: false),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthOrganizations", t => t.HealthOrganizationId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.HealthOrganizationId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DentalStatusDPCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DentalPatientCardId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        OHI_S = c.String(),
                        KPI = c.String(),
                        Bite = c.String(),
                        TheStatusOfHardTissueOfTeethPeriodontal = c.String(),
                        TheConditionOfTheOralMucosa = c.String(),
                        DataX_rayAndOtherStudies = c.String(),
                        ThePreliminaryDiagnosis = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DentalPatientCards", t => t.DentalPatientCardId)
                .Index(t => t.DentalPatientCardId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapOfDentalHealthStudents", t => t.MapOfDentalHealthStudentId)
                .Index(t => t.MapOfDentalHealthStudentId);
            
            CreateTable(
                "dbo.DiaryVisitsDPCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DentalPatientCardId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                        Entry = c.String(),
                        AListOfDoctorsAppointmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AListOfDoctorsAppointments", t => t.AListOfDoctorsAppointmentId)
                .ForeignKey("dbo.DentalPatientCards", t => t.DentalPatientCardId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.DentalPatientCardId)
                .Index(t => t.MedicalDoctorId)
                .Index(t => t.AListOfDoctorsAppointmentId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AListOfDoctorsAppointments", t => t.AListOfDoctorsAppointmentId)
                .ForeignKey("dbo.MapOfDentalHealthStudents", t => t.MapOfDentalHealthStudentId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.MapOfDentalHealthStudentId)
                .Index(t => t.MedicalDoctorId)
                .Index(t => t.AListOfDoctorsAppointmentId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeafDailyAccountingWorkOfADentists", t => t.LeafDailyAccountingWorkOfADentistId)
                .Index(t => t.LeafDailyAccountingWorkOfADentistId);
            
            CreateTable(
                "dbo.LeafDailyAccountingWorkOfADentists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.MedicalDoctorId);
            
            CreateTable(
                "dbo.ExaminationOfThePatientDuringInitialTreatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DentalPatientCardId = c.Int(),
                        MedicalDoctorId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        DiseaseOfTheCardiovascularSystem = c.String(),
                        DiseaseOfTheNervousSystem = c.String(),
                        DiseaseOfTheEndocrineSystem = c.String(),
                        DiseaseOfTheDigestiveSystem = c.String(),
                        DiseaseOfTheRespiratorySystem = c.String(),
                        InfectiousDiseases = c.String(),
                        AllergicReactions = c.String(),
                        TheConstantUseOfDrugs = c.String(),
                        HarmfulFactorsOfProductionEnvironment = c.String(),
                        PregnancyPostpartumPeriod = c.String(),
                        More = c.String(),
                        ExternalExamination = c.String(),
                        TheConditionOfTheSkin = c.String(),
                        TheStatusOfTheRegionalLymphNodes = c.String(),
                        TheConditionOfTheTemporomandibularJoint = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DentalPatientCards", t => t.DentalPatientCardId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.DentalPatientCardId)
                .Index(t => t.MedicalDoctorId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", t => t.PassportDentalHealthOfPupilsOfEducationalInstitutionsId)
                .Index(t => t.PassportDentalHealthOfPupilsOfEducationalInstitutionsId);
            
            CreateTable(
                "dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Since = c.DateTime(nullable: false),
                        For = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.MedicalDoctorId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", t => t.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId)
                .Index(t => t.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId);
            
            CreateTable(
                "dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalDoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.MedicalDoctorId);
            
            CreateTable(
                "dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DentalPatientCardId = c.Int(),
                        Date = c.DateTime(nullable: false),
                        MedicalDoctorId = c.Int(),
                        EmergencyCare = c.String(),
                        PreventiveMeasures = c.String(),
                        HygieneEducation = c.String(),
                        TherapeuticTreatment = c.String(),
                        SurgicalTreatment = c.String(),
                        OrthopedicTreatment = c.String(),
                        OrthodonticTreatment = c.String(),
                        AdditionalDiagnosticMeasures = c.String(),
                        ConsultOtherSpecialists = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DentalPatientCards", t => t.DentalPatientCardId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.DentalPatientCardId)
                .Index(t => t.MedicalDoctorId);
            
            CreateTable(
                "dbo.RecordOnReceptionToTheDoctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        MedicalDoctorId = c.Int(),
                        DateOfAdmission = c.DateTime(nullable: false),
                        RoomPass = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MedicalDoctorId);
            
            CreateTable(
                "dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DentalPatientCardId = c.Int(),
                        AssignedTypeOfResearch = c.String(),
                        MedicalDoctorId = c.Int(),
                        TheOrganizationWhichConductedTheStudy = c.String(),
                        Date = c.DateTime(nullable: false),
                        EquivalentDose = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DentalPatientCards", t => t.DentalPatientCardId)
                .ForeignKey("dbo.MedicalDoctors", t => t.MedicalDoctorId)
                .Index(t => t.DentalPatientCardId)
                .Index(t => t.MedicalDoctorId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapOfDentalHealthStudents", t => t.MapOfDentalHealthStudentId)
                .Index(t => t.MapOfDentalHealthStudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TheOverallTreatmentPlanStudents", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics", "DentalPatientCardId", "dbo.DentalPatientCards");
            DropForeignKey("dbo.RecordOnReceptionToTheDoctors", "UserId", "dbo.Users");
            DropForeignKey("dbo.RecordOnReceptionToTheDoctors", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments", "DentalPatientCardId", "dbo.DentalPatientCards");
            DropForeignKey("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId", "dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropForeignKey("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", "PassportDentalHealthOfPupilsOfEducationalInstitutionsId", "dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropForeignKey("dbo.ExaminationOfThePatientDuringInitialTreatments", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ExaminationOfThePatientDuringInitialTreatments", "DentalPatientCardId", "dbo.DentalPatientCards");
            DropForeignKey("dbo.LeafDailyAccountingWorkOfADentists", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.ElementLeafDailyAccountingWorkOfADentists", "LeafDailyAccountingWorkOfADentistId", "dbo.LeafDailyAccountingWorkOfADentists");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.DiaryVisitsMODHCs", "AListOfDoctorsAppointmentId", "dbo.AListOfDoctorsAppointments");
            DropForeignKey("dbo.DiaryVisitsDPCs", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.DiaryVisitsDPCs", "DentalPatientCardId", "dbo.DentalPatientCards");
            DropForeignKey("dbo.DiaryVisitsDPCs", "AListOfDoctorsAppointmentId", "dbo.AListOfDoctorsAppointments");
            DropForeignKey("dbo.DentalStatusMODHS", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.DentalStatusDPCs", "DentalPatientCardId", "dbo.DentalPatientCards");
            DropForeignKey("dbo.DentalPatientCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.DentalPatientCards", "HealthOrganizationId", "dbo.HealthOrganizations");
            DropForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", "MapOfDentalHealthStudentId", "dbo.MapOfDentalHealthStudents");
            DropForeignKey("dbo.MapOfDentalHealthStudents", "UserId", "dbo.Users");
            DropForeignKey("dbo.MapOfDentalHealthStudents", "InstitutionEducationId", "dbo.InstitutionEducations");
            DropForeignKey("dbo.MapOfDentalHealthStudents", "HealthOrganizationId", "dbo.HealthOrganizations");
            DropForeignKey("dbo.AListOfDoctorsAppointments", "UserId", "dbo.Users");
            DropForeignKey("dbo.AListOfDoctorsAppointments", "MedicalDoctorId", "dbo.MedicalDoctors");
            DropForeignKey("dbo.MedicalDoctors", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropForeignKey("dbo.MedicalDoctors", "StructuralDivisionId", "dbo.StructuralDivisions");
            DropForeignKey("dbo.StructuralDivisions", "HealthOrganizationId", "dbo.HealthOrganizations");
            DropForeignKey("dbo.HealthOrganizations", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MedicalDoctors", "MedicalQualificationId", "dbo.MedicalQualifications");
            DropForeignKey("dbo.MedicalDoctors", "MedicalProfileId", "dbo.MedicalProfiles");
            DropForeignKey("dbo.MedicalDoctors", "MedicalDirectionId", "dbo.MedicalDirections");
            DropForeignKey("dbo.MedicalDoctors", "DoctorQualificationId", "dbo.DoctorQualifications");
            DropIndex("dbo.TheOverallTreatmentPlanStudents", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics", new[] { "MedicalDoctorId" });
            DropIndex("dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics", new[] { "DentalPatientCardId" });
            DropIndex("dbo.RecordOnReceptionToTheDoctors", new[] { "MedicalDoctorId" });
            DropIndex("dbo.RecordOnReceptionToTheDoctors", new[] { "UserId" });
            DropIndex("dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments", new[] { "MedicalDoctorId" });
            DropIndex("dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments", new[] { "DentalPatientCardId" });
            DropIndex("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments", new[] { "SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatmentId" });
            DropIndex("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions", new[] { "PassportDentalHealthOfPupilsOfEducationalInstitutionsId" });
            DropIndex("dbo.ExaminationOfThePatientDuringInitialTreatments", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ExaminationOfThePatientDuringInitialTreatments", new[] { "DentalPatientCardId" });
            DropIndex("dbo.LeafDailyAccountingWorkOfADentists", new[] { "MedicalDoctorId" });
            DropIndex("dbo.ElementLeafDailyAccountingWorkOfADentists", new[] { "LeafDailyAccountingWorkOfADentistId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "AListOfDoctorsAppointmentId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "MedicalDoctorId" });
            DropIndex("dbo.DiaryVisitsMODHCs", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.DiaryVisitsDPCs", new[] { "AListOfDoctorsAppointmentId" });
            DropIndex("dbo.DiaryVisitsDPCs", new[] { "MedicalDoctorId" });
            DropIndex("dbo.DiaryVisitsDPCs", new[] { "DentalPatientCardId" });
            DropIndex("dbo.DentalStatusMODHS", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.DentalStatusDPCs", new[] { "DentalPatientCardId" });
            DropIndex("dbo.DentalPatientCards", new[] { "UserId" });
            DropIndex("dbo.DentalPatientCards", new[] { "HealthOrganizationId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "InstitutionEducationId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "HealthOrganizationId" });
            DropIndex("dbo.MapOfDentalHealthStudents", new[] { "UserId" });
            DropIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", new[] { "MedicalDoctorId" });
            DropIndex("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations", new[] { "MapOfDentalHealthStudentId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "RegionId" });
            DropIndex("dbo.HealthOrganizations", new[] { "CityId" });
            DropIndex("dbo.StructuralDivisions", new[] { "HealthOrganizationId" });
            DropIndex("dbo.MedicalDoctors", new[] { "DoctorQualificationId" });
            DropIndex("dbo.MedicalDoctors", new[] { "MedicalQualificationId" });
            DropIndex("dbo.MedicalDoctors", new[] { "MedicalProfileId" });
            DropIndex("dbo.MedicalDoctors", new[] { "MedicalDirectionId" });
            DropIndex("dbo.MedicalDoctors", new[] { "StructuralDivisionId" });
            DropIndex("dbo.MedicalDoctors", new[] { "UserId" });
            DropIndex("dbo.AListOfDoctorsAppointments", new[] { "MedicalDoctorId" });
            DropIndex("dbo.AListOfDoctorsAppointments", new[] { "UserId" });
            DropTable("dbo.TheOverallTreatmentPlanStudents");
            DropTable("dbo.TheListOfAppointmentsAndAccountingLoadsOfRadiographics");
            DropTable("dbo.RecordOnReceptionToTheDoctors");
            DropTable("dbo.GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatments");
            DropTable("dbo.SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropTable("dbo.ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatments");
            DropTable("dbo.PassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropTable("dbo.ElementPassportDentalHealthOfPupilsOfEducationalInstitutions");
            DropTable("dbo.ExaminationOfThePatientDuringInitialTreatments");
            DropTable("dbo.LeafDailyAccountingWorkOfADentists");
            DropTable("dbo.ElementLeafDailyAccountingWorkOfADentists");
            DropTable("dbo.DiaryVisitsMODHCs");
            DropTable("dbo.DiaryVisitsDPCs");
            DropTable("dbo.DentalStatusMODHS");
            DropTable("dbo.DentalStatusDPCs");
            DropTable("dbo.DentalPatientCards");
            DropTable("dbo.InstitutionEducations");
            DropTable("dbo.MapOfDentalHealthStudents");
            DropTable("dbo.CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Cities");
            DropTable("dbo.HealthOrganizations");
            DropTable("dbo.StructuralDivisions");
            DropTable("dbo.MedicalQualifications");
            DropTable("dbo.MedicalProfiles");
            DropTable("dbo.MedicalDirections");
            DropTable("dbo.DoctorQualifications");
            DropTable("dbo.MedicalDoctors");
            DropTable("dbo.AListOfDoctorsAppointments");
        }
    }
}
