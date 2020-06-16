//This file contains fragments that You have to fulfill


//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja dekoratorów filtrujących bazy danych 
using BigTask2.Api;
using System.Collections.Generic;

namespace BigTask2.Data
{
    public interface IGraphDatabase
    {
        //Fill the return type of the method below
        INumerator GetRoutesFrom(City from);
        City GetByName(string cityName);
    }

    public interface IGraphDatabaseDecorator : IGraphDatabase
    {
        IGraphDatabase previous { get; }
    }

    abstract class AGraphDatabaseDecorator : IGraphDatabaseDecorator
    {
        public IGraphDatabase previous { get; private set; }

        public AGraphDatabaseDecorator(IGraphDatabase prev)
        {
            previous = prev;
        }
        public City GetByName(string cityName)
        {
            if (previous == null) 
                return null;
            return previous.GetByName(cityName);
        }

        public abstract INumerator GetRoutesFrom(City from);
    }

    class RestaurantFilterDatabaseDecorator : AGraphDatabaseDecorator
    {
        public RestaurantFilterDatabaseDecorator(IGraphDatabase prev) : base(prev)
        {
        }

        public override INumerator GetRoutesFrom(City from)
        {
            if (previous == null || previous.GetRoutesFrom(from) == null)
                return null;
            return new RestaiurantFilter(previous.GetRoutesFrom(from), true);
        }
    }

    class AllowedVehiclesFilterDatabaseDecorator : AGraphDatabaseDecorator
    {
        private ISet<VehicleType> set; 
        public AllowedVehiclesFilterDatabaseDecorator(IGraphDatabase prev, ISet<VehicleType> set) : base(prev)
        {
            this.set = set;
        }

        public override INumerator GetRoutesFrom(City from)
        {
            if (previous == null || previous.GetRoutesFrom(from) == null)
                return null;
            return new VehicleFilter(previous.GetRoutesFrom(from), set);
        }
    }

    class MinPopulationFilterDatabaseDecorator : AGraphDatabaseDecorator
    {
        private int minPopulation = 0;
        public MinPopulationFilterDatabaseDecorator(IGraphDatabase prev, int minPopulation = 0) : base(prev)
        {
            this.minPopulation = minPopulation;
        }

        public override INumerator GetRoutesFrom(City from)
        {
            if (previous ==null || previous.GetRoutesFrom(from) == null)
                return null;
            return new PopulationFilter(previous.GetRoutesFrom(from), minPopulation);
        }
    }
}
