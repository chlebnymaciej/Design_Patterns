using BigTask2.Api;
using System;
using System.Collections.Generic;
using System.Text;


//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Implementacja chain of responsibility walidacji poprawności requestu
namespace BigTask2.Ui
{
    interface IChain
    {
        bool Validate(Request request);
        IChain AddNext(IChain chain);
        bool Handle(Request request);
    }

    abstract class AChain : IChain
    {
        private IChain next { get; set; }
        public IChain AddNext(IChain chain)
        {
            this.next = chain;
            return this.next;
        }

        public bool Handle(Request request)
        {
            if (!Validate(request))
                return false;
            if (next == null)
                return true;

            return next.Handle(request);
        }

        public abstract bool Validate(Request request);

    }

    class FromValidate : AChain
    {
        public override bool Validate(Request request)
        {
            return !(request.From == string.Empty);
        }
    }

    class ToValidate : AChain
    {
        public override bool Validate(Request request)
        {
            return !(request.To == string.Empty);
        }
    }

    class FilterPopulationvalidate : AChain
    {
        public override bool Validate(Request request)
        {
            return request.Filter.MinPopulation > -1;
        }
    }

    class Vehiclesvalidate : AChain
    {
        public override bool Validate(Request request)
        {
            return request.Filter.AllowedVehicles.Count > 0;
        }
    }
}
