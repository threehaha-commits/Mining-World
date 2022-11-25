using UnityEngine;

public class InventorySlot : Slot, ISlotAdder, IInitialize
{
    void ISlotAdder.Add(ISlot slot)
    {
        switch (slot)
        {
          case IOreSlot oreSlot:
              _oreType = oreSlot._oreType;
              _image.sprite = oreSlot._icon;
              _stack = new Stack();
              _slotType = SlotType.Consumable;
              Increase();
              break;
          case IItemSlot itemSlot:
              _image.sprite = itemSlot._icon;
              _slotType = SlotType.NonCosumabe;
              break;
        }
    }

    public bool Contains(Sprite sprite)
    {
        return _image.sprite == sprite;
    }

    void IInitialize.Initialize()
    {
        SetSlot(this);
    }
}