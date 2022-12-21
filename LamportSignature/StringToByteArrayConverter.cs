using System.Text;
using LamportSignature.Abstraction.Interfaces;

namespace LamportSignature;

public class StringToByteArrayConverter : IStringToByteArrayConverter
{
    public byte[] Convert(string input)
    {
        return Encoding.ASCII.GetBytes(input);
    }
}