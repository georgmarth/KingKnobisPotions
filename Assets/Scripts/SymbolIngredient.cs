using UnityEngine;

[System.Serializable]
public class SymbolIngredient : IIngredient
{
    [SerializeField] private Sprite _symbol;
    [SerializeField] private string _name;
    public IngredientType IngredientType => IngredientType.Symbol;

    public string Name => _name;
    public Sprite Symbol => _symbol;
}