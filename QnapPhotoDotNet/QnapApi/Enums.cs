namespace RobGray.QnapPhotoDotNet.QnapApi;

public enum MediaType
{
    All,
    Photos,
    Videos,
}

public enum Collection
{
    Shared = 0,
    Private = 1,
};

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

public enum AlbumType
{
    All,
    Manual,
    Smart
}

public enum AlbumSort
{
    Title
}