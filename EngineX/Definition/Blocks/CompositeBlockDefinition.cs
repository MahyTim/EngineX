using System;
using System.Collections.Generic;
using System.Linq;
using EngineX.Definition;
using EngineX.Runtime;

namespace EngineX
{
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