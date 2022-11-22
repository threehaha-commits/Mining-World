using System.Collections.Generic;
using UnityEngine;

public class WorldInitializator : MonoBehaviour
{
    private List<List<Cell>> _blockFromMap;
    [SerializeField] private OreCreator _oreCreator;
    
    private void Start()
    {
        var world = new World(_oreCreator);
        world.Create(46);
        var binder = new Binder();
        binder.Bind();
    }
}