using LamportSignature.Abstraction.Interfaces;

namespace LamportSignature;

public class KeysGenerator : IKeysGenerator
{
    private readonly IRandomBytesGenerator _randomBytesGenerator;
    private readonly IHashGenerator _hashGenerator;
    
    public KeysGenerator(IRandomBytesGenerator randomBytesGenerator, IHashGenerator hashGenerator)
    {
        _randomBytesGenerator = randomBytesGenerator;
        _hashGenerator = hashGenerator;
    }
    
    public (List<byte[]>, List<byte[]>) GeneratePrivateAndPublicKeys()
    {
        var privateKey = new List<byte[]>();
        var publicKey = new List<byte[]>();

        for (var i = 0; i < _hashGenerator.LengthOfHashFunctionOutput * 2; i++)
        {
            var randomNumber = _randomBytesGenerator.GenerateRandomBytes(_hashGenerator.LengthOfHashFunctionOutput / 8);
            var privateKeyFactor = _hashGenerator.GenerateHash(randomNumber);
            var publicKeyFactor = _hashGenerator.GenerateHash(privateKeyFactor);
            privateKey.Add(privateKeyFactor);
            publicKey.Add(publicKeyFactor);
        }

        return (privateKey, publicKey);
    }
}