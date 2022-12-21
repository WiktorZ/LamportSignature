using LamportSignature.Abstraction.Interfaces;
using LamportSignature.Shared.Extensions.Byte;

namespace LamportSignature;

public class SignatureVerifier : ISignatureVerifier
{
    private readonly IHashGenerator _hashGenerator;
    private readonly IStringToByteArrayConverter _stringToByteArrayConverter;

    public SignatureVerifier(IHashGenerator hashGenerator, IStringToByteArrayConverter stringToByteArrayConverter)
    {
        _hashGenerator = hashGenerator;
        _stringToByteArrayConverter = stringToByteArrayConverter;
    }
    
    public bool Verify(string message, IEnumerable<byte[]> signature, List<byte[]> publicKey)
    {
        if (!publicKey.Any())
        {
            throw new InvalidOperationException($"{nameof(publicKey)} contains no elements");
        }

        var bytesEnumerable = signature.ToList();
        
        if (!bytesEnumerable.Any())
        {
            throw new InvalidOperationException($"{nameof(signature)} contains no elements");
        }
        
        var binaryMessage = _stringToByteArrayConverter.Convert(message);
        var hashOfMessage = _hashGenerator.GenerateHash(binaryMessage);
        var startingIndex = 0;
        var signatureList = bytesEnumerable.ToList();
        
        foreach (var hashByte in hashOfMessage)
        {
            for (var i = 0; i < 8; i++)
            {
                var index = hashByte.ValueOnPosition(i) == 0 ? startingIndex : startingIndex + hashOfMessage.Length * 8;
                var enumerator = signatureList.Skip(startingIndex).FirstOrDefault();
                if (enumerator == null)
                {
                    throw new ArgumentNullException();
                }

                if (!publicKey[index].SequenceEqual(_hashGenerator.GenerateHash(enumerator))) return false;
                startingIndex++;
            }
        }
        return true;
    }
}