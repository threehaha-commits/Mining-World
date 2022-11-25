using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour, IInitialize
{
    [Inject] private RecipeImage _recipeImage;
    private Button _button;
    private CraftSlot[] _craftSlots;
    [Inject] private Inventory _inventory;
    private ItemRecipe _currentRecipe;
        
    public void UpdateButton(ItemRecipe recipe)
    {
        if (_recipeImage.IsEmpty)
        {
            _button.interactable = false;
            return;
        }

        var index = 0;
        index = EqualsOreFromSlotsWithOreFromRecipes(recipe, index);

        if (index == recipe.Ores.Length)
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
    }
    
    private int EqualsOreFromSlotsWithOreFromRecipes(ItemRecipe recipe, int index)
    {
        for (int i = 0; i < _craftSlots.Length; i++)
        {
            for (int j = 0; j < recipe.Ores.Length; j++)
            {
                if (_craftSlots[i].GetOre().Equals(recipe.Ores[j].Ore))
                {
                    if (_craftSlots[i].GetStack().Size() >= recipe.Ores[j].Count)
                        index++;
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