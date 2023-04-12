namespace Model
{
    public class Flight
    {
        public int Id { get; set; }
        public string? DepartureStation { get; set; }
        public string? ArrivalStation { get; set; }
        public double Price { get; set; }
        public Transport Transport { get; set; } = new Transport();
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
    }
}
