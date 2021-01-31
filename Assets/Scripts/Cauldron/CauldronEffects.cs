using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CauldronEffects : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private EffectMapping[] _effectMappings;
    [SerializeField] private EffectMapping _successEffect;
    [SerializeField] private EffectMapping _failureEffect;
    [SerializeField] private EffectMapping _idleEffect;
    
    private void Start()
    {
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(evt => PotionCorrect(evt.Potion));
        MessageBus.Instance.Subscribe<DropIngredientEvent>(evt => NewIngredient(evt.Ingredient));
    }

    public void FreshPotion() 
    {
        _renderer.material.color = Color.cyan;
    }


    private void NewIngredient(Ingredient ingredient)
    {
        var correctMapping = _effectMappings.First(mapping => mapping.IngredientData == ingredient.IngredientData);
        if (correctMapping != null)
        {
            // do effect
        }
        _renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void PotionCorrect(Potion potion) {

    }
}

[System.Serializable]
public class EffectMapping
{
    public IngredientData IngredientData;
    public AudioClip SoundEffect;
    public ParticleSystem ParticleEffect;
}
