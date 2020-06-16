using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.Text;
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja pewnych konkrecji na potrzeby chain of responsibility displayu
namespace BigTask2.Ui
{
    class XMLVehicle : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"<Vehicle>{route.VehicleType}</Vehicle>");
        }
    }
    class XMLCost : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"<Cost>{route.Cost}</Cost>");
        }
    }

    class XMLTime : ADisplayRouteElement
    {
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine($"<TravelTime>{route.TravelTime}</TravelTime>");
        }
    }

    class XMLCityName : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"<Name>{city.Name}</Name>");
        }
    }

    class XMLPopulation : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"<Population>{city.Population}</Population>");
        }
    }

    class XMLRestaurant : ADisplayCityElement
    {
        public override void DisplaySelf(City city)
        {
            Console.WriteLine($"<Restaurant>{city.HasRestaurant}</Restaurant>");
        }
    }
}
