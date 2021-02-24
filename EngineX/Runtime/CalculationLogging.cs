using System.Text;

namespace EngineX
{
    public class CalculationLogging
    {
        public string HumanReadable => _humanReadable.ToString();

        private StringBuilder _humanReadable = new StringBuilder();

        public void AppendLine(string message)
        {
            _humanReadable.AppendLine(message);
        }
    }
}