using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObstacleTypes
{
    Block,
    OilPuddle,
    Crack
}

[CreateAssetMenu(fileName = "ObstacleParameter", menuName = "ScriptableObjects/ObstacleParameter", order = 1)]
public class ObstacleParameters : ScriptableObject
{
    public ObstacleTypes ObstacleType;
    public List<Sprite> ObstacleSprites;
}
