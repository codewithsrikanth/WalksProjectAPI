using System.ComponentModel.DataAnnotations;

namespace DemoProjectAPI.Models.DTO
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
