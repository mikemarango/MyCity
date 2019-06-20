using City.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace City.Data
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Citi> Cities { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Citi>().HasData(CitiData());
            modelBuilder.Entity<Attraction>().HasData(AttractionData());
        }

        private Citi[] CitiData()
        {
            var citi = new Citi[]
            {
                new Citi {
                    ID = 1,
                    Name = "New York City",
                    Description = "The one with that big park."
                },
                new Citi {
                    ID = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished."
                },
                new Citi {
                    ID = 3,
                    Name = "Paris",
                    Description = "The one with that big tower."
                }
            };
            return citi;
        }

        private Attraction[] AttractionData()
        {
            var attraction = new Attraction[]
            {
                new Attraction {
                    ID = 1,
                    Name = "Central Park",
                    Description = "The most visited urban park in the world!",
                    CitiID = 1
                },
                new Attraction {
                    ID = 2,
                    Name = "Empire State Building",
                    Description = "A 102-story skyscrapper located in Midtown Manhatan.",
                    CitiID = 1
                },
                new Attraction {
                    ID = 3,
                    Name = "Cathedral of Our Lady",
                    Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.",
                    CitiID = 2
                },
                new Attraction {
                    ID = 4,
                    Name = "Antwerp Central Station",
                    Description = "The finest example of railway architecture in Belgium.",
                    CitiID = 2
                },
                new Attraction {
                    ID = 5,
                    Name = "Eiffel Tower",
                    Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.",
                    CitiID = 3
                },
                new Attraction {
                    ID = 6,
                    Name = "The Louvra",
                    Description = "The world's largest museum.",
                    CitiID = 3
                }
            };

            return attraction;
        }
    }
}
