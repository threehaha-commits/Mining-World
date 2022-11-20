using UnityEngine;

public class Inventory : MonoBehaviour, IInitialize
{
        private InventorySlot[] _slots;
        
        void IInitialize.Initialize()
        {
                _slots = GetComponentsInChildren<InventorySlot>();
                Bind<Inventory>.Value(this);
        }

        public void AddItem(Block item)
        {
                var itemImage = item.GetComponent<SpriteRenderer>();
                if (Contains(itemImage, out var index))
                {
                        if (_slots[index].IsFull)
                                FindEmptySlot(item);
                        else
                                _slots[index].Increase();
                        return;
                }
                AddItemToEmptySlot(item);
        }

        private void FindEmptySlot(Block item)
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

        private void AddItemToEmptySlot(Block item)
        {
                for (var i = 0; i < _slots.Length; i++)
                {
                        if (_slots[i].IsEmpty())
                        {
                                ISlotAdder changer = _slots[i];
                                changer.AddSlot(item.spriteRenderer.sprite, item.oreType);
                                break;
                        }
                }
        }

        private bool Contains(SpriteRenderer image, out int index)
        {
                for (int i = 0; i < _slots.Length; i++)
                {
                        if (_slots[i].Contains(image))
                        {
                                if(_slots[i].IsFull == false)
                                {
                                        index = i;
                                        return true;
                                }
                        }
                }

                index = -1;
                return false;
        }
}