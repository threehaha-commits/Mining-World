using UnityEngine;

public class Binder
{
    private readonly BlockFinder _blockFinder;
    private readonly BlockClicker _blockClicker;
    private readonly Player _player;
    
    public Binder()
    {
        CInjectHelper.FindObjectsOnScene();
        _blockFinder = Object.FindObjectOfType<BlockFinder>();
        _blockClicker = Object.FindObjectOfType<BlockClicker>();
        _player = Object.FindObjectOfType<Player>();
    }
    
    public void Bind()
    {
        Bind<Player>.Value(_player);
        Bind<Camera>.Value(Camera.main);
        Bind<BlockFinder>.Value(_blockFinder);
        Bind<BlockClicker>.Value(_blockClicker);
        BindInitialize();
        BindPostInitialize();
    }
    
    private void BindInitialize()
    {
        var iInitializes = Bind<IInitialize>.GetInterfaces();
        foreach (var initialize in iInitializes)
        {
            initialize.Initialize();
        }
    }
    
    private void BindPostInitialize()
    {
        var iPostInitializes = Bind<IPostInitialize>.GetInterfaces();
        foreach (var postInitialize in iPostInitializes)
        {
            postInitialize.PostInitialize();
        }
    }
}