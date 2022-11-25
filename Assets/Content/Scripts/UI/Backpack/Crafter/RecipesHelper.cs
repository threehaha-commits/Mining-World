using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipesHelper : MonoBehaviour, IInitialize
{
    private readonly Image[] _images = new Image[6];
    private readonly TMP_Text[] _textCount = new TMP_Text[6];
    private Block[] _oresFromBlocks;
    private Sprite _defaultSprite;
    private Color _defaultColor;
    
    public void Visualize(ItemRecipe recipe)
    {
        for (int i = 0; i < recipe.Ores.Length; i++)
        {
            var oreIndex = GetOreSprite(recipe, i);
            _images[i].sprite = oreIndex;
            _images[i].color = Color.white;
            _textCount[i].text = $"{recipe.Ores[i].Count}";
        }
    }

    private Sprite GetOreSprite(ItemRecipe recipe, int index)
    {
        for (int i = 0; i < _oresFromBlocks.Length; i++)
        {
            if (_oresFromBlocks[i].oreType.Equals(recipe.Ores[index].Ore))
                return _oresFromBlocks[i].GetComponent<SpriteRenderer>().sprite;
        }

        return null;
    }
    
    public void Clear()
    {
        for (var i = 0; i < _images.Length; i++)
        {
            var image = _images[i];
            var text = _textCount[i];
            image.sprite = _defaultSprite;
            image.color = _defaultColor;
            text.text = " ";
        }
    }
    
    void IInitialize.Initialize()
    {
        Bind<RecipesHelper>.Value(this);
        FindChildrenImage();
        _oresFromBlocks = Resources.LoadAll<Block>("Ores");
        for (var i = 0; i < _images.Length; i++)
        {
            var image = _images[i];
            _textCount[i] = image.GetComponentInChildren<TMP_Text>();
        }

        _defaultSprite = _images[0].sprite;
        _defaultColor = _images[0].color;
    }

    private void FindChildrenImage()
    {
        var images = GetComponentsInChildren<Image>();
        for (int i = 1; i < images.Length; i++)
            _images[i - 1] = images[i];
    }
}