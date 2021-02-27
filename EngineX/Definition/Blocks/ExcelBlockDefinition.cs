using System.Collections.Generic;
using System.IO;
using System.Linq;
using EngineX.Runtime;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing;
using Xunit;

namespace EngineX.Definition.Blocks
{
    public class ExcelBlockDefinition : BlockDefinition
    {
        public byte[] ExcelWorkbook { get; set; }
        public Dictionary<string, ParameterName> CellMappings = new();

        public ExcelBlockDefinition(string description) : base(description)
        {
        }

        public override bool IsDeterministic => false;


        protected override void InnerValidate()
        {
        }

        protected override void InnerExecute(Calculation calculation)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var ms = new MemoryStream(ExcelWorkbook))
            {
                Assert.True(ms.CanRead);
                Assert.True(ms.CanWrite);
                Assert.True(ms.CanSeek);

                var book = new ExcelPackage(ms).Workbook;
                var worksheet = book.Worksheets[0];

                foreach (var cellMapping in CellMappings)
                {
                    var input = Input.FirstOrDefault(z => z.Name == cellMapping.Value);
                    if (input != null)
                    {
                        var value = calculation.Get(input.Name).Value.ValueForCalculation;
                        worksheet.Cells[cellMapping.Key].Value = value;
                    }
                }

                worksheet.Calculate(new ExcelCalculationOption());

                foreach (var output in Output)
                {
                    var cellMapping = CellMappings.FirstOrDefault(z => z.Value == output.Name);
                    if (cellMapping.Key != null)
                    {
                        calculation.State.Set(new Parameter(output.Name,
                            new ParameterValue(worksheet.Cells[cellMapping.Key].Value)));
                    }
                }
            }
        }
    }
}