using System;
using EngineX.Runtime;
using Xunit;

namespace EngineX.Definition.Blocks
{
    public class SimpleMathBlockDefinitionTests
    {
        [Fact]
        public void SimpleMathBlockDefinition_Invalid_Output()
        {
            Assert.Throws<Exception>(() =>
            {
                var definition = new SimpleMathBlockDefinition("my calculator")
                {
                    Expression = "a + b"
                };
                definition.Output.AddNumeric("x");
                definition.Output.AddNumeric("y");
                definition.Validate();
            });
        }

        [Fact]
        public void SimpleMathBlockDefinition_No_Expression()
        {
            Assert.Throws<Exception>(() =>
            {
                var definition = new SimpleMathBlockDefinition("my calculator")
                {
                    Expression = ""
                };
                definition.Validate();
            });
        }

        [Fact]
        public void SimpleMathBlockDefinition_Correct()
        {
            var definition = new SimpleMathBlockDefinition("my calculator")
            {
                Expression = "a + b"
            };
            definition.Input.AddNumeric("a");
            definition.Input.AddNumeric("b");
            definition.Output.AddNumeric("c");

            definition.Validate();

            var instance = new Calculation(definition);
            instance.Set(new Parameter(ParameterName.For("a"), 10));
            instance.Set(new Parameter(ParameterName.For("b"), 20));

            Assert.Equal(30,(int) instance.Get(ParameterName.For("c")).Value);
        }
    }
}