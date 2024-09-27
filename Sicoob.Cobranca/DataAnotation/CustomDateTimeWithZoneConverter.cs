using Newtonsoft.Json.Converters;

public class CustomDateTimeWithZoneConverter : IsoDateTimeConverter
{
    public CustomDateTimeWithZoneConverter()
    {
        DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
    }
}

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        DateTimeFormat = "yyyy-MM-dd";
    }
}