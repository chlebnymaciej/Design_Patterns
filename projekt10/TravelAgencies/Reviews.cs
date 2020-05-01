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
     *  Decorator Pattern
     *  
     */
    public interface IReview
    {
        string User { get; }
        string Review { get; }
        string Decorated(string text);
    }

    public class PlainReview : IReview
    {
        public string User { get; private set; }
        public string Review { get; private set; }
        public PlainReview(string User, string Review)
        {
            this.User = User;
            this.Review = Review;
        }
        public string Decorated(string text)
        {
            return null;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.User);
            sb.Append(" | ");
            sb.Append(this.Review);
            return sb.ToString();
        }
    }

    public abstract class AReview : IReview
    {
        public virtual string User { get;}
        public virtual string Review { get; }

        public IReview review;
        public abstract string Decorated(string text);
        public AReview(IReview review)
        {
            this.review = review;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(this.User);
            sb.Append(" | ");
            sb.Append(this.Review);
            return sb.ToString();
        }
    }

    public class PolandReview : AReview
    {
        public override string User
        {
            get
            {
                return Decorated(review.User);
            }
        }
        public override string Review
        {
            get
            {
                return Decorated(review.Review);
            }
        }

        public PolandReview(IReview r) : base(r) { } 
        public override string Decorated(string text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                switch (c)
                {
                    case 'e':
                        sb.Append('ę');
                        break;
                    case 'a':
                        sb.Append('ą');
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    public class ItalyReview : AReview
    {
        public override string User
        {
            get
            {
                return Decorated(review.User);
            }
        }
        public override string Review
        {
            get
            {
                return review.Review;
            }
        }
        public ItalyReview(IReview r) : base(r) { }
        public override string Decorated(string text)
        {
            StringBuilder sb = new StringBuilder("Della_");
            sb.Append(text);
            return sb.ToString();
        }
    }

    public class FranceReview : AReview
    {
        public override string User
        {
            get
            {
                return review.User;
            }
        }
        public override string Review
        {
            get
            {
                return Decorated(review.Review);
            }
        }

        public FranceReview(IReview r) : base(r) { }
        public override string Decorated(string text)
        {
            List<string> decorated_array = new List<string>();
            string[] array = text.Split(' ');
            foreach (string s in array)
            {

                if (s.Length < 4)
                    decorated_array.Add("la");
                else
                    decorated_array.Add(s);

            }

            return String.Join(" ", decorated_array);
        }
    }
    
}
