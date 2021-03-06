﻿using System.IO;
using EngineX.Runtime;
using Xunit;

namespace EngineX.Definition.Blocks
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
                
                blockDefinition.Input.AddNumeric("input1");
                blockDefinition.Input.AddNumeric("input2");
                blockDefinition.Output.AddNumeric("output1");
                
                using (var calc = new Calculation(blockDefinition))
                {
                    calc.Set(new Parameter(ParameterName.For("input1"), 10));
                    calc.Set(new Parameter(ParameterName.For("input2"), 20));

                    var actual = calc.Get(ParameterName.For("output1"));
                    Assert.Equal(30, (int) actual.Value);
                }
            }
        }
    }
}