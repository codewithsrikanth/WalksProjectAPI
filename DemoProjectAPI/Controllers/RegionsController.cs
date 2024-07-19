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
            var regionDomain = _context.Regions.FirstOrDefault(x => x.Id == id);
            if(regionDomain == null)
            {
                return NotFound();
            }
            else
            {
                RegionDto regionDto = new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                };

                return Ok(regionDto);
            }
        }

        //Insert
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

        //Update
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute]Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel =  _context.Regions.Find(id);
            if (regionDomainModel == null)
                return NotFound();
            
            //Map the DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            regionDomainModel.Name = updateRegionRequestDto.Name;

            _context.Regions.Update(regionDomainModel);
            _context.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
                Code = regionDomainModel.Code
            };
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute]Guid id)
        {
            var regionDomain = _context.Regions.Find(id);
            if(regionDomain == null)
                return NotFound();
            _context.Regions.Remove(regionDomain);
            _context.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id=regionDomain.Id,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
                Code = regionDomain.Code
            };
            return Ok(regionDto);
        }
    }
}
