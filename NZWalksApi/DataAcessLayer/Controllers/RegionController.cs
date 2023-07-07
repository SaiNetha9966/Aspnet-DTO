using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.DataAcessLayer.Data;
using NZWalksApi.DataAcessLayer.Models.Domains;
using NZWalksApi.DataAcessLayer.Repositories;
using NZWalksApi.DTO;

namespace NZWalksApi.DataAcessLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(NZWalksDbContext DbContext, IRegionRepository regionRepository, IMapper mapper)

        {
            dbContext = DbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            var regionsDomain = await regionRepository.GetAllRegionAsync();

            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }



        // get a specific data by using the id 
        // Get : http:// localhost/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }







        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionsDto addRegionRequestDto)
        {

            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }




        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionRequestDto)

        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UpdateRegionDto>(regionDomainModel));
         }








        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }

    }
}
