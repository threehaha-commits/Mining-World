using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackpackSlotsFinder : MonoBehaviour
{
        [SerializeField] private Image[] _images;
        [SerializeField] private TMP_Text[] _text;
        private int _currentIndexText = 0;
        
        private void Awake()
        {
                #region From gameObject Backpack_Background
                var slotCount = transform.childCount;
                _images = new Image[slotCount];
                _text = GetComponentsInChildren<TMP_Text>();
                for (var i = 0; i < slotCount; i++)
                {
                        _images[i] =  transform.GetChild(i).GetChild(0).GetComponent<Image>();
                }
                #endregion
        }

        public Image[] GetSlots()
        {
                return _images;
        }

        public TMP_Text GetText()
        {
                return _text[_currentIndexText++];
        }
}