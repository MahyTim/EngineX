using System;
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
                    Type = new IntegerParameterType()
                });
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("y"),
                    Type = new IntegerParameterType()
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

            definition.Validate();

            var instance = new Calculation(definition);
            instance.Set(new ParameterValue(ParameterName.For("a"), 10));
            instance.Set(new ParameterValue(ParameterName.For("b"), 20));

            Assert.Equal(30, instance.Get(ParameterName.For("c")).Value);
        }
    }
}