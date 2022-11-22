using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class RecipeFinder : MonoBehaviour, IInitialize
{
        [SerializeField] private ItemRecipe[] _recipes;
        private readonly List<CraftSlot> _craftSlot = new ();
        private readonly List<Ore> _ores = new ();
        private UnityAction<ItemRecipe> _addRecipe;
        private UnityAction _clear;
        [Inject] private RecipeImage _recipeImage;
        [Inject] private RecipesHelper _recipesHelper;
        [Inject] private CraftButton _craftButton;
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
                BindEvents();
        }

        private async void BindEvents()
        {
                await Task.Delay(500);
                _addRecipe += _recipeImage.Add;
                _addRecipe += _craftButton.UpdateButton;
                _addRecipe += _recipesHelper.Visualize;
                _clear += _recipeImage.Clear;
                _clear += _recipesHelper.Clear;
                _clear += _craftButton.Clear;
                _clear += _recipeVariants.Clear;
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
                                for (int k = 0; k < _recipes[j].Ores.Length; k++) // Для каждой руды в рецепте
                                {
                                        if (_recipes[j].Ores[k].Ore.Equals(_craftSlot[i].GetOre()))
                                                list.Add(_recipes[j]);
                                }
                        }
                }

                foreach (var l in list)
                {
                        Debug.Log(l.Name);
                }
        }

        private void SetInfoToVisualizer(List<ItemRecipe> list)
        {
                if (list.Count > 0)
                {
                        _addRecipe.Invoke(list[0]);
                        _recipeVariants.Add(list.ToArray());
                }
                else
                        _clear.Invoke();
        }
}