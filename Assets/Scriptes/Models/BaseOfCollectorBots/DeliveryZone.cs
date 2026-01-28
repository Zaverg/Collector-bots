using System;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public event Action<ICollectable> ResourceDelivered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IResourceDeliverer resourceDeliverer))
        {
            ICollectable collectable = resourceDeliverer.ReleaseResource();

            if (collectable != null)
                ResourceDelivered?.Invoke(collectable);
        }
    } 
}
