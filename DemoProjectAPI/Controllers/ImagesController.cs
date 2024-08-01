using DemoProjectAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DemoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageUploadDto)
        {
            ValidateFileUpload(imageUploadDto);
            if(ModelState.IsValid)
            {

                return Ok();
            }
            return BadRequest(ModelState);  
        }
        private void ValidateFileUpload(ImageUploadDto request)
        {
            var allowedExtensions = new string[] {".jpg",".jpeg",".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(request.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file format");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Filesize must be less than 10MB");
            }
        }
    }
}
