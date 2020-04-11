using System;
using Solvers;

namespace Problems
{
    class NetworkProblem : Problem
    {
        public int DataToTransfer { get; }

        public NetworkProblem(string name, Func<int> computation, int dataToTransfer) : base(name, computation)
        {
            DataToTransfer = dataToTransfer;
        }

        public override void CanItSolveMe(ISolver solver)
        {
            int? res = solver.CanSolve(this);
            if (res != null)
                TryMarkAsSolved(res);
        }
    }
}