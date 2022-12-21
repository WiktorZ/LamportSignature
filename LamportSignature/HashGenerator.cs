using System.Security.Cryptography;
using LamportSignature.Abstraction.Interfaces;

namespace LamportSignature;

public class HashGenerator : IHashGenerator
{
    public int LengthOfHashFunctionOutput => 256;
    
    public byte[] GenerateHash(byte[] inputBytes)
    {
        HashAlgorithm hashAlgorithm = SHA256.Create();
        return hashAlgorithm.ComputeHash(inputBytes);
    }
}