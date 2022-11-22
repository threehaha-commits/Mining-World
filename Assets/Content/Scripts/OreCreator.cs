using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Creator", fileName = "OreCreator")]
public class OreCreator : ScriptableObject
{
    private List<List<Cell>> _blockFromMap;
    [SerializeField, Range(5.5f, 6f)] private float _oreIntensity;
    private FieldGenerator _generator;
    private SpriteRenderer[] _ores;

    //Чем меньше данный показатель, тем больше плотность руды
    [SerializeField] private float[] _oreDensity;

    //Значение шума при котором будет появлятся данная руда
    [SerializeField] private float[] _oreTypeFromNoise;
    
    public void StartInitialize(List<List<Cell>> blocksFromMap)
    {
        _blockFromMap = blocksFromMap;
        _generator = new FieldGenerator(_blockFromMap);
        _ores = LoadOresFromResources();
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _ores.Length - 1; i++)
        {
            var oreFieldsRadius = Random.Range(4.5f, 11.5f); //Произвольные магические числа
            Create(oreFieldsRadius, i);
        }
    }

    private void Create(float radiusOreField, int i)
    {
        var generatedOreDict = _generator.Result(radiusOreField, _oreIntensity, GetNoiseValue(i));
        foreach (var ore in generatedOreDict)
        {
            if (!(ore.Value >= _oreDensity[i])) 
                continue;
            var pos = ore.Key.Position;
            var current = GetOrePosition(i);
            if (!(pos.y < current)) 
                continue;
            var parent = ore.Key.Transform.parent;
            ore.Key.GameObject.SetActive(false);
            var newOre = Instantiate(_ores[i + 1], pos, Quaternion.identity, parent);
            newOre.gameObject.SetActive(true);
        }
    }

    private float GetNoiseValue(int i)
    {
        var oreCount = _ores.Length;
        float pieceOfWorld = (1f / oreCount);
        float result = i * pieceOfWorld;
        return result;
    }
    
    private float GetOrePosition(int i)
    {
        var indexTopBlock = _blockFromMap[i].Count- 1;
        var topBlockPositionY = _blockFromMap[i][indexTopBlock].Position.y;
        var pieceOfWorld = topBlockPositionY / _oreDensity.Length;
        return topBlockPositionY - pieceOfWorld * i;
    }
    
    private SpriteRenderer[] LoadOresFromResources()
    {
        return Resources.LoadAll<SpriteRenderer>("Ores");
    }
}