using Xunit;

namespace EngineX
{
    public class ConstantsBlockDefinitionTests
    {
        [Fact]
        public void Runs_In_Composite()
        {
            var constants = new ConstantsBlockDefinition("constants");
            constants.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("aConstant"),
                Type = new IntegerParameterType()
            });
            constants.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("bConstant"),
                Type = new IntegerParameterType()
            });
            constants.Constants.Add(new ParameterValue(ParameterName.For("aConstant"), 10));
            constants.Constants.Add(new ParameterValue(ParameterName.For("bConstant"), 20));

            var simpleMath = new SimpleMathBlockDefinition("calc");
            simpleMath.Expression = "a + b";
            simpleMath.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("a"),
                Type = new IntegerParameterType()
            });
            simpleMath.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("b"),
                Type = new IntegerParameterType()
            });
            simpleMath.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("c"),
                Type = new IntegerParameterType()
            });

            var composite = new CompositeBlockDefinition("composite");
            composite.Blocks.Add(constants);
            composite.Blocks.Add(simpleMath);
            composite.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("result"),
                Type = new IntegerParameterType()
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(constants, ParameterName.For("aConstant")),
                To = new ParameterWire.Endpoint(simpleMath, ParameterName.For("a"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(constants, ParameterName.For("bConstant")),
                To = new ParameterWire.Endpoint(simpleMath, ParameterName.For("b"))
            });
            composite.Wires.Add(new ParameterWire()
            {
                From = new ParameterWire.Endpoint(simpleMath, ParameterName.For("c")),
                To = new ParameterWire.Endpoint(composite, ParameterName.For("result"))
            });

            var calc = new Calculation(composite);

            var actual = calc.Get(ParameterName.For("result")).Value;
            Assert.Equal(30, actual);
        }
    }
}