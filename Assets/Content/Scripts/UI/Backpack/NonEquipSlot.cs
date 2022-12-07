using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NonEquipSlot : MonoBehaviour, ISlotChanger, IStackable, IIncreasable, IDecreasable, IItemAvailable, ISlotTypeable
{ 
    [SerializeField] protected SlotType _slotType = SlotType.Empty;
    protected Stack _stack = new();
    private string _sizeOutputText => _stackable == null || _stackable.stackSize == 0? string.Empty : _stackable.stackSize.ToString();
    string ISlotable.name => _rectTransform.name;
    [SerializeField] protected Consumable _consumable;
    Consumable ISlotable.consumable
    {
        get => _consumable;
        set => _consumable = value;
    }
    
    SlotType ISlotTypeable.slotType => _slotType;
    
    Image ISlotable.image => _image;

    Sprite ISlotable.defaultSprite => _defaultSprite;

    ISlotable ISlotable.slotable
    {
        get => this;
        set => GetComponent<ISlotable>().slotable = value;
    }

    RectTransform ISlotable.rectTransform => _rectTransform;
    
    bool IStackable.isFull => _stack == null ? true : _stack.IsFull;

    Stack IStackable.stack
    {
        get => _stack;
        set => _stack = value;
    }

    int IStackable.stackSize => _stack == null ? 0 : _stack.Size();

    [SerializeField] protected TMP_Text _text;
    TMP_Text IStackable.stackSizeText
    {
        get => _text;
        set => _text = value;
    }

    bool IItemAvailable.IsAvailable => _image.sprite == _defaultSprite;

    [SerializeField] protected Image _image;
    private NonEquipSlot _slot;
    private RectTransform _rectTransform;
    private Sprite _defaultSprite;
    private IStackable _stackable;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponentsInChildren<Image>()[1];
        _text = GetComponentInChildren<TMP_Text>();
        _defaultSprite = _image.sprite;
        _stackable = this;
    }

    public SlotType GetSlotType()
    {
        return _slotType;
    }
    
    public Stack GetStack()
    {
        return _stack;
    }
    
    public Consumable GetOre()
    {
        return _consumable;
    }

    public Sprite GetSprite()
    {
        return _image.sprite;
    }

    protected void SetSlot(NonEquipSlot slot)
    {
        _slot = slot;
    }
    
    void IIncreasable.Increase(int value)
    {
        _stack.Increase(value);
        ChangeStackText();
    }
    
    void IIncreasable.Increase()
    {
        _stack.Increase();
        ChangeStackText();
    }
    
    void IDecreasable.Decrease()
    {
        _stack.Decrease();
        ChangeStackText();
    }
    
    void IDecreasable.Decrease(int value)
    {
        _stack.Decrease(value);
        ChangeStackText();
    }
    
    public virtual void RemoveSlot()
    {
        _slotType = SlotType.Empty;
        _consumable = null;
        _image.sprite = _defaultSprite;
        _stack = null;
        ChangeStackText();
    }

    private void ChangeStackText()
    {
        _text.text = _sizeOutputText;
    }
    
    public virtual void ChangeSlot(ISlotable slot)
    {
        var slotTypeable = slot as ISlotTypeable;
        if (slotTypeable.slotType == SlotType.Consumable)
        {
            IStackable stackable = slot as IStackable;
            _stack = stackable.stack;
        }
        _image.sprite = slot.image.sprite;
        _slotType = slotTypeable.slotType;
        gameObject.name = slot.name;
        ChangeStackText();
    }
}