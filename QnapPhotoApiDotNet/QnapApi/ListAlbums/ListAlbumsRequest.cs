namespace RobGray.QnapPhotoApiDotNet.QnapApi.ListAlbums;

public class ListAlbumsRequest
{
    public Collection Collection { get; set; } = Collection.Shared;
    
    public AlbumType AlbumType { get; set; } = AlbumType.All;
    
    public AlbumSort Sort { get; set; } = AlbumSort.Title;
    
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
    
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 50;
    
    public override string ToString()
    {
        var searchParams = $"json=1&t={AlbumTypeParam()}&h={CollectionParam()}&s={AlbumSortParam()}&sd={SortDirectionParam()}&c={PageSizeParam()}&p={PageNumberParam()}";
        return searchParams;

        string AlbumSortParam() => this.Sort switch
        {
            AlbumSort.Title => "name",
            // Currently only 1 option
            _ => "name",
        };
        
        string PageNumberParam() => $"{this.PageNumber}";

        string PageSizeParam() => $"{this.PageSize}";
        
        string CollectionParam() => this.Collection switch
        {
            Collection.Shared => "0",
            Collection.Private => "1",
            _ => "0",
        };
        
        string SortDirectionParam() => this.SortDirection switch
        {
            SortDirection.Descending => "DESC",
            _ => "ASC",
        };

        string AlbumTypeParam() => this.AlbumType switch
        {
            AlbumType.All => "allAlbums",
            AlbumType.Manual => "albums",
            AlbumType.Smart => "smartAlbums",
            _ => "allAlbums",
        };
    }
}