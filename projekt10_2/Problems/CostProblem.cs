//This file Can be modified

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Dodanie funkcji do poprawnego działania Visitora
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace BigTask2.Problems
{
	class CostProblem : IRouteProblem
	{
		public string From, To;
		public CostProblem(string from, string to)
		{
			From = from;
			To = to;
		}

        public IGraphDatabase Graph { get; set; }

		public IEnumerable<Route> AcceptVisitor(IExtendedSolver solver)
		{
			if (Graph == null) return null;

			if (solver.CanSolve(this))
				return solver.solver.Solve(Graph, Graph.GetByName(From), Graph.GetByName(To));
			return null;
		}
	}
}
