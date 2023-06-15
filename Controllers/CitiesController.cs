//using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
namespace CityInfo.API.Controllers
{
    [ApiController]//configures the ocntroller with dev experience 
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]  //passing in as the routing template 
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
        
        }

        //[HttpGet("{id")] //attatched to "api/cities" just adding id 
        //public JsonResult GetCity(int id)
        //{ 
          //  return new JsonResult(
            //    CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        //}
    }
}
