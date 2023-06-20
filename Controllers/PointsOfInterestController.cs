//cityId
//PointsOfInterest
//IdofPointofInterest to update 
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;//creating an api controler (Supposed to use a template)

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]//when an action has city id in parameters it refers from here {cityid}
    [ApiController]//this attribute automatically returns 400 request 
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
        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]//extra check for one specific point of interest - 
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
            //if (!ModelState.IsValid)//modelstat is a essentally a dictionary 
            //{
              //  return BadRequest();
            //}
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();//returning notfound if the city does not exist 
            }

            //demo purposes - to be improved later (calculating the id of the new point of interest)
            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
                            c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Descriptin = pointOfInterest.Description
            };             

            city.PointsOfInterest.Add(finalPointOfInterest);//final point of interest attached to cities collection 

            return CreatedAtRoute("GetPointOfInterest",
                new 
                {
                    cityId = cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest);   
        }

        [HttpPut("{pointofinterestid}")]//new action, updatepointofinterest, httpput allows for full updates 
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //find point of interest
            var pointOfInterestGromStore = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestGromStore == null)
            {
                return NotFound();
            }

            //full update principle here
            pointOfInterestGromStore.Name = pointOfInterest.Name;//if consumer does not provide value, set to default 
            pointOfInterestGromStore.Descriptin = pointOfInterest.Description;
            //this updates all fields ^

            return NoContent();
        }
    }
}