namespace LearningAPIs.Model
{
    public class UserCar
    {
        public int CarId { get; set; }
        public Guid UserId { get; set; }
        public string Make { get; set; }
        public string Model {  get; set; }
        public string Year { get; set; }
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        public int Mileage { get; set; }
    }
}
