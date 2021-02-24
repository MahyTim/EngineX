using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineX
{
    public record ParameterWire
    {
        public Endpoint From { get; init; }
        public Endpoint To { get; init; }

        public record Endpoint(BlockDefinition Block, ParameterName ParameterName)
        {
        }
    }

    public class CompositeBlockDefinition : BlockDefinition
    {
        public List<BlockDefinition> Blocks = new();
        public List<ParameterWire> Wires = new();

        public override bool IsDeterministic => Blocks.All(z => z.IsDeterministic);

        protected override void InnerValidate()
        {
            Blocks.ForEach(z => z.Validate());
            //TODO: validate wires!
        }

        protected override void InnerExecute(CalculationState state)
        {
            if (Output.Any())
            {
                for (int i = Blocks.Count-1; i > 0; i--)
                {
                    var blockToPotentiallyExecute = Blocks[i];
                    using (var calculation = new Calculation(blockToPotentiallyExecute))
                    {
                        foreach (var wireToGetValueFor in Wires.Where(z =>
                            z.To.Block == this && z.From.Block == blockToPotentiallyExecute))
                        {
                            state.Values[wireToGetValueFor.To.ParameterName] =
                                calculation.Get(wireToGetValueFor.From.ParameterName);
                        }
                    }
                }
            }
        }
    }
}