using LamportSignature.Abstraction.Interfaces;
using Moq;

namespace LamportSignature.Test;

public class SignatureVerifierTests
{
    private readonly Mock<IHashGenerator> _hashGeneratorMock = new();
    private readonly Mock<IStringToByteArrayConverter> _stringToByteArrayConverter = new();
    
    [SetUp]
    public void SetUp()
    {
        _stringToByteArrayConverter.Setup(converter => converter
            .Convert(It.IsAny<string>())).Returns(It.IsAny<byte[]>);
    }

    [Test]
    public void WhenPassEmptySignatureEnumeration_ThenInvalidOperationExceptionShouldBeThrown()
    {
        //arrange
        var sut = new SignatureVerifier(_hashGeneratorMock.Object, _stringToByteArrayConverter.Object);
        
        //act
        Assert.Throws<InvalidOperationException>(() =>
        {
            sut.Verify("", new List<byte[]>(), new List<byte[]>());
        }, "signature contains no elements");
    }
    
    [Test]
    public void WhenPassEmptyPublicKeyList_ThenInvalidOperationExceptionShouldBeThrown()
    {
        //arrange
        var sut = new SignatureVerifier(_hashGeneratorMock.Object, _stringToByteArrayConverter.Object);
        
        //act
        Assert.Throws<InvalidOperationException>(() =>
        {
            sut.Verify("", new List<byte[]>{
            new [] { (byte)244 }
            }, new List<byte[]>());
        }, "publicKey contains no elements");
    }

    [TestCase(0b11001100, true)]
    [TestCase(0b11001101, false)]
    [TestCase(0b11001110, false)]
    public void WhenPassMessageWithSignatureAndPublicKey_ThenVerificationShouldReturnExpectedResult(int messageHash, bool expectedResult)
    {
        //arrange
        var (signature, publicKey) = GenerateSignatureAndCorrespondingPublicKey();
        PrepareHashGeneratorMock(messageHash, signature);
        var signatureVerifier = new SignatureVerifier(_hashGeneratorMock.Object, _stringToByteArrayConverter.Object);
       
        //act
        var result = signatureVerifier.Verify("", signature, publicKey);
        
        //assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private void PrepareHashGeneratorMock(int messageHash, List<byte[]> signature)
    {
        _hashGeneratorMock.Setup(generator => generator
            .GenerateHash(It.IsAny<byte[]>())).Returns(() => new byte[] { (byte)messageHash });
        signature.ForEach(item => _hashGeneratorMock
            .Setup(generator => generator.GenerateHash(item))
            .Returns(item));
    }

    private (List<byte[]> signature, List<byte[]> publicKey) GenerateSignatureAndCorrespondingPublicKey()
    {
        var randomGenerator = new Random();
        var publicKey = new List<byte[]>();
        var signatureWithAllByteArray = new List<byte[]>();
        
        for (var i = 0; i < 16; i++)
        {
            var randomByte = new byte[32];
            for (var j = 0; j < 32; j++)
            {
                randomByte[j] = (byte) randomGenerator.Next(0, 255);
            }
            signatureWithAllByteArray.Add(randomByte);
            publicKey.Add(randomByte);
        }

        var signature = new List<byte[]>
        {
            signatureWithAllByteArray[0],
            signatureWithAllByteArray[1],
            signatureWithAllByteArray[10],
            signatureWithAllByteArray[11],
            signatureWithAllByteArray[4],
            signatureWithAllByteArray[5],
            signatureWithAllByteArray[14],
            signatureWithAllByteArray[15]
        };
        
        return (signature, publicKey);
    }
}