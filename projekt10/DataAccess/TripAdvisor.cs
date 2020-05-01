using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
	class TripAdvisorDatabase 
	{
		public Guid[] Ids;
		public Dictionary<Guid, string>[] Names { get; set; }
		public Dictionary<Guid, string> Prices { get; set; }//Encrypted
		public Dictionary<Guid, string> Ratings { get; set; }//Encrypted
		public Dictionary<Guid, string> Countries { get; set; }

		public IIterator<TripAdvisorRecord> GetIterator()
		{
			return new TripAdvisorIterator(this);
		}
	}

	class TripAdvisorRecord
	{
		public string Name { get; set; }
		public string Rating { get; set; }//Encrypted
		public string Price { get; set; }//Encrypted

		public string Country { get; set; }
		public TripAdvisorRecord(string Name, string Rating, string Price, string Country)
		{
			this.Name = Name;
			this.Rating = Rating;
			this.Price = Price;
			this.Country = Country;
		}
	}
}

