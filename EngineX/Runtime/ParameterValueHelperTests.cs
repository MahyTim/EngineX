using Xunit;

namespace EngineX
{
    public class ParameterValueHelperTests
    {
        [Fact]
        public void Integer_Tests()
        {
            Assert.NotEqual(new ParameterValue(10), new ParameterValue(20));
            Assert.Equal(new ParameterValue(10), new ParameterValue(10));
            Assert.Equal(new ParameterValue(10), new ParameterValue("010"));
            Assert.Equal(new ParameterValue(10), new ParameterValue("10"));
            Assert.Equal(new ParameterValue(10), new ParameterValue("10,0"));
            Assert.Equal(new ParameterValue(1000), new ParameterValue("1.000,0"));
            
            Assert.NotEqual(new ParameterValue(20), new ParameterValue("010"));
            Assert.NotEqual(new ParameterValue(20), new ParameterValue("10"));
            Assert.NotEqual(new ParameterValue(20), new ParameterValue("10.0"));
            
            Assert.Equal(new ParameterValue(10), new ParameterValue(10.0M));
            Assert.Equal(new ParameterValue(10), new ParameterValue(10.0F));
            Assert.Equal(new ParameterValue(1), new ParameterValue(true));
            Assert.Equal(new ParameterValue(1), new ParameterValue("true"));
            Assert.Equal(new ParameterValue(1), new ParameterValue("truE"));
            Assert.Equal(new ParameterValue(1), new ParameterValue("TRUE"));
            Assert.Equal(new ParameterValue(1), new ParameterValue("1"));
            
            Assert.NotEqual(new ParameterValue(0), new ParameterValue(true));
            Assert.NotEqual(new ParameterValue(0), new ParameterValue("true"));
            Assert.NotEqual(new ParameterValue(0), new ParameterValue("truE"));
            Assert.NotEqual(new ParameterValue(0), new ParameterValue("TRUE"));
            Assert.NotEqual(new ParameterValue(1), new ParameterValue("2"));
        }
    }
}