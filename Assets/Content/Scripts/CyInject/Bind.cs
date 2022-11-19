using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Bind<T>
{
    public static void Value(object value)
    {
        foreach (var objectOnScene in CInjectHelper.objectsOnIScene)
        {
            var fields = objectOnScene.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            SetValue(fields, value, objectOnScene);
        }
    }

    private static void SetValue(FieldInfo[] infos, object value, object objectFromScene)
    {
        foreach (var info in infos)
        {
            var fieldHaveAtt = info.GetCustomAttribute<InjectAttribute>();
            if (fieldHaveAtt == null)
                continue;

            if (info.FieldType == typeof(T))
                info.SetValue(objectFromScene, value);
        }
    }

    public static List<T> GetInterfaces()
    {
        return Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToList();
    }
}