using Data;
using Model;

namespace Business
{
    public class BusinessJourneyFlightsCreate
    {
        private List<Journey> journeys;
        private Flight flightResult;
        private int journeyId;

        public BusinessJourneyFlightsCreate(List<Journey> journeys)
        {
            this.journeys = journeys;
            flightResult = new Flight();
        }

        public void Process()
        {


            foreach (Journey journey in journeys)
            {
                Journey(journey);
                foreach (Flight flight in journey.Flights)
                {
                    Transport(flight);
                    Flight(flight);
                    JourneyFlights();
                }
            }
        }
        private void Journey(Journey journey)
        {
            JourneyCreate journeyCreate = new JourneyCreate(journey);
            journeyId = journeyCreate.Process();
            
        }
        private void Transport(Flight flight) 
        {
            TransportGetByFlightNumber transportGetById = new TransportGetByFlightNumber(flight.Transport);
            Transport transportResponse = transportGetById.Process();
            if (transportResponse.Id == 0)
            {
                TransportCreate transportCreate = new TransportCreate(flight.Transport);
                flightResult.Transport.Id = transportCreate.Process();
            }
            else
            {
                flightResult.Transport.Id = transportResponse.Id;
            }
        }
        private void Flight(Flight flight)
        {
            flight.Transport = flightResult.Transport;
            FlightGetByDepartureAndArrivalStation flightGetByDepartureAndArrivalStation = new FlightGetByDepartureAndArrivalStation(flight);
            
            Flight flightResponse = flightGetByDepartureAndArrivalStation.Process();
            if (flightResponse.Id == 0)
            {
                FlightCreate flightCreate = new FlightCreate(flight);
                flightResult.Id = flightCreate.Process();
            }
            else
            {
                flightResult.Id = flightResponse.Id;
                flightResult.DepartureStation = flightResponse.DepartureStation;
                flightResult.ArrivalStation = flightResponse.ArrivalStation;
                flightResult.Price = flightResponse.Price;
            }
        }
        private void JourneyFlights()
        {
            JourneyFlightsCreate journeyFlightsCreate = new JourneyFlightsCreate(journeyId, flightResult.Id);
            journeyFlightsCreate.Process();

        }
    }
}
