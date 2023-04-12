using Data;
using Model;

namespace Business
{
    public class BusinessCheckAvaliableFlights
    {
        private Journey journey;
        private Journey journeyResultSql;
        private List<Journey> flightResult;

        public BusinessCheckAvaliableFlights(Journey journey)
        {
            this.journey = journey;
            flightResult = new List<Journey>();
            journeyResultSql = new Journey();
        }

        public Journey Process()
        {

            JourneyGetByOriginAndDestination journeyGet = new JourneyGetByOriginAndDestination(journey);
            journeyResultSql = journeyGet.Process();
            if (journeyResultSql.Flights.Count == 0)
            {
                List<Flight> flight = new List<Flight>();

                BusinessAvaliableFlightsAll businessAvaliableFlightsAll = new BusinessAvaliableFlightsAll();
                flight = businessAvaliableFlightsAll.Process();

                if (flight.Count(x => x.DepartureStation == journey.Origin && x.ArrivalStation == journey.Destination) == 0)
                {
                    GetRoutes(flight, journey);
                }
                else
                {
                    flight = flight.Where(x => x.DepartureStation == journey.Origin && x.ArrivalStation == journey.Destination).ToList();
                    foreach (Flight flights in flight)
                    {
                        flightResult.Add(new Journey
                        {
                            Flights = flight,
                            Origin = flights.DepartureStation,
                            Destination = flights.ArrivalStation,
                            Price = flights.Price
                        });
                    }
                }

                BusinessJourneyFlightsCreate businessJourneyFlightsCreate = new BusinessJourneyFlightsCreate(flightResult);
                businessJourneyFlightsCreate.Process();

                JourneyGetByOriginAndDestination journeyGetTwo = new JourneyGetByOriginAndDestination(journey);
                journeyResultSql = journeyGet.Process();
            }

            return journeyResultSql;
        }
        public void GetRoutes(List<Flight> flights, Journey journey)
        {
            List<Flight> flightFilterByOrigin = new List<Flight>();
            flightFilterByOrigin = flights.Where(x => x.DepartureStation == journey.Origin).ToList();


            List<Flight> flightFilterByDestination = new List<Flight>();
            flightFilterByDestination = flights.Where(x => x.ArrivalStation == journey.Destination).ToList();


            foreach (Flight flightOrigin in flightFilterByOrigin)
            {
                foreach (Flight flightDestination in flightFilterByDestination)
                {
                    if (flightOrigin.ArrivalStation == flightDestination.DepartureStation && flightDestination.ArrivalStation == journey.Destination)
                    {
                        flightResult.Add(new Journey
                        {
                            Flights = new List<Flight> { flightOrigin, flightDestination },
                            Origin = flightOrigin.DepartureStation,
                            Destination = flightDestination.ArrivalStation,
                            Price = flightOrigin.Price + flightDestination.Price
                        });
                    }
                }
            }
        }

    }
}
