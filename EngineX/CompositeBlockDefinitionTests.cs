using Xunit;

namespace EngineX
{
    public class CompositeBlockDefinitionTests
    {
        [Fact]
        public void Two_SimpleMathBlocks_Correct()
        {
            var firstBlock = new SimpleMathBlockDefinition()
            {
                Expression = "a + b"
            };
            firstBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("a"),
                Type = new IntegerParameterType()
            });
            firstBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("b"),
                Type = new IntegerParameterType()
            });
            firstBlock.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("c"),
                Type = new IntegerParameterType()
            });

            var secondBlock = new SimpleMathBlockDefinition()
            {
                Expression = "c * c"
            };
            secondBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("a"),
                Type = new IntegerParameterType()
            });
            secondBlock.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("c"),
                Type = new IntegerParameterType()
            });
            secondBlock.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("r"),
                Type = new IntegerParameterType()
            });

            var composite = new CompositeBlockDefinition();
            composite.Blocks.Add(firstBlock);
            composite.Blocks.Add(secondBlock);
            composite.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("input1"),
                Type = new IntegerParameterType()
            });
            composite.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("result"),
                Type = new IntegerParameterType()
            });
            composite.Validate();
            
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(secondBlock,ParameterName.Get("r")),
                To = new ParameterWire.Endpoint(composite,ParameterName.Get("result"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(composite,ParameterName.Get("input1")),
                To = new ParameterWire.Endpoint(secondBlock,ParameterName.Get("a"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(composite,ParameterName.Get("input1")),
                To = new ParameterWire.Endpoint(secondBlock,ParameterName.Get("b"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(firstBlock,ParameterName.Get("c")),
                To = new ParameterWire.Endpoint(secondBlock,ParameterName.Get("c"))
            });

            composite.Validate();

            using (var calculation = new Calculation(composite))
            {
                calculation.Set(new ParameterValue(ParameterName.Get("input1"), 5));
                Assert.False(calculation.IsCalculated);

                Assert.Equal(100, calculation.Get(ParameterName.Get("result")).value);

                Assert.True(calculation.IsCalculated);
            }
        }
    }
}