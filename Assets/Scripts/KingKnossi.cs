using UnityEngine;
using UnityEngine.UI;

public class KingKnossi : MonoBehaviour
{
    [SerializeField] private Image _effectImage;
    [SerializeField] private SpriteRenderer _potionImage;

    private void Start()
    {
        MessageBus.Instance.Subscribe<NewRecipeEvent>(DisplayRecipe);
    }

    private void DisplayRecipe(NewRecipeEvent evt)
    {
        _effectImage.sprite = evt.Recipe.SymbolIngredient.Symbol;
        _potionImage.color = evt.Recipe.ColorIngredient.Color;
    }
}