//This file Can be modified

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja abstract factory dla systemów XML i KeyValue
// Displaye są robione przez odpowiednie chain of responsibility dla rozszerzalności zadania
// Natomiast Form przez słownik, obydwie części są w takim samym rozszerzalne
using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Xml;

namespace BigTask2.Ui
{
    interface IForm
    {
        void Insert(string command);
        bool GetBoolValue(string name);
        string GetTextValue(string name);
        int GetNumericValue(string name);
    }
    interface IDisplay
    {
        void Print(IEnumerable<Route> routes);
    }
    interface ISystem
    {
        IForm Form { get; }
        IDisplay Display { get; }
    }

    class SystemGeneral : ISystem
    {
        public IForm Form
        {
            get;
            private set;
        }

        public IDisplay Display
        {
            get;
            private set;
        }

        public SystemGeneral(IForm form, IDisplay display)
        {
            Form = form;
            Display = display;
        }
    }

    /* Na wszelki wypadek jakby trzeba było
    class KeyValueSystem : ISystem
    {
        public IForm Form
        {
            get;
            private set;
        }

        public IDisplay Display
        {
            get;
            private set;
        }

        KeyValueSystem(IForm form, IDisplay display)
        {
            Form = form;
            Display = display;
        }
    }
    */
    class KeyValueForm : IForm
    {
        private Dictionary<string, string> obj = new Dictionary<string, string>();
        public bool GetBoolValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data");

            bool output;
            if (bool.TryParse(obj[name], out output))
                return output;
            else throw new Exception("incorrect data");
        }

        public int GetNumericValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data");

