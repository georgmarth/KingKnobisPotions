using UnityEngine;

[System.Serializable]
public class ColorIngredient :  IIngredient
{
    [SerializeField] private Color _color;
    [SerializeField] private string _name;
    public IngredientType IngredientType => IngredientType.Color;

    public string Name => _name;
    public Color Color => Color;
}