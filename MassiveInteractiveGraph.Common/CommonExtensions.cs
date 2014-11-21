using System.IO;

namespace MassiveInteractiveGraph.Common
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Converts string to a stream
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Stream ToStream(this string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
