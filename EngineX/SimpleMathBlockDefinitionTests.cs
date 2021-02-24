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
                var definition = new SimpleMathBlockDefinition()
                {
                    Expression = "a + b"
                };
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.Get("x"),
                    Type = new IntegerParameterType()
                });
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.Get("y"),
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
                var definition = new SimpleMathBlockDefinition()
                {
                    Expression = ""
                };
                definition.Validate();
            });
        }

        [Fact]
        public void SimpleMathBlockDefinition_Correct()
        {
            var definition = new SimpleMathBlockDefinition()
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

            definition.Validate();

            var instance = new Calculation(definition);
            instance.Set(new ParameterValue(ParameterName.Get("a"), 10));
            instance.Set(new ParameterValue(ParameterName.Get("b"), 20));

            Assert.Equal(30, instance.Get(ParameterName.Get("c")).value);
        }
    }
}