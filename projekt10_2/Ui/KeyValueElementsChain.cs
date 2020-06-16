using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.Text;
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja pewnych konkrecji na potrzeby chain of responsibility displayu
namespace BigTask2.Ui
{
    class KeyValueVehicle : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"Vehicle = {route.VehicleType}");
        }
    }
    class KeyValueCost : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"Cost = {route.Cost}");
        }
    }

    class KeyValueTime : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"TravelTime = {route.TravelTime}");
        }
    }

    class KeyValueCityName : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"Name = {city.Name}");
        }
    }

    class KeyValuePopulation : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"Population = {city.Population}");
        }
    }

    class KeyValueRestaurant : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"Restaurant = {city.HasRestaurant}");
        }
    }
}

