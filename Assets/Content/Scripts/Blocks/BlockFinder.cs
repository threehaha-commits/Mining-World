using System.Collections;
using UnityEngine;

public class BlockFinder : MonoBehaviour, IInitialize
{
    private const float _radius = 1.2f;
    private Collider2D[] _blocks;
    private const float _findTime = 0.17f;
    private Vector2 _PlayerPosition => _player.transform.position;
    [Inject] private Player _player;
    public Collider2D[] blocks => _blocks;
    public float FindTime => _findTime;
    
    void IInitialize.Initialize()
    {
        StartCoroutine(Find());
    }
    
    private IEnumerator Find()
    {
        while (true)
        {
            _blocks = GetBlocks();
            CheckBlock();
            yield return new WaitForSeconds(FindTime);
        }
    }

    private Collider2D[] GetBlocks()
    {
        var layerMask = LayerMask.GetMask("Mining");
        return Physics2D.OverlapCircleAll(_PlayerPosition, _radius, layerMask);
    }

    private void CheckBlock()
    {
        foreach (var block in _blocks)
        {
            if (block.TryGetComponent<BlockReceiver>(out var blockReceiver))
                blockReceiver.BlockFound(FindTime, this);
        }
    }
}