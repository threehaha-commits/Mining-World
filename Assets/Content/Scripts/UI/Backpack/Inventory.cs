using UnityEngine;

public class Inventory : MonoBehaviour, IInitialize
{
        private InventorySlot[] _slots;

        void IInitialize.Initialize()
        {
                _slots = GetComponentsInChildren<InventorySlot>();
                Bind<Inventory>.Value(this);
        }
        
        public void AddItem(ISlot item)
        {
                var itemImage = item._icon;
                if (InventoryHelper.Contains(_slots, itemImage, out var index))
                {
                        if (_slots[index].IsFull)
                                FindSuitableSlot(item);
                        else
                                _slots[index].Increase();
                        return;
                }
                AddItemToEmptySlot(item);
        }
        
        public void RemoveItem(int index)
        {
                if(_slots[index] != null)
                        _slots[index].RemoveSlot();
        }
        
        private void FindSuitableSlot(ISlot item)
        {
                foreach (var slot in _slots)
                {
                        if (slot.IsEmpty())
                        {
                                AddItemToEmptySlot(item);
                                break;
                        }
                }
        }

        private void AddItemToEmptySlot(ISlot item)
        {
                for (var i = 0; i < _slots.Length; i++)
                {
                        if (_slots[i].IsEmpty())
                        {
                                ISlotAdder changer = _slots[i];
                                changer.Add(item);
                                break;
                        }
                }
        }
}