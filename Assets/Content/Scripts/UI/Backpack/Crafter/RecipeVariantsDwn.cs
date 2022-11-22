using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeVariantsDwn : MonoBehaviour, IInitialize
{
    [SerializeField] private TMP_Dropdown _itemRecipesDwn;
    [SerializeField] private List<TMP_Dropdown.OptionData> _optionData = new ();

    private void Start()
    {
        _itemRecipesDwn = GetComponent<TMP_Dropdown>();
    }

    public void Clear()
    {
        _optionData.Clear();
    }
    
    public void Add(ItemRecipe[] recipes)
    {
        _itemRecipesDwn.ClearOptions();
        foreach (var recipe in recipes)
        {
            var data = CreateData(recipe);
            Add(data);
        }
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

    private void Add(TMP_Dropdown.OptionData data)
    {
        if (!_itemRecipesDwn.options.Contains(data))
            _itemRecipesDwn.options.Add(data);
    }
    
    public void Initialize()
    {
        Bind<RecipeVariantsDwn>.Value(this);
        _itemRecipesDwn.ClearOptions();
    }
}