using System.Collections.Generic;
using UnityEngine;

public class ObjectPullMineral : ObjectPull<Mineral>
{
    [SerializeField] private List<MineralConfig> _configs  = new List<MineralConfig>();

    private Dictionary<TypesMinerals, MineralConfig> _configsDictionary = new Dictionary<TypesMinerals, MineralConfig>();

    public override void Initialization()
    {
        base.Initialization();

        for (int i = 0; i < _configs.Count; i++)
        {
            TypesMinerals type = _configs[i].Type;
            _configsDictionary[type] = _configs[i];
        }
    }

    public Mineral GetMineral(TypesMinerals type)
    {
        Mineral mineral = GetObject();

        if (mineral.Config != null && mineral.Config.Type == type)
            return mineral;

        mineral.SetConfig(_configsDictionary.GetValueOrDefault(type));

        return mineral;
    }

    public void PutMineral(Mineral mineral)
    {
        PutObject(mineral);
    }
}