using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class FileProcessingController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;
       
        public FileProcessingController (IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile formFile,[FromHeader] string FolderRoute,[FromHeader] string FileName,[FromHeader] string extention)
        {
             
            try
            {
                string FolderPath = GetFolderPath(FolderRoute);

                if (!System.IO.File.Exists(FolderPath))
                {
                    System.IO.Directory.CreateDirectory(FolderPath);
                }

                string FilePath = GetFilePath(FolderRoute, FileName) + "." + extention;

                if(System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }

                using (FileStream stream= System.IO.File.Create(FilePath))
                {
                    await formFile.CopyToAsync(stream);
                    return Ok(formFile); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile([FromHeader] string FolderRoute, [FromHeader] string FileName, [FromHeader] string extention)
        {
            string FileUrl = string.Empty;
            string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string FilePath = GetFilePath(FolderRoute, FileName) + "." + extention;
                if (System.IO.File.Exists(FilePath))
                {
                    FileUrl = hostUrl + "/" + FolderRoute + "/" + FileName + "." + extention;
                    return Ok(FileUrl);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [NonAction]
        public string GetFolderPath([FromHeader] string FolderRoute)
        {
            return this.environment.WebRootPath + "\\" + FolderRoute;
        }
        [NonAction]
        public string GetFilePath([FromHeader] string FolderRoute, [FromHeader] string FileName)
        {
            return this.environment.WebRootPath + "\\" + FolderRoute + "\\" + FileName;
        }
    }
}
