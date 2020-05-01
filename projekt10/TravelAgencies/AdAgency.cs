using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Agencies
{
    interface IAdAgency
    {
        IOffer CreateOffer(ITravelAgency travelAgency);
    }


    class GraphicalAgency : IAdAgency
    {
        private static System.Random random = new Random();
        public int Photos { get; set; }
        public int Time { get; set; }
        internal GraphicalAgency(int number_of_photos, int time_of_temporary)
        {
            this.Photos = number_of_photos;
            this.Time = time_of_temporary;
        }
        public IOffer CreateOffer(ITravelAgency travelAgency)
        {
            ITrip trip = travelAgency.CreateTrip();
            List<IPhoto> photos = new List<IPhoto>();
            int i = 0;

            
            while (i < Photos)
            {
                photos.Add(travelAgency.CreatePhoto());
                i++;
            }
            if(random.Next()%2==0)
                return new GraphicalOffer(trip, photos.ToArray());
            return new GraphicalOffer(trip, photos.ToArray(), Time);

        }
    }

    class ReviewAgency : IAdAgency
    {
        private static System.Random random = new Random();
        public int Review { get; set; }
        public int Time { get; set; }
        internal ReviewAgency(int number_of_review, int time_of_temporary)
        {
            this.Review = number_of_review;
            this.Time = time_of_temporary;
        }
        public IOffer CreateOffer(ITravelAgency travelAgency)
        {
            ITrip trip = travelAgency.CreateTrip();
            List<IReview> reviews = new List<IReview>();
            int i = 0;
            while (i < Review)
            {
                reviews.Add(travelAgency.CreateReview());
                i++;
            }
            if (random.Next() % 2 == 0)
                return new ReviewOffer(trip, reviews.ToArray());
            return new ReviewOffer(trip, reviews.ToArray(), Time);

        }
    }
}
