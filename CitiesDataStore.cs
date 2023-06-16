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
                new CityDto()//1
                {
                    Id = 1,
                    Name = "Ney York City",
                    Description = "The one witht that big park.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto () {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visisted park in the U.S.", },
                        new PointOfInterestDto () {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper in the middle of NYC Manhatten." },
                    }
                },
                new CityDto()//2
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "Cathedrial was nerver finised.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto () {
                            Id = 3,
                            Name = "Cathedral of OUr Lady",
                            Description = "A Gothic style cathedral...", },
                        new PointOfInterestDto () {
                            Id = 4,
                            Name = "Antwerp Central Station",
                            Description = "THe finest example of railway architecture in Belgium." },
                    }
                },
                new CityDto()//3
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "One with big tower.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto () {
                            Id = 5,
                            Name = "Eiffle Tower",
                            Description = "A wrought iron lattice tower on the Champ de Mars...", },
                        new PointOfInterestDto () {
                            Id = 6,
                            Name = "The Louvre",
                            Description = "Worlds largest meseum." },
                    }
                },

            }; 
        }
    }
}
