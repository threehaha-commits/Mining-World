using UnityEngine;

public class Updater : MonoBehaviour, IInitialize
{
    [Inject] private RecipeFinder _recipeFinder;
    
    public void UpdateSlot(Slot slot, Slot root)
    {
            var isPointerOverSlot = PointerHelper.PointerOverUI<Slot>(out var slotResult);
            if (isPointerOverSlot)
            {
                if(slotResult.IsEmpty())
                {
                    ISlotChanger changer = slotResult;
                    changer.ChangeSlot(slot);
                    root.RemoveSlot();
                }
                else
                {
                    if(slot.GetOre() == slotResult.GetOre())
                    {
                        if(slotResult == root) // Защита от клика
                        {
                            slot.RemoveSlot();
                            return;
                        }
                        slotResult.Increase(slot.GetStack().Size());
                        root.RemoveSlot();
                    }
                    _recipeFinder.UpdateItems();
                }
            }
    }
    
    void IInitialize.Initialize()
    {
        Bind<Updater>.Value(this);
    }
}