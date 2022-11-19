using UnityEngine;

public class CaveCreator
{
    private readonly Cell[,] _blockFromMap;
    
    public CaveCreator(ref Cell[,] blockFromMap)
    {
        _blockFromMap = blockFromMap;
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _blockFromMap.GetLength(0); i++)
        {
            for (int j = 0; j < _blockFromMap.GetLength(1); j++)
            {
                Debug.Log(_blockFromMap[i,j].Empty);
                if (_blockFromMap[i, j].Empty)
                {
                    _blockFromMap[i, j].GameObject.SetActive(false);
                }
            }
        }
    }
}