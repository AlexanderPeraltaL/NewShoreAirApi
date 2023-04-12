using Model;
using Newtonsoft.Json;

namespace Data
{
    public static class CheckAvailableFlights
    { 
        public static List<Flight>? GetFlights()
        {

            using (var httpClient = new HttpClient())
            {
                string url = "https://recruiting-api.newshore.es/api/flights/2";
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    List<Flight>? flightsJson = JsonConvert.DeserializeObject<List<Flight>>(responseBody);
                    List<Flight> flights = flightsJson.Select(x => new Flight
                    {
                        Transport = new Transport
                        {
                            FlightNumber = x.FlightNumber,
                            FlightCarrier = x.FlightCarrier
                        },
                        DepartureStation = x.DepartureStation,
                        ArrivalStation = x.ArrivalStation,
                        Price = x.Price
                    }).ToList();

                    return flights;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
