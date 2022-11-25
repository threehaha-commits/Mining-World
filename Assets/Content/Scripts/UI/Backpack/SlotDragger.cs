using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Inject] private Updater _updater;
    private Slot _originalSlot;
    private Slot _draggerSlot;
    private RectTransform _currentRectTransform;
    private Transform _canvasTransform;
        
    private void Start()
    {
        _canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerHelper.PointerOverUI<Slot>(out var slot);//Если курсор находится над слотом, то получаем этот слот
        if (slot == null || slot.IsEmpty()) //Если не удалось получить слот или он пустой, то ничего ничего не происходит
            return;
        _originalSlot = slot; //Запоминаем слот. Это оригинальный слот
        var currentSlotPosition = slot.GetComponent<RectTransform>().position;
        _draggerSlot = Instantiate(_originalSlot, currentSlotPosition, Quaternion.identity, _canvasTransform); //Создаем новый слот, который будет двигаться за мышью
        ISlotChanger changer = _draggerSlot;
        changer.ChangeSlot(_originalSlot.GetSlot()); //Копируем данные из оригинального слота в перемещаемый
        var originalScale = _originalSlot.GetRectTransform().localScale;
        var originalSize = _originalSlot.GetRectTransform().sizeDelta;
        _draggerSlot.SetScale(originalScale); // Устанавливаем перемещаемому слоту такой же размер, как у оригинала
        _draggerSlot.SetSize(originalSize);
        _currentRectTransform = _draggerSlot.GetRectTransform(); //Запоминаем трансформ нашего перемещаемого слота
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
        Destroy(_draggerSlot.gameObject);
        _updater.UpdateSlot(_draggerSlot, _originalSlot);
    }
}