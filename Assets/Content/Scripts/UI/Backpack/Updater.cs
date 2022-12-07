using UnityEngine;

public class Updater : MonoBehaviour, IInitialize
{
    [Inject] private RecipeFinder _recipeFinder;
    
    public void UpdateSlot(ISlotable draggerSlot, ISlotable originalSlot)
    {
        var isPointerOverSlot = PointerHelper.PointerOverUI<ISlotable>(out var slotUnderMouse); //Если слот перенесли на UI, то получаем его
        if (isPointerOverSlot)
        {
            //Если слот, в который мы хотим переместить пуст, то меняем его
            IItemAvailable available = slotUnderMouse as IItemAvailable;
            var typeabe1 = slotUnderMouse as ISlotTypeable;
            var typeabe2 = originalSlot as ISlotTypeable;
            if(available.IsAvailable && (typeabe1.slotType == SlotType.Empty || typeabe1.slotType == typeabe2.slotType))
            {
                ChangeSlot(draggerSlot, originalSlot, slotUnderMouse);
            }
            else
            {
                if (SlotIsConsumable(originalSlot, slotUnderMouse))
                    IncreaseValueToSlot(draggerSlot, originalSlot, slotUnderMouse);
                    
                _recipeFinder.UpdateItems();
            }
        }
    }

    private bool SlotIsConsumable(ISlotable slot, ISlotable slotUnderMouse)
    {
        var slotTypeable1 = slot as ISlotTypeable;
        var slotTypeable2 = slotUnderMouse as ISlotTypeable;
        if (slotTypeable1.slotType != SlotType.Consumable || slotTypeable2.slotType != SlotType.Consumable)
            return false;
        var a = slot.name;
        var b = slotUnderMouse.name;
        return a.Equals(b);
    }

    private void IncreaseValueToSlot(ISlotable slot, ISlotable root, ISlotable slotResult)
    {
        if (slotResult == root) // Защита от удаления кликом
        {
            slot.RemoveSlot();
            return;
        }

        IIncreasable increasable = slotResult as IIncreasable;
        IStackable stackable = slot as IStackable;
        increasable.Increase(stackable.stackSize);
        root.RemoveSlot();
    }

    private void ChangeSlot(ISlotable slot, ISlotable root, ISlotable slotResult)
    {
        ISlotChanger changer = slotResult as ISlotChanger;
        changer.ChangeSlot(slot);
        root.RemoveSlot(); //Обязательно удаляем оригинал!
    }

    void IInitialize.Initialize()
    {
        Bind<Updater>.Value(this);
    }
}