using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;

namespace TravelAgencies.Agencies
{

    public class DayTrip
    {
        public Room room { get; set; }
        public Attraction[] attractions { get; set; }

        public DayTrip(Room room, Attraction[] attr)
        {
            this.room = room;
            this.attractions = attr;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Staying at ");
            sb.Append(room.ToString());
            sb.Append("\n\nAttractions: \n");
            foreach(Attraction a in attractions)
            {
                sb.Append("\t"+a.ToString());
                sb.Append("\n");
            }
            
            return sb.ToString();
        }
    }
    public class Room
    {
        public string Name { get; set; }
        public string Rating { get; set; }
        public string Price { get; set; }
        
        public Room(string Name, string Rating, string Price)
        {
            this.Name = Name;
            this.Rating = Rating;
            this.Price = Price;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
    public class Attraction
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; } 
        public string Country { get; set; }

        public Attraction(string Name, string Rating, string Price,string Country)
        {
            this.Name = Name;
            this.Rating = Rating;
            this.Price = Price;
            this.Country = Country;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
