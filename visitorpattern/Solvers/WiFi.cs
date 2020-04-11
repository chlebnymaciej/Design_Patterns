using System;
using Problems;

namespace Solvers
{
    class WiFi : NetworkDevice
    {
        private readonly double packetLossChance;
        private static readonly Random rng = new Random(1597);

        public WiFi(string model, int dataLimit, double packetLossChance) : base(model, dataLimit)
        {
            DeviceType = "WiFi";
            this.packetLossChance = packetLossChance;
        }

        public override int? CanSolve(NetworkProblem p)
        {
            int? result;
            if (rng.NextDouble() < packetLossChance && (result = base.CanSolve(p)) != null)
            {
                SolvingInfo.Solved(this.model, p.Name);
                return result;
            }

            SolvingInfo.NotSolved(this.model, p.Name);
            return null;
        }
    }
}