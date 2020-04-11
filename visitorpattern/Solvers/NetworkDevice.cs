using System;
using Problems;

namespace Solvers
{
    abstract class NetworkDevice : ISolver
    {
        protected string DeviceType { get; set; } = "NetworkDevice";

        protected readonly string model;
        private int dataLimit;

        protected NetworkDevice(string model, int dataLimit)
        {
            this.model = model;
            this.dataLimit = dataLimit;
        }

        public int? CanSolve(GPUProblem p)
        {
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }

        public int? CanSolve(CPUProblem p)
        {
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }
        virtual public int? CanSolve(NetworkProblem p)
        {
            if( p.DataToTransfer < this.dataLimit)
            {
                dataLimit -= p.DataToTransfer;
                return p.Computation();
            }

            return null;
        }
        
    }
}