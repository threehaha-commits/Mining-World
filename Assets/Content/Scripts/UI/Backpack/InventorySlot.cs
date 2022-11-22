using UnityEngine;

public class InventorySlot : Slot, ISlotAdder
{
    private void Start()
    {
        SetSlot(this);
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