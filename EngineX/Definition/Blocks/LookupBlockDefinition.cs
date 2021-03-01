using System.Collections.Generic;
using System.Linq;
using EngineX.Runtime;

namespace EngineX.Definition.Blocks
{
    public class LookupBlockDefinition : BlockDefinition
    {
        public class LookupTable
        {
            public List<ParameterDefinition> Columns = new();
            public List<LookupTableRow> Rows = new();
            public bool SkipMissingInputParameterValues;
        }

        public class LookupTableRow
        {
            public List<Parameter> Values = new();
        }

        public LookupBlockDefinition(string description) : base(description)
        {
        }

        public LookupTable Table = new();

        public override List<ParameterDefinition> Output => Table.Columns.ToList();

        public override bool IsDeterministic => false;

        protected override void InnerValidate()
        {
            throw new System.NotImplementedException();
        }

        protected override void InnerExecute(Calculation calculation)
        {
            foreach (var row in Table.Rows)
            {
                bool rowMatches = true;
                foreach (var input in Input)
                {
                    var valueFromInput = calculation.Get(input.Name);
                    var valueFromTable = row.Values.First(z => z.Name == input.Name);
                    if (Table.SkipMissingInputParameterValues)
                    {
                        if (valueFromInput != null && valueFromInput.Value != valueFromTable.Value)
                        {
                            rowMatches = false;
                            break;
                        }    
                    }
                    else
                    {
                        if (valueFromInput?.Value != valueFromTable.Value)
                        {
                            rowMatches = false;
                            break;
                        }
                    }
                    
                }

                if (rowMatches)
                {
                    foreach (var output in Output)
                    {
                        var valueFromTable = row.Values.First(z => z.Name == output.Name);
                        calculation.Set(valueFromTable with
                        {
                            Name = output.Name
                        });
                    }

                    break;
                }
            }
        }
    }
}