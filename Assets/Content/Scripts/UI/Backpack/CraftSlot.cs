public class CraftSlot : Slot
{
    [Inject] private RecipeFinder _itemCrafter;
    
    private void Start()
    {
        SetSlot(this);
    }

    public override void ChangeSlot(Slot slot)
    {
        base.ChangeSlot(slot);
        _itemCrafter?.Add(this);
    }

    public override void RemoveSlot()
    {
        _itemCrafter?.Remove(this); 
        base.RemoveSlot();
        _itemCrafter.UpdateItems();
    }
}