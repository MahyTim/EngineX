using System.Collections.Generic;
using EngineX.Definition;

namespace EngineX
{
    public static class FluentApi
    {
        public static ParameterName AddNumeric(this List<ParameterDefinition> list, string name)
        {
            list.Add(new ParameterDefinition(ParameterName.For(name), new NumericParameterType()));
            return ParameterName.For(name);
        }

        public static void AddText(this List<ParameterDefinition> list, string name)
        {
            list.Add(new ParameterDefinition(ParameterName.For(name), new TextParameterType()));
        }
    }
}