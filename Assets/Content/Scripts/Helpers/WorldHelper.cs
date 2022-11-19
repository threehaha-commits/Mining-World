using System.Collections.Generic;
using UnityEngine;

public static class WorldHelper
{
    private static SpriteRenderer[] _grass = new SpriteRenderer[2];

    private static SpriteRenderer[] grass
    {
        get
        {
            if(_grass[0] == null)
                _grass = Resources.LoadAll<SpriteRenderer>("Grass");
            return _grass;
        }
    }
    
    public static float GetMinValueFromLastIndexList(List<List<Cell>> list)
    {
        var min1 = Mathf.Infinity;
        for (int i = 0; i < list.Count; i++)
        {
            var lastIndex = list[i].Count - 1;
            var min2 = list[i][lastIndex].Position.y;
            if (min2 < min1)
            {
                min1 = min2;
            }
        }
        return min1;
    }
    
    public static float GetMaxValueFromLastIndexList(List<List<Cell>> list)
    {
        var min1 = 0f;
        for (int i = 0; i < list.Count; i++)
        {
            var lastIndex = list[i].Count - 1;
            var min2 = list[i][lastIndex].Position.y;
            if (min2 > min1)
            {
                min1 = min2;
            }
        }

        return min1;
    }

    public static float GetNoise(Vector2 spriteSize, int i)
    {
        var random = Random.Range(0, i);
        var x = Mathf.Cos(random * spriteSize.y);
        var y = Mathf.Sin(random / spriteSize.y);
        var value = Mathf.PerlinNoise(x, y);
        return value;
    }
    
    public static SpriteRenderer CreateBlock(SpriteRenderer sprite, Vector2 position, Transform parent)
    {
        return Object.Instantiate(sprite, position, Quaternion.identity, parent).GetComponent<SpriteRenderer>();
    }

    public static Vector2 GetPosition(Vector2 spriteSize, int i, int j)
    {
        return new Vector2(spriteSize.x * i, spriteSize.y * j);
    }

    public static SpriteRenderer GetGrass(int index)
    {
        return grass[index];
    }

    public static Vector2 GetSpriteSize(SpriteRenderer spriteRendererObject)
    {
        return spriteRendererObject.size;
    }
}