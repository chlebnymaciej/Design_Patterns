using BigTask2.Api;
using BigTask2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Stworzenie chain of responsibility tworzący nowy problem
// Mimo że jest powiedziane w treści zadania że jest ono nie rozszerzalne ze względu na problemy
// To odnosząc się do dyskusji podjętej na kanale discord dodałem ten oto chain

namespace BigTask2.Problems
{
    interface IChainCreatingProblem
    {
        IChainCreatingProblem AddNext(IChainCreatingProblem chain);
        IRouteProblem Handle(Request request);
        IRouteProblem SelfProblemCreating(Request request);
        
    }

    abstract class AChainCreatingProblem : IChainCreatingProblem
    {
        protected IChainCreatingProblem next;
        public IChainCreatingProblem AddNext(IChainCreatingProblem chain)
        {
            this.next = chain;
            return this.next;
        }

        public IRouteProblem Handle(Request request)
        {
            var obj = this.SelfProblemCreating(request);
            
            if (obj != null)
                return obj;

            if (next == null)
                return null;

            return next.Handle(request);
        }

        public abstract IRouteProblem SelfProblemCreating(Request request);
    }

    class CostCreateProblem : AChainCreatingProblem
    {
        public override IRouteProblem SelfProblemCreating(Request request)
        {
            if (request.Problem == "Cost")
                return new CostProblem(request.From, request.To);
            return null;
        }
    }

    class TimeCreateProblem : AChainCreatingProblem
    {
        public override IRouteProblem SelfProblemCreating(Request request)
        {
            if (request.Problem == "Time")
                return new TimeProblem(request.From, request.To);
            return null;
        }
    }

}
