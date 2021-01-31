using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronEffects 
{
    [SerializeField] SpriteRenderer _renderer;



    public CauldronEffects(SpriteRenderer renderer) {
        _renderer = renderer;

        MessageBus.Instance.Subscribe<PotionCorrectEvent>(evt => PotionCorrect(evt.Potion));
    }

    public void FreshPotion() {
        _renderer.material.color = Color.cyan;
    }


    public void NewIngredient() {
        _renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void PotionCorrect(Potion potion) {

    }
}
