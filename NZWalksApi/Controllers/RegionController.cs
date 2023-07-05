using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.DTO;
using NZWalksApi.Models.Domains;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        // private readonly is used inside action methods
        private readonly NZWalksDbContext dbContext;

        // Controller for using DbContext to access the database
        public RegionController(NZWalksDbContext DbContext)
        {
            this.dbContext = DbContext;
        }
        // https://localhost:7135/api/Region this is the end point of the below method 
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            // by using dbContext we are talk to db and get the data and assign to a variable
            // getting the data from database - domain model
            var regionsDomain = dbContext.regions.ToList();
            // map Domain model to DTO

            var regionDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }

            //return DTO
            return Ok(regionDto);
        }

        // get a specific data by using the id 
        // Get : http:// localhost/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute]Guid id)
        {
            // getting data through only id we can use var region  = dbContext.regions.Find(id)
            var region = dbContext.regions.Find(id);
            // if we find a data by name are code or not unified dat we need to use below statement
            // var region = dbContext.regions.FirstOrDefault(x => x.Id == id); 

            if(region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionsDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };


            // Use Domain Model to create Region
            dbContext.regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionRequestDto)
        {
            // Check if region exists
            var regionDomainModel = dbContext.regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            // Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // Delete Http Req
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.regions.FirstOrDefault(x=>x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Remove(regionDomainModel);
            dbContext.SaveChanges();

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
