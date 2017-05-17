
namespace WebApplication.Models
{
    public class User
    {
        public int? Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string NumberAndSeriesOfPassport { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string TheContactPhoneNumber { get; set; }
        public string SocialStatus { get; set; }
        public string PlaceOfWork { get; set; }
        
    }
}