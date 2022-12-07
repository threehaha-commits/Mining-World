using System.Collections.Generic;
using UnityEngine;

public class WorldInitializator : MonoBehaviour
{
    private List<List<Cell>> _blockFromMap;
    [SerializeField] private OreCreator _oreCreator;
    [SerializeField] private float[] _valueForOre = { 0f, 0.1f, 0.17f, 0.26f, 0.34f, 0.47f, 0.55f, 0.65f, 0.8f };
    [SerializeField] private float _smooth = 5f;
    
    private void Start()
    {
        var world = new World(_oreCreator, _smooth, _valueForOre);
        world.Create(64);
        var binder = new Binder();
        binder.Bind();
    }
}