using System.Security.Cryptography;
using LamportSignature.Abstraction.Interfaces;

namespace LamportSignature;

public class RandomBytesGenerator : IRandomBytesGenerator
{
    private readonly RandomNumberGenerator _randomNumberGenerator;

    public RandomBytesGenerator()
    {
        _randomNumberGenerator = RandomNumberGenerator.Create();
    }
    
    public byte[] GenerateRandomBytes(int size)
    {
        var bytes = new byte[size];
        _randomNumberGenerator.GetBytes(bytes);
        return bytes;
    }
}