using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot
{
    private readonly Image _key;
    private readonly TMP_Text _text;
    private bool _value = true;
    private readonly Stack _stack;
    public bool IsFull => _stack.IsFull;
    
    public Slot(Image key, bool value, TMP_Text text)
    {
        _key = key;
        _value = value;
        _text = text;
        _stack = new Stack();
    }

    public bool Contains(SpriteRenderer image)
    {
        return _key.sprite == image.sprite;
    }
    
    public bool IsEmpty()
    {
        return _value;
    }
    
    public void ChangeKey(Sprite image)
    {
        _key.sprite = image;
        ChangeValue();
    }

    public void Increase()
    {
        _stack.Add();
        _text.text = $"{_stack.Size()}";
    }

    private void ChangeValue()
    {
        Increase();
        _value = false;
    }
}