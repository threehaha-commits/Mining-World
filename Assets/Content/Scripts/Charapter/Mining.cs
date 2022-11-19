using UnityEngine;

public class Mining : MonoBehaviour
{
    [Inject] private BlockClicker _blockClicker;
    [Inject] private Backpack _backpack;
    
    public void DoMining()
    {
        if (_blockClicker.GetBlock() == null)
            return;
        
        var block = _blockClicker.GetBlock();
        block.blockHit.Hit();
        var blockComponent = block.GetComponent<Block>();
        _backpack.AddItem(blockComponent);
        AnimHandler.SetMining(false);
    }
}