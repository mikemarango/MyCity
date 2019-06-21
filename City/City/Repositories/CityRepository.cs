using City.Data;
using City.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Repositories
{
    public class CityRepository : ICityRepository
    {
        public CityRepository(CityContext context)
        {
            Context = context;
        }

        public CityContext Context { get; }

        public async Task<IEnumerable<Citi>> GetCitiesAsync()
        {
            return await Context.Cities.OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Citi> GetCitiAsync(int citiId, bool includeAttractions)
        {
            if (includeAttractions)
            {
                return await Context.Cities.Include(c => c.Attractions)
                    .Where(c => c.ID == citiId).FirstOrDefaultAsync();
            }

            return await Context.Cities
                .SingleOrDefaultAsync(c => c.ID == citiId);
        }

        public async Task<IEnumerable<Attraction>> GetAttractionsAsync(int citiId)
        {
            return await Context.Attractions.Where(a => a.CitiID == citiId)
                .ToListAsync();
        }

        public async Task<Attraction> GetAttractionAsync(int citiId, int id)
        {
            var attractions = await GetAttractionsAsync(citiId);
            var attraction = attractions.FirstOrDefault(a => a.ID == id);
            return attraction;
        }

        public async Task CreateAttractionAsync(int citiId, Attraction attraction)
        {
            var city = await GetCitiAsync(citiId, false);
            city.Attractions.Add(attraction);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAttractionAsync(Attraction attraction)
        {
            Context.Attractions.Remove(attraction);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> CitiExistsAsync(int citiId)
        {
            return await Context.Cities.AnyAsync(c => c.ID == citiId);
        }
    }
}
