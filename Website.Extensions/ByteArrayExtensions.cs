using System.Linq;

namespace Website.Extensions
{
    public static class ByteArrayExtensions
    {
        public static bool IsSame(this byte[] a, byte[] b) 
            => !a.Where((byt, index) => byt != b[index]).Any();
    }
}
