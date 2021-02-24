namespace EngineX
{
    public record ParameterWire
    {
        public Endpoint From { get; init; }
        public Endpoint To { get; init; }

        public record Endpoint(BlockDefinition Block, ParameterName ParameterName)
        {
        }
    }
}