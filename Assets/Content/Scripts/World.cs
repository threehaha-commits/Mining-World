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
        _parent = new GameObject("World").transform; //Создаем и запоминаем родителя, в которого засунем все наши блоки
        var grass = WorldHelper.GetGrass(0); //Текстура грязи, в которую будем красить все блоки
        var spriteSize = WorldHelper.GetSpriteSize(grass); //Размер блока
        var horizontalBlocksCount = new List<int>();//Здесь мы будем хранить количество блоков в каждом столбце
        _blockFromMap = new List<List<Cell>>();
        for (int i = 0; i < worldSize; i++)
        {
            var noiseValue = WorldHelper.GetNoise(spriteSize, i); //Получаем значение шума
            var length = Mathf.RoundToInt(worldSize * noiseValue); //Умножаем длину мира на значение шума чтобы получить случайную высоту столбца
            horizontalBlocksCount.Add(length); //Добавляем высоту столбца в массив с высотами
            var list = new List<Cell>(); //Промежуточный массив вертикальных блоков
            for (int j = 0; j < horizontalBlocksCount[i]; j++)
            {
                var position = WorldHelper.GetPosition(spriteSize, i, j); //Получаем позицию где будем спавнить новый блок
                var cell = new Cell(WorldHelper.CreateBlock(grass, position, _parent)); //Спавним новый блок
                list.Add(cell); //Добавляем нвоый блок в промежуточный массив вертикальных блоков
            }

            _blockFromMap.Add(list); //Добавляем промежуточный массив вертикальных блоков в массив из массивов
        }
        Lerp(horizontalBlocksCount, spriteSize); //Сглаживаем всё это безобразие
        _oreCreator.StartInitialize(_blockFromMap); //Создаем руду на наших блоках
        new PlayerCreator(_blockFromMap); //Создаем игрока
    }
    
    private void Lerp(List<int> newLength, Vector2 spriteSize)
    {
        var sprite = WorldHelper.GetGrass(0);
        for (int i = 0; i < _blockFromMap.Count - 1; i++)
        {
            var i1 = i + 1; //Не от нефиг делать ради, а удосбтва для
            var y1 = Mathf.RoundToInt(_blockFromMap[i][_blockFromMap[i].Count - 1].Position.y); //Высота последнего блока текущего массива
            var y2 = Mathf.RoundToInt(_blockFromMap[i1][_blockFromMap[i1].Count - 1].Position.y); //Высота последнго блока следующего массива по порядку слева направо
            var xAverage = 0;
            if (y1 > y2) //Если текущий блок выше следующего
            {
                xAverage = Random.Range(y1 - 1, y1 + 2) - y2; //Получаем разницы между высотой текущего блока +-1 ед. и следующего
            }
            else continue;

            if (xAverage <= 0) //В случае, если высота следующего блока оказалась равна или выше текущего
                continue;
            
            for (int j = newLength[i1]; j < xAverage + newLength[i1]; j++) //Начиная с высоты следующего блока; Высота следующего блока меньше разницы блоков + высоты след блока
            {
                var position = WorldHelper.GetPosition(spriteSize, i1, j);//Всем текстуру грязи за мой счет!
                var cell = new Cell(WorldHelper.CreateBlock(sprite, position, _parent)); //Создаем новый блок на позиции следующих блоков
                _blockFromMap[i1].Add(cell); //Так же добавляем их в общий массив блоков
            }

            var lastBlock = _blockFromMap[i1][_blockFromMap[i1].Count - 1]; //Получаем последний блок следующего массива
            lastBlock.Renderer.sprite = WorldHelper.GetGrass(1).sprite; //Даем ему текстуру травки
        }
    }

    public void Destroy() //Давай-давай ломай, не руками же строили!
    {
        _parent?.GameObject().SetActive(false);
    }
}