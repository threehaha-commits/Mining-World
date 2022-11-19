using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class World
{
    private readonly OreCreator _oreCreator;
    private List<List<Cell>> _blockFromMap;
    private Transform _parent;

    public World(OreCreator oreCreator)
    {
        _oreCreator = oreCreator;
    }
    
    public void Create(int worldSize)
    {
        _parent = new GameObject("World").transform;
        var grass = WorldHelper.GetGrass(0);
        var spriteSize = WorldHelper.GetSpriteSize(grass);
        var horizontalBlocks = new List<int>();
        var sprite = WorldHelper.GetGrass(0);
        _blockFromMap = new List<List<Cell>>();
        for (int i = 0; i < worldSize; i++)
        {
            var noiseValue = WorldHelper.GetNoise(spriteSize, i);
            var length = Mathf.RoundToInt(worldSize * noiseValue);
            horizontalBlocks.Add(length);
            var list = new List<Cell>();
            for (int j = 0; j < horizontalBlocks[i]; j++)
            {
                var position = WorldHelper.GetPosition(spriteSize, i, j);
                var cell = new Cell(WorldHelper.CreateBlock(sprite, position, _parent));
                list.Add(cell);
            }

            _blockFromMap.Add(list);
        }
        Lerp(horizontalBlocks, spriteSize);
        _oreCreator.StartInitialize(ref _blockFromMap);
        new PlayerCreator(_blockFromMap);
    }
    
    private void Lerp(List<int> newLength, Vector2 spriteSize)
    {
        var sprite = WorldHelper.GetGrass(0);
        for (int i = 0; i < _blockFromMap.Count - 1; i++)
        {
            var i1 = i + 1;
            var x1 = Mathf.RoundToInt(_blockFromMap[i][_blockFromMap[i].Count - 1].Position.y);
            var x2 = Mathf.RoundToInt(_blockFromMap[i1][_blockFromMap[i1].Count - 1].Position.y);
            var xAverage = 0;
            if (x1 > x2)
            {
                xAverage = Random.Range(x1 - 1, x1 + 2) - x2;
            }
            else continue;

            if (xAverage <= 0)
                continue;
            
            for (int j = newLength[i1]; j < xAverage + newLength[i1]; j++)
            {
                var position = WorldHelper.GetPosition(spriteSize, i1, j);
                var cell = new Cell(WorldHelper.CreateBlock(sprite, position, _parent));
                _blockFromMap[i1].Add(cell);
            }

            var lastBlock = _blockFromMap[i1][_blockFromMap[i1].Count - 1];
            lastBlock.Renderer.sprite = WorldHelper.GetGrass(1).sprite;
        }
    }

    public void Destroy()
    {
        _parent?.GameObject().SetActive(false);
    }
}