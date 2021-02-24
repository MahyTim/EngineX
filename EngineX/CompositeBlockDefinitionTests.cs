using Xunit;

namespace EngineX
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
            firstBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("a"),
                Type = new IntegerParameterType()
            });
            firstBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("b"),
                Type = new IntegerParameterType()
            });
            firstBlock.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("c"),
                Type = new IntegerParameterType()
            });

            var secondBlock = new SimpleMathBlockDefinition("second")
            {
                Expression = "c * c"
            };
            secondBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("a"),
                Type = new IntegerParameterType()
            });
            secondBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("c"),
                Type = new IntegerParameterType()
            });
            secondBlock.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("r"),
                Type = new IntegerParameterType()
            });

            var composite = new CompositeBlockDefinition("Composite");
            composite.Blocks.Add(firstBlock);
            composite.Blocks.Add(secondBlock);
            composite.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("input1"),
                Type = new IntegerParameterType()
            });
            composite.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("result"),
                Type = new IntegerParameterType()
            });
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
                calculation.Set(new ParameterValue(ParameterName.For("input1"), 5));
                Assert.False(calculation.IsCalculated);

                var actual = calculation.Get(ParameterName.For("result")).Value;
                Assert.Equal(100, actual);

                Assert.True(calculation.IsCalculated);
            }
        }
    }
}