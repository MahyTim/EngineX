using System.Collections.Generic;
using EngineX.Definition;
using EngineX.Definition.Blocks;
using EngineX.Runtime;

namespace EngineX
{
    public static class FluentApi
    {
        public static void Set(this Calculation calculation, ParameterName name, ParameterValue value)
        {
            calculation.Set(new Parameter(name, value));
        }

        public static LookupBlockDefinition.LookupTableRow AddValue(this LookupBlockDefinition.LookupTableRow row,
            ParameterName parameterName, ParameterValue value)
        {
            row.Values.Add(new Parameter(parameterName, value));
            return row;
        }

        public static LookupBlockDefinition.LookupTableRow Add(this List<LookupBlockDefinition.LookupTableRow> list)
        {
            LookupBlockDefinition.LookupTableRow x = new();
            list.Add(x);
            return x;
        }

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