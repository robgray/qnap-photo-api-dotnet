using System.Xml.Serialization;

namespace RobGray.QnapPhotoDotNet.Infrastructure.Extensions;

public static class StringExtensions
{
    public static T DeserializeXmLToObject<T>(this string xmlData)
    {
        T returnObject = default(T);
        if (string.IsNullOrEmpty(xmlData)) return default(T);

        var reader = new StringReader(xmlData);
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        returnObject = (T)serializer.Deserialize(reader);

        return returnObject;
    }
    
    public static string Base64Encode(this string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}