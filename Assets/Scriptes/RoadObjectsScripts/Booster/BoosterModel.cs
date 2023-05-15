using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterModel :CausingEffectRoadObjectModel<BoosterTypes>
{
    public BoosterView BoosterPrefab;

    [SerializeField] private List<BoosterParameters> _boosterParameters;
    private Dictionary<BoosterTypes, Sprite> _boosterSpries;

    protected override BoosterTypes[] GetAllTypes => _boosterSpries.Keys.ToArray();

    private void Awake()
    {
        _boosterSpries = new Dictionary<BoosterTypes, Sprite>();
        foreach (var parameter in _boosterParameters)
        {
            _boosterSpries.Add(parameter.BoosterType, parameter.BoosterSprite);
        }
    }

    public override Sprite GetSpriteByType(BoosterTypes type)
    {
        return _boosterSpries[type];
    }
}
