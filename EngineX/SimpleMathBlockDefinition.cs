using System;
using System.Linq;
using NCalc;

namespace EngineX
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

        protected override void InnerExecute(CalculationState state)
        {
            var expr = new Expression(Expression);
            expr.EvaluateParameter += ((name, args) =>
            {
                ParameterValue result;
                args.HasResult = state.Values.TryGetValue(ParameterName.Get(name), out result);
                args.Result = result?.value;
            });

            var resultValue = expr.Evaluate();
            state.Values[Output.First().Name] = new ParameterValue(Output.First().Name, resultValue);
        }
    }
}