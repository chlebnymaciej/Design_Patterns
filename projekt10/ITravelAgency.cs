using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;

namespace TravelAgencies.Agencies
{
	//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
	//  Maciej Chlebny

	/* 
	 * Abstract Factory Pattern
     *  ITravelAgency
     */
	public interface ITravelAgency
	{
		ITrip CreateTrip();
		IPhoto CreatePhoto();
		IReview CreateReview();
	}

	public abstract class AbstractTravel : ITravelAgency
	{
		protected System.Random random;
		public int Min { get; set; }
		public int Max { get; set; }
		protected string Country;
		public int AttractiionsDaily { get; set; }

		internal TripAdvisorDatabase tripAdvisorDatabase;
		internal OysterDatabase oysterDatabase;
		internal BookingDatabase bookingDatabase;
		internal ShutterStockDatabase stockDatabase;

		internal IIterator<PhotMetadata> it_stock;
		internal IIterator<PlainReview> it_oyster;
		internal IIterator<ListNode> it_booking;
		internal IIterator<TripAdvisorRecord> it_trip;

		internal BookingCoder bookingCoder;
		internal TripAdvisorCoder advisorCoder;
		internal ShutterStockCoder stockCoder;
		 

		internal AbstractTravel(TripAdvisorDatabase tripAdvisorDatabase,
		 OysterDatabase oysterDatabase,
		 BookingDatabase bookingDatabase,
		 ShutterStockDatabase stockDatabase)
		{
			this.tripAdvisorDatabase = tripAdvisorDatabase;
			this.oysterDatabase = oysterDatabase;
			this.bookingDatabase = bookingDatabase;
			this.stockDatabase = stockDatabase;

			it_booking = bookingDatabase.GetIterator();
			it_oyster = oysterDatabase.GetIterator();
			it_stock = stockDatabase.GetIterator();
			it_trip = tripAdvisorDatabase.GetIterator();
			
			Min = 1;
			Max = 4;
			AttractiionsDaily = 3;
			bookingCoder = BookingCoder.GetCoder();
			advisorCoder = TripAdvisorCoder.GetCoder();
			stockCoder = ShutterStockCoder.GetCoder();

			random = new Random();
		}
		public abstract IPhoto CreatePhoto();

		public abstract IReview CreateReview();

		public abstract ITrip CreateTrip();
	}
	public class PolandTravel : AbstractTravel
	{

		internal PolandTravel(TripAdvisorDatabase tripAdvisorDatabase,
							 OysterDatabase oysterDatabase,
							 BookingDatabase bookingDatabase,
							 ShutterStockDatabase stockDatabase) 
							: base(tripAdvisorDatabase,
								  oysterDatabase,
								  bookingDatabase,
								  stockDatabase){ this.Country = "Poland"; }

		private double MinLongitude = 14.4, MaxLongitude = 23.5;
		private double MinLatitude = 49.8, MaxLatitude = 54.2;

		public override IPhoto CreatePhoto()
		{
			while (true)
			{
				PhotMetadata tmp = it_stock.Get();
				if (tmp == null)
				{
					it_stock = stockDatabase.GetIterator();
					if (it_stock.Get() == null) throw new Exception("StockDatabase is empty");
					else
					{
						tmp = it_stock.Get();
					}
				}
				if (it_stock.HasNext())	it_stock.Next();
				else it_stock = stockDatabase.GetIterator();


				double longitute = tmp.Longitude;
				double latitute = tmp.Latitude;

				if(MinLatitude <= latitute && latitute <= MaxLatitude
				&& MinLongitude <= longitute && longitute <= MaxLongitude)
				{
					PhotMetadata decrypted = new PhotMetadata(tmp);
					decrypted.HeightPx = stockCoder.Decoder.Handle(decrypted.HeightPx);
					decrypted.WidthPx = stockCoder.Decoder.Handle(decrypted.WidthPx);

					// testing if data is correct
					String test1 = stockCoder.Coder.Handle(decrypted.HeightPx);
					if (test1 != tmp.HeightPx) continue;

					test1 = stockCoder.Coder.Handle(decrypted.WidthPx);
					if (test1 != tmp.WidthPx) continue;

					return new PolandPhoto(new PlainPhoto(decrypted)); 
				}
			}
		}

