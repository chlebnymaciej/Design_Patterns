using System;
using Problems;

namespace Solvers
{
    class Ethernet : NetworkDevice
    {
        public Ethernet(string model, int dataLimit) : base(model, dataLimit)
        {
            DeviceType = "Ethernet";
        }
        public override int? CanSolve(NetworkProblem p)
        {
            int? result;
            if((result=base.CanSolve(p))!=null)
            {
                SolvingInfo.Solved(this.model, p.Name);
                return result;
            }
            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }
    }
}