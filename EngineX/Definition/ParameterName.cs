using System.Collections.Generic;
using System.Threading;

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

        public static ParameterName For(string name)
        {
            lock (parameterNames)
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
}