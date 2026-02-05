using UnityEngine;

public class Storage : MonoBehaviour
{
    private ICollectable _item;

    public ICollectable Item => _item;

    public void SetItem(ICollectable collectable)
    {
        if (collectable == null)
            return;

        _item = collectable;
    }

    public void Clear()
    {
        if (_item == null)
            return;

        _item = null;
    }
}