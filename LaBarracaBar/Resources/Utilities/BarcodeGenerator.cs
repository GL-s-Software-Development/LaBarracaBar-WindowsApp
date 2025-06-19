using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaQuimera.Resources.Utilities
{
    public class BarcodeGenerator
    {
        private readonly Random _random = new Random();

        public string GenerateRandomNumber(int length)
        {
            const string chars = "0123456789"; // Caracteres permitidos en el número aleatorio

            // Generar una cadena aleatoria del tamaño especificado
            string randomNumber = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());

            return randomNumber;
        }
    }
}
