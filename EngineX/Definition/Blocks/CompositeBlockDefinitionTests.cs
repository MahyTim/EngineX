using EngineX.Runtime;
using Xunit;

namespace EngineX.Definition.Blocks
{
    public class CompositeBlockDefinitionTests
    {
        [Fact]
        public void Two_SimpleMathBlocks_Sequential_Correct()
        {
            var firstBlock = new SimpleMathBlockDefinition("first")
            {
                Expression = "a + b"
            };
            firstBlock.Input.AddNumeric("a");
            firstBlock.Input.AddNumeric("b");
            firstBlock.Output.AddNumeric("c");

            var secondBlock = new SimpleMathBlockDefinition("second")
            {
                Expression = "c * c"
            };
            secondBlock.Input.AddNumeric("a");
            secondBlock.Input.AddNumeric("c");
            secondBlock.Output.AddNumeric("r");

            var composite = new CompositeBlockDefinition("Composite");
            composite.Blocks.Add(firstBlock);
            composite.Blocks.Add(secondBlock);
            composite.Input.AddNumeric("input1");
            composite.Output.AddNumeric("result");
           
            composite.Validate();

            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(secondBlock, ParameterName.For("r")),
                To = new ParameterWire.Endpoint(composite, ParameterName.For("result"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(composite, ParameterName.For("input1")),
                To = new ParameterWire.Endpoint(firstBlock, ParameterName.For("a"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(composite, ParameterName.For("input1")),
                To = new ParameterWire.Endpoint(firstBlock, ParameterName.For("b"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(firstBlock, ParameterName.For("c")),
                To = new ParameterWire.Endpoint(secondBlock, ParameterName.For("c"))
            });

            composite.Validate();

            // Sequential
            composite.ExecutionOrder = ExecutionOrder.Sequential;
            using (var calculation = new Calculation(composite))
            {
                calculation.Set(new Parameter(ParameterName.For("input1"), 5));
                Assert.False(calculation.IsCalculated);

                var actual = calculation.Get(ParameterName.For("result")).Value;
                Assert.Equal(100, (int) actual);

                Assert.True(calculation.IsCalculated);
            }
        }
    }
}