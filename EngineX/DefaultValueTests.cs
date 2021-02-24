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
                Name = ParameterName.Get("a"),
                Type = new IntegerParameterType()
            });
            definition.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("b"),
                Type = new IntegerParameterType()
            });
            definition.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.Get("c"),
                Type = new IntegerParameterType()
            });
            definition.DefaultValues.Add(new ParameterValue(ParameterName.Get("a"),10));
            
            var instance = new Calculation(definition);
            instance.Set(new ParameterValue(ParameterName.Get("b"), 20));

            Assert.Equal(30, instance.Get(ParameterName.Get("c")).Value);
            
        }
    }
}