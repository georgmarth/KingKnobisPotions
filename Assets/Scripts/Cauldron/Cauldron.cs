using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : MonoBehaviour {

    private Potion _potion;
    private CauldronEffects _effects;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] GameObject _ingredientsUI;

    public bool canAddIngredient = true;
    private List<Image> _ingredientsIn;

    public void Awake() {
        MessageBus.Instance.Subscribe<NewRecipeEvent>(evt => NewPotion(evt.Recipe));
        MessageBus.Instance.Subscribe<DropIngredientEvent>(evt => IngredientAdded(evt.Ingredient));
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(evt => FlushPotion());

        _effects = new CauldronEffects(_renderer);

        _ingredientsIn = new List<Image>();

        Debug.Log(_ingredientsUI);
        Debug.Log(_ingredientsUI.transform.childCount);
        Debug.Log(_ingredientsUI.transform.GetChild(0));
        for (int i= 0; i < _ingredientsUI.transform.childCount; i++) {         
            Image child = _ingredientsUI.transform.GetChild(i).gameObject.GetComponent<Image>();
            child.color = new Color(0,0,0,0.5f);
            _ingredientsIn.Add(child);
        }
    }


    public void NewPotion(Recipe recipe) { //subscribe this to recipe created
        _potion = new Potion(recipe);
        _effects.FreshPotion();
    }



    public void FlushPotion() { //subscribe to this flushevent -- automatic or by player button

        _effects.FreshPotion();
    }

    public void IngredientAdded(Ingredient ingredient) { //subscribe to DropIngredient event with a delay corrutine.

        //check ifs
        _effects.NewIngredient();
        _potion.AddIngredient(ingredient.IngredientData);


    }

    public void ResetIngredientUI() {
        
    }


}
