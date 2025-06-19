using System;
using System.Globalization;
using System.Windows.Data;

namespace LaBarracaBar.Converters
{
    public class QuantityToWidthConverter : IValueConverter
    {
        private const double MaxWidth = 300; // ancho máximo en píxeles

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int quantity)
            {
                // Escalar la cantidad para que no se pase del máximo
                double width = Math.Min(quantity * 10, MaxWidth);
                return width;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}