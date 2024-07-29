using AutoMapper;
using DemoProjectAPI.CustomFilters;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using DemoProjectAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectAPI.Controllers
{
    //https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionsController(IRegionRepository _regionRepository, IMapper mapper)
        {
            this._regionRepository = _regionRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get the data from Database - Domain Models
            var regions = await _regionRepository.GetAllAsync();

            //Map the Domain Models to DTO
            //var regionsDto = new List<RegionDto>();
            //foreach (var item in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Code = item.Code,
            //        RegionImageUrl = item.RegionImageUrl,
            //    });
            //}

            var regionsDto = _mapper.Map<List<RegionDto>>(regions);

            //return DTO
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            //var regionDomain =await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //else
            //{
            //    RegionDto regionDto = new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageUrl = regionDomain.RegionImageUrl,
            //    };

            //    return Ok(regionDto);
            //}
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }


        [HttpPost]
        [ValidationState]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionModel = _mapper.Map<Region>(addRegionRequestDto);
            regionModel = await _regionRepository.CreateAsync(regionModel);
            var regionDto = _mapper.Map<RegionDto>(regionModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ValidationState]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
                regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                    return NotFound();
                var regionDto = _mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomain =await _context.Regions.FindAsync(id);
            var regionDomain = await _regionRepository.DeleteAsync(id);
            if (regionDomain == null)
                return NotFound();
            //_context.Regions.Remove(regionDomain);
            //await _context.SaveChangesAsync();

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //    Code = regionDomain.Code
            //};

            var regionDto = _mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }
    }
}
