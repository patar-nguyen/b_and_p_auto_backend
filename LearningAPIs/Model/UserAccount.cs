using Newtonsoft.Json;

namespace LearningAPIs.Model
{
    public class UserAccount
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EnrollDate { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }

        public string City { get; set; }
    }
}
