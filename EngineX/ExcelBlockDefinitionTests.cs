using System;
using System.IO;
using Xunit;

namespace EngineX
{
    public class ExcelBlockDefinitionTests
    {
        [Fact]
        public void Calculate_Formula_In_Excel()
        {
            using (var ms = new MemoryStream())
            {
                GetType().Assembly.GetManifestResourceStream("EngineX.ExcelBlockDefinitionTests_Formula.xlsx")
                    .CopyTo(ms);

                var blockDefinition = new ExcelBlockDefinition("my excel");
                blockDefinition.ExcelWorkbook = ms.ToArray();

                blockDefinition.CellMappings["A1"] = ParameterName.For("input1");
                blockDefinition.CellMappings["A2"] = ParameterName.For("input2");
                blockDefinition.CellMappings["B3"] = ParameterName.For("output1");
                blockDefinition.Input.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("input1"),
                    Type = new IntegerParameterType()
                });
                blockDefinition.Input.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("input2"),
                    Type = new IntegerParameterType()
                });
                blockDefinition.Output.Add(new ParameterDefinition()
                {
                    Name = ParameterName.For("output1"),
                    Type = new IntegerParameterType()
                });

                using (var calc = new Calculation(blockDefinition))
                {
                    calc.Set(new ParameterValue(ParameterName.For("input1"), 10));
                    calc.Set(new ParameterValue(ParameterName.For("input2"), 20));

                    var actual = calc.Get(ParameterName.For("output1"));
                    Assert.Equal(30, Convert.ToInt32(actual.Value));
                }
            }
        }
    }
}