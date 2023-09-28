using System.Collections;

namespace Security.Compress;

public class SortedNumberCompressor
{
    public static string Compress(List<int> orders)
    {
        var bits = new BitArray(orders[^1]);
        foreach (var order in orders)
            bits[order - 1] = true;
        var bytes = new byte[(bits.Length - 1) / 8 + 1];

        bits.CopyTo(bytes, 0);
        return Convert.ToBase64String(bytes);
    }

    public static BitArray Decompress(string compressedIds)
    {
        var bytes = Convert.FromBase64String(compressedIds);
        return new BitArray(bytes);
    }
}