using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    private Potion _potion;

    private CauldronEffects _effects;

    public bool canAddIngredient = true;


    public void NewPotion(Recipe recipe) { //subscribe this to recipe created

    }



    public void FlushPotion() { //subscribe to this flushevent -- automatic or by player button

    }

    public void IngredientAdded() { //subscribe to DropIngredient event with a delay corrutine.

    }



    IEnumerator PlayPotionEffects() {
        yield return new WaitForSeconds(2);

        // leen color

        // 
        
    }
}
