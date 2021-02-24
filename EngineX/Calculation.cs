﻿using System;
using System.Linq;

namespace EngineX
{
    public class Calculation : IDisposable
    {
        private readonly BlockDefinition _definition;
        private CalculationState _state = new();
        public bool IsCalculated { get; private set; }

        public Calculation(BlockDefinition definition)
        {
            _definition = definition;
        }

        public void Set(ParameterValue parameter)
        {
            _state.Values[parameter.Name] = parameter;
        }

        public ParameterValue Get(ParameterName name)
        {
            if (false == IsCalculated)
            {
                if (_definition.Output.Any(z => z.Name == name))
                {
                    _definition.Execute(_state);
                    IsCalculated = true;
                }    
            }
            return _state.Values[name];
        }

        public void Dispose()
        {
            _state = null;
        }
    }
}