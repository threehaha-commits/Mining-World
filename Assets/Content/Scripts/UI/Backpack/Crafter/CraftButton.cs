using UnityEngine;
using UnityEngine.UI;

public class CraftButton : MonoBehaviour, IInitialize
{
    [Inject] private RecipeImage _current;
    private Button _button;
    [SerializeField] private CraftSlot[] _craftSlots;
    
    public void UpdateButton(ItemRecipe recipe)
    {
        if (_current.IsEmpty)
        {
            _button.interactable = false;
            return;
        }

        var index = 0;
        for (int i = 0; i < _craftSlots.Length; i++)
        {
            for (int j = 0; j < recipe.Ores.Length; j++)
            {
                if (_craftSlots[i].GetOre().Equals(recipe.Ores[j].Ore))
                {
                    if(_craftSlots[i].GetStack().Size() >= recipe.Ores[j].Count)
                        index++;
                }
            }
        }

        Debug.Log($"recipe {recipe.Name}");
        if (index == recipe.Ores.Length)
        {
            Debug.Log("success!");
            _button.interactable = true;
        }
        else
        {
            Debug.Log("not success!");
            _button.interactable = false;
        }
        
    }

    public void Clear()
    {
        _button.interactable = false;
    }
    
    void IInitialize.Initialize()
    {
        Bind<CraftButton>.Value(this);
        _craftSlots = FindObjectsOfType<CraftSlot>();
        _button = GetComponent<Button>();
    }
}