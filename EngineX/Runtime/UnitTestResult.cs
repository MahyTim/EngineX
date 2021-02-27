using System.Collections.Generic;

namespace EngineX.Runtime
{
    public class UnitTestResult
    {
        public UnitTestResultStatus Status { get; set; }
        public List<FailedExpectation> FailedExpectations { get; set; }
        public string ErrorMessage { get; set; }

        public UnitTestResult()
        {
            FailedExpectations = new List<FailedExpectation>();
        }
    }
}