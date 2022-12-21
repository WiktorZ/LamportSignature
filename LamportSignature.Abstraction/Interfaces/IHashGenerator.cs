namespace LamportSignature.Abstraction.Interfaces;

public interface IHashGenerator
{
    int LengthOfHashFunctionOutput { get; }
    byte[] GenerateHash(byte[] inputBytes);
}