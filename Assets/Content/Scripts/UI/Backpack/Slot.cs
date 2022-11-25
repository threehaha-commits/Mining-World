using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, ISlotChanger, IIncreasable
{
    protected SlotType _slotType;
    protected Stack _stack = new();

    private int _slotSize => _stack == null ? 0 : _stack.Size();
    public bool IsFull => _stack.IsFull;
    [SerializeField] protected Ore _oreType;
    [SerializeField] protected Image _image;
    [SerializeField] protected TMP_Text _text;
    private Slot _slot;
    private RectTransform _rectTransform;
    private Sprite _defaultSprite;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponentsInChildren<Image>()[1];
        _text = GetComponentInChildren<TMP_Text>();
        _defaultSprite = _image.sprite;
    }

    public SlotType GetSlotType()
    {
        return _slotType;
    }
    
    public Stack GetStack()
    {
        return _stack;
    }
    
    public Ore GetOre()
    {
        return _oreType;
    }

    public Sprite GetSprite()
    {
        return _image.sprite;
    }
    
    public void SetSlot(Slot slot)
    {
        _slot = slot;
    }

    public void SetScale(Vector3 scale)
    {
        _rectTransform.localScale = scale;
    }

    public void SetSize(Vector2 size)
    {
        _rectTransform.sizeDelta = size;
    }

    public RectTransform GetRectTransform()
    {
        return _rectTransform;
    }
    
    public Slot GetSlot()
    {
        return _slot;
    }

    public bool IsEmpty()
    {
        return _image.sprite == _defaultSprite;
    }
    
    public void Increase()
    {
        _stack.Increase();
        _text.text = $"{_slotSize}";
    }
    
    public void Decrease()
    {
        _stack.Decrease();
        _text.text = $"{_slotSize}";
    }
    
    public void Decrease(int value)
    {
        _stack.Decrease(value);
        _text.text = $"{_slotSize}";
    }
    
    public void Increase(int value)
    {
        _stack.Increase(value);
        _text.text = $"{_slotSize}";
    }
    
    public virtual void RemoveSlot()
    {
        _oreType = Ore.Null;
        _image.sprite = _defaultSprite;
        _stack = null;
        ChangeStackText(String.Empty);
    }

    private void ChangeStackText(string text)
    {
        _text.text = text;
    }
    
    public virtual void ChangeSlot(Slot slot)
    {
        _oreType = slot.GetOre();
        _image.sprite = slot.GetSprite();
        _stack = slot.GetStack();
        ChangeStackText(_oreType.Equals(Ore.Null) ? String.Empty : _slotSize.ToString());
    }
}