using System.Globalization;


namespace CryptoMaui.Converters
{
    class RoiToStringConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is decimal dVal && dVal != 0)
            {
                return $"ROI: {(dVal < 0 ? "-" : "+")}{dVal:F3} %";
            }
            return "ROI: 0";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
