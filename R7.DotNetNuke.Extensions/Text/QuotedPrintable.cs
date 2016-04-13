//
//  QuotedPrintable.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, 2015 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

// http://sourceforge.net/apps/mediawiki/syncmldotnet/index.php?title=Quoted_Printable

namespace R7.DotNetNuke.Extensions.Text
{
    /// <summary>
    /// Provide encoding and decoding of Quoted-Printable.
    /// </summary>
    public class QuotedPrintable
    {
        private QuotedPrintable ()
        {
        }

        /// <summary>
        /// // so including the = connection, the length will be 76
        /// </summary>
        private const int RFC_1521_MAX_CHARS_PER_LINE = 75;

        /// <summary>
        /// Return quoted printable string with 76 characters per line.
        /// </summary>
        /// <param name="textToEncode"></param>
        /// <returns></returns>
        public static string Encode (string textToEncode)
        {
            if (textToEncode == null)
                throw new ArgumentNullException ();

            return Encode (textToEncode, RFC_1521_MAX_CHARS_PER_LINE);
        }

        private static string Encode (string textToEncode, int charsPerLine)
        {
            if (textToEncode == null)
                throw new ArgumentNullException ();

            if (charsPerLine <= 0)
                throw new ArgumentOutOfRangeException ();

            return FormatEncodedString (EncodeString (textToEncode), charsPerLine);
        }

        /// <summary>
        /// Return quoted printable string, all in one line.
        /// </summary>
        /// <param name="textToEncode"></param>
        /// <returns></returns>
        public static string EncodeString (string textToEncode)
        {
            if (textToEncode == null)
                throw new ArgumentNullException ();

            var bytes = Encoding.UTF8.GetBytes (textToEncode);
            var builder = new StringBuilder ();
            foreach (var b in bytes)
            {
                if (b != 0)
                if ((b < 32) || (b > 126))
                    builder.Append (String.Format ("={0}", b.ToString ("X2")));
                else
                {
                    switch (b)
                    {
                        case 13:
                            builder.Append ("=0D");
                            break;
                        case 10:
                            builder.Append ("=0A");
                            break;
                        case 61:
                            builder.Append ("=3D");
                            break;
                        default:
                            builder.Append (Convert.ToChar (b));
                            break;
                    }
                }
            }

            return builder.ToString ();
        }

        private static string FormatEncodedString (string qpstr, int maxcharlen)
        {
            if (qpstr == null)
                throw new ArgumentNullException ();

            var builder = new StringBuilder ();
            var charArray = qpstr.ToCharArray ();
            var i = 0;
            foreach (char c in charArray)
            {
                builder.Append (c);
                i++;
                if (i == maxcharlen)
                {
                    builder.AppendLine ("=");
                    i = 0;
                }
            }

            return builder.ToString ();
        }

        static string HexDecoderEvaluator (Match m)
        {
            if (String.IsNullOrEmpty (m.Value))
                return null;

            CaptureCollection captures = m.Groups [3].Captures;
            var bytes = new byte[captures.Count];

            for (int i = 0; i < captures.Count; i++)
            {
                bytes [i] = Convert.ToByte (captures [i].Value, 16);
            }

            return UTF8Encoding.UTF8.GetString (bytes);
        }

        static string HexDecoder (string line)
        {
            if (line == null)
                throw new ArgumentNullException ();

            var re = new Regex ("((\\=([0-9A-F][0-9A-F]))*)", RegexOptions.IgnoreCase);
            return re.Replace (line, new MatchEvaluator (HexDecoderEvaluator));
        }

        public static string Decode (string encodedText)
        {
            if (encodedText == null)
                throw new ArgumentNullException ();

            using (var sr = new StringReader (encodedText))
            {
                var builder = new StringBuilder ();
                string line;
                while ((line = sr.ReadLine ()) != null)
                {
                    if (line.EndsWith ("=", StringComparison.Ordinal))
                        builder.Append (line.Substring (0, line.Length - 1));
                    else
                        builder.Append (line);
                }

                return HexDecoder (builder.ToString ());
            }
        }


    }
}

