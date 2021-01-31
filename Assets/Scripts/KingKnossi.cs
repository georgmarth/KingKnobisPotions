using UnityEngine;
using UnityEngine.UI;

public class KingKnossi : MonoBehaviour
{
    [SerializeField] private Image _effectImage;
    [SerializeField] private SpriteRenderer _potionImage;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        MessageBus.Instance.Subscribe<NewRecipeEvent>(DisplayRecipe);
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(_ => HideBubble());
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(_ => HideBubble());
    }

    private void HideBubble()
    {
        _animator.SetTrigger("Hide");
    }

    private void DisplayRecipe(NewRecipeEvent evt)
    {
        _effectImage.sprite = evt.Recipe.SymbolIngredient.Symbol;
        _potionImage.color = evt.Recipe.ColorIngredient.Color;
        _animator.SetTrigger("Show");
    }
}