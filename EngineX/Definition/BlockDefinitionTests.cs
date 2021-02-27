using EngineX.Definition.Blocks;
using EngineX.Runtime;
using Xunit;

namespace EngineX.Definition
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
            var a = definition.Input.AddNumeric("a");
            var b = definition.Input.AddNumeric("b");
            var c = definition.Output.AddNumeric("c");

            definition.DefaultValues.Add(new Parameter(a, 10));

            var instance = new Calculation(definition);
            instance.Set(new Parameter(b, 20));

            Assert.Equal(30, (int) instance.Get(c).Value);
        }
    }
}