		public override IReview CreateReview()
		{
			PlainReview tmp = it_oyster.Get();
			if (tmp == null)
			{
				it_oyster = oysterDatabase.GetIterator();
				if (it_oyster.Get() == null) throw new Exception("OysterkDatabase is empty");
				else
				{
					tmp = it_oyster.Get();
				}
			}
			if (it_oyster.HasNext()) it_oyster.Next();
			else it_oyster = oysterDatabase.GetIterator();

			return new PolandReview(tmp);
		}

		public override ITrip CreateTrip()
		{
			
			int days = random.Next(Min, Max);
			DayTrip[] trip = new DayTrip[days];
			for(int i = 0; i < days;)
			{
				Attraction[] attr = new Attraction[AttractiionsDaily];

				for (int j = 0; j < AttractiionsDaily;)
				{
					
					TripAdvisorRecord tmp = it_trip.Get();
					if (tmp == null)
					{

						it_trip = tripAdvisorDatabase.GetIterator();
						if (it_trip.Get() == null) throw new Exception("TripAdvisorDatabase is empty");
						else tmp = it_trip.Get();
					}
					if (it_trip.HasNext()) it_trip.Next();
					else it_trip = tripAdvisorDatabase.GetIterator();
					if (tmp.Country != this.Country) continue;

					Attraction tmp_Attr = new Attraction(tmp.Name,
												advisorCoder.Decoder.Handle(tmp.Rating),
												advisorCoder.Decoder.Handle(tmp.Price),
												tmp.Country);

					String test = advisorCoder.Coder.Handle(tmp_Attr.Rating);
					if (test != tmp.Rating) continue;
					test = advisorCoder.Coder.Handle(tmp_Attr.Price);
					if (test != tmp.Price) continue;

					attr[j] = tmp_Attr;
					j++;
				}

				ListNode tmp_room = it_booking.Get();
				if(tmp_room==null)
				{
					it_booking = bookingDatabase.GetIterator();
					if (it_booking.Get() == null) throw new Exception("BookingDatabase is empty");
					else tmp_room = it_booking.Get();
				}
				if (it_booking.HasNext()) it_booking.Next();
				else it_booking = bookingDatabase.GetIterator();
				Room r = new Room(tmp_room.Name,
					bookingCoder.Decoder.Handle(tmp_room.Rating),
					bookingCoder.Decoder.Handle(tmp_room.Price));

				String test1 = bookingCoder.Coder.Handle(r.Rating);
				if (test1 != tmp_room.Rating) continue;

				test1 = bookingCoder.Coder.Handle(r.Price);
				if (test1 != tmp_room.Price) continue;

				trip[i] = new DayTrip(r, attr);
				i++;
			}

			return new Trip(days, trip);
		}
	}

	public class FranceTravel : AbstractTravel
	{

		internal FranceTravel(TripAdvisorDatabase tripAdvisorDatabase,
							 OysterDatabase oysterDatabase,
							 BookingDatabase bookingDatabase,
							 ShutterStockDatabase stockDatabase)
							: base(tripAdvisorDatabase,
								  oysterDatabase,
								  bookingDatabase,
								  stockDatabase)
		{ this.Country = "France"; }

		private double MinLongitude = 0, MaxLongitude = 5.4;
		private double MinLatitude = 43.6, MaxLatitude = 50.0;

