using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CauldronEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
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

        StopAllParticles();
        _renderer.color = _idleEffect.Color;
        _bubbleMaterial.color = _idleEffect.Color;
        _audioSource.clip = _idleEffect.SoundEffect;
        _audioSource.Play();
        
    }


    private void NewIngredient(Ingredient ingredient)
    {
        var correctMapping = _effectMappings.FirstOrDefault(mapping => mapping.IngredientData == ingredient.IngredientData);
        if (correctMapping != null)
        {
            if (correctMapping.Color != Color.black) {
                _renderer.color = correctMapping.Color;
                _bubbleMaterial.color = correctMapping.Color;
            }
            if (correctMapping.SoundEffect != null) {
                _audioSource.PlayOneShot(correctMapping.SoundEffect);
            }
            if (correctMapping.ParticleEffect != null) {
                correctMapping.ParticleEffect.Play();
            }
        }
    }

    public void PotionCorrect(Potion potion) {
        
    }

    public void WrongIngredient() {
        
    }

    private void StopAllParticles() {
        foreach (EffectMapping em in _effectMappings) {
            if(em.ParticleEffect) em.ParticleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}



[System.Serializable]
public class EffectMapping {
    public IngredientData IngredientData;
    public AudioClip SoundEffect;
    public ParticleSystem ParticleEffect;
    public Color Color;
}
