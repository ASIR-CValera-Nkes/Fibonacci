using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                ulong r = 0,
                    lr = 1;
                    //j = 1;
                Console.WriteLine();
                //Console.WriteLine("Pos #1) 0 + 0 = 0");
                for (int i = 0; i < posicion; ++i)
                {
                    //if(i > 0)
                        Console.WriteLine("Pos #" + (i + 1) + ") " + r + " + " + lr + " = " + (lr + r));
                    ulong v = lr;
                    lr = r + lr;
                    r = v;
                }
                Console.WriteLine();
                Console.WriteLine("El resultado es {0}, Fhi = {1:F5}, % = {2}", lr, (lr / (double)r), ((double)lr / ulong.MaxValue));
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
