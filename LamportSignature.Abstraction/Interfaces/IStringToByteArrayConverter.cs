namespace LamportSignature.Abstraction.Interfaces;

public interface IStringToByteArrayConverter
{
    public byte[] Convert(string input);
}