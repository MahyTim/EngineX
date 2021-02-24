using System.Collections.Generic;

namespace EngineX
{
    public record ParameterName
    {
        public string Name { get; init; }
        private static Dictionary<string, ParameterName> parameterNames = new();

        protected ParameterName(string name)
        {
            this.Name = name;
        }

        public static implicit operator string(ParameterName d) => d.Name;

        public static ParameterName Get(string name)
        {
            ParameterName reffed;
            if (false == parameterNames.TryGetValue(name, out reffed))
            {
                reffed = new ParameterName(name);
                parameterNames[name] = reffed;
                return reffed;
            }
            return reffed;
        }
    }
}