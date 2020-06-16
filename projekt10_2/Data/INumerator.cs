using BigTask2.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Zaimplementowanie iteratorów dla danych baz danych
// w konstruktorach przekazywana jest referencja do listy, a nie cała lista


// Jest również implementacja Fasadowego iteratora GraphUnionDatabase
// i dekoratory filtrujące iteratorów
namespace BigTask2.Data
{
    public interface INumerator
    {
        Route Get();
        bool HasNext();
        void Next();
    }

    public class AdjacencyListIterator : INumerator
    {
        int index = -1;
        List<Route> routesFromVerticle;
        public AdjacencyListIterator(List<Route> list)
        {
            routesFromVerticle = list;
            if (routesFromVerticle.Count > 0)
                index++;

        }

        public Route Get()
        {
            if (index == -1)
                return null;
            return routesFromVerticle[index];
        }

        public bool HasNext()
        {
            return index + 1 < routesFromVerticle.Count;
        }


        public void Next()
        {
            if (HasNext())
                index++;
            else index = -1;
        }
    }
    public class MatrixIterator : INumerator
    {
        int index = -1;
        List<Route> routesFromVerticle;
        public MatrixIterator(List<Route> routes)
        {
            routesFromVerticle = routes;
            if (routesFromVerticle.Count < 1) 
                return;

            index = 0;
            while(index < routesFromVerticle.Count && routesFromVerticle[index] == null)
            { index++; }
            if (index >= routesFromVerticle.Count)
                index = -1;
        }
        public Route Get()
        {
            if (index == -1)
                return null;
            return routesFromVerticle[index];
        }

        public bool HasNext()
        {
            if (index == -1)
                return false;
            
            int tmp = index;
            while (tmp < routesFromVerticle.Count && routesFromVerticle[tmp] == null)
            { tmp++; }
            
            if (tmp == routesFromVerticle.Count)
                tmp = -1;

            return tmp != -1;
        }

        public void Next()
        {
            index++;
            while (index < routesFromVerticle.Count && routesFromVerticle[index] == null)
            { index++; }
            if (index >= routesFromVerticle.Count)
                index = -1;
        }
    }


    // Iterator UnionDatabase
    public interface IFacedeIterator : INumerator
    {
        void AddInumerator(INumerator numerator);
    }
    public class FacadeIterator : IFacedeIterator
    {
        protected int Index { get; set; }
        protected List<INumerator> inums;

        public FacadeIterator(params INumerator[] list)
        {
            inums = new List<INumerator>();

            for(int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                    inums.Add(list[i]);
            }
            if (list.Length > 0)
                Index = 0;
            else Index = -1;
        }

        public void AddInumerator(INumerator numerator)
        {
            if(numerator!=null)
                inums.Add(numerator);
            if (inums.Count == 1)
                Index = 0;
        }
        public Route Get()
        {
            if (Index == -1)
                return null;
            return inums[Index].Get();
        }

        public bool HasNext()
        {
            if (Index == -1)
                return false;

            if (inums[Index].HasNext())
                return true;

            int tmp = Index + 1;
            while (tmp < inums.Count)
            {
                if (inums[tmp].Get() != null)
                    return true;
                else tmp++;
            }
            return false;
        }

        public void Next()
        {
            if (!HasNext())
            {
                Index = -1;
                return;
            }

            if (inums[Index].HasNext())
            {
                inums[Index].Next();
                return;
            }

            Index++;
            while (Index < inums.Count)
            {
                if (inums[Index].Get() != null)
                    return;
                else Index++;
            }
            return;

        }
    }

    // Dekoratory filtrujące iteratorów
    // Mały feature HasNext nie działa tak jak powinien 
    // gdyż nie zdążyłem zaimplementować Prototypu do klonowania
    public interface INumeratorDecorator : INumerator
    {
        INumerator previous { get; }
        bool Filter(Route route);
    }
    public abstract class AbstractDecorator : INumeratorDecorator
    {
        public INumerator previous { get; private set; }

        public abstract bool Filter(Route route);
        
        public AbstractDecorator(INumerator prev)
        {
            previous = prev;
        }
        public Route Get()
        {
            if (previous == null) return null;

            do
            {
                if (previous.Get() == null) return null;
                if (Filter(previous.Get()))
                    return previous.Get();

                previous.Next();
            } while (previous.Get() != null);
            return null;
           
        }

        public bool HasNext()
        {
            return previous.HasNext();
        }

        public void Next()
        {
            previous.Next();
        }
    }
    public class RestaiurantFilter : AbstractDecorator
    {
        public bool Restaurant { get; set; }
        public RestaiurantFilter(INumerator prev, bool Restaurant = false) : base(prev)
        {
            this.Restaurant = Restaurant;
        }

        public override bool Filter(Route route)
        {
            return this.Restaurant == true ? route.To.HasRestaurant : true;
        }
    }
    public class PopulationFilter : AbstractDecorator
    {
        public int MinPopulation { get; set; }
        public PopulationFilter(INumerator prev, int population) : base(prev)
        {
            MinPopulation = population;
        }

        public override bool Filter(Route route)
        {
            return MinPopulation <= route.To.Population;
        }
    }
    public class VehicleFilter : AbstractDecorator
    {
        public ISet<VehicleType> vehicles;
        public VehicleFilter(INumerator prev, ISet<VehicleType> vehicles) : base(prev)
        {
            this.vehicles = vehicles;
        }

        public override bool Filter(Route route)
        {
            return vehicles.Contains(route.VehicleType);
        }
    }
}
