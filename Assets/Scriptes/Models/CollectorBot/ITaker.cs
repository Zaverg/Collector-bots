public interface ITaker
{
    public void PlaceResourceInStorage(ICollectable collectable);
    public ICollectable ReleaseResource();
}