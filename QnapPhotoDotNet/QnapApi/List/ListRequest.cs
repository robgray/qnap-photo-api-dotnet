namespace RobGray.QnapPhotoDotNet.QnapApi.List;

using RobGray.QnapPhotoDotNet.QnapApi;

public class ListRequest
{
    public Collection Collection { get; set; } = Collection.Shared;
    
    public MediaType MediaType { get; set; } = MediaType.All;

    public int PageSize { get; set; } = 100;    // matches the page size of the Qnap PhotoStation App

    public int PageNumber { get; set; } = 1;

    public Sort Sort { get; set; } = Sort.Rating;

    public SortDirection SortDirection { get; set; } = SortDirection.Descending;

    public StarRating? Rating { get; set; } = null;
    
    public override string ToString()
    {
        var searchParams = $"h={CollectionParam()}&json=1&t={MediaTypeParam()}&sd={SortDirectionParam()}&c={PageSizeParam()}&p={PageNumberParam()}&s={SortParam()}";
        if (this.Rating is not null)
        {
            searchParams += $"&m={StarRatingParam()}";
        }

        return searchParams;
        
        string MediaTypeParam() => this.MediaType switch
        {
            MediaType.Photos => "photos",
            MediaType.Videos => "videos",
            _ => "allMedia",
        };

        string CollectionParam() => this.Collection switch
        {
            Collection.Shared => "0",
            Collection.Private => "1",
            _ => "0",
        };
        
        string PageNumberParam() => $"{this.PageNumber}";

        string PageSizeParam() => $"{this.PageSize}";
        
        string SortParam() => this.Sort switch
        {
            Sort.Size => "size",
            Sort.DateAdded => "time",
            _ => "time",
        };
        
        string SortDirectionParam() => this.SortDirection switch
        {
            SortDirection.Descending => "DESC",
            _ => "ASC",
        };

        string StarRatingParam() => this.Rating switch
        {
            StarRating.One => "20",
            StarRating.Two => "40",
            StarRating.Three => "60",
            StarRating.Four => "80",
            StarRating.Five => "100",
            _ => "",
        };
    }
}



