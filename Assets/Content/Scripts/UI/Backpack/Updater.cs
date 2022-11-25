using UnityEngine;

public class Updater : MonoBehaviour, IInitialize
{
    [Inject] private RecipeFinder _recipeFinder;
    
    public void UpdateSlot(Slot draggerSlot, Slot originalSlot)
    {
            var isPointerOverSlot = PointerHelper.PointerOverUI<Slot>(out var slotUnderMouse); //Если слот перенесли на UI, то получаем его
            if (isPointerOverSlot)
            {
                //Если слот, в который мы хотим переместить пуст, то меняем его
                if(slotUnderMouse.IsEmpty())
                {
                    ChangeSlot(draggerSlot, originalSlot, slotUnderMouse);
                }
                else
                {
                    if (SlotIsConsumable(draggerSlot))
                        IncreaseValueToSlot(draggerSlot, originalSlot, slotUnderMouse);
                    
                    _recipeFinder.UpdateItems();
                }
            }
    }

    private bool SlotIsConsumable(Slot slot)
    {
        return slot.GetSlotType() == SlotType.Consumable;
    }

    private void IncreaseValueToSlot(Slot slot, Slot root, Slot slotResult)
    {
        if (slotResult == root) // Защита от удаления кликом
        {
            slot.RemoveSlot();
            return;
        }

        slotResult.Increase(slot.GetStack().Size());
        root.RemoveSlot();
    }

    private void ChangeSlot(Slot slot, Slot root, Slot slotResult)
    {
        ISlotChanger changer = slotResult;
        changer.ChangeSlot(slot);
        root.RemoveSlot(); //Обязательно удаляем оригинал!
    }

    void IInitialize.Initialize()
    {
        Bind<Updater>.Value(this);
    }
}