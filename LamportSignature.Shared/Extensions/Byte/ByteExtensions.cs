namespace LamportSignature.Shared.Extensions.Byte;

public static class ByteExtensions
{
    public static int ValueOnPosition(this byte examineByte, int position)
    {
        if (position is > 7 or < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position),
                $"Argument {nameof(position)} is less then 0 and higher then 7");
        }

        var resultAfterMasking = examineByte & (1 << position);
        return resultAfterMasking == 0 ? 0 : 1;
    }
}