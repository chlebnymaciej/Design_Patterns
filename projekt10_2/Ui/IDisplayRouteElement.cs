using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.Text;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja abstrakcji i pewnych konkrecji na potrzeby chain of responsibility displayu
namespace BigTask2.Ui
{
    interface IDisplayRouteElement
    {
        IDisplayRouteElement AddNext(IDisplayRouteElement element);
        void Display(Route route);
        void DisplaySelf(Route route);
    }

    abstract class ADisplayRouteElement : IDisplayRouteElement
    {
        protected IDisplayRouteElement next { get; private set; }
        public IDisplayRouteElement AddNext(IDisplayRouteElement element)
        {
            this.next = element;
            return this.next;
        }

        public void Display(Route route)
        {
            DisplaySelf(route);
            if (next != null)
                next.Display(route);
        }
        public abstract void DisplaySelf(Route route);
        
    }


    interface IDisplayCityElement
    {
        IDisplayCityElement AddNext(IDisplayCityElement element);
        void Display(City city);
        void DisplaySelf(City city);
    }

    abstract class ADisplayCityElement : IDisplayCityElement
    {
        protected IDisplayCityElement next { get; private set; }
        public IDisplayCityElement AddNext(IDisplayCityElement element)
        {
            this.next = element;
            return this.next;
        }

        public void Display(City city)
        {
            DisplaySelf(city);
            if (next != null)
                next.Display(city);
        }
        public abstract void DisplaySelf(City city);

    }

    class DisplayCustomStringRoute : ADisplayRouteElement
    {
        private string custom;
        public DisplayCustomStringRoute(string custom = "")
        {
            this.custom = custom;
        }
        public override void DisplaySelf(Route route)
        {
            Console.WriteLine(custom);
        }
    }

    class DisplayCustomStringCity : ADisplayCityElement
    {
        private string custom;
        public DisplayCustomStringCity(string custom = "")
        {
            this.custom = custom;
        }
        public override void DisplaySelf(City city)
        {
            Console.WriteLine(custom);
        }
    }

}
