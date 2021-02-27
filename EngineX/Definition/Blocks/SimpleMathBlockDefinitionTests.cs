using System;
using EngineX.Definition;
using EngineX.Runtime;
using Xunit;

namespace EngineX
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
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("x"),
                    Type = new NumericParameterType()
                });
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("y"),
                    Type = new NumericParameterType()
                });
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

            definition.Validate();

            var instance = new Calculation(definition);
            instance.Set(new Parameter(ParameterName.For("a"), 10));
            instance.Set(new Parameter(ParameterName.For("b"), 20));

            Assert.Equal(30,(int) instance.Get(ParameterName.For("c")).Value);
        }
    }
}