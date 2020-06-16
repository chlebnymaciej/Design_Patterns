using BigTask2.Api;
using BigTask2.Data;
using System;
using System.Collections.Generic;
using System.Text;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Tworzenie chaina of responsibility dekorującego bazę danych filtrami

namespace projekt10_2.Data
{
    interface IFilterDBChain
    {
        IFilterDBChain AddNext(IFilterDBChain filterDBChain);
        IGraphDatabase Handle(Filter filter, IGraphDatabase previous = null);
        IGraphDatabase UpdatedFilter(Filter filter, IGraphDatabase previous);
    }

    abstract class AFilterDBChain : IFilterDBChain
    {
        protected IFilterDBChain next;
        public IFilterDBChain AddNext(IFilterDBChain filterDBChain)
        {
            this.next = filterDBChain;
            return this.next;
        }

        public IGraphDatabase Handle(Filter filter, IGraphDatabase previous = null)
        {
            
            var tmp = UpdatedFilter(filter, previous);
            if (next == null)
                return tmp;
            
            return next.Handle(filter, tmp);

        }

        public abstract IGraphDatabase UpdatedFilter(Filter filter, IGraphDatabase previous);
    }

    class RestaurantDBChainDecorator : AFilterDBChain
    {
        public override IGraphDatabase UpdatedFilter(Filter filter, IGraphDatabase previous)
        {
            if (filter.RestaurantRequired)
                return new RestaurantFilterDatabaseDecorator(previous);
            return previous;
        }
    }

    class AllowedVehiclesDBChainDecorator : AFilterDBChain
    {
        public override IGraphDatabase UpdatedFilter(Filter filter, IGraphDatabase previous)
        {
            if (filter.AllowedVehicles != null)
                return new AllowedVehiclesFilterDatabaseDecorator(previous, filter.AllowedVehicles);
            return previous;
        }
    }

    class MinPopulationDBChainDecorator : AFilterDBChain
    {
        public override IGraphDatabase UpdatedFilter(Filter filter, IGraphDatabase previous)
        {
            if (filter.MinPopulation >= 0)
                return new MinPopulationFilterDatabaseDecorator(previous, filter.MinPopulation);
            return previous;
        }
    }
}
