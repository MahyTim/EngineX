using System;
using System.Collections.Generic;
using EngineX.Definition;

namespace EngineX.Runtime
{
    public class UnitTestRunner
    {
        private readonly BlockDefinition _definition;

        public UnitTestRunner(BlockDefinition definition)
        {
            _definition = definition;
        }

        public UnitTestResult[] Execute()
        {
            var results = new List<UnitTestResult>();
            foreach (var unitTest in _definition.UnitTests)
            {
                var result = Execute(unitTest);
                results.Add(result);
            }

            return results.ToArray();
        }

        private UnitTestResult Execute(UnitTestDefinition testDefinition)
        {
            UnitTestResult result = new() {Status = UnitTestResultStatus.Success};
            try
            {
                using (var calc = new Calculation(_definition))
                {
                    foreach (var input in testDefinition.Input)
                    {
                        calc.Set(input with { });
                    }

                    foreach (var expectedOutput in testDefinition.ExpectedOutput)
                    {
                        var actual = calc.Get(expectedOutput.Name);
                        if (!expectedOutput.Equals(actual))
                        {
                            result.Status = UnitTestResultStatus.Failed;
                            result.FailedExpectations.Add(new FailedExpectation()
                            {
                                Actual = actual,
                                Expected = expectedOutput
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = UnitTestResultStatus.Error;
                result.ErrorMessage = ex.ToString();
            }

            return result;
        }
    }
}