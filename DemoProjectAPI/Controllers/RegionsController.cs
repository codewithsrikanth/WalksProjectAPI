using DemoProjectAPI.Data;
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
    }
}
