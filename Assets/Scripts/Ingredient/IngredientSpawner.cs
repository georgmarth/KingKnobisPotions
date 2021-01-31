using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Ingredient _ingredient;

    private Transform _spawnArea;
    void Start()
    {
        _spawnArea = transform;
        SpawnIngredient();

        MessageBus.Instance.Subscribe<DropIngredientEvent>(TrySpawnIngredient);
    }


    private void TrySpawnIngredient(DropIngredientEvent evt) 
    {
        if(_ingredient.IngredientData == evt.Ingredient.IngredientData) 
        {
            SpawnIngredient();
        }
    }
    private void SpawnIngredient() 
    {
        Instantiate(_ingredient, _spawnArea.position, _spawnArea.rotation);
    }
}
