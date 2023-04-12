using Data;
using Model;

namespace Business
{
    public class BusinessAvaliableFlightsAll
    {
        private List<Flight>? flights;
        public BusinessAvaliableFlightsAll()
        {
            flights = new List<Flight>();
        }
        public List<Flight> Process()
        {

            flights = BusinessCachingByThirtyMinutes.Flights();
            if (flights == null)
            {
                flights = CheckAvailableFlights.GetFlights();
                BusinessCachingByThirtyMinutes.SaveFlightsinCached(flights);
            }

            return flights;
        }
    }
}
