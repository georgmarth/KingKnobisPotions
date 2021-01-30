using UnityEngine;

[CreateAssetMenu(menuName = "Create Recipe", fileName = "Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    public string RecipeName;
    public Sprite Image;
    public IngredientType[] Ingredients;
}