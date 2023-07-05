using System;
using System.Text;

namespace TC_Insitu_Monitor.DAL
{
    /// <summary>
    /// https://stackoverflow.com/questions/724862/converting-from-hex-to-string
    /// http://woobit999.blogspot.com/2017/08/c-byte-hex-string-hex-string-byte.html
    /// </summary>
    public static class HexConverter
    {
        public static string StringToHex(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }
        public static string HexToString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }

        public static string ByteArrayToHex(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder str = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    str.Append(bytes[i].ToString("X2"));
                }
                hexString = str.ToString();
            }
            return hexString;
        }
        public static byte[] HexToByteArray(string HexString)
        {
            int byteLength = HexString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { HexString[j], HexString[j + 1] });
                if (hex.Length > 2 || hex.Length <= 0)
                    bytes[i] = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);

                j += 2;
            }
            return bytes;
        }

        public static byte[] StringToByteArray(string str)
        {
            return Encoding.Default.GetBytes(str);
        }
        public static string ByteArrayToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
    }
}
