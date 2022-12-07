using TMPro;

interface IStackable
{
    bool isFull { get; }
    Stack stack { get; set; }
    int stackSize { get; }
    TMP_Text stackSizeText { get; set; }
}