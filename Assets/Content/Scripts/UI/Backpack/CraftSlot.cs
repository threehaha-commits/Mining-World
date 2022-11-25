public class CraftSlot : Slot, IInitialize
{
    [Inject] private RecipeFinder _itemCrafter;
    
    public override void ChangeSlot(Slot slot)
    {
        base.ChangeSlot(slot);
        _itemCrafter?.Add(this);
    }

    public override void RemoveSlot()
    {
        _itemCrafter?.Remove(this); 
        base.RemoveSlot();
        _itemCrafter?.UpdateItems();
    }

    void IInitialize.Initialize()
    {
        SetSlot(this);
    }
}