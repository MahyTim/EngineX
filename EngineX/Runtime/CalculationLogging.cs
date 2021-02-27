using System.Text;

namespace EngineX.Runtime
{
    public class CalculationLogging
    {
        public string HumanReadable => _humanReadable.ToString();

        private StringBuilder _humanReadable = new();

        public void AppendLine(string message)
        {
            _humanReadable.AppendLine(message);
        }
    }
}