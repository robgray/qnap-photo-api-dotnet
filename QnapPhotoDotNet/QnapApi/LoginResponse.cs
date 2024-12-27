namespace RobGray.QnapPhotoDotNet.QnapApi;

using System.Xml.Serialization;

[XmlRoot("QDocRoot")]
public class LoginResponse
{
    //[XmlElement("qtoken")]
    //public required string QToken { get; set; }
    
    [XmlElement("authPassed")]
    public bool AuthPassed { get; set; }
    
    [XmlElement("authSid")]
    public required string Sid { get; set; }
}

