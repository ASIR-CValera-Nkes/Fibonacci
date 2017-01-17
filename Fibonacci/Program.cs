using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numerics;

namespace Fibonacci
{
    class Program
    {
        private static int posicion;
        static void Main(string[] args)
        {
            Console.Write("Escribe la posición que quieres calcular de la serie de Fibonacci: ");
            string p = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(p) && int.TryParse(p, out posicion) && posicion > 1)
            {
                BigRational r = 0,
                           lr = 1;
                    //j = 1;
                Console.WriteLine();
                //Console.WriteLine("Pos #1) 0 + 0 = 0");
                for (int i = 0; i < posicion; ++i)
                {
                    //if(i > 0)
                        Console.WriteLine("Pos #" + (i + 1) + ") " + r + " + " + lr + " = " + (lr + r));
                    BigRational v = lr;
                    lr = r + lr;
                    r = v;
                }
                Console.WriteLine();
                Console.WriteLine("El resultado es {0}, Fhi = {1:F5}", lr, lr / r);
                Console.WriteLine();
                Console.WriteLine("Comparando capacidades:");
                Console.WriteLine();
                Console.WriteLine("{0:F5}% de un short (2^15 = {1})", lr.GetWholePart() * 100 / ushort.MaxValue, ushort.MaxValue);
                Console.WriteLine("{0:F5}% de un ushort (2^16 = {1})", lr.GetWholePart() * 100 / short.MaxValue, short.MaxValue);
                Console.WriteLine("{0:F5}% de un int (2^31 = {1})", lr.GetWholePart() * 100 / int.MaxValue, int.MaxValue);
                Console.WriteLine("{0:F5}% de un uint (2^32 = {1})", lr.GetWholePart() * 100 / uint.MaxValue, uint.MaxValue);
                Console.WriteLine("{0:F5}% de un long (2^63 = {1})", lr.GetWholePart() * 100 / long.MaxValue, long.MaxValue);
                Console.WriteLine("{0:F5}% de un ulong (2^64 = {1})", lr.GetWholePart() * 100 / ulong.MaxValue, ulong.MaxValue);
                Console.WriteLine("0,{0:F5}% de un float ({1})", (lr * 100 / float.MaxValue).GetFractionPart().ToString().Substring(2, 25), float.MaxValue.ToString());
                Console.WriteLine("0,{0:F5}% de un double ({1})", (lr * 100 / double.MaxValue).GetFractionPart().ToString().Substring(2, 25), double.MaxValue.ToString());
                Console.WriteLine("0,{0:F5}% de un decimal ({1})", (lr * 100 / decimal.MaxValue).GetFractionPart().ToString().Substring(2, 25), decimal.MaxValue.ToString());
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
}
