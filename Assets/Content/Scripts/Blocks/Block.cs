using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : Consumable, IConsumable, ISlotTypeable
{
    [SerializeField] private Sprite[] _blockSprite;
    [SerializeField] private Ore _oreType;
    public Ore oreType => _oreType;
    private SpriteRenderer _renderer;
    public SpriteRenderer spriteRenderer => _renderer;
    private Collider2D _collider2D;
    public Collider2D colliders2d => _collider2D;
    private BlockHit _blockHit;
    private SlotType _slotType;
    public BlockHit blockHit => _blockHit;
    
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _blockHit = GetComponent<BlockHit>();
        var random = Random.Range(0, _blockSprite.Length);
        _renderer.sprite = _blockSprite[random];
    }

    Sprite Iitem._icon => _renderer.sprite;

    Consumable IConsumable.consumable
    {
        get => this;
        set => throw new NotImplementedException();
    }

    string Iitem._name => gameObject.name;

    SlotType ISlotTypeable.slotType => SlotType.Consumable;
}