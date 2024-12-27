namespace RobGray.QnapPhotoApiDotNet.QnapApi.Login;

using System.Xml.Serialization;

[XmlRoot("QDocRoot")]
public class LoginResponse
{
    [XmlElement("authPassed")]
    public bool AuthPassed { get; set; }
    
    [XmlElement("authSid")]
    public string? Sid { get; set; }
	
    [XmlElement("username")]
    public string? Username { get; set; }
	
    [XmlElement("mediaReady")]
    public bool IsMediaReady { get; set; }
	
    [XmlElement("is_booting")]
    public bool IsBooting { get; set; }
}

