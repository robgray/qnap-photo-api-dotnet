namespace QnapPhotoApi.Tests;

using System.Text.Json;
using System.Text.Json.Serialization;
using Ragware.QnapPhotoApi.QnapApi;
using Ragware.QnapPhotoApi.QnapApi.Converters;
using Ragware.QnapPhotoApi.QnapApi.List;
using Shouldly;

public class ListResponseSerializationTests
{
    private JsonSerializerOptions _options;
    
    public ListResponseSerializationTests()
    {
        _options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new QnapDateTimeJsonConverter(),
                new QnapStringArrayJsonConverter(),
                new QnapDateOnlyJsonConverter(),
                new QnapStartRatingJsonConverter(),
            }
        };
    }
    
    [Fact]
    public void WhenFieldsAreFilled_ThenCanBeDeserialized()
    {
        const string jsonResponse = """{"status":"0","photoCount":"16492","DataList":[{"FileItem":{"MediaType":"photo","id":"fWQJEk","cFileName":"pure-pleasure-v0-lllr7dwp7k8e1.jpg","cPictureTitle":"pure-pleasure-v0-lllr7dwp7k8e1","comment":"","mime":"image\/jpeg","iFileSize":"2071098","iWidth":"3667","iHeight":"5500","YearMonth":"2024-12","YearMonthDay":"2024-12-26","dateTime":"2024-12-26 10:14:48","DateCreated":"2024-12-26 10:16:31","DateModified":"2024-12-26 10:14:48","AddToDbTime":"2024-12-26 10:16:36","LastUpdate":"2024-12-26 10:17:18","ScannedFlag":"1","ColorLevel":"0","longitude":"","latitude":"","location":null,"Orientation":"1","ProtectionStatus":"0","lensInfo":"","Aperture":"","Exposure":"","ISO":"","Maker":"","Model":"","FocalLength":"","WhiteBalance":"","FlashFiring":"","MeteringMode":"","ProjectionType":"0","prefix":"Multimedia\/Images\/Adult\/2024\/","keywords":"test1;test2;test3","rating":"100","writable":"-1","Dimension":"3667 X 5500","uid":"pfWQJEk","ImportYearMonthDay":"2024-12-26"}}],"timestamp":"2024-12-29 08:05:52"}""";

        var response = JsonSerializer.Deserialize<ListResponse>(jsonResponse, _options);

        response.ShouldNotBeNull();
        response.Data.ShouldNotBeNull().Length.ShouldBe(1);
        
        response.Status.ShouldBe("0");
        response.TotalItems.ShouldBe(16492);
        response.Timestamp.ShouldBe(DateTime.Parse("2024-12-29 08:05:52"));

        var fileItem = response.Data[0].FileItem.ShouldNotBeNull();
        fileItem.Keywords.ShouldNotBeNull().Length.ShouldBe(3);
        fileItem.Keywords[0].ShouldBe("test1");
        fileItem.Keywords[1].ShouldBe("test2");
        fileItem.Keywords[2].ShouldBe("test3");
        fileItem.DateCreated.ShouldBe(DateTime.Parse("2024-12-26 10:16:31"));
        fileItem.DateModified.ShouldBe(DateTime.Parse("2024-12-26 10:14:48"));
        fileItem.DateTime.ShouldBe(DateTime.Parse("2024-12-26 10:14:48"));
        fileItem.AddToDbTime.ShouldBe(DateTime.Parse("2024-12-26 10:16:36"));
        fileItem.LastUpdate.ShouldBe(DateTime.Parse("2024-12-26 10:17:18"));
        fileItem.FileSize.ShouldBe(2071098);
        fileItem.Width.ShouldNotBeNull().ShouldBe(3667);
        fileItem.Height.ShouldNotBeNull().ShouldBe(5500);
        fileItem.Rating.ShouldNotBeNull().ShouldBe(StarRating.Five);
    }
}