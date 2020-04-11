using System;
using Solvers;

namespace Problems
{
    class GPUProblem : Problem
    {
        public int GpuTemperatureIncrease { get; }

        public GPUProblem(string name, Func<int> computation, int gpuTemperatureIncrease) : base(name, computation)
        {
            GpuTemperatureIncrease = gpuTemperatureIncrease;
        }

        public override void CanItSolveMe(ISolver solver)
        {
            int? res = solver.CanSolve(this);
            if (res != null)
                TryMarkAsSolved(res);
        }
    }
}