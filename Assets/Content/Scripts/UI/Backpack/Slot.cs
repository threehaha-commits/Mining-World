using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, ISlotChanger
{
    protected Stack _stack = new();

    protected int _slotSize
    {
        get
        {
            if (_stack == null)
                return 0;
            
            return _stack.Size();
        }
    }
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
        return _oreType == Ore.Null;
    }
    
    public void Increase()
    {
        _stack.Add();
        _text.text = $"{_slotSize}";
    }
    
    public void Increase(int value)
    {
        _stack.Add(value);
        _text.text = $"{_slotSize}";
    }
    
    public void RemoveSlot()
    {
        Debug.Log(gameObject.name);
        _oreType = Ore.Null;
        _image.sprite = _defaultSprite;
        _stack = null;
        PrintSize();
    }

    protected void PrintSize()
    {
        _text.text = $"{_slotSize}";
    }
    
    public virtual void ChangeSlot(Slot slot)
    {
        
    }
}