using Xunit;

namespace EngineX
{
    public class DefaultValuesTests
    {
        [Fact]
        public void DefaultValueIsApplied()
        {
            var definition = new SimpleMathBlockDefinition("first")
            {
                Expression = "a + b"
            };
            definition.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("a"),
                Type = new IntegerParameterType()
            });
            definition.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("b"),
                Type = new IntegerParameterType()
            });
            definition.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("c"),
                Type = new IntegerParameterType()
            });
            definition.DefaultValues.Add(new ParameterValue(ParameterName.For("a"),10));
            
            var instance = new Calculation(definition);
            instance.Set(new ParameterValue(ParameterName.For("b"), 20));

            Assert.Equal(30, instance.Get(ParameterName.For("c")).Value);
            
        }
    }
}