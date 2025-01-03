﻿namespace Ragware.QnapPhotoApi.QnapApi.ListAlbums;

#nullable disable
using System.Text.Json.Serialization;

public class ListAlbumsResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("DataList")]
    public DataList[] Data { get; set; } = [];
    
    [JsonPropertyName("KeywordList")]
    public KeywordList[] Keywords { get; set; } = [];
    
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
}

public class DataList
{
    public FileItem FileItem { get; set; }
}

public class FileItem
{
    [JsonPropertyName("iPhotoAlbumId")]
    public string PhotoAlbumId { get; set; }
    
    [JsonPropertyName("cAlbumTitle")]
    public string AlbumTitle { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime DateModified { get; set; }
    
    [JsonPropertyName("albumType")]
    public string AlbumType { get; set; }

    public int PhotoCount { get; set; }
    
    public int VideoCount { get; set; }
}

public class KeywordList
{
    private KeywordItem Item { get; set; }
}

public class KeywordItem
{
    public string Keyword { get; set; }
}


