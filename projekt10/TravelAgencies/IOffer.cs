using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Agencies
{

    interface IOffer
    {
        int TimeLeft { get; }
        ITrip trip { get; }
        void Post();
        bool IsTemporary();
    }

    class GraphicalOffer : IOffer
    {
        public ITrip trip { get; private set; }

        public IPhoto[] photo { get; private set; }

        public int TimeLeft { get; private set; }

        internal GraphicalOffer(ITrip trip, IPhoto[] photos, int TimeLeft = -1)
        {
            this.trip = trip;
            this.photo = photos;
            this.TimeLeft = TimeLeft;
        }

        public void Post()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(trip.ToString());
            foreach (IPhoto p in photo)
            {
                sb.Append(p.ToString() + "\n");
            }
            sb.Append("\n ____________________\n");

            if (TimeLeft == -1)
            {
                sb.Append(trip.ToString());
                foreach(IPhoto p in photo)
                {
                    sb.Append(p.ToString() + "\n");
                }
                sb.Append("\n ____________________\n");
                Console.WriteLine(sb.ToString());
                return;
            }
            if (TimeLeft == 0)
            {
                Console.WriteLine("This offer has expired");
                return;
            }
            else
            {
                sb.Append(trip.ToString());
                foreach (IPhoto p in photo)
                {
                    sb.Append(p.ToString() + "\n");
                }
                sb.Append("\n ____________________\n");
                Console.WriteLine(sb.ToString());
                TimeLeft--;
                return;
            }
        }
        public bool IsTemporary()
        {
            return TimeLeft != -1;
        }
    }

    class ReviewOffer : IOffer
    {
        public int TimeLeft { get; private set; }
        public ITrip trip { get; private set; }

        public IReview[] review { get; private set; }

        internal ReviewOffer(ITrip trip, IReview[] review, int TimeLeft = -1)
        {
            this.trip = trip;
            this.review = review;
            this.TimeLeft = TimeLeft;
        }

        public void Post()
        {
            StringBuilder sb = new StringBuilder();
            if (TimeLeft == -1)
            {
                sb.Append(trip.ToString());
                foreach (IReview p in review)
                {
                    sb.Append(p.ToString() + "\n");
                }
                sb.Append("\n ____________________\n");
                Console.WriteLine(sb.ToString());
                return;
            }
            if (TimeLeft == 0)
            {
                Console.WriteLine("This offer has expired");
                return;
            }
            else
            {
                sb.Append(trip.ToString());
                foreach (IReview p in review)
                {
                    sb.Append(p.ToString()+"\n");
                }
                sb.Append("\n ____________________\n");
                Console.WriteLine(sb.ToString());
                TimeLeft--;
                return;
            }
        }

        public bool IsTemporary()
        {
            return TimeLeft != -1;
        }
    }

}
