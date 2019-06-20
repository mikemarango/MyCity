using System.Collections.Generic;
using System.Threading.Tasks;
using City.Models;

namespace City.Repositories
{
    public interface ICityRepository
    {
        Task CreateAttractionAsync(int citiId, Attraction attraction);
        Task DeleteAttractionAsync(Attraction attraction);
        Task<Attraction> GetAttractionAsync(int citiId, int id);
        Task<IEnumerable<Attraction>> GetAttractionsAsync(int citiId);
        Task<Citi> GetCitiAsync(int citiId, bool includeAttractions);
        Task<IEnumerable<Citi>> GetCitiesAsync();
    }
}