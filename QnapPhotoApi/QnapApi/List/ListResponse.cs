namespace Ragware.QnapPhotoApi.QnapApi.List;

using System.Text.Json.Serialization;

public class ListResponse
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    
    [JsonPropertyName("photoCount")]
    public int TotalItems { get; set; }
    
    [JsonPropertyName("DataList")]
    public DataList[]? Data { get; set; }
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}

public class DataList
{
    public FileItem? FileItem { get; set; }
}

public class FileItem
{
    public string? MediaType { get; set; }
    
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    
    [JsonPropertyName("cFileName")]
    public required string FileName { get; set; }
    
    [JsonPropertyName("cPictureTitle")]
    public string? PictureTitle { get; set; }
    
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
    
    [JsonPropertyName("mime")]
    public string? Mime { get; set; }
    
    [JsonPropertyName("iFileSize")]
    public int? FileSize { get; set; }
    
    [JsonPropertyName("iWidth")]
    public int? Width { get; set; }
    
    [JsonPropertyName("iHeight")]
    public int? Height { get; set; }
    
    public string? YearMonth { get; set; }
    
    public string? YearMonthDay { get; set; }
    
    [JsonPropertyName("dateTime")]
    public DateTime? DateTime { get; set; }
    
    public DateTime? DateCreated { get; set; }
    
    public DateTime? DateModified { get; set; }
    
    public DateTime? AddToDbTime { get; set; }
    
    public DateTime? LastUpdate { get; set; }
    
    public string? ScannedFlag { get; set; }
    
    public string? ColorLevel { get; set; }
    
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; }
    
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; }
    
    [JsonPropertyName("location")]
    public object? Location { get; set; }
    
    public string? Orientation { get; set; }
    
    public string? ProtectionStatus { get; set; }
    
    [JsonPropertyName("lensInfo")]
    public string? LensInfo { get; set; }
    
    public string? Exposure { get; set; }
    
    [JsonPropertyName("ISO")]
    public string? Iso { get; set; }
    
    public string? Maker { get; set; }
    
    public string? Model { get; set; }
    
    public string? WhiteBalance { get; set; }
    
    public string? FlashFiring { get; set; }
    
    public string? MeteringMode { get; set; }

    public string? ProjectionType { get; set; }

    [JsonPropertyName("prefix")]
    public string? Prefix { get; set; }

    [JsonPropertyName("keywords")] 
    public string[] Keywords { get; set; } = [];
    
    [JsonPropertyName("rating")]
    public StarRating? Rating { get; set; }
    
    [JsonPropertyName("writable")]
    public string? Writable { get; set; }
    
    public string? Dimension { get; set; }
    
    [JsonPropertyName("uid")]    
    public string? UId { get; set; }
    
    public DateOnly ImportYearMonthDay { get; set; }
}


