using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;

namespace TravelAgencies.Agencies
{
    //  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
    //  Maciej Chlebny

    /* 
     * Just one class, because I do not know how to create a 3 different classes
     * because my program structure do not have any use case for them.
     *  
     */
    public interface ITrip
    {
       int Days { get; set; }
       DayTrip[] dayTrips { get; set; }
       double GetPrice();
       double GetRating();
    }

    public class Trip : ITrip
    {
        public int Days { get; set; }
        public DayTrip[] dayTrips { get; set; }


        public Trip(int Days, DayTrip[] dayTrips)
        {
            this.Days = Days;
            this.dayTrips = dayTrips;
        }

        public double GetPrice()
        {
            double sum = 0.0;
            foreach(DayTrip d in dayTrips)
            {
                sum += double.Parse(d.room.Price);

                foreach(Attraction a in d.attractions)
                    sum += double.Parse(a.Price);
            }

            return sum;
        }

        public double GetRating()
        {
            double sum = 0.0;
            int div = 1;
            foreach (DayTrip d in dayTrips)
            {
                sum += double.Parse(d.room.Rating);
                div++;
                foreach (Attraction a in d.attractions)
                {
                    sum += double.Parse(a.Rating);
                    div++;
                }
            }

            return div == 1 ? sum : sum/div;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Rating :");
            sb.Append(this.GetRating());
            sb.Append("\nPrice: ");
            sb.Append(this.GetPrice());

            sb.Append($"\n======={dayTrips[0].attractions[0].Country}==========\n");
            for (int i = 0; i < Days; i++)
            {
                sb.Append("\nDay ");
                int d = i + 1;
                sb.Append(d.ToString());
                sb.Append("\n");
                sb.Append(dayTrips[i].ToString());
            }

            return sb.ToString();
        }
    }
}
