using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CauldronEffects : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private EffectMapping[] _effectMappings;
    [SerializeField] private EffectMapping _successEffect;
    [SerializeField] private EffectMapping _failureEffect;
    [SerializeField] private EffectMapping _idleEffect;

    [SerializeField] private Material _bubbleMaterial;

    private void Start()
    {
        MessageBus.Instance.Subscribe<PotionCorrectEvent>(evt => PotionCorrect(evt.Potion));
        MessageBus.Instance.Subscribe<DropIngredientEvent>(evt => NewIngredient(evt.Ingredient));
        MessageBus.Instance.Subscribe<WrongIngredientEvent>(evt => WrongIngredient());
        MessageBus.Instance.Subscribe<NewRecipeEvent>(evt => FreshPotion());
    }

    public void FreshPotion() 
    {
        //stop VfX
        _renderer.color = _idleEffect.Color;
        _bubbleMaterial.color = _idleEffect.Color;
        _idleEffect.ParticleEffect.Play();
        

        
    }


    private void NewIngredient(Ingredient ingredient)
    {
        var correctMapping = _effectMappings.First(mapping => mapping.IngredientData == ingredient.IngredientData);
        if (correctMapping != null)
        {
            if (correctMapping.Color != null) {
                _renderer.material.color = correctMapping.Color;
                _bubbleMaterial.color = correctMapping.Color;
            }
            if (correctMapping.SoundEffect != null) {
                
            }
            if (correctMapping.ParticleEffect != null) {

            }
        }
    }

    public void PotionCorrect(Potion potion) {
        
    }

    public void WrongIngredient() {
        
    }
}

[System.Serializable]
public class EffectMapping
{
    public IngredientData IngredientData;
    public AudioClip SoundEffect;
    public ParticleSystem ParticleEffect;
    public Color Color;
}
