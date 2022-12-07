using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Items/Item Template", order = 1)]
public class ItemTemplate : ScriptableObject, Iitem, ISlotTypeable
{
    public string Name;
    [Multiline]
    public string Description;

    public Sprite Icon;
    [SerializeField] private SlotType slotType;
    SlotType ISlotTypeable.slotType => slotType;
    Sprite Iitem._icon => Icon;

    string Iitem._name => Name;
}