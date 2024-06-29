using System.ComponentModel.DataAnnotations;

namespace RobGray.QnapPhotoDotNet;

public class QnapApiOptions
{
    public const string Key = "QnapApi";
    
    [Required]
    [Url]
    public string BaseUrl { get; set; }

    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}