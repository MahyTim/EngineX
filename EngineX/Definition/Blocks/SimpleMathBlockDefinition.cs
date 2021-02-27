using System;
using System.Linq;
using EngineX.Runtime;
using NCalc;

namespace EngineX.Definition.Blocks
{
    public class SimpleMathBlockDefinition : BlockDefinition
    {
        public string Expression { get; set; }

        public override bool IsDeterministic => true;

        protected override void InnerValidate()
        {
            if (string.IsNullOrWhiteSpace(Expression))
            {
                throw new Exception("Expression is not specified"); //TODO: cleanup
            }
            if (Output.Count > 1)
            {
                throw new Exception("Output for this block type be max 1 value"); //TODO: cleanup
            }
        }

        protected override void InnerExecute(Calculation calculation)
        {
            var expr = new Expression(Expression);
            expr.EvaluateParameter += ((name, args) =>
            {
                args.Result = calculation.State.Get(ParameterName.For(name))?.Value.ValueForCalculation;
                args.HasResult = args.Result != null;
            });

            var resultValue = expr.Evaluate();
            calculation.State.Set( new Parameter(Output.First().Name, new ParameterValue(resultValue)));
        }

        public SimpleMathBlockDefinition(string description) : base(description)
        {
        }
    }
}