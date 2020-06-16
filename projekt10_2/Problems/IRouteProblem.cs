//This file Can be modified

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Dodanie funkcji do poprawnego działania Visitora
using BigTask2.Algorithms;
using BigTask2.Api;
using BigTask2.Data;
using System.Collections.Generic;

namespace BigTask2.Interfaces
{
    interface IRouteProblem
	{
        IGraphDatabase Graph { get; set; }

        IEnumerable<Route> AcceptVisitor(IExtendedSolver solver);
	}
}
