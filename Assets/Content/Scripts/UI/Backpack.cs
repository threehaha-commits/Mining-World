using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour, IInitialize
{
        [Inject] private BackpackSlotsFinder _viewer;
        private readonly List<Slot> _slots = new ();
        
        void IInitialize.Initialize()
        {
                var images = _viewer.GetSlots();
                foreach (var img in images)
                {
                        _slots.Add(new Slot(img, true, _viewer.GetText()));
                }
        }

        public void AddItem(Block item)
        {
                var itemSprite = item.spriteRenderer.sprite;
                var itemImage = item.GetComponent<SpriteRenderer>();
                if (Contains(itemImage, out var index))
                {
                        if (_slots[index].IsFull)
                                FindEmptySlot(itemSprite);
                        else
                                _slots[index].Increase();
                        return;
                }
                AddItemToEmptySlot(itemSprite);
        }

        private void FindEmptySlot(Sprite itemSprite)
        {
                foreach (var slot in _slots)
                {
                        if (slot.IsEmpty())
                        {
                                AddItemToEmptySlot(itemSprite);
                                break;
                        }
                }
        }

        private void AddItemToEmptySlot(Sprite itemSprite)
        {
                foreach (var slot in _slots)
                {
                        if (slot.IsEmpty())
                        {
                                slot.ChangeKey(itemSprite);
                                break;
                        }
                }
        }

        private bool Contains(SpriteRenderer image, out int index)
        {
                for (int i = 0; i < _slots.Count; i++)
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