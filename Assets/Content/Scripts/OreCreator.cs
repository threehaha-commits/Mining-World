using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Creator", fileName = "OreCreator")]
public class OreCreator : ScriptableObject
{
    private List<List<Cell>> _blockFromMap;
    [SerializeField, Range(5.5f, 6f)] private float _oreIntensity;
    private FieldGenerator _generator;
    private SpriteRenderer[,] _ores;

    //Чем меньше данный показатель, тем больше плотность руды
    [SerializeField] private float[] _oreDensity;

    //Значение шума при котором будет появлятся данная руда
    [SerializeField] private float[] _oreTypeFromNoise;
    
    public void StartInitialize(ref List<List<Cell>> blocksFromMap)
    {
        _blockFromMap = blocksFromMap;
        _generator = new FieldGenerator(ref _blockFromMap);
        LoadOresFromResources();
        Initialize();
    }
    
    private void LoadOresFromResources()
    {
        var ores = Resources.LoadAll<SpriteRenderer>("Ores");
        _ores = new SpriteRenderer[ores.Length / 2, 2];
        for (int i = 0, j = 0; i < ores.Length / 2; i++)
        {
            var k = 0;
            _ores[i, k++] = ores[j++];
            _ores[i, k] = ores[j++];
        }
    }

    private void Initialize()
    {
        for (int i = 0; i < _ores.GetLength(0); i++)
        {
            var oreFieldsRadius = Random.Range(8.5f, 19.5f);
            Create(oreFieldsRadius, i);
        }
    }

    private void Create(float radiusOreField, int i)
    {
        var generatedOreDict = _generator.Result(radiusOreField, _oreIntensity, _oreTypeFromNoise[i]);
        foreach (var ore in generatedOreDict)
        {
            if (!(ore.Value >= _oreDensity[i])) 
                continue;
            var pos = ore.Key.Position;
            var current = GetOrePosition(i);
            if (!(pos.y < current)) 
                continue;
            var randomSprite = Random.Range(0, 2);
            ore.Key.Sprite = _ores[i, randomSprite].sprite;
            var blockHit = ore.Key.GameObject.GetComponent<BlockHit>();
            var blockHitCount = _ores[i, randomSprite].GetComponent<BlockHit>().GetHit();
            blockHit.SetHit(blockHitCount);
        }
    }

    private float GetOrePosition(int i)
    {
        var indexTopBlock = _blockFromMap[i].Count- 1;
        var topBlockPositionY = _blockFromMap[i][indexTopBlock].Position.y;
        var pieceOfWorld = topBlockPositionY / _oreDensity.Length;
        return topBlockPositionY - pieceOfWorld * i;
    }
}