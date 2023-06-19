using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;//creating an api controler (Supposed to use a template)

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]//when an action has city id in parameters it refers from here {cityid}
    [ApiController]
    public class PointsOfInterestController : ControllerBase 
    {
        private int pointofinterestId;

        [HttpGet]//can be routed to when using get 
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
                //^used for errors when past in city id doesnt exist, not mapping to an actul resource 
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);//resulting in 200 ok status code
        }
        [HttpGet("{pointofinterestid}")]//extra check for one specific point of interest - 
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            int cityId, int pointOfInterstId)//each int is a parameter 
        {
            var city = CitiesDataStore.Current.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //find point of interest
            var pointofinterest = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointofinterestId);
            if (pointofinterest == null)
            {
                return NotFound();
            }

            return Ok(pointofinterest);
        }

        [HttpPost]//post request returns the created resruce in the response 
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(//new action, "CreatePointOfInterest"
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
            {
                if 
            }
    }
}