using projekt10.TravelAgencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;
using TravelAgencies.DataAccess;

namespace TravelAgencies
{
	//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
	//  Maciej Chlebny
	class Program
	{
		static void Main(string[] args) { new Program().Run(); }

		private const int WebsitePermanentOfferCount = 2;
		private const int WebsiteTemporaryOfferCount = 3;
		private Random rd = new Random(257);


		//----------
		//YOUR CODE - additional fileds/properties/methods
		
		private List<ITravelAgency> travelAgencies = new List<ITravelAgency>();
		private List<IAdAgency> adAgencies = new List<IAdAgency>();
		private IWebsite offerWebsite;
		
		//----------

		public void Run()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			(
				BookingDatabase accomodationData, 
				TripAdvisorDatabase tripsData, 
				ShutterStockDatabase photosData, 
				OysterDatabase reviewData
			) = Init.Init.Run();

			//----------
			//YOUR CODE - set up everything			
			travelAgencies.Add(new PolandTravel(tripsData,reviewData, accomodationData, photosData));
			travelAgencies.Add(new FranceTravel(tripsData, reviewData, accomodationData, photosData));
			travelAgencies.Add(new ItalyTravel(tripsData, reviewData, accomodationData, photosData));

			adAgencies.Add(new GraphicalAgency(3, 2));
			adAgencies.Add(new ReviewAgency(3, 2));

			offerWebsite = new Website(WebsiteTemporaryOfferCount, WebsitePermanentOfferCount, travelAgencies, adAgencies);

			//----------

			while (true)
            {
				Console.Clear();

				//----------
				//YOUR CODE - run
				// nie koniecznie potrzebne ale czemu nie niech będzie
				for (int i = 0; i < 5; i++)
				{
					offerWebsite.UpdateOfferList();
				}
				//----------

				//uncomment
				Console.WriteLine("\n\n=======================FIRST PRESENT======================");
				offerWebsite.Present();
				Console.WriteLine("\n\n=======================SECOND PRESENT======================");
				offerWebsite.Present();
				Console.WriteLine("\n\n=======================THIRD PRESENT======================");
				offerWebsite.Present();


				if (HandleInput()) break;
			}
		}
		bool HandleInput()
		{
			var key = Console.ReadKey(true);
			return key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Q;
		}
    }
}
