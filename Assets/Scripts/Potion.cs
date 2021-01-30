using System.Collections.Generic;
using System.Linq;

public class Potion
{
    public Recipe Recipe { get; }
    private readonly List<IIngredient> _ingredients;

    public Potion(Recipe recipe)
    {
        Recipe = recipe;
        _ingredients = new List<IIngredient>();
    }

    public void AddIngredient(IIngredient Ingredient)
    {
        _ingredients.Add(Ingredient);
        if (IsWrong)
            MessageBus.Instance.Publish(new WrongIngredientEvent{Potion = this, Ingredient = Ingredient});
        else if (IsCorrect)
            MessageBus.Instance.Publish(new PotionCorrectEvent{Potion = this});
    }

    public bool IsCorrect
    {
        get
        {
            return _ingredients.Count == 2 &&
                   _ingredients.Contains(Recipe.ColorIngredient) &&
                   _ingredients.Contains(Recipe.SymbolIngredient);
        }
    }

    public bool IsWrong
    {
        get
        {
            return _ingredients.Count > 2 || _ingredients.Any(ingredient =>
                !ReferenceEquals(Recipe.ColorIngredient, ingredient) &&
                !ReferenceEquals(Recipe.SymbolIngredient, ingredient));
        }
    }
}