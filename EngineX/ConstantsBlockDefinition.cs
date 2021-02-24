﻿using System.Collections.Generic;

namespace EngineX
{
    public class ConstantsBlockDefinition : BlockDefinition
    {
        public List<ParameterValue> Constants = new List<ParameterValue>();

        public ConstantsBlockDefinition(string description) : base(description)
        {
        }

        public override bool IsDeterministic => true;

        protected override void InnerValidate()
        {
        }

        protected override void InnerExecute(Calculation calculation)
        {
            foreach (var constant in Constants)
            {
                calculation.State.Set(constant);
            }
        }
    }
}