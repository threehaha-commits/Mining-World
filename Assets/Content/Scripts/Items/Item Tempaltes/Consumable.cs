using TMPro;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, IStackable
{
    private bool _isFull;
    private Stack _stack;
    private int _stackSize;
    private TMP_Text _stackSizeText;

    bool IStackable.isFull => _isFull;

    Stack IStackable.stack
    {
        get => _stack;
        set => _stack = value;
    }

    int IStackable.stackSize => _stackSize;

    TMP_Text IStackable.stackSizeText
    {
        get => _stackSizeText;
        set => _stackSizeText = value;
    }
}