            int output;
            if (int.TryParse(obj[name], out output))
                return output;
            else throw new Exception("incorrect data");
        }

        public string GetTextValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data");
            return obj[name];
        }

        public void Insert(string command)
        {
            string[] alldata = command.Split('=');

            for (int i = 0; i < alldata.Length; i += 2)
            {
                if (obj.ContainsKey(alldata[i]))
                    obj.Clear();
                obj.Add(alldata[i], alldata[i + 1]);
            }
        }
    }
    class KeyValueDisplay : IDisplay
    {
        private IDisplayRouteElement displayRouteElement;
        private IDisplayCityElement displayCityElement;
        public KeyValueDisplay(IDisplayCityElement displayCity, IDisplayRouteElement displayRouteElement)
        {
            this.displayCityElement = displayCity;
            this.displayRouteElement = displayRouteElement;
        }

        public void Print(IEnumerable<Route> routes)
        {
            if (routes == null)
            {
                Console.WriteLine("Given route do not exists");
                return;
            }

            bool first = false;
            double time = 0.0;
            double cost = 0.0;
            foreach (Route r in routes)
            {
                if (!first)
                {
                    displayCityElement.Display(r.From);
                    first = true;
                }
                displayRouteElement.Display(r);

                time += r.TravelTime;
                cost += r.Cost;
                displayCityElement.Display(r.To);
            }
            Console.WriteLine("\nCost = " + (cost * 100) / 100);
            Console.WriteLine("Time = " + (time * 100) / 100);
        }

    }

    /* Na wszelki wypadek jakby trzeba było
    class XMLSystem : ISystem
    {
        public IForm Form
        {
            get;
            private set;
        }

        public IDisplay Display
        {
            get;
            private set;
        }

        XMLSystem(IForm form, IDisplay display)
        {
            Form = form;
            Display = display;
        }
    }
    */
    class XMLForm : IForm
    {
        private Dictionary<string, string> obj = new Dictionary<string, string>();

        public bool GetBoolValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data");

            bool output;
            if (bool.TryParse(obj[name], out output))
                return output;
            else throw new Exception("incorrect data");
        }

        public int GetNumericValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data " + name);

            int output;
            if (int.TryParse(obj[name], out output))
                return output;
            else throw new Exception("incorrect data");
        }

        public string GetTextValue(string name)
        {
            if (!obj.ContainsKey(name)) throw new Exception("incorrect data " + name);
            return obj[name];
        }

        public void Insert(string command)
        {

            StringBuilder sb = new StringBuilder("<tmpRoot>");
            sb.Append(command);
            sb.Append("</tmpRoot>");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(sb.ToString());
            foreach(XmlNode x in xml.DocumentElement)
            {
                if (obj.ContainsKey(x.LocalName))
                    obj.Clear();
                obj.Add(x.LocalName, x.InnerText);
            }
        }
    }
    class XMLDisplay : IDisplay
    {
        private IDisplayRouteElement displayRouteElement;
        private IDisplayCityElement displayCityElement;
        public XMLDisplay(IDisplayCityElement displayCity, IDisplayRouteElement displayRouteElement)
        {
            this.displayCityElement = displayCity;
            this.displayRouteElement = displayRouteElement;
        }

        public void Print(IEnumerable<Route> routes)
        {
            if (routes == null)
            {
                Console.WriteLine("Given route do not exists");
                return;
            }

            bool first = false;
            double time = 0.0;
            double cost = 0.0;
            foreach (Route r in routes)
            {
                if (!first)
                {
                    displayCityElement.Display(r.From);
                    first = true;
                }
                displayRouteElement.Display(r);
                
                time += r.TravelTime;
                cost += r.Cost;
                displayCityElement.Display(r.To);
            }

            Console.WriteLine("\n<Cost> " + (cost * 100) / 100 + " </Cost>");
            Console.WriteLine("<Time> " + (time * 100) / 100 + " </Time>");
        }
    }


    interface ISystemFactory
    {
        IForm CreateForm();
        IDisplay CreateDisplay();

        IDisplayCityElement CreateCityElementDisplayChain();
        IDisplayRouteElement CreateRouteElementDisplayChain();
    }

    class XMLFactory : ISystemFactory
    {
        public IDisplayCityElement CreateCityElementDisplayChain()
        {
            IDisplayCityElement chain = new DisplayCustomStringCity("<City>");
            chain.AddNext(new XMLCityName())
                .AddNext(new XMLPopulation())
                .AddNext(new XMLRestaurant())
                .AddNext(new DisplayCustomStringCity("</City>\n"));
            return chain;
        }

        public IDisplay CreateDisplay()
        {
            return new XMLDisplay(CreateCityElementDisplayChain(),CreateRouteElementDisplayChain());
        }

        public IForm CreateForm()
        {
            return new XMLForm();
        }

        public IDisplayRouteElement CreateRouteElementDisplayChain()
        {
            IDisplayRouteElement chain = new DisplayCustomStringRoute("<Route />");
            chain.AddNext(new XMLVehicle())
                .AddNext(new XMLCost())
                .AddNext(new XMLTime())
                .AddNext(new DisplayCustomStringRoute());
            return chain;
        }
    }

    class KeyValueFactory : ISystemFactory
    {
        public IDisplayCityElement CreateCityElementDisplayChain()
        {
            IDisplayCityElement chain = new DisplayCustomStringCity("==  City  ==");
            chain.AddNext(new KeyValueCityName())
                .AddNext(new KeyValuePopulation())
                .AddNext(new KeyValueRestaurant())
                .AddNext(new DisplayCustomStringCity(" ---------- \n"));
            return chain;
        }

        public IDisplay CreateDisplay()
        {
            return new KeyValueDisplay(CreateCityElementDisplayChain(), CreateRouteElementDisplayChain());
        }

        public IForm CreateForm()
        {
            return new KeyValueForm();
        }

        public IDisplayRouteElement CreateRouteElementDisplayChain()
        {
            IDisplayRouteElement chain = new DisplayCustomStringRoute("== Route ==");
            chain.AddNext(new KeyValueVehicle())
                .AddNext(new KeyValueCost())
                .AddNext(new KeyValueTime())
                .AddNext(new DisplayCustomStringRoute());
            return chain;
        }
    }
}
