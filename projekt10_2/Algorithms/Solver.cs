using BigTask2.Api;
using BigTask2.Data;
using BigTask2.Interfaces;
using BigTask2.Problems;
using System;
using System.Collections.Generic;
using System.Text;

//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  Maciej Chlebny

// Wspomniany wcześniej interfejs ISolver oraz jego ewentualne zastąpienie Interfejs ISolverExtender,
// implementujący działanie Visitora


namespace BigTask2.Algorithms
{
    interface ISolver
    {
        IEnumerable<Route> Solve(IGraphDatabase graph, City from, City to);
    }

    interface IExtendedSolver
    {
        ISolver solver { get; }
        string Name { get; }
        bool IsNameEquals(string name);
        bool CanSolve(CostProblem problem);
        bool CanSolve(TimeProblem problem);
    }
    class DijkstraCostSolverExtended : IExtendedSolver
    {
        public ISolver solver { get; private set; }

        public string Name { get; private set; }

        public DijkstraCostSolverExtended()
        {
            this.solver = new DijkstraCost();
            this.Name = "Dijkstra";
        }
        public bool IsNameEquals(string name)
        {
            return name == Name;
        }

        public bool CanSolve(CostProblem problem)
        {
            return true;
        }

        public bool CanSolve(TimeProblem problem)
        {
            return false;
        }
    }

    class DijkstraTimeSolverExtended : IExtendedSolver
    {
        public ISolver solver { get; private set; }

        public string Name { get; private set; }

        public DijkstraTimeSolverExtended()
        {
            this.solver = new DijkstraTime();
            this.Name = "Dijkstra";
        }
        public bool IsNameEquals(string name)
        {
            return name == Name;
        }

        public bool CanSolve(CostProblem problem)
        {
            return false;
        }

        public bool CanSolve(TimeProblem problem)
        {
            return true;
        }
    }

    class BFSSolverExtended : IExtendedSolver
    {
        public ISolver solver { get; private set; }

        public string Name { get; private set; }

        public BFSSolverExtended()
        {
            this.solver = new BFS();
            this.Name = "BFS";
        }
        public bool IsNameEquals(string name)
        {
            return name == Name;
        }

        public bool CanSolve(CostProblem problem)
        {
            return true;
        }

        public bool CanSolve(TimeProblem problem)
        {
            return true;
        }
    }

    class DFSSolverExtended : IExtendedSolver
    {
        public ISolver solver { get; private set; }

        public string Name { get; private set; }

        public DFSSolverExtended()
        {
            this.solver = new DFS();
            this.Name = "DFS";
        }
        public bool IsNameEquals(string name)
        {
            return name == Name;
        }

        public bool CanSolve(CostProblem problem)
        {
            return true;
        }

        public bool CanSolve(TimeProblem problem)
        {
            return true;
        }
    }
}
