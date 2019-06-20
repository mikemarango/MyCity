namespace City.Models
{
    public class Attraction
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CitiID { get; set; }
        public Citi Citi { get; set; }
    }
}