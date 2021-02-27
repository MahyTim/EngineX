using System.Collections.Generic;
using System.Linq;
using EngineX.Runtime;
using Xunit;

namespace EngineX.Definition.Blocks
{
    public class LookupBlockDefinitionTests
    {
        [Fact]
        public void PerformLookupOneValue()
        {
            var simpleBlock = new LookupBlockDefinition("simple");
            simpleBlock.Input.AddNumeric("a");
            simpleBlock.Input.AddNumeric("d");

            //------------------
            // | a | b | c | d |
            //------------------
            // | 1 | 2 | 3 | 4 |
            // | 5 | 6 | 7 | 8 |
            // | 5 | 9 | 9 | 1 |
            //------------------

            simpleBlock.Table.Columns.AddNumeric("a");
            simpleBlock.Table.Columns.AddNumeric("b");
            simpleBlock.Table.Columns.AddNumeric("c");
            simpleBlock.Table.Columns.AddNumeric("d");
            simpleBlock.Table.Rows.Add(new LookupBlockDefinition.LookupTableRow()
            {
                Values = new List<Parameter>()
                {
                    new(ParameterName.For("a"), 1),
                    new(ParameterName.For("b"), 2),
                    new(ParameterName.For("c"), 3),
                    new(ParameterName.For("d"), 4),
                }
            });
            simpleBlock.Table.Rows.Add(new LookupBlockDefinition.LookupTableRow()
            {
                Values = new List<Parameter>()
                {
                    new(ParameterName.For("a"), 5),
                    new(ParameterName.For("b"), 6),
                    new(ParameterName.For("c"), 7),
                    new(ParameterName.For("d"), 8),
                }
            });
            simpleBlock.Table.Rows.Add(new LookupBlockDefinition.LookupTableRow()
            {
                Values = new List<Parameter>()
                {
                    new(ParameterName.For("a"), 5),
                    new(ParameterName.For("b"), 9),
                    new(ParameterName.For("c"), 9),
                    new(ParameterName.For("d"), 1),
                }
            });
        }
    }

    public class LookupBlockDefinition : BlockDefinition
    {
        public class LookupTable
        {
            public List<ParameterDefinition> Columns = new();
            public List<LookupTableRow> Rows = new();
        }

        public class LookupTableRow
        {
            public List<Parameter> Values = new();
        }

        public LookupBlockDefinition(string description) : base(description)
        {
        }

        public LookupTable Table = new();

        public override bool IsDeterministic => false;

        protected override void InnerValidate()
        {
            throw new System.NotImplementedException();
        }

        protected override void InnerExecute(Calculation calculation)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CompositeBlockDefinition : BlockDefinition
    {
        public List<BlockDefinition> Blocks = new();
        public List<ParameterWire> Wires = new();
        public ExecutionOrder ExecutionOrder { get; set; }


        public override bool IsDeterministic => Blocks.All(z => z.IsDeterministic);

        protected override void InnerValidate()
        {
            Blocks.ForEach(z => z.Validate());
            //TODO: validate wires!
        }

        protected override void InnerExecute(Calculation calculation)
        {
            if (Output.Any())
            {
                if (ExecutionOrder == ExecutionOrder.Sequential)
                {
                    var blockCalculations = Blocks.ToDictionary(z => z, z => new Calculation(z));
                    blockCalculations.Add(this, calculation);

                    foreach (var block in Blocks)
                    {
                        var blockCalculation = blockCalculations[block];
                        var inputWires = Wires.Where(z => z.To.Block == block).ToArray();
                        foreach (var inputWire in inputWires)
                        {
                            blockCalculation.Logging.AppendLine(
                                $"Requesting parameter '{inputWire.From.ParameterName}' from block '{inputWire.From.Block.Description}'");
                            var value = blockCalculations[inputWire.From.Block].State
                                .Get(inputWire.From.ParameterName);
                            blockCalculation.Set(value with {Name = inputWire.To.ParameterName});
                            blockCalculation.Logging.AppendLine($"Copied parameter value '{value.Value}'");
                        }

                        block.Execute(blockCalculation);
                    }

                    var outputWires = Wires.Where(z => z.To.Block == this).ToArray();
                    foreach (var outputWire in outputWires)
                    {
                        var value = blockCalculations[outputWire.From.Block].State
                            .Get(outputWire.From.ParameterName);
                        calculation.Set(value with {Name = outputWire.To.ParameterName});
                    }

                    foreach (var blockCalculation in blockCalculations.Where(z => z.Key != this))
                    {
                        blockCalculation.Value.Dispose();
                    }
                }
            }
        }

        public CompositeBlockDefinition(string description) : base(description)
        {
        }
    }
}