using System;
using Solvers;

namespace Problems
{
    class CPUProblem : Problem
    {
        public int RequiredThreads { get; }

        public CPUProblem(string name, Func<int> computation, int requiredThreads) : base(name, computation)
        {
            RequiredThreads = requiredThreads;
        }

        public override void CanItSolveMe(ISolver solver)
        {
            int? res = solver.CanSolve(this);
            if (res != null)
                TryMarkAsSolved(res);
        }
    }
}