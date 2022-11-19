using UnityEngine;

public class WindowEnabler : MonoBehaviour
{
    public void WindowEnable(GameObject window)
    {
        window.SetActive(!window.activeSelf);
    }
}