using LamportSignature.Shared.Extensions.Byte;

namespace LamportSignature.Shared.Test.Extensions.Byte;

public class Tests
{
    //binary value: 10101011
    private byte _testingByte = 171;
    
    [TestCase(-1)]
    [TestCase(8)]
    public void WhenPassPositionHigherThen8OrLowerThen1_ThanThrowArgumentOutOfTheRangeException(int position)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _testingByte.ValueOnPosition(position));
    }

    [TestCase(0,1)]
    [TestCase(1, 1)]
    [TestCase(2,0)]
    [TestCase(3,1)]
    [TestCase(4,0)]
    [TestCase(5,1)]
    [TestCase(6,0)]
    [TestCase(7,1)]
    public void WhenPassPositionBetween1And8_ThanBitValueShouldBeReturned(int position, int expectedValue)
    {
        Assert.That(_testingByte.ValueOnPosition(position), Is.EqualTo(expectedValue));
    }
}