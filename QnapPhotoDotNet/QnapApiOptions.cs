namespace RobGray.QnapPhotoDotNet;

using System.ComponentModel.DataAnnotations;

public class QnapApiOptions
{
    public const string Key = "QnapApi";
    
    [Required]
    [Url]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Username { get; init; }
    
    [Required]
    public required string Password { get; init; }
    
    [Required]
    public required int AuthTimeoutInHours { get; init; }
}