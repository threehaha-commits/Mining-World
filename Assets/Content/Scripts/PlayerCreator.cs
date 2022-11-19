using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator
{
    public readonly Player Player;
    
    public PlayerCreator(List<List<Cell>> blockFromMap)
    {
        var position = GetSpawnPosition(blockFromMap);
        Player = CreatePlayer(position);
    }

    private Player CreatePlayer(Vector2 position)
    {
        var player = Resources.Load<Player>("Charapter");
        var playerInstance = Object.Instantiate(player, position, Quaternion.identity);
        return playerInstance;
    }

    private Vector2 GetSpawnPosition(List<List<Cell>> blockFromMap)
    {
        var length0 = blockFromMap.Count;
        var randomX = Random.Range(1, length0);
        var length1 = blockFromMap[randomX].Count;
        var y = blockFromMap[randomX][length1 - 1].Position.y;
        var size = blockFromMap[0][0].Renderer.transform.localScale.y;
        var position = new Vector2(randomX, y + size * 2);
        return position;
    }
}