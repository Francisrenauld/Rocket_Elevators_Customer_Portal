namespace Rocket_Elevator_Customer_Portal.Models
{
    public class InterventionModel
    {
        public string Author { get; set; }
        public long Customer_id { get; set; }
        public long Building_id { get; set; }
        public long Battery_id { get; set; }
        public long Column_id { get; set; }
        public long Elevator_id { get; set; }
        public long Employee_id { get; set; }
        public string Report { get; set; }

    }
}