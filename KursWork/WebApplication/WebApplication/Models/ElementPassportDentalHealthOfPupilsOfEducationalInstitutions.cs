using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ElementPassportDentalHealthOfPupilsOfEducationalInstitutions
    {
        public int ? Id { get; set; }
        public int? PassportDentalHealthOfPupilsOfEducationalInstitutionsId { get; set; }
        public PassportDentalHealthOfPupilsOfEducationalInstitutions
            PassportDentalHealthOfPupilsOfEducationalInstitutions{ get; set; }
        public int NumberOfStudentsTotalPersons { get; set; }
        public int ViewedByTotalPersons { get; set; }
        public int Healthy { get; set; }
        public int PreviouslySanitized { get; set; }
        public int NeededRehabilitation { get; set; }
        public int Improved { get; set; }
        public float AHealthyPercentageOfTheNumberExamined { get; set; }
        public int TheIntensityOfСaries { get; set; }
        public int TheActivityOfCariesLow { get; set; }
        public int TheActivityOfCariesMedium { get; set; }
        public int TheActivityOfCariesHigh { get; set; }
        public int TheActivityOfCariesVeryHigh { get; set; }
        public int IndexOfHygieneOHI_S { get; set; }
        public int ComprehensivePeriodontalIndex { get; set; }
        public int TakenPreventiveMeasuresHealthEducation { get; set; }
        public int TakenPreventiveMeasuresFissureSealing { get; set; }
        public int CariesPermanentTeeth { get; set; }
        public int CariesTemporaryTeeth { get; set; }
        public int SealedPermanentTeeth { get; set; }
        public int SealedTemporaryTeeth { get; set; }
        public int ComplicatedCariesPermanentTeeth { get; set; }
        public int ComplicatedCariesTemporaryTeeth { get; set; }
        public int RefusedTreatmentInspection { get; set; }
        public int DirectedToTheRemovalOfTeeth { get; set; }
        public int NeedOrthodonticTreatment { get; set; }
        public int ConductedHealthLessons { get; set; }
        public int TrainingTeacher { get; set; }
    }
    
}