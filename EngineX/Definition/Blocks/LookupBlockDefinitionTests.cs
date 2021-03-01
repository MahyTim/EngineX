using System.Collections.Generic;
using EngineX.Runtime;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Frameworks;
using Xunit;

namespace EngineX.Definition.Blocks
{
    public class LookupBlockDefinitionTests
    {
        [Fact]
        public void PerformLookupOneValue()
        {
            var simpleBlock = new LookupBlockDefinition("simple");
            var a = simpleBlock.Input.AddNumeric("a");
            var d = simpleBlock.Input.AddNumeric("d");

            //------------------
            // | a | b | c | d |
            //------------------
            // | 1 | 2 | 3 | 4 |
            // | 5 | 6 | 7 | 8 |
            // | 3 | 9 | 9 | 1 |
            // | 1 | 5 | 5 | 4 |
            //------------------

            simpleBlock.Table.Columns.AddNumeric("a");
            var b = simpleBlock.Table.Columns.AddNumeric("b");
            var c = simpleBlock.Table.Columns.AddNumeric("c");
            simpleBlock.Table.Columns.AddNumeric("d");

            simpleBlock.Table.Rows.Add().AddValue(a, 1).AddValue(b, 2).AddValue(c, 3).AddValue(d, 4);
            simpleBlock.Table.Rows.Add().AddValue(a, 5).AddValue(b, 6).AddValue(c, 7).AddValue(d, 8);
            simpleBlock.Table.Rows.Add().AddValue(a, 3).AddValue(b, 9).AddValue(c, 9).AddValue(d, 1);
            simpleBlock.Table.Rows.Add().AddValue(a, 1).AddValue(b, 5).AddValue(c, 5).AddValue(d, 4);

            using (var selectFirstRow = new Calculation(simpleBlock))
            {
                selectFirstRow.Set(a, 1);
                selectFirstRow.Set(d, 4);

                Assert.Equal(1, (int) selectFirstRow.Get(a).Value);
                Assert.Equal(2, (int) selectFirstRow.Get(b).Value);
                Assert.Equal(3, (int) selectFirstRow.Get(c).Value);
                Assert.Equal(4, (int) selectFirstRow.Get(d).Value);
            }

            using (var selectThirdRow = new Calculation(simpleBlock))
            {
                simpleBlock.Table.SkipMissingInputParameterValues = true;
                selectThirdRow.Set(a, 3);

                Assert.Equal(3, (int) selectThirdRow.Get(a).Value);
                Assert.Equal(9, (int) selectThirdRow.Get(b).Value);
                Assert.Equal(9, (int) selectThirdRow.Get(c).Value);
                Assert.Equal(1, (int) selectThirdRow.Get(d).Value);
            }
        }
    }
}