using UnityEngine;
public enum BoosterTypes
{
    Shield,
    Nitro,
    Magnet,
    Heart
}

[CreateAssetMenu(fileName = "BoosterParameter", menuName = "ScriptableObjects/BoosterParameter", order = 2)]
public class BoosterParameters : ScriptableObject
{
    public BoosterTypes BoosterType;
    public Sprite BoosterSprite;
}
