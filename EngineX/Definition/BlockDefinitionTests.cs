using EngineX.Definition;
using EngineX.Runtime;
using Xunit;

namespace EngineX
{
    public class BlockDefinitionTests
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
                Type = new NumericParameterType()
            });
            definition.Input.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("b"),
                Type = new NumericParameterType()
            });
            definition.Output.Add(new ParameterDefinition()
            {
                Name = ParameterName.For("c"),
                Type = new NumericParameterType()
            });
            definition.DefaultValues.Add(new Parameter(ParameterName.For("a"),10));
            
            var instance = new Calculation(definition);
            instance.Set(new Parameter(ParameterName.For("b"), 20));

            Assert.Equal(30,(int) instance.Get(ParameterName.For("c")).Value);
            
        }
    }
}