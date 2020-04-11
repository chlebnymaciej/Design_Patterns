using Problems;

namespace Solvers
{
    interface ISolver
    {
        int? CanSolve(GPUProblem p);
        int? CanSolve(CPUProblem p);
        int? CanSolve(NetworkProblem p);
    }
}