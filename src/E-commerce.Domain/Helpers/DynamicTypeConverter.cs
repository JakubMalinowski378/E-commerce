using System.Globalization;

namespace E_commerce.Domain.Helpers;
public static class DynamicTypeConverter
{
    public static object Convert(object value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");

        string inputValue = value.ToString()?.Trim() ?? string.Empty;

        if (int.TryParse(inputValue, out int intValue))
            return intValue;

        if (double.TryParse(inputValue, out double doubleValue))
            return doubleValue;

        if (bool.TryParse(inputValue, out bool boolValue))
            return boolValue;

        if (DateTime.TryParse(inputValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTimeValue))
            return dateTimeValue;

        return inputValue;
    }
}