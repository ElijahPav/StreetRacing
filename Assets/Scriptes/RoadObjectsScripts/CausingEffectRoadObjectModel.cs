using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CausingEffectRoadObjectModel<T>: MonoBehaviour where T : System.Enum
{
    protected abstract T[] GetAllTypes { get; }
    public T GetRandomType()
    {
        var types = GetAllTypes;
        return types[Random.Range(0, types.Count())];
    }
    public abstract Sprite GetSpriteByType(T type);
}
