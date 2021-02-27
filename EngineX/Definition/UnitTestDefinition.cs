using System;
using System.Collections.Generic;
using EngineX.Runtime;

namespace EngineX.Definition
{
    public class UnitTestDefinition
    {
        public string Description { get; set; }
        public List<Parameter> Input = new();
        public List<Parameter> ExpectedOutput = new();

        public void Validate()
        {
            throw new NotImplementedException("TODO");
        }
    }
}