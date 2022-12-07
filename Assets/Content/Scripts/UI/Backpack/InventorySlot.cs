public class InventorySlot : NonEquipSlot, ISlotAdder, IInitialize
{
    private IIncreasable _increasable;
    private ISlotable _slotable;
    
    private void Start()
    {
        _increasable = this;
        _slotable = this;
    }

    //При добавлении предмета мы смотрим его тип: расходуемый и не расходуемый
    void ISlotAdder.Add(Iitem slot)
    {
        var typeable = slot as ISlotTypeable;
        if(typeable.slotType == SlotType.Consumable)
        {
            var consumableItem = slot as IConsumable;
            _slotable.consumable = consumableItem.consumable;
            _stack = new Stack();
            _increasable.Increase();
        }
        _image.sprite = slot._icon;
        _slotType = typeable.slotType;
        gameObject.name = slot._name;
    }

    public override void RemoveSlot()
    {
        base.RemoveSlot();
        gameObject.name = "Inventory_Slot";
    }

    void IInitialize.Initialize()
    {
        SetSlot(this);
    }
}