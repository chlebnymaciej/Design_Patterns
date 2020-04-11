using System;
using Problems;

namespace Solvers
{
    class CPU : ISolver
    {
        private readonly string model;
        private readonly int threads;

        public CPU(string model, int threads)
        {
            this.model = model;
            this.threads = threads;
        }

        public int? CanSolve(GPUProblem p)
        {
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }

        public int? CanSolve(CPUProblem p)
        {
            if (p.RequiredThreads <= this.threads)
            {
                SolvingInfo.Solved(this.model, p.Name);
                return p.Computation();
            }
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }

        public int? CanSolve(NetworkProblem p)
        {
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }
    }
}