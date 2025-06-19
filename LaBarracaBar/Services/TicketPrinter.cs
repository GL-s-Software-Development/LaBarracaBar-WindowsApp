using LaBarracaBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Services
{
    public static class TicketPrinter
    {
        public static void Print(string tableNumber, List<ProductModel> products, decimal total)
        {
            var sb = new StringBuilder();
            sb.AppendLine("       LA BARRACA BAR");
            sb.AppendLine(" Mesa: " + tableNumber);
            sb.AppendLine("--------------------------");

            foreach (var p in products)
            {
                decimal subtotal = p.Quantity * (decimal)p.Price;
                sb.AppendLine($"{p.Name} x{p.Quantity}  ${subtotal:F2}");
            }

            sb.AppendLine("--------------------------");
            sb.AppendLine($" TOTAL: ${total:F2}");
            sb.AppendLine("Gracias por su visita!");
            sb.AppendLine("  ");
            sb.AppendLine("  ");

            RawPrinterHelper.SendStringToPrinter("IT1050", sb.ToString()); // Nombre exacto de la impresora en Windows
        }
    }
}
