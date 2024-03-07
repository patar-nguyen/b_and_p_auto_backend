using Newtonsoft.Json;

namespace LearningAPIs.Model
{
    public class UserAccountResponse
    {
        [JsonProperty("username", Required = Required.Always)]
        public required string UserName { get; set; }

        [JsonProperty("email", Required = Required.Always)]
        public required string Email { get; set; }

        [JsonProperty("firstname", Required = Required.Always)]
        public required string FirstName { get; set; }

        [JsonProperty("lastname", Required = Required.Always)]
        public required string LastName { get; set; }

        public required DateTime DateOfBirth { get; set; }

        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
     

    }
}
