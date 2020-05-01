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
	class Program
	{
		static void Main(string[] args) { new Program().Run(); }

		private const int WebsitePermanentOfferCount = 2;
		private const int WebsiteTemporaryOfferCount = 3;
		private Random rd = new Random(257);

		private void AddSomeOffers(int number, ITravelAgency[] travelAgencies, IAdAgency[] adAgencies)
		{
			System.Random random = new Random();
			int ta = travelAgencies.Length;
			int aa = adAgencies.Length;
			for(int i = 0; i < number; i++)
			{
				offers.Add(adAgencies[random.Next(0, aa - 1)].CreateOffer(travelAgencies[random.Next(0, ta - 1)]));
			}
		}
		//----------
		//YOUR CODE - additional fileds/properties/methods
		//----------
		private List<ITravelAgency> travelAgencies = new List<ITravelAgency>();
		private List<IAdAgency> adAgencies = new List<IAdAgency>();
		private IWebsite offerWebsite;
		private List<IOffer> offers = new List<IOffer>();

		public void Run()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			(
				BookingDatabase accomodationData, 
				TripAdvisorDatabase tripsData, 
				ShutterStockDatabase photosData, 
				OysterDatabase reviewData
			) = Init.Init.Run();
			
			
			travelAgencies.Add(new PolandTravel(tripsData,reviewData, accomodationData, photosData));
			travelAgencies.Add(new FranceTravel(tripsData, reviewData, accomodationData, photosData));
			travelAgencies.Add(new ItalyTravel(tripsData, reviewData, accomodationData, photosData));

			adAgencies.Add(new GraphicalAgency(3, 3));
			adAgencies.Add(new ReviewAgency(3, 3));

			AddSomeOffers(10, travelAgencies.ToArray(), adAgencies.ToArray());
			offerWebsite = new Website(WebsiteTemporaryOfferCount, WebsitePermanentOfferCount, offers);
			//----------
			//YOUR CODE - set up everything
			//----------

			while (true)
            {
				Console.Clear();

				//----------
				//YOUR CODE - run
				//----------
				//AddSomeOffers(50, travelAgencies.ToArray(), adAgencies.ToArray());

				//uncomment
				//Console.WriteLine("\n\n=======================FIRST PRESENT======================");
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
