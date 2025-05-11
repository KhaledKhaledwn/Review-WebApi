using Microsoft.AspNetCore.Mvc;

namespace ReviewApiApp.Controllers
{
    [ApiController]
    [Route("api/file/{fileId}")]
    public class FilesController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFile(int fileId)
        {
            var path = "TextFile.txt";
            //if (!System.IO.File.Exists(path))
            //    return NotFound();
           var myfile = System.IO.File.ReadAllBytes(path);
            return File(myfile, "text/plain", Path.GetFileName(path));
        }
        

    }
}
