using UnityEngine;

[CreateAssetMenu(menuName = "Create ColorIngredient", fileName = "ColorIngredient", order = 0)]
[System.Serializable]
public class ColorIngredient : IngredientData
{
    [SerializeField] private Color _color;
    [SerializeField] private string _name;
    public IngredientType IngredientType => IngredientType.Color;

    public string Name => _name;
    public Color Color => _color;
}