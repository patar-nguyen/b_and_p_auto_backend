namespace LearningAPIs.Model
{
    public class UserAccountPatchRequest
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Zipcode { get; set; }
        public string? Country { get; set; }
        public string? DateOfBirth { get; set; }

    }
}
