using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Items/Item Template", order = 1)]
public class ItemTemplate : ScriptableObject, IItemSlot
{
    public string Name;
    [Multiline]
    public string Description;

    public Sprite Icon;
    Sprite ISlot._icon
    {
        get => Icon;
        set => Icon = value;
    }
}