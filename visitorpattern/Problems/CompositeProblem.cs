using System;
using System.Collections.Generic;
using System.Linq;
using ResultsCombiners;
using Solvers;

namespace Problems
{
    class CompositeProblem : Problem
    {
        private readonly IEnumerable<Problem> problems;
        private readonly IResultsCombiner resultsCombiner;
        // lista wyników poszczególnych problemów
        private List<int> results = new List<int>();
        // licznik czy wszystkie problemy zosta³y rozwi¹zane
        private int sumSolved = 0;
        public CompositeProblem(string name, IEnumerable<Problem> problems,
            IResultsCombiner resultsCombiner) : base(name, () => 0)
        {
            this.problems = problems;
            this.resultsCombiner = resultsCombiner;
        }

        public override void CanItSolveMe(ISolver solver)
        {
            foreach(Problem p in problems)
            {
                if (p.Solved)
                {
                    continue;
                }

                p.CanItSolveMe(solver);
                if(p.Solved)
                {
                    sumSolved++;
                    results.Add(p.Result.GetValueOrDefault());
                }
            }

            if (sumSolved == problems.Count())
            {
                TryMarkAsSolved(resultsCombiner.CombineResults(results));
            }
        }
    }
}