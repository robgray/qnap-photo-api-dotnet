namespace RobGray.QnapPhotoDotNet.Extensions;

using System.Xml.Serialization;

public static class StringExtensions
{
    public static T? DeserializeXmLToObject<T>(this string xmlData)
    {
        if (string.IsNullOrEmpty(xmlData)) return default;

        var reader = new StringReader(xmlData);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        var returnObject = (T)serializer.Deserialize(reader)!;

        return returnObject;
    }
    
    public static string Base64Encode(this string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}