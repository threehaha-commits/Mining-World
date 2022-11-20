public class CraftSlot : Slot
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
}