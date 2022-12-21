namespace LamportSignature.Abstraction.Interfaces;

public interface ISignatureVerifier
{
    bool Verify(string message, IEnumerable<byte[]> signature, List<byte[]> publicKey);
}