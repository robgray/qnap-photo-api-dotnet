namespace RobGray.QnapPhotoApiDotNet.QnapApi.ListAlbumPhotos;

public class ListAlbumPhotosRequest
{
    public required string AlbumId { get; set; }
    
    public Collection Collection { get; set; } = Collection.Shared;
    
    public int PageSize { get; set; } = 100;    // matches the page size of the Qnap Photo App

    public int PageNumber { get; set; } = 1;

    public Sort Sort { get; set; } = Sort.DateAdded;

    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
    
    public override string ToString()
    {
        var searchParams = $"h={CollectionParam()}&json=1&t={MediaTypeParam()}&a={AlbumIdParam()}&sd={SortDirectionParam()}&c={PageSizeParam()}&p={PageNumberParam()}&s={SortParam()}";

        return searchParams;
        
        string CollectionParam() => this.Collection switch
        {
            Collection.Shared => "0",
            Collection.Private => "1",
            _ => "0",
        };

        string AlbumIdParam() => $"{AlbumId}";

        string MediaTypeParam() => "albumPhotos";

        string PageNumberParam() => $"{this.PageNumber}";

        string PageSizeParam() => $"{this.PageSize}";
        
        string SortParam() => this.Sort switch
        {
            Sort.Size => "size",
            Sort.DateAdded => "time",
            _ => "rating",
        };
        
        string SortDirectionParam() => this.SortDirection switch
        {
            SortDirection.Descending => "DESC",
            _ => "ASC",
        };

       
    }
}