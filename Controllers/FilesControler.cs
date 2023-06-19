using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;//"FileExtensionContentTypeProvider" microsoft code static.files = parameter
            //^stored private, readonly 
        public FilesController(
            FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentException(
                    nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]//get Id, passing through fileId in the template 
        public ActionResult GetFile(string fileId)//one action 
        {
            //look up the actual file, depending on the fileId...
            //demo code
            var pathToFile = "Intern-GoalSetting-P.pdf";

            //check wether the file exists
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(//finding correct content type 
                pathToFile, out var contentType))//output parameter, contentType 
            {
                contentType = "application/octet-stream";//cannot be determined 
            }

            //if file found
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));//pass through the content type & path to get the file name
        }
    }
}