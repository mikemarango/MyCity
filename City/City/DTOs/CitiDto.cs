using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.DTOs
{
    public class CitiDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttractionDto> Attractions { get; set; } = new List<AttractionDto>();
    }
}
