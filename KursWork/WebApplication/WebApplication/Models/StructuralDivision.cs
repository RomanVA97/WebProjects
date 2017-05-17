namespace WebApplication.Models
{
    public class StructuralDivision
    {
        public int? Id { get; set; }
        public int? HealthOrganizationId { get; set; }
        public HealthOrganization HealthOrganization { get; set; }
        public string NameOfStructuralDivision { get; set; }
        public string Address { get; set; }
    }
}