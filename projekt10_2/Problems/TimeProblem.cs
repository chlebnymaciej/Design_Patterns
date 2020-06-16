//This file Can be modified


//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Dodanie funkcji do poprawnego działania Visitora
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using System.Collections.Generic;

namespace BigTask2.Problems
{
	class TimeProblem : IRouteProblem
	{
        public IGraphDatabase Graph { get; set; }
        public string From, To;
		public TimeProblem(string from, string to)
		{
			From = from;
			To = to;
		}

		public IEnumerable<Route> AcceptVisitor(IExtendedSolver solver)
		{
			if (Graph == null) return null;

			if (solver.CanSolve(this))
				return solver.solver.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			return null;
		}
	}
}
