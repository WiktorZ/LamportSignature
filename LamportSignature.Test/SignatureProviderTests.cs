using LamportSignature.Abstraction.Interfaces;
using Moq;

namespace LamportSignature.Test;

public class SignatureProviderTests
{
    private readonly Mock<IHashGenerator> _hashGeneratorMock = new();
    private readonly Mock<IStringToByteArrayConverter> _stringToByteArrayConverter = new();
    
    [SetUp]
    public void SetUp()
    {
        _stringToByteArrayConverter.Setup(converter => converter
            .Convert(It.IsAny<string>())).Returns(It.IsAny<byte[]>);
        _hashGeneratorMock.Setup(generator => generator
            .GenerateHash(It.IsAny<byte[]>())).Returns(() => new byte[] { 0b11001100 });
    }
    [Test]
    public void WhenPassPrivateKeyListToCreateMethod_ThenMethodShouldReturnCorrectSignatureFor11001100HashOfMessage()
    {
        //arrange 
        var sut = new SignatureProvider(_hashGeneratorMock.Object, _stringToByteArrayConverter.Object);
        var privateKey = GeneratePrivateKey();
        
        //act
        var signature = sut.CreateSignature(privateKey, "").ToList();
        
        //assert
        Assert.That(signature, Is.EquivalentTo(new List<byte[]>
        {
            privateKey[0], privateKey[1], privateKey[10], privateKey[11], 
            privateKey[4], privateKey[5], privateKey[14], privateKey[15]
        }));
    }
    [Test]
    public void WhenPassEmptyListWithPrivateKey_ThenCreateSignatureShouldThrownInvalidOperationsException()
    {
        //assert
        var emptyListWithPrivateKey = new List<byte[]>();
        var sut = new SignatureProvider(_hashGeneratorMock.Object, _stringToByteArrayConverter.Object);
        
        //act
        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = sut.CreateSignature(emptyListWithPrivateKey, "").ToList();
        });
    }

    private List<byte[]> GeneratePrivateKey()
    {
        var randomGenerator = new Random();
        var privateKey = new List<byte[]>();
        
        for (var i = 0; i < 16; i++)
        {
            var randomByte = new byte[8];
            for (var j = 0; j < 8; j++)
            {
                randomByte[j] = (byte) randomGenerator.Next(0, 255);
            }
            privateKey.Add(randomByte);
        }
        return privateKey;
    }
}