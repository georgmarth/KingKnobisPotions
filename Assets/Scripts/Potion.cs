using System.Collections.Generic;
using System.Linq;

public class Potion
{
    public Recipe Recipe { get; }
    private readonly List<IngredientData> _ingredients;

    public Potion(Recipe recipe)
    {
        Recipe = recipe;
        _ingredients = new List<IngredientData>();
    }

    public void AddIngredient(IngredientData ingredientData)
    {
        _ingredients.Add(ingredientData);
        if (IsWrong)
            MessageBus.Instance.Publish(new WrongIngredientEvent{Potion = this, IngredientData = ingredientData});
        else if (IsCorrect)
            MessageBus.Instance.Publish(new PotionCorrectEvent{Potion = this});
        else
            MessageBus.Instance.Publish(new CorrectIngredientEvent{Potion = this, IngredientData = ingredientData});
            
    }

    public bool IsCorrect =>
        _ingredients.Count == 2 &&
        _ingredients.Contains(Recipe.ColorIngredient) &&
        _ingredients.Contains(Recipe.SymbolIngredient);

    public bool IsWrong =>
        _ingredients.Count > 2 ||
        _ingredients.Any(ingredient =>
            !ReferenceEquals(Recipe.ColorIngredient, ingredient) &&
            !ReferenceEquals(Recipe.SymbolIngredient, ingredient)) ||
        _ingredients.GroupBy(data => data).Any(grouping => grouping.Count() > 1);
}