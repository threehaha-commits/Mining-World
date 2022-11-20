using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GenerateField with Perlin Noise
/// </summary>
public class FieldGenerator
{
    private readonly List<List<Cell>> _blockFromMap;

    public FieldGenerator(List<List<Cell>> blockFromMap)
    {
        _blockFromMap = blockFromMap;
    }

    public Dictionary<Cell, float> Result(float radiusField, float intensityField, float noiseValue)
    {
        var dict = new Dictionary<Cell, float>();
        for (int i = 0; i < _blockFromMap.Count - 1; i++)
        {
            for (int j = 0; j < _blockFromMap[i].Count; j++)
            {
                if (BlockInsideRadius(i, j, radiusField, intensityField, noiseValue, out var result))
                    dict.Add(_blockFromMap[i][j], result);
            }
        }
        return dict;
    }
    
    private bool BlockInsideRadius(int i, int j, float radiusField, float intensityField, float noiseValue, out float result)
    {
        var x = i / radiusField * Mathf.Tan(intensityField);
        var y = j / radiusField * Mathf.Cos(intensityField);
        result = Mathf.PerlinNoise(x, y);
        return result > noiseValue;
    }
    
}