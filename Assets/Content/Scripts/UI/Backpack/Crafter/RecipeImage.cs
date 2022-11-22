using UnityEngine;
using UnityEngine.UI;

public class RecipeImage : MonoBehaviour, IInitialize
{
    private Image _itemRecipeImage;
    private Sprite _defaultSprite;
    public bool IsEmpty => _itemRecipeImage.sprite == _defaultSprite;
    
    public void Clear()
    {
        _itemRecipeImage.sprite = _defaultSprite;
    }
    
    public void Add(ItemRecipe recipe)
    {
        _itemRecipeImage.sprite = recipe.Icon;
    }

    public void Initialize()
    {
        Bind<RecipeImage>.Value(this);
        _itemRecipeImage = GetComponent<Image>();
        _defaultSprite = _itemRecipeImage.sprite;
    }
}