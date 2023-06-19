using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("{fileId}")]//get Id, passing through fileId in the template 
        public ActionResult GetFile(string fileId)//one action 
        {
            //look up the actual file, depending on the fileId...
            //demo code
            var pathToFile = "Intern Goal Setting - 2023.docx";

            //check wether the file exists
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }
            //if file found
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, "text/plain", Path.GetFileName(pathToFile));//pass through the content type & path to get the file name
        }
    }
}