		public override IPhoto CreatePhoto()
		{
			while (true)
			{
				PhotMetadata tmp = it_stock.Get();
				if (tmp == null)
				{
					it_stock = stockDatabase.GetIterator();
					if (it_stock.Get() == null) throw new Exception("StockDatabase is empty");
					else
					{
						tmp = it_stock.Get();
					}
				}
				if (it_stock.HasNext()) it_stock.Next();
				else it_stock = stockDatabase.GetIterator();


				double longitute = tmp.Longitude;
				double latitute = tmp.Latitude;

				if (MinLatitude <= latitute && latitute <= MaxLatitude
				&& MinLongitude <= longitute && longitute <= MaxLongitude)
				{
					PhotMetadata decrypted = new PhotMetadata(tmp);
					decrypted.HeightPx = stockCoder.Decoder.Handle(decrypted.HeightPx);
					decrypted.WidthPx = stockCoder.Decoder.Handle(decrypted.WidthPx);

					// testing if data is correct
					String test1 = stockCoder.Coder.Handle(decrypted.HeightPx);
					if (test1 != tmp.HeightPx) continue;

					test1 = stockCoder.Coder.Handle(decrypted.WidthPx);
					if (test1 != tmp.WidthPx) continue;

					return new FrancePhoto(new PlainPhoto(decrypted));
				}
			}
		}

		public override IReview CreateReview()
		{
			PlainReview tmp = it_oyster.Get();
			if (tmp == null)
			{
				it_oyster = oysterDatabase.GetIterator();
				if (it_oyster.Get() == null) throw new Exception("OysterkDatabase is empty");
				else
				{
					tmp = it_oyster.Get();
				}
			}
			if (it_oyster.HasNext()) it_oyster.Next();
			else it_oyster = oysterDatabase.GetIterator();

			return new FranceReview(tmp);
		}

		public override ITrip CreateTrip()
		{
			int days = random.Next(Min, Max);
			DayTrip[] trip = new DayTrip[days];
			for (int i = 0; i < days;)
			{
				Attraction[] attr = new Attraction[AttractiionsDaily];

				for (int j = 0; j < AttractiionsDaily;)
				{
					TripAdvisorRecord tmp = it_trip.Get();
					if (tmp == null)
					{
						it_trip = tripAdvisorDatabase.GetIterator();
						if (it_trip.Get() == null) throw new Exception("TripAdvisorDatabase is empty");
						else tmp = it_trip.Get();
					}
					if (it_trip.HasNext()) it_trip.Next();
					else it_trip = tripAdvisorDatabase.GetIterator();

					if (tmp.Country != this.Country) continue;
					Attraction tmp_Attr = new Attraction(tmp.Name,
												advisorCoder.Decoder.Handle(tmp.Rating),
												advisorCoder.Decoder.Handle(tmp.Price),
												tmp.Country);
					String test = advisorCoder.Coder.Handle(tmp_Attr.Rating);
					if (test != tmp.Rating) continue;
					test = advisorCoder.Coder.Handle(tmp_Attr.Price);
					if (test != tmp.Price) continue;

					attr[j] = tmp_Attr;
					j++;
				}

				ListNode tmp_room = it_booking.Get();
				if (tmp_room == null)
				{
					it_booking = bookingDatabase.GetIterator();
					if (it_booking.Get() == null) throw new Exception("BookingDatabase is empty");
					else tmp_room = it_booking.Get();
				}
				if (it_booking.HasNext()) it_booking.Next();
				else it_booking = bookingDatabase.GetIterator();

				Room r = new Room(tmp_room.Name,
					bookingCoder.Decoder.Handle(tmp_room.Rating),
					bookingCoder.Decoder.Handle(tmp_room.Price));

				String test1 = bookingCoder.Coder.Handle(r.Rating);
				if (test1 != tmp_room.Rating) continue;
				test1 = bookingCoder.Coder.Handle(r.Price);
				if (test1 != tmp_room.Price) continue;

				trip[i] = new DayTrip(r, attr);
				i++;
			}
			
			return new Trip(days, trip);
		}
	}

	public class ItalyTravel : AbstractTravel
	{

		internal ItalyTravel(TripAdvisorDatabase tripAdvisorDatabase,
							 OysterDatabase oysterDatabase,
							 BookingDatabase bookingDatabase,
							 ShutterStockDatabase stockDatabase)
							: base(tripAdvisorDatabase,
								  oysterDatabase,
								  bookingDatabase,
								  stockDatabase)
		{ this.Country = "Italy"; }

		private double MinLongitude = 8.8, MaxLongitude = 15.2;
		private double MinLatitude = 37.7, MaxLatitude = 44.0;

