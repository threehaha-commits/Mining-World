using UnityEngine;

public interface ISlot
{
    Sprite _icon { get; set; }
}

public interface IOreSlot : ISlot
{
    Ore _oreType { get; set; }
}

public interface IItemSlot : ISlot
{
    
}
