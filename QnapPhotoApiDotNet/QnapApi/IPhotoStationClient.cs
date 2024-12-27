namespace RobGray.QnapPhotoApiDotNet.QnapApi;

using List;
using ListAlbumPhotos;
using ListAlbums;

public interface IPhotoStationClient
{
    public Task<ListResponse?> ListAsync(ListRequest request, CancellationToken cancellationToken);
    
    public Task<ListAlbumsResponse> ListAlbumsAsync(ListAlbumsRequest request, CancellationToken cancellationToken);

    public Task<ListAlbumPhotosResponse> ListAlbumPhotosAsync(ListAlbumPhotosRequest request, CancellationToken cancellationToken);
}