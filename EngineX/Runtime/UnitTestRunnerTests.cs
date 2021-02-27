using System.Collections.Generic;
using System.Linq;
using EngineX.Definition;
using EngineX.Definition.Blocks;
using Xunit;

namespace EngineX.Runtime
{
    public class UnitTestRunnerTests
    {
        [Fact]
        public void RunTests()
        {
            var block = new SimpleMathBlockDefinition("some math");
            block.Expression = "a * b";
            block.Input.AddNumeric("a");
            block.Input.AddNumeric("b");
            block.Output.AddNumeric("c");
            
            block.UnitTests.Add(new UnitTestDefinition()
            {
                Description = "fails",
                Input = new List<Parameter>()
                {
                    new(ParameterName.For("a"), 10),
                    new(ParameterName.For("b"), 20)
                },
                ExpectedOutput = new List<Parameter>()
                {
                    new(ParameterName.For("c"), 300)
                }
            });
            block.UnitTests.Add(new UnitTestDefinition()
            {
                Description = "succeeds",
                Input = new List<Parameter>()
                {
                    new(ParameterName.For("a"), 10),
                    new(ParameterName.For("b"), 20)
                },
                ExpectedOutput = new List<Parameter>()
                {
                    new(ParameterName.For("c"), 200)
                }
            });

            var runner = new UnitTestRunner(block);
            var results = runner.Execute();
            Assert.Equal(UnitTestResultStatus.Failed, results.First().Status);
            Assert.Equal(UnitTestResultStatus.Success, results.Last().Status);
        }
    }
}
