using UnityEngine;

public class CollectorBotTaker : Taker
{
    [SerializeField] private Storage _storage;

    public override void PlaceResourceInStorage(ICollectable item)
    {
        _storage.SetItem(item);

        item.Transform.SetParent(_storage.transform);
        item.Transform.position = Vector3.zero;
    }

    public override ICollectable ReleaseResource()
    {
        ICollectable collectable = _storage.Item;
        ClearStorag();

        return collectable;
    }

    protected override void ClearStorag()
    {
        if (_storage == null)
            return;

        _storage.transform.SetParent(null);
        _storage.Clear();
    }
}
