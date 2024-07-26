using System.ComponentModel.DataAnnotations;

namespace DemoProjectAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
