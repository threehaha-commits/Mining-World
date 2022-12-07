using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class World
{
    private List<List<Cell>> _blockFromMap;
    private Transform _parent;
    private readonly SpriteRenderer[] _ores;
    
    public World(OreCreator oreCreator, float smooth, float[] noiseValueForOre)
    {
        _ores = WorldHelper.LoadOresFromResources();
        Array.Reverse(_ores);
    }
    
    private int GetOreIndex(ref Vector2 position, ref int[] worldParts)
    {
        var indexPosition = 0;
        for (int i = 0; i < worldParts.Length; i++)
        {
            if (position.y <= worldParts[i])
                break;
            
            indexPosition = i;
        }
        
        return indexPosition;
    }

    private int[] GetWorldYParts(int yLength)
    {
        var grass = WorldHelper.GetGrass(0);
        var spriteSize = WorldHelper.GetSpriteSize(grass);
        var newLength = new int[_ores.Length];
        var onePartLength = Mathf.RoundToInt((yLength + 1) * spriteSize.y / _ores.Length);
        for (int i = 0; i < newLength.Length; i++)
            newLength[i] = onePartLength * (i + 1);

        return newLength;
    }
    
    public void Create(int worldSize)
    {
        _parent = new GameObject("World").transform; //Создаем и запоминаем родителя, в которого засунем все наши блоки
        var grass = WorldHelper.GetGrass(0); //Текстура грязи, в которую будем красить все блоки
        var spriteSize = WorldHelper.GetSpriteSize(grass); //Размер блока
        var horizontalBlocksCount = new List<int>();//Здесь мы будем хранить количество блоков в каждом столбце
        _blockFromMap = new List<List<Cell>>();
        for (int i = 0; i < worldSize; i++)
        {
            var noiseValue = WorldHelper.GetNoise(spriteSize, i); //Получаем значение шума
            var length = Mathf.RoundToInt(worldSize * noiseValue); //Умножаем длину мира на значение шума чтобы получить случайную высоту столбца
            var worldParts = GetWorldYParts(length); //Получаем часть мира отведенную под каждую руду
            horizontalBlocksCount.Add(length); //Добавляем высоту столбца в массив с высотами
            var list = new List<Cell>(); //Промежуточный массив вертикальных блоков
            for (int j = 0; j < horizontalBlocksCount[i]; j++)
            {
                var position = WorldHelper.GetPosition(spriteSize, i, j); //Получаем позицию где будем спавнить новый блок
                var oreIndex = GetOreIndex(ref position, ref worldParts); //Получаем индекс руды для спавна
                var ore = WorldHelper.CreateBlock(ref _ores[oreIndex], ref position, ref _parent); //Создаем новую руду
                var cell = new Cell(ore); //Создаем новую ячейку с рудой
                list.Add(cell); //Добавляем нвоый блок в промежуточный массив вертикальных блоков
            }

            _blockFromMap.Add(list); //Добавляем промежуточный массив вертикальных блоков в массив из массивов
        }
        Lerp(horizontalBlocksCount, spriteSize); //Сглаживаем всё это безобразие
        new PlayerCreator(_blockFromMap); //Создаем игрока
    }
    
    private void Lerp(List<int> newLength, Vector2 spriteSize)
    {
        for (int i = 0; i < _blockFromMap.Count - 1; i++)
        {
            var i1 = i + 1; //Удобства для
            var y1 = Mathf.RoundToInt(_blockFromMap[i][_blockFromMap[i].Count - 1].Position.y); //Высота последнего блока текущего массива
            var y2 = Mathf.RoundToInt(_blockFromMap[i1][_blockFromMap[i1].Count - 1].Position.y); //Высота последнго блока следующего массива по порядку слева направо
            int xAverage;
            if (y1 > y2) //Если текущий блок выше следующего
            {
                xAverage = Random.Range(y1 - 1, y1 + 2) - y2; //Получаем разницы между высотой текущего блока +-1 ед. и следующего
            }
            else continue;

            if (xAverage <= 0) //В случае, если высота следующего блока оказалась равна или выше текущего
                continue;
            
            var worldParts = GetWorldYParts(xAverage + newLength[i1] / 2); //Получаем длину мира по оси Y, только немного урезанную на 2
            for (int j = newLength[i1]; j < xAverage + newLength[i1]; j++) //Начиная с высоты следующего блока; Высота следующего блока меньше разницы блоков + высоты след блока
            {
                var position = WorldHelper.GetPosition(spriteSize, i1, j);//Всем текстуру грязи за мой счет!
                var oreIndex = GetOreIndex(ref position, ref worldParts); //Получаем индекс руды для спавна
                var ore = WorldHelper.CreateBlock(ref _ores[oreIndex], ref position, ref _parent); //Создаем новую руду
                var cell = new Cell(ore); //Создаем новую ячейку с рудой
                _blockFromMap[i1].Add(cell); //Так же добавляем их в общий массив блоков
            }

            var lastBlock = _blockFromMap[i1][_blockFromMap[i1].Count - 1]; //Получаем последний блок следующего массива
            lastBlock.Renderer.sprite = WorldHelper.GetGrass(1).sprite; //Даем ему текстуру травки
        }
    }

    public void Destroy() //Давай ломай, не руками же строили!
    {
        _parent?.gameObject.SetActive(false);
    }
}