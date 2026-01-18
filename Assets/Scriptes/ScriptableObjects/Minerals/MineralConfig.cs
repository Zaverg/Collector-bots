using UnityEngine;

[CreateAssetMenu(fileName = "MineralConfig", menuName = "Scriptable Objects/MineralConfig")]
public class MineralConfig : ScriptableObject
{
    [SerializeField] private TypesMinerals _type;

    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    [SerializeField] private float _collectionTime;
    [SerializeField] private float _rarity;

    public TypesMinerals Type => _type;

    public Mesh Mesh => _mesh;
    public Material Material => _material;

    public float CollectionTime => _collectionTime;
    public float Rarity => _rarity;
}
