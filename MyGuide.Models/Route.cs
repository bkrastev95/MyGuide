using System;
using System.Collections.Generic;


namespace MyGuide.Models
{
    public class Route
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long HomeId { get; set; }
        
        public DateTime Date { get; set; }

        public List<Destination> Destinations { get; set; }
    }
}
