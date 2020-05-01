using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;

//Oyster is a website with reviews of various holiday destinations.
namespace TravelAgencies.DataAccess
{
	class BSTNode
	{
		public string Review { get; set; }
		public string UserName { get; set; }
		public BSTNode Left { get; set; }
		public BSTNode Right { get; set; }
	}
	class OysterDatabase 
	{
		public BSTNode Reviews { get; set; }

		public IIterator<PlainReview> GetIterator()
		{
			return new OysterIterator(Reviews);
		}
	}
}
