using UnityEngine;

public abstract class RoadObjectView<T> : MonoBehaviour where T : System.Enum
{
    [HideInInspector] public T ObjectType;

    [SerializeField] private BoxCollider2D _colliderComponent;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void SetObjectData(T objectType, Sprite objectSprite)
    {
        ObjectType = objectType;
        _spriteRenderer.sprite = objectSprite;
        _colliderComponent.size = _spriteRenderer.size;
    }
}
