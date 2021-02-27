﻿using System.Collections.Generic;
using EngineX.Runtime;

namespace EngineX.Definition
{
    public abstract class BlockDefinition
    {
        public string Description { get; private set; }
        public List<ParameterDefinition> Input = new();
        public List<ParameterDefinition> Output = new();
        public List<Parameter> DefaultValues = new();
        public List<UnitTestDefinition> UnitTests = new();

        public abstract bool IsDeterministic { get; }

        protected BlockDefinition(string description)
        {
            Description = description;
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