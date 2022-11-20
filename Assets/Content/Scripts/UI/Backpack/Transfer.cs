using UnityEngine;
using UnityEngine.EventSystems;

namespace MyBackpack
{
    public class Transfer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Inject] private Updater _transferViewer;
        private Slot _main;
        private Slot _current;
        private RectTransform _currentRectTransform;
        private Transform _canvasParent;
        
        private void Start()
        {
            _canvasParent = FindObjectOfType<Canvas>().transform;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerHelper.PointerOverUI<Slot>(out var slot);
            if (slot == null || slot.IsEmpty())
                return;
            _main = slot;
            var currentSlotPosition = slot.GetComponent<RectTransform>().position;
            _current = Instantiate(slot, currentSlotPosition, Quaternion.identity, _canvasParent);
            ISlotChanger changer = _current;
            changer.ChangeSlot(slot.GetSlot());
            _current.SetScale(new Vector3(2f, 2f, 2f));
            _current.SetSize(new Vector2(80, 80));
            _currentRectTransform = _current.GetRectTransform();
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
            _transferViewer.UpdateSlot(_current, _main);
            Destroy(_current.gameObject);
        }
    }
}