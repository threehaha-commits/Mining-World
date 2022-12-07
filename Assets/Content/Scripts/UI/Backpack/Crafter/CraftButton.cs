using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour, IInitialize
{
    [Inject] private RecipeImage _recipeImage;
    private Button _button;
    private CraftSlot[] _craftSlots;
    [Inject] private Inventory _inventory;
    private ItemRecipe _currentRecipe;
    private Dictionary<NonEquipSlot, int> _slotWithResourcesForCraft = new();
    
    public void UpdateButton(ItemRecipe recipe)
    {
        if (_recipeImage.IsEmpty)
        {
            _button.interactable = false;
            return;
        }

        var index = 0;
        index = ItemFromSlotsEqualsRecipeItems(recipe, index);

        if (index == recipe.ConsumableInfos.Length)
        {
            _button.interactable = true;
            _recipeImage.ChangeImage(recipe);
            _currentRecipe = recipe;
        }
        else
        {
            _button.interactable = false;
        }
        
    }

    public void Craft()
    {
        Debug.Log("Craft");
        _inventory.AddItem(_currentRecipe.Item);
        DecreaseResourcesFromCraftSlot();
        UpdateButton(_currentRecipe);
    }

    private void DecreaseResourcesFromCraftSlot()
    {
        var count = _slotWithResourcesForCraft.Count;
        NonEquipSlot[] keys = _slotWithResourcesForCraft.Keys.ToArray();
        int[] values = _slotWithResourcesForCraft.Values.ToArray();
        for (int i = 0; i < count; i++)
        {
            IDecreasable decreasable = keys[i];
            decreasable.Decrease(values[i]);
        }
    }

    private int ItemFromSlotsEqualsRecipeItems(ItemRecipe recipe, int index)
    {
        _slotWithResourcesForCraft.Clear();
        for (int i = 0; i < _craftSlots.Length; i++)
        {
            for (int j = 0; j < recipe.ConsumableInfos.Length; j++)
            {
                ISlotable consumable = _craftSlots[i];
                var a = consumable.name;
                var b = recipe.ConsumableInfos[j].Item.name + "(Clone)";
                if (a.Equals(b))
                {
                    if (_craftSlots[i]?.GetStack()?.Size() >= recipe.ConsumableInfos[j]?.Count)
                    {
                        index++;
                        _slotWithResourcesForCraft.Add(_craftSlots[i], recipe.ConsumableInfos[j].Count);
                    }
                }
            }
        }

        return index;
    }

    public void Clear()
    {
        _button.interactable = false;
    }
    
    void IInitialize.Initialize()
    {
        Bind<CraftButton>.Value(this);
        _craftSlots = FindObjectsOfType<CraftSlot>();
        _button = GetComponent<Button>();
    }
}