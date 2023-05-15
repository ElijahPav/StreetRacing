using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleModel : CausingEffectRoadObjectModel<ObstacleTypes>
{
    public ObstacleView ObstaclePrefab;

    [SerializeField] private List<ObstacleParameters> _obstacleParameters;
    private Dictionary<ObstacleTypes, List<Sprite>> _obstalesSpries;

    protected override ObstacleTypes[] GetAllTypes => _obstalesSpries.Keys.ToArray();

    private void Awake()
    {
        _obstalesSpries = new Dictionary<ObstacleTypes, List<Sprite>>();
        foreach (var parameter in _obstacleParameters)
        {
            _obstalesSpries.Add(parameter.ObstacleType, parameter.ObstacleSprites);
        }
    }


    public override Sprite GetSpriteByType(ObstacleTypes type)
    {
       var sprites = _obstalesSpries[type];
       return sprites.Count == 1 ?
               sprites[0] :
               sprites[Random.Range(0, sprites.Count)];
    }
}
