using UnityEngine;

public class Cell
{
    public SpriteRenderer Renderer;
    public readonly GameObject GameObject;
    public bool Empty = true;
    public readonly Vector2 Position;
    private Sprite _sprite;
    public readonly Transform Transform;
    
    public Sprite Sprite
    {
        set => Renderer.sprite = value;
    }

    public Cell(SpriteRenderer renderer)
    {
        Renderer = renderer;
        Position = renderer.transform.position;
        Sprite = renderer.sprite;
        GameObject = renderer.gameObject;
        Transform = renderer.transform;
    }
}