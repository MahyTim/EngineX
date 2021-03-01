using System.Collections.Generic;
using EngineX.Runtime;

namespace EngineX.Definition
{
    public abstract class BlockDefinition
    {
        public string Description { get; private set; }
        public virtual List<ParameterDefinition> Input { get; private set; }
        public virtual List<ParameterDefinition> Output { get; private set; }
        public virtual List<Parameter> DefaultValues { get; private set; }
        public virtual List<UnitTestDefinition> UnitTests { get; private set; }

        public abstract bool IsDeterministic { get; }

        protected BlockDefinition(string description)
        {
            Description = description;
            Input = new List<ParameterDefinition>();
            Output = new List<ParameterDefinition>();
            DefaultValues = new List<Parameter>();
            UnitTests = new List<UnitTestDefinition>();
        }

        public void Validate()
        {
            InnerValidate();
        }

        protected abstract void InnerValidate();

        public void Execute(Calculation state)
        {
            InnerExecute(state);
        }

        protected abstract void InnerExecute(Calculation calculation);
    }
}