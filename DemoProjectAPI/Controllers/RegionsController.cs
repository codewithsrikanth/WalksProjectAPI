using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectAPI.Controllers
{
    //https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDbContext _context;
        public RegionsController(WalksDbContext dbContext)
        {
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Get the data from Database - Domain Models
            var regions = _context.Regions.ToList();

            //Map the Domain Models to DTO
            var regionsDto = new List<RegionDto>();
            foreach (var item in regions)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    RegionImageUrl = item.RegionImageUrl,
                });
            }

            //return DTO
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            var region = _context.Regions.FirstOrDefault(x => x.Id == id);
            if(region == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(region);
            }
        }

        //Post
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionModel = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain Model and Store the data
            _context.Regions.Add(regionModel);
            _context.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = regionModel.Id, 
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl,
                Code = regionModel.Code
            };

            return CreatedAtAction(nameof(GetById),new { id= regionDto.Id },regionDto);
        }
    }
}
