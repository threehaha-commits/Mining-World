using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Items/Recipes", order = 0)]
public class ItemRecipe : ScriptableObject
{
        public string Name;
        [Multiline]
        public string Description;

        public Sprite Icon;
        public OreInfo[] Ores;
}