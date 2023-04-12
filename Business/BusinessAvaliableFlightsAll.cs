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
            /// <summary>
            ///Verificamos si el response del API externo, ya se encuentra guardado en el caching de la App
            ///Este la guarda por 30min, de forma que se optimizan las solicitudes a la API
            ///En caso de no estar en el caching, hace la consulta y procede a guardarlas.
            /// <summary>

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
