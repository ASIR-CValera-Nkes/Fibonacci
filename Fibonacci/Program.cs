using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics;
using System.Globalization;
using System.Diagnostics;

namespace Fibonacci
{
    class Program
    {
        private static int posicion;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.Write("Escribe la posición que quieres calcular de la serie de Fibonacci: ");
            string p = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(p) && int.TryParse(p, out posicion) && posicion > 1)
            {
                BigRational r = 0,
                            lr = 1;
                    //j = 1;
                Console.WriteLine();
                Console.WriteLine("En la posición 1 tenemos que el primer número es 0 y el segundo número es 1 y como resultado da 1, seguimos sumando:");
                for (int i = 0; i < posicion - 1; ++i)
                {
                    Console.WriteLine("Pos #" + (i + 2) + ") " + r.Numerator + " + " + lr.Numerator + " = " + (lr + r).Numerator);
                    BigRational v = lr;
                    lr = r + lr;
                    r = v;
                }
                sw.Stop();
                Console.WriteLine();
                Console.WriteLine("El resultado es {0}, la consulta ha tardado {1} ms.", lr.Numerator.FormatString(), sw.ElapsedMilliseconds); //, (lr / r).Numerator
                Console.WriteLine();
                Console.WriteLine("Comparando capacidades:");
                Console.WriteLine();
                Console.WriteLine("{0:F5}% de un short (2^15 = {1})", (lr.GetWholePart() * 100 / ushort.MaxValue).FormatString(), ushort.MaxValue);
                Console.WriteLine("{0:F5}% de un ushort (2^16 = {1})", (lr.GetWholePart() * 100 / short.MaxValue).FormatString(), short.MaxValue);
                Console.WriteLine("{0:F5}% de un int (2^31 = {1})", (lr.GetWholePart() * 100 / int.MaxValue).FormatString(), int.MaxValue);
                Console.WriteLine("{0:F5}% de un uint (2^32 = {1})", (lr.GetWholePart() * 100 / uint.MaxValue).FormatString(), uint.MaxValue);
                Console.WriteLine("{0:F5}% de un long (2^63 = {1})", (lr.GetWholePart() * 100 / long.MaxValue).FormatString(), long.MaxValue);
                Console.WriteLine("{0:F5}% de un ulong (2^64 = {1})", (lr.GetWholePart() * 100 / ulong.MaxValue).FormatString(), ulong.MaxValue);
                Console.WriteLine("{0}% de un float ({1})", (lr * 100 / float.MaxValue).FormatString(), float.MaxValue.ToString());
                Console.WriteLine("{0}% de un double ({1})", (lr * 100 / double.MaxValue).FormatString(), double.MaxValue.ToString());
                Console.WriteLine("{0}% de un decimal ({1})", (lr * 100 / decimal.MaxValue).FormatString(), decimal.MaxValue.ToString());
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Escribe un número válido mayor a 1.");
                Main(null);
            }
            Console.Read();
        }
    }
    public static class BigRationalExtensions
    {
        public static string ToDecimalString(this BigRational r, int precision)
        {
            var fraction = r.GetFractionPart();

            // Case where the rational number is a whole number
            if (fraction.Numerator == 0 && fraction.Denominator == 1)
            {
                return r.GetWholePart() + ".0";
            }

            var adjustedNumerator = (fraction.Numerator
                                               * BigInteger.Pow(10, precision));
            var decimalPlaces = adjustedNumerator / fraction.Denominator;

            // Case where precision wasn't large enough.
            if (decimalPlaces == 0)
            {
                return "0.0";
            }

            // Give it the capacity for around what we should need for 
            // the whole part and total precision
            // (this is kinda sloppy, but does the trick)
            var sb = new StringBuilder(precision + r.ToString().Length);

            bool noMoreTrailingZeros = false;
            for (int i = precision; i > 0; i--)
            {
                if (!noMoreTrailingZeros)
                {
                    if ((decimalPlaces % 10) == 0)
                    {
                        decimalPlaces = decimalPlaces / 10;
                        continue;
                    }

                    noMoreTrailingZeros = true;
                }

                // Add the right most decimal to the string
                sb.Insert(0, decimalPlaces % 10);
                decimalPlaces = decimalPlaces / 10;
            }

            // Insert the whole part and decimal
            sb.Insert(0, ".");
            sb.Insert(0, r.GetWholePart());

            return sb.ToString();
        }
        public static string FormatString(this BigRational br)
        {
            string r = br.ToDecimalString(25);
            if(br.Denominator.GetExponent() > 25 || br.Numerator.GetExponent() > 25)
            {
                int e = GetExponent(br.Numerator) - GetExponent(br.Denominator);
                r = string.Format("~1E{0}", e);
            }
            return r;
        }
        public static string FormatString(this BigInteger bi)
        {
            int e = GetExponent(bi);
            string r = bi.ToString();
            if (e > 25)
                r = string.Format("{0}.{1}E+{2}", r.Substring(0, 1), r.Substring(1, 10), e);
            return r;
        }
        public static int GetExponent(this BigRational br)
        {
            return GetExponent(br.Numerator);
        }
        public static int GetExponent(this BigInteger bi)
        {
            return bi.ToString().Length - 1;
        }
    }
}
