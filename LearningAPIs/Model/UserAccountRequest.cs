using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LearningAPIs.Model
{    
    public class UserAccountRequest
    {
        [JsonProperty("username", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string UserName { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string Password { get; set; }

        [JsonProperty("email", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string Email { get; set; }

        [JsonProperty("firstname", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string FirstName { get; set; }

        [JsonProperty("lastname", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string LastName { get; set; }

        [JsonProperty("dateofbirth", Required = Required.Always)]
        [Required(AllowEmptyStrings = false)]
        public required string DateOfBirth { get; set; }

        public string Street { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Zipcode {  get; set; }
        
        public string City { get; set; }


    }
}
