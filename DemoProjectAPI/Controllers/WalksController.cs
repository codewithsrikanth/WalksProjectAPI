using AutoMapper;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using DemoProjectAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository repository;
        public WalksController(IMapper mapper, IWalksRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walksDomainModel = mapper.Map<Walks>(addWalksRequestDto);
            await repository.CreateAsync(walksDomainModel);
            return Ok(mapper.Map<WalksDto>(walksDomainModel));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await repository.GetAllAsync();
            return Ok(mapper.Map<List<WalksDto>>(walksDomainModel));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walksDomainModel = await repository.GetById(id);
            if (walksDomainModel == null)
                return NotFound();
            return Ok(mapper.Map<WalksDto>(walksDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalksRequestDto updateWalksRequestDto)
        {
            var walkDomainModel = mapper.Map<Walks>(updateWalksRequestDto);
            walkDomainModel = await repository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            { 
                return NotFound(); 
            }
            return Ok(mapper.Map<WalksDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalksDomainModel  = await repository.DeleteAsync(id);
            if (deletedWalksDomainModel == null)
                return NotFound();
            return Ok(mapper.Map<WalksDto>(deletedWalksDomainModel));
        }
    }
}
