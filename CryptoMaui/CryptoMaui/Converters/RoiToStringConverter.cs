using System.Globalization;


namespace CryptoMaui.Converters
{
    class RoiToColorConverter : IValueConverter
    {
        public Color Loss { get; set; } = Colors.Red;

        public Color Gain { get; set; } = Colors.Green;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal dVal && dVal < 0)
            {
                return Loss;
            }
            return Gain;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
