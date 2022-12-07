using UnityEngine;

public interface ISlotable
{
    string name { get; }
    Consumable consumable { get; set; }
    UnityEngine.UI.Image image { get; }
    Sprite defaultSprite { get; }
    ISlotable slotable { get; set; }
    RectTransform rectTransform { get; }

    void RemoveSlot();
}