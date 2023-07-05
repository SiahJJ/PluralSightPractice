//using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;//had to be added for line 21/public ActionResult<CityDto> GetCity(int id)

namespace CityInfo.API.Controllers
{
    [ApiController]//configures the controller with dev experience 
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
       // private readonly _citiesDataStore Cities;
       // private readonly  Cities;

       //Constructor 
        public CitiesController(CitiesDataStore citiesDataStore)
        {
            CitiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        private object? cityToReturn;

        public CitiesDataStore CitiesDataStore { get; }

        [HttpGet]  //passing in as the routing template 
        // public JsonResult GetCities()
        // {
        //     return new JsonResult(_citiesDataStore.Cities);//replacing "CitiesDataStore.Current" with _citiesDataStore 

        // }
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesDataStore.Cities);//replacing "CitiesDataStore.Current" with _citiesDataStore 
        }

        [HttpGet("{id}")] //attatched to "api/cities" just adding id 
        public ActionResult<CityDto> GetCity(int id)
        { 
            //finding the city
            var cityToReturn = _citiesDataStore.Cities
                .FirstOrDefault(c => c.Id ==id);

            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);//returing with a 200 Ok 
        }
    }
}
