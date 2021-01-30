using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Potion
{
    public Recipe Recipe { get; }
    private List<IngredientType> _ingredients;

    public Potion(Recipe recipe)
    {
        Recipe = recipe;
        _ingredients = new List<IngredientType>();
    }

    public void AddIngredient(IngredientType ingredientType)
    {
        _ingredients.Add(ingredientType);
        if (IsWrong)
            MessageBus.Instance.Publish(new WrongIngredientEvent{Potion = this, Ingredient = ingredientType});
        else if (IsCorrect)
            MessageBus.Instance.Publish(new PotionCorrectEvent{Potion = this});
    }

    public bool IsCorrect
    {
        get
        {
            return Recipe.Ingredients.All(recipeIngredient =>
                _ingredients.Count(ingredient => ingredient == recipeIngredient) ==
                Recipe.Ingredients.Count(ingredient => ingredient == recipeIngredient));
        }
    }

    public bool IsWrong
    {
        get
        {
            return _ingredients.Any(ingredient => !Recipe.Ingredients.Contains(ingredient));
        }
    }
}