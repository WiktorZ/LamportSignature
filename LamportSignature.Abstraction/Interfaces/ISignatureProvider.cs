namespace LamportSignature.Abstraction.Interfaces;

public interface ISignatureProvider
{
    IEnumerable<byte[]> CreateSignature(List<byte[]> privateKey, string message);
}