using System.Collections.Generic;
using UnityEngine;

public class RecipeFinder : MonoBehaviour, IInitialize
{
        private ItemRecipe[] _recipes;
        private readonly List<CraftSlot> _craftSlot = new ();
        private readonly List<Consumable> _ores = new ();
        [Inject] private RecipeVariantsDwn _recipeVariants;
        
        public void Add(CraftSlot craftSlot)
        {
                if (_ores.Contains(craftSlot.GetOre()))
                        return;
                _craftSlot.Add(craftSlot);
                _ores.Add(craftSlot.GetOre());
                UpdateItems();
        }

        public void Remove(CraftSlot craftSlot)
        {
                _ores.Remove(craftSlot.GetOre());
                _craftSlot.Remove(craftSlot);
        }

        void IInitialize.Initialize()
        {
                Bind<RecipeFinder>.Value(this);
                _recipes = Resources.LoadAll<ItemRecipe>("Recipes");
        }

        public void UpdateItems()
        {
                var list = new List<ItemRecipe>();
                FindRecipes(list);
                SetInfoToVisualizer(list);
        }

        private void FindRecipes(List<ItemRecipe> list)
        {
                for (int i = 0; i < _craftSlot.Count; i++) // Для каждого слота
                {
                        for (int j = 0; j < _recipes.Length; j++) // Для каждого рецепта
                        {
                                for (int k = 0; k < _recipes[j].ConsumableInfos.Length; k++) // Для каждой руды в рецепте
                                {
                                        ISlotable consumable = _craftSlot[i];
                                        var a = consumable.name;
                                        var b = _recipes[j].ConsumableInfos[k].Item.name +"(Clone)";
                                        if (a.Equals(b))
                                                list.Add(_recipes[j]);
                                }
                        }
                }
        }

        private void SetInfoToVisualizer(List<ItemRecipe> list)
        {
                if (list.Count > 0)
                        _recipeVariants.Add(list.ToArray());
                else
                        _recipeVariants.Clear();
        }
}