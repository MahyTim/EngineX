namespace EngineX
{
    public record ParameterName (string Name)
    {
        public static implicit operator string(ParameterName d) => d.Name;

    }
}