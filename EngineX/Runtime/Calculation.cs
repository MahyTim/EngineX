using System;
using System.Collections.Generic;
using System.Linq;

namespace EngineX
{
    public class Calculation : IDisposable
    {
        internal BlockDefinition Definition { get; private set; }
        public bool IsCalculated { get; private set; }
        public CalculationState State { get; set; }
        public CalculationLogging Logging { get; private set; }

        public Calculation(BlockDefinition definition)
        {
            Definition = definition;
            State = new CalculationState(this);
            Logging = new CalculationLogging();

            Logging.AppendLine($"Start calculation: {Definition.Description}");
        }

        public Calculation SubCalculation(BlockDefinition definition)
        {
            var calc = new Calculation(definition);
            calc.Logging = this.Logging;
            return calc;
        }

        public void Set(ParameterValue parameter)
        {
            State.Set(parameter);
            Logging.AppendLine($"Setting parameter '{parameter.Name}' value '{parameter.Value}'");
        }

        public ParameterValue Get(ParameterName name)
        {
            Logging.AppendLine($"Requesting parameter '{name}' value");
            if (false == IsCalculated)
            {
                if (Definition.Output.Any(z => z.Name == name))
                {
                    Logging.AppendLine($"Start Executing definition '{Definition.Description}'");
                    Definition.Execute(this);
                    Logging.AppendLine($"End Executing definition '{Definition.Description}'");
                    IsCalculated = true;
                }
            }

            return State.Get(name);
        }

        public void Dispose()
        {
            Logging.AppendLine($"End sub calculation: {Definition.Description}");
            State = null;
        }
    }
}