using System;
using System.Linq;
using Xunit;

namespace EngineX
{
    public class SimpleMathBlockDefinitionTests
    {
        [Fact]
        public void SimpleMathBlockDefinition_Invalid_Output()
        {
            Assert.Throws<Exception>(()=>
            {
                var definition = new SimpleMathBlockDefinition()
                {
                    Expression = "a + b"
                };
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = new ParameterName("x"),
                    Type = new IntegerParameterType()
                });
                definition.Output.Add(new ParameterDefinition()
                {
                    Name = new ParameterName("y"),
                    Type = new IntegerParameterType()
                });
                definition.Validate();
            });
        }
        [Fact]
        public void SimpleMathBlockDefinition_No_Expression()
        {
            Assert.Throws<Exception>(()=>
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
                Name = new ParameterName("a"),
                Type = new IntegerParameterType()
            });
            definition.Input.Add(new ParameterDefinition()
            {
                Name = new ParameterName("b"),
                Type = new IntegerParameterType()
            });
            definition.Output.Add(new ParameterDefinition()
            {
                Name = new ParameterName("c"),
                Type = new IntegerParameterType()
            });

            definition.Validate();
            
            var instance = new Calculation(definition);
            instance.Set(new ParameterValue(new ParameterName("a"), 10));
            instance.Set(new ParameterValue(new ParameterName("b"), 20));

            Assert.Equal(30, instance.Get(new ParameterName("c")).value);
        }
    }
}