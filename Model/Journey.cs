namespace Model
{
    public class Journey
    {
        public int Id { get; set; } 
        public List<Flight> Flights { get; set; } = new List<Flight>();
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double Price { get; set; }
    }
}
