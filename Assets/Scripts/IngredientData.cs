using UnityEngine;

public enum IngredientType {Color, Symbol}

public abstract class IngredientData : ScriptableObject
{
    IngredientType IngredientType { get; }
    string Name { get; }
}