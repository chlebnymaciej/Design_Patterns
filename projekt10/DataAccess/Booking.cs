using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
	//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
	//  Maciej Chlebny

	// dodane metoda getIterator
	public class ListNode
	{
		public ListNode Next { get; set; }
		public string Name { get; set; }
		public string Rating { get; set; }//Encrypted
		public string Price { get; set; }//Encrypted
	}
	class BookingDatabase 
	{
		public ListNode[] Rooms { get; set; }

		public IIterator<ListNode> GetIterator()
		{
			return new BookingIterator(Rooms);
		}
	}

}
