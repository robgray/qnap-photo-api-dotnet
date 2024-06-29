using System.Xml.Serialization;

namespace RobGray.QnapPhotoDotNet.Core.QnapApi;

[XmlRoot("QDocRoot")]
public class LoginResponse
{
    [XmlElement("authPassed")]
    public bool AuthPassed { get; set; }
    
    [XmlElement("authSid")]
    public string Sid { get; set; }
}

