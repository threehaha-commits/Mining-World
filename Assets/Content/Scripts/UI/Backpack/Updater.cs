using UnityEngine;

public class Updater : MonoBehaviour, IInitialize
{
    public void UpdateSlot(Slot slot, Slot root)
    {
            var isPointerUnder = PointerHelper.PointerOverUI<CraftSlot>(out var result);
            if (isPointerUnder)
            {
                if(result.IsEmpty())
                {
                    ISlotChanger changer = result;
                    changer.ChangeSlot(slot);
                    root.RemoveSlot();
                }
                else
                {
                    if(slot.GetOre() == result.GetOre())
                    {
                        result.Increase(slot.GetStack().Size());
                        root.RemoveSlot();
                    }
                }
            }
    }
    
    void IInitialize.Initialize()
    {
        Bind<Updater>.Value(this);
    }
}