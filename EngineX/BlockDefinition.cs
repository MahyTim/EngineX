using System.Collections.Generic;

namespace EngineX
{
    public abstract class BlockDefinition
    {
        public List<ParameterDefinition> Input = new List<ParameterDefinition>();
        public List<ParameterDefinition> Output = new List<ParameterDefinition>();
        public abstract bool IsDeterministic { get; }

        public void Validate()
        {
            InnerValidate();
        }

        protected abstract void InnerValidate();
        
        public void Execute(CalculationState state)
        {
            InnerExecute(state);
        }

        protected abstract void InnerExecute(CalculationState state);
    }
}