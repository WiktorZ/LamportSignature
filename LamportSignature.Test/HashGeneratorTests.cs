namespace LamportSignature.Test;

public class HashGeneratorTests
{
    private readonly byte[] _inputMessage = { 119, 121, 115, 122, 111, 103, 114, 111, 100 };
    private readonly byte[] _expectedValue =
    {
        0x37, 0x16, 0xFF, 0x15, 0xC8, 0x82, 0x95, 0xCA, 
        0x6D, 0xBD, 0x11, 0x53, 0xFA, 0xD2, 0xCD, 0xD3,
        0x9B, 0x42, 0x77, 0xB4, 0xA5, 0xB0, 0xD3, 0x19,
        0x94, 0x78, 0xE8, 0xC1 ,0x87, 0x40, 0x14, 0x9C
    };
    
    [Test]
    public void WhenRunKeysGeneratorGeneratePrivateAndPublicKeys_ThenCorrectHashShouldBeReturned()
    {
        //arrange 
        var sut = new HashGenerator();
        
        //act
        var hash = sut.GenerateHash(_inputMessage);
        
        //assert 
        Assert.That(hash, Is.EqualTo(_expectedValue));
    }
}