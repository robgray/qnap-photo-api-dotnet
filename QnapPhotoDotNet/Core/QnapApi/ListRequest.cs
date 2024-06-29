namespace RobGray.QnapPhotoDotNet.Core.QnapApi;

public class ListRequest
{
    public MediaType MediaType { get; set; } = MediaType.All;

    public int PageSize { get; set; } = 100;    // matches the page size of the Qnap Photo App

    public int PageNumber { get; set; } = 1;

    public Sort Sort { get; set; } = Sort.Rating;

    public SortDirection SortDirection { get; set; } = SortDirection.Descending;

    public StarRating? StarRating { get; set; } = null;
    
    public override string ToString()
    {
        // h: 1 for private collection, 0 = shared.
        var searchParams = $"h=1&json=1&t={MediaTypeParam()}&sd={SortDirectionParam()}&c={PageSizeParam()}&p={PageNumberParam()}&s={SortParam()}";
        if (this.StarRating is not null)
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

        string StarRatingParam() => this.StarRating switch
        {
            QnapApi.StarRating.One => "20",
            QnapApi.StarRating.Two => "40",
            QnapApi.StarRating.Three => "60",
            QnapApi.StarRating.Four => "80",
            QnapApi.StarRating.Five => "100",
            _ => "",
        };
    }
}

public enum MediaType
{
    All,
    Photos,
    Videos,
}

public enum Sort
{
    Size,
    Rating,
    DateAdded,
}

public enum SortDirection
{
    Ascending,
    Descending
}

public enum StarRating
{
    One,
    Two,
    Three,
    Four,
    Five
}

