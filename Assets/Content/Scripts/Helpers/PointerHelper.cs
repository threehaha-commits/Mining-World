using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class PointerHelper
{
    public static bool PointerOverUI<T>() where T : class
    {
        var rayResult = GetRaycastResult();
        return MouseIsOverUI<T>(rayResult);
    }

    public static bool PointerOverUI<T>(out T result) where T : class
    {
        var rayResult = GetRaycastResult();
        var boolResult = ObjectUnderMouse<T>(rayResult);
        result = boolResult as T;
        return result != null;
    }
    
    private static Component ObjectUnderMouse<T>(List<RaycastResult> result)
    {
        for (int i = 0; i < result.Count; i++)
        {
            var isUIWithCraftingHandler = result[i].gameObject.TryGetComponent(typeof(T), out var isResult);
            if (isUIWithCraftingHandler)
                return isResult;
        }

        return null;
    }
    
    private static bool MouseIsOverUI<T>(List<RaycastResult> result)
    {
        for (int i = 0; i < result.Count; i++)
        {
            var isUIWithCraftingHandler = result[i].gameObject.TryGetComponent(typeof(T), out var isResult);
            if (isUIWithCraftingHandler)
                return true;
        }

        return false;
    }
    
    private static List<RaycastResult> GetRaycastResult()
    {
        var pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var rayResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, rayResult);
        return rayResult;
    }
}