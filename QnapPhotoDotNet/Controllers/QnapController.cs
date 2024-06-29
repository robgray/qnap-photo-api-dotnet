using Microsoft.AspNetCore.Mvc;
using RobGray.QnapPhotoDotNet.Core.QnapApi;

namespace RobGray.QnapPhotoDotNet.Controllers;

[ApiController]
public class QnapController(IQnapApiClient qnapClient) : ControllerBase
{
    [HttpGet("list/{page}")]
    public async Task<ActionResult<ListResponse>> GetList(int page)
    {
        var request = new ListRequest
        {
            MediaType = MediaType.Photos,
            SortDirection = SortDirection.Descending,
            StarRating = StarRating.Five,
            PageNumber = page,
            PageSize = 100,  
        };
        return await qnapClient.List(request);
    }
}