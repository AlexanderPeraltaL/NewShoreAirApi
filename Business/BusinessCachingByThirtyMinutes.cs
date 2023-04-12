using Model;
using System.Runtime.Caching;

namespace Business
{
    public static class BusinessCachingByThirtyMinutes
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;
        const string cacheKey = "flights";
        public static List<Flight>? Flights()
        {
            try
            {
                const string cacheKey = "flights";
                List<Flight>? cachedFlights = Cache.Get(cacheKey) as List<Flight>;
                if (cachedFlights != null)
                {
                    return cachedFlights;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool SaveFlightsinCached(List<Flight> flights)
        {
            var cacheItem = new CacheItem(cacheKey, flights);
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) };
            Cache.Add(cacheItem, policy);
            return true;
        }
    }
}
