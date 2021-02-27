using System;
using System.Collections.Generic;
using EngineX.Runtime;

namespace EngineX.Definition
{
    public class UnitTestDefinition
    {
        public string Description { get; set; }
        public List<Parameter> Input = new List<Parameter>();
        public List<Parameter> ExpectedOutput = new List<Parameter>();

        public void Validate()
        {
            throw new NotImplementedException("TODO");
        }
    }
}