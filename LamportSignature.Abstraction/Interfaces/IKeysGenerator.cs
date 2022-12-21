namespace LamportSignature.Abstraction.Interfaces;

public interface IKeysGenerator
{
    (List<byte[]>, List<byte[]>) GeneratePrivateAndPublicKeys();
}