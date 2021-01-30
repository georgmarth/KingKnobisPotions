public enum IngredientType {Color, Symbol}

public interface IIngredient
{
    IngredientType IngredientType { get; }
    string Name { get; }
}