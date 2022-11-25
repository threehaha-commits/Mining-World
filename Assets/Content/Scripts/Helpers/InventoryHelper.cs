using TMPro;
using UnityEngine;

public static class InventoryHelper
{
    public static bool Contains(InventorySlot[] _slots, Sprite sprite, out int index)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].Contains(sprite))
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
    
    public static bool Contains(TMP_Dropdown.OptionData data, TMP_Dropdown itemRecipesDwn)
    {
        foreach (var item in itemRecipesDwn.options)
        {
            var itemName = item.text;
            var dataName = data.text;
            if (itemName.Equals(dataName))
                return true;
        }

        return false;
    }
}