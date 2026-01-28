public interface IResourceDeliverer
{
    public void PlaceResourceInStorage(ICollectable storage);
    public ICollectable ReleaseResource();
}