		public override IPhoto CreatePhoto()
		{
			while (true)
			{
				PhotMetadata tmp = it_stock.Get();
				if (tmp == null)
				{
					it_stock = stockDatabase.GetIterator();
					if (it_stock.Get() == null) throw new Exception("StockDatabase is empty");
					else
					{
						tmp = it_stock.Get();
					}
				}
				if (it_stock.HasNext()) it_stock.Next();
				else it_stock = stockDatabase.GetIterator();


				double longitute = tmp.Longitude;
				double latitute = tmp.Latitude;

				if (MinLatitude <= latitute && latitute <= MaxLatitude
				&& MinLongitude <= longitute && longitute <= MaxLongitude)
				{
					PhotMetadata decrypted = new PhotMetadata(tmp);
					decrypted.HeightPx = stockCoder.Decoder.Handle(decrypted.HeightPx);
					decrypted.WidthPx = stockCoder.Decoder.Handle(decrypted.WidthPx);

					// testing if data is correct
					String test1 = stockCoder.Coder.Handle(decrypted.HeightPx);
					if (test1 != tmp.HeightPx) continue;

					test1 = stockCoder.Coder.Handle(decrypted.WidthPx);
					if (test1 != tmp.WidthPx) continue;

					return new ItalyPhoto(new PlainPhoto(decrypted));
				}
			}
		}

		public override IReview CreateReview()
		{
			PlainReview tmp = it_oyster.Get();
			if (tmp == null)
			{
				it_oyster = oysterDatabase.GetIterator();
				if (it_oyster.Get() == null) throw new Exception("OysterkDatabase is empty");
				else
				{
					tmp = it_oyster.Get();
				}
			}
			if (it_oyster.HasNext()) it_oyster.Next();
			else it_oyster = oysterDatabase.GetIterator();

			return new ItalyReview(tmp);
		}

		public override ITrip CreateTrip()
		{
			int days = random.Next(Min, Max);
			DayTrip[] trip = new DayTrip[days];
			for (int i = 0; i < days;)
			{
				Attraction[] attr = new Attraction[AttractiionsDaily];

				for (int j = 0; j < AttractiionsDaily;)
				{
					TripAdvisorRecord tmp = it_trip.Get();
					if (tmp == null)
					{
						it_trip = tripAdvisorDatabase.GetIterator();
						if (it_trip.Get() == null) throw new Exception("TripAdvisorDatabase is empty");
						else tmp = it_trip.Get();
					}
					if (it_trip.HasNext()) it_trip.Next();
					else it_trip = tripAdvisorDatabase.GetIterator();

					if (tmp.Country != this.Country) continue;
					Attraction tmp_Attr = new Attraction(tmp.Name,
												advisorCoder.Decoder.Handle(tmp.Rating),
												advisorCoder.Decoder.Handle(tmp.Price),
												tmp.Country);
					String test = advisorCoder.Coder.Handle(tmp_Attr.Rating);
					if (test != tmp.Rating) continue;
					test = advisorCoder.Coder.Handle(tmp_Attr.Price);
					if (test != tmp.Price) continue;

					attr[j] = tmp_Attr;
					j++;
				}

				ListNode tmp_room = it_booking.Get();
				if (tmp_room == null)
				{
					it_booking = bookingDatabase.GetIterator();
					if (it_booking.Get() == null) throw new Exception("BookingDatabase is empty");
					else tmp_room = it_booking.Get();
				}
				if (it_booking.HasNext()) it_booking.Next();
				else it_booking = bookingDatabase.GetIterator();

				Room r = new Room(tmp_room.Name,
					bookingCoder.Decoder.Handle(tmp_room.Rating),
					bookingCoder.Decoder.Handle(tmp_room.Price));

				String test1 = bookingCoder.Coder.Handle(r.Rating);
				if (test1 != tmp_room.Rating) continue;
				test1 = bookingCoder.Coder.Handle(r.Price);
				if (test1 != tmp_room.Price) continue;

				trip[i] = new DayTrip(r, attr);
				i++;

			}

			return new Trip(days, trip);
		}
	}
}