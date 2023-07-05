//cityId
//PointsOfInterest
//IdofPointofInterest to update 
using System.ComponentModel.DataAnnotations;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;//creating an api controler (Supposed to use a template)

namespace CityInfo.API.Controllers
{
    //when an action has city id in parameters it refers from here {cityid}
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]
    //^ model binding, using apicontroller instead of frombody 
    public class PointsOfInterestController : ControllerBase 
    {
        [Required] 
        private int pointofinterestId;
        [Required] 
        private object pointOfInterestFromStore;

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,//switching localmailservice < IMailService 
            CitiesDataStore citiesDataStore)//injecting data store 
        {
            _logger = logger;// ?? throw new ArgumentNullException(nameof(logger));//null check, got rid of "_logger"
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
            //null check ^
        }
        //returning 500 internal server error^ should return 404 not found with _logger.loginformation
    
        [HttpGet]//can be routed to when using get 
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {

            try
            {
                //throw new Exception("Execption Sample."); //demo 

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
                //^used for errors when past in city id doesnt exist, not mapping to an actul resource 
                if (city == null)
                {
                    //logging the info for HttpGet 
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);//resulting in 200 ok status code
            }
            catch (Exception ex)//loggging catch 
            {
                _logger.LogCritical(
                    $"Execption while getting points of interest for city with id {cityId}.",
                    ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
          
        }
        //Get Request 
        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]//extra check for one specific point of interest - 
        public ActionResult<PointOfInterestDto> GetPointOfInterest(
            [FromRoute] int cityId, [FromRoute] int pointOfInterstId)//each int is a parameter 
        {
            var city = _citiesDataStore.Cities
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
        //creating point of interest -
        [HttpPost]//post request returns the created resruce in the response 
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(//new action, "CreatePointOfInterest"
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)//[FromBody] not needed, content assumed from request body 
        {

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)//if user requests form non existent city 
            {
                return NotFound();//returning notfound if the city does not exist 
            }

            //demo purposes - to be improved later (calculating the id of the new point of interest)
            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                            c => c.PointsOfInterest).Max(p => p.Id);//adding one to it

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description//des
            };             

            city.PointsOfInterest.Add(finalPointOfInterest);//final point of interest attached to cities collection 

            return CreatedAtRoute("GetPointOfInterest",//adding point of interest, name variable 
                new 
                {
                    cityId = cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest); //newly created point of interest, located in response body 
                
        }

        [HttpPut("{pointofinterestid}")]//new action, updatepointofinterest, httpput allows for full updates 
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);//checking to see if we can find the city 
            if (city == null)
            {
                return NotFound();
            }

            //find point of interest
            var pointOfInterestGromStore = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestGromStore == null)
            {
                return NotFound();//if not found 
            }

            //full update principle here
            pointOfInterestGromStore.Name = pointOfInterest.Name;//if consumer does not provide value, set to default 
            pointOfInterestGromStore.Description = pointOfInterest.Description;
            // pointOfInterestFromStore.Name = pointOfInterest.Name;//if consumer does not provide value, set to default 
            // pointOfInterestFromStore.Description = pointOfInterest.Description;
            //this updates all fields ^
            //*description is not returning as null in postman*
            return NoContent();//returng 204 no content 
        }

        //partually updateing a resource - 
        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfInterest(
            int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)//"JsonPatchDocument" = using microsfot.apsnecore.jsonpatch 
        {
            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //patch document - *works on pointofinterestforupdatedto*
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //transforming point of interest, from  *pointOfInterestFromStore* to *pointofinterestforupdatedto* 
            var pointOfInterestToPatch = 
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);//applying patch document 

            if (!ModelState.IsValid)//manually done, chekcking 
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
            //should return 204 no content (first patch point of interest)
        }

        [HttpDelete("{DelpointOfInterestId}")]//name = DelpointOfInterestId
        public ActionResult DeletePointOfInterest(int cityId, int DelpointOfInterestId)//int = parameters cityid & pointofinterestid 
        {
             var city = _citiesDataStore.Cities//checking for city 
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest//chekcing for point of interest 
                .FirstOrDefault(c => c.Id == DelpointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //Removing the point of interest 
            city.PointsOfInterest.Remove(pointOfInterestFromStore);
            //mail service deletion
            _mailService.Send(
                "Pointof interest deleted.",//subject for mail 
                    $"Point of interested {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore} was deleted.");//message for the body 
            return NoContent(); 

        }

    }
}