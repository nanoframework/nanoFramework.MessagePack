
using System.Reflection;
using System.Text;

namespace nanoFramework.MessagePack.Extensions
{
    internal static class MemberInfoExtension
    {
        internal static string JoinToString(this MemberInfo[] memberInfoArray, string separator)
        {
            StringBuilder sb = new();
            foreach (var memberInfo in memberInfoArray)
            {
                sb.Append(memberInfo.DeclaringType!.Name);
                sb.Append('.');
                sb.Append(memberInfo.Name);
                sb.Append(separator);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - separator.Length, separator.Length);
            return sb.ToString();
        }
    }
}
