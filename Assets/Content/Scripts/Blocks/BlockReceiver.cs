using System.Collections;
using System.Linq;
using UnityEngine;

public class BlockReceiver : MonoBehaviour, IInitialize
{
    private Block _block;
    private bool _isRunning;
    private Collider2D[] _blocks;
    
    void IInitialize.Initialize()
    {
        _block = GetComponent<Block>();
    }
    
    public void BlockFound(float findTime, BlockFinder blockFinder)
    {
        if (_isRunning)
            return;
        _isRunning = true;
        StartCoroutine(Run(findTime, blockFinder));
    }

    private IEnumerator Run(float findTime, BlockFinder blockFinder)
    {
        while (_isRunning)
        {
            if (CharapterIsContainBlock(blockFinder))
            {
                _block.spriteRenderer.color = Color.red;
            }
            else
            {
                _block.spriteRenderer.color = Color.white;
                _isRunning = false;
            }
            yield return new WaitForSeconds(findTime);
        }
    }

    private bool CharapterIsContainBlock(BlockFinder blockFinder)
    {
        var colliders = blockFinder.blocks;
        return colliders.Contains(_block.colliders2d);
    }
}