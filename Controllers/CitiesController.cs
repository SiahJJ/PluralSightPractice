//using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;//had to be added for line 21/public ActionResult<CityDto> GetCity(int id)

namespace CityInfo.API.Controllers
{
    [ApiController]//configures the controller with dev experience 
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private object? cityToReturn;

        [HttpGet]  //passing in as the routing template 
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);

        }
        //Below here is where the problem results - 
        [HttpGet("{id}")] //attatched to "api/cities" just adding id 
        public ActionResult<CityDto> GetCity(int id)
        { 
            //finding the city
            var cityToReturn = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id ==id);

            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);//returing with a 200 Ok 
        }
    }
}
