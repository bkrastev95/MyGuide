namespace MyGuide.Models
{
    public class Destination
    {
        public long Id { get; set; }

        public string CityName { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public string IconUrl { get; set; }

        public float Popularity { get; set; }

        public string MorePicsUrl { get; set; }
    }
}
