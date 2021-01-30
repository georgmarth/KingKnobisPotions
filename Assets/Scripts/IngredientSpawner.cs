using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Ingredient _ingredient;

    private Transform _spawnArea;
    void Start() {
        _spawnArea = transform.GetChild(0).transform;
        SpawnIngredient();

        MessageBus.Instance.Subscribe<DropIngredientEvent>(evt => TrySpawnIngredient(evt));
    }


    void Update()
    {
        
    }


    private void TrySpawnIngredient(DropIngredientEvent evt) {
        if(_ingredient.IngredientData == evt.Ingredient.IngredientData) {
            SpawnIngredient();
        }
    }
    


    public void SpawnIngredient() {
        var ingredient = Instantiate(_ingredient, _spawnArea.position, _spawnArea.rotation);
    }
}
