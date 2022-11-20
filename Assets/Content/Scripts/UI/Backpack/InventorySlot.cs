using UnityEngine;

public class InventorySlot : Slot, ISlotAdder
{
    private void Start()
    {
        SetSlot(this);
    }

    public override void ChangeSlot(Slot slot)
    {
        _oreType = slot.GetOre();
        _image.sprite = slot.GetSprite();
        _stack = slot.GetStack();
        PrintSize();
    }

    void ISlotAdder.AddSlot(Sprite image, Ore oreType)
    {
        _oreType = oreType;
        _image.sprite = image;
        _stack = new Stack();
        Increase();
    }

    public bool Contains(SpriteRenderer image)
    {
        return _image.sprite == image.sprite;
    }
}