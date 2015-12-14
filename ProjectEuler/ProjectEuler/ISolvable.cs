using System.Text;

namespace ProjectEuler
{
    internal interface ISolvable
    {
        long? SolutionValue { get; }

        long Solve();
        StringBuilder SolutionOutput();
    }

    abstract class Problem
    {
        protected int ProblemNumber;

        protected string OutputTemplate;

        protected long InputParam;

        private long? _solutionValue;

        public long? SolutionValue
        {
            get { return _solutionValue ?? (_solutionValue = Solve()); }
        }

        protected abstract long Solve();

        public StringBuilder SolutionOutput()
        {
            StringBuilder result = new StringBuilder();
            return Utility.GenerateOutput(ProblemNumber, result.AppendFormat(OutputTemplate, InputParam, SolutionValue));
        }
    }
}
