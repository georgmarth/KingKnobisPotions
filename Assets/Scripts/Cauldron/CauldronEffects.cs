using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CauldronEffects : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceWinAndLoss;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private EffectMapping[] _effectMappings;
    [SerializeField] private EffectMapping _idleEffect;

    [SerializeField] private Material _bubbleMaterial;

    [SerializeField] private AudioClip _successSound;
    [SerializeField] private AudioClip _failureSound;

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
        _idleEffect.ParticleEffect.Play();
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
                _audioSource.clip = correctMapping.SoundEffect;
                _audioSource.PlayDelayed(0.7f);
            }
            if (correctMapping.ParticleEffect != null) {
                correctMapping.ParticleEffect.Play();
            }
        }
    }

    public void PotionCorrect(Potion potion) {
        _audioSourceWinAndLoss.clip = _successSound;
        _audioSourceWinAndLoss.PlayDelayed(0.7f);
    }

    public void WrongIngredient() {
        _audioSourceWinAndLoss.clip = _failureSound;
        _audioSourceWinAndLoss.PlayDelayed(0.7f);
    }

    private void StopAllParticles() {
        foreach (EffectMapping em in _effectMappings) {
            if(em.ParticleEffect) em.ParticleEffect.Stop();
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



