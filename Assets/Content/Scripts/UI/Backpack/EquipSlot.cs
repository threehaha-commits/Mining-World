using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IInitialize, ISlotChanger, IItemAvailable, ISlotTypeable
{
    private Image _image;
    private Sprite _defaultSprite;
    
    private Consumable _consumable;
    Consumable ISlotable.consumable
    {
        get => _consumable;
        set => _consumable = value;
    }

    bool IItemAvailable.IsAvailable => _image.sprite == _defaultSprite;

    [SerializeField] private SlotType _slotType;
    SlotType ISlotTypeable.slotType => _slotType;

    Image ISlotable.image => _image;

    Sprite ISlotable.defaultSprite => _defaultSprite;

    ISlotable ISlotable.slotable
    {
        get => this;
        set => GetComponent<ISlotable>().slotable = value;
    }

    private RectTransform _rectTransform;
    
    RectTransform ISlotable.rectTransform => _rectTransform;
    
    private void Awake()
    {
        _image = GetComponentsInChildren<Image>()[1];
        _rectTransform = GetComponent<RectTransform>();
        _defaultSprite = _image.sprite;
    }
    
    void IInitialize.Initialize()
    {
        Bind<EquipSlot>.Value(this);
    }

    void ISlotable.RemoveSlot()
    {
        _consumable = null;
        _image.sprite = _defaultSprite;
    }

    public void ChangeSlot(ISlotable slot)
    {
        _consumable = slot.consumable;
        _image.sprite = slot.image.sprite;
        gameObject.name = slot.name;
    }
}