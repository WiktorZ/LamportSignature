namespace LamportSignature.Abstraction.Interfaces;

public interface IRandomBytesGenerator
{ 
    byte[] GenerateRandomBytes(int size);
}