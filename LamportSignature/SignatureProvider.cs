using LamportSignature.Abstraction.Interfaces;
using LamportSignature.Shared.Extensions.Byte;

namespace LamportSignature;

public class SignatureProvider : ISignatureProvider
{
    private readonly IHashGenerator _hashGenerator;
    private readonly IStringToByteArrayConverter _stringToByteArrayConverter;

    public SignatureProvider(IHashGenerator hashGenerator, IStringToByteArrayConverter stringToByteArrayConverter)
    {
        _hashGenerator = hashGenerator;
        _stringToByteArrayConverter = stringToByteArrayConverter;
    }
    
    public IEnumerable<byte[]> CreateSignature(List<byte[]> privateKey, string message)
    {
        if (!privateKey.Any())
        {
            throw new InvalidOperationException($"{nameof(privateKey)} contains no elements");
        }

        var binaryMessage = _stringToByteArrayConverter.Convert(message);
        var hashOfMessage = _hashGenerator.GenerateHash(binaryMessage);
        var startingIndex = 0;
        foreach (var hashByte in hashOfMessage)
        {
            for (var i = 0; i < 8; i++)
            {
                if (hashByte.ValueOnPosition(i) == 0) yield return privateKey[startingIndex];
                else yield return privateKey[hashOfMessage.Length * 8 + startingIndex];
                startingIndex++;
            }
        }
    }
}