using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RecipeVariantsDwn : MonoBehaviour, IInitialize, IPostInitialize
{
    private TMP_Dropdown _itemRecipesDwn;
    private List<TMP_Dropdown.OptionData> _optionData = new ();
    private readonly List<ItemRecipe> _recipes = new();
    private UnityAction<ItemRecipe> _update;
    private UnityAction _clear;
    [Inject] private RecipeImage _recipeImage;
    [Inject] private RecipesHelper _recipesHelper;
    [Inject] private CraftButton _craftButton;
    
    public void Clear()
    {
        _itemRecipesDwn.ClearOptions();
        _optionData.Clear();
        _clear.Invoke();
    }
    
    public void Add(ItemRecipe[] recipes)
    {
        _itemRecipesDwn.ClearOptions();
        foreach (var recipe in recipes)
        {
            var data = CreateData(recipe);
            _recipes.Add(recipe);
            AddToDwn(data);
        }
        ChangeRecipe();
    }

    public void ChangeRecipe()
    {
        var index = _itemRecipesDwn.value;
        var currentRecipe = _recipes[index];
        _update.Invoke(currentRecipe);
    }
    
    private TMP_Dropdown.OptionData CreateData(ItemRecipe recipe)
    {
        var data = new TMP_Dropdown.OptionData
        {
            image = recipe.Icon,
            text = recipe.Name
        };
        return data;
    }

    private void AddToDwn(TMP_Dropdown.OptionData data)
    {
        if (InventoryHelper.Contains(data, _itemRecipesDwn)) 
            return;
        _itemRecipesDwn.options.Add(data);
    }

    void IInitialize.Initialize()
    {
        _itemRecipesDwn = GetComponent<TMP_Dropdown>();
        Bind<RecipeVariantsDwn>.Value(this);
        _itemRecipesDwn.ClearOptions();
    }

    void IPostInitialize.PostInitialize()
    {
        _update += _recipeImage.ChangeImage;
        _update += _craftButton.UpdateButton;
        _update += _recipesHelper.Visualize;
        _clear += _recipeImage.Clear;
        _clear += _craftButton.Clear;
        _clear += _recipesHelper.Clear;
    }
}