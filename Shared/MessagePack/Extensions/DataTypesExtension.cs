using System.Text;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class DataTypesExtension
    {
        internal static byte GetHighBits(this DataTypes type, byte bitsCount)
        {
            return (byte)((byte)type >> (8 - bitsCount));
        }

        internal static string JoinToString(this DataTypes[] types, string separator)
        {
            StringBuilder sb = new();
            foreach (var item in types)
            {
                sb.Append(item);
                sb.Append(separator);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - separator.Length, separator.Length);
            return sb.ToString();
        }
    }
}
