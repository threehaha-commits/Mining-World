using UnityEngine;

public static class CInjectHelper
{
    private static Object[] _objectsOnIScene;
    public static Object[] objectsOnIScene => _objectsOnIScene;
    
    public static void FindObjectsOnScene()
    {
        _objectsOnIScene = Object.FindObjectsOfType<Object>();
    }
}