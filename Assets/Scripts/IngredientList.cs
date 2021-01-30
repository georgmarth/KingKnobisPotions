using UnityEngine;

[CreateAssetMenu(menuName = "Create IngredientList", fileName = "IngredientList", order = 0)]
public class IngredientList : ScriptableObject
{
    [SerializeField] private ColorIngredient[] _colorIngredients;
    [SerializeField] private SymbolIngredient[] _symbolIngredients;

    public ColorIngredient[] ColorIngredients => _colorIngredients;

    public SymbolIngredient[] SymbolIngredients => _symbolIngredients;
}