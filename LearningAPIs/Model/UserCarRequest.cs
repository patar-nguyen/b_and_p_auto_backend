namespace LearningAPIs.Model
{
    public class UserCarRequest
    {
        public required Guid UserId { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public required string Year { get; set; }
        public string? VIN { get; set; }
        public string? LicensePlate { get; set; }
        public int? Mileage { get; set; }
    }
}
