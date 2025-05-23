using Microsoft.UI.Xaml.Data;

namespace UnoCostAnalyzer.Converters;

public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object currency, string language)
    {
        if (value is null || !(value is double or float or decimal))
            return "";

        var amount = System.Convert.ToDouble(value);
        var currencyCode = (currency as string)?.ToUpperInvariant() ?? "PLN";

        return currencyCode switch
        {
            "PLN" => $"{amount:F2} zł",
            "USD" => $"${amount:F2}",
            "EUR" => $"{amount:F2} €",
            "GBP" => $"£{amount:F2}",
            _ => $"{amount:F2} {currencyCode}"
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
        => throw new NotSupportedException();
}
