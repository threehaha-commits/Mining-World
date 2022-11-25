using System;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [Inject] private BlockClicker _blockClicker;
    [Inject] private Inventory _backpack;
    private Anim _anim;

    private void Start()
    {
        _anim = GetComponent<Anim>();
    }

    public void DoMining()
    {
        if (_blockClicker.GetBlock() == null)
            return;
        
        var block = _blockClicker.GetBlock();
        block.blockHit.Hit();
        var blockComponent = block.GetComponent<Block>();
        _backpack.AddItem(blockComponent);
        _anim.Get().SetBool("Mining", false);
        _anim.Get().SetLayerWeight(1, 0f);
    }
}