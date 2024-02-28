using Newtonsoft.Json.Converters;

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
    }
}