using UnityEngine;

public class Binder
{
    private readonly BlockFinder _blockFinder;
    private readonly BlockClicker _blockClicker;
    private readonly Backpack[] _backpack;
    private readonly BackpackSlotsFinder _backpackSlotsFinder;
    private readonly Player _player;
    
    public Binder()
    {
        CInjectHelper.FindObjectsOnScene();
        _blockFinder = Object.FindObjectOfType<BlockFinder>();
        _blockClicker = Object.FindObjectOfType<BlockClicker>();
        _backpack = Resources.FindObjectsOfTypeAll<Backpack>();
        _backpackSlotsFinder = Object.FindObjectOfType<BackpackSlotsFinder>();
        _player = Object.FindObjectOfType<Player>();
    }
    
    public void Bind()
    {
        Bind<Player>.Value(_player);
        Bind<Camera>.Value(Camera.main);
        Bind<BlockFinder>.Value(_blockFinder);
        Bind<BlockClicker>.Value(_blockClicker);
        Bind<Backpack>.Value(_backpack[0]);
        Bind<BackpackSlotsFinder>.Value(_backpackSlotsFinder);
        BindInterface();
    }
    
    private void BindInterface()
    {
        var interfacesOnScene = Bind<IInitialize>.GetInterfaces();
        foreach (var @interface in interfacesOnScene)
        {
            @interface.Initialize();
        }
    }
}