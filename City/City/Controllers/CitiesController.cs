using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City.Data;
using City.Models;
using City.DTOs;
using City.Repositories;
using AutoMapper;

namespace City.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public ICityRepository Repository { get; }
        public IMapper Mapper { get; }

        public CitiesController(ICityRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitiNoAttractionDto>>> GetCities()
        {
            var cities = await Repository.GetCitiesAsync();
            //var citiesNoAttractionDto = Mapper.Map<IEnumerable<CitiNoAttractionDto>>(cities);
            var citiesNoAttractionDto = Mapper.Map<IEnumerable<CitiNoAttractionDto>>(cities);
            return citiesNoAttractionDto.ToList();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCiti(int id, bool includeAttractions = false)
        {
            var citi = await Repository.GetCitiAsync(id, includeAttractions);
            if (citi == null)
                return NotFound();
            if (includeAttractions)
            {
                var citiWithAttraction = Mapper.Map<CitiDto>(citi);
                return citiWithAttraction;
            }
            var citiWithoutAttraction = Mapper.Map<CitiNoAttractionDto>(citi);
            return citiWithoutAttraction;
        }
    }
}
