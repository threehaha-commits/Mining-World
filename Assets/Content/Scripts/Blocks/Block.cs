using UnityEngine;

public class Block : MonoBehaviour
{
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
    }
}