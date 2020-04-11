using System;
using Problems;

namespace Solvers
{
    class GPU : ISolver
    {
        static private int MaxTemperature { get; } = 100;
        static private int CPUProblemTemperatureMultiplier { get; } = 3;

        private readonly string model;
        private int temperature;
        private int coolingFactor;

        public GPU(string model, int temperature, int coolingFactor)
        {
            this.model = model;
            this.temperature = temperature;
            this.coolingFactor = coolingFactor;
        }
        private bool DidThermalThrottle()
        {
            if (temperature > MaxTemperature)
            {
                Console.WriteLine($"GPU {model} thermal throttled");
                CoolDown();
                return true;
            }

            return false;
        }

        private void CoolDown()
        {
            temperature -= coolingFactor;
        }

        public int? CanSolve(GPUProblem p)
        {
            if(DidThermalThrottle())
            {
                SolvingInfo.NotSolved(this.model, p.Name);
                return null;
            }
            SolvingInfo.Solved(this.model, p.Name);

            this.temperature += p.GpuTemperatureIncrease;
            return p.Computation();
        }

        public int? CanSolve(CPUProblem p)
        {
            if (DidThermalThrottle())
            {
                SolvingInfo.NotSolved(this.model, p.Name);
                return null;
            }
            SolvingInfo.Solved(this.model, p.Name);
            this.temperature += CPUProblemTemperatureMultiplier * p.RequiredThreads;
            return p.Computation();
        }
        public int? CanSolve(NetworkProblem p )
        {
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }
    }
}