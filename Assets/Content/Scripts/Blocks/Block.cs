using UnityEngine;

public class Block : MonoBehaviour, IOreSlot
{
    [SerializeField] private Sprite[] _blockSprite;
    [SerializeField] private Ore _oreType;
    public Ore oreType => _oreType;
    private SpriteRenderer _renderer;
    public SpriteRenderer spriteRenderer => _renderer;
    private Collider2D _collider2D;
    public Collider2D colliders2d => _collider2D;
    private BlockHit _blockHit;
    public BlockHit blockHit => _blockHit;
    
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _blockHit = GetComponent<BlockHit>();
        var random = Random.Range(0, _blockSprite.Length);
        _renderer.sprite = _blockSprite[random];
    }

    Sprite ISlot._icon
    {
        get => _renderer.sprite;
        set => _renderer.sprite = value;
    }

    Ore IOreSlot._oreType
    {
        get => _oreType;
        set => _oreType = value;
    }
}