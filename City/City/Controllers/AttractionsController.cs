using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using City.DTOs;
using City.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace City.Controllers
{
    [Route("api/cities/{citiId}/[controller]")]
    [ApiController]
    public class AttractionsController : ControllerBase
    {
        public ICityRepository Repository { get; }
        public IMapper Mapper { get; }

        public AttractionsController(ICityRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }
        // GET: api/Attractions
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<AttractionDto>>> GetAttractionsAsync(int citiId)
        {
            var attractions = await Repository.GetAttractionsAsync(citiId);
            var attractionsDto = Mapper.Map<IEnumerable<AttractionDto>>(attractions)
                .ToList();
            return attractionsDto;
        }

        // GET: api/Attractions/5
        [HttpGet("{id}", Name = "GetAttraction")]
        public async Task<ActionResult<AttractionDto>> GetAttractionAsync(int citiId, int id)
        {
            var attraction = await Repository.GetAttractionAsync(citiId, id);

            if (attraction == null)
                return NotFound();

            var attractionDto = Mapper.Map<AttractionDto>(attraction);

            return attractionDto;
        }

        // POST: api/Attractions
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Attractions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
