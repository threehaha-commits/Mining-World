using UnityEngine;

public class Inventory : MonoBehaviour, IInitialize
{
        private InventorySlot[] _slots;

        void IInitialize.Initialize()
        {
                _slots = GetComponentsInChildren<InventorySlot>();
                Bind<Inventory>.Value(this);
        }
        
        public void AddItem(Iitem item)
        {
                var itemImage = item._icon;
                if (InventoryHelper.Contains(_slots, itemImage, out var index))
                {
                        if(_slots[index].GetSlotType().Equals(SlotType.Consumable))
                        {
                                IStackable stackable = _slots[index];
                                if (stackable.isFull)
                                        FindSuitableSlot(item);
                                else
                                {
                                        IIncreasable increasable = _slots[index];
                                        increasable.Increase();
                                }
                                return;
                        }
                }
                AddItemToEmptySlot(item);
        }
        
        public void RemoveItem(int index)
        {
                if(_slots[index] != null)
                        _slots[index].RemoveSlot();
        }
        
        private void FindSuitableSlot(Iitem item)
        {
                foreach (var slot in _slots)
                {
                        IItemAvailable available = slot;
                        if (available.IsAvailable)
                        {
                                AddItemToEmptySlot(item);
                                break;
                        }
                }
        }

        private void AddItemToEmptySlot(Iitem item)
        {
                for (var i = 0; i < _slots.Length; i++)
                {
                        IItemAvailable available = _slots[i];
                        if (available.IsAvailable)
                        {
                                ISlotAdder changer = _slots[i];
                                changer.Add(item);
                                break;
                        }
                }
        }
}