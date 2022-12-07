using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Inject] private Updater _updater;
    private ISlotable _originalSlot;
    private ISlotable _draggerSlot;
    private RectTransform _currentRectTransform;
    private Transform _canvasTransform;
        
    private void Start()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerHelper.PointerOverUI<ISlotable>(out var slot);//Если курсор находится над слотом, то получаем этот слот
        IItemAvailable available = slot as IItemAvailable;
        if (slot == null || available.IsAvailable) //Если не удалось получить слот или он пустой, то ничего ничего не происходит
            return;
        _originalSlot = slot; //Запоминаем слот. Это оригинальный слот
        var currentSlotPosition = slot.rectTransform.position;
        _draggerSlot = (ISlotable)Instantiate((Object)_originalSlot, currentSlotPosition, Quaternion.identity, _canvasTransform); //Создаем новый слот, который будет двигаться за мышью
        ISlotChanger changer = _draggerSlot as ISlotChanger;
        changer.ChangeSlot(_originalSlot.slotable); //Копируем данные из оригинального слота в перемещаемый
        var originalScale = _originalSlot.rectTransform.localScale;
        var originalSize = _originalSlot.rectTransform.sizeDelta;
        _draggerSlot.rectTransform.localScale = originalScale; // Устанавливаем перемещаемому слоту такой же размер, как у оригинала
        _draggerSlot.rectTransform.sizeDelta = originalSize;
        _currentRectTransform = _draggerSlot.rectTransform; //Запоминаем трансформ нашего перемещаемого слота
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_currentRectTransform == null)
            return;
        var mousePosition = Input.mousePosition;
        _currentRectTransform.position = mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_currentRectTransform == null)
            return;
        Destroy(_draggerSlot.rectTransform.gameObject);
        _updater.UpdateSlot(_draggerSlot, _originalSlot);
    }
}