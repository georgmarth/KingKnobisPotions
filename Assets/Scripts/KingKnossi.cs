using UnityEngine;
using UnityEngine.UI;

public class KingKnossi : MonoBehaviour
{
    [SerializeField] private Text _effectText;
    [SerializeField] private Image _potionImage;

    private void Start()
    {
        MessageBus.Instance.Subscribe<NewRecipeEvent>(DisplayRecipe);
    }

    private void DisplayRecipe(NewRecipeEvent evt)
    {
        _effectText.text = evt.Recipe.SymbolIngredient.Name;
        _potionImage.color = evt.Recipe.ColorIngredient.Color;
    }
}