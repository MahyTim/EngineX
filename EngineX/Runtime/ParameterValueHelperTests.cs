using Xunit;

namespace EngineX.Runtime
{
    public class ParameterValueHelperTests
    {
        [Fact]
        public void Undefined_Tests()
        {
            Assert.NotEqual(new ParameterValue(false), new ParameterValue(null));
            Assert.NotEqual(new ParameterValue("false"), new ParameterValue(null));
            
            Assert.NotEqual(new ParameterValue(null), new ParameterValue(false));
            Assert.NotEqual(new ParameterValue(null), new ParameterValue(0));
            
            
            Assert.Equal(new ParameterValue(" "), new ParameterValue(null));
            Assert.Equal(new ParameterValue(""), new ParameterValue(null));
        }
        
        [Fact]
        public void Boolean_Tests()
        {
            Assert.NotEqual(new ParameterValue(true), new ParameterValue(false));
            Assert.Equal(new ParameterValue(true), new ParameterValue(true));
            Assert.Equal(new ParameterValue(true), new ParameterValue("true"));
            Assert.Equal(new ParameterValue(true), new ParameterValue("1"));
            Assert.Equal(new ParameterValue(false), new ParameterValue(false));
            Assert.Equal(new ParameterValue(false), new ParameterValue("false"));
            Assert.Equal(new ParameterValue(false), new ParameterValue("FAlse"));
            Assert.Equal(new ParameterValue(false), new ParameterValue("0"));
            
            Assert.NotEqual(new ParameterValue(true), new ParameterValue("SomeOther"));
            Assert.NotEqual(new ParameterValue(false), new ParameterValue("SomeOther"));
        }
        
        [Fact]
        public void String_Tests()
        {
            Assert.NotEqual(new ParameterValue("x"), new ParameterValue("y"));
            Assert.Equal(new ParameterValue("X"), new ParameterValue("x"));
            Assert.Equal(new ParameterValue("X"), new ParameterValue("  x"));
            Assert.Equal(new ParameterValue("X"), new ParameterValue(" x"));
        }
        
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
            Assert.Equal(new ParameterValue(0), new ParameterValue(false));
            Assert.Equal(new ParameterValue(0), new ParameterValue("False"));
            Assert.Equal(new ParameterValue(0), new ParameterValue("0"));
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