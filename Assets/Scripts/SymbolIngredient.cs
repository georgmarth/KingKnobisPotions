using UnityEngine;

[CreateAssetMenu(menuName = "Create SymbolIngredient", fileName = "SymbolIngredient", order = 0)]
[System.Serializable]
public class SymbolIngredient : IngredientData
{
    [SerializeField] private Sprite _symbol;
    [SerializeField] private string _name;
    public IngredientType IngredientType => IngredientType.Symbol;

    public string Name => _name;
    public Sprite Symbol => _symbol;
}