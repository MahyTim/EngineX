using System.Collections.Generic;
using System.Linq;
using EngineX.Definition;

namespace EngineX.Runtime
{
    public class CalculationState
    {
        private readonly Calculation _calculation;
        private Dictionary<ParameterName, Parameter> _values = new();

        public CalculationState(Calculation calculation)
        {
            _calculation = calculation;
        }

        public Parameter Get(ParameterName name)
        {
            Parameter fromState;
            if (false == _values.TryGetValue(name, out fromState))
            {
                fromState = _calculation.Definition.DefaultValues.FirstOrDefault(z => z.Name == name);
            }
            return fromState;
        }

        public void Set(Parameter parameterValue)
        {
            _values[parameterValue.Name] = parameterValue;
        }
    }
}