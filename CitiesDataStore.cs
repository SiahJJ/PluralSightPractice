using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore(); //"current" is a static property

        public CitiesDataStore()
        {
            // init dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Ney York City",
                    Description = "The one witht that big park."
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "Cathedrial was nerver finised."
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "One with big tower."
                }

            }; 
        }
    }
}
