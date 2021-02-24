using System.Collections.Generic;
using System.Linq;

namespace EngineX
{
    public class CalculationState
    {
        private readonly Calculation _calculation;
        private Dictionary<ParameterName, ParameterValue> _values = new Dictionary<ParameterName, ParameterValue>();

        public CalculationState(Calculation calculation)
        {
            _calculation = calculation;
        }

        public ParameterValue Get(ParameterName name)
        {
            ParameterValue fromState;
            if (false == _values.TryGetValue(name, out fromState))
            {
                fromState = _calculation.Definition.DefaultValues.FirstOrDefault(z => z.Name == name);
            }
            return fromState;
        }

        public void Set(ParameterValue parameterValue)
        {
            _values[parameterValue.Name] = parameterValue;
        }
    }
}