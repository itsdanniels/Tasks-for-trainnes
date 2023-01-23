using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    internal static class Util
    {
        public static void TemplateSucess(string message)
        {
            Console.Clear();
            Console.WriteLine(new string('-', message.Length));
            Console.WriteLine(message);
            Console.WriteLine(new string('-', message.Length));
            Console.WriteLine();
        }

        public static void TemplateError(string error)
        {
            Console.Clear();
            Console.WriteLine(new string('-', error.Length));
            Console.WriteLine(error);
            Console.WriteLine(new string('-', error.Length));
            Console.WriteLine();
        }

        public static void TemplateMenu(string titulo)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(new string('-', titulo.Length));
            Console.WriteLine(titulo);
            Console.WriteLine(new string('-', titulo.Length));
            Console.WriteLine();
        }

        public static void ErrorValorDigitado()
        {
            var error = "O valor digitado não é um numero!!!";
            Console.Clear();
            Console.WriteLine(new string('-', error.Length));
            Console.WriteLine(error);
            Console.WriteLine(new string('-', error.Length));
        }

        public static void TemplateRelease(string message)
        {
            Console.Clear();
            Console.WriteLine(new string('-', message.Length));
            Console.WriteLine(message);
            Console.WriteLine(new string('-', message.Length));
            Console.WriteLine();
        }
    }
}
